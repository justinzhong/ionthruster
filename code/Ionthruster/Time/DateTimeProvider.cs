using System;

namespace Ionthruster.Time
{
    public class DateTimeProvider : IDateTimeProvider
    {
        private Func<DateTime> Now { get; }

        public DateTimeProvider(Func<DateTime> now)
        {
            if (now == null) throw new ArgumentNullException(nameof(now));

            Now = now;
        }

        public DateTime GetCurrentDateTime()
        {
            return Now();
        }
    }
}
