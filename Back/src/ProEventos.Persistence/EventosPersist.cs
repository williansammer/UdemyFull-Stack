using System.Threading.Tasks;
using ProEventos.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProEventos.Persistence.Contratos;
using ProEventos.Persistence.Contextos;

namespace ProEventos.Persistence
{
    public class EventosPersist : IEventoPersist
    {
        private readonly ProEventosContext _proEventosContext;

        public EventosPersist(ProEventosContext proEventosContext)
        {
            this._proEventosContext = proEventosContext;
            //ou usar AsNoTracking para nao segurar objeto em cada método
            this._proEventosContext.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
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
            //ou usar AsNoTracking para nao segurar objeto
            // query = query.AsNoTracking().OrderBy(e => e.Id).Where(e => e.Id.Equals(EventooId));
            query = query.OrderBy(e => e.Id).Where(e => e.Id.Equals(EventooId));

            return await query.FirstOrDefaultAsync();
        }

      

    }
}