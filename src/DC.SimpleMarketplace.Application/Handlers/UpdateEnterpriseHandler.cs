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
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace DC.SimpleMarketplace.Application.Handlers
{
    public class UpdateEnterpriseHandler : IRequestHandler<AlterEnterpriseCommand, BaseResponse<Enterprise>>
    {
        private readonly DCContext _dbContext;
        private readonly IValidator<AlterEnterpriseCommand> _validator;

        public UpdateEnterpriseHandler(DCContext dbContext, IValidator<AlterEnterpriseCommand> validator)
        {
            _dbContext = dbContext;
            _validator = validator;
        }

        public async Task<BaseResponse<Enterprise>> Handle(AlterEnterpriseCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var enterprise = await _dbContext.Enterprise.AsNoTracking().FirstOrDefaultAsync(x => x.ID == command.ID);
                if (enterprise == null)
                    return BaseResponse<Enterprise>.NotFound($"Enterprise {command.ID} not found");

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
                    var updatableEnterprise = new Enterprise(enterprise.ID, command.Name, command.Document, enterprise.CreatedAte, DateTime.Now, command.Address1, command.Address2,
                        command.Address3, command.Address4, command.PostalCode, command.Email, command.Phone, command.ContactName);

                    _dbContext.Enterprise.Update(updatableEnterprise);

                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return BaseResponse<Enterprise>.Success(updatableEnterprise);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    return BaseResponse<Enterprise>.Error(500, ex.Message);
                }
            }
            catch (Exception ex)
            {
                return BaseResponse<Enterprise>.Error(500, ex.Message);
            }
        }
    }
}
