using AutoMapper;
using RequestManager.Core.Repositories;
using RequestManager.Database.Contexts;
using RequestManager.Database.Models;

namespace RequestManager.API.Repositories;

public class SendGoodsRepository : Repository<SendGoods>
{
    public SendGoodsRepository(DatabaseContext databaseContext, IMapper mapper) : base(databaseContext, mapper)
    {
    }
}