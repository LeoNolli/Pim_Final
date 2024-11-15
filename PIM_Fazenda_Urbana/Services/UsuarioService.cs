using PIM_Fazenda_Urbana.Interfaces.IRepository;
using PIM_Fazenda_Urbana.Interfaces.ISevice;
using PIM_Fazenda_Urbana.Models;
using PIM_Fazenda_Urbana.Models.DTO;
using System.Diagnostics.Eventing.Reader;
using System.Security.Cryptography;
using System.Text;

namespace PIM_Fazenda_Urbana.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;

        }

        public Usuario? GetByEmail(string email)
        {
            return _repository.GetByEmail(email);
        }

        public int CreateUsuario(UsuarioRegistroDTO novoUsuario)
        {
            novoUsuario.Senha = HashPassword(novoUsuario.Senha);
            if (novoUsuario.Funcao == string.Empty || novoUsuario.Funcao == null)
            {
                novoUsuario.Funcao = "Cliente";
            }
             
            return _repository.CreateUsuario(novoUsuario);
        }

        public bool ConfirmarSenha(string senha)
        {
            if (senha == HashPassword(senha))
                return true;
            return false;
        }
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
