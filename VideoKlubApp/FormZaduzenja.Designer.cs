namespace VideoKlubApp
{
    partial class FormZaduzenja
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
            this.comboKlijent = new System.Windows.Forms.ComboBox();
            this.comboFilm = new System.Windows.Forms.ComboBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btnZaduzi = new System.Windows.Forms.Button();
            this.btnVrati = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // comboKlijent
            // 
            this.comboKlijent.FormattingEnabled = true;
            this.comboKlijent.Location = new System.Drawing.Point(66, 120);
            this.comboKlijent.Name = "comboKlijent";
            this.comboKlijent.Size = new System.Drawing.Size(121, 21);
            this.comboKlijent.TabIndex = 0;
            // 
            // comboFilm
            // 
            this.comboFilm.FormattingEnabled = true;
            this.comboFilm.Location = new System.Drawing.Point(66, 183);
            this.comboFilm.Name = "comboFilm";
            this.comboFilm.Size = new System.Drawing.Size(121, 21);
            this.comboFilm.TabIndex = 1;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 288);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(776, 150);
            this.dataGridView1.TabIndex = 2;
            // 
            // btnZaduzi
            // 
            this.btnZaduzi.Location = new System.Drawing.Point(314, 120);
            this.btnZaduzi.Name = "btnZaduzi";
            this.btnZaduzi.Size = new System.Drawing.Size(75, 23);
            this.btnZaduzi.TabIndex = 3;
            this.btnZaduzi.Text = "Zaduži";
            this.btnZaduzi.UseVisualStyleBackColor = true;
            this.btnZaduzi.Click += new System.EventHandler(this.btnZaduzi_Click);
            // 
            // btnVrati
            // 
            this.btnVrati.Location = new System.Drawing.Point(314, 183);
            this.btnVrati.Name = "btnVrati";
            this.btnVrati.Size = new System.Drawing.Size(75, 23);
            this.btnVrati.TabIndex = 4;
            this.btnVrati.Text = "Vrati";
            this.btnVrati.UseVisualStyleBackColor = true;
            this.btnVrati.Click += new System.EventHandler(this.btnVrati_Click);
            // 
            // FormZaduzenja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnVrati);
            this.Controls.Add(this.btnZaduzi);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.comboFilm);
            this.Controls.Add(this.comboKlijent);
            this.Name = "FormZaduzenja";
            this.Text = "FormZaduzenja";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboKlijent;
        private System.Windows.Forms.ComboBox comboFilm;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Button btnZaduzi;
        private System.Windows.Forms.Button btnVrati;
    }
}