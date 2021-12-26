using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebProgramlamaProje.Models
{
    public class ConfirmComment
    {
        [DisplayName("Id")]
        public int Id { get; set; }

        [DisplayName("Yorum")]
        public string Text { get; set; }

        [DisplayName("Onay")]
        public bool IsApproved { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
