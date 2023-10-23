using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using efcore.Entities;
using Microsoft.AspNetCore.Mvc;

namespace efcore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {

        private readonly ILogger<UserController> _logger;

public UserController(ILogger<UserController> logger)
{
    _logger = logger;
}




        [HttpGet]
        [Route("GetUser")] 

        public IEnumerable<User> GetUser(){

                SsmBlogContext userContext = new 
                 SsmBlogContext();
                 
               return userContext.Users.ToList();

        }

    [HttpGet]
    [Route("Getzxc/{id}")] 

        public IEnumerable<User> GetAAA(int id){

               using SsmBlogContext userContext = new 
                 SsmBlogContext();

                var res = userContext.Users.ToList();
                // userContext.Dispose();
                Console.WriteLine(res[0].Username);
                 
                //  实体追踪


               return res;
        }



    }
}