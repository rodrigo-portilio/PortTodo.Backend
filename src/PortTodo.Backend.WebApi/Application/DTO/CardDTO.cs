using System;

namespace PortTodo.Backend.WebApi.Application.DTO
{
    public class CardDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Status { get; set; }
    }
}