using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackOverflowProject.DomainModel;
using StackOverflowProject.ViewModel;
using StackOverflowProject.Repository;
using AutoMapper;
using AutoMapper.Configuration;

namespace StackOverflowProject.ServiceLayer
{

    public interface IUserService
    {
        int AddUser(RegisterViewModel uvm);
        void UpdateUserdetails(EditUserDetailsViewModel uvm);
        void UpdateUserPassword(EditUserPasswordViewModel uvm);
        void DeleteUser(int uid);
        List<UserViewModel> GetUsers();
        UserViewModel GetUserByEmailAndPassword(string Email, string Password);
        UserViewModel GetUserByEmail(string Email);

        UserViewModel GetUserByUserID(int UserID);
    }
    public class UserService:IUserService
    {
        IUsersRepository ur;

        public UserService()
        {
            ur=new UserRepository();
        }

        public int AddUser(RegisterViewModel uvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<RegisterViewModel, User>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            User u = mapper.Map<RegisterViewModel, User>(uvm);
            u.PasswordHash = SHA256HashGenerator.GenerateHash(uvm.Password);
            ur.AddUser(u);
            int uid = ur.GetLatestUserID();
            return uid;
        }

        public void UpdateUserdetails(EditUserDetailsViewModel uvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditUserDetailsViewModel, User>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            User u = Mapper.Map<EditUserDetailsViewModel, User>(uvm);
            ur.UpdateUserDetails(u);
        }

        public void UpdateUserPassword(EditUserPasswordViewModel uvm)
        {
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<EditUserPasswordViewModel, User>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            User u = Mapper.Map<EditUserPasswordViewModel, User>(uvm);
            u.PasswordHash=SHA256HashGenerator.GenerateHash(uvm.Password);
            ur.UpdateUserPassword(u);
        }

        public void DeleteUser(int uid)
        {
            ur.DeleteUser(uid); 

        }

        public List<UserViewModel> GetUsers()
        {
            List<User> u = ur.GetUsers();
            var config = new MapperConfiguration(cfg => { cfg.CreateMap<UserViewModel, User>(); cfg.IgnoreUnmapped(); });
            IMapper mapper = config.CreateMapper();
            List<UserViewModel> uvm = mapper.Map<List<User>,List<UserViewModel>>(u);
            return uvm;
        }

        public UserViewModel GetUserByEmailAndPassword(string Email, string Password)
        {
            User u = ur.GetUsersByEmailandPassword(Email, SHA256HashGenerator.GenerateHash(Password)).FirstOrDefault();
            UserViewModel uvm=null;
            if(u!=null)
            {

                var config = new MapperConfiguration(cfg => { cfg.CreateMap<UserViewModel, User>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                uvm = mapper.Map<User, UserViewModel>(u);
            }
            return uvm;
        }

        public UserViewModel GetUserByEmail(string Email)
        {
            User u = ur.GetUsersByEmail(Email).FirstOrDefault();
            UserViewModel uvm = null;
            if (u != null)
            {

                var config = new MapperConfiguration(cfg => { cfg.CreateMap<UserViewModel, User>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                uvm = mapper.Map<User, UserViewModel>(u);
            }
            return uvm;
        }

        public UserViewModel GetUserByUserID(int UserID)
        {
            User u = ur.GetUsersByUserID(UserID).FirstOrDefault();
            UserViewModel uvm = null;
            if (u != null)
            {

                var config = new MapperConfiguration(cfg => { cfg.CreateMap<UserViewModel, User>(); cfg.IgnoreUnmapped(); });
                IMapper mapper = config.CreateMapper();
                uvm = mapper.Map<User, UserViewModel>(u);
            }
            return uvm;
        }



    }
}
