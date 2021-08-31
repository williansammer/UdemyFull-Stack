using System;
using System.Threading.Tasks;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Contratos;

namespace ProEventos.Application
{
    public class EventoService : IEventosService
    {
        private readonly IGeralPersist _geralPersist;
        private readonly IEventoPersist _eventoPersist;

        public EventoService(IGeralPersist geralPersist, IEventoPersist eventoPersist)
        {
            this._eventoPersist = eventoPersist;
            this._geralPersist = geralPersist;

        }
        public async Task<Evento> AddEventos(Evento model)
        {
            try
            {
                _geralPersist.Add<Evento>(model);
                if(await _geralPersist.SaveChangesAsync()){
                    return await _eventoPersist.GetEventoByIdAsync(model.Id,false);
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
        public async Task<Evento> UpdateEvento(int eventoId, Evento model)
        {
           try
           {
               var evento = await _eventoPersist.GetEventoByIdAsync(eventoId,false);
               if(evento == null) return null;

                model.Id = evento.Id;
               _geralPersist.Update(model);

                if(await _geralPersist.SaveChangesAsync()){
                    return await _eventoPersist.GetEventoByIdAsync(model.Id,false);
                }
                return null;
           }
           catch (Exception ex)
           {
               
               throw new Exception(ex.Message);
           }
        }

        public async Task<bool> DeleteEvento(int eventoId)
        {
           try
           {
               var evento = await _eventoPersist.GetEventoByIdAsync(eventoId,false);
               if(evento == null) throw new Exception("Evento para delete não foi encontrdo.");

               _geralPersist.Delete<Evento>(evento);
               return await _geralPersist.SaveChangesAsync();
           }
           catch (Exception ex)
           {
               
               throw new Exception(ex.Message);
           }
        }

        public async Task<Evento[]> GetAllEventosByAsync(bool includePalestrantes = false)
        {
            try
            {
                var eventos =  await _eventoPersist.GetAllEventosByAsync(includePalestrantes);
                if(eventos == null) return null;
                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento[]> GetAllEventosByTemaAsync(string tema, bool includePalestrantes = false)
        {
              try
            {
                var eventos =  await _eventoPersist.GetAllEventosByTemaAsync(tema,includePalestrantes);
                if(eventos == null) return null;
                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Evento> GetEventoByIdAsync(int eventooId, bool includePalestrantes = false)
        {
            try
            {
                var eventos =  await _eventoPersist.GetEventoByIdAsync(eventooId,includePalestrantes);
                if(eventos == null) return null;
                return eventos;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}