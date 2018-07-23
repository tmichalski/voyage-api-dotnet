﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Voyage.Models;
using Voyage.Models.Entities;

namespace Voyage.Services.Audit
{
    public interface IAuditService
    {
        Task RecordAsync(ActivityAuditModel model);

        IList<ActivityAudit> GetAuditActivityWithinTime(string userName, string path, int timeInMinutes);
    }
}
