using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProgramlamaProje.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public bool IsConfirmed { get; set; }
        public DateTime PublishTime { get; set; }

        public int RecipeId { get; set; }
        public Recipe Recipe { get; set; }

        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
