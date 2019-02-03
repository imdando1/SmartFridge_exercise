using SmartFridge.DataServices;
using SmartFridge.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartFridge.EventHandlers
{
    class EventConfig
    {
        public static void Configure()
        {
            // Configuration for hooking the handlers and the emitters.
            var fridgeItemsRepo = Repositories.FridgeItemsRepository.GetInstance;
            var fridgeItemsEventHandler = new FridgeItemsEventHandler();

            fridgeItemsRepo.ItemAdded += fridgeItemsEventHandler.HandleItemAdded;
            fridgeItemsRepo.ItemRemoved += fridgeItemsEventHandler.HandleItemRemoved;
        }
    }
}
