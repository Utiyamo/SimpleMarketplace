using DC.SimpleMarketplace.Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DC.SimpleMarketplace.Domain.Commands
{
    public class DeleteEnterpriseCommand : IRequest<BaseResponse>
    {
        public long Id { get; set; }

        public DeleteEnterpriseCommand() { }

        public DeleteEnterpriseCommand(long id)
        {
            Id = id;
        }
    }
}
