using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Gerenciador_de_Tarefas.Models;

[Route("tarefas")]
public class TarefasController : Controller
{
    private readonly AppDbContext _context;

    public TarefasController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var tarefas = await _context.Tarefas.ToListAsync();
        return View(tarefas);
    }

    [HttpGet("criar")]
    public IActionResult Criar()
    {
        return View();
    }

    [HttpPost("criar")]
    public async Task<IActionResult> Criar(Tarefa tarefa)
    {
        if (ModelState.IsValid)
        {
            tarefa.DataCriada = DateTime.Now;
            tarefa.Progresso = 0;
            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return View(tarefa);
    }

    [HttpGet("editar/{id}")]
    public async Task<IActionResult> Editar(int id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);
        if (tarefa == null) return NotFound();
        return View(tarefa);
    }

    [HttpPost("editar/{id}")]
    public async Task<IActionResult> Editar(int id, Tarefa tarefa)
    {
        if (id != tarefa.Id) return NotFound();
        if (ModelState.IsValid)
        {
            _context.Update(tarefa);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        return View(tarefa);
    }

    [HttpPost("excluir/{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);
        if (tarefa == null) return NotFound();
        _context.Tarefas.Remove(tarefa);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    [HttpPost("concluir/{id}")]
    public async Task<IActionResult> Concluir(int id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);
        if (tarefa == null) return NotFound();
        tarefa.Progresso = 1;
        _context.Update(tarefa);
        await _context.SaveChangesAsync();
        return RedirectToAction("Index");
    }
}
