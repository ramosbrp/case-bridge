using CaseBridge.Domain.DTO;
using CaseBridge.Domain.Entities;
using CaseBridge.Domain.Ports;

namespace CaseBridge.Domain.Services
{
    public class ProcessService: IProcessService
    {
        private readonly IClientService _clientService;
        private readonly IProcessRepository _processRepository;

        public ProcessService(IClientService clientService, IProcessRepository processRepository) {
            _clientService = clientService;
            _processRepository = processRepository;
        }

        public async Task<Process> CreateProcessWithClientAsync(CreateProcessDto dto)
        {
            try
            {
                // 1. Encontra ou cria o Client
                var client = await _clientService.FindOrCreateClientAsync(dto.ClientName, dto.ClientEmail);

                // 2. Cria o Process
                var process = new Process(dto.Title);
                process = await _processRepository.CreateAsync(process);

                // 3. Cria a relação (ProcessClient)
                var process_client = new ProcessClient(process.Id, client.Id);
                await _processRepository.CreateAssociation(process_client);

                return process;
            }
            catch (Exception ex)
            {
                throw;
            }
           
        }


    }
}
