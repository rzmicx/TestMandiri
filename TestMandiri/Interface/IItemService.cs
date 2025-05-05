using System.Threading.Tasks;
using TestMandiri.Data.Models;

namespace YourNamespace.Services.Interfaces
{
    public interface IItemService
    {
        string addTransaction(int userid ,List<ItemSell> item );
        Task<List<GridTransaction>> getTransactionsAsync(int take, string? orderby);
    }
}
