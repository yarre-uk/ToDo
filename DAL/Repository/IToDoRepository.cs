using Core.Models.ToDo;
using System.Linq.Expressions;

namespace DAL.Repository;

public interface IToDoRepository
{
    void Add(ToDo toDo);
    void Delete(Expression<Func<ToDo, bool>> filter);
    void Update(ToDo toDo);
    IEnumerable<ToDo> ReadAll();
    IEnumerable<ToDo> Read(Expression<Func<ToDo, bool>> filter);
    bool CheckAllByFilter(Expression<Func<ToDo, bool>> filter);
    bool CheckAnyByFilter(Expression<Func<ToDo, bool>> filter);
}
