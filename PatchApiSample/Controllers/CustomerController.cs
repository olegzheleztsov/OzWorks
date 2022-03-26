// Copyright (c) Zheleztsov Oleh.All Rights Reserved.

using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace PatchApiSample.Controllers;

[ApiController]
public class CustomerController : ControllerBase
{
    
    [Route("customer")]
    [HttpPatch]
    public IActionResult JsonPatchWithModelState([FromBody] JsonPatchDocument<Customer>? patchDocument)
    {
        if (patchDocument == null)
        {
            return BadRequest(ModelState);
        }

        var customer = new Customer()
        {
            CustomerName = "Oleh",
            Orders = new List<Order>()
            {
                new Order() {OrderName = "Ord1", OrderType = "Ord1Type"},
                new Order() {OrderName = "Ord2", OrderType = "Ord2Type"}
            }
        };
        patchDocument.ApplyTo(customer, ModelState);
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        return new ObjectResult(customer);
    }
}