using System;

namespace Simple.Aop.Domain.Interfaces
{
    public interface IAuditEntry
    {
        Guid Id { get; }
        DateTime TimeOfExecution { get; }
        string Operation { get; }
        string Data { get; }
    }
}