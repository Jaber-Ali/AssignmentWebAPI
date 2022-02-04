using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentWebAPI.Models.Domain
{
    [Table("Character")]
    public class Character
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; }

        [MaxLength(100)]
        public string Alias { get; set; }

        [MaxLength(20)]
        public string Gender { get; set; }
        public string Picture { get; set; }
        public ICollection<Movie> Movies { get; set; }
    }
}
