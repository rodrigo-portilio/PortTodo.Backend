using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PortTodo.Backend.WebApi.Models.Interfaces
{
    public interface ICardRepository : IRepository<Card>
    {
        void Add(Card card);
        void Remove(Card card);
        Task<IEnumerable<Card>> GetAll();
        Task<Card> GetById(Guid id);
    }
}