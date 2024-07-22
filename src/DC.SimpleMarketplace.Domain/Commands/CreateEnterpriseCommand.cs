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
    public class CreateEnterpriseCommand : IRequest<BaseResponse<Enterprise>>
    {
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

        public CreateEnterpriseCommand() { }

        public CreateEnterpriseCommand(string name, string document, string address1, string? address2, string? address3, string? address4, string postalCode, string? email, string? phone, string contactName)
        {
            Name = name;
            Document = document;
            Address1 = address1;
            Address2 = address2;
            Address3 = address3;
            Address4 = address4;
            PostalCode = postalCode;
            Email = email;
            Phone = phone;
            ContactName = contactName;
        }
    }
}
