using RequestManager.Database.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace RequestManager.Database.Models;

public class Goods : AuditableDatabaseEntity
{
    [Required]
    public string Surname { get; set; }

    public List<SendGoods> SendGoods { get; set; }
    public List<DeliverGoods> DeliverGoods { get; set; }
}