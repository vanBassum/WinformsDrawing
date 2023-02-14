
using Draw;
using Draw.Elements;
using Draw.Manipulators;

namespace DrawTest3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Visualizer window = new Visualizer();
            this.Controls.Add(window);
            window.Dock = DockStyle.Fill;

            GraphView graphView = new GraphView();  
            window.AddElement(graphView);
            graphView.AddManipulator(new ContentZoomer());


            var fps = new System.Windows.Forms.Timer();
            fps.Interval = 1000/30;
            fps.Tick += (s, e) => window.Refresh();
            fps.Start();
        }


    }




}
