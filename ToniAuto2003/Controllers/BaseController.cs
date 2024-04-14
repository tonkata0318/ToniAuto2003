using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ToniAuto2003.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        
    }
}
