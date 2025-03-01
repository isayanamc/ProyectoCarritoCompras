using Microsoft.AspNetCore.Mvc;
using CoreApp;   
using DTO;     
using System;
using System.Collections.Generic;


[ApiController]
[Route("api/Product")]
public class ProductController : ControllerBase
{
    private readonly ProductManager productManager = new ProductManager();

    [HttpPost("Create")]
    public IActionResult Create([FromBody] Product product)
    {
        try
        {
            productManager.Create(product);
            return Ok("Producto creado correctamente");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpGet("RetrieveAll")]
    public IActionResult RetrieveAll()
    {
        return Ok(productManager.RetrieveAll());
    }

    [HttpPut("Update")]
    public IActionResult Update([FromBody] Product product)
    {
        try
        {
            productManager.Update(product);
            return Ok("Producto actualizado correctamente");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpDelete("Delete/{id}")]
    public IActionResult Delete(int id)
    {
        try
        {
            productManager.Delete(id);
            return Ok("Producto eliminado correctamente.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }



}
