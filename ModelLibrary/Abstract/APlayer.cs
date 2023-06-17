using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ModelLibrary.Abstract.PlayerShipComponents;
using ModelLibrary.Enums;

namespace ModelLibrary.Abstract
{
    public abstract class APlayer
    {
        public Vector2 Coordinates { get; set; } = new Vector2(0, 0);
        public Vector2 Direction { get; set; } = new Vector2(0, 0);
        public float XVelocity { get; set; }
        public float YVelocity { get; set; }

        public float XOffset { get; set; }
        public float YOffset { get; set; }

        public PlayerOrientation Orientation
        {
            get
            {
                if (Direction == new Vector2(0, 1)) return PlayerOrientation.Down;
                if (Direction == new Vector2(-1, 0)) return PlayerOrientation.Left;
                if (Direction == new Vector2(0, -1)) return PlayerOrientation.Up;
                if (Direction == new Vector2(1, 0)) return PlayerOrientation.Right;
                return PlayerOrientation.Base;
            }
        }

        public Vector2 SetDirectionFromInput(KeyboardState state)
        {
            Vector2 direction = new(0, 0);

            if (state.IsKeyDown(Keys.Up))
            {
                direction = new Vector2(direction.X, -1);
            }
            else if (state.IsKeyDown(Keys.Down))
            {
                direction = new Vector2(direction.X, 1);
            }
            else if (state.IsKeyDown(Keys.Left))
            {
                direction = new Vector2(-1, direction.Y);
            }
            else if (state.IsKeyDown(Keys.Right))
            {
                direction = new Vector2(1, direction.Y);
            }

            this.Direction = direction;
            return direction;
        }

        public bool Mining { get; set; } = false;
        public string Name { get; set; } = "Undefined";
        public double Cash { get; set; } = 0.0f;
        public AEngine Engine { get; set; }
        public AHull Hull { get; set; }
        public ADrill Drill { get; set; }
        public AInventory Inventory { get; set; }
        public AThruster Thruster { get; set; }
        public AFuelTank FuelTank { get; set; }
        public float Weight { get; } = 0.0f;

        public float MaximumActiveVelocity => Math.Abs(XVelocity) > Math.Abs(YVelocity) ? Math.Abs(XVelocity) : Math.Abs(YVelocity);

        public void UpdateOffset()
        {
            XOffset += XVelocity;
            YOffset += YVelocity;
        }

        public int BlocksToMove(float pixels)
        {
            switch (Orientation)
            {
                case PlayerOrientation.Left:
                    return (int)Math.Floor(Math.Abs(XOffset / pixels));
                case PlayerOrientation.Up:
                    return (int)Math.Floor(Math.Abs(YOffset / pixels));
                case PlayerOrientation.Right:
                    return (int)Math.Floor(XOffset / pixels);
                case PlayerOrientation.Down:
                    return (int)Math.Floor(YOffset / pixels);
            }
            return 0;
        }

        public void SubstractOffset(int pixels)
        {
            switch (Orientation)
            {
                case PlayerOrientation.Left:
                    XOffset += pixels;
                    break;
                case PlayerOrientation.Up:
                    YOffset += pixels;
                    break;
                case PlayerOrientation.Right:
                    XOffset -= pixels;
                    break;
                case PlayerOrientation.Down:
                    YOffset -= pixels;
                    break;
            }
        }

        // TODO: Instead of 0.0f, decrease by value.
        // TODO: Incorporate gravity.
        public void UpdateVelocity()
        {
            if (Direction.X == 0)
            {
                XVelocity = 0.0f;
            }

            if (Direction.Y == 0)
            {
                YVelocity = 0.0f;
            }

            if (Direction.Y == 1)
            {
                if (YVelocity < 0)
                {
                    YVelocity = 0.0f;
                }

                YVelocity += Thruster.Acceleration;

                if (YVelocity > Thruster.Speed)
                {
                    YVelocity = Thruster.Speed;
                }

                return;
            }

            if (Direction.X == 1)
            {
                if (XVelocity < 0)
                {
                    XVelocity = 0.0f;
                }

                XVelocity += Thruster.Acceleration;

                if (XVelocity > Thruster.Speed)
                {
                    XVelocity = Thruster.Speed;
                }

                return;
            }

            if (Direction.Y == -1)
            {
                if (YVelocity > 0)
                {
                    YVelocity = 0.0f;
                }

                YVelocity -= Thruster.Acceleration;

                if (Math.Abs(YVelocity) > Thruster.Speed)
                {
                    YVelocity = Thruster.Speed * -1;
                }

                return;
            }

            if (Direction.X == -1)
            {
                if (XVelocity > 0)
                {
                    XVelocity = 0.0f;
                }

                XVelocity -= Thruster.Acceleration;

                if (Math.Abs(XVelocity) > Thruster.Speed)
                {
                    XVelocity = Thruster.Speed * -1;
                }

                return;
            }

        }

        public void ResetVelocity()
        {
            XVelocity = 0.0f;
            YVelocity = 0.0f;
        }

        public void ResetOffset()
        {
            XOffset = 0.0f;
            YOffset = 0.0f;
        }
    }
}