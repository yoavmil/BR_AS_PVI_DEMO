namespace Client2
{
    partial class Form1
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
            this.connectControllerBtn = new System.Windows.Forms.Button();
            this.connectVarBtn = new System.Windows.Forms.Button();
            this.statusLbl = new System.Windows.Forms.Label();
            this.counterLbl = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // connectControllerBtn
            // 
            this.connectControllerBtn.Location = new System.Drawing.Point(13, 13);
            this.connectControllerBtn.Name = "connectControllerBtn";
            this.connectControllerBtn.Size = new System.Drawing.Size(116, 23);
            this.connectControllerBtn.TabIndex = 0;
            this.connectControllerBtn.Text = "connect controller";
            this.connectControllerBtn.UseVisualStyleBackColor = true;
            this.connectControllerBtn.Click += new System.EventHandler(this.connectControllerBtn_Click);
            // 
            // connectVarBtn
            // 
            this.connectVarBtn.Enabled = false;
            this.connectVarBtn.Location = new System.Drawing.Point(13, 43);
            this.connectVarBtn.Name = "connectVarBtn";
            this.connectVarBtn.Size = new System.Drawing.Size(116, 23);
            this.connectVarBtn.TabIndex = 1;
            this.connectVarBtn.Text = "connect variable";
            this.connectVarBtn.UseVisualStyleBackColor = true;
            this.connectVarBtn.Click += new System.EventHandler(this.conectVarBtn_Click);
            // 
            // statusLbl
            // 
            this.statusLbl.AutoSize = true;
            this.statusLbl.Location = new System.Drawing.Point(12, 69);
            this.statusLbl.Name = "statusLbl";
            this.statusLbl.Size = new System.Drawing.Size(86, 13);
            this.statusLbl.TabIndex = 2;
            this.statusLbl.Text = "status: waiting ...";
            // 
            // counterLbl
            // 
            this.counterLbl.AutoSize = true;
            this.counterLbl.Location = new System.Drawing.Point(10, 82);
            this.counterLbl.Name = "counterLbl";
            this.counterLbl.Size = new System.Drawing.Size(0, 13);
            this.counterLbl.TabIndex = 3;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(181, 124);
            this.Controls.Add(this.counterLbl);
            this.Controls.Add(this.statusLbl);
            this.Controls.Add(this.connectVarBtn);
            this.Controls.Add(this.connectControllerBtn);
            this.Name = "Form1";
            this.Text = "PVI Demo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button connectControllerBtn;
        private System.Windows.Forms.Button connectVarBtn;
        private System.Windows.Forms.Label statusLbl;
        private System.Windows.Forms.Label counterLbl;
    }
}

