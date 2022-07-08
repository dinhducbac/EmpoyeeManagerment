using EmployeeManagerment.Models;
using Exercise2.EF;
using Exercise2.Entity;
using Exercise2.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagerment.Services
{
    public class PositionService : IPositionService
    {
        public readonly EmployeeDBContext Db;
        public PositionService(EmployeeDBContext dBContext)
        {
            Db = dBContext;
        }

        public async Task<APIResult<Position>> Create(PositionCreateRequest request)
        {
            using var transaction = Db.Database.BeginTransaction();
            var apiResult = new APIResult<Position>();
            try
            {
                var positions = new Position();
                positions.Name = request.Name;
                await Db.Positions.AddAsync(positions);
                await Db.SaveChangesAsync();
                await transaction.CommitAsync();
                var employeeViewModel = await GetPositionById(positions.Id);
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

        public async Task<APIResult<Position>> GetPositionById(int id)
        {
            var apiResult = new APIResult<Position>();
            var position = await Db.Positions.FirstOrDefaultAsync(pos => pos.Id == id);
            if (position == null)
            {
                apiResult.Success = false;
                apiResult.Message = "Cannot find position!";
            }
            else
            {
                apiResult.Success = true;
                apiResult.Message = "Succesful!";
                apiResult.ResultObject = position;
            }
            return apiResult;
        }

        public async Task<APIResult<List<Position>>> GetAll()
        {
            var positions = await Db.Positions.ToListAsync();
            return new APIResult<List<Position>>() { Success = true, Message = "Successful!", ResultObject = positions };
        }

        public async Task<APIResult<Position>> Update(int id, PositionUpdateRequest request)
        {
            using var transaction = Db.Database.BeginTransaction();
            var apiResult = new APIResult<Position>();
            try
            {
                var position = await Db.Positions.FirstOrDefaultAsync(pos => pos.Id == id);
                if (position == null)
                {
                    apiResult.Success = false;
                    apiResult.Message = "Cannot find employee!";
                    return apiResult;
                }
                position.Name = request.Name;
                Db.Positions.Update(position);
                await Db.SaveChangesAsync();
                await transaction.CommitAsync();
                var employeeModel = await GetPositionById(id);
                apiResult.Success = true;
                apiResult.Message = "Update success";
                apiResult.ResultObject = employeeModel.ResultObject;

            }
            catch (Exception ex)
            {
                apiResult.Success = false;
                apiResult.Message = $"Update failed, Exeption: {ex.Message}, line {ex.StackTrace}";
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
                var position = await Db.Positions.FirstOrDefaultAsync(pos => pos.Id == id);
                if (position == null)
                {
                    apiResult.Success = false;
                    apiResult.Message = "Cannot find employee!";
                    return apiResult;
                }
                Db.Positions.Remove(position);
                await Db.SaveChangesAsync();
                await transaction.CommitAsync();
                apiResult.Success = true;
                apiResult.Message = "Delete success!";
            }
            catch (Exception ex)
            {
                apiResult.Success = false;
                apiResult.Message = $"Delete failed, Exeption: {ex.Message}, line {ex.StackTrace}";
                await transaction.RollbackAsync();
            }
            return apiResult;
        }
    }
}
