using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface IMainMenuRepository
    {
        MainmenuItemsSection GetMainMenuSection(string language);
    }
}
