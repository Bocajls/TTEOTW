using ModelLibrary.Abstract;

namespace ModelLibrary.Concrete
{
    public class Size : ASize
    {
        public Size(float Width, float Height)
        {
            this.Width = Width;
            this.Height = Height;
        }
    }
}