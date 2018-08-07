using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApiProject.DAL;
using WebApiProject.DAL.Infrastructure;
using WebApiProject.DAL.Models;
using WebApiProject.DAL.ViewModel;

namespace WebApiProject.Service.Service.Logic
{
    public class PersonalDetailsLogic : BaseLogic<PersonalDetail>
    {
        // Create Personal Details
        public static async Task<PersonalDetail> CreatePersonalDetails(PersonalDetailViewModel model)
        {
            try
            {
                using (var db = new WebApiProjectDbContext())
                using (var repo = new EntityFrameworkRepository<WebApiProjectDbContext>(db))
                {
                    var entity = new PersonalDetail()
                    {
                        Email = model.Email,
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        BirthDate = model.BirthDate,
                        PhoneNumber = model.PhoneNumber,
                        Age = model.Age,
                    };

                    repo.Create<PersonalDetail>(entity);
                    var result = await db.SaveChangesAsync() == 0 ? null : entity;
                    return result;
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                return null;
            }
        }

        // Find Personal Details
        public static async Task<PersonalDetail> FindPersonalDetailsById(Guid id)
        {
            try
            {
                Errors.Clear();
                using (var db = new WebApiProjectDbContext())
                using (var repo = new EntityFrameworkRepository<WebApiProjectDbContext>(db))
                {
                    var profiles = await db.PersonalDetails.ToListAsync();
                    var profile = profiles.Where(_ => _.Id == id).FirstOrDefault();
                    return (profile);
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);

                //Add Error message
                Errors.Add("There was an error trying to reading data from database");
                return null;
            }
        }

        // Update Personal Details
        public static async Task<bool> UpdatePersonalDetails(PersonalDetailViewModel model)
        {
            try
            {
                Errors.Clear();
                using (var db = new WebApiProjectDbContext())
                using (var repo = new EntityFrameworkRepository<WebApiProjectDbContext>(db))
                {
                    var entity = await db.PersonalDetails.FindAsync(model.Id);
                    entity.FirstName = model.FirstName;
                    entity.LastName = model.LastName;
                    entity.Age = model.Age;
                    entity.BirthDate = model.BirthDate;
                    entity.Email = model.Email;
                    entity.PhoneNumber = model.PhoneNumber;
                   
                    repo.Update<PersonalDetail>(entity);
                    return await db.SaveChangesAsync() == 0 ? false : true;
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);

                //Add Error message
                Errors.Add("There was an error trying to send the information to the database");
                return false;
            }
        }

        // Delete Personal Details
        public static async Task<bool> DeletePersonalDetails(Guid Id)
        {
            try
            {
                Errors.Clear();
                using (var db = new WebApiProjectDbContext())
                using (var repo = new EntityFrameworkRepository<WebApiProjectDbContext>(db))
                {
                    var entity = await db.PersonalDetails.FindAsync(Id);
                    repo.Delete<PersonalDetail>(entity);
                    return await db.SaveChangesAsync() == 0 ? false : true;
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                return false;
            }
        }

        // List all Personal Details
        public static async Task<List<PersonalDetail>> ListAllPersonalDetails()
        {
            try
            {
                using (var db = new WebApiProjectDbContext())
                using (var repo = new EntityFrameworkRepository<WebApiProjectDbContext>(db))
                {
                    var personalDetails = await db.PersonalDetails.ToListAsync();
                    personalDetails = personalDetails.Where(_ => _.IsDeleted == false).ToList();
                    return personalDetails;
                }
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                return null;
            }
        }

    }
}
