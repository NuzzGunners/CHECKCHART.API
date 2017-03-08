using CHECKCHART.API.Abstract;
using CHECKCHART.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CHECKCHART.API.Repositories
{
    public class ViewAuditRepository : EntityBaseRepository<V_Audit>, IViewAuditRepository
    {
        public ViewAuditRepository(CheckChartDbContext context) : base(context)
        {
        }
    }
}
