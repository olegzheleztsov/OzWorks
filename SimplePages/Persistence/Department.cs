// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using Newtonsoft.Json;
using System;

namespace SimplePages.Persistence
{
    public sealed class Department
    {
        public short DepartmentId { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public DateTime ModifiedDate { get; set; }

        public override string ToString() =>
            JsonConvert.SerializeObject(this, Formatting.Indented);
    }
}