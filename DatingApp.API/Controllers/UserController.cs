using System.Linq;
using DatingApp.API.Data;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly DataContext _dbContext;
        public UserController(DataContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpGet]
        public IActionResult getUsers() {
            var datas = _dbContext.Users.ToList();
            return Ok(datas);
        }

         [HttpGet("{id}")]
        public IActionResult getUsers(int id) {
            var data = _dbContext.Users.ToList().FirstOrDefault(x=>x.Id == id);
            return Ok(data);
        }
    }
}