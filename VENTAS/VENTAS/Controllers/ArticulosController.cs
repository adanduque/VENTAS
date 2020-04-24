using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VENTAS.Models;
using VENTAS.RequestDTO;

namespace VENTAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        private readonly DBSVentasContext _context;
        private readonly IMapper _mapper;


        public ArticulosController(DBSVentasContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Articulos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ArticuloDTO>>> GetArticuloes()
        {
            var articulos = await _context.Articuloes.Include(c=>c.Categoria).Include(u=>u.UndMedida).ToListAsync();
            var articulosDTO = _mapper.Map<List<ArticuloDTO>>(articulos);
            return articulosDTO;
        }

        // GET: api/Articulos/5
        [HttpGet("{id}",Name = "ObtenerArticulo")]
        public async Task<ActionResult<ArticuloDTO>> GetArticulo(long id)
        {
            var articulo = await _context.Articuloes.Include(x => x.Categoria).Include(x => x.UndMedida).FirstOrDefaultAsync(x=>x.Id==id);     
            if (articulo == null)
            {
                return NotFound();
            }
            var articuloDTO = _mapper.Map<ArticuloDTO>(articulo);
            return articuloDTO;
        }

        // PUT: api/Articulos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticulo(long id, [FromBody]ArticuloCreacionDTO articuloActualizacion)
        {
            var exist = _context.Articuloes.FirstOrDefault(x => x.Nombre.Trim().ToLower().Equals(articuloActualizacion.Nombre.Trim().ToLower())&&x.Id!=id);
            if (exist != null)
            {
                ModelState.AddModelError("Nombre", "El nombre del articulo asignado ya existe");
                return BadRequest(ModelState);
            }
            var articulo = _mapper.Map<Articulo>(articuloActualizacion);
            articulo.Id = id;
            _context.Entry(articulo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticuloExists(id))
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

        // POST: api/Articulos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Articulo>> PostArticulo([FromBody]ArticuloCreacionDTO articuloCreacionDTO)
        {
            //Name Unique
            var exist = _context.Articuloes.FirstOrDefault(x => x.Nombre.Trim().ToLower().Equals(articuloCreacionDTO.Nombre.Trim().ToLower()));
            if (exist != null)
            {
                ModelState.AddModelError("Nombre", "El nombre del articulo ya existe");
                return BadRequest(ModelState);
            }

            var articulo = _mapper.Map<Articulo>(articuloCreacionDTO);
            _context.Articuloes.Add(articulo);

            await _context.SaveChangesAsync();

            var articuloDTO = _mapper.Map<ArticuloDTO>(articulo);
            return new CreatedAtRouteResult("ObtenerArticulo", new { id = articuloDTO.Id }, articuloDTO);
        }

        // DELETE: api/Articulos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Articulo>> DeleteArticulo(long id)
        {
            var articuloId = await _context.Articuloes.Select(x => x.Id).FirstOrDefaultAsync(x => x == id);

            if (articuloId == default(int))
            {
                return NotFound();
            }

            _context.Remove(new Articulo { Id=articuloId});
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool ArticuloExists(long id)
        {
            return _context.Articuloes.Any(e => e.Id == id);
        }
    }
}
