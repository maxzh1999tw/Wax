using Wax.Domain.Users;
using Wax.Domain.Utilities;
using Wax.Domain.Utilities.Records;
using Wax.Domain.Utilities.Repositories;

namespace Wax.Infra.Memory.Utilities
{
    public class ListRepository<T> : IRepository<T> where T : IEntity
    {
        private int _nextId;

        protected static List<T> EntityList { get; private set; } = new();
        protected IUserIdentity User { get; init; }

        public ListRepository(IUserIdentity user)
        {
            User = user;
        }

        public Task DeleteAsync(int id)
        {
            var removed = EntityList.RemoveAll(x => x.Id == id);
            if (removed < 1)
            {
                throw new KeyNotFoundException();
            }

            return Task.CompletedTask;
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<T>>(EntityList);
        }

        public Task<T?> GetByIdAsync(int id)
        {
            return Task.FromResult(EntityList.FirstOrDefault(x => x.Id == id));
        }

        public Task<int> InsertAsync(T entity)
        {
            entity.Id = _nextId++;

            if(entity is IRecord record)
            {
                record.Creater = User;
                record.CreateTime = DateTime.Now;
            }

            if(entity is IEditableRecord editableRecord)
            {
                editableRecord.UpdateUser = User;
                editableRecord.LastUpdateTime = DateTime.Now;
            }
            
            EntityList.Add(entity);
            return Task.FromResult(entity.Id);
        }

        public Task UpdateAsync(T entity)
        {
            var index = EntityList.FindIndex(x => x.Id == entity.Id);
            if (index < 0)
            {
                throw new KeyNotFoundException();
            }

            if(entity is IEditableRecord editableRecord)
            {
                editableRecord.UpdateUser = User;
                editableRecord.LastUpdateTime = DateTime.Now;
            }

            EntityList[index] = entity;
            return Task.CompletedTask;
        }
    }
}
