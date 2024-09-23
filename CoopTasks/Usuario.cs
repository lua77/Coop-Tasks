using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopTasks
{
    public class Usuario
    {
        private static int _proxId = 1;
        public int Id { get; set; } = _proxId++;
        public string nome {  get; set; }
        public string user {  get; set; }
        public string email { get; set; }
        public string senha { get; set; }
        public string datacadastro {  get; set; } = DateTime.Now.ToString();
        public NivelUsuario nivel { get; set; }

        public Usuario() 
        {
            nivel = NivelUsuario.min;
        }

        public Usuario(string user, string senha, NivelUsuario nivel)
        {
            this.user = user;
            this.senha = senha;
            this.nivel = nivel;
        }
    }

    public enum NivelUsuario
    {
        min,
        med,
        max
    }
}
