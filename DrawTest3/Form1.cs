
using DrawTest3.Components;
using System.Numerics;

namespace DrawTest3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            Block block1, block2, block3;

            window1.Components.Add(block1 = new Block() { WorldPos = new Vector2(20, 10) });
            window1.Components.Add(block2 = new Block() { WorldPos = new Vector2(100, 10) });
            window1.Components.Add(block3 = new Block() { WorldPos = new Vector2(100, 50) });

            IO io = new IO();
            io.WorldPos = block1.WorldPos + new Vector2(2, 10);
            io.Name = "Somename";
            block1.Blocks.Add(io);


            //window1.Blocks.Add(new DrawTest3.Blocks.Relais() { WorldPos = new Vector2(100, 100) });
        }


    }




}
