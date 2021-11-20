using System;

namespace ADOCrud.Models
{
    public class ToDo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Time { get; set; }
        public bool Complete { get; set; } = false;
    }
}
