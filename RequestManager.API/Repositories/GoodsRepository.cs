using AutoMapper;
using RequestManager.Core.Repositories;
using RequestManager.Database.Contexts;
using RequestManager.Database.Models;

namespace RequestManager.API.Repositories;

public class GoodsRepository : Repository<Goods>
{
    public GoodsRepository(DatabaseContext databaseContext, IMapper mapper) : base(databaseContext, mapper)
    {
    }
}