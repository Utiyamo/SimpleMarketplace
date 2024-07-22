﻿using DC.SimpleMarketplace.Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SimpleMarketplace.Domain.Validators
{
    public class CreateEnterpriseValidator : AbstractValidator<CreateEnterpriseCommand>
    {
        public CreateEnterpriseValidator() 
        {
            RuleFor(command => command.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(command => command.Document).NotEmpty().WithMessage("Document is required");
            RuleFor(command => command.PostalCode).NotEmpty().WithMessage("PostalCode is required");
            RuleFor(command => command.Address1).NotEmpty().WithMessage("Address1 is required");
        }
    }
}
