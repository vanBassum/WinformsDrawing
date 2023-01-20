using System.Collections;

namespace DrawTest.Draw
{
    public class DrawComponentCollection : IEnumerable<DrawComponent>
    {
        private List<DrawComponent> components = new List<DrawComponent>();
        public IEnumerator<DrawComponent> GetEnumerator() => this.components.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => this.components.GetEnumerator();
        DrawUi Parent { get; set; }
        public DrawComponentCollection(DrawUi parent)
        {
            Parent = parent;
        }

        public void Add(DrawComponent component)
        {
            component.Parent = Parent;
            components.Add(component);
        }
    }
}
