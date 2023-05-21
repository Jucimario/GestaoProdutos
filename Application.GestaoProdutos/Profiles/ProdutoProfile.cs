using Domain.GestaoProdutos.Dtos.ProdutoDtos;
using Domain.GestaoProdutos.Entities;
using AutoMapper;

namespace Application.GestaoProdutos.Profiles;

public class ProdutoProfile : Profile
{
    public ProdutoProfile()
    {
        CreateMap<ProdutoDto, Produto>();
        CreateMap<Produto, ProdutoDto>();
    }
}
