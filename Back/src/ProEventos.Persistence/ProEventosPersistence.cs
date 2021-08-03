using System.Threading.Tasks;
using ProEventos.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProEventos.Persistence
{
    public class ProEventosPersistence : IProEventosPersistence
    {
        private readonly ProEventosContext _proEventosContext;

        public ProEventosPersistence(ProEventosContext proEventosContext)
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

        public async Task<Evento[]> GetAllEventosByAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _proEventosContext.Eventos
                                       .Include(e => e.Lotes)
                                       .Include(e => e.RedesSociais)
                                       .OrderBy(e => e.Id);
            if (includePalestrantes)
            {
                query = query.Include(e => e.PalestranteEventos).ThenInclude(p => p.Palestrante);
            }

            return await query.ToArrayAsync();
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
             IQueryable<Evento> query = _proEventosContext.Eventos
                                       .Include(e => e.Lotes)
                                       .Include(e => e.RedesSociais);
                                       
            if (includePalestrantes)
            {
                query = query.Include(e => e.PalestranteEventos).ThenInclude(p => p.Palestrante);
            }

            query = query.OrderBy(e => e.Id).Where(e => e.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }

       public async Task<Evento> GetEventoByIdAsync(int EventooId, bool includePalestrantes = false)
        {
             IQueryable<Evento> query = _proEventosContext.Eventos
                                       .Include(e => e.Lotes)
                                       .Include(e => e.RedesSociais);
                                       
            if (includePalestrantes)
            {
                query = query.Include(e => e.PalestranteEventos).ThenInclude(p => p.Palestrante);
            }

            query = query.OrderBy(e => e.Id).Where(e => e.Id.Equals(EventooId));

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByAsync(bool includeEventos = false)
        {
             IQueryable<Palestrante> query = _proEventosContext.Palestrantes
                                       .Include(p => p.RedesSociais)
                                       .OrderBy(p=> p.Id);
            if (includeEventos)
            {
                query = query.Include(p => p.PalestranteEventos).ThenInclude(e => e.Evento);
            }

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _proEventosContext.Palestrantes
                                       .Include(p => p.RedesSociais);

            if (includeEventos)
            {
                query = query.Include(p => p.PalestranteEventos).ThenInclude(e => e.Palestrante);
            }

            query = query.OrderBy(p => p.Id).Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

            return await query.ToArrayAsync();
        }

        public async Task<Palestrante> GetPalestranteByIdAsync(int PalestranteId, bool includeEventos = false)
        {
             IQueryable<Palestrante> query = _proEventosContext.Palestrantes
                                       .Include(e => e.RedesSociais);
                                       
            if (includeEventos)
            {
                query = query.Include(p => p.PalestranteEventos).ThenInclude(p => p.Evento);
            }

            query = query.OrderBy(p => p.Id).Where(p => p.Id.Equals(PalestranteId));

            return await query.FirstOrDefaultAsync();
        }


    }
}