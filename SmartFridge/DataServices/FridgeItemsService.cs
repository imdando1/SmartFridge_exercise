using SmartFridge.DataModels;
using SmartFridge.DataServices.Interfaces;
using SmartFridge.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace SmartFridge.DataServices
{
    /// <summary>
    /// This is the supposed SmartFridgeManager class.  I have decided to rename it to FridgeItemsService under DataServices
    /// because it is basically an area where it manipulates the data that comes from the data source provider but doesn't directly 
    /// connect to the data source.
    /// </summary>
    public class FridgeItemsService : IFridgeItemsService
    {

        private readonly IFridgeItemsRepository repo;

        public FridgeItemsService()
        {
            repo = FridgeItemsRepository.GetInstance;
        }

        /// <summary>
        /// Stop tracking a given item. This method is used by the fridge to signal that its
        /// owner will no longer stock this item and thus should not be returned from #getItems()
        /// </summary>
        /// <param name="itemType"></param>
        public void ForgetItem(long itemType)
        {
            var items = repo.GetFridgeItems()
                .Where(i => i.ItemType == itemType).ToList();

            foreach(var i in items)
            {
                i.IsActive = false;
            }
        }

        /// <summary>
        /// Returns the fill factor for a given item type to be displayed to the owner. Unless all available containers are
        /// empty, this method should only consider the non-empty containers
        /// when calculating the overall fillFactor for a given item.
        /// </summary>
        /// <param name="itemType"></param>
        /// <returns></returns>
        public double GetFillFactor(long itemType)
        {
            return repo.GetFridgeItems()
                .Where(i => i.ItemType == itemType)
                .Select(i => i.FillFactor)
                .DefaultIfEmpty(0)
                .Average();
        }

        /// <summary>
        /// Returns a list of items based on their fill factor. This method is used by the
        /// fridge to display items that are running low and need to be replenished.
        /// i.e. GetItems(0.5) will return all items with a fill factor of 50% or less full.
        /// </summary>
        /// <param name="fillFactor"></param>
        /// <returns></returns>
        public List<FridgeItem> GetItems(double fillFactor)
        {
            return repo.GetFridgeItems()
                .Where(i => i.FillFactor <= fillFactor).ToList();
        }

        /// <summary>
        /// Adds a new item into the SmartFridge. By default, new items are tracked by the SmartFridge.
        /// PS: The original HandleItemAdded method has been moved to the EventHandlers area.
        /// </summary>
        /// <param name="itemType"></param>
        /// <param name="itemUUID"></param>
        /// <param name="name"></param>
        /// <param name="fillFactor"></param>
        public void AddItem(long itemType, string itemUUID, string name, double fillFactor)
        {
            var itemToAdd = new FridgeItem(itemUUID, itemType, name, fillFactor);
            repo.AddFridgeItem(itemToAdd);
        }

        /// <summary>
        /// Removes an item from the SmartFridge by the itemUUID.
        /// PS: The original HandleItemRemoved has been moved to the EventHandlers area.
        /// </summary>
        /// <param name="itemUUID"></param>
        public void RemoveItemByUUID(string itemUUID)
        {
            repo.RemoveFridgeItem(itemUUID);
        }

        /// <summary>
        /// Removes all items from the SmartFridge
        /// </summary>
        public void ClearSmartFridge()
        {
            repo.WipeData();
        }
    }
}
