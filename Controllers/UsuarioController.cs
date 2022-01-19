using Biblioteca.Models;
using Microsoft.AspNetCore.Mvc;

namespace Biblioteca.Controllers
{
    public class UsuarioController : Controller
    {
        //Inserção ---

        public IActionResult RegistrarUsuario()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarUsuario (Usuario novoUsuario)
        {
            novoUsuario.Senha = Criptografo.TextoCriptografado(novoUsuario.Senha);
            new UsuarioService().incluirUsuario(novoUsuario);
            return RedirectToAction ("CadastroRealizado");
        
        }
        public IActionResult CadastroRealizado ()
        {
            return View();
        }


        //Lista de Usuários ---

        public IActionResult listaDeUsuarios()
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);
            return View(new UsuarioService().Listar());
        }
        //Edição ---

        public IActionResult editarUsuario (int id)
        {
            Autenticacao.CheckLogin(this);
            Autenticacao.verificaSeUsuarioEAdmin(this);
            Usuario u = new UsuarioService().Listar (id);
            return View (u);
        }

        [HttpPost]
        public IActionResult editarUsuario (Usuario userEditado)
        {
            new UsuarioService().editarUsuario(userEditado);
            return RedirectToAction ("ListaDeUsuarios");
        }


        //Exclusão ---

        public IActionResult ExcluirUsuario(int id)
        {
            return View (new UsuarioService().Listar(id));
        }

        [HttpPost]
        public IActionResult ExcluirUsuario (string decisao, int id)
        {
            if(decisao == "EXCLUIR")
            {
                ViewData["Mensagem"] = "Exclusão do Usuário " + new UsuarioService().Listar(id).Nome + " realizada com sucesso!";
                new UsuarioService().excluirUsuario(id);
                return View("ListaDeUsuarios", new UsuarioService().Listar());
            }
            else
            {
                ViewData["Mensagem"] = "Exclusão Cancelada";
                return View("ListaDeUsuarios", new UsuarioService().Listar());
            }
            
        }
        //Funções Extras ---

        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }

        public IActionResult NeedAdmin()
        {
            Autenticacao.CheckLogin(this);
            return View("NeedAdmin");
        }

    }
}