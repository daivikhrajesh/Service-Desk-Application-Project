using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.ViewModel;
using WebApplication.DataLayer;

namespace WebApplication.Controllers
{
    public class LoginController : Controller
    {
        readonly ApplicationDataLayer ad = new ApplicationDataLayer();


        [HttpGet]
        public ActionResult Login()
        {
            return View();       
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel loginViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    
                    if (!ad.CheckUsernameExists(loginViewModel.Username))
                    {
                        TempData["LoginErrors"] = "Username Doesn't Exists, Please Contact Admin";
                        return View(loginViewModel);
                    }
                    var emp = ad.GetUserByUsername(loginViewModel.Username);
                    if (emp != null)
                    {
                        var storedpassword = ad.GetPasswordbyUserId(emp.Emp_Id);
                        if (storedpassword == null)
                        {
                            TempData["LoginErrors"] = "Password Doesn't Exists, Please Contact Admin";
                            return View(loginViewModel);
                        }

                        if (string.Equals(storedpassword, loginViewModel.Password, StringComparison.Ordinal))
                        {
                            Session["EmpId"] = emp.Emp_Id;
                            Session["EmpName"] = emp.Emp_Name;
                            Session["EmailId"] = emp.Email_Id;
                            Session["DepName"] = emp.Department.Dept_Name;
                            Session["Role"] = emp.Role.Role_Name;
                            Session["GrpName"] = emp.Group.Group_Name;
                            Session["GrpId"] = emp.Group.Group_Id;
                            Session["DepId"] = emp.Department.Dept_Id;

                            if (ad.GetRoleByEmpId(emp.Emp_Id).Role_Id == 1)
                            {
                                return RedirectToAction("HomePage", "Admin");
                            }
                            if(ad.GetRoleByEmpId(emp.Emp_Id).Role_Id == 2)
                            {
                                return RedirectToAction("HomePage", "Manager");
                            }
                            if (ad.GetRoleByEmpId(emp.Emp_Id).Role_Id == 3)
                            {
                                return RedirectToAction("HomePage", "Lead");
                            }
                            if (ad.GetRoleByEmpId(emp.Emp_Id).Role_Id == 4)
                            {
                                return RedirectToAction("HomePage", "Employee");
                            }
                            

                        }
                        else
                        {
                            TempData["LoginErrors"] = "Please Enter Correct Password";
                            return View(loginViewModel);
                        }
                    }
                    
                }
                return View(loginViewModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }

   

}

