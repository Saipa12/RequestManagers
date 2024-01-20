using RequestManager.Database.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace RequestManager.Database.Models;

public class DeliverGoods : AuditableDatabaseEntity
{
    [Required]
    public DateTime DeliverDate { get; set; }

    [Required]
    public int Count { get; set; }

    [Required]
    public Goods Goods { get; set; }
}