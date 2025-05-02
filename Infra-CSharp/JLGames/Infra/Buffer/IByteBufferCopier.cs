namespace JLGames.Infra.Buffer
{
    public interface IByteBufferCopier
    {
        /// <summary>
        /// Copy a byte data.
        /// 复制一个无符号8位整型数据
        /// </summary>
        /// <returns></returns>
        byte CopyByte();

        /// <summary>
        /// Copy all unread data starting with offset subscripts
        /// 复制从偏移下标开始的全部未读取的数据
        /// </summary>
        /// <param name="offset"></param>
        /// <returns></returns>
        byte[] CopyBytes(int offset = 0);

        /// <summary>
        /// Copy unread data of specified length
        /// 复制指定长度的未读取数据
        /// </summary>
        /// <param name="size"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        byte[] CopyBytes(int size, int offset);

        /// <summary>
        /// Copy the specified length of data and write it into the target array
        /// 复制指定长度数据， 并写入到目标数组中
        /// </summary>
        /// <param name="dst"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
        int CopyBytesTo(ref byte[] dst, int offset = 0);

        /// <summary>
        /// Copy the specified length of data and write it into the target array
        /// 复制指定长度数据， 并写入到目标数组中
        /// </summary>
        /// <param name="dst"></param>
        /// <param name="size"></param>
        /// <param name="offset"></param>
        int CopyBytesTo(ref byte[] dst, int size, int offset);
    }
}