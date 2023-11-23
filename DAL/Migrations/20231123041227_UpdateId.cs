using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "Usuarios",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "IdServicio",
                table: "Servicios",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "IdDetalleServicio",
                table: "DetalleServicios",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "IdDetalleServicio",
                table: "ComentarioServicio",
                newName: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Usuarios",
                newName: "IdUsuario");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Servicios",
                newName: "IdServicio");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "DetalleServicios",
                newName: "IdDetalleServicio");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ComentarioServicio",
                newName: "IdDetalleServicio");
        }
    }
}
