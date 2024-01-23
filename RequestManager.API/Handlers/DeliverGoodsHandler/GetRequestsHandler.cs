using AutoMapper;
using RequestManager.API.Dto;
using RequestManager.API.Repositories;
using RequestManager.Core.Handlers;

namespace RequestManager.API.Handlers.DeliverHandler;

public record GetRequestsDeliverGoods(int PageNumber = 1, int PageSize = 10);

public record GetResponsesDeliverGoods(IEnumerable<DeliverGoodsDto> RequestDto, int Count);

public class GetRequestsDeliverGoodsHandler : IAsyncHandler<GetRequestsDeliverGoods, GetResponsesDeliverGoods>
{
    private readonly DeliverGoodsRepository _deliverGoodsRepository;
    private readonly IMapper _mapper;

    public GetRequestsDeliverGoodsHandler(DeliverGoodsRepository deliverGoodsRepository, IMapper mapper)
    {
        _deliverGoodsRepository = deliverGoodsRepository;
        _mapper = mapper;
    }

    public async Task<GetResponsesDeliverGoods> Handle(GetRequestsDeliverGoods request)
    {
        var skip = request.PageNumber * request.PageSize;
        var count = await _deliverGoodsRepository.GetCount();
        var query = await _deliverGoodsRepository.GetAsync(x =>
        {
            x = x.Skip(skip).Take(request.PageSize);
            return x;
        });

        var requests = query.ToList();
        var response = requests.Select(_mapper.Map<DeliverGoodsDto>);
        return new GetResponsesDeliverGoods(response, count);
    }
}