using DC.SimpleMarketplace.Application.Handlers;
using DC.SimpleMarketplace.Domain.Commands;
using DC.SimpleMarketplace.Domain.Entities;
using DC.SimpleMarketplace.Domain.Models;
using DC.SimpleMarketplace.Domain.Queries;
using DC.SimpleMarketplace.Domain.Validators;
using DC.SimpleMarketplace.Infrastructure.ORM;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DC.SimpleMarketplace.WebAPI.DI
{
    public static class Bootstrap
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration)
        {
            services.AddHttpClient();
            services.AddMediatR(typeof(Program).Assembly);

            services.AddDbContext<DCContext>(options =>
               options.UseSqlServer(configuration.GetConnectionString("Default"))
           );

            AddValidator(services);
            AddRepositories(services);
            AddServices(services);
        }

        public static void AddValidator(IServiceCollection services)
        {
            services.AddValidatorsFromAssemblyContaining<Program>();

            //Enterprise
            services.AddTransient<IValidator<CreateEnterpriseCommand>, CreateEnterpriseValidator>();
            services.AddTransient<IValidator<AlterEnterpriseCommand>, UpdateEnterpriseValidator>();
            services.AddTransient<IValidator<DeleteEnterpriseCommand>, DeleteEnterpriseValidator>();
        }

        public static void AddRepositories(IServiceCollection services)
        {

        }

        public static void AddServices(IServiceCollection services)
        {
            //Queries
            services.AddTransient<IRequestHandler<GetEnterpriseQuery, BaseResponse<Enterprise>>, GetEnterpriseHandler>();
            services.AddTransient<IRequestHandler<GetAllEnterprisesQuery, BaseResponse<PaginationResponse<Enterprise>>>, GetAllEnterprisesHandler>();

            //Commands
            services.AddTransient<IRequestHandler<CreateEnterpriseCommand, BaseResponse<Enterprise>>, CreateEnterpriseHandler>();
            services.AddTransient<IRequestHandler<AlterEnterpriseCommand, BaseResponse<Enterprise>>, UpdateEnterpriseHandler>();
            services.AddTransient<IRequestHandler<DeleteEnterpriseCommand, BaseResponse>, DeleteEnterpriseHandler>();
        }
    }
}
