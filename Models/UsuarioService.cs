using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;


namespace Biblioteca.Models
{
    public class UsuarioService
    {
        public List<Usuario> Listar()
        {
            using(BibliotecaContext bc = new BibliotecaContext())
                {
                    return bc.Usuarios.ToList();
                }   
        }

        public Usuario Listar(int id)
        {
            using(BibliotecaContext bc = new BibliotecaContext())
                {
                    return bc.Usuarios.Find(id);
                }   
        }

        public void incluirUsuario(Usuario u)
        {
             using(BibliotecaContext bc = new BibliotecaContext())
                {
                    bc.Add(u);
                    bc.SaveChanges();
                }   
        }
       
        public void editarUsuario(Usuario EditU)
        {
             using(BibliotecaContext bc = new BibliotecaContext())
                {
                    Usuario antigoU = bc.Usuarios.Find (EditU.Id);
                    
                    antigoU.Login = EditU.Login;
                    antigoU.Nome = EditU.Nome;
                    antigoU.Senha = EditU.Senha;
                    antigoU.Tipo = EditU.Tipo;

                    bc.SaveChanges();
                }   
        }

        public void excluirUsuario(int id)
        {
             using(BibliotecaContext bc = new BibliotecaContext())
                {
                   bc.Usuarios.Remove(bc.Usuarios.Find(id));
                    bc.SaveChanges();
                } 
        }
    }
}