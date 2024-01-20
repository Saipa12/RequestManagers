using AutoMapper;
using RequestManager.Core.Repositories;
using RequestManager.Database.Contexts;
using RequestManager.Database.Models;

namespace RequestManager.API.Repositories;

public class DeliverGoodsRepository : Repository<DeliverGoods>
{
    public DeliverGoodsRepository(DatabaseContext databaseContext, IMapper mapper) : base(databaseContext, mapper)
    {
    }
}