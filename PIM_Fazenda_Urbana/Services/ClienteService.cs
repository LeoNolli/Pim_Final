using PIM_Fazenda_Urbana.Interfaces.IRepository;
using PIM_Fazenda_Urbana.Interfaces.ISevice;
using PIM_Fazenda_Urbana.Models;

namespace PIM_Fazenda_Urbana.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
        {
            _clienteRepository = clienteRepository;

        }

        public void CreateCliente(Cliente cliente)
        {
            _clienteRepository.CreateCliente(cliente);
        }

        public Cliente GetCliente(int id)
        {
            return _clienteRepository.GetCliente(id);
        }

        public Cliente GetClienteByUsuarioId(int id)
        {
            return _clienteRepository.GetClienteByUsuarioId(id);
        }
    }
}
