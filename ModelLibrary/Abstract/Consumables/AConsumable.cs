namespace ModelLibrary.Abstract.Consumables
{
    public abstract class AConsumable : IConsumable
    {
        // Abstract since every consumable will have special methods.

        public AConsumable(short ID, string Name, float Weight, float Worth)
        {
            this.ID = ID;
            this.Name = Name;
            this.Weight = Weight;
            this.Worth = Worth;
        }

        public short ID { get; set; }
        public string Name { get; set; }
        public float Weight { get; set; }
        public float Worth { get; set; }
    }
}