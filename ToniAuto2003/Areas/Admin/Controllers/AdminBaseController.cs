using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static ToniAuto2003.Core.Constants.RoleConstants;

namespace ToniAuto2003.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = AdminRole)]
    public class AdminBaseController : Controller
    {
        
    }
}
