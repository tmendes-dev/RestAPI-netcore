using System;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models
{
    public class Example
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public bool Available { get; set; }
    }
}