using Microsoft.EntityFrameworkCore.Migrations;

namespace eVoting.Server.Migrations
{
    public partial class PartyEdit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PartyCandidates_Votelists_VotelistId",
                table: "PartyCandidates");

            migrationBuilder.DropForeignKey(
                name: "FK_UserVotes_Candidates_CandidateId",
                table: "UserVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserVotes_Parties_PartyId",
                table: "UserVotes");

            migrationBuilder.DropForeignKey(
                name: "FK_UserVotes_Votelists_VotelistId",
                table: "UserVotes");

            migrationBuilder.DropIndex(
                name: "IX_PartyCandidates_VotelistId",
                table: "PartyCandidates");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserVotes",
                table: "UserVotes");

            migrationBuilder.DropColumn(
                name: "VotelistId",
                table: "PartyCandidates");

            migrationBuilder.RenameTable(
                name: "UserVotes",
                newName: "VotelistParties");

            migrationBuilder.RenameIndex(
                name: "IX_UserVotes_VotelistId",
                table: "VotelistParties",
                newName: "IX_VotelistParties_VotelistId");

            migrationBuilder.RenameIndex(
                name: "IX_UserVotes_PartyId",
                table: "VotelistParties",
                newName: "IX_VotelistParties_PartyId");

            migrationBuilder.RenameIndex(
                name: "IX_UserVotes_CandidateId",
                table: "VotelistParties",
                newName: "IX_VotelistParties_CandidateId");

            migrationBuilder.AddColumn<string>(
                name: "VotelistId",
                table: "Parties",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LastName",
                table: "Candidates",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldMaxLength: 5000);

            migrationBuilder.AddPrimaryKey(
                name: "PK_VotelistParties",
                table: "VotelistParties",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Parties_VotelistId",
                table: "Parties",
                column: "VotelistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Parties_Votelists_VotelistId",
                table: "Parties",
                column: "VotelistId",
                principalTable: "Votelists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VotelistParties_Candidates_CandidateId",
                table: "VotelistParties",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VotelistParties_Parties_PartyId",
                table: "VotelistParties",
                column: "PartyId",
                principalTable: "Parties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_VotelistParties_Votelists_VotelistId",
                table: "VotelistParties",
                column: "VotelistId",
                principalTable: "Votelists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Parties_Votelists_VotelistId",
                table: "Parties");

            migrationBuilder.DropForeignKey(
                name: "FK_VotelistParties_Candidates_CandidateId",
                table: "VotelistParties");

            migrationBuilder.DropForeignKey(
                name: "FK_VotelistParties_Parties_PartyId",
                table: "VotelistParties");

            migrationBuilder.DropForeignKey(
                name: "FK_VotelistParties_Votelists_VotelistId",
                table: "VotelistParties");

            migrationBuilder.DropIndex(
                name: "IX_Parties_VotelistId",
                table: "Parties");

            migrationBuilder.DropPrimaryKey(
                name: "PK_VotelistParties",
                table: "VotelistParties");

            migrationBuilder.DropColumn(
                name: "VotelistId",
                table: "Parties");

            migrationBuilder.RenameTable(
                name: "VotelistParties",
                newName: "UserVotes");

            migrationBuilder.RenameIndex(
                name: "IX_VotelistParties_VotelistId",
                table: "UserVotes",
                newName: "IX_UserVotes_VotelistId");

            migrationBuilder.RenameIndex(
                name: "IX_VotelistParties_PartyId",
                table: "UserVotes",
                newName: "IX_UserVotes_PartyId");

            migrationBuilder.RenameIndex(
                name: "IX_VotelistParties_CandidateId",
                table: "UserVotes",
                newName: "IX_UserVotes_CandidateId");

            migrationBuilder.AddColumn<string>(
                name: "VotelistId",
                table: "PartyCandidates",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Candidates",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserVotes",
                table: "UserVotes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_PartyCandidates_VotelistId",
                table: "PartyCandidates",
                column: "VotelistId");

            migrationBuilder.AddForeignKey(
                name: "FK_PartyCandidates_Votelists_VotelistId",
                table: "PartyCandidates",
                column: "VotelistId",
                principalTable: "Votelists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserVotes_Candidates_CandidateId",
                table: "UserVotes",
                column: "CandidateId",
                principalTable: "Candidates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserVotes_Parties_PartyId",
                table: "UserVotes",
                column: "PartyId",
                principalTable: "Parties",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserVotes_Votelists_VotelistId",
                table: "UserVotes",
                column: "VotelistId",
                principalTable: "Votelists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
