using System.Collections;

namespace DrawTest.Draw
{
    public class DrawComponentCollection : IEnumerable<DrawComponent>
    {
        public event EventHandler<DrawComponent?> SelectedItemChanged;
		private int selectedIndex = -1;
		public int SelectedIndex 
        { 
            get => selectedIndex; 
            set 
            { 
                selectedIndex = value;
                SelectedItemChanged?.Invoke(this, SelectedComponent);
			}
        }

        public DrawComponent? SelectedComponent 
        {
            get => components.ElementAtOrDefault(SelectedIndex);
            set => SelectedIndex = value == null ? -1 : components.IndexOf(value);
        }



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
