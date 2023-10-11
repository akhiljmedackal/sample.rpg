using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace sample.rpg.Data
{
    public class RepositoryServices : IRepositoryServices
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configruations;

        public RepositoryServices(DataContext context,IConfiguration configruations)
        {
            _context = context;
            _configruations = configruations;
        }
        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            var response=new ServiceResponse<string>();
            var user=await _context.Users.FirstOrDefaultAsync(u=>u.UserName==username);
            if(user is null)
            {
                response.Success=false;
                response.Message="username is incorrect";
            }
            else if(!VerifyPassword(password,user.PasswordHash,user.PasswordSalt))
            {
                response.Success=false;
                response.Message="wrong password";
            }
            else{
                response.Message="Succesfully logined";
                response.Data=CreateToken(user);
            }
            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _context.Users.AnyAsync(u=>u.UserName.ToLower()==username.ToLower()))
            {
                return true;
            }
            return false;
        }

        public async Task<ServiceResponse<int>> UserRegisteration(User user, string password)
        {
            var response=new ServiceResponse<int>();
           
            if(await UserExists(user.UserName))
             {
                 response.Success=false;
                 response.Message=$"user with the username {user.UserName} already exist";
                 return response;
             }
            CreatePassword(password,out byte[]passwordHash,out byte[]passwordSalt);
            user.PasswordSalt=passwordSalt;
            user.PasswordHash=passwordHash;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            
            response.Data=user.Id;
            return response;
        }
        private void CreatePassword(string password,out byte[] passwordHash ,out byte[] passwordSalt)
        {
            using(var hmac=new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt=hmac.Key;
                passwordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
        private bool VerifyPassword(string password, byte[] passwordHash , byte[] passwordSalt)
        {
            using(var hmac=new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
            
        }
        private string CreateToken(User user)
        {
           var claims=new List<Claim>
           {
            new Claim(ClaimTypes.NameIdentifier ,user.Id.ToString()),
            new Claim(ClaimTypes.Name,user.UserName)
            
           };
           var appsettingsToken=_configruations.GetSection("AppSettings:Token").Value;
           if(appsettingsToken is null)
           {
            throw new Exception("appsettingtoken is null");
           }
           SymmetricSecurityKey key=new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(appsettingsToken));
           SigningCredentials creds=new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);
           var tokenDescriptor =new SecurityTokenDescriptor{
            Subject=new ClaimsIdentity(claims),
            Expires=DateTime.Now.AddDays(1),
            SigningCredentials=creds
           };
           JwtSecurityTokenHandler jwtSecurityTokenHandler=new JwtSecurityTokenHandler();
           SecurityToken token= jwtSecurityTokenHandler.CreateToken(tokenDescriptor);
           return jwtSecurityTokenHandler.WriteToken(token);
        }
    }
}