using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Reflection;

namespace DrawTest
{
	public interface IIdentifiable
	{
		Guid Id { get; }
	}

	public class HistoryManager<T> where T : IIdentifiable, INotifyPropertyChanged
	{
		List<ChangeInfo> changes = new List<ChangeInfo>();
		ObservableCollection<T> parent;
		bool record = true;
		public HistoryManager(ObservableCollection<T> parent)
		{
			this.parent = parent;
			parent.CollectionChanged += Parent_CollectionChanged;
		}

		private void Parent_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
		{
			if (!record)
				return;

			ChangeInfo info = new ChangeInfo(this, e);
			changes.Add(info);
		}

		private void Item_PropertyChanged(object? sender, PropertyChangedEventArgs e)
		{
			if (!record)
				return;
			if(sender is T item)
			{
				ChangeInfo info = new ChangeInfo(item, e);
				changes.Add(info);
			}
		}

		public enum ChangeTypes
		{
			Add,
			Remove,
			Property,
		}



		class ChangeInfo
		{
			public Guid Id { get; set; }
			public Dictionary<string, object?> Properties { get; } = new Dictionary<string, object?>();
			public Type ObjectType { get; set; }
			public ChangeTypes ChangeType { get; set; }

			public ChangeInfo(HistoryManager<T> parent, NotifyCollectionChangedEventArgs e)
			{
				switch (e.Action)
				{
					case NotifyCollectionChangedAction.Add:
						if (e.NewItems?[0] is T nitem)
						{
							nitem.PropertyChanged += parent.Item_PropertyChanged;
							ChangeType = ChangeTypes.Add;
							ObjectType = nitem.GetType();
							Id = nitem.Id;
							foreach (var prop in ObjectType.GetProperties())
								Properties.Add(prop.Name, prop.GetValue(nitem));
						}
						break;
					case NotifyCollectionChangedAction.Remove:
						if (e.OldItems?[0] is T oitem)
						{
							ChangeType = ChangeTypes.Remove;
							Id = oitem.Id;
						}
						break;

				}
			}

			

			public ChangeInfo(T item, PropertyChangedEventArgs e)
			{
				ChangeType = ChangeTypes.Property;
				Id = item.Id;
				if(item.GetType().GetProperty(e.PropertyName) is PropertyInfo pi)
					Properties.Add(e.PropertyName, pi.GetValue(item));
			}
		}





		/*
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

                    case HistoryItemDelete historyItemDelete:
                        Undo(historyItemDelete);
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

		private void Undo(HistoryItemDelete item)
		{
            if(Activator.CreateInstance(item.Type) is T newItem) 
            {
				foreach (var propVal in item.Properties)
				{
					var prop = item.Type.GetProperty(propVal.Key);
					if (prop == null) continue;
					prop.SetValue(newItem, propVal.Value);
				}
				parent.Add(newItem);
			}
		}
        */




		/*
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


		class HistoryItemDelete : IHistoryItem
		{
			public Guid Id { get; private set; }
			public string Name { get; set; }
            public Type Type { get; set; }
			public Dictionary<string, object?> Properties { get; } = new Dictionary<string, object?>();
			public HistoryItemDelete(BindingList<DrawComponent> list, ListChangedEventArgs e)
			{
				var component = list[e.NewIndex];
				Id = component.Id;
				Name = component.Name;
                Type = component.GetType();
				foreach (var prop in component.GetType().GetProperties())
				{
					Properties.Add(prop.Name, prop.GetValue(component));
				}
			}

			public override string ToString() => $"[Deleted] {Name}";
		}
        */
	}
}
