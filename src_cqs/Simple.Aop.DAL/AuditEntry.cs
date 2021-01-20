using System;
using Simple.Aop.Domain.Interfaces;

namespace Simple.Aop.DAL
{
    public class AuditEntry : IAuditEntry
    {
        public AuditEntry(
            Guid id, DateTime timeOfExecution, string operation, string data)
        {
            Id = id;
            TimeOfExecution = timeOfExecution;
            Operation = operation;
            Data = data;
        }

        public Guid Id { get; }
        public DateTime TimeOfExecution { get; }
        public string Operation { get; }
        public string Data { get; }
    }
}