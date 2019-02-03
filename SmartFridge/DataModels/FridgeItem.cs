namespace SmartFridge.DataModels
{
    public class FridgeItem
    {
        // Constructor overrides.  
        public FridgeItem() {}
        public FridgeItem(string itemUUID, long itemType, string name, double fillFactor, bool isActive = true)
        {
            this.ItemUUID = itemUUID;
            this.ItemType = itemType;
            this.Name = name;
            this.FillFactor = fillFactor;
            this.IsActive = isActive;       // using soft delete to be used for item tracking
        }

        // Properties - In an app that connects to the database, I would have also created an Id property
        // to serve as a primary key.
        public string ItemUUID { get; set; }
        public long ItemType { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        // Full Properties
        private double fillFactor;
        public double FillFactor
        {
            get { return fillFactor; }
            set {
                // There are many ways to handle an out of range inputs but I have decided to keep it simple for now.
                if (value > 1)
                {
                    value = 1;
                }
                else if (value < 0)
                {
                    value = 0;
                }
                fillFactor = value;
            }
        }

    }
}
