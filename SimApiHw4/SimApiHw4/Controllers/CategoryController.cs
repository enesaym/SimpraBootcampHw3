using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SimApiHw4.Base;
using SimApiHw4.Data;
using SimApiHw4.Operation;
using SimApiHw4.Schema;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SimApiHw4.Service;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{

    private readonly IDapperCategoryService _categoryService;
    public CategoryController(IDapperCategoryService categoryService)
    {
        this._categoryService = categoryService;
    }
    [HttpGet]
    public ApiResponse<List<CategoryResponse>> GetAll()
    {
        var CategoryList = _categoryService.GetAll();
        return CategoryList;
    }
    [HttpGet("{id}")]
    public async Task<ApiResponse<CategoryResponse>> GetById(int id)
    {
        var response = await _categoryService.GetById(id);
        return response;
    }
    [HttpPost]
    public ApiResponse Post([FromBody] CategoryRequest request)
    {
        var response = _categoryService.Insert(request);
        return response;
    }
    [HttpPut("{id}")]
    public ApiResponse Update(int id,[FromBody] CategoryRequest request)
    {
        var response = _categoryService.Update(id, request);
        return response;
    }
}
