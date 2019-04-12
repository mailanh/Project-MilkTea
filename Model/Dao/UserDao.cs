using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.EF;


namespace Model.Dao
{
    public class UserDao
    {
        private MilkTeaDbContext db = null;
        public UserDao()
        {
            db = new MilkTeaDbContext();
        }
        public int Insert(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return user.ID;
        }
        public User GetByUserName(string userName)
        {
            var result = db.Users.SingleOrDefault(s => s.UserName == userName);
            return result;
        }
        public int Login(string userName, string passWord)
        {

            var result = db.Users.SingleOrDefault(s => s.UserName == userName && s.Password == passWord);
            if (result == null)
            {
                return 0;
            }
            else if (result.Status == false)
            {
                return -1;
            }
            else if (result.Password != passWord)
            {
                return 2;
            }
            else
            {
                return 1;
            }
        }
    }
}
