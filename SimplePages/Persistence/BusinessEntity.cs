// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace SimplePages.Persistence
{
    public class BusinessEntity
    {
        public int BusinessEntityId { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}