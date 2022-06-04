using Meetup.Data.Entities.Base;

namespace Meetup.DAO.Interfaces
{
    public interface IBaseDAO<TModel,TViewModel,TPostModel,TPutModel> 
        where TModel : BaseEntity 
        where TViewModel : class
        where TPostModel : class
        where TPutModel : class
    {
        public Task<List<TViewModel>> GetAllAsync(params string[] includes);
        public Task<TViewModel?> Get(Guid id, params string[] includes);
        public Task<Guid> Post(TPostModel model);
        public Task<bool> Put(TPutModel model);
        public Task<bool> Delete(Guid id);
    }
}
