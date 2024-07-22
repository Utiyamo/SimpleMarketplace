using DC.SimpleMarketplace.Domain.Commands;
using DC.SimpleMarketplace.Domain.Entities;
using DC.SimpleMarketplace.Domain.Models;
using DC.SimpleMarketplace.Infrastructure.ORM;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SimpleMarketplace.Application.Handlers
{
    public class DeleteEnterpriseHandler : IRequestHandler<DeleteEnterpriseCommand, BaseResponse>
    {
        private readonly DCContext _dbContext;
        private readonly IValidator<DeleteEnterpriseCommand> _validator;

        public DeleteEnterpriseHandler(DCContext dbContext, IValidator<DeleteEnterpriseCommand> validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }

        public async Task<BaseResponse> Handle(DeleteEnterpriseCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var enterprise = await _dbContext.Enterprise.AsNoTracking().FirstOrDefaultAsync(x => x.ID == command.Id);
                if (enterprise == null)
                    return new BaseResponse
                    {
                        isSuccess = false,
                        Status = 404,
                        Message = $"Enterprise {command.Id} not found"
                    };

                var validationResult = _validator.Validate(command);
                if (!validationResult.IsValid)
                {
                    string errorMessages = "";
                    foreach (var error in validationResult.Errors)
                    {
                        errorMessages += $"{error} / ";
                    }
                    return BaseResponse<Enterprise>.Error(400, errorMessages);
                }

                using var transaction = await _dbContext.Database.BeginTransactionAsync();

                try
                {
                    _dbContext.Enterprise.Remove(enterprise);

                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return new BaseResponse
                    {
                        isSuccess = true,
                        Status = 200,
                        Message = string.Empty
                    };
                }
                catch(Exception ex)
                {
                    await transaction.RollbackAsync();
                    return new BaseResponse
                    {
                        isSuccess = false,
                        Status = 500,
                        Message = ex.Message
                    };
                }
            }
            catch(Exception ex)
            {
                return new BaseResponse
                {
                    isSuccess = false,
                    Status = 500,
                    Message = ex.Message
                };
            }
        }
    }
}
