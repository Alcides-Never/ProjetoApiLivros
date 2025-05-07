using ProjetoLivros.Context;
using ProjetoLivros.Models;
using ProjetoLivros.Repositories;

namespace ProjetoLivros.Interface
{
    public interface ICategoriaRepository
    {
        // Assincrono - Task
        Task<List<Categoria>> ListarTodosAsync();
        //Sincrono
        List<Categoria> ListarTodos();

        void Cadastrar(Categoria categoria);
        Categoria? Atualizar(int id, Categoria categoria);
        Categoria? Deletar(int id);
    }
}
