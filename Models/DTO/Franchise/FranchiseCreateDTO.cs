using System.ComponentModel.DataAnnotations;

namespace AssignmentWebAPI.Models.DTO.Franchise
{
    public class FranchiseCreateDTO
    {
        [Required]
        [MaxLength(100)]
        public string FranchiseName { get; set; }
        [Required]
        [MaxLength(255)]
        public string Description { get; set; }

    }
}
