namespace RPG_II
{
    partial class FormEquipment
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
            this.savebtn = new System.Windows.Forms.Button();
            this.btnback = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // savebtn
            // 
            this.savebtn.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.savebtn.Location = new System.Drawing.Point(988, 43);
            this.savebtn.Name = "savebtn";
            this.savebtn.Size = new System.Drawing.Size(86, 78);
            this.savebtn.TabIndex = 1;
            this.savebtn.Text = "Save\r\nLoadout";
            this.savebtn.UseVisualStyleBackColor = true;
            this.savebtn.Click += new System.EventHandler(this.savebtn_Click_1);
            // 
            // btnback
            // 
            this.btnback.Font = new System.Drawing.Font("MS UI Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(128)));
            this.btnback.Location = new System.Drawing.Point(988, 138);
            this.btnback.Name = "btnback";
            this.btnback.Size = new System.Drawing.Size(86, 78);
            this.btnback.TabIndex = 2;
            this.btnback.Text = "Clear\r\nLoadout";
            this.btnback.UseVisualStyleBackColor = true;
            this.btnback.Click += new System.EventHandler(this.btnback_Click_1);
            // 
            // FormEquipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1132, 524);
            this.Controls.Add(this.btnback);
            this.Controls.Add(this.savebtn);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormEquipment";
            this.Text = "FormEquipment";
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button savebtn;
        private System.Windows.Forms.Button btnback;
    }
}