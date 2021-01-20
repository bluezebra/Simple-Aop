using System;

namespace Simple.Aop.Domain.Interfaces
{
    public interface ITimeProvider
    {
        DateTime Now { get; }
    }
}