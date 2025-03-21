using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StageTest.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Documentacoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentacoes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Equipe",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Equipe", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Ferramentas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ferramentas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Processos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    ResponsaveisId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Processos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Processos_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Processos_Equipe_ResponsaveisId",
                        column: x => x.ResponsaveisId,
                        principalTable: "Equipe",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Responsaveis",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cargo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EquipeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Responsaveis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Responsaveis_Equipe_EquipeId",
                        column: x => x.EquipeId,
                        principalTable: "Equipe",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Subprocessos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProceId = table.Column<int>(type: "int", nullable: false),
                    SubprocessoPaiId = table.Column<int>(type: "int", nullable: true),
                    ProcessoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subprocessos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subprocessos_Processos_ProcessoId",
                        column: x => x.ProcessoId,
                        principalTable: "Processos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Subprocessos_Subprocessos_SubprocessoPaiId",
                        column: x => x.SubprocessoPaiId,
                        principalTable: "Subprocessos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProcessoDocumentacoes",
                columns: table => new
                {
                    ProcessoDocumentacaoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessoId = table.Column<int>(type: "int", nullable: true),
                    SubprocessoId = table.Column<int>(type: "int", nullable: true),
                    DocumentacaoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessoDocumentacoes", x => x.ProcessoDocumentacaoId);
                    table.ForeignKey(
                        name: "FK_ProcessoDocumentacoes_Documentacoes_DocumentacaoId",
                        column: x => x.DocumentacaoId,
                        principalTable: "Documentacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessoDocumentacoes_Processos_ProcessoId",
                        column: x => x.ProcessoId,
                        principalTable: "Processos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProcessoDocumentacoes_Subprocessos_SubprocessoId",
                        column: x => x.SubprocessoId,
                        principalTable: "Subprocessos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProcessoFerramentas",
                columns: table => new
                {
                    ProcessoFerramentaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessoId = table.Column<int>(type: "int", nullable: true),
                    SubprocessoId = table.Column<int>(type: "int", nullable: true),
                    FerramentaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessoFerramentas", x => x.ProcessoFerramentaId);
                    table.ForeignKey(
                        name: "FK_ProcessoFerramentas_Ferramentas_FerramentaId",
                        column: x => x.FerramentaId,
                        principalTable: "Ferramentas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessoFerramentas_Processos_ProcessoId",
                        column: x => x.ProcessoId,
                        principalTable: "Processos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProcessoFerramentas_Subprocessos_SubprocessoId",
                        column: x => x.SubprocessoId,
                        principalTable: "Subprocessos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProcessoResponsaveis",
                columns: table => new
                {
                    ProcessoResponsavelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProcessoId = table.Column<int>(type: "int", nullable: true),
                    SubprocessoId = table.Column<int>(type: "int", nullable: true),
                    ResponsavelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProcessoResponsaveis", x => x.ProcessoResponsavelId);
                    table.ForeignKey(
                        name: "FK_ProcessoResponsaveis_Processos_ProcessoId",
                        column: x => x.ProcessoId,
                        principalTable: "Processos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProcessoResponsaveis_Responsaveis_ResponsavelId",
                        column: x => x.ResponsavelId,
                        principalTable: "Responsaveis",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProcessoResponsaveis_Subprocessos_SubprocessoId",
                        column: x => x.SubprocessoId,
                        principalTable: "Subprocessos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProcessoDocumentacoes_DocumentacaoId",
                table: "ProcessoDocumentacoes",
                column: "DocumentacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessoDocumentacoes_ProcessoId",
                table: "ProcessoDocumentacoes",
                column: "ProcessoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessoDocumentacoes_SubprocessoId",
                table: "ProcessoDocumentacoes",
                column: "SubprocessoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessoFerramentas_FerramentaId",
                table: "ProcessoFerramentas",
                column: "FerramentaId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessoFerramentas_ProcessoId",
                table: "ProcessoFerramentas",
                column: "ProcessoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessoFerramentas_SubprocessoId",
                table: "ProcessoFerramentas",
                column: "SubprocessoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessoResponsaveis_ProcessoId",
                table: "ProcessoResponsaveis",
                column: "ProcessoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessoResponsaveis_ResponsavelId",
                table: "ProcessoResponsaveis",
                column: "ResponsavelId");

            migrationBuilder.CreateIndex(
                name: "IX_ProcessoResponsaveis_SubprocessoId",
                table: "ProcessoResponsaveis",
                column: "SubprocessoId");

            migrationBuilder.CreateIndex(
                name: "IX_Processos_DepartamentoId",
                table: "Processos",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Processos_ResponsaveisId",
                table: "Processos",
                column: "ResponsaveisId");

            migrationBuilder.CreateIndex(
                name: "IX_Responsaveis_EquipeId",
                table: "Responsaveis",
                column: "EquipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Subprocessos_ProcessoId",
                table: "Subprocessos",
                column: "ProcessoId");

            migrationBuilder.CreateIndex(
                name: "IX_Subprocessos_SubprocessoPaiId",
                table: "Subprocessos",
                column: "SubprocessoPaiId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProcessoDocumentacoes");

            migrationBuilder.DropTable(
                name: "ProcessoFerramentas");

            migrationBuilder.DropTable(
                name: "ProcessoResponsaveis");

            migrationBuilder.DropTable(
                name: "Documentacoes");

            migrationBuilder.DropTable(
                name: "Ferramentas");

            migrationBuilder.DropTable(
                name: "Responsaveis");

            migrationBuilder.DropTable(
                name: "Subprocessos");

            migrationBuilder.DropTable(
                name: "Processos");

            migrationBuilder.DropTable(
                name: "Departamentos");

            migrationBuilder.DropTable(
                name: "Equipe");
        }
    }
}
