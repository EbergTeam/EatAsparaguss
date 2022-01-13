using EatAsparagus.Data;
using EatAsparagus.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EatAsparagus.Controllers
{
    public class HomeController : Controller
    {
        // Отображение формы "Съесть спаржу"
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        // Добавление информации в БД по нажатию кнопки с формы "Съесть спаржу"
        [HttpPost]
        public IActionResult Index(string name, string email)
        {
            // Проверка ввода null
            if ((name == null) || (email == null))
                return View(true);
            
            // Изменение/Добавление инфы в БД
            using (UsersDbContext db = new UsersDbContext())
            {
                // Проверка на существование пользователя в базе по email
                User checkUser = db.Users.Where(w => w.Email == email).FirstOrDefault();                            
                if (checkUser != null) // Если есть в базе, то изменияем текущее значение EatCount
                {
                    checkUser.EatCount++;
                    checkUser.EatDate = DateTime.Now;
                }
                else // если нет в базе, то добавляем новую запись в БД
                {
                    db.Add(new User() { Name = name, Email = email, EatCount = 1, EatDate = DateTime.Now});
                }                                  
                db.SaveChanges(); // сохраняем изменения в БД
            }
            return Redirect("~/Home/News"); // открываем форму "Лента"
        }

        // Очистка БД от всех записей
        public IActionResult ClearDb()
        {
            using (UsersDbContext db = new UsersDbContext())
            {
                foreach (var users in db.Users)
                {
                    db.Remove(users);
                }
                db.SaveChanges();
            }
            return Redirect("~/Home/News");
        }

        // Передаем все записи с БД в View для отображения
        List<User> users = new List<User>();
        public IActionResult News()
        {         
            using (UsersDbContext db = new UsersDbContext())
            {
                foreach (User user in db.Users)
                {
                    users.Add(user);
                }
            }
            return View(users);
        }
    }
}
