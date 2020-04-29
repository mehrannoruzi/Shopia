using Microsoft.AspNetCore.Mvc;
using Shopia.Domain;
using System.Collections.Generic;

namespace Shopia.Store.Api.Controllers
{
    public class ProductController : Controller
    {
        List<ProductDTO> products = new List<ProductDTO>
                {
                   new ProductDTO
                   {
                       Id=1,
                       Name = "پیراهن مردانه 1",
                       Price = 80000,
                       Discount = 5,
                       Currency = "تومان",
                       MaxCount=3,
                       ImageUrl = "https://www.sarabara.com/wp-content/uploads/2020/04/پولوشرت-جودون-مشکی-مردانه-مشکی-روبه‌رو.jpg"
                   },
                   new ProductDTO
                   {
                       Id=2,
                       Name = "پیراهن مردانه 2",
                       Price = 85000,
                       Discount = 6,
                       Currency = "تومان",
                       MaxCount=4,
                       ImageUrl = "https://www.sarabara.com/wp-content/uploads/2020/03/تیشرت-آستین-کوتاه-مردانه-طرحدار-سفید-روبه-رو.jpg"
                   },
                   new ProductDTO
                   {
                       Id=3,
                       Name = "پیراهن مردانه 3",
                       Price = 88000,
                       Discount = 0,
                       Currency = "تومان",
                       MaxCount=2,
                       ImageUrl = "https://www.sarabara.com/wp-content/uploads/2020/04/پولوشرت-جودون-مشکی-مردانه-مشکی-روبه‌رو.jpg"
                   },
                   new ProductDTO
                   {
                       Id=4,
                       Name = "پیراهن مردانه 4",
                       Price = 90000,
                       Discount = 3,
                       Currency = "تومان",
                       MaxCount=2,
                       ImageUrl = "https://www.sarabara.com/wp-content/uploads/2020/04/پولوشرت-جودون-مشکی-مردانه-مشکی-روبه‌رو.jpg"
                   },
                    new ProductDTO
                   {
                       Id=5,
                       Name = "پیراهن مردانه 5",
                       Price = 85000,
                       Discount = 0,
                       Currency = "تومان",
                       MaxCount=2,
                       ImageUrl = "https://www.sarabara.com/wp-content/uploads/2020/03/تیشرت-آستین-کوتاه-مردانه-طرحدار-سفید-روبه-رو.jpg"
                   },
                };

        [HttpGet]
        public IActionResult Get(ProductFilterDTO filter)
        {
            return Json(new
            {
                IsSuccessful = true,
                Result = products
            });
        }

        [HttpGet]
        public IActionResult GetSingle(int id)
        {
            return Json(new
            {
                IsSuccessful = true,
                Result = new ProductDTO
                {
                    Id = 1,
                    Name = "پیراهن مردانه 1",
                    Price = 80000,
                    Discount = 5,
                    Currency = "تومان",
                    MaxCount = 3,
                    Description = "لورم ایپسوم متن ساختگی با تولید سادگی نامفهوم از صنعت چاپ و با استفاده از طراحان گرافیک است. چاپگرها و متون بلکه روزنامه و مجله در ستون و سطرآنچنان که لازم است و برای شرایط فعلی تکنولوژی مورد نیاز و کاربردهای متنوع با هدف بهبود ابزارهای کاربردی می باشد. ",
                    Slides = new List<string> { "https://www.sarabara.com/wp-content/uploads/2020/03/تیشرت-آستین-کوتاه-مردانه-طرحدار-سفید-روبه-رو.jpg", "https://www.sarabara.com/wp-content/uploads/2020/04/پولوشرت-جودون-مشکی-مردانه-مشکی-روبه‌رو.jpg" }
                }
            });
        }
    }
}