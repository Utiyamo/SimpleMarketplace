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
    public class GetAllEnterprisesQuery : IRequest<BaseResponse<PaginationResponse<Enterprise>>>
    {
        public int Page { get; set; }
        public int AmountPerPage { get; set; }

        public GetAllEnterprisesQuery() { }

        public GetAllEnterprisesQuery(int page, int amountPerPage)
        {
            Page = page;
            AmountPerPage = amountPerPage;
        }
    }
}
