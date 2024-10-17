using Microsoft.EntityFrameworkCore;
using CuentaService.Models;
using CuentaService.Repository.Interface;

namespace CuentaService.Repository.Implementation
{
    public class CuentaRepository : ICuentaRepository
    {
        private readonly CuentaContext _personaContext;

        public CuentaRepository(CuentaContext cuentaContext
            )
        {
            _personaContext = cuentaContext;
        }


        public async Task<Cuenta> GetCuenta(int idCuenta) => await _personaContext.Cuenta.AsNoTracking().FirstOrDefaultAsync(c => c.Idcuenta == idCuenta);
        public async Task<Cuenta> GetCuentaXNumCuenta(string numCuenta) => await _personaContext.Cuenta.AsNoTracking().FirstOrDefaultAsync(c => c.Numerocuenta == numCuenta);
        public async Task CreateCuenta(Cuenta data)
        {
            await _personaContext.Cuenta.AddAsync(data);
            await _personaContext.SaveChangesAsync();
        }
        public void UpdateCuenta(Cuenta data) 
        { 
            _personaContext.Cuenta.Update(data); 
            _personaContext.SaveChangesAsync(); 
        }

        public async Task DeleteCuenta(Cuenta data)
        {
            _personaContext.Cuenta.Remove(data);
            await _personaContext.SaveChangesAsync();
        }

        public async Task<Movimiento> GetMovimiento(int idMovimiento) => await _personaContext.Movimiento.AsNoTracking().FirstOrDefaultAsync(c => c.Idmovimiento== idMovimiento);
        public async Task<Movimiento> GetUltimoMovimientoxCuenta(int idCuenta) => await _personaContext.Movimiento.AsNoTracking().FirstOrDefaultAsync(c => c.Idcuenta== idCuenta);
        public async Task CreateMovimiento(Movimiento data)
        {
            await _personaContext.Movimiento.AddAsync(data);
            await _personaContext.SaveChangesAsync();
        }
        public void UpdateMovimiento(Movimiento data)
        {
            _personaContext.Movimiento.Update(data);
            _personaContext.SaveChangesAsync();
        }
        public async Task DeleteMovimiento(Movimiento data)
        {
              _personaContext.Movimiento.Remove(data);
             await _personaContext.SaveChangesAsync();
        }

        public async Task<Cuenta> GetReporteMovimiento(string identificacion, DateTime fechaInicio, DateTime fechaFIn)
        {
            Cuenta response =await _personaContext.Cuenta.AsNoTracking().Where(c => c.Identificacioncli == identificacion).Include(c=>c.Movimientos).FirstOrDefaultAsync();
            response.Movimientos = response.Movimientos.OrderByDescending(m => m.Fecha).Where(m => m.Fecha >= fechaInicio && m.Fecha <= fechaFIn).ToList();
            return response;
        }

    }

}
