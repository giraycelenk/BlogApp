using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class PostDeleteViewModel
    {
        public int PostId { get; set; }
        
        [Display(Name = "Başlık")]
        public string? Title { get; set; }

        
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }

        
        [Display(Name = "İçerik")]
        public string? Content { get; set; }

        
        [Display(Name = "Url")]
        public string? Url { get; set; }

        public bool IsActive { get; set; }
    }
}