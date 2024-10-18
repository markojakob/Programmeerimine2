using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace KooliProjekt.Data
{
    public class Comment
    {
        public int Id { get; set; }

        [Required]
        [StringLength(512)]
        public string Content { get; set; }

        [Required]
        public IdentityUser User { get; set; }
    }
}
