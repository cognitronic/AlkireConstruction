using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Core.Domain.User;
using RAM.Infrastructure.Querying;
using RAM.Services.Interfaces;
using RAM.Services.Messaging.UserService;
using RAM.Services.Mapping;
using RAM.Services.Cache;
using RAM.Infrastructure.UnitOfWork;

namespace RAM.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly ICacheStorage _cache;
        private readonly IUnitOfWork _uow;

        public UserService(IUserRepository repository, ICacheStorage cache, IUnitOfWork uow)
        {
            _repository = repository;
            _cache = cache;
            _uow = uow;
        }

        #region IUserService Members

        public GetValidUserResponse AuthenticateUser(string email, string password)
        {
            GetValidUserResponse response = new GetValidUserResponse();
            var query = new Query();
            query.Add(new Criterion("Email", email, CriteriaOperator.Equal));
            query.Add(new Criterion("Password", password, CriteriaOperator.Equal));
            var account = _repository.FindBy(query);
            if (account != null && account.Count() > 0)
            {
                response.IsAuthenticated = true;
                response.SelectedUser = account.FirstOrDefault<IUser>();
            }
            return response;
        }

        public GetUserResponse GetUser(GetUserRequest request)
        {
            GetUserResponse response = new GetUserResponse();

            IUser user = _repository.FindByID(request.UserID);

            if (user != null)
            {
                response.UserFound = true;
                response.User = user.ConvertToUserView();
            }
            else
            {
                response.UserFound = false;
            }
            return response;
        }

        private void ThrowExceptionIfUserIsInvalid(User user)
        {
            if (user.GetBrokenRules().Count() > 0)
            {
                StringBuilder brokenRules = new StringBuilder();
                brokenRules.AppendLine("there were problems saving the User: ");
                foreach (var businessRule in user.GetBrokenRules())
                {
                    brokenRules.AppendLine(businessRule.Rule);
                }
                throw new UserInvalidException(brokenRules.ToString());
            }
        }

        #endregion

        public User FindByID(int id)
        {
            return _repository.FindByID(id);
        }

        public User FindByEmail(string email)
        {
            Query query = new Query();
            query.Add(new Criterion("Email", email, CriteriaOperator.Equal));
            return _repository.FindBy(query).FirstOrDefault<User>();
        }

        public IList<User> FindAll()
        {
            return _repository.FindAll();
        }

        public User CreateNewUser(User user)
        {
            if (user != null)
            {
                _repository.Add(user);
                return user;
            }
            return null;
        }

        public User UpdateUser(User user)
        {
            if (user != null)
            {
                _repository.Save(user);
                return user;
            }
            return null;
        }

        public User DeleteUser(User user)
        {
            if (user != null)
            {
                _repository.Remove(user);
                _uow.Commit();
                return user;
            }
            return null;
        }
    }
}
