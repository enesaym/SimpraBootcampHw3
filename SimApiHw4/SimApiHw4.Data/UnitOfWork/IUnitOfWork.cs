using SimApiHw4.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SimApiHw4.Data;

public interface IUnitOfWork 
{
   
    IDapperRepository<Entity> DapperRepository<Entity>() where Entity : BaseModel;
   
    
}
