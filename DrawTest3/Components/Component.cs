using DrawTest3.Collision;
using DrawTest3.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace DrawTest3.Components
{
    public abstract class Component
    {
        public List<Component> Blocks { get; } = new List<Component>();
        public Vector2 WorldPos { get; set; } = Vector2.Zero;
        public abstract ICollider Collider { get; }
        public bool Moveable { get; set; } = true;
        public bool Hover { get; set; }
        public bool Selected { get; set; }

        protected Pen DefaultPen
        {
            get => (Selected, Hover) switch
            {
                (true, true)  => Pens.Red,
                (false, true) => Pens.Orange,
                (true, false) => Pens.OrangeRed,
                _ => Pens.Black,
            };
        }

        public void Draw(MyGraphics g)
        {
            OnDraw(g);
            foreach (var block in Blocks)
                block.Draw(g);
        }
        protected abstract void OnDraw(MyGraphics g);
    }



}
