using Microsoft.EntityFrameworkCore.Migrations;
using NpgsqlTypes;

#nullable disable

namespace TheAggregate.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddFTSearchToFeedAndFeedItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<NpgsqlTsVector>(
                name: "SearchVector",
                table: "Feeds",
                type: "tsvector",
                nullable: false)
                .Annotation("Npgsql:TsVectorConfig", "english")
                .Annotation("Npgsql:TsVectorProperties", new[] { "Title", "Description" });

            migrationBuilder.AddColumn<NpgsqlTsVector>(
                name: "SearchVector",
                table: "FeedItems",
                type: "tsvector",
                nullable: false)
                .Annotation("Npgsql:TsVectorConfig", "english")
                .Annotation("Npgsql:TsVectorProperties", new[] { "Title", "Summary" });

            migrationBuilder.CreateIndex(
                name: "IX_Feeds_FeedUrl",
                table: "Feeds",
                column: "FeedUrl",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Feeds_SearchVector",
                table: "Feeds",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");

            migrationBuilder.CreateIndex(
                name: "IX_Feeds_Title",
                table: "Feeds",
                column: "Title");

            migrationBuilder.CreateIndex(
                name: "IX_Feeds_WebUrl",
                table: "Feeds",
                column: "WebUrl",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeedItems_FeedId",
                table: "FeedItems",
                column: "FeedId");

            migrationBuilder.CreateIndex(
                name: "IX_FeedItems_SearchVector",
                table: "FeedItems",
                column: "SearchVector")
                .Annotation("Npgsql:IndexMethod", "GIN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Feeds_FeedUrl",
                table: "Feeds");

            migrationBuilder.DropIndex(
                name: "IX_Feeds_SearchVector",
                table: "Feeds");

            migrationBuilder.DropIndex(
                name: "IX_Feeds_Title",
                table: "Feeds");

            migrationBuilder.DropIndex(
                name: "IX_Feeds_WebUrl",
                table: "Feeds");

            migrationBuilder.DropIndex(
                name: "IX_FeedItems_FeedId",
                table: "FeedItems");

            migrationBuilder.DropIndex(
                name: "IX_FeedItems_SearchVector",
                table: "FeedItems");

            migrationBuilder.DropColumn(
                name: "SearchVector",
                table: "Feeds");

            migrationBuilder.DropColumn(
                name: "SearchVector",
                table: "FeedItems");
        }
    }
}
