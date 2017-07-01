using System;

namespace Ionthruster.Time
{
    public interface IDateTimeProvider
    {
        DateTime GetCurrentDateTime();
    }
}