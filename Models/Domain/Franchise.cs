using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentWebAPI.Models.Domain
{
    [Table("Franchise")]
    public class Franchise
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string FranchiseName { get; set; }
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
     
        public ICollection<Movie> Movies { get; set; }
    }
}
