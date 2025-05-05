using System;
using System.Linq;
using StackExchange.Redis;
using Microsoft.EntityFrameworkCore;
using TestMandiri.Data.Models;
using TestMandiri.Data.Common;
using YourNamespace.Services.Interfaces;

namespace TestMandiri.Services
{
    public class AuthService : IAuthService
    {
        private readonly TestMandiriContext _db;
        private readonly IConnectionMultiplexer _redis;

        public AuthService(TestMandiriContext db, IConnectionMultiplexer redis)
        {
            _db = db;
            _redis = redis;
        }

        public string Login(string username, string password)
        {
            try { 
            var user = _db.Msusers.Where(u => u.Username == username).FirstOrDefault();

            if (user == null)
                return "User tidak ditemukan.";

            if (!user.Active.Value)
                return "User nonaktif.";

            var redisDb = _redis.GetDatabase();
            var redisKey = $"loginAttempts:{username}";

            int loginAttempts = (int)redisDb.StringGet(redisKey);

            if (loginAttempts >= 3)
            {
                user.Active = false;
                _db.SaveChanges();
                return "Akun Anda dinonaktifkan setelah 3 percobaan login gagal.";
            }

            var decryptor = AESHelper.Decrypt(user.Passcode);

            if (! password.SequenceEqual(decryptor))
            {
                redisDb.StringIncrement(redisKey);
                return "Username atau password salah.";
            }

            redisDb.KeyDelete(redisKey);  
            return "Login berhasil.";
        }catch (Exception ex)
            {
                LoggerHelper.LogError(nameof(Register), ex);
                return "login gagal tolong hubungi support";
            }
        }

        public async Task<string> Register(string username, string password, MsdetailUser detaildata)
        {
           
                var exists = _db.Msusers.Any(u => u.Username == username);
                if (exists)
                   return ("Username sudah digunakan.");

                var encrypted = AESHelper.Encrypt(password);

                var user = new Msuser
                {
                    Username = username,
                    Passcode = encrypted,
                    Active = true
                };

                _db.Msusers.Add(user);
             await   _db.SaveChangesAsync();
            var detail = new MsdetailUser
            {
                Umur = detaildata.Umur,
                IdUser=user.Id,
                Nama= detaildata.Nama,
                Tanggallahir=detaildata.Tanggallahir
            };
            _db.MsdetailUsers.Add(detail);
            await _db.SaveChangesAsync();
            return "ok";
            }
            
                 
                

             
        
    }
}
