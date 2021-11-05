using ChainOfResponsibility.Middlewares;
using ChainOfResponsibility.Servers;
using System;

namespace ChainOfResponsibility
{
    class Program
    {
        private static Server server;
        static void Init() 
        {
            server = new Server();
            server.RegisterUser("master@hcode.com.br","123123sdkjh@%$");
            server.RegisterUser("panda@hcode.com.br", "123455");
            server.RegisterUser("minipanda@hcode.com.br", "123456");

            Middleware middleware = new CheckUserMiddleware(server);

            middleware.LinkWith(new CheckPermissionMiddleware());
            middleware.LinkWith(new CheckWeakPassword());

            server.SetMiddleware(middleware);
        }

        static void Main(string[] args)
        {
            Init();
            Boolean done = false;

            do
            {
                Console.WriteLine("Digite seu e-mail: ");
                string email = Console.ReadLine();

                Console.WriteLine("Disite sua senha: ");
                string password = Console.ReadLine();
                done=server.LogIng(email, password);

            } while (!done);

            Console.ReadLine();
        }
    }
}
