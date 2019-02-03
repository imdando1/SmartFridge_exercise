using System;
using System.Collections.Generic;
using SmartFridge.DataModels;

namespace SmartFridge.Repositories
{
    public interface IFridgeItemsRepository
    {
        event EventHandler<ItemEventArgs> ItemAdded;
        event EventHandler<ItemEventArgs> ItemRemoved;

        void AddFridgeItem(FridgeItem item);
        FridgeItem GetFridgeItem(string itemUUID);
        List<FridgeItem> GetFridgeItems();
        void RemoveFridgeItem(string itemUUID);
        void WipeData();
    }
}