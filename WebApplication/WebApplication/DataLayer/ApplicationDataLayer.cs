using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using WebApplication.Models;

namespace WebApplication.DataLayer
{
    public class ApplicationDataLayer
    {

        ServiceDeskEntities db = new ServiceDeskEntities();

        //private readonly ServiceDeskEntities db;

        //public ApplicationDataLayer(ServiceDeskEntities context)
        //{
        //    db = context;
        //}

        public Employee GetEmployeeById(int Id)
        {
            return db.Employees.Find(Id);
        }


        public bool CheckUsernameExists(string username)
        {
            try
            {
                var result = (from menu in db.Employees
                              where menu.Email_Id == username
                              select menu).Any();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool CheckGroupNameExists(string grp)
        {
            try
            {
                var result = (from name in db.Groups
                              where name.Group_Name == grp
                              select name).Any();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        

        public Employee GetUserByUsername(string username)
        {
            try
            {
                var result = (from Employee in db.Employees
                              where  Employee.Email_Id == username
                              select Employee).FirstOrDefault();

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Role GetRoleByEmpId(int EmpId)
        {
            try
            {
                var role = (from x in db.Employees
                            where x.Emp_Id == EmpId
                            select x.Role).FirstOrDefault();

                return role;
            }
            catch (Exception)
            {

                throw;
            }
        }






        public string GetPasswordbyUserId(int userId)
        {
            try
            {
                var password = (from pass in db.Employees
                                where pass.Emp_Id == userId
                                select pass.Emp_Password).FirstOrDefault();

                return password;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public Ticket GetTicketByTicketId(int tickid)
        {
            try
            {
                var ticket = (from tick in db.Tickets
                              where tick.Ticket_Id == tickid
                              select tick).FirstOrDefault();
                return ticket;

            }
            catch (Exception)
            {
                throw;
            }
        }
        



    }
}