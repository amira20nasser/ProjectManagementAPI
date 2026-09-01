using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectManagement.Domain.Entities
{
    internal class BaseEntity<TKey> 
    {
        public TKey Id { get; set; }
        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; } 
    }
}
