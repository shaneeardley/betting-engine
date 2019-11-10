using System.Collections.Generic;
using System.Linq;
using BettingEngineServer.Classes;

namespace BettingEngineServer.Interfaces
{
    public interface ICrudRepository<TEntity> where TEntity : IEntity
    {
        List<TEntity> GetAll();
        TEntity GetById(string id);
        TEntity Create(TEntity newItem);
        TEntity Update(TEntity updateItem);
        void Delete(string id);

    }
}