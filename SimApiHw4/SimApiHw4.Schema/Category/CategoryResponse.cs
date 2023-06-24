﻿using SimApiHw4.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimApiHw4.Schema;

public class CategoryResponse 
{
    public string Name { get; set; }
    public int Order { get; set; }
    public int Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string UpdatedBy { get; set; }
}
