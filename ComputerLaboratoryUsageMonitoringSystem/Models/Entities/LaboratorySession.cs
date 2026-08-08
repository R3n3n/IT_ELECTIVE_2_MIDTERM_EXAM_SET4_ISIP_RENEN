using System.ComponentModel.DataAnnotations;

namespace ComputerLaboratoryUsageMonitoringSystem.Models
{
    public class LaboratorySession
    {
        public int Id { get; set; }

        [Display(Name = "Session Number")]
        public int SessionNumber { get; set; }

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
        [Display(Name = "Computer Number")]
        public int ComputerNumber { get; set; }

        [Required]
        public string Purpose { get; set; } = string.Empty;

        [Display(Name = "Time In")]
        [DataType(DataType.DateTime)]
        public DateTime TimeIn { get; set; }

        [Display(Name = "Time Out")]
        [DataType(DataType.DateTime)]
        public DateTime? TimeOut { get; set; }

        [Required]
        public string Status { get; set; } = "Using";

        public string? Notes { get; set; }
    }
}