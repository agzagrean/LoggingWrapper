using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Validation;
using System.Linq;
using Iris.Logging.Dal.Entities;

namespace Iris.Logging.Dal
{
    public class LoggingDbContext : DbContext, IDbContext
    {
        public LoggingDbContext() : base(nameof(LoggingDbContext))
        {
        }

        public override int SaveChanges()
        {
            try
            {
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                var message = ex.EntityValidationErrors.Where(x => !x.IsValid).Aggregate(string.Empty, (current, errors) => current + ("\r\n\tError: \r\n" + string.Join(",", errors.ValidationErrors.Select(x => string.Format("\t\tFieldName: {0} {1}\r\n", x.PropertyName, x.ErrorMessage)))));
                throw new InvalidOperationException("Cannot save changes because validation failed:  " + message, ex);
            }
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

    }
}
