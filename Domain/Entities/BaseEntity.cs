using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Domain.Entities
{
    internal class BaseEntity<T>  where T : class
    {
        public T Id { get; set; }
        public DateOnly CreatedAt { get; set; }

        public DateOnly UpdatedAt { get; set; }
    }
}
