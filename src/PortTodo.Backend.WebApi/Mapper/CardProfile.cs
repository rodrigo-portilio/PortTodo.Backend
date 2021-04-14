using System.Collections.Generic;
using AutoMapper;
using PortTodo.Backend.Domain.Models;
using PortTodo.Backend.WebApi.Application.DTO;

namespace PortTodo.Backend.WebApi.Mapper
{
    public class CardProfile : Profile
    {
        public CardProfile()
        {
            CreateMap<Card, CardDTO>();
            CreateMap<CardDTO, Card>();
            CreateMap<List<CardDTO>, List<Card>>();
        }
    }
}