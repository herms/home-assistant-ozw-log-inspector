using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace ConsoleApp2
{
    class Program
    {
        private static readonly Regex Pattern = new Regex(@"(\d{4}-\d{2}-\d{2}\s\d{2}:\d{2}:\d{2}\.\d{3}).*Node(\d{3})");

        static void Main(string[] args)
        {
            var dict = new Dictionary<string, int>();
            var reader = File.OpenText(args[0]);
            int i = 0;
            int cap = 2000000;
            var previousTimestamp = "";
            while (!reader.EndOfStream && i < cap)
            {
                var line = reader.ReadLine();
                i++;
                if(i%100000==0) Console.WriteLine("Read "+i+" lines...");
                if (line != null && Pattern.IsMatch(line))
                {
                    var match = Pattern.Match(line);
                    var timestamp = match.Groups["1"].Value;
                    
                    var nodeId = match.Groups["2"].Value;
                    if (dict.ContainsKey(nodeId)) { 
                        if(timestamp!=previousTimestamp)
                            dict[nodeId]++;
                    }
                    else dict[nodeId] = 1;

                    previousTimestamp = timestamp;
                }
            }

            Console.WriteLine("-- Results ("+dict.Keys.Count+" devices) --");
            foreach (var i1 in dict.OrderByDescending(kv => kv.Value))
            {
                /*if (int.Parse(i1.Key) > 072)
                {
                    Console.WriteLine("*" + i1.Key + " " + i1.Value);
                }
                else*/
                    Console.WriteLine(i1.Key + " " + i1.Value);
            }
        }
    }
}