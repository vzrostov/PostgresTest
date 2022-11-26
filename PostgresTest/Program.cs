// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Hosting;
using Npgsql;
using PostgresTest.Infrastructure;
using PostgresTest.Model;

NpgsqlConnection? connection = null;
try
{
    // db install
    var dbPassword = "Sql123456";
    var connectionString = $"Host=localhost;Username=postgres;Password={dbPassword};Database=postgres";
    await using var dataSource = NpgsqlDataSource.Create(connectionString);
    connection = await dataSource.OpenConnectionAsync();
}
catch (Exception ex)
{
    Console.WriteLine(ex.ToString());
}

// timer
int count = 11;
TimerCallback timerCallback = (st) => 
{ 
    try
    {
        if(connection != null)
            PopulateDb();
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }
};
Timer timer = new Timer(timerCallback, null, 0, 1000);

Console.ReadLine();

async void PopulateDb()
{
    Console.WriteLine("Record!");
    var d = DateTime.Now;
    Payload payload = new Payload() { Id = count++, DateTime = d, Digit = d.Second + d.Minute + d.Hour, Info = d.ToString() };
    await using var cmd = 
         new NpgsqlCommand("INSERT INTO payloads (id, digit, datetime, info) VALUES (($1), ($2), ($3), ($4))", connection)
    {
        Parameters =
        {
            new() { Value = payload.Id },
            new() { Value = payload.Digit },
            new() { Value = payload.DateTime },
            new() { Value = payload.Info }
        }
    };

    await cmd.ExecuteNonQueryAsync();
}
