using SmartFridge.DataServices;
using SmartFridge.EventHandlers;
using System;

namespace SmartFridge
{
    class Program
    {
        static void Main(string[] args)
        {
            // using this section to demonstrate HandleItemRemoved and HandleItemAdded event handlers.
            EventConfig.Configure();
            var service = new FridgeItemsService();
            service.AddItem(24601, "ABC1", "PB", 0.3);
            service.AddItem(24602, "ABC2", "Jelly", 0.4);
            Console.ReadKey();
        }
    }
}
