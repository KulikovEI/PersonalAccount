using System.ComponentModel.DataAnnotations;

namespace PersonalAccount.Models
{
    public class AddDisciplineViewModel
    {
        [Required(ErrorMessage = "Введите название дисциплины")]
        [StringLength(100, ErrorMessage = "Название дисциплины не должно превышать 100 символов")]
        [Display(Name = "Название дисциплины")]
        public string Name { get; set; } = string.Empty;
    }
}
