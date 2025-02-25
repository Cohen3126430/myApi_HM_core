using Microsoft.AspNetCore.Mvc;
using myApi.models;
using myApi.Interfaces;
namespace myApi.Controllers;

[ApiController]
[Route("[controller]")]
public class GiftsController : ControllerBase
{
    private IGiftService GiftService;

    public GiftsController(IGiftService giftService){
        this.GiftService=giftService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Gift>> Get()
    {
        return GiftService.Get();
    }
    [HttpGet("{id}")]
    public ActionResult<Gift> Get(int id)
    {
        var gift=GiftService.Get(id);
        if(gift==null)
            return NotFound("not found");
        return gift;
    }
    [HttpPost]
    public ActionResult Post (Gift newGift){
        var newId=GiftService.Insert(newGift);
        if(newId==-1)
            return BadRequest();
        return CreatedAtAction(nameof(Post),new{id=newId});
    }

    [HttpPut("{id}")]
    public ActionResult Put(int id,Gift newGift){
        if(GiftService.Update(id,newGift));
            return NoContent();
        return BadRequest();
    }

    [HttpDelete("{id}")]
    public ActionResult Delete(int id){
        if(GiftService.Delete(id))
            return Ok();
        return NotFound();
    }
}
