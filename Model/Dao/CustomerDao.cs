using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;

namespace Model.Dao
{
    public class CustomerDao
    {
        private MilkTeaDbContext db = null;
        public CustomerDao()
        {
            db = new MilkTeaDbContext();
        }

        public void InsertCustomer(Customer customer)
        {
            db.Customers.Add(customer);
            db.SaveChanges();
            //return true;
        }
        public Customer GetCustomerByID(int customerID)
        {
            var result = db.Customers.SingleOrDefault(s => s.CustomerID == customerID);
            return result;
        }
    }
}
