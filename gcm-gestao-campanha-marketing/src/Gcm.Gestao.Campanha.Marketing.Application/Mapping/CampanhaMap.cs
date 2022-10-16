using AutoMapper;
using Gcm.Gestao.Campanha.Marketing.Application.Models;
using Gcm.Gestao.Campanha.Marketing.Domain.Entities;
using Gcm.Gestao.Campanha.Marketing.Domain.ValueObjects;
using Gcm.Gestao.Campanha.Marketing.Infrastructure.Models;

namespace Gcm.Gestao.Campanha.Marketing.Application.Mapping
{
    /// <summary>
    /// Mapper de campanha
    /// </summary>
    public class CampanhaMap : Profile
    {
        /// <summary>
        /// Mapeamento de dados
        /// </summary>
        public CampanhaMap()
        {
            CreateMap<CampanhaMkt, CampanhaModel>()
                .ForMember(dest => dest.Codigo, m => m.MapFrom(src => src.Codigo))
                .ForMember(dest => dest.Ativa, m => m.MapFrom(src => src.Ativa))  
                .ForMember(dest => dest.AudienciaId, m => m.MapFrom(src => src.AudienciaId))
                .ForMember(dest => dest.Descricao, m => m.MapFrom(src => src.Descricao))                
                .ReverseMap();
        }
    }
}
