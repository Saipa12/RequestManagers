using AutoMapper;
using RequestManager.API.Dto;
using RequestManager.API.Repositories;
using RequestManager.Core.Handlers;
using RequestManager.Database.Models;

namespace RequestManager.API.Handlers.GoodsHandler;

public record GetRequestGoods(int Id);

public record GetResponseGoods(GoodsDto Request);

public class GetRequestGoodsHandler : IAsyncHandler<GetRequestGoods, GetResponseGoods>
{
    private readonly GoodsRepository _goodsRepository;
    private readonly IMapper _mapper;

    public GetRequestGoodsHandler(GoodsRepository goodsRepository, IMapper mapper)
    {
        _goodsRepository = goodsRepository;
        _mapper = mapper;
    }

    public async Task<GetResponseGoods> Handle(GetRequestGoods request)
    {
        var requestDto = new GoodsDto() { Id = request.Id };
        var getedRequestDto = _mapper.Map<Goods>(requestDto);
        var gettedRequest = _mapper.Map<GoodsDto>(await _goodsRepository.GetFirstOrDefaultAsync(x => x.Id == getedRequestDto.Id));
        return new GetResponseGoods(gettedRequest);
    }
}