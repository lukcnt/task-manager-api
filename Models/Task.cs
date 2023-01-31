using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace task_manager_api.Models
{
    public class Task
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public EnumStatusTask Status { get; set; }
    }
}