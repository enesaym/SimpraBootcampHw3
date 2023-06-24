using AutoMapper;
using Serilog;
using SimApiHw4.Base;
using SimApiHw4.Data;
using SimApiHw4.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using static Dapper.SqlMapper;

namespace SimApiHw4.Operation;

public class DapperCategoryService :IDapperCategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public DapperCategoryService(IUnitOfWork unitOfWork,IMapper mapper)
    {
        this._unitOfWork = unitOfWork;
        this._mapper = mapper;  

    }
    
    public ApiResponse<List<CategoryResponse>> GetAll()
    {
        try
        {
            var entityList = _unitOfWork.DapperRepository<Category>().GetAll();
            var mapped = _mapper.Map<List<Category>, List<CategoryResponse>>(entityList);
            return new ApiResponse<List<CategoryResponse>>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<List<CategoryResponse>>(ex.Message);
        }
    }
    public ApiResponse Insert(CategoryRequest request)
    {
        try
        {
            var entity = _mapper.Map<CategoryRequest, Category>(request);
            entity.CreatedAt=DateTime.UtcNow;
            entity.CreatedBy = Environment.UserName;
            _unitOfWork.DapperRepository<Category>().InsertAsync(entity);
            return new ApiResponse();
        }
        catch (Exception ex)
        {
            return new ApiResponse(ex.Message);
        }

    }
    public virtual ApiResponse Update(int Id, CategoryRequest request)
    {
        try
        {
            var entity = _mapper.Map<CategoryRequest, Category>(request);

            var exist = _unitOfWork.DapperRepository<Category>().GetByIdAsync(Id);
            if (exist is null)
            {
                return new ApiResponse("Record not found");
            }
            entity.Id = Id;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.UpdatedBy= Environment.UserName;
            _unitOfWork.DapperRepository<Category>().UpdateAsync(entity);
            return new ApiResponse();
        }
        catch (Exception ex)
        {
            return new ApiResponse(ex.Message);
        }
    }
    public async Task<ApiResponse<CategoryResponse>> GetById(int id)
    {
        try
        {
            var entity = await _unitOfWork.DapperRepository<Category>().GetByIdAsync(id);
            if (entity is null)
            {
                return new ApiResponse<CategoryResponse>("Category not found");
            }

            var mapped = _mapper.Map<Category, CategoryResponse>(entity);
            return new ApiResponse<CategoryResponse>(mapped);
        }
        catch (Exception ex)
        {
            return new ApiResponse<CategoryResponse>(ex.Message);
        }
    }


}
