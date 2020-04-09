using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFLibCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PocoLib;

namespace CommentServices.Controllers
{
    [Route("api/comment")]
    [ApiController]
    public class CommentController : ControllerBase
    {
        private UnitOfWork operation;
        private List<Comment> CommentList;

        [HttpGet]
        public List<Comment> Get()
        {
            CommentList = new List<Comment>();
            operation = new UnitOfWork();
            var courses = operation.CommentRepository.Get();
            CommentList = courses.ToList();
            return CommentList;
        }


        [HttpGet("{id}")]
        public Comment Get(int id)
        {
            operation = new UnitOfWork();
            Comment Comment = operation.CommentRepository.GetByID(id);
            return Comment;
        }

        [HttpPost]
        public int Post(Comment newItem)
        {
            int result = -1;

            operation = new UnitOfWork();
            try
            {
                var item = new Comment
                {
                    ArticleId = newItem.ArticleId,
                    CreatedDate = DateTime.Now,
                    Star = newItem.Star,
                    Email = newItem.Email,
                    Contents = newItem.Contents,
                    Name = newItem.Name,
                    Allowance = newItem.Allowance
                };

                operation.CommentRepository.Insert(item);
                operation.Save();
                result = item.Id;
                if (result > 0)
                    operation.LogRepository.Insert(new Log { Details = result.ToString(), LogDate = DateTime.Now, FunctionName = "CommentPost", LogType = (int)LogType.SuccessAudit });
            }
            catch (Exception exc)
            {
                operation.Dispose();
                operation = new UnitOfWork();
                operation.LogRepository.Insert(new Log { Details = exc.InnerException.Message, LogDate = DateTime.Now, FunctionName = "CommentPost", LogType = (int)LogType.Error });
            }
            finally
            {
                operation.Save();
            }
            return result;

        }

        [HttpPut]
        public int Put(Comment updateItem)
        {
            int result = -1;

            operation = new UnitOfWork();
            try
            {
                Comment OldItem = Get(updateItem.Id);
                if (OldItem != null)
                {
                    var item = new Comment
                    {
                        Id = OldItem.Id,
                        ArticleId = OldItem.ArticleId,
                        Star = updateItem.Star,
                        UpdateDate = DateTime.Now,
                        CreatedDate = OldItem.CreatedDate,
                        Contents = updateItem.Contents,
                        Name = updateItem.Name,
                        Allowance = updateItem.Allowance,
                        Email = updateItem.Email

                    };

                    operation.CommentRepository.Update(item);
                    operation.Save();
                    operation.LogRepository.Insert(new Log { Details = result.ToString(), LogDate = DateTime.Now, FunctionName = "CommentPut", LogType = (int)LogType.SuccessAudit });
                }
                else
                {
                    operation.LogRepository.Insert(new Log { Details = "Object Undefined", LogDate = DateTime.Now, FunctionName = "CommentPut", LogType = (int)LogType.Error });
                }

            }
            catch (Exception exc)
            {
                operation.Dispose();
                operation = new UnitOfWork();
                operation.LogRepository.Insert(new Log { Details = exc.Message, LogDate = DateTime.Now, FunctionName = "CommentPut", LogType = (int)LogType.Error });
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
                operation.CommentRepository.Delete(id);
                operation.Save();
                operation.LogRepository.Insert(new Log { Details = id.ToString(), LogDate = DateTime.Now, FunctionName = "CommentDelete", LogType = (int)LogType.SuccessAudit });
                return true;
            }
            catch (Exception exc)
            {
                operation.LogRepository.Insert(new Log { Details = exc.InnerException.Message, LogDate = DateTime.Now, FunctionName = "CommentDelete", LogType = (int)LogType.Error });
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
