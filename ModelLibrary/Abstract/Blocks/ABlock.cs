using ModelLibrary.Abstract.Types;

namespace ModelLibrary.Abstract.Blocks
{
    public abstract class ABlock : AType
    {
        public bool Destroyed { get; }
        public bool Ethereal { get; set; }
        public float Hardness { get; set; }
        public float MaximumHealth { get; set; }
        public float CurrentHealth { get; set; }
        public ABlockInfo? Info { get; set; }
    }
}