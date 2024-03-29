﻿using RequestManager.Database.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace RequestManager.Database.Models;

public class SendGoods : AuditableDatabaseEntity
{
    [Required]
    public DateTime SendDate { get; set; }

    [Required]
    public int Count { get; set; }

    public Goods Requests { get; set; }
}