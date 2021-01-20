using System;
using System.Linq;

namespace Simple.Aop.Domain
{
    public class TenantId
    {
        public TenantId(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("TenantId not found.");

            if (value.Any(c => c == '/' || c == '\\' || c == '?' || c == '#' || char.IsControl(c)))
                throw new ArgumentException("TenantId must not contain /, \\, #, ?, or control characters.");
            
            Value = value;
        }

        public string Value { get; }
    }
}
