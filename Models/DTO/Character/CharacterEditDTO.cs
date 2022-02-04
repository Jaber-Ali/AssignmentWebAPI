using System;

namespace AssignmentWebAPI.Models.DTO.Character
{
    public class CharacterEditDTO
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Alias { get; set; }
        public string Gender { get; set; }
        public string Picture { get; set; }

    }
}
