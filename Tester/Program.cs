using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            string In1 = "dsafsa";
            string In2 = "dsafsasfdgdsgdsgfdsgfsdgsdgrewtrewrtewtewrtewtrewrtewtewtewrtew";
            string In3 = "dsafsafdssdgfds";

            Console.WriteLine(Sec.PasswordToSaltedHash(In1).SaltedHashedPassword + " " + Sec.PasswordToSaltedHash(In1).SaltedHashedPassword.Length);
            Console.WriteLine(Sec.PasswordToSaltedHash(In2).SaltedHashedPassword);
            Console.WriteLine(Sec.PasswordToSaltedHash(In3).SaltedHashedPassword);
            Console.ReadKey();
        }
    }
}
