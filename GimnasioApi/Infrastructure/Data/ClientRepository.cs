﻿using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    class ClientRepository :RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(ApplicationDbContext context) : base(context) 
        { }
    }
}
