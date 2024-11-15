namespace Carpeta_Sistema_de_Ventas
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
            this.Admin = new System.Windows.Forms.ToolStripMenuItem();
            this.gestiónDeUsuariosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gestiónDePerfilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eventosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.respaldosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Maestros = new System.Windows.Forms.ToolStripMenuItem();
            this.clientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.proveedoresToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.productosCToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Usuarios = new System.Windows.Forms.ToolStripMenuItem();
            this.cambiarClaveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cambiarIdiomaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Ventas = new System.Windows.Forms.ToolStripMenuItem();
            this.generarFacturaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Compras = new System.Windows.Forms.ToolStripMenuItem();
            this.generarSolicitudDeCotizaciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generarOrdenDeCompraToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.corroborarRecepciónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Reportes = new System.Windows.Forms.ToolStripMenuItem();
            this.generarReporteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Ayuda = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.btnSesion = new System.Windows.Forms.ToolStripMenuItem();
            this.cerrarSesionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iniciarSesiónToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnInicio = new System.Windows.Forms.ToolStripMenuItem();
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
            // Admin
            // 
            this.Admin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.gestiónDeUsuariosToolStripMenuItem,
            this.gestiónDePerfilesToolStripMenuItem,
            this.eventosToolStripMenuItem,
            this.respaldosToolStripMenuItem});
            this.Admin.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.Admin.ForeColor = System.Drawing.Color.White;
            this.Admin.Name = "Admin";
            this.Admin.Size = new System.Drawing.Size(73, 40);
            this.Admin.Text = "Admin";
            // 
            // gestiónDeUsuariosToolStripMenuItem
            // 
            this.gestiónDeUsuariosToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.gestiónDeUsuariosToolStripMenuItem.Name = "gestiónDeUsuariosToolStripMenuItem";
            this.gestiónDeUsuariosToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.gestiónDeUsuariosToolStripMenuItem.Text = "Gestión de Usuarios";
            this.gestiónDeUsuariosToolStripMenuItem.Click += new System.EventHandler(this.gestiónDeUsuariosToolStripMenuItem_Click);
            // 
            // gestiónDePerfilesToolStripMenuItem
            // 
            this.gestiónDePerfilesToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gestiónDePerfilesToolStripMenuItem.Name = "gestiónDePerfilesToolStripMenuItem";
            this.gestiónDePerfilesToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.gestiónDePerfilesToolStripMenuItem.Text = "Gestión de Perfiles";
            this.gestiónDePerfilesToolStripMenuItem.Click += new System.EventHandler(this.gestiónDePerfilesToolStripMenuItem_Click);
            // 
            // eventosToolStripMenuItem
            // 
            this.eventosToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.eventosToolStripMenuItem.Name = "eventosToolStripMenuItem";
            this.eventosToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.eventosToolStripMenuItem.Text = "Eventos";
            this.eventosToolStripMenuItem.Click += new System.EventHandler(this.eventosToolStripMenuItem_Click);
            // 
            // respaldosToolStripMenuItem
            // 
            this.respaldosToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.respaldosToolStripMenuItem.Name = "respaldosToolStripMenuItem";
            this.respaldosToolStripMenuItem.Size = new System.Drawing.Size(219, 26);
            this.respaldosToolStripMenuItem.Text = "Respaldos";
            this.respaldosToolStripMenuItem.Click += new System.EventHandler(this.respaldosToolStripMenuItem_Click);
            // 
            // Maestros
            // 
            this.Maestros.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clientesToolStripMenuItem,
            this.proveedoresToolStripMenuItem,
            this.productosToolStripMenuItem,
            this.productosCToolStripMenuItem});
            this.Maestros.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.Maestros.ForeColor = System.Drawing.Color.White;
            this.Maestros.Name = "Maestros";
            this.Maestros.Size = new System.Drawing.Size(91, 40);
            this.Maestros.Text = "Maestros";
            // 
            // clientesToolStripMenuItem
            // 
            this.clientesToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clientesToolStripMenuItem.Name = "clientesToolStripMenuItem";
            this.clientesToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            this.clientesToolStripMenuItem.Text = "Clientes";
            this.clientesToolStripMenuItem.Click += new System.EventHandler(this.clientesToolStripMenuItem_Click);
            // 
            // proveedoresToolStripMenuItem
            // 
            this.proveedoresToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.proveedoresToolStripMenuItem.Name = "proveedoresToolStripMenuItem";
            this.proveedoresToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            this.proveedoresToolStripMenuItem.Text = "Proveedores";
            this.proveedoresToolStripMenuItem.Click += new System.EventHandler(this.proveedoresToolStripMenuItem_Click);
            // 
            // productosToolStripMenuItem
            // 
            this.productosToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productosToolStripMenuItem.Name = "productosToolStripMenuItem";
            this.productosToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            this.productosToolStripMenuItem.Text = "Productos";
            this.productosToolStripMenuItem.Click += new System.EventHandler(this.productosToolStripMenuItem_Click);
            // 
            // productosCToolStripMenuItem
            // 
            this.productosCToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productosCToolStripMenuItem.Name = "productosCToolStripMenuItem";
            this.productosCToolStripMenuItem.Size = new System.Drawing.Size(167, 26);
            this.productosCToolStripMenuItem.Text = "Productos-C";
            this.productosCToolStripMenuItem.Click += new System.EventHandler(this.productosCToolStripMenuItem_Click);
            // 
            // Usuarios
            // 
            this.Usuarios.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cambiarClaveToolStripMenuItem,
            this.cambiarIdiomaToolStripMenuItem});
            this.Usuarios.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.Usuarios.ForeColor = System.Drawing.Color.White;
            this.Usuarios.Name = "Usuarios";
            this.Usuarios.Size = new System.Drawing.Size(88, 40);
            this.Usuarios.Text = "Usuarios";
            // 
            // cambiarClaveToolStripMenuItem
            // 
            this.cambiarClaveToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cambiarClaveToolStripMenuItem.Name = "cambiarClaveToolStripMenuItem";
            this.cambiarClaveToolStripMenuItem.Size = new System.Drawing.Size(191, 26);
            this.cambiarClaveToolStripMenuItem.Text = "Cambiar clave";
            this.cambiarClaveToolStripMenuItem.Click += new System.EventHandler(this.cambiarClaveToolStripMenuItem_Click);
            // 
            // cambiarIdiomaToolStripMenuItem
            // 
            this.cambiarIdiomaToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cambiarIdiomaToolStripMenuItem.Name = "cambiarIdiomaToolStripMenuItem";
            this.cambiarIdiomaToolStripMenuItem.Size = new System.Drawing.Size(191, 26);
            this.cambiarIdiomaToolStripMenuItem.Text = "Cambiar Idioma";
            this.cambiarIdiomaToolStripMenuItem.Click += new System.EventHandler(this.cambiarIdiomaToolStripMenuItem_Click);
            // 
            // Ventas
            // 
            this.Ventas.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generarFacturaToolStripMenuItem});
            this.Ventas.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.Ventas.ForeColor = System.Drawing.Color.White;
            this.Ventas.Name = "Ventas";
            this.Ventas.Size = new System.Drawing.Size(73, 40);
            this.Ventas.Text = "Ventas";
            // 
            // generarFacturaToolStripMenuItem
            // 
            this.generarFacturaToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.generarFacturaToolStripMenuItem.Name = "generarFacturaToolStripMenuItem";
            this.generarFacturaToolStripMenuItem.Size = new System.Drawing.Size(188, 26);
            this.generarFacturaToolStripMenuItem.Text = "Generar factura";
            this.generarFacturaToolStripMenuItem.Click += new System.EventHandler(this.generarFacturaToolStripMenuItem_Click);
            // 
            // Compras
            // 
            this.Compras.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generarSolicitudDeCotizaciónToolStripMenuItem,
            this.generarOrdenDeCompraToolStripMenuItem,
            this.corroborarRecepciónToolStripMenuItem});
            this.Compras.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.Compras.ForeColor = System.Drawing.Color.White;
            this.Compras.Name = "Compras";
            this.Compras.Size = new System.Drawing.Size(89, 40);
            this.Compras.Text = "Compras";
            // 
            // generarSolicitudDeCotizaciónToolStripMenuItem
            // 
            this.generarSolicitudDeCotizaciónToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.generarSolicitudDeCotizaciónToolStripMenuItem.Name = "generarSolicitudDeCotizaciónToolStripMenuItem";
            this.generarSolicitudDeCotizaciónToolStripMenuItem.Size = new System.Drawing.Size(292, 26);
            this.generarSolicitudDeCotizaciónToolStripMenuItem.Text = "Generar solicitud de cotización";
            this.generarSolicitudDeCotizaciónToolStripMenuItem.Click += new System.EventHandler(this.generarSolicitudDeCotizaciónToolStripMenuItem_Click);
            // 
            // generarOrdenDeCompraToolStripMenuItem
            // 
            this.generarOrdenDeCompraToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.generarOrdenDeCompraToolStripMenuItem.Name = "generarOrdenDeCompraToolStripMenuItem";
            this.generarOrdenDeCompraToolStripMenuItem.Size = new System.Drawing.Size(292, 26);
            this.generarOrdenDeCompraToolStripMenuItem.Text = "Generar orden de compra";
            this.generarOrdenDeCompraToolStripMenuItem.Click += new System.EventHandler(this.generarOrdenDeCompraToolStripMenuItem_Click);
            // 
            // corroborarRecepciónToolStripMenuItem
            // 
            this.corroborarRecepciónToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.corroborarRecepciónToolStripMenuItem.Name = "corroborarRecepciónToolStripMenuItem";
            this.corroborarRecepciónToolStripMenuItem.Size = new System.Drawing.Size(292, 26);
            this.corroborarRecepciónToolStripMenuItem.Text = "Corroborar recepción";
            this.corroborarRecepciónToolStripMenuItem.Click += new System.EventHandler(this.corroborarRecepciónToolStripMenuItem_Click);
            // 
            // Reportes
            // 
            this.Reportes.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.generarReporteToolStripMenuItem});
            this.Reportes.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.Reportes.ForeColor = System.Drawing.Color.White;
            this.Reportes.Name = "Reportes";
            this.Reportes.Size = new System.Drawing.Size(89, 40);
            this.Reportes.Text = "Reportes";
            // 
            // generarReporteToolStripMenuItem
            // 
            this.generarReporteToolStripMenuItem.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.generarReporteToolStripMenuItem.Name = "generarReporteToolStripMenuItem";
            this.generarReporteToolStripMenuItem.Size = new System.Drawing.Size(240, 26);
            this.generarReporteToolStripMenuItem.Text = "Generar reporte ventas";
            this.generarReporteToolStripMenuItem.Click += new System.EventHandler(this.generarReporteToolStripMenuItem_Click);
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
            // btnInicio
            // 
            this.btnInicio.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnInicio.ForeColor = System.Drawing.Color.White;
            this.btnInicio.Name = "btnInicio";
            this.btnInicio.Size = new System.Drawing.Size(71, 40);
            this.btnInicio.Text = "Ayuda";
            this.btnInicio.Click += new System.EventHandler(this.btnInicio_Click);
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
        private System.Windows.Forms.ToolStripMenuItem gestiónDeUsuariosToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem btnSesion;
        private System.Windows.Forms.ToolStripMenuItem cerrarSesionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cambiarClaveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem iniciarSesiónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generarFacturaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cambiarIdiomaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem gestiónDePerfilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generarReporteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eventosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem respaldosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem productosCToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generarSolicitudDeCotizaciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generarOrdenDeCompraToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem corroborarRecepciónToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem proveedoresToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem btnInicio;
    }
}

