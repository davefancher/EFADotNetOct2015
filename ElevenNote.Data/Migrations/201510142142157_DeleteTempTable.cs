namespace ElevenNote.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteTempTable : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Temp");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Temp",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
