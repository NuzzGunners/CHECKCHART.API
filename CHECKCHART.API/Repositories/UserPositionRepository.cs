﻿using CHECKCHART.API.Abstract;
using CHECKCHART.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CHECKCHART.API.Repositories
{
    public class UserPositionRepository : EntityBaseRepository<Userposition>, IUserPositionRepository
    {
        public UserPositionRepository(CheckChartDbContext context)
            : base(context)
        { }
    }
}
