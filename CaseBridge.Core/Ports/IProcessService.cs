using CaseBridge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBridge.Domain.Ports
{
    public interface IProcessService
    {
        Task<Process> CreateProcessAsync(Process process);
        Task<Process> GetProcessAsync(int id);
        Task<IEnumerable<Process>> GetAllProcessesAsync();
        Task UpdateProcessAsync(Process process);
        Task DeleteClientAsync(int id);
    }
}
