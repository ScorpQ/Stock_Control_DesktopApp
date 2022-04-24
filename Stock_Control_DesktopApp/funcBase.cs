using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock_Control_Manager
{
    class funcBase
    {

        public string generatePID()  //Ürünler için
        {
            Random rndm = new Random();
            int randomNum = rndm.Next(999999, 9999999);

            string collection = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int index = rndm.Next(26);

            return collection[index] + randomNum.ToString();
        }

        public int generateCID() // Müşteriler,Personeller, Firmalar için
        {
            Random rndm = new Random();
            return rndm.Next(100000, 1000000);
        }

    }
}
