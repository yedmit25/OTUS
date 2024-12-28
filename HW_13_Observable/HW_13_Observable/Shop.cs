using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace HW_13_Observable
{

    public class Shop
    {
        private ObservableCollection<Item> items;
        public event Action<Item, string> ItemChanged;

        public Shop()
        {
            items = new ObservableCollection<Item>();
            items.CollectionChanged += (sender, e) =>
            {
                if (e.NewItems != null)
                {
                    foreach (Item newItem in e.NewItems)
                    {
                        ItemChanged?.Invoke(newItem, "добавлен");
                    }
                }

                if (e.OldItems != null)
                {
                    foreach (Item oldItem in e.OldItems)
                    {
                        ItemChanged?.Invoke(oldItem, "удален");
                    }
                }
            };
        }

        public void AddItem(string name)
        {
            int newId = items.Count > 0 ? items.Max(item => item.Id) + 1 : 1;
            var newItem = new Item(newId, name);
            items.Add(newItem);
        }

        public void RemoveItem(int id)
        {
            var itemToRemove = items.FirstOrDefault(item => item.Id == id);
            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);
            }
            else
            {
                Console.WriteLine("Товар не найден");
            }
        }
    }
}
