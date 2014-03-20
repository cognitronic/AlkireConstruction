using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Core.Domain.Banner;
using RAM.Infrastructure.Querying;
using RAM.Services.Interfaces;
using RAM.Services.Messaging.Banner;
using RAM.Services.Mapping;
using RAM.Services.Cache;
using RAM.Infrastructure.UnitOfWork;

namespace RAM.Services.Implementations
{
    public class BannerService : IBannerService
    {
        private readonly IBannerRepository _repository;
        private readonly ICacheStorage _cache;
        private readonly IUnitOfWork _uow;

        public BannerService(IBannerRepository repository, ICacheStorage cache, IUnitOfWork uow)
        {
            _repository = repository;
            _cache = cache;
            _uow = uow;
        }
        public Banner GetByID(int postID)
        {
            var list = new List<IBanner>();
            var query = new Query();
            query.Add(new Criterion("ID", postID, CriteriaOperator.Equal));

            if (_cache.Get<IList<IBanner>>(RAM.Core.ResourceStrings.Cache_Banners) == null)
            {
                return  _repository.FindBy(query).FirstOrDefault<Banner>();
            }
            return _cache.Get<List<Banner>>(RAM.Core.ResourceStrings.Cache_Banners).SingleOrDefault(s => s.ID == postID);
        }

        public GetBannersResponse GetAll()
        {
            var response = new GetBannersResponse();
            var list = new List<IBanner>();
            if (_cache.Get<IList<IBanner>>(RAM.Core.ResourceStrings.Cache_Banners) == null)
            {
                list = _repository.FindAll()
                    .OrderBy(o => o.ID).ToList<IBanner>();
                _cache.Store(RAM.Core.ResourceStrings.Cache_Banners, list);
            }
            else
            {
                list = _cache.Get<List<IBanner>>(RAM.Core.ResourceStrings.Cache_Banners);
            }

            if (list != null)
            {
                response.Success = true;
                response.Message = "Banners Retrieved Successfully!";
                response.BannerList = list.ToList<IBanner>();
            }
            else
            {
                response.Success = false;
                response.Message = "Banners Retrieved Failed!";
            }

            return response;
        }

        public GetBannersResponse GetAllForAdmin()
        {
            throw new NotImplementedException();
        }

        public void SaveBanner(Banner banner)
        {
            _repository.Save(banner);
            _uow.Commit();
            _cache.Remove(RAM.Core.ResourceStrings.Cache_Banners);
        }

        public void DeleteBanner(Banner banner)
        {
            _repository.Remove(banner);
            _uow.Commit();
            _cache.Remove(RAM.Core.ResourceStrings.Cache_Banners);
        }
    }
}
