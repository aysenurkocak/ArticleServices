using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ArticleServices.Models;
using Microsoft.AspNetCore.Mvc;

namespace ArticleServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        // GET api/article
        [HttpGet]
        public List<Article> Get()
        {
            List<Article> articleList = new List<Article>();
            articleList.Add(new Article
            {
                Category = Categories.Personal,
                CreatedDate = DateTime.Now,
                Content = "Kitap oku ..",
                Title ="Karantina günlerinde neler yaparız ?"
            });

            return articleList;
        }

        // GET api/article/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/article
        [HttpPost]
        public string Post([FromBody] Article value)
        {
            return value.Title;
        }

        // PUT api/article/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/article/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
