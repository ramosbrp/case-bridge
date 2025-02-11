using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseBridge.Domain.DTO
{
    public class CreateProcessDto
    {
        public string Title { get; set; }
        public string ClientName { get; set; }
        public string ClientEmail { get; set; }
    }

}
