using ModelLibrary.Abstract.Types;

namespace ModelLibrary.Abstract.PlayerShipComponents
{
    public abstract class AFuelTank : AType
    {
        public float Fuel { get; set; }
        public float Capacity { get; set; }
    }
}