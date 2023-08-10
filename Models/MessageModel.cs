#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace TheWall.Models;

public class Message

{
    [Key]
    public int MessageId { get; set; }

    [Required]
    public string? MessageText { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    [Required]
    public int UserId { get; set; }

    public List<Comment> MessagesWithUsers { get; set; } = new List<Comment>();

}