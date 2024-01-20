using AutoMapper;
using RequestManager.API.Dto;
using RequestManager.API.Repositories;
using RequestManager.Core.Handlers;
using RequestManager.Database.Models;

namespace RequestManager.API.Handlers.RequestHandler;

public record EditRequestDeliverGoods(DeliverGoodsDto Request);

public record EditResponseDeliverGoods(DeliverGoodsDto Request);

public class EditDeliverRequestHandler : IAsyncHandler<EditRequestDeliverGoods, EditResponseDeliverGoods>
{
    private readonly DeliverGoodsRepository _deliverGoodsRepository;
    private readonly IMapper _mapper;

    public EditDeliverRequestHandler(DeliverGoodsRepository deliverGoodsRepository, IMapper mapper)
    {
        _deliverGoodsRepository = deliverGoodsRepository;
        _mapper = mapper;
    }

    public async Task<EditResponseDeliverGoods> Handle(EditRequestDeliverGoods request)
    {
        var updatedRequest = _mapper.Map<DeliverGoods>(request.Request);
        var updatedRequestDto = _mapper.Map<DeliverGoodsDto>(await _deliverGoodsRepository.UpdateAsync(updatedRequest));
        return new EditResponseDeliverGoods(updatedRequestDto);
    }
}