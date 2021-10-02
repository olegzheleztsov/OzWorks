// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace SimplePages.Persistence
{
    public class ProductSubcategory
    {
        public int ProductSubcategoryId { get; set; }
        public int ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
        public string Name { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}