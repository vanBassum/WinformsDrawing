using System.ComponentModel;
using System.Drawing;
using System.Xml.Linq;
using Draw.Elements;

namespace Draw
{
    public class Visualizer : PictureBox
    {
        private VisualElement RootElement { get; set; } = new VisualElement();
        private InputCollector InputCollector { get; }
        
        public Visualizer()
        {
            InputCollector = new InputCollector(this);
            InputCollector.OnInput += (s, e) => RootElement.Input(e);
        }


        protected override void OnPaint(PaintEventArgs pe)
        {
            MyGraphics graphics = new MyGraphics(pe.Graphics);
            RootElement.Draw(graphics);
        }

        public void AddElement(VisualElement element)
        {
            RootElement.AddElement(element);
        }
    }
}