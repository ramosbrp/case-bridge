using CaseBridge.Domain.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBridge.Domain.Services
{
    public class ProcessService
    {
        private readonly IProcessService _processService;

        public ProcessService(IProcessService processService) {
            _processService = processService;
        }
    }
}
