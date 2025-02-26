﻿using System.ComponentModel.DataAnnotations;

namespace Bikes.Models
{
    public class Make
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Name { get; set; }
    }
}
