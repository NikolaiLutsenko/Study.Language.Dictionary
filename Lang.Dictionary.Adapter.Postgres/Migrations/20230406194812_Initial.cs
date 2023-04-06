using System;
using Lang.Dictionary.Adapter.Postgres.Users;
using Lang.Dictionary.Adapter.Postgres.Words;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lang.Dictionary.Adapter.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    content = table.Column<UserDal>(type: "jsonb", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("user_id", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "word",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    from = table.Column<TranslatedWordDal>(type: "jsonb", nullable: false),
                    to = table.Column<TranslatedWordDal>(type: "jsonb", nullable: false),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_word", x => x.id);
                    table.ForeignKey(
                        name: "FK_word_user_owner_id",
                        column: x => x.owner_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_word_owner_id",
                table: "word",
                column: "owner_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "word");

            migrationBuilder.DropTable(
                name: "user");
        }
    }
}
