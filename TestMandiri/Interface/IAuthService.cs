using System.Threading.Tasks;
using TestMandiri.Data.Models;

namespace YourNamespace.Services.Interfaces
{
    public interface IAuthService
    {
        string Login(string username, string password);
        Task<string> Register(string username, string password,MsdetailUser detaildata);
    }
}
