using Cs2Ext.Persistence.Schema;
using Microsoft.Data.Sqlite;

namespace Cs2Ext.Persistence.Stores;

public sealed class MigrationRunner
{
    private readonly SqliteConnection _connection;

    public MigrationRunner(SqliteConnection connection)
    {
        _connection = connection;
    }

    public async Task ApplyAsync(CancellationToken cancellationToken)
    {
        foreach (var statement in SchemaDefinitions.AllMigrations)
        {
            await using var command = _connection.CreateCommand();
            command.CommandText = statement;
            await command.ExecuteNonQueryAsync(cancellationToken).ConfigureAwait(false);
        }
    }
}
