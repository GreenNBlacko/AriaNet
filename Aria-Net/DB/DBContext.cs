using Aria_Net.DB.Classes;
using Aria_Net.IO;
using Microsoft.EntityFrameworkCore;

namespace Aria_Net.DB {
	public class DBContext : DbContext {
		public DbSet<Server> Servers { get; set; }
		public DbSet<VerifiedUsers> VerifiedUsers { get; set; }

		private Logger _logger;
		private DiscordClient _client;

		public DBContext(DiscordClient client) : base() { 
			_logger = new Logger(); 
			_client = client;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			var config = _client._config;

			optionsBuilder.UseMySql(
				connectionString:string.Format("server={0};database=s146890_s146890_Main;user={1};password={2}", config["ARIANET:CONNECTION:IP"], config["ARIANET:CONNECTION:USERNAME"], config["ARIANET:CONNECTION:PASSWORD"]), 
				ServerVersion.Create(Version.Parse("11.1.3"), Pomelo.EntityFrameworkCore.MySql.Infrastructure.ServerType.MariaDb)
			);

			_logger.Log("Database connected successfully");
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Channel>(entity => {
				entity.HasKey(e => e.ChannelID);
			});

			modelBuilder.Entity<CommandRestriction>(entity => {
				entity.HasKey(e => e.CommandRestrictionID);
				entity.HasMany(e => e.Channels);
			});

			modelBuilder.Entity<Server>(entity => {
				entity.HasKey(e => e.ServerID);
				entity.HasMany(e => e.CommandRestrictions);
				entity.HasOne(e => e.Verification);
			});

			modelBuilder.Entity<VerificationClass>(entity => {
				entity.HasKey(e => e.VerificationID);
			});

			modelBuilder.Entity<VerifiedUsers>(entity => {
				entity.HasKey(e => e.UserID);
			});
		}
	}
}
