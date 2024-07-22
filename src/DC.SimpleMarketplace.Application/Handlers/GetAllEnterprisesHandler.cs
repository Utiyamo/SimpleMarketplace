using Azure.Core;
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
    public class GetAllEnterprisesHandler : IRequestHandler<GetAllEnterprisesQuery, BaseResponse<PaginationResponse<Enterprise>>>
    {
        private readonly DCContext _dbContext;

        public GetAllEnterprisesHandler(DCContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<BaseResponse<PaginationResponse<Enterprise>>> Handle(GetAllEnterprisesQuery query, CancellationToken cancellationToken)
        {
            try
            {
                var totalRecords = await _dbContext.Enterprise.AsNoTracking().CountAsync(cancellationToken);

                var result = await _dbContext.Enterprise
                    .Skip((query.Page - 1) * query.AmountPerPage)
                    .Take(query.AmountPerPage)
                    .ToListAsync(cancellationToken);

                return BaseResponse<PaginationResponse<Enterprise>>.Success(new PaginationResponse<Enterprise>(
                    query.Page, query.AmountPerPage, totalRecords, result));
            }
            catch(Exception ex)
            {
                return BaseResponse<PaginationResponse<Enterprise>>.Error(500, ex.Message);
            }
        }
    }
}
