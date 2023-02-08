using System.Numerics;

namespace DrawTest2
{

    public class Window : PictureBox
    {
        public List<Ctrl> Ctrls { get; } = new List<Ctrl>();
        Rect selector = Rect.Empty;
        public Scaling Scaling { get; set; } = new Scaling();

        protected override void OnPaint(PaintEventArgs pe)
        {
            MyGraphics g = new MyGraphics(pe.Graphics);
            g.Scaling = Scaling;
            foreach (var ctrl in Ctrls)
                ctrl.Draw(g);
            selector.Draw(g);
        }

        IEnumerable<Ctrl> AllControls(IEnumerable<Ctrl> ctrls)
        {
            foreach (var c in ctrls)
            {
                yield return c;
                foreach (var cc in AllControls(c.Controls))
                    yield return cc;
                
            }
        }



        enum States
        {
            Idle,
            Down,
            MoveSelected,
            SelectRectangle,
        }
        States state = States.Idle;
        Vector2 pPos, pDown;
        void Actions(InputInfo info)
        {
            var worldPos = Scaling.ToWorld(info.ScreenPos);
            States nextState = state;
            var allControls = AllControls(Ctrls).ToArray();
            var selected = allControls.Where(c => c.Selected);
            var colliding = allControls.Where(c => c.Collider.Collides(worldPos));
            var collidingSelected = colliding.Where(c => c.Selected);
            if (info.MouseActions == MouseActions.Down)
                pDown = worldPos;
            switch (state)
            {
                case States.Idle:
                    if (info.MouseActions == MouseActions.Move)
                    {
                        foreach (var c in allControls)
                            c.Hover = c.Collider.Collides(worldPos);
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
                        bool noSelected = true;
                        foreach (var ctrl in allControls)
                        {
                            ctrl.Selected = ctrl.Collider.Collides(worldPos) && noSelected;
                            noSelected &= !ctrl.Selected;
                        }
                        nextState = States.Idle;
                    }
                    break;
                case States.MoveSelected:
                    foreach (var s in selected)
                        s.Position += worldPos - pPos;
                    if (info.MouseActions == MouseActions.Up)
                        nextState = States.Idle;
                    break;
                case States.SelectRectangle:
                    selector = Rect.FromPoints(pDown, worldPos);
                    foreach (var ctrl in allControls)
                        ctrl.Selected = ctrl.Collider.Collides(selector);

                    if (info.MouseActions == MouseActions.Up)
                    {
                        selector = Rect.Empty;
                        nextState = States.Idle;
                    }
                    break;
            }

            if(nextState != state)
            {
                state = nextState;
            }
            pPos = worldPos;
            Redraw();
        }


        protected override void OnMouseUp(MouseEventArgs e) => Actions(new InputInfo(e, MouseActions.Up));
        protected override void OnMouseDown(MouseEventArgs e) => Actions(new InputInfo(e, MouseActions.Down));
        protected override void OnMouseMove(MouseEventArgs e) => Actions(new InputInfo(e, MouseActions.Move));
        void Redraw() => this.Refresh();

        
    }
}





