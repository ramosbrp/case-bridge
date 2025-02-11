using CaseBridge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBridge.Domain.Ports
{
    public interface IClientRepository
    {
        Task<Client> GetByEmailAsync(string email);
        Task<Client> GetByIdAsync(int id);
        Task<Client> CreateAsync(Client client);
        Task<IEnumerable<Client>> GetAllAsync();
        Task UpdateAsync(Client client);
        Task DeleteAsync(int id);
    }
}
