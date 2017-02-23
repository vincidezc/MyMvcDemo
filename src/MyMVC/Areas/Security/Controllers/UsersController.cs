using MyMvc.Areas.Security.Models;
using MyMvc.Dal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace MyMvc.Areas.Security.Controllers
{
    public class UsersController : Controller
    {
        public UsersController()
        {
            var genders = new List<SelectListItem> {
                new SelectListItem
                {
                    Value = "Male",
                    Text = "Male"
                },
                new SelectListItem
                {
                    Value = "Female",
                    Text = "Female"
                }
            };
            ViewBag.Genders = genders;
        }
        // GET: Security/Users
        public ActionResult Index()
        {
            using (var db = new DatabaseContext())
            {
                var users = (from item in db.Users
                             select new UserViewModel
                     {
                         Guid = item.Guid,
                         Firstname = item.FirstName,
                         LastName = item.LastName,
                         Age = item.Age,
                         Gender = item.Gender,
                         Educations = item.Educations.Select(s => 
                             new EducationViewModel 
                             { 
                                 School = s.School, 
                                 YearAttended = s.YearAttended
                             }).ToList()
                     }).ToList();

                return View(users);
            }
        }

        // GET: Security/Users/Details/5
        public ActionResult Details(Guid id)
        {
            return View(GetUser(id));
        }

        // GET: Security/Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Security/Users/Create
        [HttpPost]
        public ActionResult Create(UserViewModel viewModel)
        {
            try
            {
                if (ModelState.IsValid == false)
                    return View();

                using (var db = new DatabaseContext())
                {
                    var newUser = new User
                    {
                        Guid = Guid.NewGuid(),
                        FirstName = viewModel.Firstname,
                        LastName = viewModel.LastName,
                        Age = viewModel.Age,
                        Gender = viewModel.Gender,
                        EmploymentDate = viewModel.EmploymentDate
                    };

                    foreach (var edu in viewModel.Educations)
                    {
                        newUser.Educations.Add(new Education
                        {
                            School = edu.School,
                            YearAttended = edu.YearAttended
                        });
                    }

                    db.Users.Add(newUser);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Security/Users/Edit/5
        public ActionResult Edit(Guid id)
        {
            return View(GetUser(id));
        }

        // POST: Security/Users/Edit/5
        [HttpPost]
        public ActionResult Edit(Guid id, UserViewModel viewModel)
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.Guid == id);
                    user.FirstName = viewModel.Firstname;
                    user.LastName = viewModel.LastName;
                    user.Age = viewModel.Age;
                    user.Gender = viewModel.Gender;

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Security/Users/Delete/5
        public ActionResult Delete(Guid id)
        {
            return View(GetUser(id));
        }

        // POST: Security/Users/Delete/5
        [HttpPost]
        public ActionResult Delete(Guid id, FormCollection collection)
        {
            try
            {
                using (var db = new DatabaseContext())
                {
                    var user = db.Users.FirstOrDefault(u => u.Guid == id);
                    db.Users.Remove(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        private UserViewModel GetUser(Guid id)
        {
            using (var db = new DatabaseContext())
            {
                return (from user in db.Users
                        where user.Guid == id
                        select new UserViewModel
                        {
                            Guid = user.Guid,
                            Firstname = user.FirstName,
                            LastName = user.LastName,
                            Age = user.Age,
                            Gender = user.Gender
                        }).FirstOrDefault();

                //db.Users.Select(u => new UserViewModel { 
                //    Id = u.Id
                //}).FirstOrDefault(x => x.Id == id);
            }

        }
    }
}
