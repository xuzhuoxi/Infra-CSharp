using System;

namespace JLGames.Infra.Mathx
{
    /// <summary>
    /// 2D line segment with integer endpoints.
    /// 二维线段（整型端点）。
    /// </summary>
    public struct Line2Int : IEquatable<Line2Int>
    {
        /// <summary>
        /// Start point.
        /// 起点。
        /// </summary>
        public Point2Int Start;

        /// <summary>
        /// End point.
        /// 终点。
        /// </summary>
        public Point2Int End;

        /// <summary>
        /// Squared length (avoids sqrt).
        /// 长度的平方（免开方）。
        /// </summary>
        public int SqrMagnitude => (Start.X - End.X) * (Start.X - End.X) + (Start.Y - End.Y) * (Start.Y - End.Y);

        /// <summary>
        /// Euclidean length.
        /// 欧几里得长度。
        /// </summary>
        public double Magnitude => Math.Sqrt(SqrMagnitude);

        /// <summary>
        /// Whether start and end coincide.
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
            return obj is Line2Int && Equals((Line2Int) obj);
        }

        /// <inheritdoc />
        public bool Equals(Line2Int other)
        {
            return Start == other.Start && End == other.End;
        }

        /// <summary>
        /// Creates a 2D integer segment.
        /// 创建二维整型线段。
        /// </summary>
        public Line2Int(Point2Int start, Point2Int end)
        {
            Start = start;
            End = end;
        }

//        public Line2[] SliceBaseZero(uint sliceSize, uint sliceYSize)
//        {
//            var absSize = AbsSize;
//            if (0 == absSize)
//            {
//                return null;
//            }
//
//            var start = InnerGetArea(Start, (int) sliceSize);
//            var end = InnerGetArea(End, (int) sliceSize);
//
//            if (start.Integer == end.Integer)
//            {
//                var thisObj = this;
//                return new Line2[] {thisObj};
//            }
//
//            var ln = Math.Abs(end.Integer - start.Integer) + 1;
////            var rs = new int[ln];
////            rs[0] = start;
////            rs[ln - 1] = end;
////            for (var i = 1; i < ln - 1; i++)
////            {
////                rs[i] = sliceSize * (i + startIndex.MapIndex);
////            }
////
////            return rs;
//
//
////            var start0 = (int) (Start / sliceSize);
////            var start1 = (int) (Start % sliceSize);
////            var end0 = (int) (End / sliceSize);
////            var end1 = (int) (End % sliceSize);
////
////            var ln = Math.Abs(end0 - start0 + (end1 <= 0 ? 1 : 0));
//            var rs = new Line2[ln];
//            var add = Size > 0 ? (int) sliceSize : (int) -sliceSize;
//
//            rs[0] = start.Remainder == 0
//                ? new Line2 {Start = Start, End = Start + add}
//                : new Line2 {Start = Start, End = Start + add - start.Remainder};
//            rs[ln - 1] = end.Remainder == 0
//                ? new Line2 {Start = End, End = End}
//                : new Line2 {Start = End - end.Remainder, End = End};
//
//            if (ln > 2)
//            {
//                for (var i = 1; i < ln - 1; i++)
//                {
//                    rs[i] = new Line2 {Start = rs[i - 1].End, End = rs[i - 1].End + add};
//                }
//            }
//
//            return rs;
//        }

//        public Line2Int[] SliceNoBase(uint sliceXSize, uint sliceYSize)
//        {
//            var xline = new LineInt(Start.X, End.Y);
//            var yLine = new LineInt(Start.Y, End.Y);
//
//            var xSlice = xline.SliceNoBase(sliceXSize);
//            var ySlice = yLine.SliceNoBase(sliceYSize);
//
//            if (null == xSlice || null == ySlice || xSlice.Length == 0 || ySlice.Length == 0)
//            {
//                return null;
//            }
//
//            if (xSlice.Length != ySlice.Length)
//            {
//                return null;
//            }
//
//            var ln = xSlice.Length;
//            var rs = new Line2Int[ln];
//
//            for (var i = ln - 1; i >= 0; i--)
//            {
//                rs[i] = new Line2Int(new Point2Int(xSlice[i].Start, ySlice[i].Start),
//                    new Point2Int(xSlice[i].End, ySlice[i].End));
//            }
//
//            return rs;
//        }
    }
}
