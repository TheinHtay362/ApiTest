using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Context;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext db;

        //public UserController()
        //{
        //    db = new AppDbContext();
        //}
        
        public UserController(AppDbContext dbContext, IConfiguration configuration )
        {
            var conStr = configuration.GetConnectionString("DbStr");
            db = dbContext;
        }

        // CRUD
        // get => Read
        // post => Create
        // put => Update
        // delete => Delete

        [HttpGet]
        public async Task<IActionResult> GetUserList()
        {
            var lst = await db.Users.Where(x => x.delFlag == false).ToListAsync();
            return Ok(lst);
        }

        [HttpGet("{pageNo:int}/{rowCount:int}")]
        public async Task<IActionResult> GetUserList(int pageNo= 1, int rowCount = 10)
        {
            var lst = await db.Users.Where(x => x.delFlag == false).Skip(pageNo * rowCount - rowCount).Take(rowCount).ToListAsync();
            return Ok(lst);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var item = await db.Users.Where(x => x.userId == id && x.delFlag == false).FirstOrDefaultAsync();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(UserModel userModel)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                await db.Users.AddAsync(userModel);
                var count = await db.SaveChangesAsync();
                model.respCode = count == 1 ? "000" : "999";
                model.respDesp = count == 1 ? "Saving successful!" : "Saving failed!";
            }
            catch (Exception ex)
            {
                model.respCode = "999";
                model.respDesp = ex.Message + ex.StackTrace;
            }
            return Ok(model);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateUser(int id, UserModel userModel)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                userModel.userId = id;
                db.Users.Update(userModel);
                var count = await db.SaveChangesAsync();
                model.respCode = count == 1 ? "000" : "999";
                model.respDesp = count == 1 ? "Saving successful!" : "Saving failed!";
            }
            catch (Exception ex)
            {
                model.respCode = "999";
                model.respDesp = ex.Message + ex.StackTrace;
            }
            return Ok(model);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                var item = await db.Users.Where(x => x.userId == id).FirstOrDefaultAsync();
                item.delFlag = true;
                db.Users.Update(item);
                var count = await db.SaveChangesAsync(); 
                model.respCode = count == 1 ? "000" : "999";
                model.respDesp = count == 1 ? "Saving successful!" : "Saving failed!";
            }
            catch (Exception ex)
            {
                model.respCode = "999";
                model.respDesp = ex.Message + ex.StackTrace;
            }
            return Ok(model);
        }
    }
}
