using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BarterEvolution.Infrastructure;

namespace BarterEvolution.Infrastructure.Data
{
    public partial class BarterEvolutionDbContext : DbContext
    {
        public BarterEvolutionDbContext()
        {
        }

        public BarterEvolutionDbContext(DbContextOptions<BarterEvolutionDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Articulos> Articulos { get; set; }
        public virtual DbSet<Categorias> Categorias { get; set; }
        public virtual DbSet<Clientes> Clientes { get; set; }
        public virtual DbSet<CondicionArticulos> CondicionArticulos { get; set; }
        public virtual DbSet<CondicionContratos> CondicionContratos { get; set; }
        public virtual DbSet<CondicionLegalidad> CondicionLegalidad { get; set; }
        public virtual DbSet<Contratos> Contratos { get; set; }
        public virtual DbSet<EstadoArticulos> EstadoArticulos { get; set; }
        public virtual DbSet<Genero> Genero { get; set; }
        public virtual DbSet<Inventario> Inventario { get; set; }
        public virtual DbSet<LegalidadArticulos> LegalidadArticulos { get; set; }
        public virtual DbSet<Localidad> Localidad { get; set; }
        public virtual DbSet<Prorrogas> Prorrogas { get; set; }
        public virtual DbSet<TipoCliente> TipoCliente { get; set; }
        public virtual DbSet<TipoDocumento> TipoDocumento { get; set; }
        public virtual DbSet<TipoUsuario> TipoUsuario { get; set; }
        public virtual DbSet<UsuariosSistema> UsuariosSistema { get; set; }
        public virtual DbSet<Ventas> Ventas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseMySQL("database=barterevolution;server=localhost;port=3306;user id=root;password=98072155122");
            }*/
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Articulos>(entity =>
            {
                entity.HasKey(e => e.IdArticulo)
                    .HasName("PRIMARY");

                entity.ToTable("articulos");

                entity.HasIndex(e => e.Genero)
                    .HasName("fk_genero_idx");

                entity.HasIndex(e => e.IdCategoria)
                    .HasName("fk_categoria_idx");

                entity.HasIndex(e => e.IdEstadoArticulo)
                    .HasName("fk_estadoarti_idx");

                entity.Property(e => e.IdArticulo).HasMaxLength(10);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.Genero)
                    .HasMaxLength(2)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.IdCategoria)
                    .IsRequired()
                    .HasColumnName("Id_categoria")
                    .HasMaxLength(4);

                entity.Property(e => e.IdEstadoArticulo)
                    .IsRequired()
                    .HasColumnName("Id_estado_articulo")
                    .HasMaxLength(2);

