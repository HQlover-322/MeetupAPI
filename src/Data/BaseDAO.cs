using AutoMapper;
using Meetup.DAO.Interfaces;
using Meetup.Data;
using Meetup.Data.Entities.Base;
using Microsoft.EntityFrameworkCore;

namespace Meetup.DAO
{
    public class BaseDAO<TModel, TViewModel, TPostModel, TPutModel> : IBaseDAO<TModel, TViewModel, TPostModel, TPutModel>
        where TModel : BaseEntity
        where TViewModel : class
        where TPostModel : class
        where TPutModel : class
    {
        private readonly EfDbContex _dbContex;
        private readonly IMapper _mapper;
        public BaseDAO(EfDbContex dbContex, IMapper mapper)
        {
            _dbContex = dbContex;
            _mapper = mapper;
        }

        public async Task<Guid> Post(TPostModel model)
        {
            try
            {
                var post = _mapper.Map<TModel>(model);
                await _dbContex.Set<TModel>().AddAsync(post);
                _dbContex.SaveChanges();
                return post.Id;
            }
            catch
            {
                throw new DAOExeption(DAOErrorType.WrongEntity);
            }
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                var item = await _dbContex.Set<TModel>().FindAsync(id);
                if (item is not null)
                {
                    _dbContex.Set<TModel>().Remove(item);
                    _dbContex.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                throw new DAOExeption(DAOErrorType.WrongEntity);
            }
        }

        public async Task<TViewModel?> Get(Guid id, params string[] includes)
        {
            var data = _dbContex.Set<TModel>().AsQueryable();
            data = includes.Aggregate(data, (current, include) => current.Include(include));
            var item = await data.AsNoTracking().FirstOrDefaultAsync(x=>x.Id==id);
              return _mapper.Map<TViewModel>(item);
        }

        public async Task<List<TViewModel>> GetAllAsync(params string[] includes)
        {
            var data = _dbContex.Set<TModel>().AsQueryable();
            data = includes.Aggregate(data, (current, include) => current.Include(include));
            var items = await data.AsNoTracking().ToListAsync();
            return _mapper.Map<List<TViewModel>>(items);
        }

        public async Task<bool> Put(TPutModel model)
        {
            try
            {
                if (model != null)
                {
                    _dbContex.Set<TModel>().Update(_mapper.Map<TModel>(model));
                    _dbContex.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                throw new DAOExeption(DAOErrorType.WrongEntity);
            }
        }
    }
}
