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
        //Task<Process?> GetByIdAsync(Process process);
        //Task<IEnumerable<Process>> GetAllAsync();
        Task<Process> CreateProcessAsync(string process);
        //Task UpdateAsync(Process process);
        //Task DeleteAsync(int id);
    }
}
