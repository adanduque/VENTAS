using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VENTAS.Models;
using VENTAS.RequestDTO;

namespace VENTAS.Controllers
{
    [Route("api/unidades")]
    [ApiController]
    public class UndMedidasController : ControllerBase
    {
        private readonly DBSVentasContext _context;
        private readonly IMapper _mapper;


        public UndMedidasController(DBSVentasContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/UndMedidas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UndMedidaDTO>>> GetUndMedidas()
        {
            var unidades = await _context.UndMedidas.ToListAsync();
            var unidadesDTO = _mapper.Map<List<UndMedidaDTO>>(unidades);
            return unidadesDTO;
        }

        // GET: api/UndMedidas/5
        [HttpGet("{id}",Name ="Obtenerunidad")]
        public async Task<ActionResult<UndMedidaDTO>> GetUndMedida(int id)
        {
            var undMedida = await _context.UndMedidas.FindAsync(id);

            if (undMedida == null)
            {
                return NotFound();
            }
            var unidadDTO = _mapper.Map<UndMedidaDTO>(undMedida);
            return unidadDTO;
        }

        // PUT: api/UndMedidas/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUndMedida(int id, UnidadCreacionDTO unidadActulizacion)
        {

            //Validad Nombre Unique
            var exist = _context.UndMedidas.FirstOrDefault(x => x.Nombre.Trim().ToLower().Equals(unidadActulizacion.Nombre.Trim().ToLower()) && x.Id!=id);
            if (exist != null)
            {
                ModelState.AddModelError("Nombre", "El nombre de la unidad ya existe");
                return BadRequest(ModelState);
            }

            var unidad = _mapper.Map<UndMedida>(unidadActulizacion);
            unidad.Id = id;
            _context.Entry(unidad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UndMedidaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/UndMedidas
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<UndMedida>> PostUndMedida([FromBody] UnidadCreacionDTO unidadCrecionDTO)
        {
            //Validad Nombre Unique
            var exist = _context.UndMedidas.FirstOrDefault(x => x.Nombre.Trim().ToLower().Equals(unidadCrecionDTO.Nombre.Trim().ToLower()));
            if (exist != null)
            {
                ModelState.AddModelError("Nombre", "El nombre de la unidad ya existe");
                return BadRequest(ModelState);
            }
            var unidad = _mapper.Map<UndMedida>(unidadCrecionDTO);
            _context.UndMedidas.Add(unidad);
            await _context.SaveChangesAsync();
            var unidadDTO = _mapper.Map<UndMedidaDTO>(unidad);
            return new CreatedAtRouteResult("Obtenerunidad", new { id = unidadDTO.Id }, unidadDTO);
        }

        // DELETE: api/UndMedidas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UndMedida>> DeleteUndMedida(int id)
        {
            var unidadId = await _context.UndMedidas.Select(x=>x.Id).FirstOrDefaultAsync(x=>x==id);
            if (unidadId == default(int))
            {
                return NotFound();
            }

            _context.Remove(new UndMedida { Id = unidadId });
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UndMedidaExists(int id)
        {
            return _context.UndMedidas.Any(e => e.Id == id);
        }
    }
}
