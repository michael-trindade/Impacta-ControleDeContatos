using ControleDeContatos.Data;
using ControleDeContatos.Models;

namespace ControleDeContatos.Repositorio
{
    //como estou impementando (usando) sou obirgado a assinar este contrato
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly BancoContext _bancoContext;
        public UsuarioRepositorio(BancoContext bancoContext)
        {
            this._bancoContext = bancoContext;

        }

        public UsuarioModel ListarPorId(int id)
        {
            return _bancoContext.Usuarios.FirstOrDefault(x => x.UsuarioID == id);
        }

        public List<UsuarioModel> BuscarTodos()
        {
            return _bancoContext.Usuarios.ToList();
        }

        public UsuarioModel Adicionar(UsuarioModel usuario)
        {
            usuario.DataCadastro = DateTime.Now;
            usuario.DataAtualizacao = DateTime.MinValue;
            _bancoContext.Usuarios.Add(usuario);
            _bancoContext.SaveChanges();
            return usuario;
        }

        public UsuarioModel Atualizar(UsuarioModel usuario)
        {
            UsuarioModel usuarioDb = ListarPorId(usuario.UsuarioID);
            if (usuarioDb == null) throw new System.Exception("Houve um erro de atualização");


            usuarioDb.UsuarioNome = usuario.UsuarioNome;
            usuarioDb.Email = usuario.Email;
            usuarioDb.Login = usuario.Login;
            usuarioDb.Senha = usuario.Senha;
            usuarioDb.Perfil = usuario.Perfil;
            usuarioDb.DataAtualizacao = DateTime.Now;

            _bancoContext.Usuarios.Update(usuarioDb);
            _bancoContext.SaveChanges();
            return usuarioDb;
        }

        public bool Apagar(int id)
        {
            UsuarioModel usuarioDb = ListarPorId(id);
            if (usuarioDb == null) throw new System.Exception("Houve um erro de Exclusão");
            _bancoContext.Usuarios.Remove(usuarioDb);
            _bancoContext.SaveChanges();
            return true;
        }


    }
}
