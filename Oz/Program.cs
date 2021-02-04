using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Oz.Algorithms;
using Oz.Algorithms.DataStructures;
using Oz.Graph;
using Oz.RecordsSample;
using static System.Console;

TestConnectedComponents();

void TestConnectedComponents()
{
    var test = new GraphCase();
    test.TestConnectedComponents();
}

void TestTopologicalSort()
{
    var graphCase = new GraphCase();
    graphCase.RunTopologicalSort();
}

void TestDfs()
{
    var graphCase = new GraphCase();
    graphCase.RunDfsOnListGraph();
    Console.WriteLine("-----------------");
    graphCase.RunDfsOnMatrixGraph();
}

void TestBfs()
{
    var graphCase = new GraphCase();
    graphCase.RunBfs();
    WriteLine("Nex graph:");
    graphCase.RunBfsOnMatrixGraph();   
}

void TestPathFinding()
{
    var graphCase = new GraphCase();
    graphCase.VerifyPathFinding();
}

