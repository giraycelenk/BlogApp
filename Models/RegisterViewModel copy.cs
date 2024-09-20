using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Username")]
        public string? UserName { get; set; }

        [Required]
        [Display(Name = "Ad Soyad")]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Eposta")]
        public string? Email { get; set; }

        [Required]
        [StringLength(10,ErrorMessage = "{0} alanı en az {2} karakter uzunluğunda olmalıdır."),MinLength(6,ErrorMessage = "Minimum 6 karakter belirtiniz.")]
        [DataType(DataType.Password)]
        [Display(Name = "Parola")]
        public string? Password { get; set; }

        [Required]
        [StringLength(10,ErrorMessage = "{0} alanı en az {2} karakter uzunluğunda olmalıdır."),MinLength(6,ErrorMessage = "Minimum 6 karakter belirtiniz.")]
        [DataType(DataType.Password)]
        [Compare(nameof(Password),ErrorMessage = "Parolalar eşleşmemektedir.")]
        [Display(Name = "Parola Tekrar")]
        public string? ConfirmPassword { get; set; }
    }
}