using System;
using System.Collections.Generic;

namespace JLGames.Infra.AStar
{
    public static class AStarUtil
    {
        /// <summary>
        /// Judging the direction, the premise is that the two points are the line direction
        /// 判断方向,前提是两点为线向
        /// </summary>
        /// <param name="startPos"></param>
        /// <param name="endPos"></param>
        /// <returns></returns>
        public static Direction3D GetDirection3D(Position startPos, Position endPos)
        {
            if (startPos == endPos)
            {
                return Direction3D.None;
            }

            var directionValue = new DirectionValue
            {
                OffsetX = endPos.X - startPos.X,
                OffsetY = endPos.Y - startPos.Y,
                OffsetZ = endPos.Z - startPos.Z
            }.UnitValue;
            return DirectionsStatic.GetDirectionByValue(directionValue);
        }

        /// <summary>
        /// Judging the direction, the premise is that the two points are the line direction
        /// 判断方向,前提是两点为线向
        /// Use the Cartesian coordinate system
        /// 采用笛卡尔坐标系
        /// </summary>
        /// <param name="sourcePos"></param>
        /// <param name="targetPos"></param>
        /// <returns></returns>
        public static Direction2D GetDirection2D(Position sourcePos, Position targetPos)
        {
            if (sourcePos.Equals(targetPos))
            {
                return Direction2D.Center;
            }

            if (sourcePos.X == targetPos.X || sourcePos.Y == targetPos.Y)
            {
                if (sourcePos.X == targetPos.X)
                {
                    //垂直
                    if (sourcePos.Y < targetPos.Y)
                    {
                        return Direction2D.North;
                    }
                    else
                    {
                        return Direction2D.South;
                    }
                }
                else
                {
                    if (sourcePos.X < targetPos.X)
                    {
                        return Direction2D.East;
                    }
                    else
                    {
                        return Direction2D.West;
                    }
                }
            }
            else
            {
                if (sourcePos.X - targetPos.X == sourcePos.Y - targetPos.Y)
                {
                    //左下角 或 右上角
                    if (sourcePos.X < targetPos.X)
                    {
                        return Direction2D.EastNorth;
                    }
                    else
                    {
                        return Direction2D.WestSouth;
                    }
                }
                else
                {
                    if (sourcePos.Y < targetPos.Y)
                    {
                        return Direction2D.WestNorth;
                    }
                    else
                    {
                        return Direction2D.EastSouth;
                    }
                }
            }
        }

        /// <summary>
        /// Clear redundant points, keep inflection points
        /// 清除冗余点，保留拐点
        /// </summary>
        /// <param name="path"></param>
        /// <param name="allowDirection">允许的行走方向</param>
        /// <returns></returns>
        public static Position[] ClearRedundancies(Position[] path, int[] allowDirection)
        {
            if (null == path)
            {
                return path;
            }

            var ln = path.Length;
            if (ln <= 2)
            {
                return path;
            }

//            DebugUtil.Log("原始路径：", string.Join(",", path));
            List<Position> temp = new List<Position>(path);
            for (var index = ln - 2; index >= 1; index--)
            {
                if (IsInLine(path[index - 1], path[index], path[index + 1]))
                {
                    var dir = GetDirection3D(path[index - 1], path[index + 1]);
//                    DebugUtil.Log("Dir:", dir);
                    foreach (var ad in allowDirection)
                    {
                        if ((int) dir == ad)
                        {
                            temp.RemoveAt(index);
                            break;
                        }
                    }
                }
            }

//            DebugUtil.Log("去拐路径：", string.Join(",", temp.ToArray()));
            return temp.ToArray();
        }

        /// <summary>
        /// Clear redundant points, keep inflection points
        /// 清除冗余点，保留拐点
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Position[] ClearRedundancies(Position[] path)
        {
            if (null == path)
            {
                return path;
            }

            var ln = path.Length;
            if (ln <= 2)
            {
                return path;
            }

            List<Position> temp = new List<Position>(path);

            for (var index = ln - 2; index >= 1; index--)
            {
                if (IsInLine(path[index - 1], path[index], path[index + 1]))
                {
                    temp.RemoveAt(index);
                }
            }

            return temp.ToArray();
        }

        /// <summary>
        /// Determine whether three points are in a line
        /// 判断三点是否一线 
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="third"></param>
        /// <returns></returns>
        public static bool IsInLine(Position first, Position second, Position third)
        {
            return (second.Y - first.Y) * (third.X - first.X) == (third.Y - first.Y) * (second.X - first.X);
        }

        /// <summary>
        /// Whether the standard line direction
        /// 是否标准线向
        /// </summary>
        /// <param name="pos1"></param>
        /// <param name="pos2"></param>
        /// <param name="includeOblique">包含斜向</param>
        /// <returns></returns>
        public static bool IsInStandardLine(Position pos1, Position pos2, bool includeOblique)
        {
            if (pos1.Equals(pos2))
            {
                return false;
            }

            var xe = pos1.X == pos2.X;
            var ye = pos1.Y == pos2.Y;
            var ze = pos1.Z == pos2.Z;

            if (xe ^ ye ^ ze)
            {
                return true;
            }

            if (!includeOblique)
            {
                return false;
            }

            var lx = Math.Abs(pos1.X - pos2.X);
            var ly = Math.Abs(pos1.Y - pos2.Y);
            var lz = Math.Abs(pos1.Z - pos2.Z);

            return (lx == ly && lz == 0) || (lx == lz && ly == 0) || (ly == lz && lx == 0) || (lx == ly && ly == lz);
        }
    }
}