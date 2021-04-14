using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PortTodo.Backend.Core.Data;
using PortTodo.Backend.Domain.Interfaces;
using PortTodo.Backend.Domain.Models;

namespace PortTodo.Backend.Infra.Data.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly TodoContext _context;

        public CardRepository(TodoContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;
        
        public void Add(Card card)
        {
            _context.Cards.Add(card);
        }

        public void Remove(Card card)
        {
            _context.Cards.Remove(card);
        }

        public async Task<IEnumerable<Card>> GetAll()
        {
            return await _context.Cards.ToListAsync();
        }

        public async Task<Card> GetById(Guid id)
        {
            return await _context.Cards.FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}