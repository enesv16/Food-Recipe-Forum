using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebProgramlamaProje.Models
{
    public class AddRecipe
    {
        [DisplayName("Ad")]
        public string Title { get; set; }

        [DisplayName("Tarif")]
        public string FoodRecipe { get; set; }

        public IFormFile ImageUrl { get; set; }
        [DisplayName("Hazırlanma süresi")]
        public int PreparationTime { get; set; }

        public int CategoryId { get; set; }
    }
}
