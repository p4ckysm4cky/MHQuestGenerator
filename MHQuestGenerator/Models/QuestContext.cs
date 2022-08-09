using Microsoft.EntityFrameworkCore;


namespace MHQuestGenerator.Models
{
    public class QuestContext: DbContext
    {
        public QuestContext(DbContextOptions<QuestContext> options) : base(options) { }
        public DbSet<Quest> Quest { get; set; } = null!;
    }
}
