using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SimpleBlog.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "blog_comment",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    Author = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    CreatedDateUTC = table.Column<DateTime>(nullable: false),
                    PostId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog_comment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "blog_configuration",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    CreatedDateUTC = table.Column<DateTime>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog_configuration", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "blog_post",
                columns: table => new
                {
                    Id = table.Column<string>(maxLength: 50, nullable: false),
                    Author = table.Column<string>(nullable: true),
                    Content = table.Column<string>(nullable: true),
                    CreatedDateUTC = table.Column<DateTime>(nullable: false),
                    Excerpt = table.Column<string>(nullable: true),
                    SeoName = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_blog_post", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blog_comment");

            migrationBuilder.DropTable(
                name: "blog_configuration");

            migrationBuilder.DropTable(
                name: "blog_post");
        }
    }
}
