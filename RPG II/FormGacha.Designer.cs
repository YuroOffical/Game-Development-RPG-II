namespace RPG_II
{
    partial class FormGacha
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
            this.pbox_chest = new System.Windows.Forms.PictureBox();
            this.pnl_main = new System.Windows.Forms.Panel();
            this.pbox_reward = new System.Windows.Forms.PictureBox();
            this.btn_open = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_chest)).BeginInit();
            this.pnl_main.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_reward)).BeginInit();
            this.SuspendLayout();
            // 
            // pbox_chest
            // 
            this.pbox_chest.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pbox_chest.Image = global::RPG_II.Properties.Resources.MT_treasure;
            this.pbox_chest.Location = new System.Drawing.Point(443, 131);
            this.pbox_chest.Name = "pbox_chest";
            this.pbox_chest.Size = new System.Drawing.Size(300, 300);
            this.pbox_chest.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbox_chest.TabIndex = 0;
            this.pbox_chest.TabStop = false;
            // 
            // pnl_main
            // 
            this.pnl_main.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnl_main.Controls.Add(this.label1);
            this.pnl_main.Controls.Add(this.pbox_reward);
            this.pnl_main.Controls.Add(this.btn_open);
            this.pnl_main.Controls.Add(this.pbox_chest);
            this.pnl_main.Location = new System.Drawing.Point(12, 12);
            this.pnl_main.Name = "pnl_main";
            this.pnl_main.Size = new System.Drawing.Size(1108, 512);
            this.pnl_main.TabIndex = 1;
            // 
            // pbox_reward
            // 
            this.pbox_reward.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pbox_reward.Image = global::RPG_II.Properties.Resources.MT_treasure;
            this.pbox_reward.Location = new System.Drawing.Point(550, 182);
            this.pbox_reward.Name = "pbox_reward";
            this.pbox_reward.Size = new System.Drawing.Size(76, 68);
            this.pbox_reward.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbox_reward.TabIndex = 2;
            this.pbox_reward.TabStop = false;
            this.pbox_reward.Visible = false;
            // 
            // btn_open
            // 
            this.btn_open.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_open.Location = new System.Drawing.Point(502, 451);
            this.btn_open.Name = "btn_open";
            this.btn_open.Size = new System.Drawing.Size(165, 50);
            this.btn_open.TabIndex = 1;
            this.btn_open.Text = "I xxx ඞ I";
            this.btn_open.UseVisualStyleBackColor = true;
            this.btn_open.Click += new System.EventHandler(this.button1_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("MS UI Gothic", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.label1.Location = new System.Drawing.Point(370, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(450, 27);
            this.label1.TabIndex = 3;
            this.label1.Text = "Totally not a Gacha Hall for Equipment";
            // 
            // FormGacha
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 524);
            this.Controls.Add(this.pnl_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormGacha";
            this.Text = "FormGacha";
            ((System.ComponentModel.ISupportInitialize)(this.pbox_chest)).EndInit();
            this.pnl_main.ResumeLayout(false);
            this.pnl_main.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pbox_reward)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbox_chest;
        private System.Windows.Forms.Panel pnl_main;
        private System.Windows.Forms.Button btn_open;
        private System.Windows.Forms.PictureBox pbox_reward;
        private System.Windows.Forms.Label label1;
    }
}