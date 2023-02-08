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
            window1.Ctrls.Add(new Relais() { Position = new Vector2(100, 100) });
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
