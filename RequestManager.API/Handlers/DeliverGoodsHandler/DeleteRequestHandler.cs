using AutoMapper;
using RequestManager.API.Dto;
using RequestManager.API.Repositories;
using RequestManager.Core.Handlers;
using RequestManager.Database.Models;

namespace RequestManager.API.Handlers.DeliverHandler;

public record DeleteRequestDeliverGoods(DeliverGoodsDto Request);

public record DeleteResponseDeliverGoods();

public class DeleteDelivRequestHandler : IAsyncHandler<DeleteRequestDeliverGoods, DeleteResponseDeliverGoods>
{
    private readonly DeliverGoodsRepository _deliverGoodsRepository;
    private readonly IMapper _mapper;

    public DeleteDelivRequestHandler(DeliverGoodsRepository deliverGoodsRepository, IMapper mapper)
    {
        _deliverGoodsRepository = deliverGoodsRepository;
        _mapper = mapper;
    }

    public async Task<DeleteResponseDeliverGoods> Handle(DeleteRequestDeliverGoods request)
    {
        var deletedRequest = _mapper.Map<DeliverGoods>(request.Request);
        await _deliverGoodsRepository.DeleteAsync(deletedRequest);
        return new DeleteResponseDeliverGoods();
    }
}