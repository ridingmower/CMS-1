using CMS.Core.Database;
using CMS.Domain.Domains;
using CMS.Domain.Domains.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Core.Repository
{
    public abstract class AbstractBaseRepository<T> : IDisposable where T : class,IBaseEntity
    {

        internal CMSDbContext context = null;
        public DbSet<T> Entity { get { return context.Set<T>(); } }

        public AbstractBaseRepository()
        {
            context = new CMSDbContext();
        }

        public virtual bool Add(T entity)
        {
            entity.CreateTime = DateTime.Now;
            Entity.Add(entity);
            return context.SaveChanges() > 0;
        }

        public virtual T Find(int Id)
        {
            return Entity.FirstOrDefault(x => x.Id == Id);
        }

        public virtual bool Delete(T entity)
        {
            if(entity!=null && entity.Id != default(int))
            {
                var record = Find(entity.Id);
                Entity.Remove(record);
                //if (record == null)
                //{
                //    throw new NullReferenceException(nameof(entity.Id));
                //}
                
                //record.IsDeleted = true;
                return context.SaveChanges() > 0;
            }
            throw new ArgumentOutOfRangeException(nameof(entity.Id));
        }

        public virtual bool UpdateContent(PageContent entity)
        {
            var model = context.PageContents.Where(p => p.Id == entity.Id).First();
            model.divs = entity.divs;
            model.Content = entity.Content;
            model.CreateTime = entity.CreateTime;
            return context.SaveChanges() > 0;
        }

        public virtual bool Update(T entity)
        {
            var update = Find(entity.Id);
            update = entity;
            return context.SaveChanges() > 0;
        }

        public virtual bool UpdateMenuName(Menu entity)
        {
            var update = context.Menus.Where(x => x.Id == entity.Id).First();
            update.MenuName = entity.MenuName;
            return context.SaveChanges() > 0;
        }



        public virtual bool UpdateLayout(PLItem entity)
        {
            var model = context.PLItems.Where(p => p.Id == entity.Id).First();
            model.SizeValue = entity.SizeValue;
            model.Order = entity.Order;
            model.CreateTime = DateTime.Now;
            return context.SaveChanges() > 0;
        }
        public IQueryable<E> Query<E>() where E : class
        {
            return context.Set<E>();
        }

        public class BaseRepository<T>:AbstractBaseRepository<T>
            where T : class,IBaseEntity
        {

        }

        public void Dispose()
        {
            if (context != null)
            {
                context.Dispose();
            }
        }
    }
}
