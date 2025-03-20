using Microsoft.AspNetCore.Mvc;
using myApi.models;
using myApi.Interfaces;
namespace myApi.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController: ControllerBase
{
    private IUserService UserService;

    public UserController(IUserService UserService){
        this.UserService = UserService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<User>> Get()
    {
        System.Console.WriteLine("in get of UserController-----------");
        return UserService.Get();
    }
    [HttpGet("{id}")]
    public ActionResult<User> Get(int id)
    {
        var User=UserService.Get(id);
        if(User==null)
            return NotFound("not found");
        return User;
    }
    [HttpPost]
    public ActionResult Post (User newUser){
        System.Console.WriteLine(newUser);
        var newId=UserService.Insert(newUser);
        System.Console.WriteLine(newId);
        if(newId==-1)
            return BadRequest();
        return CreatedAtAction(nameof(Post),new{id=newId});
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id,User newUser){
        if(UserService.Update(id,newUser));
            return NoContent();
        return BadRequest();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id){
        if(UserService.Delete(id))
            return Ok();
        return NotFound();
    }
}
