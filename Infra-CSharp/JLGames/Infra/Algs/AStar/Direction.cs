using System;
using System.Runtime.CompilerServices;

namespace JLGames.Infra.AStar
{
    /// <summary>
    /// 3D Direction
    /// 三维方向
    /// </summary>
    public enum Direction3D
    {
        X0_Y0_Z0 = 0, // 水平方向：中心 ----------------
        X0_Y1_Z0, // 水平方向：↑
        X1_Y1_Z0, // 水平方向：↗
        X1_Y0_Z0, // 水平方向：→
        X1_Y__Z0, // 水平方向：↘
        X0_Y__Z0, // 水平方向：↓
        X__Y__Z0, // 水平方向：↙
        X__Y0_Z0, // 水平方向：←
        X__Y1_Z0, // 水平方向：↖
        X0_Y0_Z1, // Z增加方向：中心 ----------------
        X0_Y1_Z1, // Z增加方向：↑
        X1_Y1_Z1, // Z增加方向：↗
        X1_Y0_Z1, // Z增加方向：→
        X1_Y__Z1, // Z增加方向：↘
        X0_Y__Z1, // Z增加方向：↓
        X__Y__Z1, // Z增加方向：↙
        X__Y0_Z1, // Z增加方向：←
        X__Y1_Z1, // Z增加方向：↖
        X0_Y0_Z_, // Z减小方向：中心 ----------------
        X0_Y1_Z_, // Z减小方向：↑
        X1_Y1_Z_, // Z减小方向：↗
        X1_Y0_Z_, // Z减小方向：→
        X1_Y__Z_, // Z减小方向：↘
        X0_Y__Z_, // Z减小方向：↓
        X__Y__Z_, // Z减小方向：↙
        X__Y0_Z_, // Z减小方向：←
        X__Y1_Z_, // Z减小方向：↖
        None, // None
    }

    /// <summary>
    /// 2D Direction
    /// 二维方向
    /// 注意：二维数据的视角坐标，向下Y+1, 对应为Up方向
    /// </summary>
    public enum Direction2D
    {
        Center = Direction3D.X0_Y0_Z0,

        North = Direction3D.X0_Y1_Z0,
        EastNorth = Direction3D.X1_Y1_Z0,
        East = Direction3D.X1_Y0_Z0,
        EastSouth = Direction3D.X1_Y__Z0,
        South = Direction3D.X0_Y__Z0,
        WestSouth = Direction3D.X__Y__Z0,
        West = Direction3D.X__Y0_Z0,
        WestNorth = Direction3D.X__Y1_Z0,

        Up = Direction3D.X0_Y1_Z0,
        RightUp = Direction3D.X1_Y1_Z0,
        Right = Direction3D.X1_Y0_Z0,
        RightDown = Direction3D.X1_Y__Z0,
        Down = Direction3D.X0_Y__Z0,
        LeftDown = Direction3D.X__Y__Z0,
        Left = Direction3D.X__Y0_Z0,
        LeftUp = Direction3D.X__Y1_Z0
    }

    /// <summary>
    /// Direction group
    /// 方向集合
    /// </summary>
    public class DirectionGroup
    {
        private int[] m_Directions;

        public int[] Directions => m_Directions;

        public void SetDirrections(Direction2D[] directions)
        {
            m_Directions = new int[directions.Length];
            for (var i = 0; i < directions.Length; i++)
            {
                m_Directions[i] = (int) directions[i];
            }
        }

        public void SetDirrections(Direction3D[] directions)
        {
            m_Directions = new int[directions.Length];
            for (var i = 0; i < directions.Length; i++)
            {
                m_Directions[i] = (int) directions[i];
            }
        }
    }

    /// <summary>
    /// Direction offset value
    /// 方向偏移值
    /// </summary>
    public struct DirectionValue : IEquatable<DirectionValue>
    {
        public int OffsetX, OffsetY, OffsetZ;

