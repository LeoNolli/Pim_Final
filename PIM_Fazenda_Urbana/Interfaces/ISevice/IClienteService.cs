using PIM_Fazenda_Urbana.Models;

namespace PIM_Fazenda_Urbana.Interfaces.ISevice
{
    public interface IClienteService
    {
        public void CreateCliente(Cliente cliente);
        public Cliente GetCliente(int id);
        public Cliente GetClienteByUsuarioId(int id);
    }
}
