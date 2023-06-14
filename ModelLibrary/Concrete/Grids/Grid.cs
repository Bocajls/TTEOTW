using Microsoft.Xna.Framework;
using ModelLibrary.Abstract.Grids;
using ModelLibrary.Abstract.Types;

namespace ModelLibrary.Concrete.Grids
{
    public class Grid : AGrid
    {
        public Grid(int ID, Vector2 InternalCoordinate, GridBox[,] InternalGrid)
        {
            this.ID = ID;
            this.InternalCoordinate = InternalCoordinate;
            this.InternalGrid = InternalGrid;

            for (var x = 0; x < InternalGrid.GetLength(0); x++)
            {
                for (var y = 0; y < InternalGrid.GetLength(1); y++)
                {
                    if (InternalGrid[x, y] is null)
                    {
                        InternalGrid[x, y] = new GridBox(ID, new List<AType>());
                    }
                }
            }
        }
    }
}