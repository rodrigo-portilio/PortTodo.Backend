using System;
using PortTodo.Backend.WebApi.Models;
using Xunit;

namespace PortTodo.Backend.WebApi.Tests.Models
{
    [Trait("Category","Card Tests")]
    public class CardTests
    {
        [Fact(DisplayName = "Create new Card")]
        public void Card_NewCard_ShouldBeStatusToDo()
        {
            Card card = new(Guid.NewGuid(), "Card Test");
            
            Assert.Equal(CardStatus.ToDo, card.Status);
        }
        
        [Fact(DisplayName = "Create Card Valid")]
        public void Card_NewCard_ShouldValid()
        {
            Card card = new(Guid.NewGuid(), "Card Test");
            
            Assert.True(card.IsValid());
        }
        
        [Fact(DisplayName = "Create Card InValid")]
        public void Card_NewCard_ShouldInValid()
        {
            var ex = Assert.Throws<Exception>( () => new Card(Guid.NewGuid(), "ca") ) ;
            Assert.Equal("Name can't be empty or less 3 character", ex.Message);
        }
        
        [Fact(DisplayName = "Change Status Card to InProgress")]
        public void Card_ChangeStatus_ShouldBeStatusInProgress()
        {
            Card card = new(Guid.NewGuid(), "New Card Test");
            card.ChangeStatus(CardStatus.InProgress);
            
            Assert.Equal(CardStatus.InProgress, card.Status);
        }
    }
}