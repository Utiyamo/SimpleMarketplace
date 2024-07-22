using DC.SimpleMarketplace.Application.Handlers;
using DC.SimpleMarketplace.Domain.Commands;
using DC.SimpleMarketplace.Test.Mocks;
using FluentAssertions;
using FluentValidation;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SimpleMarketplace.Test.Handlers
{
    public class CreateEnterpriseHandlerTest
    {
        [Fact]
        public async Task Handle_ShouldReturnCreatedEnterprise_WhenEnterpriseIsValid()
        {
            var dbContext = DCContextMocker.GetMarketplaceContext(nameof(Handle_ShouldReturnCreatedEnterprise_WhenEnterpriseIsValid));
            var validator = new Mock<IValidator<CreateEnterpriseCommand>>();
            validator.Setup(v => v.ValidateAsync(It.IsAny<CreateEnterpriseCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            var handler = new CreateEnterpriseHandler(dbContext, validator.Object);
            var command = new CreateEnterpriseCommand("Enterprise Test", "00000000000000", "Rua Teste", "001", null, "Bairro Teste", "00000-000", "teste@teste.com", "+5500000000000", "Contato Teste");

            var result = await handler.Handle(command, CancellationToken.None);

            result.Should().NotBeNull();
            result.isSuccess.Should().BeTrue();
            result.Data.Name.Should().Be(command.Name);
            result.Data.Document.Should().Be(command.Document);
        }

        [Fact]
        public async Task Handle_ShouldThrowValidationException_WhenEnterpriseInvalidName()
        {
            var dbContext = DCContextMocker.GetMarketplaceContext(nameof(Handle_ShouldReturnCreatedEnterprise_WhenEnterpriseIsValid));
            var validator = new Mock<IValidator<CreateEnterpriseCommand>>();
            validator.Setup(v => v.ValidateAsync(It.IsAny<CreateEnterpriseCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new FluentValidation.Results.ValidationResult());

            var handler = new CreateEnterpriseHandler(dbContext, validator.Object);
            var command = new CreateEnterpriseCommand("", "00000000000000", "Rua Teste", "001", null, "Bairro Teste", "00000-000", "teste@teste.com", "+5500000000000", "Contato Teste");

            Func<Task> act = async () => handler.Handle(command, CancellationToken.None);

            await act.Should().ThrowAsync<ValidationException>()
                .Where(e => e.Errors.Any(f => f.PropertyName == "Name" && f.ErrorMessage == "Name is required"));
        }
    }
}
