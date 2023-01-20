namespace DrawTest.Draw
{
    public class DrawUi : PictureBox
    {
        public DrawComponentCollection DrawComponents { get; }
        public Scaling Scaling { get; }
        public InputController InputController { get; }

        public DrawUi()
        {
            InputController = new InputController(this);
            DrawComponents = new DrawComponentCollection(this);
            Scaling = new Scaling();
            
            this.Paint += PaintForeground;
            this.BackColor = Color.Transparent;
        }

        private void PaintForeground(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            foreach (var component in DrawComponents)
            {
                component.Draw(g);
            }
        }

        public void Redraw()
        {
            this.Refresh();
        }
    }




}