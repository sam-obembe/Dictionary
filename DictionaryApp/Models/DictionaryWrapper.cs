using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DictionaryApp.Models
{
    public class DictionaryWrapper:IDictionaryWrapper
    {
        private Dictionary<string, IEnumerable<string>> dictionary;
        public DictionaryWrapper(Dictionary<string,IEnumerable<string>> _dictionary)
        {
            dictionary = _dictionary;
        }

        public bool Add(string key, string member)
        {
            //var isPresent = dictionary.Keys.Contains(key);
            if(string.IsNullOrEmpty(key) || string.IsNullOrEmpty(member) || string.IsNullOrWhiteSpace(key) || string.IsNullOrWhiteSpace(member))
            {
                return false;
            }
            if (dictionary.Keys.Contains(key))
            {
                if (dictionary[key].Contains(member))
                {
                    return false;
                }
                else
                {
                    var members = dictionary[key].ToList();
                    members.Add(member);
                    dictionary[key] = members;

                    return true;
                }
            }
            else
            {
                var members = new List<string>();
                members.Add(member);
                dictionary.Add(key, members);

                return true;
            }
            
            
        }

        public IEnumerable<string> AllMembers()
        {
            var members = new List<string>();
            var keys = dictionary.Keys.ToList();

            foreach(var key in keys)
            {
                var records = dictionary[key];
                members.AddRange(records);
            }

            return members;
        }

        public bool Clear()
        {
            dictionary.Clear();
            return true;
            //throw new NotImplementedException();
        }

        public IEnumerable<string> Items()
        {
            var items = new List<string>();
            var keys = dictionary.Keys.ToList();

            foreach(var key in keys)
            {
                var records = dictionary[key].Select((val) => $"{key}:{val}");
                items.AddRange(records);
            }

            return items;

            //throw new NotImplementedException();
        }

        public bool KeyExists(string key)
        {
            var keyExists = dictionary.Keys.ToList().Contains(key);
            return keyExists;
            //throw new NotImplementedException();
        }

        public IEnumerable<string> Keys()
        {
            var keys = dictionary.Keys.ToList();
            return keys;
        }

        public IEnumerable<string> Members(string key)
        {
            if (dictionary.Keys.Contains(key))
            {
                var members = dictionary[key];
                return members;
            }
            else
            {
                return null;
            }
            //throw new NotImplementedException();
        }

        public bool MemberExists(string key, string member)
        {
            var keyMembers = dictionary[key];

            if(keyMembers == null)
            {
                return false;
            }
            else if (keyMembers.Contains(member))
            {
                return true;
            }
            else
            {
                return false;
            }
            //throw new NotImplementedException();
        }

        public bool RemoveAll(string key)
        {
            var existingKey = KeyExists(key);

            if (existingKey)
            {
                dictionary.Remove(key);
                return true;
            }
            else
            {
                return false;
            }
            
        }

        public bool Remove(string key, string member)
        {
            if (dictionary.Keys.Contains(key))
            {
                var members = dictionary[key];
                
                if(Array.IndexOf(members.ToArray(), member) >= 0)
                //if (members.Contains(member))
                {
                  var updated = members.ToList();
                    updated.Remove(member);
                    if(updated.Count > 0)
                    {
                        dictionary[key] = updated;
                    }
                    else
                    {
                        dictionary.Remove(key);
                    }
                    return true;
                }
                else
                {
                    return false;
                }
                
            }
            else
            {
                return false;
            }
            //throw new NotImplementedException();
        }
    }
}
