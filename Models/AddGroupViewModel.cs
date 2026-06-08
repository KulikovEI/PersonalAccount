using System.ComponentModel.DataAnnotations;

namespace PersonalAccount.Models
{
    public class AddGroupViewModel
    {
        [Required(ErrorMessage = "Введите название группы")]
        [StringLength(50, ErrorMessage = "Название группы не должно превышать 50 символов")]
        [Display(Name = "Название группы")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "Описание не должно превышать 500 символов")]
        [Display(Name = "Описание группы")]
        public string Description { get; set; } = string.Empty;

        [Url(ErrorMessage = "Введите корректный URL-адрес (например, https://site.com)")]
        [Display(Name = "Ссылка на обложку группы")]
        public string? ImageUrl { get; set; }
    }
}
