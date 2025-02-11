using CaseBridge.Domain.Entities;
using CaseBridge.Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBridge.Domain.Services
{
    public class ClientService : IClientService
    {
s        private readonly IClientRepository _clientRepository;
        public ClientService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Client> FindOrCreateClientAsync(string name, string email)
        {
            // 1. Tenta encontrar pelo e-mail
            var existingClient = await _context.Clients
                .FirstOrDefaultAsync(c => c.Email == email);

            // 2. Se existir, retorna
            if (existingClient != null)
            {
                return existingClient;
            }

            // 3. Se não existir, cria e salva
            var newClient = new Client(name, email);
            _context.Clients.Add(newClient);
            await _context.SaveChangesAsync();

            return newClient;
        }
    }
}
