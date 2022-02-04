using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentWebAPI.Models.Domain
{
    [Table("Movie")]
    public class Movie
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Title { get; set; }
        [MaxLength(200)]
        public string Genre { get; set; }
        public int ReleaseYear { get; set; }
        [MaxLength(100)]
        public string Director { get; set; }
        [MaxLength(255)]
        public string PictureUrl { get; set; }
        [MaxLength(255)]
        public string Trailer { get; set; }

        //F_Key
        //Relationships
        public int? FranchiseId { get; set; }

        public Franchise Franchise { get; set; }
        public ICollection<Character> Characters { get; set; }
        
    }
}
