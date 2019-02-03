using SmartFridge.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridge.EventHandlers
{
    class FridgeItemsEventHandler
    {
        /// <summary>
        /// This method is called whenever a new item has been added to the data source.  
        /// It sends a simple message telling that the new item has been added.
        /// PS: This is the re-purposed from the original description.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void HandleItemAdded(object source, ItemEventArgs e)
        {
            Console.WriteLine("[{0}] {1} has been added to the SmartFridge", e.FridgeItem.ItemUUID, e.FridgeItem.Name);
        }

        /// <summary>
        /// This method is called whenever a new item has been removed from the data source.  
        /// It sends a simple message telling that the new item has been added.
        /// PS: This is the re-purposed from the original description.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        public void HandleItemRemoved(object source, ItemEventArgs e)
        {
            Console.WriteLine("[{0}] {1} has been removed from the SmartFridge", e.FridgeItem.ItemUUID, e.FridgeItem.Name);
        }
    }
}
