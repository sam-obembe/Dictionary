using Xunit;
using DictionaryApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.IO;

namespace DictionaryAppTests
{
    public class DictionaryWrapperTests
    {

        [Fact]
        public void KeysReturnsAllKeys()
        {
            var dictionary = new Dictionary<string, IEnumerable<string>>();
            
            var wrapper = new DictionaryWrapper(dictionary);
            wrapper.Add("foo", "bar");
            wrapper.Add("foo", "buz");
            wrapper.Add("buzz", "kill");
            var keys = wrapper.Keys();

            Assert.Equal(2, keys.Count());

            Assert.Equal<string>(new string[] { "foo", "buzz" }, keys.ToArray());

        }

        [Theory]
        [InlineData("foo","bar")]
        [InlineData("buzz","kill")]
        [InlineData("run","")]
        [InlineData("run", " ")]
        [InlineData("","run")]
        [InlineData(" ", "run")]
        public void AddInsertsNewEntry(string key, string value)
        {
            var dictionary = new Dictionary<string, IEnumerable<string>>();
            var wrapper = new DictionaryWrapper(dictionary);

            var added = wrapper.Add(key, value);

            if (string.IsNullOrWhiteSpace(value) || string.IsNullOrWhiteSpace(key )|| string.IsNullOrEmpty(value) ||string.IsNullOrEmpty(key))
            {
                Assert.False(added);
            }
            else
            {
                Assert.True(added);
            }            

        }

        [Fact]
        public void AddDisallowsDuplicateInserts()
        {
            var dictionary = new Dictionary<string, IEnumerable<string>>();
            var wrapper = new DictionaryWrapper(dictionary);

            var firstAttempt = wrapper.Add("foo", "bar");

            var secondAttempt = wrapper.Add("foo", "bar");

            var members = wrapper.Members("foo").Count();

            Assert.Equal(1, members);
        }


        [Theory]
        [InlineData("foo","bar")]
        [InlineData("play", "soccer")]
        public void RemoveDeletesMembersCorrectly(string key,string value)
        {
            var dictionary = LoadSampleData();
          
            var wrapper = new DictionaryWrapper(dictionary);            

            wrapper.Remove(key,value);
  
            var valueIsPresent = dictionary[key].Contains(value);

            Assert.False(valueIsPresent);
        }

        [Theory]
        [InlineData("removeMe","nada")]
        public void RemoveDeletesKey(string key,string value)
        {
            var dictionary = LoadSampleData();
            

            var wrapper = new DictionaryWrapper(dictionary);

            wrapper.Remove(key, value);

            var isDeleted = dictionary.ContainsKey(key);

            Assert.False(isDeleted);

        }


        [Theory]
        [InlineData("play")]
        public void RemoveAllDeletesKey(string key)
        {
            var dictionary = LoadSampleData();
            

            var wrapper = new DictionaryWrapper(dictionary);

            wrapper.RemoveAll(key);

            var isDeleted = dictionary.ContainsKey(key);

            Assert.False(isDeleted);
        }

        [Fact]
        public void ClearRemovesAllData()
        {
            var dictionary = LoadSampleData();
            var wrapper = new DictionaryWrapper(dictionary);

            wrapper.Clear();

            var keys = dictionary.Keys.ToList().Count;

            Assert.Equal(0, keys);
        }

        [Fact]
        public void KeyExistsDetectsKey()
        {
            var dictionary = LoadSampleData();
            var wrapper = new DictionaryWrapper(dictionary);

            var exists = wrapper.KeyExists("play");

            Assert.True(exists);
        }

        [Fact]
        public void KeyExistsHandlesMissingKey()
        {
            var dictionary = LoadSampleData();
            var wrapper = new DictionaryWrapper(dictionary);
            var exists = wrapper.KeyExists("house");

            Assert.False(exists);

        }


        [Fact]
        public void MemberExists()
        {
            var dictionary = LoadSampleData();
            var wrapper = new DictionaryWrapper(dictionary);

            var exists = wrapper.MemberExists("play", "football");

            Assert.True(exists);
        }


        [Fact]
        public void MemberDoesNotExist()
        {
            var dictionary = LoadSampleData();
            var wrapper = new DictionaryWrapper(dictionary);

            var exists = wrapper.MemberExists("play", "basketball");

            Assert.False(exists);
        }


        [Fact]
        public void AllMembersWorks()
        {
            var dictionary = LoadSampleData();
            var wrapper = new DictionaryWrapper(dictionary);

            var allMembers = wrapper.AllMembers().Count();

            Assert.Equal(10, allMembers);
        }


        [Fact]
        public void ItemsWorks()
        {
            var dictionary = LoadSampleData();
            var wrapper = new DictionaryWrapper(dictionary);

            var items = wrapper.Items().Count();

            Assert.Equal(10, items);
        }

        private Dictionary<string, IEnumerable<string>> LoadSampleData()
        {
            var json = File.ReadAllText("./testData.json");
            var data = JsonSerializer.Deserialize<Dictionary<string,IEnumerable<string>>>(json);

            return data;
        }

    }
}