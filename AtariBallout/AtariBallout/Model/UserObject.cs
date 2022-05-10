using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtariBallout.Model
{
    public class UserObject: INotifyPropertyChanged
    {
        public UserObject()
        {
        }

        public UserObject(string un, string hs, string st, string fn, string ln)
        {
            this.Username = un;
            this.Hash = hs;
            this.Salt = st;
            this.FirstName = fn;
            this.LastName = ln;
            this.Highscore = 0;
        }

        public string Username { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Int32 Highscore { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string GetFullname()
        {
            return $"{FirstName} {LastName}";
        }
    }
}
