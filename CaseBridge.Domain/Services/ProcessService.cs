using CaseBridge.Domain.DTO;
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
        private readonly IClientService _clientService;

        public ProcessService(IProcessService processService, IClientService clientService) {
            _processService = processService;
            _clientService = clientService;
        }

        public async Task<Process> CreateProcessWithClientAsync(CreateProcessDto dto)
        {
            try
            {
                //Encontra ou cria o Client
                var client = await _clientService.FindOrCreateClientAsync(dto.ClientName, dto.ClientEmail);

                //Cria o Process
                var process = new Process(dto.Title);

                //Cria a relação (ProcessClient)
                return process;
            }
            catch (Exception)
            {
                throw;
            }
           
        }


    }
}
