using System;

namespace TTHandiCrafts.Utils
{
    /// <summary>
    /// Системное время
    /// </summary>
    public static class SystemTime
    {
        public static Func<DateTime> Now = () => DateTime.Now;
    }
}