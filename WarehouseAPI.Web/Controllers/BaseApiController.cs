using Microsoft.AspNetCore.Mvc;

namespace WarehouseAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BaseApiController : ControllerBase
    {
    }
}
