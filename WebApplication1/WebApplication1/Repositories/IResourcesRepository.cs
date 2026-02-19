using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface IResourcesRepository
    {
        ResourcesSectionModel GetResourcesSection(string language);
    }
}
