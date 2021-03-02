using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraper
{
   public class Dish
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Directions1 { get; set; }
        public int TimeForPrepare { get; set; }
        public int Calories { get; set; }
        public string KitchenFrom { get; set; }
        public string Directions2 { get; set; }
        public string Directions3 { get; set; }
        public string Directions4 { get; set; }
        public string PictUrl { get; set; }
        public string Video { get; set; }

        public string IngridientsList { get; set; }

    }
}
