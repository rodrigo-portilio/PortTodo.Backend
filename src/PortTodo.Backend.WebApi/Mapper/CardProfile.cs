using System.Collections.Generic;
using AutoMapper;
using PortTodo.Backend.WebApi.Application.DTO;
using PortTodo.Backend.WebApi.Models;

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