using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestionEmploiTemps.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Creneaux",
                columns: table => new
                {
                    IdCreneau = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Jour = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HeureDebut = table.Column<TimeSpan>(type: "time", nullable: false),
                    HeureFin = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Creneaux", x => x.IdCreneau);
                });

            migrationBuilder.CreateTable(
                name: "Enseignants",
                columns: table => new
                {
                    IdEns = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enseignants", x => x.IdEns);
                });

            migrationBuilder.CreateTable(
                name: "Matieres",
                columns: table => new
                {
                    IdMatiere = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matieres", x => x.IdMatiere);
                });

            migrationBuilder.CreateTable(
                name: "Niveaux",
                columns: table => new
                {
                    IdNiveau = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Niveaux", x => x.IdNiveau);
                });

            migrationBuilder.CreateTable(
                name: "Salles",
                columns: table => new
                {
                    IdSalle = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacite = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salles", x => x.IdSalle);
                });

            migrationBuilder.CreateTable(
                name: "Enseigners",
                columns: table => new
                {
                    IdEns = table.Column<int>(type: "int", nullable: false),
                    IdMatiere = table.Column<int>(type: "int", nullable: false),
                    EnseignantIdEns = table.Column<int>(type: "int", nullable: false),
                    MatiereIdMatiere = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enseigners", x => new { x.IdEns, x.IdMatiere });
                    table.ForeignKey(
                        name: "FK_Enseigners_Enseignants_EnseignantIdEns",
                        column: x => x.EnseignantIdEns,
                        principalTable: "Enseignants",
                        principalColumn: "IdEns",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enseigners_Matieres_MatiereIdMatiere",
                        column: x => x.MatiereIdMatiere,
                        principalTable: "Matieres",
                        principalColumn: "IdMatiere",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parcours",
                columns: table => new
                {
                    IdParcours = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nom = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdNiveau = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parcours", x => x.IdParcours);
                    table.ForeignKey(
                        name: "FK_Parcours_Niveaux_IdNiveau",
                        column: x => x.IdNiveau,
                        principalTable: "Niveaux",
                        principalColumn: "IdNiveau",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Seances",
                columns: table => new
                {
                    IdSeance = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdParcours = table.Column<int>(type: "int", nullable: false),
                    IdMatiere = table.Column<int>(type: "int", nullable: false),
                    IdEns = table.Column<int>(type: "int", nullable: false),
                    IdSalle = table.Column<int>(type: "int", nullable: false),
                    IdCreneau = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seances", x => x.IdSeance);
                    table.ForeignKey(
                        name: "FK_Seances_Creneaux_IdCreneau",
                        column: x => x.IdCreneau,
                        principalTable: "Creneaux",
                        principalColumn: "IdCreneau",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Seances_Enseignants_IdEns",
                        column: x => x.IdEns,
                        principalTable: "Enseignants",
                        principalColumn: "IdEns",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Seances_Matieres_IdMatiere",
                        column: x => x.IdMatiere,
                        principalTable: "Matieres",
                        principalColumn: "IdMatiere",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Seances_Parcours_IdParcours",
                        column: x => x.IdParcours,
                        principalTable: "Parcours",
                        principalColumn: "IdParcours",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Seances_Salles_IdSalle",
                        column: x => x.IdSalle,
                        principalTable: "Salles",
                        principalColumn: "IdSalle",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Enseigners_EnseignantIdEns",
                table: "Enseigners",
                column: "EnseignantIdEns");

            migrationBuilder.CreateIndex(
                name: "IX_Enseigners_MatiereIdMatiere",
                table: "Enseigners",
                column: "MatiereIdMatiere");

            migrationBuilder.CreateIndex(
                name: "IX_Parcours_IdNiveau",
                table: "Parcours",
                column: "IdNiveau");

            migrationBuilder.CreateIndex(
                name: "IX_Seances_IdCreneau",
                table: "Seances",
                column: "IdCreneau");

            migrationBuilder.CreateIndex(
                name: "IX_Seances_IdEns",
                table: "Seances",
                column: "IdEns");

            migrationBuilder.CreateIndex(
                name: "IX_Seances_IdMatiere",
                table: "Seances",
                column: "IdMatiere");

            migrationBuilder.CreateIndex(
                name: "IX_Seances_IdParcours",
                table: "Seances",
                column: "IdParcours");

            migrationBuilder.CreateIndex(
                name: "IX_Seances_IdSalle",
                table: "Seances",
                column: "IdSalle");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enseigners");

            migrationBuilder.DropTable(
                name: "Seances");

            migrationBuilder.DropTable(
                name: "Creneaux");

            migrationBuilder.DropTable(
                name: "Enseignants");

            migrationBuilder.DropTable(
                name: "Matieres");

            migrationBuilder.DropTable(
                name: "Parcours");

            migrationBuilder.DropTable(
                name: "Salles");

            migrationBuilder.DropTable(
                name: "Niveaux");
        }
    }
}
