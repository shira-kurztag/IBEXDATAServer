﻿using IBEXDATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public interface IOwnerDB
    {
        Task<IEnumerable<Owner>> Get();
    }
}
