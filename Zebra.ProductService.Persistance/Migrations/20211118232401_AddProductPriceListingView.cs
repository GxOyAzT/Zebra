using Microsoft.EntityFrameworkCore.Migrations;

namespace Zebra.ProductService.Persistance.Migrations
{
    public partial class AddProductPriceListingView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sqlCreateOrAlterView = @"CREATE OR ALTER VIEW dbo.ProductPriceListring AS
                                        SELECT * FROM
                                        (select p.ProductId AS ProductId, MAX(p.Cost) AS Cost from dbo.Prices p 
                                        WHERE [From] <= GETDATE()
                                        GROUP BY ProductId) as X
                                        left join Products prod on prod.Id = X.ProductId";

            migrationBuilder.Sql(sqlCreateOrAlterView);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP VIEW ProductPriceListring");
        }
    }
}
