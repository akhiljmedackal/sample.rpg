using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace sample.rpg.Data
{
    public interface IRepositoryServices
    {
        Task<ServiceResponse<int>> UserRegisteration(User user,string password);
        Task<ServiceResponse<string>> Login(string username,string password);
        Task<bool> UserExists(string username);
    }
}