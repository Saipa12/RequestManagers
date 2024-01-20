using RequestManager.API.Common;
using RequestManager.Database.Models;

namespace RequestManager.API.Dto;

public class DeliverGoodsDto : DatabaseEntity, IMapFrom<DeliverGoods>
{
    public DateTime DeliverDate { get; set; }

    public int Count { get; set; }

    public GoodsDto Goods { get; set; }
}