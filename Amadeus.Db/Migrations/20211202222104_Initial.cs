using Amadeus.Db.Enums;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Amadeus.Db.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:discord_entity_type", "channel,role");

            migrationBuilder.CreateTable(
                name: "command_config",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    is_module = table.Column<bool>(type: "boolean", nullable: false),
                    is_enabled = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_command_config", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "guilds",
                columns: table => new
                {
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_guilds", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "configs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    config_option_id = table.Column<int>(type: "integer", nullable: false),
                    guild_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_configs", x => x.id);
                    table.ForeignKey(
                        name: "fk_configs_guilds_guild_id",
                        column: x => x.guild_id,
                        principalTable: "guilds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "discord_entities",
                columns: table => new
                {
                    id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    guild_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    discord_entity_type = table.Column<DiscordEntityType>(type: "discord_entity_type", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_discord_entities", x => x.id);
                    table.ForeignKey(
                        name: "fk_discord_entities_guilds_guild_id",
                        column: x => x.guild_id,
                        principalTable: "guilds",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "command_config_discord_entity_assignment",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    command_config_id = table.Column<int>(type: "integer", nullable: false),
                    discord_entity_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false),
                    is_blacklist = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_command_config_discord_entity_assignment", x => x.id);
                    table.ForeignKey(
                        name: "fk_command_config_discord_entity_assignment_command_config_com",
                        column: x => x.command_config_id,
                        principalTable: "command_config",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_command_config_discord_entity_assignment_discord_entities_d",
                        column: x => x.discord_entity_id,
                        principalTable: "discord_entities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "self_assign_menu",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    required_role_id = table.Column<decimal>(type: "numeric(20,0)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_self_assign_menu", x => x.id);
                    table.ForeignKey(
                        name: "fk_self_assign_menu_discord_entities_required_role_id",
                        column: x => x.required_role_id,
                        principalTable: "discord_entities",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "self_assign_menu_discord_entity_assignment",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    self_assign_menu_id = table.Column<int>(type: "integer", nullable: false),
                    discord_entity_id = table.Column<decimal>(type: "numeric(20,0)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_self_assign_menu_discord_entity_assignment", x => x.id);
                    table.ForeignKey(
                        name: "fk_self_assign_menu_discord_entity_assignment_discord_entities",
                        column: x => x.discord_entity_id,
                        principalTable: "discord_entities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_self_assign_menu_discord_entity_assignment_self_assign_menu",
                        column: x => x.self_assign_menu_id,
                        principalTable: "self_assign_menu",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_command_config_discord_entity_assignment_command_config_id",
                table: "command_config_discord_entity_assignment",
                column: "command_config_id");

            migrationBuilder.CreateIndex(
                name: "ix_command_config_discord_entity_assignment_discord_entity_id",
                table: "command_config_discord_entity_assignment",
                column: "discord_entity_id");

            migrationBuilder.CreateIndex(
                name: "ix_configs_guild_id",
                table: "configs",
                column: "guild_id");

            migrationBuilder.CreateIndex(
                name: "ix_discord_entities_guild_id",
                table: "discord_entities",
                column: "guild_id");

            migrationBuilder.CreateIndex(
                name: "ix_self_assign_menu_required_role_id",
                table: "self_assign_menu",
                column: "required_role_id");

            migrationBuilder.CreateIndex(
                name: "ix_self_assign_menu_discord_entity_assignment_discord_entity_id",
                table: "self_assign_menu_discord_entity_assignment",
                column: "discord_entity_id");

            migrationBuilder.CreateIndex(
                name: "ix_self_assign_menu_discord_entity_assignment_self_assign_menu",
                table: "self_assign_menu_discord_entity_assignment",
                column: "self_assign_menu_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "command_config_discord_entity_assignment");

            migrationBuilder.DropTable(
                name: "configs");

            migrationBuilder.DropTable(
                name: "self_assign_menu_discord_entity_assignment");

            migrationBuilder.DropTable(
                name: "command_config");

            migrationBuilder.DropTable(
                name: "self_assign_menu");

            migrationBuilder.DropTable(
                name: "discord_entities");

            migrationBuilder.DropTable(
                name: "guilds");
        }
    }
}
