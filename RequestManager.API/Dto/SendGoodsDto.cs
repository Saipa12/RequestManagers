using RequestManager.API.Common;
using RequestManager.Database.Models;

namespace RequestManager.API.Dto;

public class SendGoodsDto : DatabaseEntity, IMapFrom<SendGoods>
{
    public DateTime SendDate { get; set; }

    public int Count { get; set; }

    public GoodsDto Requests { get; set; }
}