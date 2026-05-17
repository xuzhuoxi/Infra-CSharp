using System;
using System.Collections.Generic;

namespace JLGames.Infra.Mathx
{
    /// <summary>
    /// 1D line segment (integer endpoints).
    /// 一维线段（整型端点）。
    /// </summary>
    public struct Line1Int : IEquatable<Line1Int>
    {
        /// <summary>
        /// Start endpoint.
        /// 起点。
        /// </summary>
        public int Start;

        /// <summary>
        /// End endpoint.
        /// 终点。
        /// </summary>
        public int End;

        /// <summary>
        /// Smaller of <see cref="Start"/> and <see cref="End"/>.
        /// <see cref="Start"/> 与 <see cref="End"/> 中的较小值。
        /// </summary>
        public int Min => Math.Min(Start, End);

        /// <summary>
        /// Larger of <see cref="Start"/> and <see cref="End"/>.
        /// <see cref="Start"/> 与 <see cref="End"/> 中的较大值。
        /// </summary>
        public int Max => Math.Max(Start, End);

        /// <summary>
        /// Signed length (<see cref="End"/> − <see cref="Start"/>).
        /// 有符号长度（<see cref="End"/> − <see cref="Start"/>）。
        /// </summary>
        public int Size => End - Start;

        /// <summary>
        /// Absolute length.
        /// 长度绝对值。
        /// </summary>
        public int AbsSize => Math.Abs(Size);

        /// <summary>
        /// Whether start and end are equal.
        /// 起终点是否重合。
        /// </summary>
        public bool IsPoint => Start == End;

        /// <inheritdoc />
        public override string ToString()
        {
            return $"{{Start={Start},End={End}}}";
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return Start.GetHashCode() ^ End.GetHashCode() << 2;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            return obj is Line1Int && Equals((Line1Int) obj);
        }

        /// <inheritdoc />
        public bool Equals(Line1Int other)
        {
            return Start == other.Start && End == other.End;
        }

        /// <summary>
        /// Creates a 1D integer segment.
        /// 创建整型一维线段。
        /// </summary>
        public Line1Int(int start, int end)
        {
            Start = start;
            End = end;
        }

        /// <summary>
        /// Splits this segment into sub-segments of length <paramref name="size"/>, aligned at coordinate 0.
        /// 按长度 <paramref name="size"/> 分割本线段，对齐基准点为 0。
        /// </summary>
        public Line1Int[] SliceAtZero(int size)
        {
            return SliceAt(0, size);
        }

        /// <summary>
        /// Splits this segment aligned at <see cref="Start"/>.
        /// 以 <see cref="Start"/> 为对齐基准点分割。
        /// </summary>
        public Line1Int[] SliceAtStart(int size)
        {
            return SliceAt(Start, size);
        }

        /// <summary>
        /// Splits this segment aligned at <see cref="End"/>.
        /// 以 <see cref="End"/> 为对齐基准点分割。
        /// </summary>
        public Line1Int[] SliceAtEnd(int size)
        {
            return SliceAt(End, size);
        }

        /// <summary>
        /// Splits this segment aligned at <see cref="Min"/>.
        /// 以 <see cref="Min"/> 为对齐基准点分割。
        /// </summary>
        public Line1Int[] SliceAtMin(int size)
        {
            return SliceAt(Min, size);
        }

        /// <summary>
        /// Splits this segment aligned at <see cref="Max"/>.
        /// 以 <see cref="Max"/> 为对齐基准点分割。
        /// </summary>
        public Line1Int[] SliceAtMax(int size)
        {
            return SliceAt(Max, size);
        }

        /// <summary>
        /// Split into multiple Line1Int segments based on basis point
        /// 基于指定点分割为多条整型线段
        /// </summary>
        /// <param name="basisPoint">Alignment reference on the axis.<br/>轴上的对齐基准坐标。</param>
        /// <param name="size">Segment length (must be non-zero).<br/>分段长度（不可为 0）。</param>
        /// <returns>Sub-segments, or null if <paramref name="size"/> is zero.<br/>子线段数组；<paramref name="size"/> 为 0 时返回 null。</returns>
        public Line1Int[] SliceAt(int basisPoint, int size)
        {
            if (size == 0) return null;
            size = Math.Abs(size);
            var min = Min;
            var offset = Math.Abs(min - basisPoint).Mod(size);
            var point = min + offset;
            var list = new List<Line1Int>();
            if (offset != 0)
            {
                list.Add(new Line1Int(min, point));
            }

            var max = Max;
            while (point < max)
            {
                var end = point + size;
                list.Add(new Line1Int(point, Math.Min(end, max)));
                point = end;
            }

            return list.ToArray();
        }
    }
}
