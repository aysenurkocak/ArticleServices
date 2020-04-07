using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

/**
 * GenericRepository
 * - EF aracılığıyla gerçekleşen CRUD işlemlerini farklı nesne tipleri için de yapabilmeyi sağlayan class
 * @author  Ayşe Nur 
 * @version 1.0
 * @CreatedDate 07.04.2020
 */

namespace EFLibCore
{

    public class GenericRepository<TEntity> where TEntity : class
    {
        internal DBContext context;
        internal DbSet<TEntity> dbSet;

        public GenericRepository(DBContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Kayıt getir
        /// </summary>
        /// <param name="filter">kriter</param>
        /// <param name="orderBy">sıralama</param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        /// <summary>
        /// Kayıt getir
        /// </summary>
        /// <param name="id">Id ye göre arama</param>
        /// <returns></returns>
        public virtual TEntity GetByID(object id)
        {
            return dbSet.Find(id);
        }

        /// <summary>
        /// Ekleme işlemi
        /// </summary>
        /// <param name="entity">Eklenecek nesne</param>
        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        /// <summary>
        /// Silme işlemi
        /// </summary>
        /// <param name="id">silinecek nesne Id si</param>
        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        /// <summary>
        /// Silme işlemi
        /// </summary>
        /// <param name="entityToDelete">silinecek nesne</param>
        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                dbSet.Attach(entityToDelete);
            }
            dbSet.Remove(entityToDelete);
        }

        /// <summary>
        /// Güncelleme işlemi 
        /// </summary>
        /// <param name="entityToUpdate">Güncellenecek kayıta ait nesne</param>
        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }

}