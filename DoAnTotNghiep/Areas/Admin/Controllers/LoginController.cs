using DoAnTotNghiep.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Model.Dao;
using Model.EF;
using DoAnTotNghiep.Common;

namespace DoAnTotNghiep.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        private MilkTeaDbContext db = new MilkTeaDbContext();
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginModel loginModel)
        {
            Messages messages = new Messages();

            var dao = new UserDao();
            var target = dao.Login(loginModel.UserName, Encryptor.MD5Hash(loginModel.Password));
            if (target == 1)
            {
                var user = dao.GetByUserName(loginModel.UserName);
                var userSession = new UserLogin();
                userSession.UserName = user.UserName;
                userSession.UserID = user.ID;
                Session.Add(CommonConstants.USER_SESSION, userSession);

                messages.Success = true;
                return Content(JsonConvert.SerializeObject(new
                {
                    messages
                }));
            }
            else if (target == 0)
            {
                messages.Success = false;
                messages.Contents = "Tài khoản hoặc mật khẩu không tồn tại";
                return Content(JsonConvert.SerializeObject(new
                {
                    messages
                }));
            }
            else if (target == -1)
            {
                messages.Success = false;
                messages.Contents = "Tài khoản bị khoá, vui lòng liên hệ với Admin.";
                return Content(JsonConvert.SerializeObject(new
                {
                    messages
                }));
            }
            else if (target == 2)
            {
                messages.Success = false;
                messages.Contents = "Mật khẩu không đúng @@";
                return Content(JsonConvert.SerializeObject(new
                {
                    messages
                }));
            }
            else
            {
                messages.Success = false;
                messages.Contents = "Đăng nhập thất bại";
                return Content(JsonConvert.SerializeObject(new
                {
                    messages
                }));
            }
            return View("Index");
        }
    }
}