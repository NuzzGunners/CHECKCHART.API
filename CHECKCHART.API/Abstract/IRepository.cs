using CHECKCHART.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CHECKCHART.API.Abstract
{
    public interface ICheckchartRepository : IEntityBaseRepository<Checkchart> { }
    public interface IPositionRepository : IEntityBaseRepository<Position> { }
    public interface IReasonRepository : IEntityBaseRepository<Reason> { }
    public interface IRoleRepository : IEntityBaseRepository<Role> { }
    public interface IUserRepository : IEntityBaseRepository<User> { }
    public interface IUserPositionRepository : IEntityBaseRepository<Userposition> { }
    public interface IAuditRepository : IEntityBaseRepository<Audit> { }
    public interface IAuditCategoryRepository : IEntityBaseRepository<AuditCategory> { }
    public interface ICheckchartLogRepository : IEntityBaseRepository<V_CheckchartLog> { }
    public interface IViewAuditRepository : IEntityBaseRepository<V_Audit> { }
}
