// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System.ComponentModel.DataAnnotations;

namespace SimplePages.Persistence
{
    public class Customer
    {
        public int Id { get; set; }
        
        [Required, StringLength(10)]
        public string Name { get; set; }
    }
}