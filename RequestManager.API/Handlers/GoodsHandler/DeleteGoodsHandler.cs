using AutoMapper;
using RequestManager.API.Dto;
using RequestManager.API.Repositories;
using RequestManager.Core.Handlers;
using RequestManager.Database.Models;

namespace RequestManager.API.Handlers.GoodsHandler;

public record DeleteRequestGoods(GoodsDto Request);

public record DeleteResponseGoods();

public class DeleteGoodsHandler : IAsyncHandler<DeleteRequestGoods, DeleteResponseGoods>
{
    private readonly GoodsRepository _goodsRepository;
    private readonly IMapper _mapper;

    public DeleteGoodsHandler(GoodsRepository goodsRepository, IMapper mapper)
    {
        _goodsRepository = goodsRepository;
        _mapper = mapper;
    }

    public async Task<DeleteResponseGoods> Handle(DeleteRequestGoods request)
    {
        var deletedRequest = _mapper.Map<Goods>(request.Request);
        await _goodsRepository.DeleteAsync(deletedRequest);
        return new DeleteResponseGoods();
    }
}