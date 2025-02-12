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
        private readonly IProcessRepository _processRepository;

        public ProcessService(IProcessService processService, IClientService clientService, IProcessRepository processRepository) {
            _processService = processService;
            _clientService = clientService;
            _processRepository = processRepository;
        }

        public async Task<Process> CreateProcessWithClientAsync(CreateProcessDto dto)
        {
            try
            {
                //Encontra ou cria o Client
                var client = await _clientService.FindOrCreateClientAsync(dto.ClientName, dto.ClientEmail);

                //Cria o Process
                var process = new Process(dto.Title);
                process = await _processRepository.CreateAsync(process);

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
