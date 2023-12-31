﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.DataLayer;
using WebApplication.Models;
using WebApplication.ViewModel;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace WebApplication.Controllers
{
    public class AdminController : Controller
    {
        readonly ServiceDeskEntities db = new ServiceDeskEntities();
        readonly ApplicationDataLayer ad = new ApplicationDataLayer();

        
        
        public ActionResult HomePage()
        {

            return View();
        }

        public ActionResult CreateTicket(TicketViewModel ticket)
        {
            string val = Session["EmpId"].ToString();
            
            Ticket Tick = new Ticket();
            ModelState.Clear();
            Tick.Ticket_Desc = ticket.Ticket_Desc;
            Tick.Ticket_Status = "Un Resolved";
            Tick.Ticket_Priority = ticket.Ticket_Priority;
            var emp = (from x in db.Employees
                       where x.Emp_Id.ToString() == val
                       select x).FirstOrDefault();
            Tick.Ticket_By = emp;
            db.Tickets.Add(Tick);
            if (Tick.Ticket_Desc != null )
            {
                db.SaveChanges();
            }
            return View();
        }

        public ActionResult AllTicket()
        {

            var data = db.Tickets.ToList();

            return View(data);

        }


        public ActionResult MyTicket()
        {
            
            var email = Session["EmailId"].ToString();
            var data = db.Tickets.Where(x=>x.Ticket_By.Email_Id == email).ToList();
            var db1 = new ServiceDeskEntities();
            var response = db1.Responses.ToList();
            var res = db1.Responses.ToList<Response>();
            TempData["ResponseData"] = res;
            return View(data);

        }


        [HttpGet]
        public ActionResult UpdateTicket(int tktid)
        {
            var db1 = new ServiceDeskEntities();
            var response = db1.Responses.ToList();
            TicketViewModel tkt = new TicketViewModel();
            var data = db.Tickets.Where(x => x.Ticket_Id == tktid).SingleOrDefault();
            var res = db1.Responses.Where(x => x.Ticket_Id == tktid).Select(x => x.Response_Msg).FirstOrDefault();
            tkt.Ticket_Desc = data.Ticket_Desc;
            tkt.Ticket_Status = data.Ticket_Status;
            tkt.Ticket_Priority = data.Ticket_Priority;
            ViewData["ResponseData"] = res;
            return View(tkt);

        }


        [HttpPost]
        public ActionResult UpdateTicket(int tktid,TicketViewModel model)
        {

            var data = db.Tickets.FirstOrDefault(x => x.Ticket_Id == tktid);
            if (data != null)
            {
                data.Ticket_Status = model.Ticket_Status;
                data.Ticket_Desc = model.Ticket_Desc;
                db.SaveChanges();
                return RedirectToAction("MyTicket");
            }
            else
                return View();

        }




        [HttpGet]
        public ActionResult Department()
        {
            var depnames = new SelectList(db.Departments.ToList().Select(x => x.Dept_Name));
            TempData["depnames"] = depnames;
            return View();
        }

        [HttpPost]
        public ActionResult Department(DepartmentViewModel dep)
        {

            return RedirectToAction("DepartmentTicket", Session["Role"].ToString(),  new { depname = dep.Dept_Name });
        }

        [HttpGet]
        public ActionResult DepartmentTicket(string depname)
        {
            TempData["depname_tickets"] = depname;
            var data = db.Tickets.Where(x => x.Ticket_By.Department.Dept_Name == depname).ToList();
            return View(data);

        }



        [HttpGet]
        public ActionResult Group()
        {
            var grpnames = new SelectList(db.Groups.ToList().Select(x=>x.Group_Name));
            TempData["grpnames"] = grpnames;
            return View();
        }

        [HttpPost]
        public ActionResult Group(GroupViewModel grp)
        {
            if (ad.CheckGroupNameExists(grp.Group_Name))
            {
                return RedirectToAction("GroupTicket", Session["Role"].ToString(), new { grpname = grp.Group_Name });
            }
            else
            {
                TempData["grpname_error"] = "Entered Group Name does not Exists";
            }
            return View();
        }

        [HttpGet]
        public ActionResult GroupTicket(string grpname)
        {
            TempData["grpname_tickets"] = grpname;
            var data = db.Tickets.Where(x => x.Ticket_By.Group.Group_Name == grpname).ToList();
            return View(data);

        }


        [HttpGet]
        public ActionResult AssignTicket(int tktid)
        {
            
            var assign = db.Assign_Tickets.Where(x => x.Ticket_Id == tktid).FirstOrDefault();
            var email = Session["EmailId"].ToString();
            var assigntkt = new AssignTicketViewModel();
            if(assign != null)
            {
                assigntkt.Assign_Id = assign.Assign_Id;
                assigntkt.Group_Id = assign.Group_Id;
                assigntkt.Dept_Id = assign.Dept_Id;
                assigntkt.Assigned_By = email;
                assigntkt.Assign_Status = assign.Assign_Status;
            }
            assigntkt.Ticket_Id = tktid;
            return View(assigntkt);
        }

        [HttpPost]
        public ActionResult AssignTicket(int tktid,AssignTicketViewModel assigntkt)
        {
            var emp = db.Tickets.Where(x => x.Ticket_Id == tktid).FirstOrDefault();
            var email = Session["EmailId"].ToString();

            if (db.Assign_Tickets.Where(x => x.Ticket_Id == tktid).Any())
            {
                var assign = db.Assign_Tickets.Where(x => x.Ticket_Id == tktid).FirstOrDefault();
                if(assign != null)
                {
                    assign.Ticket_Id = tktid;
                    assign.Assigned_By = email;
                    assign.Assigned_To = assigntkt.Assigned_To;
                    assign.Assign_Status = "Assigned";
                    assign.Dept_Id = emp.Ticket_By.Department.Dept_Id;
                    assign.Group_Id = emp.Ticket_By.Group.Group_Id;
                    db.SaveChanges();
                }
            }
            else
            {
                var a = new Assign_Ticket();
                a.Ticket_Id = tktid;
                a.Assigned_By = email;
                a.Assigned_To = assigntkt.Assigned_To;
                a.Dept_Id = emp.Ticket_By.Department.Dept_Id;
                a.Group_Id = emp.Ticket_By.Group.Group_Id;
                a.Assign_Status = "Assigned";
                db.Assign_Tickets.Add(a);
                db.SaveChanges();
            }

            


            return View();
        }


        public ActionResult AssignedTicket()
        {
            var email = Session["EmailId"].ToString();
            var data = db.Assign_Tickets.Where(x => x.Assigned_To == email).ToList();
            return View(data);
        }

       
        public ActionResult RespondTicket(int tkt,ResponseViewModel model)
        {
            
            var ticket = db.Tickets.Where(x => x.Ticket_Id == tkt).Select(x => x.Ticket_Desc).FirstOrDefault();
            TempData["TicketDes"] = ticket;
            var email = Session["EmailId"].ToString();
            
            
                var data = db.Responses.Where(x => x.Ticket_Id == tkt).FirstOrDefault();
                if(data != null)
                {
                    data.Ticket_Id = tkt;
                    data.Response_Date = DateTime.Now;
                    data.Response_Msg = model.Response_Msg;
                    data.Response_status = model.Response_status;
                    if (data.Response_status == null || data.Response_Msg == null)
                    {
                        return View();

                    }
                    db.SaveChanges();
                    return View();
                }
            
            
                var res = new Response();
                res.Response_Date = DateTime.Now;
                res.Response_Msg = model.Response_Msg;
                res.Response_status = model.Response_status;
                res.Ticket_Id = tkt;
                if (!db.Responses.Where(x => x.Ticket_Id == tkt).Any())
                {
                    db.Responses.Add(res);
                } 
                db.SaveChanges();
                return View();
            

           
        }

     
        public ActionResult AddEmployee(EmployeeViewModel emp)
        {
            var grpnames = new SelectList(db.Groups.ToList().Select(x => x.Group_Name));
            var depnames = new SelectList(db.Departments.ToList().Select(x => x.Dept_Name));
            TempData["grpnames"] = grpnames;
            TempData["depnames"] = depnames;
            var addemp = new Employee();
            ModelState.Clear();
            addemp.Emp_Name = emp.Emp_Name;
            addemp.Email_Id = emp.Email_Id;
            addemp.Emp_Password = emp.Emp_Password;
            addemp.Emp_Phone = emp.Emp_Phone;
            var dep = (from x in db.Departments
                       where x.Dept_Name == emp.Department_Name
                       select x).FirstOrDefault();
            addemp.Department = dep;
            var grp = (from x in db.Groups
                       where emp.Group_Name == x.Group_Name
                       select x).FirstOrDefault();
            addemp.Group = grp;
            var role = (from x in db.Roles
                        where emp.Role_Name == x.Role_Name
                        select x).FirstOrDefault();
            addemp.Role = role;
            db.Employees.Add(addemp);

            if (emp.Email_Id != null && emp.Emp_Password != null)
            {
                if (emp.Department_Name != null && emp.Group_Name != null)
                {
                    db.SaveChanges();
                }
            }
               


            return View();
        }

        public ActionResult ProfileDetail()
        {
            var id = (int)Session["EmpId"];
            var data = (from x in db.Employees
                        where x.Emp_Id == id
                        select x).FirstOrDefault();
;            return View(data);
        }

        public ActionResult UpdateProfile(int empid)
        {

            var grpnames = new SelectList(db.Groups.ToList().Select(x => x.Group_Name));
            TempData["grpnames"] = grpnames;
            EmployeeViewModel emp = new EmployeeViewModel();
            var data = db.Employees.Where(x => x.Emp_Id == empid).SingleOrDefault();
            emp.Emp_Id = data.Emp_Id;
            emp.Emp_Name = data.Emp_Name;
            emp.Email_Id = data.Email_Id;
            emp.Emp_Password = data.Emp_Password;
            emp.Emp_Phone = data.Emp_Phone;
            string dep;
            dep = (from x in db.Departments
                   where x.Dept_Name == data.Department.Dept_Name
                   select x.Dept_Name).SingleOrDefault();
            emp.Department_Name = dep;
            emp.Role_Name = data.Role.Role_Name;
            emp.Group_Name = data.Group.Group_Name;
            return View(emp);
        }


        [HttpPost]

        public ActionResult UpdateProfile(int empid, EmployeeViewModel model)
        {
            var data = db.Employees.FirstOrDefault(x => x.Emp_Id == empid);
            if (data != null)
            {
                data.Emp_Name = model.Emp_Name;
                data.Emp_Password = model.Emp_Password;
                data.Emp_Phone = model.Emp_Phone;
                var Dep = (from x in db.Departments
                           where x.Dept_Name == model.Department_Name
                           select x).FirstOrDefault();
                data.Department = Dep;
                var grp = (from x in db.Groups
                           where x.Group_Id == model.Group_Id
                           select x).FirstOrDefault();
                data.Group = grp;
                var role = db.Roles.Where(x => x.Role_Name == model.Role_Name).FirstOrDefault();
                data.Role = role;
                db.SaveChanges();
                return View();
            }
            else
                return View();
        }

        [HttpGet]
        public ActionResult Read()
        {
            var data = db.Employees.ToList(); 
            return View(data);
        }

        public ActionResult Update(int empid)
        {
            var grpnames = new SelectList(db.Groups.ToList().Select(x => x.Group_Name));
            TempData["grpnames"] = grpnames;
            EmployeeViewModel emp = new EmployeeViewModel();
            var data = db.Employees.Where(x => x.Emp_Id == empid).SingleOrDefault();
            emp.Emp_Id = data.Emp_Id;
            emp.Emp_Name = data.Emp_Name;
            emp.Email_Id = data.Email_Id;
            emp.Emp_Password = data.Emp_Password;
            emp.Emp_Phone = data.Emp_Phone;
            string dep;
            dep = (from x in db.Departments
                   where x.Dept_Name == data.Department.Dept_Name
                   select x.Dept_Name).SingleOrDefault();
            emp.Department_Name = dep;
            emp.Role_Name = data.Role.Role_Name;
            emp.Group_Name = data.Group.Group_Name;
            return View(emp);
        }


        [HttpPost]

        public ActionResult Update(int empid, EmployeeViewModel model)
        {
            var data = db.Employees.FirstOrDefault(x => x.Emp_Id == empid); 
            if (data != null) 
            {
                data.Emp_Name = model.Emp_Name;
                data.Email_Id = model.Email_Id;
                data.Emp_Password = model.Emp_Password;
                data.Emp_Phone = model.Emp_Phone;
                var Dep = (from x in db.Departments
                           where x.Dept_Name == model.Department_Name
                           select x).FirstOrDefault();
                data.Department = Dep;
                var grp = (from x in db.Groups
                           where x.Group_Id == model.Group_Id
                           select x).FirstOrDefault();
                data.Group = grp;
                var role = db.Roles.Where(x => x.Role_Name == model.Role_Name).FirstOrDefault();
                data.Role = role;
                db.SaveChanges();
                return RedirectToAction("Read");
            }
            else
                return View();
        }

        public ActionResult Delete()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int empid)
        {
            var data = db.Employees.FirstOrDefault(x => x.Emp_Id == empid);
            if (data != null)
            {
                db.Employees.Remove(data);
                db.SaveChanges();
                return RedirectToAction("Read");
            }
            else
                return View();

        }


        public ActionResult Logout()
        {
            Session.Clear();
            Session.RemoveAll();
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }
    }
}