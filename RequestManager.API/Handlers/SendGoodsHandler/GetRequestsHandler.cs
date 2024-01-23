using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RequestManager.API.Dto;
using RequestManager.API.Repositories;
using RequestManager.Core.Handlers;

namespace RequestManager.API.Handlers.SendHandler;

public record GetRequestsSendGoods(DateTime? DateFrom, DateTime? DateBefore, int PageNumber = 1, int PageSize = 10);

public record GetResponsesSendGoods(IEnumerable<SendGoodsDto> RequestDto, int Count);

public class GetRequestsSendGoodsHandler : IAsyncHandler<GetRequestsSendGoods, GetResponsesSendGoods>
{
    private readonly SendGoodsRepository _sendGoodsRepository;
    private readonly IMapper _mapper;

    public GetRequestsSendGoodsHandler(SendGoodsRepository sendGoodsRepository, IMapper mapper)
    {
        _sendGoodsRepository = sendGoodsRepository;
        _mapper = mapper;
    }

    public async Task<GetResponsesSendGoods> Handle(GetRequestsSendGoods request)
    {
        var skip = request.PageNumber * request.PageSize;

        var query = await _sendGoodsRepository.GetAsync(x =>
        {
            return x.Skip(skip)
                    .Take(request.PageSize)
                    .Include(s => s.Requests)
                    .Where(s => s.SendDate > request.DateFrom && s.SendDate < request.DateBefore);
        });
        var count = query.Count();
        var requests = query.ToList();
        var response = requests.Select(_mapper.Map<SendGoodsDto>);
        return new GetResponsesSendGoods(response, count);
    }
}