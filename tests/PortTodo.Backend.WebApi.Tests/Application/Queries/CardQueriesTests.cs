using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using Moq.AutoMock;
using PortTodo.Backend.WebApi.Application.DTO;
using PortTodo.Backend.WebApi.Application.Queries;
using PortTodo.Backend.WebApi.Application.Services;
using PortTodo.Backend.WebApi.Models;
using PortTodo.Backend.WebApi.Models.Interfaces;
using Xunit;

namespace PortTodo.Backend.WebApi.Tests.Application.Queries
{
    [Trait("Category","CardQueries Tests")]
    public class CardQueriesTests
    {
        [Fact(DisplayName = "Get all with cache cards")]
        public async Task CardQueries_GetAllWithCache_ShouldEqual()
        {
            var cardsDTO = FakeCardDTO.FakeListValidCardDTO();
            var cards = FakeCard.FakeListValidCard();
            var mocker = new AutoMocker();
            var cardQueries = mocker.CreateInstance<CardQueries>();
            mocker.GetMock<ICacheService>()
                .Setup(x => x.GetCache<List<CardDTO>>(It.IsAny<string>()))
                .Returns(Task.FromResult<List<CardDTO>>(cardsDTO));
            mocker.GetMock<ICardRepository>()
                .Setup(x => x.GetAll()).Returns(Task.FromResult<IEnumerable<Card>>(cards));

            var result = await cardQueries.GetAll();
            
            Assert.Equal(3, result.Count);
            mocker.GetMock<ICacheService>()
                .Verify(x => x.GetCache<List<CardDTO>>(It.IsAny<string>()), Times.Once);
            mocker.GetMock<ICacheService>()
                .Verify(x => x.SetCache(It.IsAny<string>(), It.IsAny<CardDTO>()), Times.Never);
            mocker.GetMock<ICardRepository>()
                .Verify(x => x.GetAll(), Times.Never);
        }
        
        [Fact(DisplayName = "Get all without cache cards")]
        public async Task CardQueries_GetAllWithoutCache_ShouldEqual()
        {
            // Arrange
            var cards = FakeCard.FakeListValidCard();
            var cardsDto = FakeCardDTO.FakeListValidCardDTO();
            var mocker = new AutoMocker();
            var cardQueries = mocker.CreateInstance<CardQueries>();
            mocker.GetMock<IMapper>()
                .Setup(x => x.Map<List<CardDTO>>(It.IsAny<List<Card>>())).Returns(cardsDto);
            mocker.GetMock<ICacheService>()
                .Setup(x => x.SetCache(It.IsAny<string>(), It.IsAny<List<CardDTO>>()));
            mocker.GetMock<ICardRepository>()
                .Setup(x => x.GetAll()).Returns(Task.FromResult<IEnumerable<Card>>(cards));

            // Act
            var result = await cardQueries.GetAll();
            
            // Assert
            Assert.Equal(3, result.Count);
            mocker.GetMock<ICacheService>()
                .Verify(x => x.GetCache<List<CardDTO>>(It.IsAny<string>()), Times.Once);
            mocker.GetMock<ICacheService>()
                .Verify(x => x.SetCache(It.IsAny<string>(), It.IsAny<List<CardDTO>>()), Times.Once);
            mocker.GetMock<ICardRepository>()
                .Verify(x => x.GetAll(), Times.Once);
        }
    }
}