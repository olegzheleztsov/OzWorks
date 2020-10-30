using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Oz
{
    public class CommandExecutor
    {
        private readonly Dictionary<string, Func<string[], object>> _argumentParsers =
            new Dictionary<string, Func<string[], object>>
            {
                ["pascal"] = PascalTriangle.ParseArguments
            };

        private readonly Dictionary<string, IExecutor> _executors = new Dictionary<string, IExecutor>
        {
            ["pascal"] = new PascalTriangle()
        };


        public async Task<bool> RunAsync(string[] args)
        {
            if (args.Length > 0)
            {
                try
                {
                    var commandName = args[0].ToLower();
                    if (_executors.ContainsKey(commandName))
                    {
                        await _executors[commandName].RunAsync(_argumentParsers[commandName](args[1..]))
                            .ConfigureAwait(false);
                        return await Task.FromResult(false);
                    }

                    if (commandName == "q" || commandName == "quit" || commandName == "exit")
                    {
                        return await Task.FromResult(true);
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(JsonConvert.SerializeObject(exception));
                }
            }
            else
            {
                Console.WriteLine("Wrong command...");
            }

            return await Task.FromResult(false);
        }
    }
}