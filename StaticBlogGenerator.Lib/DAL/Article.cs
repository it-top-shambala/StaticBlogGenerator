using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StaticBlogGenerator.Lib.DAL;

[Table("table_articles")]
public class Article
{
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    [Column("id")]
    [Key]
    public Guid Guid { get; set; }

    [Column("title")] [Required] public string Title { get; set; }

    [Column("content")] [Required] public string Content { get; set; }

    [Column("creation_date")] [Required] public DateTime CreationDate { get; set; }

    [Column("last_modification_date")]
    [Required]
    public DateTime LastModificationDate { get; set; }
}
