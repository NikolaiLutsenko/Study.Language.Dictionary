using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lang.Dictionary.Adapter.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class AddWordList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "word_list",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    owner_id = table.Column<Guid>(type: "uuid", nullable: true),
                    created_at = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_word_list", x => x.id);
                    table.ForeignKey(
                        name: "FK_word_list_user_owner_id",
                        column: x => x.owner_id,
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "word_to_word_list",
                columns: table => new
                {
                    WordId = table.Column<Guid>(type: "uuid", nullable: false),
                    WordListId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_word_to_word_list", x => new { x.WordId, x.WordListId });
                    table.ForeignKey(
                        name: "FK_word_to_word_list_word_WordId",
                        column: x => x.WordId,
                        principalTable: "word",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_word_to_word_list_word_list_WordListId",
                        column: x => x.WordListId,
                        principalTable: "word_list",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_word_list_owner_id",
                table: "word_list",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "IX_word_to_word_list_WordListId",
                table: "word_to_word_list",
                column: "WordListId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "word_to_word_list");

            migrationBuilder.DropTable(
                name: "word_list");
        }
    }
}
