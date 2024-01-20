using RequestManager.API.Common;
using RequestManager.Database.Models;

namespace RequestManager.API.Dto;

public class GoodsDto : DatabaseEntity, IMapFrom<Goods>
{
    public string Surname { get; set; }

    public List<SendGoods> SendGoods { get; set; }
    public List<DeliverGoods> DeliverGoods { get; set; }
}