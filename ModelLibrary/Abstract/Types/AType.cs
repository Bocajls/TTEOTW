namespace ModelLibrary.Abstract.Types
{
    public abstract class AType : IType
    {
        // Abstract since every game item type will have special methods and attributes.

        public AType()
        {

        }

        public AType(short ID, string Name, float Worth, float Weight)
        {
            this.ID = ID;
            this.Name = Name;
            this.Worth = Worth;
            this.Weight = Weight;
        }

        public short ID { get; set; }
        public string Name { get; set; }
        public float Worth { get; set; }
        public float Weight { get; set; }

        /* Move below to ICraftingRecipe and impl. model for CraftingRecipe */

        //public bool Craftable { get; set; }

        //public IType[,] Recipe { get; set; }
    }
}