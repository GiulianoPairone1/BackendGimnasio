﻿using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    class SuperAdminRepository :RepositoryBase<SuperAdmin>, ISuperAdminRepository
    {
        public SuperAdminRepository(ApplicationDbContext context) : base(context) 
        { }
    }
}
