using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Tjuv_ochPolis_Inlämningsuppgift1_
{
    /// <summary>
    /// Stores keys, phones, watches and money. It also contains a propertry for naming the inventory. 
    /// </summary>
    class Inventory
    {
        private string name;
        private int key;
        private int phone;
        private int watch;
        private int money;
        public string Name { get => name; set => name = value; }
        public int Key { get => key; set => key = value;  }
        public int Phone { get => phone; set => phone = value;  }
        public int Watch { get  => watch; set => watch = value;  }
        public int Money { get => money; set => money = value;  }
        /// <summary>
        /// Constructs an Inventory when only name parameter is supplied.
        /// </summary>
        /// <param name="_name"></param>
        public Inventory(string _name)
        {
            Name = _name;
            Key = 0;
            Phone = 0;
            Watch = 0;
            Money = 0;
        }
        /// <summary>
        /// Constructs an Inventory when all values are supplied.
        /// </summary>
        /// <param name="_name"></param>
        /// <param name="_key"></param>
        /// <param name="_phone"></param>
        /// <param name="_watch"></param>
        /// <param name="_money"></param>
        public Inventory(string _name, int _key, int _phone, int _watch, int _money)
        {
            Name = _name;
            Key = _key;
            Phone = _phone;
            Watch = _watch;
            Money = _money;
        }


    }
}
