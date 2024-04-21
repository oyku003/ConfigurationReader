using Microsoft.EntityFrameworkCore.Migrations;

namespace ConfigurationReader.Services.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ServiceConfiguration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceConfiguration", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "ServiceConfiguration",
                columns: new[] { "Id", "ApplicationName", "IsActive", "Name", "Type", "Value" },
                values: new object[] { 1, "SERVICE-A", (short)1, "SiteName", "String", "Boyner.com.tr" });

            migrationBuilder.InsertData(
                table: "ServiceConfiguration",
                columns: new[] { "Id", "ApplicationName", "IsActive", "Name", "Type", "Value" },
                values: new object[] { 2, "SERVICE-B", (short)1, "IsBasktetEnabled", "Boolean", "1" });

            migrationBuilder.InsertData(
                table: "ServiceConfiguration",
                columns: new[] { "Id", "ApplicationName", "IsActive", "Name", "Type", "Value" },
                values: new object[] { 3, "SERVICE-A", (short)0, "MaxItemCount", "Int", "50" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceConfiguration");
        }
    }
}
