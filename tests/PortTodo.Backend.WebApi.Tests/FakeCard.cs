using System;
using System.Collections.Generic;
using PortTodo.Backend.WebApi.Application.Commands;
using PortTodo.Backend.WebApi.Application.DTO;
using PortTodo.Backend.WebApi.Models;

namespace PortTodo.Backend.WebApi.Tests
{
    public static class FakeCard
    {
        public static List<Card> FakeListValidCard()
        {
            var cards = new List<Card>();
            cards.Add(new (Guid.Parse("A2CD31BB-1534-498A-822D-112E6540A25A"), "Card Test 1"));
            cards.Add(new (Guid.Parse("958CB1B7-C47B-4AA6-8B2F-B702C70F1055"), "Card Test 2"));
            cards.Add(new (Guid.Parse("C0BC951F-6F33-47AC-ADAC-8F6A77D9B9A3"), "Card Test 3"));

            return cards;
        }
        
        public static Card FakeValidCard()
        {
            return new(Guid.Parse("A2CD31BB-1534-498A-822D-112E6540A25A"), "Card Test 1");
        }
        
        public static Card FakeInValidCard()
        {
            return new(Guid.Parse("A2CD31BB-1534-498A-822D-112E6540A25A"), "Ca");
        }
    }
    
    public static class FakeCardDTO
    {
        public static List<CardDTO> FakeListValidCardDTO()
        {
            var cards = new List<CardDTO>();
            cards.Add(new CardDTO{Id = Guid.Parse("A2CD31BB-1534-498A-822D-112E6540A25A"), Name = "Card Test 1", Status = 0});
            cards.Add(new CardDTO{Id = Guid.Parse("958CB1B7-C47B-4AA6-8B2F-B702C70F1055"), Name = "Card Test 2", Status = 1});
            cards.Add(new CardDTO{Id = Guid.Parse("C0BC951F-6F33-47AC-ADAC-8F6A77D9B9A3"), Name = "Card Test 3", Status = 2});

            return cards;
        }
        
        public static CardDTO FakeValidCardDTO()
        {
            return new CardDTO
                {Id = Guid.Parse("A2CD31BB-1534-498A-822D-112E6540A25A"), Name = "Card Test 1", Status = 0};
        }
        
        public static CardDTO FakeInValidCardDTO()
        {
            return new CardDTO {Id = Guid.Parse("A2CD31BB-1534-498A-822D-112E6540A25A"), Name = "Ca", Status = 0};
        }
    }
    
    public static class FakeCreateCardCommand
    {
        public static CreateCardCommand FakeValidCard()
        {
            return new(Guid.Parse("A2CD31BB-1534-498A-822D-112E6540A25A"), "Card Test 1");
        }
        
        public static CreateCardCommand FakeInValidCard()
        {
            return new(Guid.Parse("A2CD31BB-1534-498A-822D-112E6540A25A"), "Ca");
        }
    }
}