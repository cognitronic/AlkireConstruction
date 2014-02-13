using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Core.Domain.User;
using RAM.Infrastructure.Authentication;
using RAM.Infrastructure.Domain;
using StructureMap;
using RAM.Services.ViewModels;
using RAM.Services.Interfaces;
using RAM.Services.Messaging.Authentication;
using RAM.Services.Cache;
using RAM.Infrastructure.UnitOfWork;
using RAM.Infrastructure.Querying;

namespace RAM.Services.Implementations
{
    public class HRRMarketingAuthenticationService : IHRRMarketingAuthenticationService
    {
        private readonly IUserRepository _repository;
        private readonly ICacheStorage _cache;
        private readonly IUnitOfWork _uow;

        public HRRMarketingAuthenticationService(IUserRepository repository, ICacheStorage cache, IUnitOfWork uow)
        {
            _repository = repository;
            _cache = cache;
            _uow = uow;
        }

        public GetUserResponse AuthenticateUser(GetUserRequest request)
        {
            GetUserResponse response = new GetUserResponse();
            var query = new Query();
            query.Add(new Criterion("Email", request.Email, CriteriaOperator.Equal));
            query.Add(new Criterion("Password", request.Password, CriteriaOperator.Equal));
            var user = _repository.FindBy(query);
            if (user != null && user.Count() > 0)
                response.User = user.FirstOrDefault<IUser>();
            return response;
        }

        #region ILocalAuthenticationService Members

        public IUserAccount Login(string email, string password)
        {
            var service = new UserService(_repository, _cache);
            var response = service.AuthenticateUser(email, password);

            UserView userAccount = new UserView();
            userAccount.IsAuthenticated = false;

            //var user = _repository.FindBy(email);
            if (response.SelectedUser != null && password == response.SelectedUser.Password)
            {
                userAccount.IsAuthenticated = true;
                userAccount.Email = response.SelectedUser.Email;
                userAccount.AuthenticationToken = response.SelectedUser.ID.ToString();
                userAccount.FirstName = response.SelectedUser.FirstName;
                userAccount.LastName = response.SelectedUser.LastName;
                userAccount.Type = response.SelectedUser.Type;
            }
            return userAccount;
        }

        public IUserAccount RegisterUser(string email, string password)
        {
            throw new NotImplementedException();
        }

        #endregion



    }
}
