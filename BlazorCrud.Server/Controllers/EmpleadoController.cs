using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;




namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly DbcrudBlazorContext _dbContext;
        public EmpleadoController(DbcrudBlazorContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<EmpleadoDTO>>();
            var listaEmpleadoDTO = new List<EmpleadoDTO>();

            try
            {
                foreach (var item in await _dbContext.Empleados.Include(d => d.IdDepartamentoNavigation).ToListAsync())
                {
                    listaEmpleadoDTO.Add(new EmpleadoDTO
                    {
                        IdEmpleado = item.IdEmpleado,
                        NombreCompleto = item.NombreCompleto,
                        IdDepartamento = item.IdDepartamento,
                        Sueldo = item.Sueldo,
                        FechaContrato = item.FechaContrato,
                        Departamento = new DepartamentoDTO
                        {
                            IdDepartamento = item.IdDepartamentoNavigation.IdDepartamento,
                            Nombre = item.IdDepartamentoNavigation.Nombre
                        }
                    });
                }
                responseApi.EsCorrecto = true;
                responseApi.Valor = listaEmpleadoDTO;
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);

        }
        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var responseApi = new ResponseAPI<EmpleadoDTO>();
            var EmpleadoDTO = new EmpleadoDTO();

            try
            {
                var dbEmpleado = await _dbContext.Empleados.FirstOrDefaultAsync(x => x.IdEmpleado == id);

                if(dbEmpleado != null)
                {
                    EmpleadoDTO.IdEmpleado = dbEmpleado.IdEmpleado;
                    EmpleadoDTO.NombreCompleto = dbEmpleado.NombreCompleto;
                    EmpleadoDTO.IdDepartamento = dbEmpleado.IdDepartamento;
                    EmpleadoDTO.Sueldo = dbEmpleado.Sueldo;
                    EmpleadoDTO.FechaContrato = dbEmpleado.FechaContrato;
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = EmpleadoDTO;
                }  else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "no encontrado";
                }

                
                
            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }
        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(EmpleadoDTO empleado)
        {
            var responseApi = new ResponseAPI<EmpleadoDTO>();


            try
            {
                var dbEmpleado = new Empleado
                {
                    NombreCompleto = empleado.NombreCompleto,
                    IdDepartamento = empleado.IdDepartamento,
                    Sueldo = empleado.Sueldo,
                    FechaContrato = empleado.FechaContrato,
                };

                _dbContext.Empleados.Add(dbEmpleado);
                await _dbContext.SaveChangesAsync();

                if(dbEmpleado.IdEmpleado != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbEmpleado.IdEmpleado;
                }else
                {

                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = EmpleadoDTO;

            }
            catch (Exception ex)
            {
                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }
            return Ok(responseApi);
        }
    }
}
