using System.Threading.Tasks;
using ProEventos.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProEventos.Persistence.Contratos;
using ProEventos.Persistence.Contextos;

namespace ProEventos.Persistence
{
    public class GeralPersist : IGeralPersist
    {
        private readonly ProEventosContext _proEventosContext;

        public GeralPersist(ProEventosContext proEventosContext)
        {
            this._proEventosContext = proEventosContext;

        }
        public void Add<T>(T entity) where T : class
        {
            _proEventosContext.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _proEventosContext.Remove(entity);
        }

        public void DeleteRange<T>(T[] entity) where T : class
        {
            _proEventosContext.RemoveRange(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _proEventosContext.SaveChangesAsync()) > 0;
        }

        public void Update<T>(T entity) where T : class
        {
            _proEventosContext.Update(entity);
        }

    }
}