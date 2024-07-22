using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SimpleMarketplace.Domain.Entities
{
    public class Enterprise : BaseEntity<long>
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

        public Enterprise() { }

        public Enterprise(string name, string document, string address1, string postalCode, string address2 = null, string address3 = null, string address4 = null, string email = null,
            string phone = null, string contactName = null)
        {
            this.Name = name;
            this.Document = document;
            this.Address1 = address1;
            
            if (!String.IsNullOrEmpty(address2))
                this.Address2 = address2;

            if (!String.IsNullOrEmpty(address3))
                this.Address3 = address3;

            if (!String.IsNullOrEmpty(address4))
                this.Address4 = address4;

            this.CreatedAte = DateTime.Now;
            this.LastUpdate = null;

            this.PostalCode = postalCode;

            if (!String.IsNullOrEmpty(email))
                this.Email = email;

            if(!String.IsNullOrEmpty(phone))
                this.Phone = phone;

            if(!String.IsNullOrEmpty(contactName))
                this.ContactName = contactName;
        }

        public Enterprise(long iD, string name, string document, DateTime createdAte, DateTime? lastUpdate, string address1, string? address2, string? address3, string? address4, string postalCode, string? email, string? phone, string contactName)
        {
            ID = iD;
            Name = name;
            Document = document;
            CreatedAte = createdAte;
            LastUpdate = lastUpdate;
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
