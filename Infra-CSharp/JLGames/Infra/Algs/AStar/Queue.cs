using System.Collections.Generic;

namespace JLGames.Infra.AStar
{
    public struct QueueResult
    {
        public Position Position;
        public bool Ok;
        
        public override string ToString()
        {
            return $"QueueResult[{Position},{Ok}]";
        }

        public static readonly QueueResult Error = new QueueResult {Position = Position.Empty, Ok = false};
    }

    public class PositionQueue : Queue<Position>
    {
        public void Push(Position pos)
        {
            Enqueue(pos);
        }

        public QueueResult Shift(Position pos)
        {
            if (Count == 0)
            {
                return QueueResult.Error;
            }

            return new QueueResult {Position = Dequeue(), Ok = false};
        }
    }

    public struct PriorityPositionQueueResult
    {
        public PriorityPosition Position;
        public bool Ok;

        public override string ToString()
        {
            return $"PriorityPositionQueueResult[{Position},{Ok}]";
        }

        public static readonly PriorityPositionQueueResult Error =
            new PriorityPositionQueueResult {Position = PriorityPosition.Empty, Ok = false};
    }


    public class PriorityPositionQueue : List<PriorityPosition>
    {
        public PriorityPosition[] GetAll()
        {
            var rs = ToArray();
            return rs;
        }

        /// <summary>
        /// select insert
        /// 选择插入
        /// </summary>
        /// <param name="ppos"></param>
        public void PushPriorityPosition(PriorityPosition ppos)
        {
            if (Count == 0)
            {
                Add(ppos);
                return;
            }

            var added = false;

            for (var i = Count - 1; i >= 0; i--)
            {
                if (ppos.Priority >= this[i].Priority)
                {
                    Insert(i + 1, ppos);
                    added = true;
                    break;
                }
            }

            if (!added)
            {
                Insert(0, ppos);
            }
        }

        public void Push(int x, int y, int z, int priority)
        {
            var pp = Positions.NewPriorityPosition(x, y, z, priority);
            PushPriorityPosition(pp);
        }

        public void Push(int x, int y, int priority)
        {
            var pp = Positions.NewPriorityPosition(x, y, priority);
            PushPriorityPosition(pp);
        }

        /// <summary>
        /// extract tail data
        /// 取出尾部数据
        /// </summary>
        /// <returns></returns>
        public PriorityPositionQueueResult Pop()
        {
            if (Count == 0)
            {
                return PriorityPositionQueueResult.Error;
            }

            var pos = this[Count - 1];
            RemoveAt(Count - 1);
            return new PriorityPositionQueueResult {Position = pos, Ok = true};
        }

        /// <summary>
        /// extract header data
        /// 取出头部数据
        /// </summary>
        /// <returns></returns>
        public PriorityPositionQueueResult Shift()
        {
            if (Count == 0)
            {
                return PriorityPositionQueueResult.Error;
            }

            var pos = this[0];
            RemoveAt(0);
            return new PriorityPositionQueueResult {Position = pos, Ok = true};
        }

        /// <summary>
        /// Length
        /// 长度
        /// </summary>
        public int Len => Count;
    }
}