        public DirectionValue UnitValue
        {
            get
            {
                if (0 == OffsetX && 0 == OffsetY && 0 == OffsetZ)
                {
                    return this;
                }

                if (OffsetX == 1 || OffsetY == 1 || OffsetZ == 1 || OffsetX == -1 || OffsetY == -1 || OffsetZ == -1)
                {
                    return this;
                }

                var min = int.MaxValue;
                min = OffsetX == 0 ? min : Math.Min(min, Math.Abs(OffsetX));
                min = OffsetY == 0 ? min : Math.Min(min, Math.Abs(OffsetY));
                min = OffsetZ == 0 ? min : Math.Min(min, Math.Abs(OffsetZ));

                if (min <= 1)
                {
                    return this;
                }

                var remainderX = OffsetX == 0 ? 0 : OffsetX % min;
                var remainderY = OffsetY == 0 ? 0 : OffsetY % min;
                var remainderZ = OffsetZ == 0 ? 0 : OffsetZ % min;
                if (0 != remainderX || 0 != remainderY || 0 != remainderZ)
                {
                    return this;
                }

                var x = OffsetX / min;
                var y = OffsetY / min;
                var z = OffsetZ / min;
                return new DirectionValue {OffsetX = x, OffsetY = y, OffsetZ = z};
            }
        }

        public override string ToString()
        {
            return $"{{OffsetX={OffsetX},OffsetY={OffsetY},OffsetZ={OffsetZ}}}";
        }

        public override bool Equals(object obj)
        {
            return obj is DirectionValue && Equals((DirectionValue) obj);
        }

        public override int GetHashCode()
        {
            return OffsetX.GetHashCode() ^ OffsetY.GetHashCode() << 2 ^ OffsetZ.GetHashCode() << 4;
        }

        public bool Equals(DirectionValue other)
        {
            return OffsetX == other.OffsetX && OffsetY == other.OffsetY && OffsetZ == other.OffsetZ;
        }

