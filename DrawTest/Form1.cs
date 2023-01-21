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
            splitContainer1.Panel1.Controls.Add(drawUi);
            drawUi.Dock = DockStyle.Fill;
            drawUi.DrawComponents.Add(new Rectangle { Position = new Vector2(10, 10) });
            drawUi.DrawComponents.Add(new Rectangle { Position = new Vector2(200, 200) });

            listBox1.DataSource = drawUi.DrawComponents;
        }


	}
}