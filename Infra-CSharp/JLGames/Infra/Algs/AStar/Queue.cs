using System.Collections.Generic;

namespace JLGames.Infra.AStar
{
    /// <summary>
    /// Result of dequeuing from a position queue.
    /// 从坐标队列出队的结果
    /// </summary>
    public struct QueueResult
    {
        /// <summary>Dequeued position; 出队坐标</summary>
        public Position Position;

        /// <summary>Whether the operation succeeded; 操作是否成功</summary>
        public bool Ok;

        /// <inheritdoc />
        public override string ToString()
        {
            return $"QueueResult[{Position},{Ok}]";
        }

        /// <summary>Error sentinel when the queue is empty; 队列为空时的错误哨兵</summary>
        public static readonly QueueResult Error = new QueueResult {Position = Position.Empty, Ok = false};
    }

    /// <summary>
    /// FIFO queue of grid positions.
    /// 格子坐标先进先出队列
    /// </summary>
    public class PositionQueue : Queue<Position>
    {
        /// <summary>
        /// Enqueue a position.
        /// 入队一个坐标
        /// </summary>
        /// <param name="pos">Position; 坐标</param>
        public void Push(Position pos)
        {
            Enqueue(pos);
        }

        /// <summary>
        /// Dequeue the front element.
        /// 出队队首元素
        /// </summary>
        /// <param name="pos">Unused; kept for API compatibility; 未使用，保留以兼容 API</param>
        /// <returns>Dequeue result; 出队结果</returns>
        public QueueResult Shift(Position pos)
        {
            if (Count == 0)
            {
                return QueueResult.Error;
            }

            return new QueueResult {Position = Dequeue(), Ok = false};
        }
    }

    /// <summary>
    /// Result of dequeuing from a priority position queue.
    /// 从优先级坐标队列出队的结果
    /// </summary>
    public struct PriorityPositionQueueResult
    {
        /// <summary>Dequeued entry; 出队项</summary>
        public PriorityPosition Position;

        /// <summary>Whether the operation succeeded; 操作是否成功</summary>
        public bool Ok;

        /// <inheritdoc />
        public override string ToString()
        {
            return $"PriorityPositionQueueResult[{Position},{Ok}]";
        }

        /// <summary>Error sentinel when the queue is empty; 队列为空时的错误哨兵</summary>
        public static readonly PriorityPositionQueueResult Error =
            new PriorityPositionQueueResult {Position = PriorityPosition.Empty, Ok = false};
    }

    /// <summary>
    /// Open-list queue sorted by ascending priority (lower f-score first).
    /// 按优先级升序排列的 Open 表（f 值越小越靠前）
    /// </summary>
    public class PriorityPositionQueue : List<PriorityPosition>
    {
        /// <summary>
        /// Copy all entries to a new array.
        /// 将所有项复制为新数组
        /// </summary>
        /// <returns>Snapshot of the queue; 队列快照</returns>
        public PriorityPosition[] GetAll()
        {
            var rs = ToArray();
            return rs;
        }

        /// <summary>
        /// Insert by ascending priority (selection-style insert).
        /// 按优先级升序插入（选择式插入）
        /// </summary>
        /// <param name="ppos">Entry to insert; 待插入项</param>
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

        /// <summary>
        /// Enqueue a 3D position with priority.
        /// 入队三维坐标及优先级
        /// </summary>
        /// <param name="x">X coordinate; X 坐标</param>
        /// <param name="y">Y coordinate; Y 坐标</param>
        /// <param name="z">Z coordinate; Z 坐标</param>
        /// <param name="priority">Priority (f-score); 优先级（f 值）</param>
        public void Push(int x, int y, int z, int priority)
        {
            var pp = Positions.NewPriorityPosition(x, y, z, priority);
            PushPriorityPosition(pp);
        }

        /// <summary>
        /// Enqueue a 2D position with priority (Z = 0).
        /// 入队二维坐标及优先级（Z = 0）
        /// </summary>
        /// <param name="x">X coordinate; X 坐标</param>
        /// <param name="y">Y coordinate; Y 坐标</param>
        /// <param name="priority">Priority (f-score); 优先级（f 值）</param>
        public void Push(int x, int y, int priority)
        {
            var pp = Positions.NewPriorityPosition(x, y, priority);
            PushPriorityPosition(pp);
        }

        /// <summary>
        /// Remove and return the tail element (lowest priority in this ordering).
        /// 取出并返回尾部元素（本排序下优先级最低）
        /// </summary>
        /// <returns>Dequeue result; 出队结果</returns>
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
        /// Remove and return the head element (highest priority / lowest f-score).
        /// 取出并返回头部元素（优先级最高 / f 值最小）
        /// </summary>
        /// <returns>Dequeue result; 出队结果</returns>
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
        /// Current queue length.
        /// 当前队列长度
        /// </summary>
        public int Len => Count;
    }
}
