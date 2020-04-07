using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
//using ArticleServices.Models;
using EFLibCore;
using Microsoft.AspNetCore.Mvc;
using PocoLib;

namespace ArticleServices.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private UnitOfWork unitOfWork = new UnitOfWork();
        private List<Article> articleList = new List<Article>();

        // GET api/article
        [HttpGet]
        public List<Article> Get()
        {
           
            var courses = unitOfWork.ArticleRepository.Get();
            articleList =  courses.ToList();
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
        public string Post([FromBody] Article newObject)
        {
            unitOfWork.ArticleRepository.Insert(
                new Article
                {
                    Category = newObject.Category,
                    CreatedDate = DateTime.Now,
                    Contents = newObject.Contents,
                    Title = newObject.Title
                }
                );

            unitOfWork.Save();
            return "success";
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
