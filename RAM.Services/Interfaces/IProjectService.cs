using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RAM.Services.Messaging.Project;
using RAM.Core.Domain.Project;

namespace RAM.Services.Interfaces
{
    public interface IProjectService
    {
        GetProjectByTitleResponse GetByTitle(GetProjectByTitleRequest request);
        GetProjectsResponse GetByCategory(GetProjectsByCategoryRequest request);
        Project GetByID(int postID);
        GetProjectsResponse GetAll();
        GetProjectsResponse GetAllForAdmin();
        void SavePost(Project post);
        void DeletePost(Project post);

        IList<IProjectImage> GetImagesByProjectID(int projectID);
        IProjectImage GetImageByID(int imageID);
        void SaveImage(ProjectImage image);
        void DeleteImage(ProjectImage image);

    }
}
