using SmartFridge.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridge.Repositories
{
    /// <summary>
    /// Custom EventArgs so that it can hold a FridgeItem when event is broad-casted.
    /// </summary>
    public class ItemEventArgs : EventArgs
    {
        public FridgeItem FridgeItem { get; set; }
    }

    /// <summary>
    /// Data source for the fridge items.  A singleton was used as a mock database. 
    /// </summary>
    public class FridgeItemsRepository : IFridgeItemsRepository
    {
        // Singleton instance
        private static FridgeItemsRepository instance = null;
        // Mock database
        private List<FridgeItem> items = new List<FridgeItem>();

        // Events
        public event EventHandler<ItemEventArgs> ItemAdded;
        public event EventHandler<ItemEventArgs> ItemRemoved;

        /// <summary>
        /// Returns a singleton instance when called.
        /// </summary>
        public static FridgeItemsRepository GetInstance
        {
            get
            {
                if (instance == null) instance = new FridgeItemsRepository();
                return instance;
            }
        }

        /// <summary>
        /// Gets a single FridgeItem by itemUUID.  Returns null if none is found.
        /// </summary>
        /// <param name="itemUUID"></param>
        /// <returns></returns>
        public FridgeItem GetFridgeItem(string itemUUID)
        {
            return items.FirstOrDefault(item => item.ItemUUID == itemUUID);
        }

        /// <summary>
        /// Gets a list of FridgeItem that is active(being tracked).
        /// </summary>
        /// <returns></returns>
        public List<FridgeItem> GetFridgeItems()
        {
            return items.Where(item => item.IsActive == true).ToList();
        }

        /// <summary>
        /// Adds a new FridgeItem into the data source. When a new item is added, it emits an OnItemAdded event.
        /// </summary>
        /// <param name="item"></param>
        public void AddFridgeItem(FridgeItem item)
        {
            items.Add(item);
            OnItemAdded(item);
        }

        /// <summary>
        /// Removes a FridgeItem by itemUUID. When an item is removed, it emits and OnItemRemoved event.
        /// </summary>
        /// <param name="itemUUID"></param>
        public void RemoveFridgeItem(string itemUUID)
        {
            var itemToRemove = GetFridgeItem(itemUUID);
            if (itemToRemove != null)
            {
                items.Remove(itemToRemove);
            }
            OnItemRemoved(itemToRemove);
        }

        public void WipeData()
        {
            items.Clear();
        }

        /// <summary>
        /// Emitter used for when a new item has been added.
        /// </summary>
        /// <param name="item"></param>
        protected virtual void OnItemAdded(FridgeItem item)
        {
            if (ItemAdded != null)
                ItemAdded(this, new ItemEventArgs() { FridgeItem = item });
        }

        /// <summary>
        /// Emitter used for when an item has been removed.
        /// </summary>
        /// <param name="item"></param>
        protected virtual void OnItemRemoved(FridgeItem item)
        {
            if (ItemAdded != null)
                ItemRemoved(this, new ItemEventArgs() { FridgeItem = item });
        }
    }
}
