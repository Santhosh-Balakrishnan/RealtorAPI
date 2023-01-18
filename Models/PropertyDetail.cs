using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class PropertyDetail
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Title { get; set; }
        [Required]
        public double? Price { get; set; }
        [Required]
        public double? Area { get; set; }
        [Required]
        public int NoOfBeds { get; set; }
        [Required]
        public int NoOfBaths { get; set; }
        [Required]
        public int? BuiltYear { get; set; }
        public string Description { get; set; }
        [Required]
        public string Category { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string State { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public int? ZipCode { get; set; }
        [Required]
        public int? AgentId { get; set; }

        [ForeignKey("AgentId")]
        [ValidateNever]
        public User Agent { get; set; }
    }
}
