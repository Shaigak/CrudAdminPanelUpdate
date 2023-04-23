using EntityFrameWork.Models;

namespace EntityFrameWork.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> GetAll();
   
    }



}
