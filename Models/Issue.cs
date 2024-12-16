using System;
using System.ComponentModel.DataAnnotations;

namespace ITS.Models
{
    public class Issue
    {
        public int Id { get; set; }

        [Required]
        public string? Title { get; set; }

        [Required]
        public string? Description { get; set; }

        public string Status { get; set; } = "Open";

        public DateTime CreatedAt { get; set; }
       

    }

}