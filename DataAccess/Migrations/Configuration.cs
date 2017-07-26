namespace DataAccess.Migrations
{
    using global::DataAccess.Migrations.Seeds;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TAD>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(TAD context)
        {
            SeedCore.RunSeeds(context);
        }
    }
}
