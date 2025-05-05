using System;
using System.Linq;
using StackExchange.Redis;
using Microsoft.EntityFrameworkCore;
using TestMandiri.Data.Models;
using TestMandiri.Data.Common;
using YourNamespace.Services.Interfaces;
using Microsoft.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.Data;
using Microsoft.AspNetCore.Http.HttpResults;

namespace TestMandiri.Services
{
    public class ItemService : IItemService
    {
        private readonly TestMandiriContext _db;
        private readonly IConfiguration _configuration;

        public ItemService(TestMandiriContext db, IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;
        }


        public string addTransaction(int userid, List<ItemSell> item)
        {
            var fullitem = _db.Msitems.ToList();
            foreach (var items in item)
            {
                var check = fullitem.FirstOrDefault(i => i.Id == items.idItem);
                if (check == null)
                {
                    return "kamu membeli product yang tidak pernah ada , hubungi IT";
                }
                if(check.Qty < items.qty)
                {
                    return "product yang kamu beli sudah habis";
                }
            }
            var user = _db.Msusers.Where(q=>q.Id== userid).FirstOrDefault();
            foreach (var items in item)
            {
            var transaksi = new TrTransaction
            {
                Createat = DateTime.Now,
                Createby = user==null?"kasir":user.Username,
                IdItem= items.idItem,
                Qty = items.qty,
                IdUser=userid
            };
                _db.TrTransactions.Add(transaksi);
                var data = _db.Msitems.Where(q=>q.Id == items.idItem).FirstOrDefault();
                data.Qty=data.Qty-items.qty;
                _db.SaveChanges();
            }
            return "ok";
        }


        public async Task<List<GridTransaction>> getTransactionsAsync(int take,string? orderby)
        {
            try
            {
                var connectionString = _configuration.GetConnectionString("DefaultConnection");
                using var connection = new SqlConnection(connectionString);

                var result = await connection.QueryAsync<GridTransaction>(
                    "sp_GridData",
                    new { take, orderby },
                    commandType: CommandType.StoredProcedure
                );

                return result.ToList();
            }
            catch (Exception ex)
            {
                LoggerHelper.LogError(nameof(getTransactionsAsync), ex);
                throw new Exception("tolong hubungin administrator");
            }
        }






    }
}
