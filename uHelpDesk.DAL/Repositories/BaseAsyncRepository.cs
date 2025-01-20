using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uHelpDesk.DAL.Contracts;
using uHelpDesk.Models;

namespace uHelpDesk.DAL.Repositories
{
    public class BaseRepository<T> : IAsyncRepository<T> where T : BaseModel
    {
        protected uHelpDeskDbContext context;
        public BaseRepository(uHelpDeskDbContext dbContext)
        {
            this.context = dbContext;
        }
        public async Task<T> AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            //do not saveChanges here if you are saving "child/nested" objects. This will cause the datacontext 
            //to be disposed and you will get an exception. EF is a Unit of Work (UoW) pattern and changes should
            //be saved when everything is done - as the last thing before exiting. Look in the controller for the
            //saveChanges()
            await context.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            context.Set<T>().Remove(context.Set<T>().FirstOrDefault(x => x.Id == id));
            await context.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        public async Task<IList<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
