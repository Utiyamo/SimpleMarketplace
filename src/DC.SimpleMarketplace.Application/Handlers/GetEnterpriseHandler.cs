using DC.SimpleMarketplace.Domain.Entities;
using DC.SimpleMarketplace.Domain.Models;
using DC.SimpleMarketplace.Domain.Queries;
using DC.SimpleMarketplace.Infrastructure.ORM;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SimpleMarketplace.Application.Handlers
{
    public class GetEnterpriseHandler : IRequestHandler<GetEnterpriseQuery, BaseResponse<Enterprise>>
    {
        private readonly DCContext _dbContext;

        public GetEnterpriseHandler(DCContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BaseResponse<Enterprise>> Handle(GetEnterpriseQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var enterprise = await _dbContext.Enterprise.AsNoTracking().FirstOrDefaultAsync(x => x.ID == query.Id);
                if (enterprise == null)
                    return BaseResponse<Enterprise>.NotFound($"Enterprise with this ID {query.Id} not found");

                return BaseResponse<Enterprise>.Success(enterprise);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
