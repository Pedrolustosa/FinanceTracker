﻿using System.ComponentModel.DataAnnotations;

namespace FinanceTracker.API.Entities
{
    public class Group
    {
        [Key]
        public required string Name { get; set; }
        public ICollection<Connection> Connections { get; set; } = [];
    }
}
