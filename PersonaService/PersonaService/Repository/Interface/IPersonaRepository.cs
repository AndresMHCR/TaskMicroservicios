using PersonaService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PersonaService.Repository.Interface
{
    public interface IPersonaRepository
    {
        Task<Persona> GetPerson(int idPersona);
        Task<Persona> GetPersonByNui(string nui);
        Task CreatePerson(Persona data);
        void UpdatePerson(Persona data);

        Task<Cliente> GetClient(int idCliente);
        Task<Cliente> GetClientbyPerson(int idPerson);
        Task CreateClient(Cliente data);
        void UpdateClient(Cliente data);
        Task DeleteClient(Cliente data);

    }
}