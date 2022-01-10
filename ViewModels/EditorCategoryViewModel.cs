using System.ComponentModel.DataAnnotations;

namespace BlogNew.ViewModels
{
    public class EditorCategoryViewModel
    {
        [Required(ErrorMessage = "o nome é obrigatório")]
        // [Required]
        public string Name { get; set; }
        [Required(ErrorMessage = "o slug é obrigatório")]
        // [Required]
        public string Slug { get; set; }
    }
}