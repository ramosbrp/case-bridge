using CaseBridge.Domain.Entities;
using CaseBridge.Domain.Ports;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBridge.Infrastructure
{
    public class ProcessRepository : IProcessRepository
    {
        private readonly string _connectionString;

        public ProcessRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<Process> CreateAsync(Process process)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var sql = "INSERT INTO Processes (title, number, status, created_at) VALUES (@Title, @Number, @Status, @CreatedAt) RETURNING id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("Title", process.Title);
                    cmd.Parameters.AddWithValue("Number", process.Number);
                    cmd.Parameters.AddWithValue("Status", process.Status);
                    cmd.Parameters.AddWithValue("CreatedAt", process.CreatedAt);
                    process.Id = Convert.ToInt32(await cmd.ExecuteScalarAsync());
                }
            }
            return process;
        }

        public async Task<Process> GetByIdAsync(int id)
        {
            Process process = null;
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var sql = "SELECT id, title, status, created_at FROM Processes WHERE id = @Id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("Id", id);
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        if (await reader.ReadAsync())
                        {
                            process = new Process(reader.GetString(1)) // Passa o título diretamente, pois Process sempre precisa de um "title"
                            {
                                Id = reader.GetInt32(0),
                                Status = reader.GetString(2),
                                CreatedAt = reader.GetDateTime(3)
                            };
                        }
                    }
                }
            }
            return process;
        }

        public async Task<IEnumerable<Process>> GetAllAsync()
        {
            var processes = new List<Process>();
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var sql = "SELECT id, title, status, created_at FROM Processes";
                using (var cmd = new NpgsqlCommand(sql, conn))
                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        processes.Add(new Process(reader.GetString(1))
                        {
                            Id = reader.GetInt32(0),
                            Status = reader.GetString(2),
                            CreatedAt = reader.GetDateTime(3)
                        });
                    }
                }
            }
            return processes;
        }

        public async Task UpdateAsync(Process process)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var sql = "UPDATE Processes SET title = @Title, status = @Status, created_at = @CreatedAt WHERE id = @Id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("Title", process.Title);
                    cmd.Parameters.AddWithValue("Status", process.Status);
                    cmd.Parameters.AddWithValue("CreatedAt", process.CreatedAt);
                    cmd.Parameters.AddWithValue("Id", process.Id);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task DeleteAsync(int id)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var sql = "DELETE FROM Processes WHERE id = @Id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("Id", id);
                    await cmd.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task CreateAssociation(ProcessClient processClient)
        {
            using (var conn = new NpgsqlConnection(_connectionString))
            {
                await conn.OpenAsync();
                var sql = "INSERT INTO Process_clients (process_id, client_id) VALUES (@processId, @clientId) RETURNING process_id";
                using (var cmd = new NpgsqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("ProcessId", processClient.ProcessId);
                    cmd.Parameters.AddWithValue("ClientId", processClient.ClientId);
                    await cmd.ExecuteNonQueryAsync();

                }

            }
        }
    }
}
