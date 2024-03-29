﻿using Horeb.Infrastructure.Data;
using Horeb.MoneySaver.Persistency.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Horeb.MoneySaver.Persistency.EntityDataModels;

[Table(TableNames.AppSettings)]
public class AppSettingsModel : Identity<int>
{
    [Required]
    public int PeriodDurationType { get; set; }

    [Required]
    public int CycleStartDay { get; set; }
}
