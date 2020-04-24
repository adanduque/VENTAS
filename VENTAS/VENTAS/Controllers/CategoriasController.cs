using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using VENTAS.Models;
using VENTAS.RequestDTO;

namespace VENTAS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly DBSVentasContext _context;
        private readonly IMapper _mapper;

        public CategoriasController(DBSVentasContext context,IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Categorias
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoriaDTO>>> GetCategorias()
        {
            var categorias= await _context.Categorias.ToListAsync();
            var categoriasDTO = _mapper.Map<List<CategoriaDTO>>(categorias);
            return categoriasDTO;
        }

        // GET: api/Categorias/5
        [HttpGet("{id}",Name = "ObtenerCategoria")]
        public async Task<ActionResult<CategoriaDTO>> GetCategoria(int id)
        {
           
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
            {
                return NotFound();
            }

            var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);
            return categoriaDTO;
        }

        // PUT: api/Categorias/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCategoria(int id, CategoriaCreacionDTO categoriaActualizacion)
        {
            //Validar Nombre Unique
            var exist = _context.Categorias.FirstOrDefault(x => x.Nombre.Trim().ToLower().Equals(categoriaActualizacion.Nombre.Trim().ToLower()) && x.Id!=id);
            if (exist != null)
            {
                ModelState.AddModelError("Nombre", "El nombre de la categoria asignada ya existe");
                return BadRequest(ModelState);
            }

            var categoria = _mapper.Map<Categoria>(categoriaActualizacion);
            categoria.Id = id;
            _context.Entry(categoria).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoriaExists(id))
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

        // POST: api/Categorias
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Categoria>> PostCategoria([FromBody]CategoriaCreacionDTO categoriacreacionDTO)
        {
            //Validar Nombre Unique
            var exist= _context.Categorias.FirstOrDefault(x => x.Nombre.Trim().ToLower().Equals(categoriacreacionDTO.Nombre.Trim().ToLower()));
            if (exist!=null)
            {
                ModelState.AddModelError("Nombre","El nombre de la categoria ya existe");
                return BadRequest(ModelState);
            }

            var categoria = _mapper.Map<Categoria>(categoriacreacionDTO);
            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();
            var categoriaDTO = _mapper.Map<CategoriaDTO>(categoria);
            return new CreatedAtRouteResult("ObtenerCategoria", new { id = categoriaDTO.Id }, categoriaDTO);
        }

        // DELETE: api/Categorias/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Categoria>> DeleteCategoria(int id)
        {
            var categoriaId = await _context.Categorias.Select(x=>x.Id).FirstOrDefaultAsync(x=>x==id);
            if (categoriaId == default(int))
            {
                return NotFound();
            }

            _context.Remove(new Categoria { Id = categoriaId });
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoriaExists(int id)
        {
            return _context.Categorias.Any(e => e.Id == id);
        }
    }
}
