using System.Threading.Tasks;
using ProEventos.Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ProEventos.Persistence.Contratos;
using ProEventos.Persistence.Contextos;

namespace ProEventos.Persistence
{
    public class PalestrantePersist : IPalestrantePersist
    {
        private readonly ProEventosContext _proEventosContext;

        public PalestrantePersist(ProEventosContext proEventosContext)
        {
            this._proEventosContext = proEventosContext;

        }
    
        public async Task<Palestrante[]> GetAllPalestrantesByAsync(bool includeEventos = false)
        {
             IQueryable<Palestrante> query = _proEventosContext.Palestrantes
                                       .Include(p => p.RedesSociais)
                                       .OrderBy(p=> p.Id);
            if (includeEventos)
            {
                query = query.AsNoTracking().Include(p => p.PalestranteEventos).ThenInclude(e => e.Evento);
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

            query = query.AsNoTracking().OrderBy(p => p.Id).Where(p => p.Nome.ToLower().Contains(nome.ToLower()));

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

            query = query.AsNoTracking().OrderBy(p => p.Id).Where(p => p.Id.Equals(PalestranteId));

            return await query.FirstOrDefaultAsync();
        }


    }
}