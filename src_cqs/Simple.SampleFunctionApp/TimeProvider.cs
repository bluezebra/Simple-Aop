using System;
using Simple.Aop.Domain.Interfaces;

namespace Simple.Aop.Functions
{
    public sealed class TimeProvider : ITimeProvider
    {
        public DateTime Now => DateTime.Now;
    }
}