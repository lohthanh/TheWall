#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TheWall.Models;

public class MyViewModel 
{   
    [Required]
    public Message? Message { get; set; }
    
    public List<Message> AllMessages { get; set; } = new List<Message>();
}