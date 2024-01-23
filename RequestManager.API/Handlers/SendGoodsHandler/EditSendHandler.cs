using AutoMapper;
using RequestManager.API.Dto;
using RequestManager.API.Repositories;
using RequestManager.Core.Handlers;
using RequestManager.Database.Models;

namespace RequestManager.API.Handlers.SendHandler;

public record EditRequestSendGoods(SendGoodsDto Request);

public record EditResponseSendGoods(SendGoodsDto Request);

public class EditSendHandler : IAsyncHandler<EditRequestSendGoods, EditResponseSendGoods>
{
    private readonly SendGoodsRepository _sendGoodsRepository;
    private readonly IMapper _mapper;

    public EditSendHandler(SendGoodsRepository sendGoodsRepository, IMapper mapper)
    {
        _sendGoodsRepository = sendGoodsRepository;
        _mapper = mapper;
    }

    public async Task<EditResponseSendGoods> Handle(EditRequestSendGoods request)
    {
        var updatedRequest = _mapper.Map<SendGoods>(request.Request);
        var updatedRequestDto = _mapper.Map<SendGoodsDto>(await _sendGoodsRepository.UpdateAsync(updatedRequest));
        return new EditResponseSendGoods(updatedRequestDto);
    }
}