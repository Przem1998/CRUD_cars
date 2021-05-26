using Microsoft.EntityFrameworkCore.Migrations;

namespace UsedCarDealer.Data.StoredProcedures.AddOrEditCar
{
    public partial class spAddOrEditCar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"
                IF EXISTS (SELECT * FROM sys.procedures WHERE name= 'AddOrEditCar' AND type='P')
                DROP PROCEDURE [dbo].[AddOrEditCar]
                GO
                create procedure AddOrEditCar
                @CarId int,
                @CarBrand varchar(50),
                @CarModel varchar(50),
                @YearProduction int,
                @Price money
                as
                set nocount on;

                if @CarId = 0
                begin
	                insert into Car values(@CarBrand,@CarModel,@YearProduction,@Price)
                end
                else
                begin
	                update Car
	                set
		                brand=@CarBrand,
		                model=@CarModel,
		                yearProduction=@YearProduction,
		                price=@Price
	                where id=@CarId
                end
            ";
            migrationBuilder.Sql(sql);
            //migrationBuilder.CreateTable(
            //    name: "Car",
            //    columns: table => new
            //    {
            //        Id = table.Column<int>(type: "int", nullable: false)
            //            .Annotation("SqlServer:Identity", "1, 1"),
            //        Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
            //        YearProduction = table.Column<int>(type: "int", nullable: false),
            //        Price = table.Column<decimal>(type: "decimal(18,4)", nullable: false)
            //    },
            //    constraints: table =>
            //    {
            //        table.PrimaryKey("PK_Car", x => x.Id);
            //    });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROC AddOrEditCar");
        }
    }
}
