using PIM_Fazenda_Urbana.Models;

namespace PIM_Fazenda_Urbana.Interfaces.IRepository
{
    public interface IInsumoRepository
    {
        // Método para obter todos os insumos
        IEnumerable<Insumo> GetAll();

        // Método para obter um insumo pelo ID
        Insumo GetById(int id);

        // Método para adicionar um novo insumo
        void Add(Insumo insumo);

        // Método para atualizar um insumo existente
        void Update(Insumo insumo);

        // Método para deletar um insumo pelo ID
        void Delete(int id);
    }
}
