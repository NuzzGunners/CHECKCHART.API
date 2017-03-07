using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CHECKCHART.API.Models;

namespace CHECKCHART.API.DbInitializer
{
    public class CheckchartDbInitializer
    { 
        private static CheckChartDbContext context;
        public static void Initialize(IServiceProvider serviceProvider)
        {
            context = (CheckChartDbContext)serviceProvider.GetService(typeof(CheckChartDbContext));

            InitializeCheckchart();
        }
        private static void InitializeCheckchart()
        {
            if (!context.Checkchart.Any()) { }
            if (!context.Position.Any()) { }
            if (!context.Reason.Any()) { }
            if (!context.Role.Any()) { }
            if (!context.User.Any()) { }
            if (!context.Userposition.Any()) { }
        }
    }
}
