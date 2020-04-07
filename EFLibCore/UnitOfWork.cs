using PocoLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/**
 * UnitOfWork
 * -Tek bir transaction ile toplu halde db işlemi
 * @author  Ayşe Nur 
 * @version 1.0
 * @CreatedDate 07.04.2020
 */

namespace EFLibCore
{
   public class UnitOfWork : IDisposable
    {
        private DBContext context = new DBContext();
        private GenericRepository<Article> articleRepository;

        /// <summary>
        /// Article nesnesi için repository
        /// </summary>
        public GenericRepository<Article> ArticleRepository
        {
            get
            {

                if (this.articleRepository == null)
                {
                    this.articleRepository = new GenericRepository<Article>(context);
                }
                return articleRepository;
            }
        }

        /// <summary>
        /// Her create, delete , update sonrası db tetikleme
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        /// <summary>
        /// Bellekten at
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
