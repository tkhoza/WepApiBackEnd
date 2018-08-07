using WebApiProject.DAL;
using WebApiProject.DAL.Models;
using WebApiProject.Service.IService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiProject.DAL.Infrastructure;

namespace WebApiProject.Service.Service
{
    public class BaseLogic<T> : IBaseLogic<T> where T : class, IAuditableEntity
    {
        private static readonly List<string> _errors = new List<string>();
        private static readonly List<string> _errorDetail = new List<string>();

        //Immutable list to Share a collection of Errors between layers
        public static List<string> Errors => _errors.ToList();
        public static List<string> ErrorDetail => _errorDetail.ToList();

        public static bool HasError => _errors.Any();

        //Static member to return an instance of this object
        public static readonly IBaseLogic<T> Instance = new BaseLogic<T>();

        public async Task<IEnumerable<T>> List()
        {
            try
            {
                Errors.Clear();
                using (var db = new WebApiProjectDbContext())
                using (var repo = new EntityFrameworkRepository<WebApiProjectDbContext>(db))
                {
                    return await repo.GetAllAsync<T>();
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);

                //Add Error message
                //Errors.Add("There was an error trying to reading data from database ");
                _errors.Add("There was an error trying to reading data from database ");

                return null;
            }
        }

        public async Task<bool> Add(T entity)
        {
            try
            {
                _errors.Clear();
                _errorDetail.Clear();
                using (var db = new WebApiProjectDbContext())
                using (var repo = new EntityFrameworkRepository<WebApiProjectDbContext>(db))
                {
                    repo.Create<T>(entity);
                    return await db.SaveChangesAsync() == 0 ? false : true;
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);

                //Add Error message
                _errors.Add("There was an error trying to send the information to the database ");
                return false;
            }
        }

        public async Task Delete(T entity)
        {
            try
            {
                _errors.Clear();
                _errorDetail.Clear();

                using (var db = new WebApiProjectDbContext())
                using (var repo = new EntityFrameworkRepository<WebApiProjectDbContext>(db))
                {
                    repo.Delete<T>(entity);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);

                //Add Error message
                _errors.Add("There was an error trying to send the information to the database ");
                _errorDetail.Add(e.Message);
                _errorDetail.Add(e.InnerException.Message);
            }
        }

        public async Task<T> FindById(string id)
        {
            try
            {
                _errors.Clear();
                _errorDetail.Clear();
                using (var db = new WebApiProjectDbContext())
                using (var repo = new EntityFrameworkRepository<WebApiProjectDbContext>(db))
                {
                    var temp = await repo.GetByIdAsync<T>(id);
                    if (temp.IsDeleted == true)
                    {
                        return null;
                    }
                    return temp;
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);

                //Add Error message
                _errors.Add("There was an error trying to reading data from database");
                _errorDetail.Add(e.Message);
                _errorDetail.Add(e.InnerException.Message);
                return null;
            }
        }

        public async Task Update(T entity)
        {
            try
            {
                _errors.Clear();
                _errorDetail.Clear();
                using (var db = new WebApiProjectDbContext())
                using (var repo = new EntityFrameworkRepository<WebApiProjectDbContext>(db))
                {
                    repo.Update<T>(entity);
                    await db.SaveChangesAsync();
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);

                //Add Error message
                _errors.Add("There was an error trying to send the information to the database");
                _errorDetail.Add(e.Message);
                _errorDetail.Add(e.InnerException.Message);
            }
        }
        public bool ValidateModel(T entity)
        {
            try
            {
                _errors.Clear();

                if (entity == null)
                {

                    _errors.Add("There was an error trying to send the information to the database ");
                    return false;
                }
                if (entity.Id == null)
                {
                    _errors.Add("There was an error trying to send the information to the database ");
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);

                //Add Error message
                _errors.Add("There was an error trying to send the information to the database ");

                return false;
            }
        }
    }
}
