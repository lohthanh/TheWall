using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Filters;
using TheWall.Models;

namespace TheWall.Controllers;

public class MessageController : Controller
{
    private readonly ILogger<MessageController> _logger;
    private MyContext DB;

    public MessageController(ILogger<MessageController> logger, MyContext context)
    {
        _logger = logger;
        DB = context;
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    //route to view dashboard once logged in, view all messages
    [HttpGet("messages")]
    public IActionResult Dashboard()
    {
        if (HttpContext.Session.GetInt32("UUID") == null)
        {
            return RedirectToAction("Index", "User");
        }

        MyViewModel MyModels = new MyViewModel
        {
            AllMessages = DB.Messages.ToList()
        };

        return View(MyModels);
    }

    //route form to create a message
    [HttpPost("messages/create")]
    public IActionResult CreateMessage(Message newMessage)
    {
        if (!ModelState.IsValid)
        {
            return View("Dashboard", "MyViewModel");
        }

        newMessage.UserId = (int)HttpContext.Session.GetInt32("UUID");
        DB.Messages.Add(newMessage);
        DB.SaveChanges();
        return RedirectToAction("Dashboard", newMessage);
    }

}
