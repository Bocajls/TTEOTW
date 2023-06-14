using ModelLibrary.Abstract.Grids;
using ModelLibrary.Abstract.Types;

namespace ModelLibrary.Abstract.PlayerShipComponents
{
    public abstract class AInventory : AType
    {
        public float SizeLimit { get; set; }
        public AGrid Items { get; set; }
    }
}