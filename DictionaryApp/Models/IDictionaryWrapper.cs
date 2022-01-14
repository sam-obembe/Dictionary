using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryApp.Models
{
    public interface IDictionaryWrapper
    {
        public IEnumerable<string> Keys ();

        public IEnumerable<string> Members(string key);

        public bool Add(string key, string member);

        public bool Remove(string key, string member);

        public bool RemoveAll(string key);

        public bool Clear();

        public bool KeyExists(string key);

        public bool MemberExists(string key, string member);

        public IEnumerable<string> AllMembers();

        public IEnumerable<string> Items();
    }
}
