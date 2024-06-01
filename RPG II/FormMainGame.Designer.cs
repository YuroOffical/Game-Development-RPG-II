namespace RPG_II
{
    partial class FormMainGame
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
            this.pnl_main = new System.Windows.Forms.Panel();
            this.btn_exit = new System.Windows.Forms.Button();
            this.btn_check = new System.Windows.Forms.Button();
            this.btn_equipment = new System.Windows.Forms.Button();
            this.btn_gacha = new System.Windows.Forms.Button();
            this.btn_map = new System.Windows.Forms.Button();
            this.lbl_cash = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // pnl_main
            // 
            this.pnl_main.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.pnl_main.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnl_main.Location = new System.Drawing.Point(12, 35);
            this.pnl_main.Name = "pnl_main";
            this.pnl_main.Size = new System.Drawing.Size(1132, 524);
            this.pnl_main.TabIndex = 0;
            // 
            // btn_exit
            // 
            this.btn_exit.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_exit.Location = new System.Drawing.Point(930, 590);
            this.btn_exit.Name = "btn_exit";
            this.btn_exit.Size = new System.Drawing.Size(214, 56);
            this.btn_exit.TabIndex = 2;
            this.btn_exit.Text = "Back to Menu";
            this.btn_exit.UseVisualStyleBackColor = true;
            this.btn_exit.Click += new System.EventHandler(this.btn_exit_Click);
            // 
            // btn_check
            // 
            this.btn_check.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_check.Location = new System.Drawing.Point(12, 590);
            this.btn_check.Name = "btn_check";
            this.btn_check.Size = new System.Drawing.Size(214, 56);
            this.btn_check.TabIndex = 3;
            this.btn_check.Text = "Check Team";
            this.btn_check.UseVisualStyleBackColor = true;
            this.btn_check.Click += new System.EventHandler(this.btn_check_Click);
            // 
            // btn_equipment
            // 
            this.btn_equipment.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_equipment.Location = new System.Drawing.Point(242, 590);
            this.btn_equipment.Name = "btn_equipment";
            this.btn_equipment.Size = new System.Drawing.Size(214, 56);
            this.btn_equipment.TabIndex = 4;
            this.btn_equipment.Text = "Equipment";
            this.btn_equipment.UseVisualStyleBackColor = true;
            this.btn_equipment.Click += new System.EventHandler(this.btn_equipment_Click);
            // 
            // btn_gacha
            // 
            this.btn_gacha.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_gacha.Location = new System.Drawing.Point(701, 590);
            this.btn_gacha.Name = "btn_gacha";
            this.btn_gacha.Size = new System.Drawing.Size(214, 56);
            this.btn_gacha.TabIndex = 5;
            this.btn_gacha.Text = "Not Gacha";
            this.btn_gacha.UseVisualStyleBackColor = true;
            this.btn_gacha.Click += new System.EventHandler(this.btn_gacha_Click);
            // 
            // btn_map
            // 
            this.btn_map.Font = new System.Drawing.Font("MS UI Gothic", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btn_map.Location = new System.Drawing.Point(471, 590);
            this.btn_map.Name = "btn_map";
            this.btn_map.Size = new System.Drawing.Size(214, 56);
            this.btn_map.TabIndex = 6;
            this.btn_map.Text = "Adventure Map";
            this.btn_map.UseVisualStyleBackColor = true;
            this.btn_map.Click += new System.EventHandler(this.btn_map_Click);
            // 
            // lbl_cash
            // 
            this.lbl_cash.AutoSize = true;
            this.lbl_cash.Font = new System.Drawing.Font("MS UI Gothic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.lbl_cash.Location = new System.Drawing.Point(900, 8);
            this.lbl_cash.Name = "lbl_cash";
            this.lbl_cash.Size = new System.Drawing.Size(67, 24);
            this.lbl_cash.TabIndex = 7;
            this.lbl_cash.Text = "label1";
            // 
            // FormMainGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1156, 666);
            this.Controls.Add(this.lbl_cash);
            this.Controls.Add(this.btn_map);
            this.Controls.Add(this.btn_gacha);
            this.Controls.Add(this.btn_equipment);
            this.Controls.Add(this.btn_check);
            this.Controls.Add(this.btn_exit);
            this.Controls.Add(this.pnl_main);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormMainGame";
            this.Text = "FormMainGame";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnl_main;
        private System.Windows.Forms.Button btn_exit;
        private System.Windows.Forms.Button btn_check;
        private System.Windows.Forms.Button btn_equipment;
        private System.Windows.Forms.Button btn_gacha;
        private System.Windows.Forms.Button btn_map;
        private System.Windows.Forms.Label lbl_cash;
    }
}