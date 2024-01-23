using AutoMapper;
using RequestManager.API.Dto;
using RequestManager.API.Repositories;
using RequestManager.Core.Handlers;
using RequestManager.Database.Models;

namespace RequestManager.API.Handlers.DeliverHandler;

public record AddRequestDelivGoods(DeliverGoodsDto DelivGoodsDto);

public record AddResponseDelivGoods(long Id);

public class AddDelivGoodsHandler : IAsyncHandler<AddRequestDelivGoods, AddResponseDelivGoods>
{
    private readonly DeliverGoodsRepository _deliverGoodsRepository;
    private readonly IMapper _mapper;

    public AddDelivGoodsHandler(DeliverGoodsRepository deliverGoodsRepository, IMapper mapper)
    {
        _deliverGoodsRepository = deliverGoodsRepository;
        _mapper = mapper;
    }

    public async Task<AddResponseDelivGoods> Handle(AddRequestDelivGoods deliverGoods)
    {
        var addedRequest = _mapper.Map<DeliverGoods>(deliverGoods.DelivGoodsDto);
        await _deliverGoodsRepository.CreateAsync(addedRequest);
        return new AddResponseDelivGoods(addedRequest.Id);
    }
}