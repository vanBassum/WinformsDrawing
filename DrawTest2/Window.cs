using System.Numerics;

namespace DrawTest2
{

    public class Window : PictureBox
    {
        public List<Ctrl> Ctrls { get; } = new List<Ctrl>();
        Rect selector = Rect.Empty;
        Line connection = Line.Empty;
        public Scaling Scaling { get; set; } = new Scaling();

        protected override void OnPaint(PaintEventArgs pe)
        {
            MyGraphics g = new MyGraphics(pe.Graphics);
            g.Scaling = Scaling;
            foreach (var ctrl in Ctrls)
                ctrl.Draw(g);
            selector.Draw(g);
            connection.Draw(g);
        }

        class CtrlInfo
        {
            public Ctrl Ctrl { get; set; }
            //public int Layer { get; set; }
            public Vector2 WorldOffset { get; set; }

            public CtrlInfo(Ctrl ctrl, int layer, Vector2 worldOffset)
            {
                Ctrl = ctrl;
                //Layer = layer;
                WorldOffset = worldOffset;
            }
        }


        IEnumerable<CtrlInfo> AllControls(IEnumerable<Ctrl> ctrls, int layer, Vector2 offset)
        {
            foreach (var c in ctrls)
            {
                yield return new CtrlInfo(c, layer, offset);
                foreach (var cc in AllControls(c.Controls, layer + 1, offset + c.Position))
                    yield return cc;
                
            }
        }



        enum States
        {
            Idle,
            Down,
            MoveSelected,
            SelectRectangle,
            IOSelected,
        }
        States state = States.Idle;
        States nextState = States.Idle;
        CtrlInfo? selectedIO;
        bool OnEntry = true;
        Vector2 pPos, pDown;

        void Actions(InputInfo info)
        {
            var mouseWorldPos = Scaling.ToWorld(info.ScreenPos);
            var allControls = AllControls(Ctrls, 0, Vector2.Zero);
            var selected = allControls.Where(c => c.Ctrl.Selected);
            var colliding = allControls.Where(c => c.Ctrl.Collider.Collides(mouseWorldPos - c.WorldOffset));
            var collidingSelected = colliding.Where(c => c.Ctrl.Selected);

            if (info.MouseActions == MouseActions.Down)
                pDown = mouseWorldPos;

            switch (state)
            {
                case States.Idle:
                    if (info.MouseActions == MouseActions.Move)
                    {
                        bool mayHover = true;
                        foreach (var c in allControls.Reverse())
                            mayHover &= !(c.Ctrl.Hover = c.Ctrl.Collider.Collides(mouseWorldPos - c.WorldOffset) && mayHover);
                    }
                    if (info.MouseActions == MouseActions.Down)
                        nextState = States.Down;
                    break;
                case States.Down:
                    if (info.MouseActions == MouseActions.Move)
                    {
                        if (collidingSelected.Any())
                            nextState = States.MoveSelected;
                        else if (!colliding.Any())
                            nextState = States.SelectRectangle;
                    }
                    else if (info.MouseActions == MouseActions.Up)
                    {
                        bool maySelect = true;
                        foreach (var c in allControls.Reverse())
                            maySelect &= !(c.Ctrl.Selected = c.Ctrl.Collider.Collides(mouseWorldPos - c.WorldOffset) && maySelect);
                        selectedIO = selected.FirstOrDefault(c=>c.Ctrl.Selected && c.Ctrl.IsIO);
                        if (selectedIO != null)
                            nextState = States.IOSelected;
                        else
                            nextState = States.Idle;
                    }
                    break;
                case States.MoveSelected:
                    foreach (var s in selected.Where(c=>c.Ctrl.Moveable))
                        s.Ctrl.Position += mouseWorldPos - pPos;
                    if (info.MouseActions == MouseActions.Up)
                        nextState = States.Idle;
                    break;
                case States.SelectRectangle:
                    selector = Rect.FromPoints(pDown, mouseWorldPos);
                    foreach (var c in Ctrls)
                    {
                        c.Selected = c.Collider.Collides(selector);
                    }

                    if (info.MouseActions == MouseActions.Up)
                    {
                        selector = Rect.Empty;
                        nextState = States.Idle;
                    }
                    break;
                case States.IOSelected:
                    if (selectedIO == null) return;

                    if (OnEntry)
                    {
                        foreach(var c in allControls)
                            c.Ctrl.CompatibleToSelected = c.Ctrl.IsCompatible(selectedIO.Ctrl);
                        
                    }
                    if(info.MouseActions == MouseActions.Move)
                        connection = new Line(selectedIO.Ctrl.Position + selectedIO.WorldOffset, mouseWorldPos);
                    
                    if(info.MouseActions == MouseActions.Down)
                    {
                        var col = colliding.Reverse().FirstOrDefault();
                        if(col != null)
                        {

                        }
                        connection = Line.Empty;
                        nextState = States.Idle;
                        foreach (var c in allControls)
                            c.Ctrl.Selected = c.Ctrl.Hover = c.Ctrl.CompatibleToSelected = false;
                    }

                    break;
            }

            OnEntry = nextState != state;
            if (OnEntry)
                state = nextState;
            pPos = mouseWorldPos;
            Redraw();
        }


        protected override void OnMouseUp(MouseEventArgs e) => Actions(new InputInfo(e, MouseActions.Up));
        protected override void OnMouseDown(MouseEventArgs e) => Actions(new InputInfo(e, MouseActions.Down));
        protected override void OnMouseMove(MouseEventArgs e) => Actions(new InputInfo(e, MouseActions.Move));
        void Redraw() => this.Refresh();

        
    }
}





