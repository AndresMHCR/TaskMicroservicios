using CuentaService.Models;
using CuentaService.Models.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CuentaService.Repository.Interface
{
    public interface ICuentaRepository
    {
        Task<Cuenta> GetCuenta(int IdCuenta);
        Task<Cuenta> GetCuentaXNumCuenta(string numCuenta);
        Task CreateCuenta(Cuenta data);
        void UpdateCuenta(Cuenta data);
        Task DeleteCuenta(Cuenta data);

        Task<Movimiento> GetMovimiento(int idMovimiento);
        Task<Movimiento> GetUltimoMovimientoxCuenta(int idCuenta);
        Task CreateMovimiento(Movimiento data);
        void UpdateMovimiento(Movimiento data);
        Task DeleteMovimiento(Movimiento data);

        Task<Cuenta> GetReporteMovimiento(string numCuenta, DateTime fechaInicio, DateTime fechaFIn);

    }
}