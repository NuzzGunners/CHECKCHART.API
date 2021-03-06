﻿using CHECKCHART.API.Abstract;
using CHECKCHART.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CHECKCHART.API.Repositories
{
    public class ReasonRepository : EntityBaseRepository<Reason>, IReasonRepository
    {
        public ReasonRepository(CheckChartDbContext context)
            : base(context)
        { }
    }
}
