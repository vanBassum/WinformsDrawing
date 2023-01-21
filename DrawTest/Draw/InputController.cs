using System.Diagnostics;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace DrawTest.Draw
{
    public class InputController
    {
        DrawComponent? hoverComponent;
        Vector2 screenPosAtMouseDown;
        Vector2 offsetAtDown;
        //InterceptKeys keyInterceptor;

		public InputController(DrawUi parent)
        {
            parent.MouseDown += Parent_MouseDown;
            parent.MouseUp += Parent_MouseUp;
            parent.MouseMove += Parent_MouseMove;
            parent.MouseWheel += Parent_MouseWheel;
			parent.KeyDown += Parent_KeyDown;
			//keyInterceptor = new InterceptKeys();

		}

		private void Parent_KeyDown(object? sender, KeyEventArgs e)
		{
            if (sender is DrawUi parent)
            {
                if (e.Modifiers == Keys.Control)
                {
                    if (e.KeyCode == Keys.D)
                    {
                        if (hoverComponent?.Clone() is DrawComponent clone)
                        {
                            while (parent.DrawComponents.Any(c => c.Position == clone.Position))
                                clone.Position += Vector2.One * 10;
                            parent.DrawComponents.Add(clone);
                            parent.Refresh();
                        }
                    }
				}
            }
		}

		private void Parent_MouseWheel(object? sender, MouseEventArgs e)
        {
            if (sender is DrawUi parent)
            {
                if (e.Delta > 0)
                    parent.Scaling.Scale *= 1.2f;
                else
                    parent.Scaling.Scale *= 0.8f;
                parent.Refresh();
            }
        }

        private void Parent_MouseMove(object? sender, MouseEventArgs e)
        {
            if (sender is DrawUi parent)
            {
                var screenPos = e.Location.ToVector2();
                var worldPos = parent.Scaling.GetWorldPosition(screenPos);


                if (e.Button == MouseButtons.None)
                {
                    var prefCol = hoverComponent;
                    hoverComponent = parent.DrawComponents.Reverse().FirstOrDefault(a => a.CheckCollision(worldPos));

                    if (prefCol == null && hoverComponent != null)
                        hoverComponent.MouseEnter(parent, screenPos);
                    else if (prefCol != null && hoverComponent == null)
                        prefCol.MouseLeave(parent, screenPos);

                }

                hoverComponent?.MouseMove(parent, screenPos);

                if (e.Button.HasFlag(MouseButtons.Right))
                {
                    parent.Scaling.Offset = offsetAtDown + screenPos - screenPosAtMouseDown;
                    parent.Redraw();
                }
            }
        }

		private void Parent_MouseUp(object? sender, MouseEventArgs e)
        {
            if (sender is DrawUi parent)
            {
                var screenPos = e.Location.ToVector2();
                hoverComponent?.MouseUp(parent, screenPos);
            }
        }

        private void Parent_MouseDown(object? sender, MouseEventArgs e)
        {
            if (sender is DrawUi parent)
            {
                var screenPos = e.Location.ToVector2();
                screenPosAtMouseDown = screenPos;
                offsetAtDown = parent.Scaling.Offset;
                hoverComponent?.MouseDown(parent, screenPos);
            }
        }
    }


	class InterceptKeys
	{
		private const int WH_KEYBOARD_LL = 13;
		private const int WM_KEYDOWN = 0x0100;
		private static LowLevelKeyboardProc _proc = HookCallback;
		private static IntPtr _hookID = IntPtr.Zero;

        static InterceptKeys singleton;

		public class KeyInfo
        { 
            public int KeyCode { get; set; } 


        }

        public event EventHandler<KeyInfo> KeyDown;

        public InterceptKeys()
		{
			_hookID = SetHook(_proc);
            singleton = this;
		}

        ~InterceptKeys()
        {
			UnhookWindowsHookEx(_hookID);
		}

		private static IntPtr SetHook(LowLevelKeyboardProc proc)
		{
			using (Process curProcess = Process.GetCurrentProcess())
			using (ProcessModule curModule = curProcess.MainModule)
			{
				return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
					GetModuleHandle(curModule.ModuleName), 0);
			}
		}

		private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

		private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
		{
			if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
			{
				int vkCode = Marshal.ReadInt32(lParam);

                KeyInfo keyInfo = new KeyInfo
                {
                    KeyCode = vkCode,
                };
                singleton.KeyDown?.Invoke(singleton, keyInfo);
			}
			return CallNextHookEx(_hookID, nCode, wParam, lParam);
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr SetWindowsHookEx(int idHook,
			LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool UnhookWindowsHookEx(IntPtr hhk);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
			IntPtr wParam, IntPtr lParam);

		[DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern IntPtr GetModuleHandle(string lpModuleName);
	}
}