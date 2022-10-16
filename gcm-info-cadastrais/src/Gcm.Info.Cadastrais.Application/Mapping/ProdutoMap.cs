using AutoMapper;
using Gcm.Info.Cadastrais.Application.Models;
using Gcm.Info.Cadastrais.Domain.Entities;

namespace Gcm.Info.Cadastrais.Application.Mapping
{
    /// <summary>
    /// Mapper de produto
    /// </summary>
    public class ProdutoMap : Profile
    {
        /// <summary>
        /// Mapeamento de dados
        /// </summary>
        public ProdutoMap()
        {
            CreateMap<Produto, ProdutoModel>()
                .ForMember(dest => dest.Quantidade, m => m.MapFrom(src => src.Quantidade))
                .ForMember(dest => dest.Valor, m => m.MapFrom(src => src.Valor))              
                .ForMember(dest => dest.Codigo, m => m.MapFrom(src => src.Codigo))
                .ForMember(dest => dest.Nome, m => m.MapFrom(src => src.Nome))
                .ForMember(dest => dest.CategoriaProdutoId, m => m.MapFrom(src => src.CategoriaProdutoId))
                .ReverseMap();
        }
    }
}
