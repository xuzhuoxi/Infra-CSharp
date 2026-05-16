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

        /// <summary>Allowed direction indices; 允许的方向索引</summary>
        public int[] Directions => m_Directions;

        /// <summary>
        /// Set allowed directions from 2D enums.
        /// 以二维方向枚举设置允许方向
        /// </summary>
        /// <param name="directions">2D directions; 二维方向数组</param>
        public void SetDirrections(Direction2D[] directions)
        {
            m_Directions = new int[directions.Length];
            for (var i = 0; i < directions.Length; i++)
            {
                m_Directions[i] = (int) directions[i];
            }
        }

        /// <summary>
        /// Set allowed directions from 3D enums.
        /// 以三维方向枚举设置允许方向
        /// </summary>
        /// <param name="directions">3D directions; 三维方向数组</param>
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
        /// <summary>Offset on X; X 方向偏移</summary>
        public int OffsetX, OffsetY, OffsetZ;

        /// <summary>
        /// Normalized unit direction (gcd-reduced when possible).
        /// 归一化单位方向（尽可能按最大公约数约简）
        /// </summary>
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

        /// <summary>
        /// Compare direction equality, including equivalent unit vectors.
        /// 比较方向是否相同（含单位向量等价）
        /// </summary>
        /// <param name="other">Other offset; 另一偏移</param>
        /// <returns>True if same direction; 同向时返回 true</returns>
        public bool DirectionEquals(DirectionValue other)
        {
            if (Equals(other))
            {
                return true;
            }

            return UnitValue.Equals(other.UnitValue);
        }

        /// <summary>Inequality operator; 不等比较</summary>
        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator !=(DirectionValue b, DirectionValue c)
        {
            return !b.Equals(c);
        }

        /// <summary>Equality operator; 相等比较</summary>
        [MethodImpl((MethodImplOptions) 256)]
        public static bool operator ==(DirectionValue b, DirectionValue c)
        {
            return b.Equals(c);
        }

        /// <summary>Add offsets; 偏移相加</summary>
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

        /// <summary>Subtract offsets; 偏移相减</summary>
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

        /// <summary>Scale offset; 缩放偏移</summary>
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
        /// <summary>Base direction enum; 基础方向枚举</summary>
        public Direction3D Direction;

        /// <summary>Step length multiplier; 步长倍数</summary>
        public int Len;

        /// <summary>Movement cost for one step in this direction; 该方向单步移动代价</summary>
        public int Vector;

        /// <summary>Unit offset for this direction; 该方向的单位偏移</summary>
        public DirectionValue Value => DirectionsStatic.BasicDirectionValue[(int) Direction];

        /// <summary>Offset scaled by <see cref="Len"/>; 按 Len 缩放后的偏移</summary>
        public DirectionValue RealValue => Value * Len;

        /// <summary>X offset per step; 单步 X 偏移</summary>
        public int OffsetX => Value.OffsetX;

        /// <summary>Y offset per step; 单步 Y 偏移</summary>
        public int OffsetY => Value.OffsetY;

        /// <summary>Z offset per step; 单步 Z 偏移</summary>
        public int OffsetZ => Value.OffsetZ;

        /// <summary>Step cost (alias of <see cref="Vector"/>); 步进代价（同 Vector）</summary>
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
        /// <param name="dValue">Direction offset; 方向偏移</param>
        /// <returns>Matching direction, or <see cref="Direction3D.None"/>; 匹配方向，无匹配时返回 None</returns>
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

        /// <summary>Center / no movement; 中心 / 无移动</summary>
        public static DirectionVector VectorCenter => GetVector(Direction3D.X0_Y0_Z0);

        /// <summary>North (+Y); 北（+Y）</summary>
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

        /// <summary>
        /// Get weighted direction vector by index.
        /// 按索引获取带权方向向量
        /// </summary>
        /// <param name="direction">Direction index; 方向索引</param>
        /// <returns>Direction vector; 方向向量</returns>
        public static DirectionVector GetVector(int direction)
        {
            return BasicDirectionVector[direction];
        }

        /// <summary>
        /// Get weighted direction vector for a 2D direction.
        /// 获取二维方向的带权向量
        /// </summary>
        /// <param name="direction">2D direction; 二维方向</param>
        /// <returns>Direction vector; 方向向量</returns>
        public static DirectionVector GetVector(Direction2D direction)
        {
            return BasicDirectionVector[(int) direction];
        }

        /// <summary>
        /// Get weighted direction vector for a 3D direction.
        /// 获取三维方向的带权向量
        /// </summary>
        /// <param name="direction">3D direction; 三维方向</param>
        /// <returns>Direction vector; 方向向量</returns>
        public static DirectionVector GetVector(Direction3D direction)
        {
            return BasicDirectionVector[(int) direction];
        }
    }
}