// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

namespace PatchApiSample.Controllers;

public class Customer
{
    public string? CustomerName { get; set; }
    public List<Order>? Orders { get; set; }
}

public class Order
{
    public string OrderName { get; set; }
    public string OrderType { get; set; }
}

