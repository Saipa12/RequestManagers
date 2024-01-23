using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RequestManager.API.Dto;
using RequestManager.API.Repositories;
using RequestManager.Core.Handlers;

namespace RequestManager.API.Handlers.GoodsHandler;

public record GetRequestsGoods(int PageNumber = 1, int PageSize = 10);

public record GetResponsesGoods(IEnumerable<GoodsDto> RequestDto, int Count);

public class GetRequestsGoodsHandler : IAsyncHandler<GetRequestsGoods, GetResponsesGoods>
{
    private readonly GoodsRepository _goodsRepository;
    private readonly IMapper _mapper;

    public GetRequestsGoodsHandler(GoodsRepository goodsRepository, IMapper mapper)
    {
        _goodsRepository = goodsRepository;
        _mapper = mapper;
    }

    public async Task<GetResponsesGoods> Handle(GetRequestsGoods request)
    {
        var skip = request.PageNumber * request.PageSize;
        var count = await _goodsRepository.GetCount();
        var query = await _goodsRepository.GetAsync(x =>
        {
            x = x.Skip(skip).Take(request.PageSize).Include(x => x.SendGoods).Include(x => x.DeliverGoods).AsNoTracking();
            return x;
        });

        var requests = query.ToList();
        var response = requests.Select(_mapper.Map<GoodsDto>);
        return new GetResponsesGoods(response, count);
    }
}