using Exercise2.EF;
using Exercise2.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Exercise2.Entity;
using EmployeeManagerment.Models;
using System.Runtime.ExceptionServices;
using System;

namespace Exercise2.Services
{
    public class EmployeeService : IEmployeeService
    {
        public readonly EmployeeDBContext Db;
        public EmployeeService(EmployeeDBContext db)
        {
            Db = db;
        }

        public async Task<APIResult<EmployeeViewModel>> Create(EmployeeCreateRequest request)
        {
            using var transaction = Db.Database.BeginTransaction();
            var apiResult = new APIResult<EmployeeViewModel>();
            try
            {
                var employee = new Employee();
                employee.Name = request.Name;
                employee.PositionID = request.PositionId;
                await Db.Employees.AddAsync(employee);
                await Db.SaveChangesAsync();
                await transaction.CommitAsync();
                var employeeViewModel = await GetEmployee(employee.Id);
                apiResult.Success = true;
                apiResult.Message = "Create success!";
                apiResult.ResultObject = employeeViewModel.ResultObject;
            }
            catch (Exception ex)
            { 
                apiResult.Success = false;
                apiResult.Message = $"Create failed, Exeption: {ex.Message}, line {ex.StackTrace}";
                await transaction.RollbackAsync();
            }
            return apiResult;
        }

        public async Task<APIResult<string>> Delete(int id)
        {
            var apiResult = new APIResult<string>();
            using var transaction = Db.Database.BeginTransaction();
            try
            {
                var employee = await Db.Employees.FirstOrDefaultAsync(emp => emp.Id == id);
                if (employee == null)
                {
                    apiResult.Success = false;
                    apiResult.Message = "Cannot find employee!";
                    return apiResult;
                }
                Db.Employees.Remove(employee);
                await Db.SaveChangesAsync();
                await transaction.CommitAsync();
                apiResult.Success = true;
                apiResult.Message = "Delete success!";
            }
            catch(Exception ex)
            {
                apiResult.Success = false;
                apiResult.Message = $"Delete failed, Exeption: {ex.Message}, line {ex.StackTrace}";
                await transaction.RollbackAsync();
            }
            return apiResult;
        }

        public async Task<APIResult<List<EmployeeViewModel>>> GetAll()
        {
            var employees = await Db.Employees.Join(
                    Db.Positions,
                    emp => emp.PositionID,
                    pos => pos.Id,
                    (emp, pos) => new { emp.Id, emp.Name, Position = pos.Name }
                ).ToListAsync();
            var listEmployeeViewModel = new List<EmployeeViewModel>();
            if(employees.Count > 0)
            {
                foreach(var emp in employees)
                {
                    listEmployeeViewModel.Add(new EmployeeViewModel(emp.Id, emp.Name, emp.Position));
                }
            }
            return new APIResult<List<EmployeeViewModel>>() { Success = true, Message = "Success", ResultObject = listEmployeeViewModel };
        }

        public async Task<APIResult<EmployeeViewModel>> GetEmployee(int id)
        {
            var employees = await Db.Employees.Where(emp=>emp.Id == id)
                .Join(
                    Db.Positions, 
                    emp => emp.PositionID,
                    pos => pos.Id,
                    (emp,pos) => new { emp.Id,emp.Name, Position = pos.Name}
                ).FirstOrDefaultAsync();
            var employeeViewModel = new EmployeeViewModel(employees.Id, employees.Name, employees.Position);
            return new APIResult<EmployeeViewModel>() { Success = true, Message = "Success", ResultObject = employeeViewModel };
        }

        public async Task<APIResult<EmployeeViewModel>> Update(int id, EmployeeUpdateRequest request)
        {
            using var transaction = Db.Database.BeginTransaction();
            var apiResult = new APIResult<EmployeeViewModel>();
            try
            { 
                var employee = await Db.Employees.FirstOrDefaultAsync(emp => emp.Id == id);
                if (employee == null)
                {
                    apiResult.Success = false;
                    apiResult.Message = "Cannot find employee!";
                    return apiResult;
                }
                employee.Name = request.Name;
                employee.PositionID = request.PositionId;
                Db.Employees.Update(employee);
                await Db.SaveChangesAsync();
                await transaction.CommitAsync();
                var employeeModel = await GetEmployee(id);
                apiResult.Success = true;
                apiResult.Message = "Update success";
                apiResult.ResultObject = employeeModel.ResultObject;
                
            }
            catch(Exception ex)
            {
                apiResult.Success = false;
                apiResult.Message = $"Update failed, Exeption: {ex.Message}, line {ex.StackTrace}";
                await transaction.RollbackAsync();
            }
            return apiResult;
        }
    }
}
