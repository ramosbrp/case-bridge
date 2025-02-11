using CaseBridge.Domain.Entities;
using CaseBridge.Domain.Ports;
using Npgsql;
using System.Xml.Linq;

namespace CaseBridge.Infrastructure;

public class ClientRepository : IClientRepository
{
    private readonly string _connectionString;

    public ClientRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public async Task<Client> GetByEmailAsync(string email)
    {
        Client client = null;
        using (var conn = new NpgsqlConnection(_connectionString))
        {
            await conn.OpenAsync();
            var sql = "SELECT id, name, email FROM Clients WHERE email = @Email";
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("Email", email);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        client = new Client(reader.GetString(1), reader.GetString(2))
                        {
                            Id = reader.GetInt32(0),
                        };
                    }
                }
            }
        }
        return client;
    }

    public async Task<Client> GetByIdAsync(int id)
    {
        Client client = null;
        using (var conn = new NpgsqlConnection(_connectionString))
        {
            await conn.OpenAsync();
            var sql = "SELECT id, name, email FROM Clients WHERE id = @Id";
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("Id", id);
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        client = new Client(reader.GetString(1), reader.GetString(2))
                        {
                            Id = reader.GetInt32(0),
                        };
                    }
                }
            }
        }
        return client;
    }

    public async Task<Client> CreateAsync(Client client)
    {
        using (var conn = new NpgsqlConnection(_connectionString))
        {
            await conn.OpenAsync();
            var sql = "INSERT INTO Clients (name, email) VALUES (@Name, @Email) RETURNING id";
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("Name", client.Name);
                cmd.Parameters.AddWithValue("Email", client.Email);
                client.Id = Convert.ToInt32(await cmd.ExecuteScalarAsync());
            }
        }
        return client;
    }

    public async Task<IEnumerable<Client>> GetAllAsync()
    {
        var clients = new List<Client>();
        using (var conn = new NpgsqlConnection(_connectionString))
        {
            await conn.OpenAsync();
            var sql = "SELECT id, name, email FROM Clients";
            using (var cmd = new NpgsqlCommand(sql, conn))
            using (var reader = await cmd.ExecuteReaderAsync())
            {
                while (await reader.ReadAsync())
                {
                    clients.Add(new Client(reader.GetString(1), reader.GetString(2))
                    {
                        Id = reader.GetInt32(0),
                        Name = reader.GetString(1),
                        Email = reader.GetString(2)
                    });
                }
            }
        }
        return clients;
    }

    public async Task UpdateAsync(Client client)
    {
        using (var conn = new NpgsqlConnection(_connectionString))
        {
            await conn.OpenAsync();
            var sql = "UPDATE Clients SET name = @Name, email = @Email WHERE id = @Id";
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("Name", client.Name);
                cmd.Parameters.AddWithValue("Email", client.Email);
                cmd.Parameters.AddWithValue("Id", client.Id);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }

    public async Task DeleteAsync(int id)
    {
        using (var conn = new NpgsqlConnection(_connectionString))
        {
            await conn.OpenAsync();
            var sql = "DELETE FROM Clients WHERE id = @Id";
            using (var cmd = new NpgsqlCommand(sql, conn))
            {
                cmd.Parameters.AddWithValue("Id", id);
                await cmd.ExecuteNonQueryAsync();
            }
        }
    }
}

