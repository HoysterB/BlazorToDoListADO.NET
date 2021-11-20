using ADOCrud.Models;
using System.Collections.Generic;

namespace ADOCrud.Interfaces
{
    public interface IToDoRepository
    {
        List<ToDo> GetAll();
        ToDo GetById(int id);
        ToDo Create(ToDo toDo);
        ToDo Update(ToDo toDo);
        void Delete(int id);
        void ChangeComplete(ToDo toDo);
    }
}
