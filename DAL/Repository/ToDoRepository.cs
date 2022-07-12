using Core.Models.ToDo;
using DAL.Abstractions.Interfaces;
using DAL.Data;
using System.Linq.Expressions;

namespace DAL.Repository;

public class ToDoRepository : IToDoRepository
{
    private readonly AppDbContext _db;

    public ToDoRepository(AppDbContext db)
    {
        _db = db;
        _db.Database.EnsureCreated();
    }

    public void Add(ToDo toDo)
    {
        toDo.LastTimeEditied = DateTime.Now;

        _db.ToDos.Add(toDo);
        _db.SaveChanges();
    }

    public bool CheckAllByFilter(Expression<Func<ToDo, bool>> filter)
    {
        if (_db.ToDos.All(filter))
        {
            return true;
        }

        return false;
    }

    public bool CheckAnyByFilter(Expression<Func<ToDo, bool>> filter)
    {
        if (_db.ToDos.Any(filter))
        {
            return true;
        }

        return false;
    }

    public void Delete(Expression<Func<ToDo, bool>> filter)
    {
        var toDo = _db.ToDos.Where(filter).FirstOrDefault();

        _db.ToDos.Remove(toDo!);

        _db.SaveChanges();
    }

    public IEnumerable<ToDo> Read(Expression<Func<ToDo, bool>> filter)
    {
        var toDo = _db.ToDos.Where(filter);

        return toDo;
    }

    public IEnumerable<ToDo> ReadAll()
    {
        return _db.ToDos;
    }

    public void Update(ToDo toDo)
    {
        toDo.LastTimeEditied = DateTime.Now;

        _db.Update(toDo);
        _db.SaveChanges();
    }
}
