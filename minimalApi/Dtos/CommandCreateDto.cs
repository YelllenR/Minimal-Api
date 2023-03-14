using System.ComponentModel.DataAnnotations;

namespace minimalApi.Dtos
{
    public class CommandCreateDto
    {
        //No need to put an id here, the database will generate one

        [Required]
        public string? HowTo { get; set; }

        [Required]
        [MaxLength(5)]
        public string? Plateform { get; set; }

        [Required]
        public string? CommandLine { get; set; }
    }
}
