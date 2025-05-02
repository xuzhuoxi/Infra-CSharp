using System;
using System.Collections.Generic;
using JLGames.Infra.Extensions;

namespace JLGames.Infra.Mathx
{
    public struct Line1 : IEquatable<Line1>
    {
        public double Start;
        public double End;

        public double Min => Math.Min(Start, End);
        public double Max => Math.Max(Start, End);

        public double Size => End - Start;
        public double AbsSize => Math.Abs(Size);
        public bool IsPoint => Start.DoubleEquals(End);

        public override string ToString()
        {
            return $"{{Start={Start},End={End}}}";
        }

        public override int GetHashCode()
        {
            return Start.GetHashCode() ^ End.GetHashCode() << 2;
        }

        public override bool Equals(object obj)
        {
            return obj is Line1 && Equals((Line1) obj);
        }

        public bool Equals(Line1 other)
        {
            return Start.DoubleEquals(other.Start) && End.DoubleEquals(other.End);
        }

        public Line1(double start, double end)
        {
            Start = start;
            End = end;
        }

        public Line1[] SliceAtZero(double size)
        {
            return SliceAt(0, size);
        }

        public Line1[] SliceAtStart(double size)
        {
            return SliceAt(Start, size);
        }

        public Line1[] SliceAtEnd(double size)
        {
            return SliceAt(End, size);
        }

        public Line1[] SliceAtMin(double size)
        {
            return SliceAt(Min, size);
        }

        public Line1[] SliceAtMax(double size)
        {
            return SliceAt(Max, size);
        }

        /// <summary>
        /// Split into multiple Line1s based on basis Point
        /// 基于指定点分割为多条直线
        /// </summary>
        /// <param name="basisPoint"></param>
        /// <param name="size"></param>
        /// <returns></returns>
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