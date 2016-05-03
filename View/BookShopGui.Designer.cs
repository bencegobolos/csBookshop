namespace BookShop.View
{
    partial class BookShopGui
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BookShopGui));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newCustomerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.buyBookToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listCustomersToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listAvailableBooksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.booksDataGridView = new System.Windows.Forms.DataGridView();
            this.menuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.booksDataGridView)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.dataToolStripMenuItem,
            this.listToolStripMenuItem});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(784, 24);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.ExitToolStripMenuItem_Click);
            // 
            // dataToolStripMenuItem
            // 
            this.dataToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newCustomerToolStripMenuItem,
            this.buyBookToolStripMenuItem});
            this.dataToolStripMenuItem.Name = "dataToolStripMenuItem";
            this.dataToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.dataToolStripMenuItem.Text = "&Data";
            // 
            // newCustomerToolStripMenuItem
            // 
            this.newCustomerToolStripMenuItem.Name = "newCustomerToolStripMenuItem";
            this.newCustomerToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.newCustomerToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.newCustomerToolStripMenuItem.Text = "New Customer...";
            this.newCustomerToolStripMenuItem.Click += new System.EventHandler(this.NewCustomerToolStripMenuItem_Click);
            // 
            // buyBookToolStripMenuItem
            // 
            this.buyBookToolStripMenuItem.Name = "buyBookToolStripMenuItem";
            this.buyBookToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.buyBookToolStripMenuItem.Size = new System.Drawing.Size(181, 22);
            this.buyBookToolStripMenuItem.Text = "Buy Book...";
            this.buyBookToolStripMenuItem.Click += new System.EventHandler(this.BuyBookToolStripMenuItem_Click);
            // 
            // listToolStripMenuItem
            // 
            this.listToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.listCustomersToolStripMenuItem,
            this.listAvailableBooksToolStripMenuItem});
            this.listToolStripMenuItem.Name = "listToolStripMenuItem";
            this.listToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.listToolStripMenuItem.Text = "&List";
            // 
            // listCustomersToolStripMenuItem
            // 
            this.listCustomersToolStripMenuItem.Name = "listCustomersToolStripMenuItem";
            this.listCustomersToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.listCustomersToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.listCustomersToolStripMenuItem.Text = "List Customers";
            // 
            // listAvailableBooksToolStripMenuItem
            // 
            this.listAvailableBooksToolStripMenuItem.Name = "listAvailableBooksToolStripMenuItem";
            this.listAvailableBooksToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.listAvailableBooksToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.listAvailableBooksToolStripMenuItem.Text = "List Available Books";
            this.listAvailableBooksToolStripMenuItem.Click += new System.EventHandler(this.listAvailableBooksToolStripMenuItem_Click);
            // 
            // booksDataGridView
            // 
            this.booksDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.booksDataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.booksDataGridView.Location = new System.Drawing.Point(0, 24);
            this.booksDataGridView.Name = "booksDataGridView";
            this.booksDataGridView.Size = new System.Drawing.Size(784, 538);
            this.booksDataGridView.TabIndex = 1;
            this.booksDataGridView.Visible = false;
            // 
            // BookShopGui
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.booksDataGridView);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip;
            this.Name = "BookShopGui";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Book Shop Application";
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.booksDataGridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem dataToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newCustomerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem buyBookToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listCustomersToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listAvailableBooksToolStripMenuItem;
        private System.Windows.Forms.DataGridView booksDataGridView;
    }
}

