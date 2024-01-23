using AutoMapper;
using RequestManager.API.Dto;
using RequestManager.API.Repositories;
using RequestManager.Core.Handlers;

using RequestManager.Database.Models;

namespace RequestManager.API.Handlers.DeliverHandler;

public record GetRequestDeliverGoods(int Id);

public record GetResponseDeliverGoods(DeliverGoodsDto Request);

public class GetRequestDeliverGoodsHandler : IAsyncHandler<GetRequestDeliverGoods, GetResponseDeliverGoods>
{
    private readonly DeliverGoodsRepository _deliverGoodsRepository;
    private readonly IMapper _mapper;

    public GetRequestDeliverGoodsHandler(DeliverGoodsRepository deliverGoodsRepository, IMapper mapper)
    {
        _deliverGoodsRepository = deliverGoodsRepository;
        _mapper = mapper;
    }

    public async Task<GetResponseDeliverGoods> Handle(GetRequestDeliverGoods request)
    {
        var requestDto = new DeliverGoodsDto() { Id = request.Id };
        var getedRequestDto = _mapper.Map<DeliverGoods>(requestDto);
        var gettedRequest = _mapper.Map<DeliverGoodsDto>(await _deliverGoodsRepository.GetFirstOrDefaultAsync(x => x.Id == getedRequestDto.Id));
        return new GetResponseDeliverGoods(gettedRequest);
    }
}