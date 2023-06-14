using Microsoft.Xna.Framework;
using System;
using ModelLibrary.Abstract.PlayerShipComponents;
using ModelLibrary.Enums;

namespace ModelLibrary.Abstract
{
    public abstract class APlayer
    {
        public Vector2 Coordinates { get; set; } = new Vector2(0, 0);
        public Vector2 Direction { get; set; } = new Vector2(0, 0);
        public float XOffset { get; set; }
        public float YOffset { get; set; }
        public float XVelocity { get; set; }
        public float YVelocity { get; set; }

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
        public void SetOffset(float XO, float YO)
        {
            XOffset = XO;
            YOffset = YO;
        }
        public void UpdateOffset()
        {
            XOffset += XVelocity;
            YOffset += YVelocity;
        }

        public bool WithinBlockPositiveXBoundariesAfterMoving(float XBoundary)
        {
            return XVelocity + XOffset < XBoundary * 1.0f;
        }

        public bool WithinBlockNegativeYBoundariesAfterMoving(float YBboundary)
        {
            return YVelocity + YOffset > YBboundary * -1.0f;
        }

        public bool WithinBlockPositiveYBoundariesAfterMoving(float YBboundary)
        {
            return YVelocity + YOffset < YBboundary * 1.0f;
        }

        public bool WithinBlockNegativeXBoundariesAfterMoving(float XBoundary)
        {
            return XVelocity + XOffset > XBoundary * -1.0f;
        }

        public void UpdateVelocity(Vector2 direction)
        {
            if (direction.X == 0)
            {
                XVelocity = 0.0f;
            }

            if (direction.Y == 0)
            {
                YVelocity = 0.0f;
            }

            if (direction.Y == 1)
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

            if (direction.X == 1)
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

            if (direction.Y == -1)
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

            if (direction.X == -1)
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

        public void ResetOffset()
        {
            XOffset = 0.0f;
            YOffset = 0.0f;
        }

        public void ResetVelocity()
        {
            XVelocity = 0.0f;
            YVelocity = 0.0f;
        }
    }
}