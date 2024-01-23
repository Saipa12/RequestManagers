using AutoMapper;
using RequestManager.API.Dto;
using RequestManager.API.Repositories;
using RequestManager.Core.Handlers;
using RequestManager.Database.Models;

namespace RequestManager.API.GoodsHandler;

public record AddRequestGoods(GoodsDto GoodsDto);

public record AddResponseGoods(long Id);

public class AddGoodsHandler : IAsyncHandler<AddRequestGoods, AddResponseGoods>
{
    private readonly GoodsRepository _goodsRepository;
    private readonly IMapper _mapper;

    public AddGoodsHandler(GoodsRepository goodsRepository, IMapper mapper)
    {
        _goodsRepository = goodsRepository;
        _mapper = mapper;
    }

    public async Task<AddResponseGoods> Handle(AddRequestGoods goods)
    {
        var addedRequest = _mapper.Map<Goods>(goods.GoodsDto);
        await _goodsRepository.CreateAsync(addedRequest);
        return new AddResponseGoods(addedRequest.Id);
    }
}