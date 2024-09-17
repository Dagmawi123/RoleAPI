using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Diagnostics;
using OrgRoles.Models;
using System.Data.Common;

namespace OrgRoles.Interceptors
{
    public class EFLoggingInterceptor : DbCommandInterceptor
    {
        public override InterceptionResult<int> NonQueryExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<int> result)
        {
            Console.WriteLine($"Executing NonQuery: {command.CommandText}");
            return base.NonQueryExecuting(command, eventData, result);
        }
        public override InterceptionResult<DbDataReader> ReaderExecuting(DbCommand command, CommandEventData eventData, InterceptionResult<DbDataReader> result)
        {
            Console.WriteLine($"Executing Reader: {command.CommandText}");
            return base.ReaderExecuting(command, eventData, result);
        }
        // Additional methods for various actions such as ScalarExecuting, ScalarExecuted, etc.

       
    }
}
