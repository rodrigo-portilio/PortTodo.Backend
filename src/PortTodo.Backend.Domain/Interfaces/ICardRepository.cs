using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PortTodo.Backend.Core.Data;
using PortTodo.Backend.Domain.Models;

namespace PortTodo.Backend.Domain.Interfaces
{
    public interface ICardRepository : IRepository<Card>
    {
        void Add(Card card);
        void Remove(Card card);
        Task<IEnumerable<Card>> GetAll();
        Task<Card> GetById(Guid id);
    }
}