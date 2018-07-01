namespace Pokemon
{
    partial class AfterWinForm
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
            this.btnContinue = new System.Windows.Forms.Button();
            this.btnQuit = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.rbWinnings = new System.Windows.Forms.RichTextBox();
            this.btnGoToShop = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnContinue
            // 
            this.btnContinue.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnContinue.Location = new System.Drawing.Point(388, 226);
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.Size = new System.Drawing.Size(75, 23);
            this.btnContinue.TabIndex = 0;
            this.btnContinue.Text = "Next fight";
            this.btnContinue.UseVisualStyleBackColor = true;
            // 
            // btnQuit
            // 
            this.btnQuit.Location = new System.Drawing.Point(13, 226);
            this.btnQuit.Name = "btnQuit";
            this.btnQuit.Size = new System.Drawing.Size(75, 23);
            this.btnQuit.TabIndex = 1;
            this.btnQuit.Text = "Quit";
            this.btnQuit.UseVisualStyleBackColor = true;
            this.btnQuit.Click += new System.EventHandler(this.btnQuit_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Unispace", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(319, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "You won this fight, but battle goes on!";
            // 
            // rbWinnings
            // 
            this.rbWinnings.Location = new System.Drawing.Point(13, 28);
            this.rbWinnings.Name = "rbWinnings";
            this.rbWinnings.Size = new System.Drawing.Size(450, 96);
            this.rbWinnings.TabIndex = 3;
            this.rbWinnings.Text = "";
            // 
            // btnGoToShop
            // 
            this.btnGoToShop.Location = new System.Drawing.Point(388, 131);
            this.btnGoToShop.Name = "btnGoToShop";
            this.btnGoToShop.Size = new System.Drawing.Size(75, 23);
            this.btnGoToShop.TabIndex = 4;
            this.btnGoToShop.Text = "Shop";
            this.btnGoToShop.UseVisualStyleBackColor = true;
            this.btnGoToShop.Click += new System.EventHandler(this.btnGoToShop_Click);
            // 
            // AfterWinForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(475, 261);
            this.Controls.Add(this.btnGoToShop);
            this.Controls.Add(this.rbWinnings);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnQuit);
            this.Controls.Add(this.btnContinue);
            this.Name = "AfterWinForm";
            this.ShowIcon = false;
            this.Text = "You won!";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnContinue;
        private System.Windows.Forms.Button btnQuit;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rbWinnings;
        private System.Windows.Forms.Button btnGoToShop;
    }
}