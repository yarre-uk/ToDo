using Core.Models.ToDo;
using DAL.Repository;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class ToDoController : ControllerBase
{
    private readonly IToDoRepository _db;

    public ToDoController(IToDoRepository db)
    {
        _db = db;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ToDo>> Get()
    {
        return Ok(_db.ReadAll());
    }

    [HttpGet("{id}")]
    public ActionResult<ToDo> Get(int id)
    {
        try
        {
            var toDo = _db.Read(toDo => toDo.Id == id).SingleOrDefault();

            if (toDo == null)
            {
                return NotFound();
            }

            return Ok(toDo);
        }
        catch
        {
            return BadRequest();
        }
    }

    [HttpPost]
    public IActionResult Post([FromBody] ToDo toDo)
    {
        try
        {
            _db.Add(toDo);

            return Created("ToDo created - ", toDo);
        }
        catch (Exception ex)
        {
            return BadRequest($"Exeption - {ex.Message}");
        }
    }

    [HttpPut]
    public IActionResult Put([FromBody] ToDo toDo)
    {
        try
        {
            _db.Update(toDo);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest($"Exeption - {ex.Message}");
        }
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            if (!_db.CheckAnyByFilter(x => x.Id == id))
            {
                return NotFound();
            }

            _db.Delete(x => x.Id == id);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest($"Exeption - {ex.Message}");
        }
    }
}