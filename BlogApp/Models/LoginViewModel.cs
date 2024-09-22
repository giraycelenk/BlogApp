using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Eposta")]
        public string? Email { get; set; }
        [Required]
        [StringLength(10,ErrorMessage = "{0} alanı en az {2} karakter uzunluğunda olmalıdır."),MinLength(6,ErrorMessage = "Minimum 6 karakter belirtiniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string? Password { get; set; }
    }
}