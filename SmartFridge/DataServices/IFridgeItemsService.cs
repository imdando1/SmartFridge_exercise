using SmartFridge.DataModels;
using System.Collections.Generic;

namespace SmartFridge.DataServices.Interfaces
{
    interface IFridgeItemsService
    {
        List<FridgeItem> GetItems(double fillFactor);
        double GetFillFactor(long itemType);
        void ForgetItem(long itemType);
        void AddItem(long itemType, string itemUUID, string name, double fillFactor);
        void RemoveItemByUUID(string itemUUID);
    }
}
