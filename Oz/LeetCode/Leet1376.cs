// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using System;

namespace Oz.LeetCode;

public class Leet1376
{
    public int NumOfMinutes(int n, int headID, int[] manager, int[] informTime)
    {
        Employee[] employees = new Employee[n];

        for (int i = 0; i < n; i++)
        {
            employees[i] = new Employee() {CurrentTime = informTime[i]};
        }

        for (int i = 0; i < n; i++)
        {
            Visit(i, manager[i], manager, informTime, employees);
        }

        return employees[headID].CurrentTime;
    }

    private void Visit(int childIndex, int eIndex, int[] managers, int[] informTime, Employee[] employees)
    {
        if (eIndex == -1)
        {
            return;
        }
        employees[eIndex].CurrentTime = Math.Max(employees[eIndex].CurrentTime,
            informTime[eIndex] + employees[childIndex].CurrentTime);
        
        Visit(eIndex, managers[eIndex], managers, informTime, employees);
    }
    
    public class Employee
    {
        public int CurrentTime { get; set; }
    }
}