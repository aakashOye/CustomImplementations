using CustomImplementations.CustomCache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CustomImplementations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BogusController : ControllerBase
    {
        private readonly ICacheService _cacheService;
        public BogusController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }
    }
}
