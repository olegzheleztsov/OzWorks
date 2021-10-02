// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace SimplePages.Persistence
{
    public class ProductModel
    {
        public int ProductModelId { get; set; }
        public string Name { get; set; }
        public string CatalogDescription { get; set; }
        public string Instructions { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}