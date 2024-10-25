using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalBlog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCommentLikes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleUser_Articles_ArticleLikesId",
                table: "ArticleUser");

            migrationBuilder.DropTable(
                name: "CommentUser");

            migrationBuilder.RenameColumn(
                name: "Files",
                table: "Comments",
                newName: "MediaUrls");

            migrationBuilder.RenameColumn(
                name: "ArticleLikesId",
                table: "ArticleUser",
                newName: "ArticleLikedId");

            migrationBuilder.RenameColumn(
                name: "Message",
                table: "Articles",
                newName: "Text");

            migrationBuilder.RenameColumn(
                name: "Files",
                table: "Articles",
                newName: "MediaUrls");

            migrationBuilder.AddColumn<string>(
                name: "Base64Avatar",
                table: "Users",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsPublished",
                table: "Articles",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleUser_Articles_ArticleLikedId",
                table: "ArticleUser",
                column: "ArticleLikedId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleUser_Articles_ArticleLikedId",
                table: "ArticleUser");

            migrationBuilder.DropColumn(
                name: "Base64Avatar",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsPublished",
                table: "Articles");

            migrationBuilder.RenameColumn(
                name: "MediaUrls",
                table: "Comments",
                newName: "Files");

            migrationBuilder.RenameColumn(
                name: "ArticleLikedId",
                table: "ArticleUser",
                newName: "ArticleLikesId");

            migrationBuilder.RenameColumn(
                name: "Text",
                table: "Articles",
                newName: "Message");

            migrationBuilder.RenameColumn(
                name: "MediaUrls",
                table: "Articles",
                newName: "Files");

            migrationBuilder.CreateTable(
                name: "CommentUser",
                columns: table => new
                {
                    CommentLikesId = table.Column<long>(type: "bigint", nullable: false),
                    LikesId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentUser", x => new { x.CommentLikesId, x.LikesId });
                    table.ForeignKey(
                        name: "FK_CommentUser_Comments_CommentLikesId",
                        column: x => x.CommentLikesId,
                        principalTable: "Comments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommentUser_Users_LikesId",
                        column: x => x.LikesId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommentUser_LikesId",
                table: "CommentUser",
                column: "LikesId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleUser_Articles_ArticleLikesId",
                table: "ArticleUser",
                column: "ArticleLikesId",
                principalTable: "Articles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
