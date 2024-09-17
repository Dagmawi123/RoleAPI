using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OrgRoles.Models;

namespace OrgRoles.Interceptors
{
    public class EFSaveChangesInterceptor : SaveChangesInterceptor
    {
      public async override  ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,InterceptionResult<int> result,CancellationToken cancellationToken = default)
        {
            RoleContext? dbContext = eventData.Context as RoleContext;

            //if (dbContext == null)
            //{
            //    return base.SavingChanges(eventData, result);
            //}
            var entities = dbContext!.ChangeTracker.Entries().ToList();
            foreach (var entry in entities)
            {

                var auditMessage = entry.State;

                if (entry.State == EntityState.Deleted)
                {
                    CreateDeleted(entry);
                }
                if (entry.State == EntityState.Modified)
                {
                    CreateModified(entry);

                }
                if (entry.State == EntityState.Added)
                {
                    CreateAdded(entry);
                }

                Console.WriteLine("Audit Message..." + auditMessage);

            }
           return await base.SavingChangesAsync(eventData, result);
           // return await result;
        }

        private void CreateAdded(EntityEntry entry)
        {
            if (entry.Entity is Role role)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("Going to save this new Role object...\n" + role.Id.ToString() + "...." + role.Name);
            }
            //throw new NotImplementedException();

        }

        private void CreateModified(EntityEntry entry)
        {
            //Console.WriteLine("Modified role object going to be saved...", entry);
            if (entry.Entity is Role role)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Modified role object going to be saved...\n" + role.Id.ToString() + "...." + role.Name);
            }
          //  Console.Colo

        }

        private void CreateDeleted(EntityEntry entry)
        {
            if (entry.Entity is Role role)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Deleted role going to be saved ...\n" + role.Id.ToString() + "...." + role.Name);
               
            }
            //Console.WriteLine("Deleted role going to be saved ...", entry);
        }
    }
}
