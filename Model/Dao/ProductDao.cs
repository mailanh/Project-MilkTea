using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using Model.EF;

namespace Model.Dao
{
    public class ProductDao
    {
        private MilkTeaDbContext db = null;
        public ProductDao()
        {
            db = new MilkTeaDbContext();
        }

        public Product GetById(int ID)
        {
            var result = db.Products.FirstOrDefault(x =>x.ProductID ==ID && x.Status == true);
            return result;
        }
        public List<Product> GetListAllProduct()
        {
            var result = db.Products
                           .Where(x => x.Status == true)
                           .Include(x => x.Supplier)
                           .ToList();
            return result;
        }
    }
}
