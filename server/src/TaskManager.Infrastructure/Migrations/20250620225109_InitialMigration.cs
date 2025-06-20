using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TaskManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedById = table.Column<string>(type: "text", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tasks_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TaskHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TaskId = table.Column<int>(type: "integer", nullable: false),
                    PreviousStatus = table.Column<int>(type: "integer", nullable: false),
                    CurrentStatus = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    UpdatedById = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TaskHistory_Tasks_TaskId",
                        column: x => x.TaskId,
                        principalTable: "Tasks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskHistory_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "944cc574-7936-4e3f-8b59-56c4af209d6f", 0, "1f2226b9-feb7-49bb-8779-ddec32a6be9e", "admin@taskmanager.com", true, true, null, "ADMIN@TASKMANAGER.COM", "ADMIN@TASKMANAGER.COM", "AQAAAAIAAYagAAAAEDNp4hs5LoG14uSkMSi++QTc4IauP3GpOv/Xl4Uhnt6MerlT3evllIZvHsNnEFue3w==", null, false, "KFH2DVXTLVQIXVPVAOWGGQU62PSGKRB4", false, "admin@taskmanager.com" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "Id", "CompletedAt", "CreatedAt", "CreatedById", "Description", "Status", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2025, 6, 20, 22, 32, 35, 0, DateTimeKind.Utc), "944cc574-7936-4e3f-8b59-56c4af209d6f", "Montar a vitrine com as novas peças da coleção de verão, destacando promoções de bermudas e camisetas.", 1, "Organizar vitrine de verão", null },
                    { 2, null, new DateTime(2025, 6, 20, 22, 32, 35, 0, DateTimeKind.Utc), "944cc574-7936-4e3f-8b59-56c4af209d6f", "Verificar a quantidade de tênis esportivos no estoque e atualizar o sistema caso haja divergências.", 1, "Conferir estoque de tênis", null },
                    { 3, null, new DateTime(2025, 6, 20, 22, 32, 35, 0, DateTimeKind.Utc), "944cc574-7936-4e3f-8b59-56c4af209d6f", "Auxiliar cliente na escolha de vestidos para festa e sugerir acessórios combinando.", 1, "Atender cliente na seção feminina", null },
                    { 4, null, new DateTime(2025, 6, 20, 22, 32, 35, 0, DateTimeKind.Utc), "944cc574-7936-4e3f-8b59-56c4af209d6f", "Inserir no sistema as novas jaquetas recebidas do fornecedor, incluindo fotos e preços.", 1, "Cadastrar novos produtos", null },
                    { 5, null, new DateTime(2025, 6, 20, 22, 32, 35, 0, DateTimeKind.Utc), "944cc574-7936-4e3f-8b59-56c4af209d6f", "Efetuar a troca de uma calça jeans conforme solicitação do cliente, seguindo o procedimento da loja.", 1, "Realizar troca de mercadoria", null },
                    { 6, null, new DateTime(2025, 6, 20, 22, 32, 35, 0, DateTimeKind.Utc), "944cc574-7936-4e3f-8b59-56c4af209d6f", "Organizar e higienizar os provadores ao final do expediente.", 1, "Limpar provadores", null },
                    { 7, null, new DateTime(2025, 6, 20, 22, 32, 35, 0, DateTimeKind.Utc), "944cc574-7936-4e3f-8b59-56c4af209d6f", "Gerar e enviar o relatório diário de vendas para o gerente até as 18h.", 1, "Enviar relatório de vendas", null },
                    { 8, null, new DateTime(2025, 6, 20, 22, 32, 35, 0, DateTimeKind.Utc), "944cc574-7936-4e3f-8b59-56c4af209d6f", "Verificar a arara de camisetas básicas e repor os tamanhos que estiverem em falta.", 1, "Repor camisetas na arara", null },
                    { 9, null, new DateTime(2025, 6, 20, 22, 32, 35, 0, DateTimeKind.Utc), "944cc574-7936-4e3f-8b59-56c4af209d6f", "Receber e conferir a entrega de sapatos do fornecedor, checando quantidades e modelos.", 1, "Acompanhar entrega de fornecedor", null },
                    { 10, null, new DateTime(2025, 6, 20, 22, 32, 35, 0, DateTimeKind.Utc), "944cc574-7936-4e3f-8b59-56c4af209d6f", "Preparar um kit presente com camiseta, cinto e carteira para cliente que solicitou embalagem especial.", 1, "Montar kit presente", null },
                    { 11, null, new DateTime(2025, 6, 20, 22, 32, 35, 0, DateTimeKind.Utc), "944cc574-7936-4e3f-8b59-56c4af209d6f", "Alterar os preços das peças em liquidação no sistema e nas etiquetas da loja.", 1, "Atualizar preços de liquidação", null },
                    { 12, null, new DateTime(2025, 6, 20, 22, 32, 35, 0, DateTimeKind.Utc), "944cc574-7936-4e3f-8b59-56c4af209d6f", "Finalizar a compra de um cliente, oferecendo opções de parcelamento e embalagem para presente.", 1, "Atender cliente no caixa", null },
                    { 13, null, new DateTime(2025, 6, 20, 22, 32, 35, 0, DateTimeKind.Utc), "944cc574-7936-4e3f-8b59-56c4af209d6f", "Selecionar e embalar os produtos vendidos pelo site para envio via transportadora.", 1, "Separar pedidos do e-commerce", null },
                    { 14, null, new DateTime(2025, 6, 20, 22, 32, 35, 0, DateTimeKind.Utc), "944cc574-7936-4e3f-8b59-56c4af209d6f", "Apresentar a loja, explicar procedimentos de atendimento e demonstrar o sistema de vendas ao novo funcionário.", 1, "Treinar novo colaborador", null },
                    { 15, null, new DateTime(2025, 6, 20, 22, 32, 35, 0, DateTimeKind.Utc), "944cc574-7936-4e3f-8b59-56c4af209d6f", "Realizar contagem dos produtos em estoque e registrar eventuais diferenças para conferência.", 1, "Fazer inventário semanal", null }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistory_TaskId",
                table: "TaskHistory",
                column: "TaskId");

            migrationBuilder.CreateIndex(
                name: "IX_TaskHistory_UpdatedById",
                table: "TaskHistory",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CreatedById",
                table: "Tasks",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "Users",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "Users",
                column: "NormalizedUserName",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "TaskHistory");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
