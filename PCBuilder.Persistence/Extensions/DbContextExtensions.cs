using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace PCBuilder.Persistence.Extensions;

public static class DbContextExtensions
{
    private const string UniqueViolationSqlState = "23505";

    public static async Task SaveChangesAndHandleErrorsAsync(
        this DbContext dbContext, 
        CancellationToken ct,
        Action<PostgresException> onDuplicateKeyError)
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
                    onDuplicateKeyError(pgEx);
                }
            }
            
            throw; 
        }
    }
}