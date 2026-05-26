using System.ComponentModel.DataAnnotations;

namespace PersonalAccount.Models.Students
{
    public class StudentEditModel
    {
        [Required(ErrorMessage = "FullName is reqired")]
        [StringLength(255, ErrorMessage = "Name cannot be longer than 255 characters")]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "GroupName is reqired")]
        [StringLength(255, ErrorMessage = "Name cannot be longer than 255 characters")]
        public string GroupName { get; set; } = string.Empty;

        public Uri? PhotoUrl { get; set; }
    }
}
