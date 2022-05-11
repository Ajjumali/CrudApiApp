using CrudApiApp.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrudApiApp.Controller
{
    [Route("api/[Controller]")]
    [ApiController]
    [Authorize(Roles ="Admin")]
    public class todoController : ControllerBase
    {
        private readonly EmployeeDbContext _context;
        public todoController(EmployeeDbContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public Employee GetemployeeById(int id)
        {
            return _context.Employees.FirstOrDefault(x => x.Id == id);
        }
        [Authorize(Roles ="user")]
        public List<Employee> GetEmployees()
        {
            return _context.Employees.ToList();
        }
        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, Employee employee)
        {
            var emp = _context.Employees.SingleOrDefault(x => x.Id == id);
            if (emp == null)
            {
                return NotFound("Employee with the ID " + id + "does not  Exist");
            }
            if (employee.UserName != null)
            {
                emp.UserName = employee.UserName;
            }
            if (employee.Email != null)
            {
                emp.Email = employee.Email;
            }
            if (employee.FirstName != null)
            {
                emp.FirstName = employee.FirstName;
            }if(employee.LastName!=null)
            {
                employee.LastName = employee.LastName;
            }


            _context.Update(emp);
            _context.SaveChanges();
            return Ok("Employee with the id " + id + "updated successfully");
        }
        [HttpPost]
        public IActionResult AddEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return Created("api/employees/" + employee.Id, employee);
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var existingEmployee = _context.Employees.SingleOrDefault(x => x.Id == id);
            if (existingEmployee == null)
            {
                return NotFound("Employee with the ID " + id + "does not  Exist");

            }
            _context.Employees.Remove(existingEmployee);
            _context.SaveChanges();
            return Ok("Employee with the ID " + id + "detele successfully");
        }

    }
}
