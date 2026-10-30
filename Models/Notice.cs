using System.ComponentModel.DataAnnotations;

namespace CommunityNoticeBoard.Models;

public class Notice
{
    public int Id { get; set; }

    [Required, StringLength(120)]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(40)]
    public string Category { get; set; } = "General";

    [Required, StringLength(3000)]
    [Display(Name = "Notice Details")]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Publish Date")]
    [DataType(DataType.Date)]
    public DateTime PublishedOn { get; set; } = DateTime.Today;

    [Display(Name = "Important Notice")]
    public bool IsImportant { get; set; }

    [StringLength(100)]
    public string? AttachmentUrl { get; set; }
}
