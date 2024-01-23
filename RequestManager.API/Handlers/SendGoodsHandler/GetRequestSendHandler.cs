using AutoMapper;
using RequestManager.API.Dto;
using RequestManager.API.Repositories;
using RequestManager.Core.Handlers;
using RequestManager.Database.Models;

namespace RequestManager.API.Handlers.SendHandler;

public record GetRequestSendGoods(int Id);

public record GetResponseSendGoods(SendGoodsDto Request);

public class GetRequestSendHandler : IAsyncHandler<GetRequestSendGoods, GetResponseSendGoods>
{
    private readonly SendGoodsRepository _sendGoodsRepository;
    private readonly IMapper _mapper;

    public GetRequestSendHandler(SendGoodsRepository sendGoodsRepository, IMapper mapper)
    {
        _sendGoodsRepository = sendGoodsRepository;
        _mapper = mapper;
    }

    public async Task<GetResponseSendGoods> Handle(GetRequestSendGoods request)
    {
        var requestDto = new SendGoodsDto() { Id = request.Id };
        var getedRequestDto = _mapper.Map<SendGoods>(requestDto);
        var gettedRequest = _mapper.Map<SendGoodsDto>(await _sendGoodsRepository.GetFirstOrDefaultAsync(x => x.Id == getedRequestDto.Id));
        return new GetResponseSendGoods(gettedRequest);
    }
}