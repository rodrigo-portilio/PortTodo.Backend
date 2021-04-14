using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using PortTodo.Backend.Domain.Interfaces;
using PortTodo.Backend.WebApi.Application.DTO;
using PortTodo.Backend.WebApi.Application.Services;

namespace PortTodo.Backend.WebApi.Application.Queries
{
    public interface ICardQueries
    {
        Task<List<CardDTO>> GetAll();
    }

    public class CardQueries : ICardQueries
    {
        private readonly ICacheService _cacheService;
        private readonly ICardRepository _cardRepository;
        private readonly IMapper _mapper;

        public CardQueries(ICacheService cacheService, ICardRepository cardRepository, IMapper mapper)
        {
            _cacheService = cacheService;
            _cardRepository = cardRepository;
            _mapper = mapper;
        }
        
        public async Task<List<CardDTO>> GetAll()
        {
            var cards = await _cacheService.GetCache<List<CardDTO>>("cards");
            if (cards == null)
            {
                cards = _mapper.Map<List<CardDTO>>(await _cardRepository.GetAll());
                await _cacheService.SetCache("cards", cards);
            }

            return cards;
        }
    }
}