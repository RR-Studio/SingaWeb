using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Singa.Data.Migrations
{
    public partial class AddMemberInvitationEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MemberInvitations",
                columns: table => new
                {
                    Guid = table.Column<string>(nullable: false),
                    BecameUserId = table.Column<int>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    InvitationStatus = table.Column<int>(nullable: false),
                    InvitationType = table.Column<int>(nullable: false),
                    SenderidId = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    TargetPageId = table.Column<int>(nullable: true),
                    Version = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemberInvitations", x => x.Guid);
                    table.ForeignKey(
                        name: "FK_MemberInvitations_AspNetUsers_SenderidId",
                        column: x => x.SenderidId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MemberInvitations_SenderidId",
                table: "MemberInvitations",
                column: "SenderidId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MemberInvitations");
        }
    }
}
