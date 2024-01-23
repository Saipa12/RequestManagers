using AutoMapper;
using RequestManager.API.Dto;
using RequestManager.API.Repositories;
using RequestManager.Core.Handlers;
using RequestManager.Database.Models;

namespace RequestManager.API.Handlers.GoodsHandler;

public record EditRequestGoods(GoodsDto Request);

public record EditResponseGoods(GoodsDto Request);

public class EditRequestGoodsHandler : IAsyncHandler<EditRequestGoods, EditResponseGoods>
{
    private readonly GoodsRepository _goodsRepository;
    private readonly IMapper _mapper;

    public EditRequestGoodsHandler(GoodsRepository goodsRepository, IMapper mapper)
    {
        _goodsRepository = goodsRepository;
        _mapper = mapper;
    }

    public async Task<EditResponseGoods> Handle(EditRequestGoods request)
    {
        var updatedRequest = _mapper.Map<Goods>(request.Request);
        var updatedRequestDto = _mapper.Map<GoodsDto>(await _goodsRepository.UpdateAsync(updatedRequest));
        return new EditResponseGoods(updatedRequestDto);
    }
}