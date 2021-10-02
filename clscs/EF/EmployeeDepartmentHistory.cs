// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace clscs.EF
{
    public class EmployeeDepartmentHistory
    {
        public int BusinessEntityId { get; set; }

        public Employee Employee { get; set; }
        public short DepartmentId { get; set; }
        public Department Department { get; set; }
        public byte ShiftId { get; set; }

        public Shift Shift { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}