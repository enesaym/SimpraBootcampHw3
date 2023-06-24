using SimApiHw4.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimApiHw4.Data;

public interface IDapperRepository<Entity>  where Entity : BaseModel
{
    Task InsertAsync(Entity t);
    Task UpdateAsync(Entity t);
    List<Entity> GetAll();
    Task DeleteAsync(int id);
    Task<Entity> GetByIdAsync(int id);

}
