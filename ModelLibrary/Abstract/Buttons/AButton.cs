using Microsoft.Xna.Framework;

namespace ModelLibrary.Abstract.Buttons
{
    public abstract class AButton
    {
        public int ID { get; set; }
        public Vector2 InternalCoordinate { get; set; }
        public ASize Size { get; set; } // Size in pixels
    }
}