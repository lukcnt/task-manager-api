using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using task_manager_api.Context;
using task_manager_api.Models;

namespace task_manager_api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase
    {
        private readonly OrganizerContext _context;

        public TaskController(OrganizerContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var task = _context.Tasks.Find(id);
            if(task == null)
                return NotFound();

            return Ok(task);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var task = _context.Tasks.ToList();
            return Ok(task);
        }

        [HttpGet("GetByTitle")]
        public IActionResult GetByTitle(string title)
        {
            var task = _context.Tasks.Where(x => x.Title.Contains(title));
            return Ok(task);
        }

        [HttpGet("GetByDate")]
        public IActionResult GetByDate(DateTime date)
        {
            var task = _context.Tasks.Where(x => x.Date.Date == date.Date);
            return Ok(task);
        }

        [HttpGet("GetByStatus")]
        public IActionResult GetByStatus(EnumStatusTask status)
        {
            var task = _context.Tasks.Where(x => x.Status == status);
            return Ok(task);
        }

        [HttpPost]
        public IActionResult Create(TaskAt task)
        {
            if (task.Date == DateTime.MinValue)
                return BadRequest(new { Error = "The date of the task cannot be empty" });

            _context.Add(task);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, task);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, TaskAt task)
        {
            var taskDatabase = _context.Tasks.Find(id);

            if (taskDatabase == null)
                return NotFound();

            if (task.Date == DateTime.MinValue)
                return BadRequest(new { Error = "The date of the task cannot be empty" });

            taskDatabase.Title = task.Title;
            taskDatabase.Description = task.Description;
            taskDatabase.Date = task.Date;
            taskDatabase.Status = task.Status;

            _context.Tasks.Update(taskDatabase);
            _context.SaveChanges();
            return Ok(taskDatabase);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var taskDatabase = _context.Tasks.Find(id);

            if (taskDatabase == null)
                return NotFound();

            _context.Tasks.Remove(taskDatabase);
            _context.SaveChanges();
            return NoContent();
        }
    }
}