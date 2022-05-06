using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CBT.Data.Migrations
{
    public partial class AddAdminUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO [Security].[Users] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [Age], [Gendre], [FullName], [ProfilePicture]) VALUES (N'eb6b35f7-6134-4543-9df7-61cf804febd9', N'admin', N'ADMIN', N'admin@gmail.com', N'ADMIN@GMAIL.COM', 0, N'AQAAAAEAACcQAAAAECe0E8oFPCZqbEPcTFo/YRMWDQXzJXAvjYp3M3UVip+EfLHCv8IWITMNAbXy7TdawQ==', N'NQLTFGLUKBPSCMIXONRB4VB4A73ZH26Z', N'f658f386-c272-4279-a8d4-7499bfd0d01b', NULL, 0, 0, NULL, 1, 0, 45, 0, N'mohamedfatoh', NULL)");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [Security].[Users] WHERE Id='eb6b35f7-6134-4543-9df7-61cf804febd9'");
        }
    }
}
