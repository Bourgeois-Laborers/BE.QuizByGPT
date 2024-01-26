using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BE.QuizByGPT.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserAnswer_Question_QuestionId",
                table: "UserAnswer");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSession_QuizSession_QuizSessionId",
                table: "UserSession");

            migrationBuilder.DropIndex(
                name: "IX_UserSession_QuizSessionId",
                table: "UserSession");

            migrationBuilder.DropIndex(
                name: "IX_UserAnswer_QuestionId",
                table: "UserAnswer");

            migrationBuilder.DropColumn(
                name: "QuizSessionId",
                table: "UserSession");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "UserAnswer");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "QuizSession");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Quiz");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "QuizSession",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "State",
                table: "Quiz",
                newName: "Status");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "QuizSession",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "QuizSession",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedBy",
                table: "Quiz",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "QuestionsCount",
                table: "Quiz",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "UserSessionQuizSession",
                columns: table => new
                {
                    UserSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    QuizSessionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsUserReady = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSessionQuizSession", x => new { x.QuizSessionId, x.UserSessionId });
                    table.ForeignKey(
                        name: "FK_UserSessionQuizSession_QuizSession_QuizSessionId",
                        column: x => x.QuizSessionId,
                        principalTable: "QuizSession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserSessionQuizSession_UserSession_UserSessionId",
                        column: x => x.UserSessionId,
                        principalTable: "UserSession",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserSessionQuizSession_UserSessionId",
                table: "UserSessionQuizSession",
                column: "UserSessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSessionQuizSession");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "QuizSession");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "QuizSession");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Quiz");

            migrationBuilder.DropColumn(
                name: "QuestionsCount",
                table: "Quiz");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "QuizSession",
                newName: "State");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Quiz",
                newName: "State");

            migrationBuilder.AddColumn<Guid>(
                name: "QuizSessionId",
                table: "UserSession",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "QuestionId",
                table: "UserAnswer",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "QuizSession",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Quiz",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_UserSession_QuizSessionId",
                table: "UserSession",
                column: "QuizSessionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAnswer_QuestionId",
                table: "UserAnswer",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserAnswer_Question_QuestionId",
                table: "UserAnswer",
                column: "QuestionId",
                principalTable: "Question",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSession_QuizSession_QuizSessionId",
                table: "UserSession",
                column: "QuizSessionId",
                principalTable: "QuizSession",
                principalColumn: "Id");
        }
    }
}
