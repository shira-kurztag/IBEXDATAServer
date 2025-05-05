using AutoMapper;
using DB;
using IBEXDATA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class UserService: IUserService
    {


        private readonly IUserDB _userDB;

        private readonly IMapper _mapper;


        public UserService(IUserDB UserDB, IMapper mapper)
        {
            _userDB = UserDB;
            _mapper = mapper;

        }
      
        public async Task<List<User>> GetAllAdmin()
        {
            // שולף את כל המשתמשים ומסנן רק את אלו שהuserRole שלהם שווה ל-1
            var allUsers = await _userDB.GetAllUsers();
            var admins = allUsers.Where(user => user.UserRole == 1).ToList();
            return admins;
        }
    }
}
