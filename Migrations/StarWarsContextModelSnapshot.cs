using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace GraphQL.Benchmarks.Migrations
{
    [DbContext(typeof(StarWarsContext))]
    partial class StarWarsContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("GraphQL.Benchmarks.Droid", b =>
                {
                    b.Property<int>("DroidId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("PrimaryFunction");

                    b.HasKey("DroidId");

                    b.ToTable("Droids");
                });

            modelBuilder.Entity("GraphQL.Benchmarks.DroidAppearance", b =>
                {
                    b.Property<int>("DroidAppearanceId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DroidId");

                    b.Property<int>("EpisodeId");

                    b.HasKey("DroidAppearanceId");

                    b.HasIndex("DroidId");

                    b.HasIndex("EpisodeId");

                    b.ToTable("DroidAppearances");
                });

            modelBuilder.Entity("GraphQL.Benchmarks.Episode", b =>
                {
                    b.Property<int>("EpisodeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<string>("Year");

                    b.HasKey("EpisodeId");

                    b.ToTable("Episodes");
                });

            modelBuilder.Entity("GraphQL.Benchmarks.Friendship", b =>
                {
                    b.Property<int>("FriendshipId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("DroidId");

                    b.Property<int>("HumanId");

                    b.HasKey("FriendshipId");

                    b.HasIndex("DroidId");

                    b.HasIndex("HumanId");

                    b.ToTable("Friendships");
                });

            modelBuilder.Entity("GraphQL.Benchmarks.Human", b =>
                {
                    b.Property<int>("HumanId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("HomePlanet");

                    b.Property<string>("Name");

                    b.HasKey("HumanId");

                    b.ToTable("Humans");
                });

            modelBuilder.Entity("GraphQL.Benchmarks.HumanAppearance", b =>
                {
                    b.Property<int>("HumanAppearanceId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EpisodeId");

                    b.Property<int>("HumanId");

                    b.HasKey("HumanAppearanceId");

                    b.HasIndex("EpisodeId");

                    b.HasIndex("HumanId");

                    b.ToTable("HumanAppearances");
                });

            modelBuilder.Entity("GraphQL.Benchmarks.DroidAppearance", b =>
                {
                    b.HasOne("GraphQL.Benchmarks.Droid", "Droid")
                        .WithMany("Appearances")
                        .HasForeignKey("DroidId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GraphQL.Benchmarks.Episode", "Episode")
                        .WithMany("DroidAppearances")
                        .HasForeignKey("EpisodeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GraphQL.Benchmarks.Friendship", b =>
                {
                    b.HasOne("GraphQL.Benchmarks.Droid", "Droid")
                        .WithMany("Friendships")
                        .HasForeignKey("DroidId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GraphQL.Benchmarks.Human", "Human")
                        .WithMany("Friendships")
                        .HasForeignKey("HumanId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("GraphQL.Benchmarks.HumanAppearance", b =>
                {
                    b.HasOne("GraphQL.Benchmarks.Episode", "Episode")
                        .WithMany("HumanAppearances")
                        .HasForeignKey("EpisodeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("GraphQL.Benchmarks.Human", "Human")
                        .WithMany("Appearances")
                        .HasForeignKey("HumanId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