        public bool DirectionEquals(DirectionValue other)
        {
            if (Equals(other))
            {
                return true;
            }

            return UnitValue.Equals(other.UnitValue);
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator !=(DirectionValue b, DirectionValue c)
        {
            return !b.Equals(c);
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator ==(DirectionValue b, DirectionValue c)
        {
            return b.Equals(c);
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static DirectionValue operator +(DirectionValue b, DirectionValue c)
        {
            return new DirectionValue
            {
                OffsetX = b.OffsetX + c.OffsetX,
                OffsetY = b.OffsetY + c.OffsetY,
                OffsetZ = b.OffsetZ + c.OffsetZ
            };
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static DirectionValue operator -(DirectionValue b, DirectionValue c)
        {
            return new DirectionValue
            {
                OffsetX = b.OffsetX - c.OffsetX,
                OffsetY = b.OffsetY - c.OffsetY,
                OffsetZ = b.OffsetZ - c.OffsetZ
            };
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static DirectionValue operator *(DirectionValue b, int scale)
        {
            return new DirectionValue
            {
                OffsetX = b.OffsetX * scale,
                OffsetY = b.OffsetY * scale,
                OffsetZ = b.OffsetZ * scale
            };
        }
    }

    /// <summary>
    /// 3D Orientation with Weights
    /// 带权值的三维方向
    /// </summary>
    public struct DirectionVector : IEquatable<DirectionVector>
    {
        public Direction3D Direction;
        public int Len;
        public int Vector;

        public DirectionValue Value => DirectionsStatic.BasicDirectionValue[(int) Direction];

        public DirectionValue RealValue => Value * Len;

        public int OffsetX => Value.OffsetX;
        public int OffsetY => Value.OffsetY;
        public int OffsetZ => Value.OffsetZ;
        public int OffsetV => Vector;

        public override string ToString()
        {
            return $"{{{Direction},{Value},Vector={Vector}";
        }

        public override bool Equals(object obj)
        {
            return obj is DirectionVector && Equals((DirectionVector) obj);
        }

        public override int GetHashCode()
        {
            return Direction.GetHashCode() ^ Len.GetHashCode() << 2 ^ Vector.GetHashCode() << 4;
        }

        public bool Equals(DirectionVector other)
        {
            return Direction == other.Direction && Value == other.Value && Vector == other.Vector;
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator !=(DirectionVector b, DirectionVector c)
        {
            return !b.Equals(c);
        }

        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator ==(DirectionVector b, DirectionVector c)
        {
            return b.Equals(c);
        }
    }

    /// <summary>
    /// Some default common direction combinations
    /// 一些默认常用方向组合
    /// </summary>
    public static class DirectionsStatic
    {
        /// <summary>
        /// 2D 8 directions
        /// 二维8方向
        /// </summary>
        public static readonly int[] DefaultDirections2D =
        {
            (int) Direction2D.North,
            (int) Direction2D.EastNorth,
            (int) Direction2D.East,
            (int) Direction2D.EastSouth,
            (int) Direction2D.South,
            (int) Direction2D.WestSouth,
            (int) Direction2D.West,
            (int) Direction2D.WestNorth
        };

        /// <summary>
        /// 3D 26 directions
        /// 三维26方向
        /// </summary>
        public static readonly int[] DefaultDirections3D =
        {
            (int) Direction3D.X0_Y1_Z0, (int) Direction3D.X1_Y1_Z0, (int) Direction3D.X1_Y0_Z0,
            (int) Direction3D.X1_Y__Z0, (int) Direction3D.X0_Y__Z0, (int) Direction3D.X__Y__Z0,
            (int) Direction3D.X__Y0_Z0, (int) Direction3D.X__Y1_Z0,
            (int) Direction3D.X0_Y0_Z1, (int) Direction3D.X0_Y1_Z1, (int) Direction3D.X1_Y1_Z1,
            (int) Direction3D.X1_Y0_Z1, (int) Direction3D.X1_Y__Z1, (int) Direction3D.X0_Y__Z1,
            (int) Direction3D.X__Y__Z1, (int) Direction3D.X__Y0_Z1, (int) Direction3D.X__Y1_Z1,
            (int) Direction3D.X0_Y0_Z_, (int) Direction3D.X0_Y1_Z_, (int) Direction3D.X1_Y1_Z_,
            (int) Direction3D.X1_Y0_Z_, (int) Direction3D.X1_Y__Z_, (int) Direction3D.X0_Y__Z_,
            (int) Direction3D.X__Y__Z_, (int) Direction3D.X__Y0_Z_, (int) Direction3D.X__Y1_Z_
        };

        /// <summary>
        /// 2D oblique 4 directions
        /// 二维斜向4方向
        /// </summary>
        public static readonly int[] ObliqueDirections2D =
        {
            (int) Direction2D.EastNorth, (int) Direction2D.EastSouth, (int) Direction2D.WestSouth,
            (int) Direction2D.WestNorth
        };

        /// <summary>
        /// 3D oblique 14 directions
        /// 三维斜向14方向
        /// </summary>
        public static readonly int[] ObliqueDirections3D =
        {
            (int) Direction3D.X0_Y1_Z0, (int) Direction3D.X1_Y0_Z0, (int) Direction3D.X0_Y__Z0,
            (int) Direction3D.X__Y0_Z0,
            (int) Direction3D.X0_Y0_Z1, (int) Direction3D.X0_Y1_Z1, (int) Direction3D.X1_Y0_Z1,
            (int) Direction3D.X0_Y__Z1, (int) Direction3D.X__Y0_Z1,
            (int) Direction3D.X0_Y0_Z_, (int) Direction3D.X0_Y1_Z_, (int) Direction3D.X1_Y0_Z_,
            (int) Direction3D.X0_Y__Z_,
            (int) Direction3D.X__Y0_Z_
        };

        /// <summary>
        /// base direction offset value
        /// 基本方向偏移值
        /// </summary>
        public static readonly DirectionValue[] BasicDirectionValue =
        {
            new DirectionValue {OffsetX = 0, OffsetY = 0, OffsetZ = 0}, // 水平方向：中心 ----------------
            new DirectionValue {OffsetX = 0, OffsetY = 1, OffsetZ = 0}, // 水平方向：↑
            new DirectionValue {OffsetX = 1, OffsetY = 1, OffsetZ = 0}, // 水平方向：↗
            new DirectionValue {OffsetX = 1, OffsetY = 0, OffsetZ = 0}, // 水平方向：→
            new DirectionValue {OffsetX = 1, OffsetY = -1, OffsetZ = 0}, // 水平方向：↘
            new DirectionValue {OffsetX = 0, OffsetY = -1, OffsetZ = 0}, // 水平方向：↓
            new DirectionValue {OffsetX = -1, OffsetY = -1, OffsetZ = 0}, // 水平方向：↙
            new DirectionValue {OffsetX = -1, OffsetY = 0, OffsetZ = 0}, // 水平方向：←
            new DirectionValue {OffsetX = -1, OffsetY = 1, OffsetZ = 0}, // 水平方向：↖
            new DirectionValue {OffsetX = 0, OffsetY = 0, OffsetZ = 1}, // Z增加方向：中心 ----------------
            new DirectionValue {OffsetX = 0, OffsetY = 1, OffsetZ = 1}, // Z增加方向：↑
            new DirectionValue {OffsetX = 1, OffsetY = 1, OffsetZ = 1}, // Z增加方向：↗
            new DirectionValue {OffsetX = 1, OffsetY = 0, OffsetZ = 1}, // Z增加方向：→
            new DirectionValue {OffsetX = 1, OffsetY = -1, OffsetZ = 1}, // Z增加方向：↘
            new DirectionValue {OffsetX = 0, OffsetY = -1, OffsetZ = 1}, // Z增加方向：↓
            new DirectionValue {OffsetX = -1, OffsetY = -1, OffsetZ = 1}, // Z增加方向：↙
            new DirectionValue {OffsetX = -1, OffsetY = 0, OffsetZ = 1}, // Z增加方向：←
            new DirectionValue {OffsetX = -1, OffsetY = 1, OffsetZ = 1}, // Z增加方向：↖
            new DirectionValue {OffsetX = 0, OffsetY = 0, OffsetZ = -1}, // Z减小方向：中心 ----------------
            new DirectionValue {OffsetX = 0, OffsetY = 1, OffsetZ = -1}, // Z减小方向：↑
            new DirectionValue {OffsetX = 1, OffsetY = 1, OffsetZ = -1}, // Z减小方向：↗
            new DirectionValue {OffsetX = 1, OffsetY = 0, OffsetZ = -1}, // Z减小方向：→
            new DirectionValue {OffsetX = 1, OffsetY = -1, OffsetZ = -1}, // Z减小方向：↘
            new DirectionValue {OffsetX = 0, OffsetY = -1, OffsetZ = -1}, // Z减小方向：↓
            new DirectionValue {OffsetX = -1, OffsetY = -1, OffsetZ = -1}, // Z减小方向：↙
            new DirectionValue {OffsetX = -1, OffsetY = 0, OffsetZ = -1}, // Z减小方向：←
            new DirectionValue {OffsetX = -1, OffsetY = 1, OffsetZ = -1}, // Z减小方向：↖
        };

        /// <summary>
        /// Find Direction Based on Direction Offset Value
        /// 根据方向偏移值查找方向
        /// </summary>
        /// <param name="dValue"></param>
        /// <returns></returns>
        public static Direction3D GetDirectionByValue(DirectionValue dValue)
        {
            for (var dir = BasicDirectionValue.Length - 1; dir >= 0; dir--)
            {
                if (BasicDirectionValue[dir] == dValue)
                {
                    return (Direction3D) dir;
                }
            }

            return Direction3D.None;
        }

        /// <summary>
        /// Base weighted direction offset value
        /// 基本带权方向偏移值
        /// </summary>
        public static readonly DirectionVector[] BasicDirectionVector =
        {
            new DirectionVector {Direction = Direction3D.X0_Y0_Z0, Len = 1, Vector = 5}, // 水平方向：中心 ----------------
            new DirectionVector {Direction = Direction3D.X0_Y1_Z0, Len = 1, Vector = 5}, // 水平方向：↑
            new DirectionVector {Direction = Direction3D.X1_Y1_Z0, Len = 1, Vector = 5}, // 水平方向：↗
            new DirectionVector {Direction = Direction3D.X1_Y0_Z0, Len = 1, Vector = 5}, // 水平方向：→
            new DirectionVector {Direction = Direction3D.X1_Y__Z0, Len = 1, Vector = 5}, // 水平方向：↘
            new DirectionVector {Direction = Direction3D.X0_Y__Z0, Len = 1, Vector = 5}, // 水平方向：↓
            new DirectionVector {Direction = Direction3D.X__Y__Z0, Len = 1, Vector = 5}, // 水平方向：↙
            new DirectionVector {Direction = Direction3D.X__Y0_Z0, Len = 1, Vector = 5}, // 水平方向：←
            new DirectionVector {Direction = Direction3D.X__Y1_Z0, Len = 1, Vector = 5}, // 水平方向：↖
            new DirectionVector {Direction = Direction3D.X0_Y0_Z1, Len = 1, Vector = 5}, // Z增加方向：中心 ----------------
            new DirectionVector {Direction = Direction3D.X0_Y1_Z1, Len = 1, Vector = 5}, // Z增加方向：↑
            new DirectionVector {Direction = Direction3D.X1_Y1_Z1, Len = 1, Vector = 5}, // Z增加方向：↗
            new DirectionVector {Direction = Direction3D.X1_Y0_Z1, Len = 1, Vector = 5}, // Z增加方向：→
            new DirectionVector {Direction = Direction3D.X1_Y__Z1, Len = 1, Vector = 5}, // Z增加方向：↘
            new DirectionVector {Direction = Direction3D.X0_Y__Z1, Len = 1, Vector = 5}, // Z增加方向：↓
            new DirectionVector {Direction = Direction3D.X__Y__Z1, Len = 1, Vector = 5}, // Z增加方向：↙
            new DirectionVector {Direction = Direction3D.X__Y0_Z1, Len = 1, Vector = 5}, // Z增加方向：←
            new DirectionVector {Direction = Direction3D.X__Y1_Z1, Len = 1, Vector = 5}, // Z增加方向：↖
            new DirectionVector {Direction = Direction3D.X0_Y0_Z_, Len = 1, Vector = 5}, // Z减小方向：中心 ----------------
            new DirectionVector {Direction = Direction3D.X0_Y1_Z_, Len = 1, Vector = 5}, // Z减小方向：↑
            new DirectionVector {Direction = Direction3D.X1_Y1_Z_, Len = 1, Vector = 5}, // Z减小方向：↗
            new DirectionVector {Direction = Direction3D.X1_Y0_Z_, Len = 1, Vector = 5}, // Z减小方向：→
            new DirectionVector {Direction = Direction3D.X1_Y__Z_, Len = 1, Vector = 5}, // Z减小方向：↘
            new DirectionVector {Direction = Direction3D.X0_Y__Z_, Len = 1, Vector = 5}, // Z减小方向：↓
            new DirectionVector {Direction = Direction3D.X__Y__Z_, Len = 1, Vector = 5}, // Z减小方向：↙
            new DirectionVector {Direction = Direction3D.X__Y0_Z_, Len = 1, Vector = 5}, // Z减小方向：←
            new DirectionVector {Direction = Direction3D.X__Y1_Z_, Len = 1, Vector = 5}, // Z减小方向：↖
        };

        public static DirectionVector VectorCenter => GetVector(Direction3D.X0_Y0_Z0);

        public static DirectionVector VectorNorth => GetVector(Direction3D.X0_Y1_Z0);
        public static DirectionVector VectorEastNorth => GetVector(Direction3D.X1_Y1_Z0);
        public static DirectionVector VectorEast => GetVector(Direction3D.X1_Y0_Z0);
        public static DirectionVector VectorEastSouth => GetVector(Direction3D.X1_Y__Z0);
        public static DirectionVector VectorSouth => GetVector(Direction3D.X0_Y__Z0);
        public static DirectionVector VectorWestSouth => GetVector(Direction3D.X__Y__Z0);
        public static DirectionVector VectorWest => GetVector(Direction3D.X__Y0_Z0);
        public static DirectionVector VectorWestNorth => GetVector(Direction3D.X__Y1_Z0);

        public static DirectionVector VectorUp => VectorNorth;
        public static DirectionVector VectorRightUp => VectorEastNorth;
        public static DirectionVector VectorRight => VectorEast;
        public static DirectionVector VectorRightDown => VectorEastSouth;
        public static DirectionVector VectorDown => VectorSouth;
        public static DirectionVector VectorLeftDown => VectorWestSouth;
        public static DirectionVector VectorLeft => VectorWest;
        public static DirectionVector VectorLeftUp => VectorWestNorth;

        public static DirectionVector GetVector(int direction)
        {
            return BasicDirectionVector[direction];
        }

        public static DirectionVector GetVector(Direction2D direction)
        {
            return BasicDirectionVector[(int) direction];
        }

        public static DirectionVector GetVector(Direction3D direction)
        {
            return BasicDirectionVector[(int) direction];
        }
    }
}