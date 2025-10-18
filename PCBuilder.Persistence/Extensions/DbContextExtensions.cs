using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace PCBuilder.Persistence.Extensions;

public static class DbContextExtensions
{
    private const string UniqueViolationSqlState = "23505";
    private const string ForeignKeyViolationSqlState = "23503"; 

    public static async Task SaveChangesAndHandleErrorsAsync(
        this DbContext dbContext, 
        CancellationToken ct,
        Action<PostgresException>? onForeignKeyError = null, 
        Action<PostgresException>? onDuplicateKeyError = null) 
    {
        try
        {
            await dbContext.SaveChangesAsync(ct);
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException is PostgresException pgEx)
            {
                if (pgEx.SqlState == UniqueViolationSqlState)
                {
                    onDuplicateKeyError?.Invoke(pgEx); 
                }
                else if (pgEx.SqlState == ForeignKeyViolationSqlState)
                {
                    onForeignKeyError?.Invoke(pgEx); 
                }
            }
            
            throw; 
        }
    }
}