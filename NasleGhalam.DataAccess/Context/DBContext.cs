using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Text;
using NasleGhalam.Common;
using NasleGhalam.DomainClasses.Entities;
using NasleGhalam.DomainClasses.EntityConfigs;
using Action = NasleGhalam.DomainClasses.Entities.Action;

namespace NasleGhalam.DataAccess.Context
{
    public class DBContext : DbContext, IUnitOfWork
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // disable cascade delete
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            // default navarcha(50)
            modelBuilder.Properties<string>().Configure(p => p.HasMaxLength(50));

            modelBuilder.Configurations.AddFromAssembly(typeof(ActionConfig).Assembly);
            
            base.OnModelCreating(modelBuilder);
        }


        #region ### Unit Of Work ###
        public new IDbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            return base.Set<TEntity>();
        }
        public void MarkAsChanged<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Modified;
        }
        public void MarkAsDeleted<TEntity>(TEntity entity) where TEntity : class
        {
            Entry(entity).State = EntityState.Deleted;
        }

        public MessageResult CommitChanges(CrudType type = CrudType.None, string fieldName = "")
        {
            MessageResult result = new MessageResult();

            try
            {
                SaveChanges();

                string str;
                switch (type)
                {
                    case CrudType.Create:
                        str = " با موفقیت ثبت گردید.";
                        break;
                    case CrudType.Update:
                        str = " با موفقیت ویرایش گردید.";
                        break;
                    case CrudType.Delete:
                        str = " با موفقیت حذف گردید.";
                        break;
                    default:
                        str = "عملیات با موفقیت انجام گردید.";
                        break;
                }

                if (!string.IsNullOrEmpty(fieldName) && type != CrudType.None)
                {
                    str = fieldName + str;
                }

                result.FaMessage = str;
                result.MessageType = MessageType.Success;
            }
            catch (SqlException ex)
            {
                StringBuilder err = new StringBuilder();
                foreach (SqlError sql_err in ex.Errors)
                {
                    err.Append(sql_err.Message);
                    result.ErrorNumber = sql_err.Number;
                }

                result.FaMessage = "خطا در اعمال اطلاعات! با مدیر تماس بگیرید.";
                result.MessageType = MessageType.Error;
                result.EnMessage = err.ToString();
            }
            catch (DbUpdateException ex)
            {
                SqlException innerException = null;
                Exception tmp = ex;
                while (innerException == null && tmp != null)
                {
                    if (tmp.InnerException != null)
                    {
                        innerException = tmp.InnerException as SqlException;
                    }
                    tmp = tmp.InnerException;
                }
                if (innerException != null && (innerException.Number == 2601 || innerException.Number == 2627))
                {
                    result.FaMessage = string.IsNullOrEmpty(fieldName) ? "خطای تکراری بودن داده! با مدیر تماس بگیرید." : $"این {fieldName} تکراری میباشد";
                    result.ErrorNumber = 2601;
                }
                else if (innerException != null && innerException.Number == 547)
                {
                    result.FaMessage = "خطا در حذف اطلاعات <br />";
                    result.FaMessage += "خطای رابطه ای! این موجودیت با دیگر جداول در ارتباط میباشد... ابتدا آنها را حذف نمایید";

                    result.ErrorNumber = 547;
                }
                else
                {
                    result.FaMessage = "خطا در اعمال اطلاعات! با مدیر تماس بگیرید.";
                    result.ErrorNumber = innerException?.Number ?? 0;
                }

                result.MessageType = MessageType.Error;
                result.EnMessage = innerException?.ToString() ?? ex.ToString();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    //Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                    //    eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        result.FaMessage += ve.PropertyName + ": " + ve.ErrorMessage + "</br>";
                    }
                }

                result.MessageType = MessageType.Error;
            }

            return result;
        }
        #endregion


        #region ### Db Sets ###
        public DbSet<Action> Actions { get; set; }

        public DbSet<Controller> Controllers { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }
        #endregion
    }
}
