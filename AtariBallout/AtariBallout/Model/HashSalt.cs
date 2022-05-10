using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtariBallout.Model
{
    public class HashSalt
    {
        public HashSalt(string hash, string salt)
        {
            Hash = hash;
            Salt = salt;
        }

        public string Hash { get; set; }
        public string Salt { get; set; }
    }
}
