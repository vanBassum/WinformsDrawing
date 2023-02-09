using DrawTest3.Components;
using DrawTest3.Drawing;
using DrawTest3.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static DrawTest3.Controls.InputCollector;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace DrawTest3.Controls
{
    public class Window : PictureBox
    {
        internal List<Component> Components { get; }
        InputCollector InputCollector { get; }
        Scaling Scaling { get; set; }
        MyRectangle SelectionRectangle { get; set; }
        public Window()
        {
            InputCollector = new InputCollector(this);
            Components = new List<Component>();
            Scaling  = new Scaling();
            SelectionRectangle = MyRectangle.Empty;
            InputCollector.OnInput += InputCollector_OnInput;
        }


        IEnumerable<Component> GetAllComponents(IEnumerable<Component>? blocks = null)
        {
            blocks ??= Components;
            foreach (var block in blocks)
            {
                yield return block;
                foreach (var child in GetAllComponents(block.Blocks))
                    yield return child;
            }
        }


        enum States
        {
            Idle,
            LeftDown,
            MoveSelected,
            SelectRectangle,
            IOSelected,
        }
        States state = States.Idle;
        States nextState = States.Idle;
        Vector2 previousMouseWorldPos, leftDownMouseWorldPos;
        bool OnEntry = true;
        private void InputCollector_OnInput(object? sender, InputCollector.Info info)
        {
            var mouseWorldPos = Scaling.ToWorld(info.ScreenPos);
            var allComponents = GetAllComponents();
            var selected = allComponents.Where(c => c.Selected);
            var colliding = allComponents.Where(c => c.Collider.Collides(mouseWorldPos));
            var collidingSelected = colliding.Where(c => c.Selected);

            if (info.MouseActions == MouseActions.LeftDown)
                leftDownMouseWorldPos = mouseWorldPos;

            switch (state)
            {
                case States.Idle:
                    if (info.MouseActions == MouseActions.Move)
                    {
                        bool mayHover = true;
                        foreach (var component in allComponents.Reverse())
                            mayHover &= !(component.Hover = component.Collider.Collides(mouseWorldPos) && mayHover);
                    }
                    if (info.MouseActions == MouseActions.LeftDown)
                        nextState = States.LeftDown;
                    break;
                case States.LeftDown:
                    if (info.MouseActions == MouseActions.Move)
                    {
                        if (collidingSelected.Any())
                            nextState = States.MoveSelected;
                        else if (!colliding.Any())
                            nextState = States.SelectRectangle;
                    }
                    else if (info.MouseActions == MouseActions.LeftUp)
                    {
                        bool maySelect = true;
                        foreach (var component in allComponents.Reverse())
                            maySelect &= !(component.Selected = component.Collider.Collides(mouseWorldPos) && maySelect);
                        nextState = States.Idle;
                    }
                    break;
                case States.MoveSelected:
                    foreach (var s in selected.Where(s=>s.Moveable))
                        MoveRecursive(s, mouseWorldPos - previousMouseWorldPos);
                    if (info.MouseActions == MouseActions.LeftUp)
                        nextState = States.Idle;
                    break;
                case States.SelectRectangle:
                    if (info.MouseActions == MouseActions.Move)
                    {
                        SelectionRectangle = MyRectangle.FromPoints(leftDownMouseWorldPos, mouseWorldPos);
                        foreach (var block in allComponents)
                        {
                            var parent = allComponents.FirstOrDefault(b=>b.Blocks.Contains(block));
                            bool parentSelected = parent?.Selected ?? false;
                            block.Selected = block.Collider.Collides(SelectionRectangle) && !parentSelected;
                        }
                    }
                    if (info.MouseActions == MouseActions.LeftUp)
                    {
                        SelectionRectangle = MyRectangle.Empty;
                        nextState = States.Idle;
                    }
                    break;
            }

            previousMouseWorldPos = mouseWorldPos;
            OnEntry = nextState != state;
            if (OnEntry)
            {
                Debug.WriteLine($"{state} => {nextState}");
                state = nextState;
            }
            Redraw();
        }

        void MoveRecursive(Component b, Vector2 diff)
        {
            b.WorldPos += diff;
            foreach (var child in b.Blocks)
                MoveRecursive(child, diff);
        }


        protected override void OnPaint(PaintEventArgs pe)
        {
            MyGraphics graphics = new MyGraphics(pe.Graphics);
            graphics.Scaling = Scaling;
            foreach (var block in Components)
                block.Draw(graphics);

            SelectionRectangle?.Draw(graphics);
        }


        protected override void OnMouseWheel(MouseEventArgs e) 
        { 
            _ = (e.Delta > 0) ? (Scaling.Scale *= 1.1f) : (Scaling.Scale *= 0.9f);
            Redraw();
        } 
        void Redraw() => this.Refresh();
    }
}
