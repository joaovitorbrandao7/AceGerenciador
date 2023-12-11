using AceGerenciador.Data;
using AceGerenciador.Models;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : Controller
{
    [HttpGet]
    public IActionResult Get([FromServices] AppDbContext context)
    {
        return Ok(context.Cliente.ToList());
    }

    [HttpGet("{id:int}")]
    public IActionResult GetById([FromRoute] int id, [FromServices] AppDbContext context)
    {
        var clienteModel = context.Cliente.FirstOrDefault(x => x.IdCliente == id);

        if (clienteModel == null)
        {
            return NotFound();
        }

        return Ok(clienteModel);
    }

    [HttpPost]
    public IActionResult Post([FromBody] Clientes clienteModel, [FromServices] AppDbContext context)
    {
        context.Cliente.Add(clienteModel);
        context.SaveChanges();
        return Created($"/{clienteModel.IdCliente}", clienteModel);
    }

    [HttpPut("{id:int}")]
    public IActionResult Put([FromRoute] int id, [FromBody] Clientes clienteModel, [FromServices] AppDbContext context)
    {
        var model = context.Cliente.FirstOrDefault(x => x.IdCliente == id);
        if (model == null)
        {
            return NotFound();
        }

        model.NomeCliente = clienteModel.NomeCliente;
        model.EmailCliente = clienteModel.EmailCliente;
        model.TelefoneCliente = clienteModel.TelefoneCliente;
        model.EnderecoCliente = clienteModel.EnderecoCliente;
        model.TipoPet = clienteModel.TipoPet;

        context.Cliente.Update(model);
        context.SaveChanges();
        return Ok(model);
    }

    [HttpDelete("{id:int}")]
    public IActionResult Delete([FromRoute] int id, [FromServices] AppDbContext context)
    {
        var model = context.Cliente.FirstOrDefault(x => x.IdCliente == id);
        if (model == null)
        {
            return NotFound();
        }

        context.Cliente.Remove(model);
        context.SaveChanges();
        return Ok(model);
    }
}
