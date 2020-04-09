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
                if(result > 0)
                    operation.LogRepository.Insert(new Log { Details = result.ToString(), LogDate = DateTime.Now, FunctionName = "ArticlePost", LogType = (int)LogType.SuccessAudit });
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

        [HttpPut]
        public int Put(Article updateItem)
        {
            int result = -1;

            operation = new UnitOfWork();
            try
            {
                Article OldItem = Get(updateItem.Id);
                if(OldItem != null)
                {
                    var item = new Article
                    {
                        Id = updateItem.Id,
                        Category = updateItem.Category,
                        UpdateDate = DateTime.Now,
                        CreatedDate = OldItem.CreatedDate,
                        Contents = updateItem.Contents,
                        Title = updateItem.Title,
                        MediaBase64 = updateItem.MediaBase64
                    };

                    operation.ArticleRepository.Update(item);
                    operation.Save();
                    operation.LogRepository.Insert(new Log { Details = result.ToString(), LogDate = DateTime.Now, FunctionName = "ArticlePut", LogType = (int)LogType.SuccessAudit });
                }
                else
                {
                    operation.LogRepository.Insert(new Log { Details = "Object Undefined", LogDate = DateTime.Now, FunctionName = "ArticlePut", LogType = (int)LogType.Error });
                }

            }
            catch (Exception exc)
            {
                operation.Dispose();
                operation = new UnitOfWork();
                operation.LogRepository.Insert(new Log { Details = exc.InnerException.Message, LogDate = DateTime.Now, FunctionName = "ArticlePut", LogType = (int)LogType.Error });
            }
            finally
            {
                operation.Save();
            }
            return result;
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            operation = new UnitOfWork();
            try
            {
                operation.ArticleRepository.Delete(id);
                operation.Save();
                operation.LogRepository.Insert(new Log { Details = id.ToString(), LogDate = DateTime.Now, FunctionName = "ArticleDelete", LogType = (int)LogType.SuccessAudit });
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
