using AutoMapper;
using SimApiHw4.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimApiHw4.Schema;

public class MapperInfo : Profile
{
    public MapperInfo()
    {
        CreateMap<Category, CategoryResponse>();
        CreateMap<CategoryRequest, Category>();
    }
  
}
