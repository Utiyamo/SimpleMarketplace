using DC.SimpleMarketplace.Domain.Entities;
using DC.SimpleMarketplace.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SimpleMarketplace.Domain.Queries
{
    public class GetEnterpriseQuery : IRequest<BaseResponse<Enterprise>>
    {
        public long Id { get; set; }

        public GetEnterpriseQuery() { }

        public GetEnterpriseQuery(long id)
        {
            Id = id;
        }
    }
}
