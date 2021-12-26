using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebProgramlamaProje.Models
{
    public class ViewModelSR
    {
        public Recipe Recipe { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
