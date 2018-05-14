namespace ShoppingGo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MobileNoAddedToOrderModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "MobileNo", c => c.String(nullable: false, maxLength: 10));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "MobileNo");
        }
    }
}
