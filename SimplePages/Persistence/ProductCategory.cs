// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace SimplePages.Persistence
{
    public class ProductCategory
    {
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}