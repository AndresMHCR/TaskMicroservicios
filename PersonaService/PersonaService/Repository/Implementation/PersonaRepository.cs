using Microsoft.EntityFrameworkCore;
using PersonaService.Models;
using PersonaService.Repository.Interface;

namespace PersonaService.Repository.Implementation
{
    public class PersonaRepository : IPersonaRepository
    {
        private readonly PersonaContext _personaContext;

        public PersonaRepository(PersonaContext personaContext
            )
        {
            _personaContext = personaContext;
        }


        public async Task<Persona> GetPerson(int idPersona) => await _personaContext.Persona.AsNoTracking().FirstOrDefaultAsync(c => c.Idpersona == idPersona);
        public async Task<Persona> GetPersonByNui(string nui) => await _personaContext.Persona.AsNoTracking().FirstOrDefaultAsync(c => c.Identificacion == nui);
        public async Task CreatePerson(Persona data)
        {
            await _personaContext.Persona.AddAsync(data);
            await _personaContext.SaveChangesAsync();
        }
        public void UpdatePerson(Persona data) 
        { 
            _personaContext.Persona.Update(data); 
            _personaContext.SaveChangesAsync(); 
        }

        public async Task<Cliente> GetClient(int idCliente) => await _personaContext.Cliente.AsNoTracking().FirstOrDefaultAsync(c => c.Idcliente == idCliente);
        public async Task<Cliente> GetClientbyPerson(int idPerson) => await _personaContext.Cliente.AsNoTracking().FirstOrDefaultAsync(c => c.Idpersona == idPerson);
        public async Task CreateClient(Cliente data)
        {
            await _personaContext.Cliente.AddAsync(data);
            await _personaContext.SaveChangesAsync();
        }
        public void UpdateClient(Cliente data)
        {
            _personaContext.Cliente.Update(data);
            _personaContext.SaveChangesAsync();
        }
        public async Task DeleteClient(Cliente data)
        {
              _personaContext.Cliente.Remove(data);
             await _personaContext.SaveChangesAsync();
        }

    }

}
