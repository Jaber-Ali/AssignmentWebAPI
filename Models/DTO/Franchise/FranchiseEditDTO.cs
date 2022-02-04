using System.ComponentModel.DataAnnotations;

namespace AssignmentWebAPI.Models.DTO.Franchise
{
    public class FranchiseEditDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string FranchiseName { get; set; }
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }
    }
}
