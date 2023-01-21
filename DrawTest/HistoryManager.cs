using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;
using DrawTest.Draw;

namespace DrawTest
{
    public interface IIdentifiable
    { 
        Guid Id { get; }
    }

    public class HistoryManager<T> where T : IIdentifiable
	{
        List<IHistoryItem> historyItems = new List<IHistoryItem>();
        BindingList<T> parent;
        bool record = true;
        public HistoryManager(BindingList<T> parent)
        { 
            this.parent = parent;
            parent.ListChanged += DrawComponents_ListChanged;
        }

        private void DrawComponents_ListChanged(object? sender, ListChangedEventArgs e)
        {
            if (!record)
                return;
            if (sender is BindingList<DrawComponent> list)
            {
                var change = GetHistoryItem(list, e);
                if (change != null)
                    historyItems.Add(change);
            }
        }


        IHistoryItem? GetHistoryItem(BindingList<DrawComponent> list, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    return new HistoryItemNew(list, e);
                case ListChangedType.ItemChanged:
                    return new HistoryItemChange(list, e);
                default:
                    return null;
            }
        }

        public void Undo()
        {
            record = false;
            IHistoryItem? change = historyItems.LastOrDefault();

            if (change != null)
            {
                historyItems.Remove(change);
                switch (change)
                {
                    case HistoryItemNew changeNew:
                        Undo(changeNew);
                        break;

                    case HistoryItemChange changeProperty:
                        Undo(changeProperty);
                        break;
                }
            }
            record = true;
        }

        private void Undo(HistoryItemNew item)
        {
            var comp = parent.FirstOrDefault(a => a.Id == item.Id);
            if (comp != null)
                parent.Remove(comp);
        }

        private void Undo(HistoryItemChange item)
        {
            if (item.PropertyName == null)
                return;

            var obj = parent.FirstOrDefault(a => a.Id == item.Id);
            var property = obj?.GetType()?.GetProperty(item.PropertyName);
            if (property == null)
                return;

            var last = historyItems.LastOrDefault(a => a.Id == item.Id && a is HistoryItemChange c && c.PropertyName == item.PropertyName);
            if (last is HistoryItemChange lastPC)
            {
                property.SetValue(obj, lastPC.NewValue);
                return;
            }

            var created = historyItems.LastOrDefault(a => a.Id == item.Id && a is HistoryItemNew);

            if (created is HistoryItemNew createdNew)
            {
                if (createdNew.Properties.TryGetValue(item.PropertyName, out var val))
                {
                    property.SetValue(obj, val);
                    return;
                }
            }
        }


        interface IHistoryItem
        {
            Guid Id { get; }
        }

        class HistoryItemChange : IHistoryItem
        {
            public Guid Id { get; private set; }
            public string Name { get; set; }
            public string? PropertyName { get; set; }
            public object? NewValue { get; set; }

            public HistoryItemChange(BindingList<DrawComponent> list, ListChangedEventArgs e)
            {
                var component = list[e.NewIndex];
                Id = component.Id;
                Name = component.Name;
                PropertyName = e.PropertyDescriptor?.Name;
                NewValue = e.PropertyDescriptor?.GetValue(component);
            }

            public override string ToString() => $"[Change] {Name}.{PropertyName} = {NewValue}";
        }

        class HistoryItemNew : IHistoryItem
        {
            public Guid Id { get; private set; }
            public string Name { get; set; }
            public Dictionary<string, object?> Properties { get; } = new Dictionary<string, object?>();
            public HistoryItemNew(BindingList<DrawComponent> list, ListChangedEventArgs e)
            {
                var component = list[e.NewIndex];
                Id = component.Id;
                Name = component.Name;
                foreach (var prop in component.GetType().GetProperties())
                {
                    Properties.Add(prop.Name, prop.GetValue(component));
                }
            }

            public override string ToString() => $"[Added] {Name}";
        }
    }
}
