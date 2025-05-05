using AutoMapper;
using IBEXDATA.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class UserDB: IUserDB
    {

        private readonly dbContext _dbContext;
        private readonly IMapper mapper;

       
        public UserDB(dbContext dbContext, IMapper _Mapper, ILogger<UserDB> logger)
        {
            _dbContext = dbContext;
            mapper = _Mapper;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }
       
        //public async Task<User> GetUserById(int id)
        //{
        //    return await _dbContext.Users.FindAsync(id);
        //}


    }
}
