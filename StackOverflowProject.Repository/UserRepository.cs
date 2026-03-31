using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using StackOverflowProject.DomainModel;

namespace StackOverflowProject.Repository
{
    public interface IUsersRepository
    {
        void AddUser(User u);
        void UpdateUserDetails(User u);
        void UpdateUserPassword(User u);

        void DeleteUser(int uid);

        List<User> GetUsers();

        List<User> GetUsersByEmailandPassword(String Email,string password);

        List<User> GetUsersByEmail(String Email);

        List<User> GetUsersByUserID(int UserID);

        int GetLatestUserID();
    }
    public class UserRepository:IUsersRepository
    {
        StackOverflowDatabaseDbcontext db;

        public UserRepository()
        {
            db = new StackOverflowDatabaseDbcontext();
        }

        public void AddUser(User u)
        {
            db.users.Add(u);
            db.SaveChanges();
        }

        public void UpdateUserDetails(User u)
        {
            User us= db.users.Where(temp=>temp.UserID==u.UserID).FirstOrDefault();
            if (us!=null) 
                {
                  us.Name = u.Name;
                  us.Mobile = u.Mobile;
                  db.SaveChanges();
                }
        }

        public void UpdateUserPassword(User u)
        {
           User P=db.users.Where(temp=>temp.UserID==u.UserID).FirstOrDefault();
            if(P!=null)
            {
                P.PasswordHash = u.PasswordHash;
            }
        }

        public void DeleteUser(int uid)
        {
            User us = db.users.Where(temp => temp.UserID == uid).FirstOrDefault();
            if (us != null)
            {
                db.users.Remove(us);
                db.SaveChanges();
            }
        }

        public List<User> GetUsers()
        {
            List<User> us=db.users.Where(temp=>temp.IsAdmin == false).OrderBy(temp=>temp.Name).ToList();
            return us;
        }

        public List<User> GetUsersByEmailandPassword(String Email,String PasswordHash)
        {
            List<User> us = db.users.Where(temp => temp.Email ==Email && temp.PasswordHash==PasswordHash ).ToList();
            return us;
        }

        public List<User> GetUsersByEmail(String Email)
        {
            List<User> us = db.users.Where(temp => temp.Email == Email ).ToList();
            return us;
        }
        public List<User> GetUsersByUserID(int UserID)
        {
            List<User> us = db.users.Where(temp => temp.UserID == UserID).ToList();
            return us;
        }

        public int GetLatestUserID()
        {
            int uid = db.users.Select(temp => temp.UserID ).Max();
            return uid;
        }

       
    }
}
