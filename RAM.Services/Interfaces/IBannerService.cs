using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Services.Messaging.Banner;
using RAM.Core.Domain.Banner;

namespace RAM.Services.Interfaces
{
    public interface IBannerService
    {
        Banner GetByID(int postID);
        GetBannersResponse GetAll();
        GetBannersResponse GetAllForAdmin();
        void SaveBanner(Banner banner);
        void DeleteBanner(Banner banner);
    }
}
