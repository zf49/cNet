using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using efcore.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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

        public IEnumerable<User> GetUser()
        {

            SsmBlogContext userContext = new
             SsmBlogContext();

            return userContext.Users.ToList();

        }

        [HttpGet]
        [Route("Getzxc/{id}")]

        public User GetAAA(int id)
        {

            using SsmBlogContext userContext = new
              SsmBlogContext();

            userContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

            var user = userContext.Users.AsNoTracking().FirstOrDefault(x => x.Id == 1);

            var article = userContext.Articles.FirstOrDefault(x => x.Id == 1);
            article.Title = "6666";

            // var res = userContext.Users.ToList();
            // // userContext.Dispose();
            // Console.WriteLine(res[0].Username);



            // var user = userContext.Users.FirstOrDefault(x=>x.Id == 1);

            Console.WriteLine(user.Pwd);
            //  实体追踪

            user.Pwd = "666";

            //add

            var newUser = new User { Username = "Dotnet New User1", Pwd = "123" };

            userContext.Users.Add(newUser);

            // userContext.Users.Update(newUser);


            string jsonData = @"[
    {""Username"": ""Alice"", ""Pwd"": ""password1""},
    {""Username"": ""Bob"", ""Pwd"": ""password2""},
    {""Username"": ""Charlie"", ""Pwd"": ""password3""}
]";

            var users = JsonConvert.DeserializeObject<List<User>>(jsonData);

            SsmBlogContext rangeAddContext = new SsmBlogContext();

            rangeAddContext.Users.AddRange(users);

            rangeAddContext.Users.Where(x => x.Username.Contains("A"));




            rangeAddContext.SaveChanges();




            userContext.Update(article);
            userContext.SaveChanges();
            Console.WriteLine(article.Title);

            return user;
        }



        [HttpGet]
        [Route("checkUser")]
        public IEnumerable<User> CheckUser()
        {

            SsmBlogContext context = new SsmBlogContext();


            List<User> users = context.Users.Where(x => EF.Functions.Collate(x.Username, "utf8mb4_bin").Contains("A")).ToList();

            return users;

        }


        [HttpGet]
        [Route("delete")]
        public void DeleteUser()
        {

            SsmBlogContext context = new SsmBlogContext();

            var userIdToDelete = 1;

            var articlesToDelete = context.Articles.Where(a => a.AuthorId == userIdToDelete).ToList();

            // 为每篇文章获取评论列表并删除
            foreach (var article in articlesToDelete)
            {
                var commentsToDelete = context.Comments.Where(c => c.ArticleId == article.Id).ToList();
                context.Comments.RemoveRange(commentsToDelete);
            }

            // 删除文章
            context.Articles.RemoveRange(articlesToDelete);

            // 删除与该用户关联的所有评论
            var userCommentsToDelete = context.Comments.Where(c => c.AuthorId == userIdToDelete).ToList();
            context.Comments.RemoveRange(userCommentsToDelete);

            // 删除用户
            var userToDelete = context.Users.Find(userIdToDelete);
            context.Users.Remove(userToDelete);

            context.SaveChanges();






        }




    }
}