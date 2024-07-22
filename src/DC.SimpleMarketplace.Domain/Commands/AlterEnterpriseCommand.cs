using DC.SimpleMarketplace.Domain.Entities;
using DC.SimpleMarketplace.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SimpleMarketplace.Domain.Commands
{
    public class AlterEnterpriseCommand : IRequest<BaseResponse<Enterprise>>
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public String Address1 { get; set; }
        public String? Address2 { get; set; }
        public String? Address3 { get; set; }
        public String? Address4 { get; set; }
        public String PostalCode { get; set; }
        public String? Email { get; set; }
        public String? Phone { get; set; }
        public String? ContactName { get; set; }
    }
}
