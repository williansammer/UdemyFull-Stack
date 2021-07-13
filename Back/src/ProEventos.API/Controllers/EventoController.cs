using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ProEventos.API.Models;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventoController : ControllerBase
    {

        public EventoController()
        {
       
        }

        private IEnumerable<Evento> Eventos(){
            var eventos = new Evento[5];
            var random = new Random();
            for (int i = 0; i < 5; i++)
            {
                var numero = random.Next(1,100);
                var id = i+1;
                eventos[i] = 
                    new Evento()
                    {
                        EventoId = id,
                        Tema = "Angular 11 e .NET 5",
                        Local = "Sorocaba",
                        Lote = $"{id} º lote",
                        QtdPessoas = (5 * numero),
                        DataEvento = DateTime.Now.AddDays(id).ToString("d"),
                        ImagemURL = $"foto{id}.png"
                    };
            }
            return eventos;
        }

        [HttpGet]
        public IEnumerable<Evento> Get()
        {
           return Eventos();
        }

        [HttpGet("{id}")]
        public IEnumerable<Evento> GetById(int id)
        {
           return Eventos().Where(x => x.EventoId == id);
        }
    }
}
