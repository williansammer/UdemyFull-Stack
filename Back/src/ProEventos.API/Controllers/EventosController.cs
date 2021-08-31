using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProEventos.Application.Contratos;
using ProEventos.Domain;
using ProEventos.Persistence.Contextos;

namespace ProEventos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventosController : ControllerBase
    {

        private readonly IEventosService eventoService;

        public EventosController(IEventosService eventoService)
        {
            this.eventoService = eventoService;


        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var eventos = await eventoService.GetAllEventosByAsync(true);
                if(eventos == null)return NotFound("Nenhum Evento Encontrado!");

                return Ok(eventos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,$"Erro ao tentar recuperar eventos. Erro:{ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
           try
            {
                var evento = await eventoService.GetEventoByIdAsync(id,true);
                if(evento == null)return NotFound($"Evento {id} não encontrado!");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,$"Erro ao tentar recuperar o evento. Erro:{ex.Message}");
            }
        }

        [HttpGet("{tema}")]
        public async Task<IActionResult> GetByTema(string tema)
        {
           try
            {
                var evento = await eventoService.GetAllEventosByTemaAsync(tema,true);
                if(evento == null)return NotFound("Eventos por tema não encontrado!");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,$"Erro ao tentar recuperar os eventos. Erro:{ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(Evento model)
        {
            try
            {
                var evento = await eventoService.AddEventos(model);
                if(evento == null)return BadRequest($"Erro ao tentar adicionar o evento {model.Tema}!");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,$"Erro ao tentar salvar o evento. Erro:{ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id,Evento model)
        {
            try
            {
                var evento = await eventoService.UpdateEvento(id,model);
                if(evento == null)return BadRequest($"Erro ao tentar alterar o evento {model.Tema}!");

                return Ok(evento);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,$"Erro ao tentar alterar o evento. Erro:{ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var evento = await eventoService.DeleteEvento(id);
                if(!evento)return BadRequest($"Erro ao tentar deletar o evento {id}!");

                return Ok("Deletado");
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,$"Erro ao tentar deletar o evento. Erro:{ex.Message}");
            }
        }


    }
}
