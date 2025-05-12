using CustomImplementations.CustomCache;
using CustomImplementations.Data;
using CustomImplementations.Models;
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

        [HttpGet("random_user")]
        public IActionResult GetRandomUser()
        {
            var cacheKey = "random_user";
            var cached = _cacheService.Get<FakeUser>(cacheKey);

            if (cached!.Found)
            {
                return Ok(new
                {
                    Source = "Cache",
                    Data = cached.Value
                });
            }

            var newUser = FakeUserGenerator.Generate();

            _cacheService.Set(cacheKey, newUser, TimeSpan.FromMinutes(2)); // sliding expiration applies

            return Ok(new
            {
                Source = "Generated",
                Data = newUser
            });
        }

        [HttpGet("random_users_list")]
        public IActionResult GetRandomUserList([FromQuery] int count = 10)
        {
            var cacheKey = "random_users_list";
            var cached = _cacheService.Get<List<FakeUser>>(cacheKey);

            if (cached!.Found)
            {
                return Ok(new
                {
                    Source = "Cache",
                    Data = cached.Value
                });
            }

            var newUser = FakeUserGenerator.GenerateList(count);

            _cacheService.Set(cacheKey, newUser, TimeSpan.FromMinutes(5)); // sliding expiration applies

            return Ok(new
            {
                Source = "Generated",
                Data = newUser
            });
        }
    }
}
