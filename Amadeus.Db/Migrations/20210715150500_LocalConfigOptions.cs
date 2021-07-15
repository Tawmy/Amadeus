using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Amadeus.Db.Migrations
{
    public partial class LocalConfigOptions : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_configs_config_options_config_option_id",
                table: "configs");

            migrationBuilder.DropTable(
                name: "config_options");

            migrationBuilder.DropTable(
                name: "config_option_categories");

            migrationBuilder.DropIndex(
                name: "ix_configs_config_option_id",
                table: "configs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "config_option_categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_config_option_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "config_options",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    config_option_category_id = table.Column<int>(type: "integer", nullable: false),
                    cs_type = table.Column<int>(type: "integer", nullable: false),
                    default_value = table.Column<string>(type: "text", nullable: true),
                    description = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_config_options", x => x.id);
                    table.ForeignKey(
                        name: "fk_config_options_config_option_categories_config_option_categ",
                        column: x => x.config_option_category_id,
                        principalTable: "config_option_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "ix_configs_config_option_id",
                table: "configs",
                column: "config_option_id");

            migrationBuilder.CreateIndex(
                name: "ix_config_options_config_option_category_id",
                table: "config_options",
                column: "config_option_category_id");

            migrationBuilder.AddForeignKey(
                name: "fk_configs_config_options_config_option_id",
                table: "configs",
                column: "config_option_id",
                principalTable: "config_options",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
