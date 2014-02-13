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

namespace RAM.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly ICacheStorage _cache;

        public UserService(IUserRepository repository, ICacheStorage cache)
        {
            _repository = repository;
            _cache = cache;
        }

        #region IUserService Members

        //public GetAllUsersByTypeResponse GetAllUsers()
        //{
        //    GetAllUsersByTypeResponse response = new GetAllUsersByTypeResponse();
        //    var accounts = _repository.FindAll();
        //    response.Users = accounts;
        //    return response;
        //}

        //public GetValidUserResponse GetUserByEmail(string email)
        //{
        //    GetValidUserResponse response = new GetValidUserResponse();

        //    Query query = new Query();
        //    query.Add(new Criterion("email", email, CriteriaOperator.Equal));
        //    var account = _repository.FindBy(query);
        //    if (account != null && account.Count() > 0)
        //        response.SelectedUser = account.FirstOrDefault<IUser>();
        //    return response;
        //}

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

        //public CreateUserResponse CreateUser(CreateUserRequest request)
        //{
        //    CreateUserResponse response = new CreateUserResponse();
        //    User user = new User();
        //    user.ID = request.UserID;
        //    user.Email = request.Email;
        //    user.FirstName = request.FirstName;
        //    user.LastName = request.LastName;

        //    ThrowExceptionIfUserIsInvalid(user);

        //    _repository.Add(user);

        //    response.User = user.ConvertToUserView();

        //    return response;
        //}

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
    }
}
