using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebProgramlamaProje.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        
        [DisplayName("Ad")]
        public string Title { get; set; }

        [DisplayName("Tarif")]
        public string FoodRecipe { get; set; }
        public string ImageUrl { get; set; }
        public bool IsConfirmed { get; set; }
        [DisplayName("Hazırlanma süresi")]
        public int PreparationTime { get; set; }
        [DisplayName("Yayınlanma tarihi")]
        public DateTime PublishTime { get; set; }

        public string AppUserId { get; set; }

        public AppUser AppUser { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }

         public ICollection<Comment> Comments { get; set; }

    }
}
