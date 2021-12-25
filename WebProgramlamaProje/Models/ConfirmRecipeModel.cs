using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace WebProgramlamaProje.Models
{
    public class ConfirmRecipeModel
    {
        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Yemek adı")]
        public string Title { get; set; }
        [DisplayName("Tarif")]
        public string FoodRecipe { get; set; }
        [DisplayName("Onay")]
        public bool IsApproved { get; set; }
    }
}
