using CaseBridge.Domain.Entities;
using CaseBridge.Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBridge.Domain.Services
{
    public class ProcessService: IProcessService
    {
        private readonly IProcessService _processService;

        public ProcessService(IProcessService processService) {
            _processService = processService;
        }

        public async Task<Process> CreateProcessAsync(string title)
        {
            try
            {
                var newProcess = new Process(title);

                return newProcess;
            }
            catch (Exception)
            {
                throw;
            }
           
        }


    }
}
