using CaseBridge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBridge.Domain.Ports
{
    public interface IClientService
    {
        Task<Client> FindOrCreateClientAsync(string name, string email);
    }
}