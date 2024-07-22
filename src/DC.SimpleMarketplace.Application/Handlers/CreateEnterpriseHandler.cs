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
    public class CreateEnterpriseHandler : IRequestHandler<CreateEnterpriseCommand, BaseResponse<Enterprise>>
    {
        private readonly DCContext _dbContext;
        private readonly IValidator<CreateEnterpriseCommand> _validator;

        public CreateEnterpriseHandler(DCContext dbContext, IValidator<CreateEnterpriseCommand> validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }

        public async Task<BaseResponse<Enterprise>> Handle(CreateEnterpriseCommand command, CancellationToken cancellationToken)
        {
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
                var enterpriseExists = await _dbContext.Enterprise.AsNoTracking().FirstOrDefaultAsync(x => x.Document == command.Document);
                if (enterpriseExists != null)
                    return BaseResponse<Enterprise>.Error(400, $"Document arready exists in Database");

                var newEnterprise = new Enterprise(command.Name, command.Document, command.Address1, command.PostalCode, command.Address2, command.Address3, 
                    command.Address4, command.Email, command.Phone, command.ContactName);

                _dbContext.Enterprise.Add(newEnterprise);
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();

                return BaseResponse<Enterprise>.Success(newEnterprise);
            }
            catch(Exception ex)
            {
                await transaction.RollbackAsync();
                return BaseResponse<Enterprise>.Error(500, ex.Message);
            }
        }
    }
}
