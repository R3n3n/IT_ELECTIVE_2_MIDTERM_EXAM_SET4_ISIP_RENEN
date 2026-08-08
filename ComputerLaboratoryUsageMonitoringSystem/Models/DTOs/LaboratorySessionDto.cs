using System.ComponentModel.DataAnnotations;

namespace ComputerLaboratoryUsageMonitoringSystem.Models.DTOs
{
    public class LaboratorySessionDto
    {
        [Required]
        [Display(Name = "Student Number")]
        public string StudentNumber { get; set; } = string.Empty;

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public string Course { get; set; } = string.Empty;

        [Required]
        [Range(1, 6)]
        [Display(Name = "Year Level")]
        public int YearLevel { get; set; }

        [Required]
        [Range(1, 100)]
        [Display(Name = "Computer Number")]
        public int ComputerNumber { get; set; }

        [Required]
        public string Purpose { get; set; } = string.Empty;

        [Display(Name = "Time In")]
        [DataType(DataType.DateTime)]
        public DateTime TimeIn { get; set; }

        public string? Notes { get; set; }
    }
}