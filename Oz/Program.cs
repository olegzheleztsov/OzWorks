using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Oz.Algorithms;
using Oz.Algorithms.DataStructures;
using Oz.Algorithms.Sort;
using Oz.Config;
using Oz.Graph;


//TestSingleLinkedList();

// void TestSingleLinkedList()
// {
//     OzSingleLinkedList<int> numbers = new OzSingleLinkedList<int>();
//     numbers.InsertLastRange(new List<int>() {1, 2, 3});
//     Console.WriteLine(string.Join(" ", numbers));
//     numbers.Clear();
//     numbers.InsertLastRange(new []{4, 5});
//     Console.WriteLine(string.Join(" ", numbers));
// }

// GraphCase graphCase = new GraphCase();
// graphCase.SlowAllPairsShortestPaths();

// Initialize();
//
// static void Initialize()
// {
//     var builder = new ConfigurationBuilder()
//         .SetBasePath(Directory.GetCurrentDirectory())
//         .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
//         .AddUserSecrets<CityGraphGenerator>()
//         .AddEnvironmentVariables();
//     IConfigurationRoot configurationRoot = builder.Build();
//     CitiesConfig citiesConfig = new CitiesConfig();
//     configurationRoot.GetSection("Cities").Bind(citiesConfig);
//     
//     Console.WriteLine($"Path: {citiesConfig.WorldCitiesFilePath}, File Size: {new FileInfo(citiesConfig.WorldCitiesFilePath).Length}");
// }
//