                entity.Property(e => e.Marca)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Modelo)
                    .HasMaxLength(15)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.NombreArticulo)
                    .IsRequired()
                    .HasColumnName("Nombre_articulo")
                    .HasMaxLength(25);

                entity.Property(e => e.PrecioUnitario)
                    .HasColumnName("Precio_unitario")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Serie)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.GeneroNavigation)
                    .WithMany(p => p.Articulos)
                    .HasForeignKey(d => d.Genero)
                    .HasConstraintName("fk_artgene");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Articulos)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_categoria");

                entity.HasOne(d => d.IdEstadoArticuloNavigation)
                    .WithMany(p => p.Articulos)
                    .HasForeignKey(d => d.IdEstadoArticulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_estadoarti");
            });

            modelBuilder.Entity<Categorias>(entity =>
            {
                entity.HasKey(e => e.IdCategoria)
                    .HasName("PRIMARY");

                entity.ToTable("categorias");

                entity.Property(e => e.IdCategoria)
                    .HasColumnName("Id_categoria")
                    .HasMaxLength(4);

                entity.Property(e => e.NombreCategoria)
                    .IsRequired()
                    .HasColumnName("Nombre_categoria")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Clientes>(entity =>
            {
                entity.HasKey(e => e.CedulaCliente)
                    .HasName("PRIMARY");

                entity.ToTable("clientes");

                entity.HasIndex(e => e.Genero)
                    .HasName("fk_genero_idx");

                entity.HasIndex(e => e.IdDocumento)
                    .HasName("fk_tdocumento_idx");

                entity.HasIndex(e => e.IdLocalidad)
                    .HasName("fk_localidad_idx");

                entity.HasIndex(e => e.IdTipocliente)
                    .HasName("fk_tcliente_idx");

                entity.Property(e => e.CedulaCliente)
                    .HasColumnName("Cedula_cliente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Apellidocliente1)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Apellidocliente2)
                    .HasMaxLength(15)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Ciudad)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.DireccionResidencia)
                    .IsRequired()
                    .HasColumnName("Direccion_residencia")
                    .HasMaxLength(25);

                entity.Property(e => e.Email)
                    .HasMaxLength(35)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Genero)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.IdDocumento)
                    .IsRequired()
                    .HasColumnName("Id_documento")
                    .HasMaxLength(2);

                entity.Property(e => e.IdLocalidad)
                    .HasColumnName("Id_localidad")
                    .HasColumnType("int(2)");

                entity.Property(e => e.IdTipocliente)
                    .IsRequired()
                    .HasColumnName("Id_tipocliente")
                    .HasMaxLength(2);

                entity.Property(e => e.Nombrecliente1)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Nombrecliente2)
                    .HasMaxLength(15)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.TelefonoMovil)
                    .HasColumnName("Telefono_movil")
                    .HasColumnType("bigint(10)")
                    .HasDefaultValueSql("'NULL'");

                entity.HasOne(d => d.GeneroNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.Genero)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_genero");

                entity.HasOne(d => d.IdDocumentoNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.IdDocumento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_documentoti");

                entity.HasOne(d => d.IdLocalidadNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.IdLocalidad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_localidad");

                entity.HasOne(d => d.IdTipoclienteNavigation)
                    .WithMany(p => p.Clientes)
                    .HasForeignKey(d => d.IdTipocliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tcliente");
            });

            modelBuilder.Entity<CondicionArticulos>(entity =>
            {
                entity.HasKey(e => e.IdCondicionArticulo)
                    .HasName("PRIMARY");

                entity.ToTable("condicion_articulos");

                entity.Property(e => e.IdCondicionArticulo)
                    .HasColumnName("id_condicion_articulo")
                    .HasMaxLength(2);

                entity.Property(e => e.NombreCondicionart)
                    .IsRequired()
                    .HasColumnName("nombre_condicionart")
                    .HasMaxLength(11);
            });

            modelBuilder.Entity<CondicionContratos>(entity =>
            {
                entity.HasKey(e => e.IdCondicionContrato)
                    .HasName("PRIMARY");

                entity.ToTable("condicion_contratos");

                entity.Property(e => e.IdCondicionContrato)
                    .HasColumnName("Id_condicion_contrato")
                    .HasMaxLength(3);

                entity.Property(e => e.NombreCondicioncon)
                    .IsRequired()
                    .HasColumnName("Nombre_condicioncon")
                    .HasMaxLength(17);
            });

            modelBuilder.Entity<CondicionLegalidad>(entity =>
            {
                entity.HasKey(e => e.CondicionLegalidad1)
                    .HasName("PRIMARY");

                entity.ToTable("condicion_legalidad");

                entity.Property(e => e.CondicionLegalidad1)
                    .HasColumnName("CondicionLegalidad")
                    .HasMaxLength(2);

                entity.Property(e => e.NombreCondicion)
                    .IsRequired()
                    .HasColumnName("Nombre_Condicion")
                    .HasMaxLength(12);
            });

            modelBuilder.Entity<Contratos>(entity =>
            {
                entity.HasKey(e => e.NoContrato)
                    .HasName("PRIMARY");

                entity.ToTable("contratos");

                entity.HasIndex(e => e.CedulaCliente)
                    .HasName("fk_ccliente_idx");

                entity.HasIndex(e => e.CedulaUsuario)
                    .HasName("fk_cusuario_idx");

                entity.HasIndex(e => e.IdArticulo)
                    .HasName("fk_narticulo_idx");

                entity.HasIndex(e => e.IdCondicionContrato)
                    .HasName("fk_ccontrato_idx");

                entity.HasIndex(e => e.NoProrroga)
                    .HasName("fk_nprorrogas_idx");

                entity.Property(e => e.NoContrato)
                    .HasColumnName("No_contrato")
                    .HasMaxLength(10);

                entity.Property(e => e.CedulaCliente)
                    .HasColumnName("Cedula_cliente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CedulaUsuario)
                    .HasColumnName("Cedula_usuario")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FechaInicio)
                    .HasColumnName("Fecha_inicio")
                    .HasColumnType("date");

                entity.Property(e => e.FechaPago)
                    .HasColumnName("Fecha_pago")
                    .HasColumnType("date")
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.FechaVencimiento)
                    .HasColumnName("Fecha_vencimiento")
                    .HasColumnType("date");

                entity.Property(e => e.IdArticulo)
                    .IsRequired()
                    .HasColumnName("Id_articulo")
                    .HasMaxLength(10);

                entity.Property(e => e.IdCondicionContrato)
                    .IsRequired()
                    .HasColumnName("Id_condicion_contrato")
                    .HasMaxLength(3);

                entity.Property(e => e.NoProrroga)
                    .IsRequired()
                    .HasColumnName("No_prorroga")
                    .HasMaxLength(10);

                entity.Property(e => e.PlazoEstipulado)
                    .HasColumnName("Plazo_estipulado")
                    .HasColumnType("int(2)");

                entity.Property(e => e.ValorContrato)
                    .HasColumnName("Valor_contrato")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.CedulaClienteNavigation)
                    .WithMany(p => p.Contratos)
                    .HasForeignKey(d => d.CedulaCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ccliente");

                entity.HasOne(d => d.CedulaUsuarioNavigation)
                    .WithMany(p => p.Contratos)
                    .HasForeignKey(d => d.CedulaUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cusuario");

                entity.HasOne(d => d.IdArticuloNavigation)
                    .WithMany(p => p.Contratos)
                    .HasForeignKey(d => d.IdArticulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_narticulo");

                entity.HasOne(d => d.IdCondicionContratoNavigation)
                    .WithMany(p => p.Contratos)
                    .HasForeignKey(d => d.IdCondicionContrato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ccontrato");

                entity.HasOne(d => d.NoProrrogaNavigation)
                    .WithMany(p => p.Contratos)
                    .HasForeignKey(d => d.NoProrroga)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_nprorroga");
            });

            modelBuilder.Entity<EstadoArticulos>(entity =>
            {
                entity.HasKey(e => e.IdEstadoArticulo)
                    .HasName("PRIMARY");

                entity.ToTable("estado_articulos");

                entity.Property(e => e.IdEstadoArticulo)
                    .HasColumnName("Id_estado_articulo")
                    .HasMaxLength(2);

                entity.Property(e => e.NombreEstadoart)
                    .IsRequired()
                    .HasColumnName("Nombre_estadoart")
                    .HasMaxLength(7);
            });

            modelBuilder.Entity<Genero>(entity =>
            {
                entity.HasKey(e => e.IdGenero)
                    .HasName("PRIMARY");

                entity.ToTable("genero");

                entity.Property(e => e.IdGenero)
                    .HasColumnName("idGenero")
                    .HasMaxLength(2);

                entity.Property(e => e.NombreGenero)
                    .IsRequired()
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<Inventario>(entity =>
            {
                entity.HasKey(e => e.IdInventario)
                    .HasName("PRIMARY");

                entity.ToTable("inventario");

                entity.HasIndex(e => e.IdArticulo)
                    .HasName("fk_idarticulo_idx");

                entity.HasIndex(e => e.IdCategoria)
                    .HasName("fk_cate_idx");

                entity.HasIndex(e => e.IdCondicionArticulo)
                    .HasName("fk_conarti_idx");

                entity.HasIndex(e => e.IdEstadoArticulo)
                    .HasName("fk_esart_idx");

                entity.Property(e => e.IdInventario)
                    .HasColumnName("Id_inventario")
                    .HasMaxLength(10);

                entity.Property(e => e.Cantidad).HasColumnType("int(3)");

                entity.Property(e => e.IdArticulo)
                    .IsRequired()
                    .HasColumnName("Id_articulo")
                    .HasMaxLength(10);

                entity.Property(e => e.IdCategoria)
                    .IsRequired()
                    .HasColumnName("Id_categoria")
                    .HasMaxLength(4);

                entity.Property(e => e.IdCondicionArticulo)
                    .IsRequired()
                    .HasColumnName("Id_condicion_articulo")
                    .HasMaxLength(2);

                entity.Property(e => e.IdEstadoArticulo)
                    .IsRequired()
                    .HasColumnName("Id_estado_articulo")
                    .HasMaxLength(2);

                entity.HasOne(d => d.IdArticuloNavigation)
                    .WithMany(p => p.Inventario)
                    .HasForeignKey(d => d.IdArticulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_idarticulo");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Inventario)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cate");

                entity.HasOne(d => d.IdCondicionArticuloNavigation)
                    .WithMany(p => p.Inventario)
                    .HasForeignKey(d => d.IdCondicionArticulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_conarti");

                entity.HasOne(d => d.IdEstadoArticuloNavigation)
                    .WithMany(p => p.Inventario)
                    .HasForeignKey(d => d.IdEstadoArticulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_esart");
            });

            modelBuilder.Entity<LegalidadArticulos>(entity =>
            {
                entity.HasKey(e => e.IdLegalidad)
                    .HasName("PRIMARY");

                entity.ToTable("legalidad_articulos");

                entity.HasIndex(e => e.CedulaCliente)
                    .HasName("fk_idcliente_idx");

                entity.HasIndex(e => e.CondLegalidad)
                    .HasName("fk_conlega_idx");

                entity.HasIndex(e => e.IdArticulo)
                    .HasName("fk_numarti_idx");

                entity.Property(e => e.IdLegalidad)
                    .HasColumnName("Id_legalidad")
                    .HasColumnType("int(3)");

                entity.Property(e => e.CedulaCliente)
                    .HasColumnName("Cedula_cliente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CondLegalidad)
                    .IsRequired()
                    .HasMaxLength(2);

                entity.Property(e => e.DescripcionLegalidad)
                    .IsRequired()
                    .HasColumnName("Descripcion_legalidad")
                    .HasMaxLength(45);

                entity.Property(e => e.FechaLegalidad)
                    .HasColumnName("Fecha_legalidad")
                    .HasColumnType("date");

                entity.Property(e => e.IdArticulo)
                    .IsRequired()
                    .HasColumnName("Id_articulo")
                    .HasMaxLength(10);

                entity.HasOne(d => d.CedulaClienteNavigation)
                    .WithMany(p => p.LegalidadArticulos)
                    .HasForeignKey(d => d.CedulaCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_idcliente");

                entity.HasOne(d => d.CondLegalidadNavigation)
                    .WithMany(p => p.LegalidadArticulos)
                    .HasForeignKey(d => d.CondLegalidad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_conlega");

                entity.HasOne(d => d.IdArticuloNavigation)
                    .WithMany(p => p.LegalidadArticulos)
                    .HasForeignKey(d => d.IdArticulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_numarti");
            });

            modelBuilder.Entity<Localidad>(entity =>
            {
                entity.HasKey(e => e.IdLocalidad)
                    .HasName("PRIMARY");

                entity.ToTable("localidad");

                entity.Property(e => e.IdLocalidad)
                    .HasColumnName("Id_localidad")
                    .HasColumnType("int(11)");

                entity.Property(e => e.NombreLocalidad)
                    .IsRequired()
                    .HasColumnName("Nombre_localidad")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Prorrogas>(entity =>
            {
                entity.HasKey(e => e.NoProrroga)
                    .HasName("PRIMARY");

                entity.ToTable("prorrogas");

                entity.HasIndex(e => e.NoContrato)
                    .HasName("fk_ncontrato_idx");

                entity.Property(e => e.NoProrroga)
                    .HasColumnName("No_prorroga")
                    .HasMaxLength(10);

                entity.Property(e => e.DiasVencidos)
                    .HasColumnName("Dias_vencidos")
                    .HasColumnType("int(4)");

                entity.Property(e => e.FechaInicioProrroga)
                    .HasColumnName("Fecha_inicio_prorroga")
                    .HasColumnType("date");

                entity.Property(e => e.FechaVencimientoProrroga)
                    .HasColumnName("Fecha_vencimiento_prorroga")
                    .HasColumnType("date");

                entity.Property(e => e.MesesAPagar)
                    .HasColumnName("Meses_a_pagar")
                    .HasColumnType("int(2)");

                entity.Property(e => e.NoContrato)
                    .IsRequired()
                    .HasColumnName("No_contrato")
                    .HasMaxLength(10);

                entity.Property(e => e.ValorMes)
                    .HasColumnName("Valor_mes")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.NoContratoNavigation)
                    .WithMany(p => p.Prorrogas)
                    .HasForeignKey(d => d.NoContrato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_ncontrato");
            });

            modelBuilder.Entity<TipoCliente>(entity =>
            {
                entity.HasKey(e => e.IdTipocliente)
                    .HasName("PRIMARY");

                entity.ToTable("tipo_cliente");

                entity.Property(e => e.IdTipocliente)
                    .HasColumnName("Id_tipocliente")
                    .HasMaxLength(2);

                entity.Property(e => e.NombreTipocliente)
                    .IsRequired()
                    .HasColumnName("Nombre_tipocliente")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<TipoDocumento>(entity =>
            {
                entity.HasKey(e => e.IdTipoDocumento)
                    .HasName("PRIMARY");

                entity.ToTable("tipo_documento");

                entity.Property(e => e.IdTipoDocumento)
                    .HasColumnName("Id_tipo_documento")
                    .HasMaxLength(2);

                entity.Property(e => e.TipoDocumento1)
                    .IsRequired()
                    .HasColumnName("Tipo_documento")
                    .HasMaxLength(22);
            });

            modelBuilder.Entity<TipoUsuario>(entity =>
            {
                entity.HasKey(e => e.IdTipoUsuario)
                    .HasName("PRIMARY");

                entity.ToTable("tipo_usuario");

                entity.Property(e => e.IdTipoUsuario)
                    .HasColumnName("Id_tipo_usuario")
                    .HasMaxLength(2);

                entity.Property(e => e.NombreTipous)
                    .IsRequired()
                    .HasColumnName("Nombre_tipous")
                    .HasMaxLength(15);
            });

            modelBuilder.Entity<UsuariosSistema>(entity =>
            {
                entity.HasKey(e => e.CedulaUsuario)
                    .HasName("PRIMARY");

                entity.ToTable("usuarios_sistema");

                entity.HasIndex(e => e.IdDocumento)
                    .HasName("fk_tipodoc_idx");

                entity.HasIndex(e => e.IdUsuario)
                    .HasName("fk_usuariotipo_idx");

                entity.Property(e => e.CedulaUsuario)
                    .HasColumnName("Cedula_usuario")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Apellidousuario1)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Apellidousuario2)
                    .HasMaxLength(15)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Clave)
                    .IsRequired()
                    .HasColumnName("clave")
                    .HasMaxLength(10);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(35);

                entity.Property(e => e.IdDocumento)
                    .IsRequired()
                    .HasColumnName("Id_documento")
                    .HasMaxLength(2);

                entity.Property(e => e.IdUsuario)
                    .IsRequired()
                    .HasColumnName("Id_usuario")
                    .HasMaxLength(2);

                entity.Property(e => e.Nombreusuario1)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.Property(e => e.Nombreusuario2)
                    .HasMaxLength(15)
                    .HasDefaultValueSql("'NULL'");

                entity.Property(e => e.Usuario)
                    .IsRequired()
                    .HasMaxLength(15);

                entity.HasOne(d => d.IdDocumentoNavigation)
                    .WithMany(p => p.UsuariosSistema)
                    .HasForeignKey(d => d.IdDocumento)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tipodoc");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.UsuariosSistema)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_tipous");
            });

            modelBuilder.Entity<Ventas>(entity =>
            {
                entity.HasKey(e => e.NoFactura)
                    .HasName("PRIMARY");

                entity.ToTable("ventas");

                entity.HasIndex(e => e.CedulaCliente)
                    .HasName("fk_cedulacl_idx");

                entity.HasIndex(e => e.CedulaUsuario)
                    .HasName("fk_cedulaus_idx");

                entity.HasIndex(e => e.IdArticulo)
                    .HasName("fk_artinum_idx");

                entity.HasIndex(e => e.IdEstadoArticulo)
                    .HasName("fk_estadoa_idx");

                entity.Property(e => e.NoFactura)
                    .HasColumnName("No_factura")
                    .HasMaxLength(10);

                entity.Property(e => e.Cantidad).HasColumnType("int(3)");

                entity.Property(e => e.CedulaCliente)
                    .HasColumnName("Cedula_cliente")
                    .HasColumnType("int(11)");

                entity.Property(e => e.CedulaUsuario)
                    .HasColumnName("Cedula_usuario")
                    .HasColumnType("int(11)");

                entity.Property(e => e.FechaFactura)
                    .HasColumnName("Fecha_factura")
                    .HasColumnType("date");

                entity.Property(e => e.IdArticulo)
                    .IsRequired()
                    .HasColumnName("Id_articulo")
                    .HasMaxLength(10);

                entity.Property(e => e.IdEstadoArticulo)
                    .IsRequired()
                    .HasColumnName("Id_estado_articulo")
                    .HasMaxLength(2);

                entity.Property(e => e.Iva).HasColumnName("IVA");

                entity.Property(e => e.PrecioUnitario)
                    .HasColumnName("Precio_unitario")
                    .HasColumnType("int(11)");

                entity.Property(e => e.SubTotal)
                    .HasColumnName("SUB_TOTAL")
                    .HasColumnType("int(11)");

                entity.Property(e => e.ValorTotal)
                    .HasColumnName("VALOR_TOTAL")
                    .HasColumnType("int(11)");

                entity.HasOne(d => d.CedulaClienteNavigation)
                    .WithMany(p => p.Ventas)
                    .HasForeignKey(d => d.CedulaCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cedulacl");

                entity.HasOne(d => d.CedulaUsuarioNavigation)
                    .WithMany(p => p.Ventas)
                    .HasForeignKey(d => d.CedulaUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_cedulaus");

                entity.HasOne(d => d.IdArticuloNavigation)
                    .WithMany(p => p.Ventas)
                    .HasForeignKey(d => d.IdArticulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_artinum");

                entity.HasOne(d => d.IdEstadoArticuloNavigation)
                    .WithMany(p => p.Ventas)
                    .HasForeignKey(d => d.IdEstadoArticulo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_estadoa");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
