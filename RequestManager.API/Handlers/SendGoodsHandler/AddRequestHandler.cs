using AutoMapper;
using RequestManager.API.Dto;
using RequestManager.API.Repositories;
using RequestManager.Core.Handlers;

namespace RequestManager.API.Handlers.RequestHandler;

public record AddRequestSendGoods(SendGoodsDto SendGoodsDto);

public record AddResponseSendGoods(long Id);

public class AddSendGoodsHandler : IAsyncHandler<AddRequestSendGoods, AddResponseSendGoods>
{
    private readonly SendGoodsRepository _sendGoodsRepository;
    private readonly IMapper _mapper;

    public AddSendGoodsHandler(SendGoodsRepository sendGoodsRepository, IMapper mapper)
    {
        _sendGoodsRepository = sendGoodsRepository;
        _mapper = mapper;
    }

    public async Task<AddResponseSendGoods> Handle(AddRequestSendGoods sendGoods)
    {
        var addedRequest = _mapper.Map<SendGoods>(sendGoods.SendGoodsDto);
        await _sendGoodsRepository.CreateAsync(addedRequest);
        return new AddResponseSendGoods(addedRequest.Id);
    }
}