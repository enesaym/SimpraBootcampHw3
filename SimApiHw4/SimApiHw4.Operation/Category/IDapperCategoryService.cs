using SimApiHw4.Base;
using SimApiHw4.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimApiHw4.Operation;

public interface IDapperCategoryService
{
    ApiResponse<List<CategoryResponse>> GetAll();
    ApiResponse Insert(CategoryRequest request);
    ApiResponse Update(int Id, CategoryRequest request);
    Task<ApiResponse<CategoryResponse>> GetById(int id);
}
