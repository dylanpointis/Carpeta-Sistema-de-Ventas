﻿namespace Carpeta_Sistema_de_Ventas
{
    partial class frmMenu
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMenu));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.btnInicio = new System.Windows.Forms.ToolStripMenuItem();
            this.Admin = new System.Windows.Forms.ToolStripMenuItem();
            this.GestionUsuarios = new System.Windows.Forms.ToolStripMenuItem();
            this.GestionPerfiles = new System.Windows.Forms.ToolStripMenuItem();
            this.Eventos = new System.Windows.Forms.ToolStripMenuItem();
            this.Respaldos = new System.Windows.Forms.ToolStripMenuItem();
            this.Maestros = new System.Windows.Forms.ToolStripMenuItem();
            this.Clientes = new System.Windows.Forms.ToolStripMenuItem();
            this.Proveedores = new System.Windows.Forms.ToolStripMenuItem();
            this.Productos = new System.Windows.Forms.ToolStripMenuItem();
            this.ProductosC = new System.Windows.Forms.ToolStripMenuItem();
            this.Usuarios = new System.Windows.Forms.ToolStripMenuItem();
            this.CambiarClave = new System.Windows.Forms.ToolStripMenuItem();
            this.CambiarIdioma = new System.Windows.Forms.ToolStripMenuItem();
            this.Ventas = new System.Windows.Forms.ToolStripMenuItem();
            this.GenerarFactura = new System.Windows.Forms.ToolStripMenuItem();
            this.Compras = new System.Windows.Forms.ToolStripMenuItem();
            this.GenerarSolicitudCotizacion = new System.Windows.Forms.ToolStripMenuItem();
            this.GenerarOrdenCompra = new System.Windows.Forms.ToolStripMenuItem();
            this.CorroborarRecepcion = new System.Windows.Forms.ToolStripMenuItem();
            this.Reportes = new System.Windows.Forms.ToolStripMenuItem();
            this.ReporteVentas = new System.Windows.Forms.ToolStripMenuItem();
            this.ReporteCompras = new System.Windows.Forms.ToolStripMenuItem();
            this.ReporteInteligente = new System.Windows.Forms.ToolStripMenuItem();
            this.Ayuda = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.btnSesion = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarSesionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iniciarSesiónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnInicio,
            this.Admin,
            this.Maestros,
            this.Usuarios,
            this.Ventas,
            this.Compras,
            this.Reportes,
            this.Ayuda});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1025, 44);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // btnInicio
            // 
            this.btnInicio.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnInicio.ForeColor = System.Drawing.Color.White;
            this.btnInicio.Name = "btnInicio";
            this.btnInicio.Size = new System.Drawing.Size(65, 40);
            this.btnInicio.Text = "Inicio";
            this.btnInicio.Click += new System.EventHandler(this.btnInicio_Click);
            // 
            // Admin
            // 
            this.Admin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GestionUsuarios,
            this.GestionPerfiles,
            this.Eventos,
            this.Respaldos});
            this.Admin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.Admin.ForeColor = System.Drawing.Color.White;
            this.Admin.Name = "Admin";
            this.Admin.Size = new System.Drawing.Size(73, 40);
            this.Admin.Text = "Admin";
            // 
            // GestionUsuarios
            // 
            this.GestionUsuarios.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.GestionUsuarios.Name = "GestionUsuarios";
            this.GestionUsuarios.Size = new System.Drawing.Size(219, 26);
            this.GestionUsuarios.Text = "Gestión de Usuarios";
            this.GestionUsuarios.Click += new System.EventHandler(this.gestiónDeUsuariosToolStripMenuItem_Click);
            // 
            // GestionPerfiles
            // 
            this.GestionPerfiles.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GestionPerfiles.Name = "GestionPerfiles";
            this.GestionPerfiles.Size = new System.Drawing.Size(219, 26);
            this.GestionPerfiles.Text = "Gestión de Perfiles";
            this.GestionPerfiles.Click += new System.EventHandler(this.gestiónDePerfilesToolStripMenuItem_Click);
            // 
            // Eventos
            // 
            this.Eventos.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Eventos.Name = "Eventos";
            this.Eventos.Size = new System.Drawing.Size(219, 26);
            this.Eventos.Text = "Eventos";
            this.Eventos.Click += new System.EventHandler(this.eventosToolStripMenuItem_Click);
            // 
            // Respaldos
            // 
            this.Respaldos.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Respaldos.Name = "Respaldos";
            this.Respaldos.Size = new System.Drawing.Size(219, 26);
            this.Respaldos.Text = "Respaldos";
            this.Respaldos.Click += new System.EventHandler(this.respaldosToolStripMenuItem_Click);
            // 
            // Maestros
            // 
            this.Maestros.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.Clientes,
            this.Proveedores,
            this.Productos,
            this.ProductosC});
            this.Maestros.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.Maestros.ForeColor = System.Drawing.Color.White;
            this.Maestros.Name = "Maestros";
            this.Maestros.Size = new System.Drawing.Size(91, 40);
            this.Maestros.Text = "Maestros";
            // 
            // Clientes
            // 
            this.Clientes.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Clientes.Name = "Clientes";
            this.Clientes.Size = new System.Drawing.Size(180, 26);
            this.Clientes.Text = "Clientes";
            this.Clientes.Click += new System.EventHandler(this.clientesToolStripMenuItem_Click);
            // 
            // Proveedores
            // 
            this.Proveedores.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Proveedores.Name = "Proveedores";
            this.Proveedores.Size = new System.Drawing.Size(180, 26);
            this.Proveedores.Text = "Proveedores";
            this.Proveedores.Click += new System.EventHandler(this.proveedoresToolStripMenuItem_Click);
            // 
            // Productos
            // 
            this.Productos.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Productos.Name = "Productos";
            this.Productos.Size = new System.Drawing.Size(180, 26);
            this.Productos.Text = "Productos";
            this.Productos.Click += new System.EventHandler(this.productosToolStripMenuItem_Click);
            // 
            // ProductosC
            // 
            this.ProductosC.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ProductosC.Name = "ProductosC";
            this.ProductosC.Size = new System.Drawing.Size(180, 26);
            this.ProductosC.Text = "Productos-C";
            this.ProductosC.Click += new System.EventHandler(this.productosCToolStripMenuItem_Click);
            // 
            // Usuarios
            // 
            this.Usuarios.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.CambiarClave,
            this.CambiarIdioma});
            this.Usuarios.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.Usuarios.ForeColor = System.Drawing.Color.White;
            this.Usuarios.Name = "Usuarios";
            this.Usuarios.Size = new System.Drawing.Size(88, 40);
            this.Usuarios.Text = "Usuarios";
            // 
            // CambiarClave
            // 
            this.CambiarClave.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CambiarClave.Name = "CambiarClave";
            this.CambiarClave.Size = new System.Drawing.Size(191, 26);
            this.CambiarClave.Text = "Cambiar clave";
            this.CambiarClave.Click += new System.EventHandler(this.cambiarClaveToolStripMenuItem_Click);
            // 
            // CambiarIdioma
            // 
            this.CambiarIdioma.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CambiarIdioma.Name = "CambiarIdioma";
            this.CambiarIdioma.Size = new System.Drawing.Size(191, 26);
            this.CambiarIdioma.Text = "Cambiar Idioma";
            this.CambiarIdioma.Click += new System.EventHandler(this.cambiarIdiomaToolStripMenuItem_Click);
            // 
            // Ventas
            // 
            this.Ventas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GenerarFactura});
            this.Ventas.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.Ventas.ForeColor = System.Drawing.Color.White;
            this.Ventas.Name = "Ventas";
            this.Ventas.Size = new System.Drawing.Size(73, 40);
            this.Ventas.Text = "Ventas";
            // 
            // GenerarFactura
            // 
            this.GenerarFactura.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenerarFactura.Name = "GenerarFactura";
            this.GenerarFactura.Size = new System.Drawing.Size(188, 26);
            this.GenerarFactura.Text = "Generar factura";
            this.GenerarFactura.Click += new System.EventHandler(this.generarFacturaToolStripMenuItem_Click);
            // 
            // Compras
            // 
            this.Compras.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GenerarSolicitudCotizacion,
            this.GenerarOrdenCompra,
            this.CorroborarRecepcion});
            this.Compras.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.Compras.ForeColor = System.Drawing.Color.White;
            this.Compras.Name = "Compras";
            this.Compras.Size = new System.Drawing.Size(89, 40);
            this.Compras.Text = "Compras";
            // 
            // GenerarSolicitudCotizacion
            // 
            this.GenerarSolicitudCotizacion.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenerarSolicitudCotizacion.Name = "GenerarSolicitudCotizacion";
            this.GenerarSolicitudCotizacion.Size = new System.Drawing.Size(292, 26);
            this.GenerarSolicitudCotizacion.Text = "Generar solicitud de cotización";
            this.GenerarSolicitudCotizacion.Click += new System.EventHandler(this.generarSolicitudDeCotizaciónToolStripMenuItem_Click);
            // 
            // GenerarOrdenCompra
            // 
            this.GenerarOrdenCompra.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GenerarOrdenCompra.Name = "GenerarOrdenCompra";
            this.GenerarOrdenCompra.Size = new System.Drawing.Size(292, 26);
            this.GenerarOrdenCompra.Text = "Generar orden de compra";
            this.GenerarOrdenCompra.Click += new System.EventHandler(this.generarOrdenDeCompraToolStripMenuItem_Click);
            // 
            // CorroborarRecepcion
            // 
            this.CorroborarRecepcion.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CorroborarRecepcion.Name = "CorroborarRecepcion";
            this.CorroborarRecepcion.Size = new System.Drawing.Size(292, 26);
            this.CorroborarRecepcion.Text = "Corroborar recepción";
            this.CorroborarRecepcion.Click += new System.EventHandler(this.corroborarRecepciónToolStripMenuItem_Click);
            // 
            // Reportes
            // 
            this.Reportes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ReporteVentas,
            this.ReporteCompras,
            this.ReporteInteligente});
            this.Reportes.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.Reportes.ForeColor = System.Drawing.Color.White;
            this.Reportes.Name = "Reportes";
            this.Reportes.Size = new System.Drawing.Size(89, 40);
            this.Reportes.Text = "Reportes";
            // 
            // ReporteVentas
            // 
            this.ReporteVentas.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReporteVentas.Name = "ReporteVentas";
            this.ReporteVentas.Size = new System.Drawing.Size(268, 26);
            this.ReporteVentas.Text = "Generar reporte ventas";
            this.ReporteVentas.Click += new System.EventHandler(this.generarReporteToolStripMenuItem_Click);
            // 
            // ReporteCompras
            // 
            this.ReporteCompras.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReporteCompras.Name = "ReporteCompras";
            this.ReporteCompras.Size = new System.Drawing.Size(268, 26);
            this.ReporteCompras.Text = "Generar reporte compras";
            this.ReporteCompras.Click += new System.EventHandler(this.generarReporteComprasToolStripMenuItem_Click);
            // 
            // ReporteInteligente
            // 
            this.ReporteInteligente.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ReporteInteligente.Name = "ReporteInteligente";
            this.ReporteInteligente.Size = new System.Drawing.Size(268, 26);
            this.ReporteInteligente.Text = "Generar reporte inteligente";
            this.ReporteInteligente.Click += new System.EventHandler(this.generarReporteInteligenteToolStripMenuItem_Click);
            // 
            // Ayuda
            // 
            this.Ayuda.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.Ayuda.ForeColor = System.Drawing.Color.White;
            this.Ayuda.Name = "Ayuda";
            this.Ayuda.Size = new System.Drawing.Size(71, 40);
            this.Ayuda.Text = "Ayuda";
            this.Ayuda.Click += new System.EventHandler(this.Ayuda_Click);
            // 
            // menuStrip2
            // 
            this.menuStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSesion});
            this.menuStrip2.Location = new System.Drawing.Point(0, 570);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(1025, 24);
            this.menuStrip2.TabIndex = 2;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // btnSesion
            // 
            this.btnSesion.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cerrarSesionToolStripMenuItem,
            this.iniciarSesiónToolStripMenuItem});
            this.btnSesion.Name = "btnSesion";
            this.btnSesion.Size = new System.Drawing.Size(53, 20);
            this.btnSesion.Text = "Sesión";
            // 
            // cerrarSesionToolStripMenuItem
            // 
            this.cerrarSesionToolStripMenuItem.Name = "cerrarSesionToolStripMenuItem";
            this.cerrarSesionToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.cerrarSesionToolStripMenuItem.Text = "Cerrar sesión";
            this.cerrarSesionToolStripMenuItem.Click += new System.EventHandler(this.cerrarSesionToolStripMenuItem_Click);
            // 
            // iniciarSesiónToolStripMenuItem
            // 
            this.iniciarSesiónToolStripMenuItem.Name = "iniciarSesiónToolStripMenuItem";
            this.iniciarSesiónToolStripMenuItem.Size = new System.Drawing.Size(142, 22);
            this.iniciarSesiónToolStripMenuItem.Text = "Iniciar sesión";
            this.iniciarSesiónToolStripMenuItem.Click += new System.EventHandler(this.iniciarSesiónToolStripMenuItem_Click);
            // 
            // frmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(1025, 594);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.menuStrip2);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Alta Gama";
            this.Load += new System.EventHandler(this.frmMenu_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem Admin;
        private System.Windows.Forms.ToolStripMenuItem Maestros;
        private System.Windows.Forms.ToolStripMenuItem Usuarios;
        private System.Windows.Forms.ToolStripMenuItem Ventas;
        private System.Windows.Forms.ToolStripMenuItem Compras;
        private System.Windows.Forms.ToolStripMenuItem Reportes;
        private System.Windows.Forms.ToolStripMenuItem Ayuda;
        private System.Windows.Forms.ToolStripMenuItem GestionUsuarios;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem btnSesion;
        private System.Windows.Forms.ToolStripMenuItem cerrarSesionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem CambiarClave;
        private System.Windows.Forms.ToolStripMenuItem iniciarSesiónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem GenerarFactura;
        private System.Windows.Forms.ToolStripMenuItem CambiarIdioma;
        private System.Windows.Forms.ToolStripMenuItem GestionPerfiles;
        private System.Windows.Forms.ToolStripMenuItem Productos;
        private System.Windows.Forms.ToolStripMenuItem Clientes;
        private System.Windows.Forms.ToolStripMenuItem ReporteVentas;
        private System.Windows.Forms.ToolStripMenuItem Eventos;
        private System.Windows.Forms.ToolStripMenuItem Respaldos;
        private System.Windows.Forms.ToolStripMenuItem ProductosC;
        private System.Windows.Forms.ToolStripMenuItem GenerarSolicitudCotizacion;
        private System.Windows.Forms.ToolStripMenuItem GenerarOrdenCompra;
        private System.Windows.Forms.ToolStripMenuItem CorroborarRecepcion;
        private System.Windows.Forms.ToolStripMenuItem Proveedores;
        private System.Windows.Forms.ToolStripMenuItem btnInicio;
        private System.Windows.Forms.ToolStripMenuItem ReporteCompras;
        private System.Windows.Forms.ToolStripMenuItem ReporteInteligente;
    }
}

