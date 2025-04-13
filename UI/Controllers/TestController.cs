using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UI.MySession;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : ControllerBase
    {
        [HttpGet]
        public IActionResult TestGetSession()
        {
            // Lấy session lần thứ nhất(Trong cùng 1 Request-Scoped)
            var session = HttpContext.GetSession();
            //Set key-value vào session đó
            session.SetString("Name", "Test");

            // Lấy session lần thứ 2(Trong cùng 1 Request-Scoped)
            session = HttpContext.GetSession();
            //Thử lấy giá trị của key-value set ở lần thứ nhất
            var value = session.GetString("Name");

            if(value == "Test")
            {
                //Nếu value == Test => Session tồn tại trong Request-Scoped(HttpContext)
                return Ok();
            }
            else
            {
                // Nếu value != Test => Session không dc cache trong 1 Request-Scoped(HttpContext)
                return BadRequest();
            }
        }

        [HttpGet("SetSession")]
        public async Task<IActionResult> SetSessionAsync(string key, string value)
        {
            var session = HttpContext.GetSession();
            await session.LoadAsync();

            session.SetString(key, value);

            await session.CommitAsync();

            return Ok();
        }
        [HttpGet("GetSession")]
        public async Task<IActionResult> GetSessionAsync(string key)
        {
            var session = HttpContext.GetSession();
            await session.LoadAsync();

            var value = session.GetString(key);

            await session.CommitAsync();

            return Ok(value);
        }
    }
}
