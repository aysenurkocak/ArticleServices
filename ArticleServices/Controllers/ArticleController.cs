using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFLibCore;
using Microsoft.AspNetCore.Mvc;
using PocoLib;

namespace ArticleServices.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ArticleController : ControllerBase
    {
        private UnitOfWork operation;
        private List<Article> articleList;

        [HttpGet]
        public List<Article> Get()
        {
            articleList = new List<Article>();
            operation = new UnitOfWork();
            var courses = operation.ArticleRepository.Get();
            articleList =  courses.ToList();    
            return articleList;
        }


        [HttpGet("{id}")]
        public Article Get(int id)
        {
            operation = new UnitOfWork();
            Article article = operation.ArticleRepository.GetByID(id);
            return article;
        }

        [HttpPost]
        public int Post(Article newItem)
        {
            int result = -1;

            operation = new UnitOfWork();
            try
            {
                var item = new Article
                {
                    Category = newItem.Category,
                    CreatedDate = DateTime.Now,
                    Contents = newItem.Contents,
                    Title = newItem.Title,
                    MediaBase64 = newItem.MediaBase64
                };

                operation.ArticleRepository.Insert(item);
                operation.Save();
                result = item.Id;
                if(result < 1)
                    operation.LogRepository.Insert(new Log { Details = result.ToString(), LogDate = DateTime.Now, FunctionName = "ArticlePost", LogType = (int)LogType.Error });
            }
            catch(Exception exc)
            {
                operation.Dispose();
                operation = new UnitOfWork();
                operation.LogRepository.Insert(new Log{Details = exc.InnerException.Message, LogDate = DateTime.Now, FunctionName = "ArticlePost", LogType = (int)LogType.Error });
            }
            finally
            {
                operation.Save();
            }
            return result;

        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {

        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            operation = new UnitOfWork();
            try
            {
                operation.ArticleRepository.Delete(id);
                operation.Save();
                operation.LogRepository.Insert(new Log { Details = "", LogDate = DateTime.Now, FunctionName = "ArticleDelete", LogType = (int)LogType.Error });
                return true;
            }
            catch(Exception exc)
            {
                operation.LogRepository.Insert(new Log { Details = exc.InnerException.Message, LogDate = DateTime.Now, FunctionName = "ArticleDelete", LogType = (int)LogType.Error });
                return false;
            }

            finally
            {
                operation.Save();
                operation.Dispose();
            }

        }
    }
}
