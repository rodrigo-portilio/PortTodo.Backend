using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.AutoMock;
using PortTodo.Backend.Domain.Interfaces;
using PortTodo.Backend.Domain.Models;
using PortTodo.Backend.WebApi.Application.Commands;
using Xunit;

namespace PortTodo.Backend.WebApi.Tests.Application.Commands
{
    [Trait("Category", "CardCommandHandler Tests ")]
    public class CardCommandHandlerTests
    {
        [Fact(DisplayName = "Create new Card with success")]
        public async Task CreateCardCommand_NewCard_ShouldCreateWithSuccess()
        {
            var createCardCommand = FakeCreateCardCommand.FakeValidCard();

            var mocker = new AutoMocker();
            var cardCommandHandler = mocker.CreateInstance<CardCommandHandler>();
            mocker.GetMock<ICardRepository>()
                .Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));
            var result = await cardCommandHandler.Handle(createCardCommand, CancellationToken.None);
            
            Assert.True(result.IsValid);
            mocker.GetMock<ICardRepository>().Verify(r => 
                r.Add(It.IsAny<Card>()),
                Times.Once
                );
            mocker.GetMock<ICardRepository>().Verify(r => 
                    r.UnitOfWork.Commit(),
                Times.Once
            );
        }
        
        [Fact(DisplayName = "Create new Card with fail")]
        public async Task CreateCardCommand_NewCard_ShouldCreateWithFail()
        {
            var createCardCommand = FakeCreateCardCommand.FakeInValidCard();

            var mocker = new AutoMocker();
            var cardCommandHandler = mocker.CreateInstance<CardCommandHandler>();
            mocker.GetMock<ICardRepository>()
                .Setup(r => r.UnitOfWork.Commit()).Returns(Task.FromResult(true));
            var result = await cardCommandHandler.Handle(createCardCommand, CancellationToken.None);
            
            Assert.False(result.IsValid);
            mocker.GetMock<ICardRepository>().Verify(r => 
                    r.Add(It.IsAny<Card>()),
                Times.Never
            );
            mocker.GetMock<ICardRepository>().Verify(r => 
                    r.UnitOfWork.Commit(),
                Times.Never
            );
        }
    }
}