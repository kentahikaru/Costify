namespace Core.Domain.Entities
{
    using System;
    
    public class Cost
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }

        public double Price { get; set; }

        public Category Category { get; set; }

    }
}