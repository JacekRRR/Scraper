using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Scraper
{
    public class PostClass
    {

        public async Task<Dish> PostAsync(Dish dish)
        {
            using (HttpClient client = new HttpClient())
            {
                var myContent = JsonConvert.SerializeObject(dish);
                var sender = System.Text.Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(sender);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var url = "http://localhost:5000/api/recipies/UpdateDish/dishUpdateDTO";
                var result = client.PutAsync(url, byteContent).Result;

                return null;

            }
            return null;
        } 
    }
    
}
