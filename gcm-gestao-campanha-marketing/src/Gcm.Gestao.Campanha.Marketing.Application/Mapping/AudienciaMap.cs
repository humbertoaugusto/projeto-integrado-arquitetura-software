using AutoMapper;
using Gcm.Gestao.Campanha.Marketing.Application.Models;
using Gcm.Gestao.Campanha.Marketing.Domain.Entities;
using Gcm.Gestao.Campanha.Marketing.Infrastructure.Models;

namespace Gcm.Gestao.Campanha.Marketing.Application.Mapping
{
    /// <summary>
    /// Mapper de estoque
    /// </summary>
    public class AudienciaMap : Profile
    {
        /// <summary>
        /// Mapeamento de dados
        /// </summary>
        public AudienciaMap()
        {
            CreateMap<Audiencia, AudienciaModel>()
                .ForMember(dest => dest.Codigo, m => m.MapFrom(src => src.Codigo))
                .ForMember(dest => dest.Clientes, m => m.MapFrom(src => src.Clientes))                
                .ForMember(dest => dest.FiltroAudiencia, m => m.MapFrom(src => src.FiltroAudiencia))
                .ReverseMap();            
        }
    }
}
