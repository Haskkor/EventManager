using System.Data.Entity;

namespace _3NetEventManagerS.Models
{
    public class EventManagerSContext : DbContext
    {
        public DbSet<_3NetEventManagerS.Models.Contribution> Contribution { get; set; }

        public DbSet<_3NetEventManagerS.Models.ContributionType> ContributionType { get; set; }

        public DbSet<_3NetEventManagerS.Models.Event> Event { get; set; }

        public DbSet<_3NetEventManagerS.Models.EventStatus> EventStatus { get; set; }

        public DbSet<_3NetEventManagerS.Models.EventType> EventType { get; set; }

        public DbSet<_3NetEventManagerS.Models.Role> Role { get; set; }

        public DbSet<_3NetEventManagerS.Models.User> User { get; set; }
    }
}