using CHECKCHART.API.Abstract;
using CHECKCHART.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CHECKCHART.API.Repositories
{
    public class CheckchartRepository : EntityBaseRepository<Checkchart>, ICheckchartRepository
    {
        public CheckchartRepository(CheckChartDbContext context)
            : base(context)
        { }
    }
}
