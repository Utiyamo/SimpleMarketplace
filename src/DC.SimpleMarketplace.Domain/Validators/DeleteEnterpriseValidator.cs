using DC.SimpleMarketplace.Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SimpleMarketplace.Domain.Validators
{
    public class DeleteEnterpriseValidator : AbstractValidator<DeleteEnterpriseCommand>
    {
        public DeleteEnterpriseValidator()
        {
            RuleFor(command => command.Id).NotEmpty().WithMessage("ID is required");
        }
    }
}
