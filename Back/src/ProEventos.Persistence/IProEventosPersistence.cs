using System.Threading.Tasks;
using ProEventos.Domain;

namespace ProEventos.Persistence
{
    public interface IProEventosPersistence
    {
        void Add<T>(T entity) where T:class; 

        void Update<T>(T entity) where T:class;

        void Delete<T>(T entity) where T:class;

        void DeleteRange<T>(T[] entity) where T:class;

        Task<bool> SaveChangesAsync();

        //Eventos
        Task<Evento[]> GetAllEventosByTemaAsync(string tema,bool includePalestrantes);
        Task<Evento[]> GetAllEventosByAsync(bool includePalestrantes);
        Task<Evento> GetEventoByIdAsync(int EventooId, bool includePalestrantes);

         //Palestrantes
        Task<Palestrante[]> GetAllPalestrantesByNomeAsync(string nome,bool includeEventos);
        Task<Palestrante[]> GetAllPalestrantesByAsync(bool includeEventos);
        Task<Palestrante> GetPalestranteByIdAsync(int PalestranteId, bool includeEventos);
    }
}