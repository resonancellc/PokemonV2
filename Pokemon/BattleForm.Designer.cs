namespace Pokemon
{
    partial class BattleForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRun = new System.Windows.Forms.Button();
            this.btnSwitchPkmn = new System.Windows.Forms.Button();
            this.btnItem = new System.Windows.Forms.Button();
            this.panelAttackPool = new System.Windows.Forms.Panel();
            this.btnAttack2 = new System.Windows.Forms.Button();
            this.btnAttack4 = new System.Windows.Forms.Button();
            this.btnAttack3 = new System.Windows.Forms.Button();
            this.btnAttack1 = new System.Windows.Forms.Button();
            this.barPlayerPkmnHealth = new System.Windows.Forms.ProgressBar();
            this.lblPlayerPkmnName = new System.Windows.Forms.Label();
            this.lblPlayerPkmnHealth = new System.Windows.Forms.Label();
            this.progressBar2 = new System.Windows.Forms.ProgressBar();
            this.tbLog = new System.Windows.Forms.RichTextBox();
            this.barEnemyPkmnHealth = new System.Windows.Forms.ProgressBar();
            this.lblEnemyPkmnName = new System.Windows.Forms.Label();
            this.lblEnemyPkmnHealth = new System.Windows.Forms.Label();
            this.playerPkmnImage = new System.Windows.Forms.PictureBox();
            this.enemyPkmnImage = new System.Windows.Forms.PictureBox();
            this.lblPlayerPkmnLevel = new System.Windows.Forms.Label();
            this.lblEnemyPkmnLevel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panelAttackPool.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.playerPkmnImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemyPkmnImage)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRun);
            this.panel1.Controls.Add(this.btnSwitchPkmn);
            this.panel1.Controls.Add(this.btnItem);
            this.panel1.Location = new System.Drawing.Point(442, 271);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 80);
            this.panel1.TabIndex = 5;
            // 
            // btnRun
            // 
            this.btnRun.Font = new System.Drawing.Font("Unispace", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRun.Location = new System.Drawing.Point(101, 41);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(96, 36);
            this.btnRun.TabIndex = 0;
            this.btnRun.Text = "RUN";
            this.btnRun.UseVisualStyleBackColor = true;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // btnSwitchPkmn
            // 
            this.btnSwitchPkmn.Font = new System.Drawing.Font("Unispace", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSwitchPkmn.Location = new System.Drawing.Point(101, 3);
            this.btnSwitchPkmn.Name = "btnSwitchPkmn";
            this.btnSwitchPkmn.Size = new System.Drawing.Size(96, 36);
            this.btnSwitchPkmn.TabIndex = 0;
            this.btnSwitchPkmn.Text = "PKMN";
            this.btnSwitchPkmn.UseVisualStyleBackColor = true;
            this.btnSwitchPkmn.Click += new System.EventHandler(this.btnSwitchPkmn_Click);
            // 
            // btnItem
            // 
            this.btnItem.Font = new System.Drawing.Font("Unispace", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnItem.Location = new System.Drawing.Point(4, 41);
            this.btnItem.Name = "btnItem";
            this.btnItem.Size = new System.Drawing.Size(96, 36);
            this.btnItem.TabIndex = 0;
            this.btnItem.Text = "ITEM";
            this.btnItem.UseVisualStyleBackColor = true;
            this.btnItem.Click += new System.EventHandler(this.btnItem_Click);
            // 
            // panelAttackPool
            // 
            this.panelAttackPool.Controls.Add(this.btnAttack2);
            this.panelAttackPool.Controls.Add(this.btnAttack4);
            this.panelAttackPool.Controls.Add(this.btnAttack3);
            this.panelAttackPool.Controls.Add(this.btnAttack1);
            this.panelAttackPool.Location = new System.Drawing.Point(12, 276);
            this.panelAttackPool.Name = "panelAttackPool";
            this.panelAttackPool.Size = new System.Drawing.Size(200, 80);
            this.panelAttackPool.TabIndex = 1;
            // 
            // btnAttack2
            // 
            this.btnAttack2.Enabled = false;
            this.btnAttack2.Font = new System.Drawing.Font("Unispace", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttack2.Location = new System.Drawing.Point(100, 4);
            this.btnAttack2.Name = "btnAttack2";
            this.btnAttack2.Size = new System.Drawing.Size(96, 36);
            this.btnAttack2.TabIndex = 0;
            this.btnAttack2.Text = "---";
            this.btnAttack2.UseVisualStyleBackColor = true;
            this.btnAttack2.Click += new System.EventHandler(this.btnAttack_Click);
            // 
            // btnAttack4
            // 
            this.btnAttack4.Enabled = false;
            this.btnAttack4.Font = new System.Drawing.Font("Unispace", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttack4.Location = new System.Drawing.Point(100, 41);
            this.btnAttack4.Name = "btnAttack4";
            this.btnAttack4.Size = new System.Drawing.Size(96, 36);
            this.btnAttack4.TabIndex = 0;
            this.btnAttack4.Text = "---";
            this.btnAttack4.UseVisualStyleBackColor = true;
            this.btnAttack4.Click += new System.EventHandler(this.btnAttack_Click);
            // 
            // btnAttack3
            // 
            this.btnAttack3.Enabled = false;
            this.btnAttack3.Font = new System.Drawing.Font("Unispace", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttack3.Location = new System.Drawing.Point(4, 41);
            this.btnAttack3.Name = "btnAttack3";
            this.btnAttack3.Size = new System.Drawing.Size(96, 36);
            this.btnAttack3.TabIndex = 0;
            this.btnAttack3.Text = "---";
            this.btnAttack3.UseVisualStyleBackColor = true;
            this.btnAttack3.Click += new System.EventHandler(this.btnAttack_Click);
            // 
            // btnAttack1
            // 
            this.btnAttack1.Enabled = false;
            this.btnAttack1.Font = new System.Drawing.Font("Unispace", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAttack1.Location = new System.Drawing.Point(4, 4);
            this.btnAttack1.Name = "btnAttack1";
            this.btnAttack1.Size = new System.Drawing.Size(96, 36);
            this.btnAttack1.TabIndex = 0;
            this.btnAttack1.Text = "---";
            this.btnAttack1.UseVisualStyleBackColor = true;
            this.btnAttack1.Click += new System.EventHandler(this.btnAttack_Click);
            // 
            // barPlayerPkmnHealth
            // 
            this.barPlayerPkmnHealth.ForeColor = System.Drawing.Color.IndianRed;
            this.barPlayerPkmnHealth.Location = new System.Drawing.Point(440, 234);
            this.barPlayerPkmnHealth.Name = "barPlayerPkmnHealth";
            this.barPlayerPkmnHealth.Size = new System.Drawing.Size(200, 14);
            this.barPlayerPkmnHealth.TabIndex = 6;
            // 
            // lblPlayerPkmnName
            // 
            this.lblPlayerPkmnName.AutoSize = true;
            this.lblPlayerPkmnName.Font = new System.Drawing.Font("Unispace", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerPkmnName.Location = new System.Drawing.Point(437, 216);
            this.lblPlayerPkmnName.Name = "lblPlayerPkmnName";
            this.lblPlayerPkmnName.Size = new System.Drawing.Size(71, 15);
            this.lblPlayerPkmnName.TabIndex = 7;
            this.lblPlayerPkmnName.Text = "pkmnName";
            // 
            // lblPlayerPkmnHealth
            // 
            this.lblPlayerPkmnHealth.AutoSize = true;
            this.lblPlayerPkmnHealth.Font = new System.Drawing.Font("Unispace", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerPkmnHealth.Location = new System.Drawing.Point(577, 216);
            this.lblPlayerPkmnHealth.Name = "lblPlayerPkmnHealth";
            this.lblPlayerPkmnHealth.Size = new System.Drawing.Size(63, 15);
            this.lblPlayerPkmnHealth.TabIndex = 7;
            this.lblPlayerPkmnHealth.Text = "999/999";
            // 
            // progressBar2
            // 
            this.progressBar2.ForeColor = System.Drawing.Color.LightSeaGreen;
            this.progressBar2.Location = new System.Drawing.Point(440, 254);
            this.progressBar2.Name = "progressBar2";
            this.progressBar2.Size = new System.Drawing.Size(200, 7);
            this.progressBar2.TabIndex = 6;
            this.progressBar2.Value = 50;
            // 
            // tbLog
            // 
            this.tbLog.Font = new System.Drawing.Font("Unispace", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbLog.Location = new System.Drawing.Point(11, 362);
            this.tbLog.Name = "tbLog";
            this.tbLog.ReadOnly = true;
            this.tbLog.Size = new System.Drawing.Size(634, 132);
            this.tbLog.TabIndex = 8;
            this.tbLog.Text = "";
            this.tbLog.TextChanged += new System.EventHandler(this.tbLog_TextChanged);
            // 
            // barEnemyPkmnHealth
            // 
            this.barEnemyPkmnHealth.ForeColor = System.Drawing.Color.IndianRed;
            this.barEnemyPkmnHealth.Location = new System.Drawing.Point(11, 27);
            this.barEnemyPkmnHealth.Name = "barEnemyPkmnHealth";
            this.barEnemyPkmnHealth.Size = new System.Drawing.Size(200, 14);
            this.barEnemyPkmnHealth.TabIndex = 6;
            // 
            // lblEnemyPkmnName
            // 
            this.lblEnemyPkmnName.AutoSize = true;
            this.lblEnemyPkmnName.Font = new System.Drawing.Font("Unispace", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnemyPkmnName.Location = new System.Drawing.Point(8, 9);
            this.lblEnemyPkmnName.Name = "lblEnemyPkmnName";
            this.lblEnemyPkmnName.Size = new System.Drawing.Size(71, 15);
            this.lblEnemyPkmnName.TabIndex = 7;
            this.lblEnemyPkmnName.Text = "pkmnName";
            // 
            // lblEnemyPkmnHealth
            // 
            this.lblEnemyPkmnHealth.AutoSize = true;
            this.lblEnemyPkmnHealth.Font = new System.Drawing.Font("Unispace", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnemyPkmnHealth.Location = new System.Drawing.Point(148, 9);
            this.lblEnemyPkmnHealth.Name = "lblEnemyPkmnHealth";
            this.lblEnemyPkmnHealth.Size = new System.Drawing.Size(63, 15);
            this.lblEnemyPkmnHealth.TabIndex = 7;
            this.lblEnemyPkmnHealth.Text = "999/999";
            // 
            // playerPkmnImage
            // 
            this.playerPkmnImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.playerPkmnImage.Image = global::Pokemon.Properties.Resources._013_weedle_back;
            this.playerPkmnImage.Location = new System.Drawing.Point(51, 172);
            this.playerPkmnImage.Name = "playerPkmnImage";
            this.playerPkmnImage.Size = new System.Drawing.Size(120, 120);
            this.playerPkmnImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.playerPkmnImage.TabIndex = 9;
            this.playerPkmnImage.TabStop = false;
            // 
            // enemyPkmnImage
            // 
            this.enemyPkmnImage.Location = new System.Drawing.Point(545, 12);
            this.enemyPkmnImage.Name = "enemyPkmnImage";
            this.enemyPkmnImage.Size = new System.Drawing.Size(100, 100);
            this.enemyPkmnImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.enemyPkmnImage.TabIndex = 9;
            this.enemyPkmnImage.TabStop = false;
            // 
            // lblPlayerPkmnLevel
            // 
            this.lblPlayerPkmnLevel.AutoSize = true;
            this.lblPlayerPkmnLevel.Font = new System.Drawing.Font("Unispace", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPlayerPkmnLevel.Location = new System.Drawing.Point(532, 216);
            this.lblPlayerPkmnLevel.Name = "lblPlayerPkmnLevel";
            this.lblPlayerPkmnLevel.Size = new System.Drawing.Size(39, 15);
            this.lblPlayerPkmnLevel.TabIndex = 7;
            this.lblPlayerPkmnLevel.Text = "L100";
            // 
            // lblEnemyPkmnLevel
            // 
            this.lblEnemyPkmnLevel.AutoSize = true;
            this.lblEnemyPkmnLevel.Font = new System.Drawing.Font("Unispace", 9.749999F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEnemyPkmnLevel.Location = new System.Drawing.Point(103, 9);
            this.lblEnemyPkmnLevel.Name = "lblEnemyPkmnLevel";
            this.lblEnemyPkmnLevel.Size = new System.Drawing.Size(39, 15);
            this.lblEnemyPkmnLevel.TabIndex = 7;
            this.lblEnemyPkmnLevel.Text = "L100";
            // 
            // BattleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 506);
            this.Controls.Add(this.enemyPkmnImage);
            this.Controls.Add(this.tbLog);
            this.Controls.Add(this.lblEnemyPkmnHealth);
            this.Controls.Add(this.lblEnemyPkmnName);
            this.Controls.Add(this.lblPlayerPkmnHealth);
            this.Controls.Add(this.lblEnemyPkmnLevel);
            this.Controls.Add(this.lblPlayerPkmnLevel);
            this.Controls.Add(this.lblPlayerPkmnName);
            this.Controls.Add(this.barEnemyPkmnHealth);
            this.Controls.Add(this.progressBar2);
            this.Controls.Add(this.barPlayerPkmnHealth);
            this.Controls.Add(this.panelAttackPool);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.playerPkmnImage);
            this.Name = "BattleForm";
            this.Text = "Battle";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panelAttackPool.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.playerPkmnImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.enemyPkmnImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panelAttackPool;
        private System.Windows.Forms.Button btnAttack1;
        private System.Windows.Forms.ProgressBar barPlayerPkmnHealth;
        private System.Windows.Forms.Label lblPlayerPkmnName;
        private System.Windows.Forms.Label lblPlayerPkmnHealth;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button btnSwitchPkmn;
        private System.Windows.Forms.Button btnItem;
        private System.Windows.Forms.RichTextBox tbLog;
        private System.Windows.Forms.ProgressBar barEnemyPkmnHealth;
        private System.Windows.Forms.Label lblEnemyPkmnName;
        private System.Windows.Forms.Label lblEnemyPkmnHealth;
        private System.Windows.Forms.PictureBox playerPkmnImage;
        private System.Windows.Forms.PictureBox enemyPkmnImage;
        private System.Windows.Forms.Button btnAttack2;
        private System.Windows.Forms.Button btnAttack4;
        private System.Windows.Forms.Button btnAttack3;
        private System.Windows.Forms.Label lblPlayerPkmnLevel;
        private System.Windows.Forms.Label lblEnemyPkmnLevel;
    }
}