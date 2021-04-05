using System.Threading.Tasks;
using FluentValidation.Results;
using Moq;
using Moq.AutoMock;
using PortTodo.Backend.WebApi.Application.Queries;
using PortTodo.Backend.WebApi.Controllers;
using PortTodo.Backend.WebApi.Core.Mediator;
using PortTodo.Backend.WebApi.Core.Messages;
using Xunit;

namespace PortTodo.Backend.WebApi.Tests.Controllers
{
    [Trait("Category","CardsController Tests")]
    public class CardsControllerTests
    {
        [Fact(DisplayName = "Get cards")]
        async Task CardsController_Get_ShouldEqual()
        {
            var mocker = new AutoMocker();
            var cardsController = mocker.CreateInstance<CardsController>();

            await cardsController.Get();
            
            mocker.GetMock<ICardQueries>().Verify(x => x.GetAll(), Times.Once);
        }

        [Fact(DisplayName = "Post create new card")]
        async Task CardsController_PostCreateNewCard_ShouldEqual()
        {
            var createCardCommand = FakeCreateCardCommand.FakeValidCard();
            
            var mocker = new AutoMocker();
            var cardsController = mocker.CreateInstance<CardsController>();
            mocker.GetMock<IMediatorHandler>()
                .Setup(x => x.SendCommand(It.IsAny<Command>()))
                .Returns(Task.FromResult<ValidationResult>(new ValidationResult()));
            
            await cardsController.Post(createCardCommand);
            
            mocker.GetMock<IMediatorHandler>().Verify(x => x.SendCommand(It.IsAny<Command>()), Times.Once);
        }
    }
}