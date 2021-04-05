using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PortTodo.Backend.WebApi.Application.Commands;
using PortTodo.Backend.WebApi.Application.Queries;
using PortTodo.Backend.WebApi.Core.Mediator;

namespace PortTodo.Backend.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CardsController : MainController
    {
        private readonly IMediatorHandler _mediatorHandle;
        private readonly ICardQueries _cardQueries;

        public CardsController(IMediatorHandler mediatorHandle, ICardQueries cardQueries)
        {
            _mediatorHandle = mediatorHandle;
            _cardQueries = cardQueries;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var cards = await _cardQueries.GetAll();

            return CustomResponse(cards);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCardCommand createCardCommand)
        {
            var result = await _mediatorHandle.SendCommand(createCardCommand);

            return CustomResponse(result);
        }
    }
}