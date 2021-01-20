using System;

namespace Simple.Aop.Domain
{
    public class Ticket
    {
        public Ticket(Guid id, string title, string description)
        {
            Id = id;
            Title = title;
            Description = description;
        }

        public Guid Id { get; }
        public string Title { get; }
        public string Description { get; }
    }
}
