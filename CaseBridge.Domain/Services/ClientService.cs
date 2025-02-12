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
        private readonly IClientRepository _clientRepository;
        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<Client> FindOrCreateClientAsync(string name, string email)
        {
            // 1. Tenta encontrar pelo e-mail
            var client = await _clientRepository.GetByEmailAsync(email);

            // 2. Se não existir, cria
            if (client == null)
            {
                client = new Client(name, email);
                client = await _clientRepository.CreateAsync(client);
            }

            return client;
        }
    }
}
