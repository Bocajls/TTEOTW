namespace ModelLibrary.Abstract.Types
{
    public interface IType
    {
        public string Name { get; set; }
        public short ID { get; set; }
        public float Worth { get; set; }
        public float Weight { get; set; }
    }
}