using DrawTest.Draw;
using System.ComponentModel;
using System.Numerics;

namespace DrawTest
{
    public partial class Form1 : Form
    {
        DrawUi drawUi = new DrawUi();
        public Form1()
        {
            InitializeComponent();
            this.Controls.Add(drawUi);
            drawUi.Dock = DockStyle.Fill;

            drawUi.DrawComponents.Add(new Rectangle { Location = new Vector2(10, 10) });
            drawUi.DrawComponents.Add(new Rectangle { Location = new Vector2(200, 200) });
        }
    }
}