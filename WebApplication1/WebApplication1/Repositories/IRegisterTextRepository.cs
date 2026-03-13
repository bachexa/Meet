using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public interface IRegisterTextRepository
    {
        RegisterTextModel? GetRegisterText(string language);
    }
}
