using AutoMapper;
using RequestManager.API.Dto;
using RequestManager.API.Repositories;
using RequestManager.Core.Handlers;

using RequestManager.Database.Models;

namespace RequestManager.API.Handlers.RequestHandler;

public record DeleteRequestSendGoods(SendGoodsDto Request);

public record DeleteResponseSendGoods();

public class DeleteSendRequestHandler : IAsyncHandler<DeleteRequestSendGoods, DeleteResponseSendGoods>
{
    private readonly SendGoodsRepository _sendGoodsRepository;
    private readonly IMapper _mapper;

    public DeleteSendRequestHandler(SendGoodsRepository sendGoodsRepository, IMapper mapper)
    {
        _sendGoodsRepository = sendGoodsRepository;
        _mapper = mapper;
    }

    public async Task<DeleteResponseSendGoods> Handle(DeleteRequestSendGoods request)
    {
        var deletedRequest = _mapper.Map<SendGoods>(request.Request);
        await _sendGoodsRepository.DeleteAsync(deletedRequest);
        return new DeleteResponseSendGoods();
    }
}