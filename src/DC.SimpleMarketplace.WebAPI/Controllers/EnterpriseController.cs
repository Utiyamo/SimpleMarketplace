using DC.SimpleMarketplace.Domain.Commands;
using DC.SimpleMarketplace.Domain.Entities;
using DC.SimpleMarketplace.Domain.Queries;
using MediatR;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace DC.SimpleMarketplace.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EnterpriseController : ControllerBase
    {
        private readonly IMediator _mediator;
        
        public EnterpriseController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? page, [FromQuery] int? amountPerPage)
        {
            try
            {
                var minpage = 1;
                var minAmoutPerPage = 10;
                
                if(page.HasValue)
                    minpage = page.Value;
                
                if(amountPerPage.HasValue)
                    minAmoutPerPage = amountPerPage.Value;

                var queryCommand = new GetAllEnterprisesQuery(minpage, minAmoutPerPage);

                var result = await _mediator.Send(queryCommand);

                if (result.isSuccess)
                    return Ok(result);
                else
                    return StatusCode(result.Status, result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, Newtonsoft.Json.JsonConvert.SerializeObject(ex));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            try
            {
                var queryCommand = new GetEnterpriseQuery(id);

                var result = await _mediator.Send(queryCommand);

                if (result.isSuccess)
                    return Ok(result);
                else
                    return StatusCode(result.Status, result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, Newtonsoft.Json.JsonConvert.SerializeObject(ex));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateEnterprise(CreateEnterpriseCommand command)
        {
            try
            {
                var createResult = await _mediator.Send(command);

                if (createResult.isSuccess)
                    return CreatedAtAction(nameof(Get), new { id = createResult.Data.ID }, createResult);
                else
                    return StatusCode(createResult.Status, createResult);
            }
            catch(Exception ex)
            {
                return StatusCode(500, Newtonsoft.Json.JsonConvert.SerializeObject(ex));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> AlterEnterprise(long id, AlterEnterpriseCommand command)
        {
            try
            {
                command.ID = id;
                var updateResult = await _mediator.Send(command);

                if (updateResult.isSuccess)
                    return await Get(updateResult.Data.ID);
                else
                    return StatusCode(updateResult.Status, updateResult);
            }
            catch(Exception ex)
            {
                return StatusCode(500, Newtonsoft.Json.JsonConvert.SerializeObject(ex));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnterprise(long id)
        {
            try
            {
                var deleteResult = await _mediator.Send(new DeleteEnterpriseCommand(id));

                if (deleteResult.isSuccess)
                    return NoContent();
                else
                    return StatusCode(deleteResult.Status, deleteResult);
            }
            catch(Exception ex)
            {
                return StatusCode(500, Newtonsoft.Json.JsonConvert.SerializeObject(ex));
            }
        }
    }
}
