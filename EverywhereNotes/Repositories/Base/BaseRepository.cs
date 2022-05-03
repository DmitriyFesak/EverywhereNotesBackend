using EverywhereNotes.Models.Entities;

namespace EverywhereNotes.Repositories.Base
{
    public abstract class BaseRepository
    {
        public abstract Task AddAsync(IEntity entity);

        public abstract Task DeleteAsync(long id);

        public abstract Task<List<IEntity>> GetAllAsync();

        public abstract Task<IEntity> GetByIdAsync(long id);

        public abstract IQueryable<IEntity> GetQueryableNoTracking();

        public abstract Task<bool> IsEntityExistAsync(long id);

        public abstract Task UpdateAsync(IEntity entity);
    }
}
