using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoopTasks
{
    internal class Program
    {
        static bool ValidarEmail(string email)
        {
            if(!email.Contains("@") || !email.EndsWith(".com"))
            {
                return false;
            }

            string[] partesEmail = email.Split('@');

            if(partesEmail.Length != 2 || partesEmail[0].Length == 0)
            {
                return false;
            }

            string parteDominio = partesEmail[1];
            int indexDoCom = parteDominio.LastIndexOf(".com");

            if(indexDoCom < 3)
            {
                return false;
            }

            return true;
        }
        static string LerSenha()
        {
            string senha = "";
            ConsoleKeyInfo tecla;

            do
            {
                // Captura a tecla pressionada sem exibir na tela
                tecla = Console.ReadKey(true);

                // Se não for Enter, adiciona à senha e exibe *
                if (tecla.Key != ConsoleKey.Enter)
                {
                    senha += tecla.KeyChar;
                    Console.Write("*");
                }

            } while (tecla.Key != ConsoleKey.Enter); // Continue até Enter ser pressionado

            return senha;
        }
        static void login(List<Usuario> BdUsuarios) 
        {
            string user, senha;

            Usuario UsuarioEncontrado = null;

            bool senhaValida;


            Console.WriteLine("Login");
            do
            {
                Console.Write("Insira seu usuario: ");
                user = Console.ReadLine();

                UsuarioEncontrado = BdUsuarios.FirstOrDefault(u => u.user == user);

                if (UsuarioEncontrado == null)
                {
                    Console.WriteLine("O usuario não existe, verifique se escreveu corretamente.");
                }
            } while (UsuarioEncontrado == null);


            do
            {
                Console.Write("Insira seu senha: ");
                senha = Console.ReadLine();

                senhaValida = UsuarioEncontrado.senha == senha;

                if (!senhaValida)
                {
                    Console.WriteLine("Senha invalida! Tente novamente.");
                }
            } while(!senhaValida);

            Console.WriteLine($"Login nivel {UsuarioEncontrado.nivel} efetuado com sucesso!");

            Console.ReadKey();
            
        }

        static (string user, string nome, string email, string senha) Cadastro(List<Usuario> BdUsuarios)
        {
            string user, nome, email;
            string senha = "1";
            string senhaV = "0";

            bool userExiste;
            bool emailValido;
            bool emailExiste;
            do
            {
                Console.WriteLine("");
                Console.Write("Insira seu username: ");
                user = Console.ReadLine();

                userExiste = BdUsuarios.Any(u => u.user == user);

                if (userExiste)
                {
                    Console.WriteLine("Esse nome de usuario já está em uso. Tente usar outro.");
                }
            } while (userExiste);
            
            Console.Write("Insira seu nome: ");
            nome = Console.ReadLine();

            do
            {
                Console.Write("Insira seu email: ");
                email = Console.ReadLine();

                
                emailExiste = BdUsuarios.Any(e => e.email == email);

                if (emailExiste)
                {
                    Console.WriteLine("Esse email já está em uso. Tente outro.");
                }

                emailValido = ValidarEmail(email);

                if (!emailValido)
                {
                    Console.WriteLine("Email inválido. O email deve conter um '@', ao menos 3 caracteres antes de '.com' e terminar com '.com'.");
                }

                

            } while (emailExiste || !emailValido);
            

            while (senha != senhaV) 
            {
                if(senha != "1")
                {
                    Console.WriteLine("As senhas estão diferentes! Tente novamente: ");
                }
                Console.Write("Digite sua senha: ");
                senha = LerSenha();
                Console.WriteLine();
                Console.Write("Confirme sua senha: ");
                senhaV = LerSenha();
                Console.WriteLine();
            }

            return (user, nome, email, senha);
           
        }
            static void Main(string[] args)
        {
            List<Usuario> BdUsuarios = new List<Usuario>();

            Usuario tecUser = new Usuario("tec", "tec", NivelUsuario.max);
            BdUsuarios.Add(tecUser);

            Usuario item;

            int op  = 0;
            string user, nome, email, senha;
            
            while (op != 1 && op != 2 && op != 9)
            {
                Console.Clear();
                Console.WriteLine("     Coop Tasks      \n\n");
                Console.WriteLine(" 1. Login");//ok mas para após login
                Console.WriteLine(" 2. Cadastre-se");//funcionando com verificações basicas
                Console.WriteLine(" 9. Sair\n");//sempre ok
                Console.Write(" Selecione uma opção: ");
                op = int.Parse(Console.ReadLine());

                switch (op)
                {
                    case 1:
                        login(BdUsuarios);
                        break;

                    case 2:
                        item = new Usuario();

                        Console.Clear();
                        Console.WriteLine("     Cadastro de usuario     \n\n");
                        Console.WriteLine("User ID: " + item.Id + "\n");

                        (user, nome, email, senha) = Cadastro(BdUsuarios);

                        item.user = user;
                        item.nome = nome;
                        item.email = email;
                        item.senha = senha;

                        BdUsuarios.Add(item);

                        Console.WriteLine("\n\nUsuario cadastrado!! \nPressione qualquer tecla para continuar para o login...");
                        Console.ReadKey();
                        Console.Clear();

                        login(BdUsuarios);

                        break;

                    case 9:
                        Environment.Exit(0);
                        break;
                }
            }
        }
    }
}
