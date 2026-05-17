using System;
using System.Collections.Generic;
using JLGames.Infra.Extensions;

namespace JLGames.Infra.Mathx
{
    /// <summary>
    /// 1D line segment (floating-point endpoints).
    /// 一维线段（浮点端点）。
    /// </summary>
    public struct Line1 : IEquatable<Line1>
    {
        /// <summary>
        /// Start endpoint.
        /// 起点。
        /// </summary>
        public double Start;

        /// <summary>
        /// End endpoint.
        /// 终点。
        /// </summary>
        public double End;

        /// <summary>
        /// Smaller of <see cref="Start"/> and <see cref="End"/>.
        /// <see cref="Start"/> 与 <see cref="End"/> 中的较小值。
        /// </summary>
        public double Min => Math.Min(Start, End);

        /// <summary>
        /// Larger of <see cref="Start"/> and <see cref="End"/>.
        /// <see cref="Start"/> 与 <see cref="End"/> 中的较大值。
        /// </summary>
        public double Max => Math.Max(Start, End);

        /// <summary>
        /// Signed length (<see cref="End"/> − <see cref="Start"/>).
        /// 有符号长度（<see cref="End"/> − <see cref="Start"/>）。
        /// </summary>
        public double Size => End - Start;

        /// <summary>
        /// Absolute length.
        /// 长度绝对值。
        /// </summary>
        public double AbsSize => Math.Abs(Size);

        /// <summary>
        /// Whether start and end coincide (within float tolerance).
        /// 起终点是否重合（浮点容差内）。
        /// </summary>
        public bool IsPoint => Start.DoubleEquals(End);

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
            return obj is Line1 && Equals((Line1) obj);
        }

        /// <inheritdoc />
        public bool Equals(Line1 other)
        {
            return Start.DoubleEquals(other.Start) && End.DoubleEquals(other.End);
        }

        /// <summary>
        /// Creates a 1D segment.
        /// 创建一维线段。
        /// </summary>
        public Line1(double start, double end)
        {
            Start = start;
            End = end;
        }

        /// <summary>
        /// Splits this segment into sub-segments of length <paramref name="size"/>, aligned at coordinate 0.
        /// 按长度 <paramref name="size"/> 分割本线段，对齐基准点为 0。
        /// </summary>
        /// <param name="size">Segment length (must be non-zero).<br/>分段长度（不可为 0）。</param>
        /// <returns>Sub-segments, or null if <paramref name="size"/> is zero.<br/>子线段数组；<paramref name="size"/> 为 0 时返回 null。</returns>
        public Line1[] SliceAtZero(double size)
        {
            return SliceAt(0, size);
        }

        /// <summary>
        /// Splits this segment aligned at <see cref="Start"/>.
        /// 以 <see cref="Start"/> 为对齐基准点分割。
        /// </summary>
        public Line1[] SliceAtStart(double size)
        {
            return SliceAt(Start, size);
        }

        /// <summary>
        /// Splits this segment aligned at <see cref="End"/>.
        /// 以 <see cref="End"/> 为对齐基准点分割。
        /// </summary>
        public Line1[] SliceAtEnd(double size)
        {
            return SliceAt(End, size);
        }

        /// <summary>
        /// Splits this segment aligned at <see cref="Min"/>.
        /// 以 <see cref="Min"/> 为对齐基准点分割。
        /// </summary>
        public Line1[] SliceAtMin(double size)
        {
            return SliceAt(Min, size);
        }

        /// <summary>
        /// Splits this segment aligned at <see cref="Max"/>.
        /// 以 <see cref="Max"/> 为对齐基准点分割。
        /// </summary>
        public Line1[] SliceAtMax(double size)
        {
            return SliceAt(Max, size);
        }

        /// <summary>
        /// Split into multiple Line1s based on basis Point
        /// 基于指定点分割为多条直线
        /// </summary>
        /// <param name="basisPoint">Alignment reference on the axis.<br/>轴上的对齐基准坐标。</param>
        /// <param name="size">Segment length (must be non-zero).<br/>分段长度（不可为 0）。</param>
        /// <returns>Sub-segments, or null if <paramref name="size"/> is zero.<br/>子线段数组；<paramref name="size"/> 为 0 时返回 null。</returns>
        public Line1[] SliceAt(double basisPoint, double size)
        {
            if (size.DoubleEquals(0)) return null;
            size = Math.Abs(size);
            var min = Min;
            var offset = Math.Abs(min - basisPoint).Mod(size);
            var point = min + offset;
            var list = new List<Line1>();
            if (!offset.DoubleEquals(0))
            {
                list.Add(new Line1(min, point));
            }

            var max = Max;
            var index = 0;
            var end = point;
            while (end < max)
            {
                // To prevent the superposition of errors, it cannot be added segment by segment like Line1Int
                // 防止误差叠加，不可以像Line1Int那样一段一段地加
                var start = point + size * index;
                end = point + size * (index + 1);
                list.Add(new Line1(start, Math.Min(end, max)));
                index++;
            }

            return list.ToArray();
        }
    }
}
