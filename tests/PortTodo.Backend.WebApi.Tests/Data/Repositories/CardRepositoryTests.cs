using System.Threading.Tasks;
using Xunit;
using Xunit.Priority;
using Microsoft.EntityFrameworkCore;
using Moq;
using PortTodo.Backend.WebApi.Core.Mediator;
using PortTodo.Backend.WebApi.Data;
using PortTodo.Backend.WebApi.Data.Repositories;

namespace PortTodo.Backend.WebApi.Tests.Data.Repositories
{
    [Trait("Category", "CardRepository Tests")]
    [TestCaseOrderer(PriorityOrderer.Name, PriorityOrderer.Assembly)]
    public class CardRepositoryTests
    {
        private readonly CardRepository _cardRepository;
        public CardRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<TodoContext>()
                .UseInMemoryDatabase(databaseName: "Todo-Test")
                .Options;
            
            var mediatorHandlerMock = new Mock<IMediatorHandler>();
            var context = new TodoContext(options, mediatorHandlerMock.Object);

            _cardRepository = new CardRepository(context);
        }
        
        
        [Fact(DisplayName = "Create new card with success"), Priority(0)]
        public async Task CardRepository_Add_ShouldWithSuccess()
        {
            
            _cardRepository.Add(FakeCard.FakeValidCard());
            
            Assert.True(await _cardRepository.UnitOfWork.Commit());
        }

        [Fact(DisplayName = "GetById card"), Priority(1)]
        public async Task CardRepository_GetById_ShouldAreEqual()
        {
            var card = FakeCard.FakeValidCard();
            
            
            var cardVerify = await _cardRepository.GetById(card.Id);

            Assert.Equal(card.Id.ToString(), cardVerify.Id.ToString());
        }
        
        [Fact(DisplayName = "GetAll Card"), Priority(2)]
        public async Task CardRepository_GetAll_ShouldAreEqual()
        {
            var cardsGetAll = await _cardRepository.GetAll();

            Assert.Single(cardsGetAll);
        }
        
        [Fact(DisplayName = "RemoveAll Card"), Priority(3)]
        public async Task CardRepository_RemoveAll_ShouldAreEqual()
        {
            _cardRepository.Remove(FakeCard.FakeValidCard());
            
            Assert.True(await _cardRepository.UnitOfWork.Commit());

            Assert.Empty(await _cardRepository.GetAll());
            
        }
    }
}