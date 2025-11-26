using CoreApiAngular.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CoreApiAngular.Controllers
{
    [Route("UserManagement")]
    [ApiController]
    public class UserManagementController : ControllerBase
    {
        private readonly string _uploadDir = Path.Combine(Directory.GetCurrentDirectory(), "Uploads");
        public UserManagementController()
        {
            if (!Directory.Exists(_uploadDir))
            {
                Directory.CreateDirectory(_uploadDir);
            }
        }
        UserDBCls dbobj = new UserDBCls();
        // GET: api/<UserManagementController>
       
        [HttpGet("gettabWithId/{id}")]
      
        public async Task<IActionResult> Get(int id)
        {
            UserCls getEmployee = dbobj.SelectProfileDB(id);
            var fileUrl = Path.Combine(Directory.GetCurrentDirectory(), "Uploads",getEmployee.photo);
            byte[] imageBytes = await System.IO.File.ReadAllBytesAsync(fileUrl);
            string base64String = Convert.ToBase64String(imageBytes);
            getEmployee.photo = base64String;
            return Ok(getEmployee);
        }

       

        // POST api/<UserManagementController>
        [HttpPost]
        [Route("inserttab")]
        public async Task<IActionResult> Post([FromForm] UserCreateDTO createdto)
        {
            UserCls userCls = new UserCls();
            if(createdto.path==null||createdto.path.Length==0)
            {
                return BadRequest("No file uploaded..");
            }
            if(!createdto.path.ContentType.StartsWith("image/"))
            {
                return BadRequest("Only image files are allowed.");
            }
            var filepath = Path.Combine(_uploadDir, createdto.path.FileName);
            using(var stream=new FileStream(filepath,FileMode.Create))
            {
                await createdto.path.CopyToAsync(stream);
            }
            userCls.name = createdto.name;
            userCls.age = createdto.age;
            userCls.addr = createdto.addr;
            userCls.email = createdto.email;
            userCls.photo = createdto.path.FileName;
            userCls.uname = createdto.uname;
            userCls.password = createdto.password;
            dbobj.InsertDB(userCls);
            return await Task.Run(() => Ok(new { message = "Registered successfully.." }));
        }
        [HttpPost]
        [Route("logintab")]
        public async Task<IActionResult>PostLogin([FromBody] UserLoginDTO createdto)
        {
            UserCls userCls = new UserCls();
            userCls.uname = createdto.uname;
            userCls.password = createdto.password;
            string cid = dbobj.LoginDB(userCls);
            if(cid=="1")
            {
                string uid = dbobj.GetUserID(userCls);
                return await Task.Run(() => Ok(new { userId = uid }));
                // return await Task.Run(()=>Ok(new{message="Success"}));
            }
            else
            {
                return await Task.Run(() => Ok(new { message = "not Success" }));
            }
        }

   

        // PUT api/<UserManagementController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<UserManagementController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
