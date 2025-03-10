﻿using IBEXDATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public interface IBankDB
    {
       Task<IEnumerable<Bank>> Get();
       Task<IEnumerable<Bank>> GetNames();
    }
}
