using Microsoft.EntityFrameworkCore.Migrations;

namespace Flowershop.API.Migrations
{
    public partial class InsertDatas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Shops",
                columns: new[] { "Id", "Name", "StreetName", "StreetNumber", "Region" },
                values: new object[,]
                {
                    {1, "Het Blomke", "Leuvensestraat", "46", "Lo"},
                    {2, "Bloemenwinkel Mechelen Borze", "Borzestraat", "10", "Mechelen"},
                    {3, "Mijn Boeketje", "Veemarkt", "8", "Mechelen"}
                    
                });
            migrationBuilder.InsertData(
                table: "Bouquets",
                columns: new[] { "Id", "ShopId", "Name", "Price", "Description" },
                values: new object[,]
                {
                    {1, 3, "Tulips", 20, "yellow tulips"},
                    {2, 2, "Roses", 15, "White roses"},
                    {3, 1, "Cotton Flower", 15, "Fluffy"},
                    {4, 2, "Red Tulips", 19, "Red tulips"},
                    {5, 1, "Red Roses", 20, "White roses"},
                    {6, 3, "Cotton Flower", 15, "Fluffy"}
                    
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
