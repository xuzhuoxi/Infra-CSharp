using System;
using System.Collections.Generic;

namespace JLGames.Infra.Mathx
{
    public struct Line1Int : IEquatable<Line1Int>
    {
        public int Start;
        public int End;

        public int Min => Math.Min(Start, End);
        public int Max => Math.Max(Start, End);

        public int Size => End - Start;
        public int AbsSize => Math.Abs(Size);
        public bool IsPoint => Start == End;

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
            return obj is Line1Int && Equals((Line1Int) obj);
        }

        public bool Equals(Line1Int other)
        {
            return Start == other.Start && End == other.End;
        }

        public Line1Int(int start, int end)
        {
            Start = start;
            End = end;
        }

        public Line1Int[] SliceAtZero(int size)
        {
            return SliceAt(0, size);
        }

        public Line1Int[] SliceAtStart(int size)
        {
            return SliceAt(Start, size);
        }

        public Line1Int[] SliceAtEnd(int size)
        {
            return SliceAt(End, size);
        }

        public Line1Int[] SliceAtMin(int size)
        {
            return SliceAt(Min, size);
        }

        public Line1Int[] SliceAtMax(int size)
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