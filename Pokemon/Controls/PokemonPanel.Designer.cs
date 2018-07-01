namespace Pokemon
{
    partial class PokemonPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblName = new System.Windows.Forms.Label();
            this.barPkmnHealth = new System.Windows.Forms.ProgressBar();
            this.lblHealth = new System.Windows.Forms.Label();
            this.lblLevel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(38, 0);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(61, 13);
            this.lblName.TabIndex = 0;
            this.lblName.Text = "pkmnName";
            // 
            // barPkmnHealth
            // 
            this.barPkmnHealth.ForeColor = System.Drawing.Color.Chartreuse;
            this.barPkmnHealth.Location = new System.Drawing.Point(3, 34);
            this.barPkmnHealth.Name = "barPkmnHealth";
            this.barPkmnHealth.Size = new System.Drawing.Size(294, 14);
            this.barPkmnHealth.TabIndex = 0;
            // 
            // lblHealth
            // 
            this.lblHealth.AutoSize = true;
            this.lblHealth.Location = new System.Drawing.Point(13, 18);
            this.lblHealth.Name = "lblHealth";
            this.lblHealth.Size = new System.Drawing.Size(48, 13);
            this.lblHealth.TabIndex = 5;
            this.lblHealth.Text = "999/999";
            // 
            // lblLevel
            // 
            this.lblLevel.AutoSize = true;
            this.lblLevel.Location = new System.Drawing.Point(3, 0);
            this.lblLevel.Name = "lblLevel";
            this.lblLevel.Size = new System.Drawing.Size(35, 13);
            this.lblLevel.TabIndex = 0;
            this.lblLevel.Text = "100lvl";
            // 
            // PokemonPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.barPkmnHealth);
            this.Controls.Add(this.lblLevel);
            this.Controls.Add(this.lblHealth);
            this.Controls.Add(this.lblName);
            this.Name = "PokemonPanel";
            this.Size = new System.Drawing.Size(300, 50);
            this.Load += new System.EventHandler(this.PokemonPanel_Load);
            this.Click += new System.EventHandler(this.PokemonPanel_Click);
            this.DoubleClick += new System.EventHandler(this.PokemonPanel_DoubleClick);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblName;
        private System.Windows.Forms.ProgressBar barPkmnHealth;
        private System.Windows.Forms.Label lblHealth;
        private System.Windows.Forms.Label lblLevel;
    }
}
