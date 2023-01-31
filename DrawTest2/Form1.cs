using System.Numerics;

namespace DrawTest2
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
            window1.Ctrls.Add(new Button() { Position = new Vector2(20, 10) });
            window1.Ctrls.Add(new Button() { Position = new Vector2(100, 10) });
            window1.Ctrls.Add(new Button() { Position = new Vector2(100, 50) });
        }

        private void PictureBox1_Paint(object? sender, PaintEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }

    public class Window : PictureBox
    {
        public List<Ctrl> Ctrls { get; } = new List<Ctrl>();

        protected override void OnPaint(PaintEventArgs pe)
        {
            MyGraphics g = new MyGraphics(pe.Graphics);
            foreach (var ctrl in Ctrls)
                ctrl.Draw(g);
        }


        InputAction[] inputActions = new InputAction[] { 
            new SelectInstance(),
        };


        protected override void OnMouseUp(MouseEventArgs e)
        {
            var pos = e.Location.ToVector2();
            var col = Ctrls.Where(c => c.Collides(pos)).ToArray();
            foreach (var a in inputActions)
                a.OnMouseUp(pos, col);
            Redraw();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            var pos = e.Location.ToVector2();
            var col = Ctrls.Where(c => c.Collides(pos)).ToArray();
            foreach (var a in inputActions)
                a.OnMouseDown(pos, col);
            Redraw();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            var pos = e.Location.ToVector2();
            var col = Ctrls.Where(c => c.Collides(pos)).ToArray();
            foreach (var a in inputActions)
                a.OnMouseMove(pos, col);
            Redraw();
        }


        void Redraw()
        {
            this.Refresh();
        }
    }

    public class InputAction
    {
        public virtual void OnMouseUp(Vector2 pos, Ctrl[] collide) { }
        public virtual void OnMouseDown(Vector2 pos, Ctrl[] collide) { }
        public virtual void OnMouseMove(Vector2 pos, Ctrl[] collide) { }
    }


    public class SelectInstance : InputAction
    {
        public override void OnMouseDown(Vector2 pos, Ctrl[] collide)
        {
            foreach (var c in collide)
                c.IsSelected = true;
        }

    }




    //Recognize the action, 
    // Left down for select instance
    // Left down + move to move that instance

    // Left down, no instance
    // Left down + move to make selector





    public abstract class Ctrl
    {
        public Vector2 Position { get; set; } = Vector2.Zero;
        public List<Ctrl> Controls { get; } = new List<Ctrl>();
        public bool IsSelected { get; set; } = false;
        public bool Moveable { get; set; } = true;
        public abstract bool Collides(Vector2 point);
        public void Draw(MyGraphics g)
        {
            g.Offset += Position;
            OnDraw(g);
            foreach (Ctrl ctrl in Controls)
                ctrl.Draw(g);
            g.Offset -= Position;
        }
        protected abstract void OnDraw(MyGraphics g);
    }


    public class Button : Ctrl
    {
        public Vector2 Size { get; set; } = new Vector2(100, 50);
        Output output = new Output();

        public Button()
        {
            output.Position = new Vector2(90, 20);
            Controls.Add(output);
        }


        protected override void OnDraw(MyGraphics g)
        {
            Pen pen = IsSelected ? Pens.Red : Pens.Black;  


            g.DrawLines(pen,
                new Vector2[] {
                    Vector2.Zero,
                    Vector2.UnitX * Size,
                    Vector2.One * Size,
                    Vector2.UnitY * Size,
                    Vector2.Zero,
                });
        }

        public override bool Collides(Vector2 point)
        {
            return point.X > Position.X
                && point.X < (Position.X + Size.X)
                && point.Y > Position.Y
                && point.Y < (Position.Y + Size.Y);
        }
    }

    public class Output : Ctrl
    {
        public Vector2 Size { get; set; } = new Vector2(10, 10);

        public override bool Collides(Vector2 point) => false;

        protected override void OnDraw(MyGraphics g)
        {
            g.DrawLines(Pens.Black,
                new Vector2[] {
                    Vector2.Zero,
                    new Vector2(Size.X, Size.Y / 2),
                    Vector2.UnitY * Size,
                    Vector2.Zero,
                });
        }
    }



    public class MyGraphics
    {
        Graphics g;
        public Vector2 Offset { get; set; } = Vector2.Zero;
        public Scaling Scaling { get; set; } = new Scaling();

        public MyGraphics(Graphics g)
        {
            this.g = g;
        }

        public void DrawLines(Pen pen, Vector2[] points)
        {
            g.DrawLines(pen, points.Select(pt => (Scaling.ToScreen(pt + Offset)).ToPoint()).ToArray());
        }
    }


    public class Scaling
    {
        public Vector2 Scale { get; set; } = Vector2.One;
        public Vector2 Offset { get; set; } = Vector2.Zero;

        public Vector2 ToScreen(Vector2 worldPos) => worldPos * Scale + Offset;

    }


    public static class VectorExt
    {
        public static Vector2 ToVector2(this Point point) => new Vector2(point.X, point.Y);
        public static Vector2 ToVector2(this Size point) => new Vector2(point.Width, point.Height);
        public static Size ToSize(this Vector2 point) => new Size((int)point.X, (int)point.Y);
        public static Point ToPoint(this Vector2 point) => new Point((int)point.X, (int)point.Y);
        public static SizeF ToSizeF(this Vector2 point) => new SizeF(point.X, point.Y);
        public static PointF ToPointF(this Vector2 point) => new PointF(point.X, point.Y);

        public static IEnumerable<Point> ToPoints(this Vector2[] points) => points.Select(p => p.ToPoint());

    }
}
