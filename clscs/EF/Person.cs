﻿// Copyright (c) Zheleztsov Oleh. All Rights Reserved.

using System;

namespace clscs.EF
{
    public class Person
    {
        public int BusinessEntityId { get; set; }

        public BusinessEntity BusinessEntity { get; set; }
        public string PersonType { get; set; }
        public bool NameStyle { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public int EmailPromotion { get; set; }
        public string AdditionalContactInfo { get; set; }
        public string Demographics { get; set; }
        public Guid rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }
        
        
    }
}