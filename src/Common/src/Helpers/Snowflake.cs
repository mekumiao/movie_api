namespace MovieAPI.Common;

/// <summary>
/// 雪花算法(生成唯一ID)
/// </summary>
public class Snowflake
{
    #region 每一部分占用的位数
    /// <summary>
    /// 机器标识位数
    /// </summary>
    private const int MachineIdBits = 5;
    /// <summary>
    /// 数据标志位数
    /// </summary>
    private const int DatacenterIdBits = 5;
    /// <summary>
    /// 序列号识位数
    /// </summary>
    private const int SequenceBits = 12;
    #endregion

    #region 每一部分的最大值
    /// <summary>
    /// 机器ID最大值(31)
    /// </summary>
    private const long MaxMachineNum = -1L ^ (-1L << MachineIdBits);
    /// <summary>
    /// 数据标志ID最大值(31)
    /// </summary>
    private const long MaxDatacenterNum = -1L ^ (-1L << DatacenterIdBits);
    /// <summary>
    /// 序列号ID最大值(4095)
    /// </summary>
    private const long MaxSequenceNum = -1L ^ (-1L << SequenceBits);
    #endregion

    #region 每一部分向左的位移
    /// <summary>
    /// 机器ID偏左移12位
    /// </summary>
    private const int MachineShift = SequenceBits;
    /// <summary>
    /// 数据ID偏左移17位
    /// </summary>
    private const int DatacenterIdShift = SequenceBits + MachineIdBits;
    /// <summary>
    /// 时间毫秒左移22位
    /// </summary>
    private const int TimestampLeftShift = SequenceBits + MachineIdBits + DatacenterIdBits;
    /// <summary>
    /// 序列号
    /// </summary>
    private long _sequence = 0L;
    /// <summary>
    /// 上一次时间戳
    /// </summary>
    private long _lastTimestamp = -1L;
    /// <summary>
    /// 机器标识ID
    /// </summary>
    public long MachineId { get; protected set; }
    /// <summary>
    /// 数据中心ID
    /// </summary>
    public long DatacenterId { get; protected set; }
    #endregion

    /// <summary>
    /// 基准时间
    /// </summary>
    private const long StartStmp = 1288834974657L;
    /// <summary>
    /// 时间戳开始计算的时间
    /// </summary>
    private readonly DateTimeOffset Jan1st1970 = new(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
    /// <summary>
    /// 同步锁
    /// </summary>
    private readonly object _lock = new();
    /// <summary>
    /// 雪花算法
    /// </summary>
    /// <param name="machineId">机器标识ID(1~31)</param>
    /// <param name="datacenterId">数据中心ID(1~31)</param>
    /// <exception cref="ArgumentException"></exception>
    public Snowflake(long machineId, long datacenterId)
    {
        _ = machineId is (> 0 and < MaxMachineNum) ? true : throw new ArgumentException($"machineId 必须大于0，MaxMachineNum： {MaxMachineNum}");

        _ = datacenterId is (> 0 and < MaxDatacenterNum) ? true : throw new ArgumentException($"datacenterId必须大于0，且小于MaxDatacenterNum： {MaxDatacenterNum}");

        MachineId = machineId;
        DatacenterId = datacenterId;
    }
    /// <summary>
    /// 取得下一个ID
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    /// <returns></returns>
    public long NextId()
    {
        lock (_lock)
        {
            var timestamp = TimeGen();
            _ = timestamp < _lastTimestamp ? throw new InvalidOperationException($"时间戳必须大于上一次生成ID的时间戳.  拒绝为{_lastTimestamp - timestamp}毫秒生成id") : true;

            //如果上次生成时间和当前时间相同,在同一毫秒内
            if (_lastTimestamp == timestamp)
            {
                //sequence自增，和sequenceMask相与一下，去掉高位
                _sequence = (_sequence + 1) & MaxSequenceNum;
                //判断是否溢出,也就是每毫秒内超过1024，当为1024时，与sequenceMask相与，sequence就等于0
                if (_sequence == 0L)
                {
                    //等待到下一毫秒
                    timestamp = TilNextMillis(_lastTimestamp);
                }
            }
            else
            {
                //如果和上次生成时间不同,重置sequence，就是下一毫秒开始，sequence计数重新从0开始累加,
                //为了保证尾数随机性更大一些,最后一位可以设置一个随机数
                _sequence = 0L;//new Random().Next(10);
            }

            _lastTimestamp = timestamp;
            return ((timestamp - StartStmp) << TimestampLeftShift) | (DatacenterId << DatacenterIdShift) | (MachineId << MachineShift) | _sequence;
        }
    }
    /// <summary>
    /// 防止产生的时间比之前的时间还要小（由于NTP回拨等问题）,保持增量的趋势.
    /// </summary>
    /// <param name="lastTimestamp"></param>
    /// <returns></returns>
    protected virtual long TilNextMillis(long lastTimestamp)
    {
        var timestamp = TimeGen();
        while (timestamp <= lastTimestamp)
        {
            timestamp = TimeGen();
        }
        return timestamp;
    }
    /// <summary>
    /// 获取当前的时间戳
    /// </summary>
    /// <returns></returns>
    protected virtual long TimeGen()
    {
        return (long)(DateTimeOffset.UtcNow - Jan1st1970).TotalMilliseconds;
    }
}
