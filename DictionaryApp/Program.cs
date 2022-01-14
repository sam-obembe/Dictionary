// See https://aka.ms/new-console-template for more information
using System;
using DictionaryApp.Models;

namespace DictionaryApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            bool isRunning = true;

            var dictionary = new Dictionary<string, IEnumerable<string>>();

            while (isRunning)
            {
                Console.Write(">");
                var input = Console.ReadLine();
                var commandSegments = input.Split(' ');

                var command = commandSegments[0].ToUpper();
                var commandParams = commandSegments.Skip(1).ToArray();
                var key = " ";
                var member = " ";

                if(commandParams.Length > 0)
                {
                    key = commandParams[0];                    
                }
                if(commandParams.Length > 1)
                {
                    member = string.Join(' ', commandParams.Skip(1).ToArray()); //if the value for the key is more than one word
                }


                Command commandEnum;

                var commandTry = Enum.TryParse( command,out commandEnum);

                if (commandTry)
                {
                    RunCommand(commandEnum, dictionary, key, member);
                }
                else
                {
                    Console.WriteLine("Invalid command. Type HELP for a list of valid commands");
                }
                

            }

        }


        public static void RunCommand(Command command,  Dictionary<string, IEnumerable<string>> dictionary, string dictKey = "", string member = "")
        {
            var wrapper = new DictionaryWrapper(dictionary);
            switch (command)
            {
                case Command.KEYS:
                {
                    var keys = wrapper.Keys().Select((key,loc) => $"{loc+1}) {key}");
                    foreach(var key in keys)
                        {
                            Console.WriteLine(key);
                        }
                    break;
                }
                case Command.ADD:
                    {
                        var action = wrapper.Add(dictKey, member);
                        if (action)
                        {
                            Console.WriteLine(") Added");
                        }
                        else
                        {
                            Console.WriteLine(") ERROR, member already exists for key");
                        }
                        
                        break;
                    }
                case Command.MEMBERS:
                    {
                        var members = wrapper.Members(dictKey);
                        if(members != null && members.Count() > 0)
                        {
                            var memberLog = members.Select((member, loc) => $"{loc+1}) {member}");
                            Console.WriteLine(string.Join('\n', memberLog));
                        }
                        else
                        {
                            Console.WriteLine(") ERROR, Key does not exist");
                        }
                        
                        break;
                    }
                case Command.REMOVE:
                    {
                        var result = wrapper.Remove(dictKey, member);
                        if (result)
                        {
                            Console.WriteLine(") Removed");
                        }
                        else
                        {
                            Console.WriteLine(") ERROR, member does not exist");
                        }
                        
                        break;
                    }
                case Command.KEYEXISTS:
                    {
                        var result = wrapper.KeyExists(dictKey);
                        Console.WriteLine($") {result.ToString().ToLower()}");
                        break;
                    }
                case Command.REMOVEALL:
                    {
                        var result = wrapper.RemoveAll(dictKey);

                        if (result)
                        {
                            Console.WriteLine(") Removed");
                        }
                        else
                        {
                            Console.WriteLine(") ERROR, key does not exist");
                        }
                        
                        break;
                    }
                case Command.ITEMS:
                    {
                        var result = wrapper.Items().Select((item,i)=> $"{i+1}) {item}");
                        if (result.Count() == 0)
                        {
                            Console.WriteLine("(empty set)");
                            break;
                        }
                        else
                        {
                            var output = string.Join('\n', result);
                            Console.WriteLine(output);
                            break;
                        }
                       
                    }
                case Command.ALLMEMBERS:
                    {
                        var members = wrapper.AllMembers().Select((item,i)=>$"{i+1}) {item}");
                        if(members.Count() == 0)
                        {
                            Console.WriteLine("(empty set)");
                            break;
                        }
                        else
                        {
                            var output = string.Join('\n', members);
                            Console.WriteLine(output);
                            break;
                        }
                    }
                case Command.CLEAR:
                    {
                        bool isCleared = wrapper.Clear();
                        if (isCleared)
                        {
                            Console.WriteLine(") Cleared");
                        }
                        break;
                    }
                case Command.EXIT:
                    {
                        Environment.Exit(0);
                        break;
                    }
                case Command.HELP:
                    {
                        DisplayHelp();
                        //log commands and their arguments
                        break;
                    }
                default:
                    {
                        break;
                    }
               
            }
        }

        public static void DisplayHelp()
        {
            var text = File.ReadAllText("./Help.txt");

            Console.WriteLine(text);
        }

        
    }

}

