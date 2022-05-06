using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CBT.Data.Migrations
{
    public partial class AssignAdminUserToAllRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("insert into [Security].[UserRoles] (UserId,RoleId) select 'eb6b35f7-6134-4543-9df7-61cf804febd9',Id from [Security].[Roles]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("delete from [Security].[UserRoles] where UserId='eb6b35f7-6134-4543-9df7-61cf804febd9'");
        }
    }
}
