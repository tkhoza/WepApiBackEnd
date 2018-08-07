using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiProject.DAL.Models;

namespace WebApiProject.Service.IService
{
    public interface IBaseLogic<T> where T : class, IAuditableEntity
    {
        Task<IEnumerable<T>> List();
        Task<bool> Add(T entity);
        Task Delete(T entity);
        Task Update(T entity);
        Task<T> FindById(string id);
    }
}
