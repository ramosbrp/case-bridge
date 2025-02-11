using CaseBridge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBridge.Domain.Ports
{
    public interface IProcessRepository
    {
        Task<Process> CreateAsync(Process process);
        Task<Process> GetByIdAsync(int id);
        Task<IEnumerable<Process>> GetAllAsync();
        Task UpdateAsync(Process process);
        Task DeleteAsync(int id);
    }

}
