namespace EX._01_saori_
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
            this.button1 = new System.Windows.Forms.Button();
            this.brp = new System.Windows.Forms.Button();
            this.bselLine = new System.Windows.Forms.Button();
            this.brlin = new System.Windows.Forms.Button();
            this.bselPlt = new System.Windows.Forms.Button();
            this.brplt = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(29, 34);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(111, 32);
            this.button1.TabIndex = 0;
            this.button1.Text = "Select a Point";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // brp
            // 
            this.brp.Location = new System.Drawing.Point(333, 34);
            this.brp.Name = "brp";
            this.brp.Size = new System.Drawing.Size(111, 32);
            this.brp.TabIndex = 1;
            this.brp.Text = "Rename";
            this.brp.UseVisualStyleBackColor = true;
            this.brp.Click += new System.EventHandler(this.brp_Click);
            // 
            // bselLine
            // 
            this.bselLine.Location = new System.Drawing.Point(29, 93);
            this.bselLine.Name = "bselLine";
            this.bselLine.Size = new System.Drawing.Size(111, 32);
            this.bselLine.TabIndex = 2;
            this.bselLine.Text = "Select a line";
            this.bselLine.UseVisualStyleBackColor = true;
            this.bselLine.Click += new System.EventHandler(this.bselLine_Click);
            // 
            // brlin
            // 
            this.brlin.Location = new System.Drawing.Point(333, 93);
            this.brlin.Name = "brlin";
            this.brlin.Size = new System.Drawing.Size(111, 32);
            this.brlin.TabIndex = 3;
            this.brlin.Text = "Rename";
            this.brlin.UseVisualStyleBackColor = true;
            this.brlin.Click += new System.EventHandler(this.brlin_Click);
            // 
            // bselPlt
            // 
            this.bselPlt.Location = new System.Drawing.Point(29, 146);
            this.bselPlt.Name = "bselPlt";
            this.bselPlt.Size = new System.Drawing.Size(111, 32);
            this.bselPlt.TabIndex = 4;
            this.bselPlt.Text = "Select a plan";
            this.bselPlt.UseVisualStyleBackColor = true;
            this.bselPlt.Click += new System.EventHandler(this.bselPlt_Click);
            // 
            // brplt
            // 
            this.brplt.Location = new System.Drawing.Point(333, 146);
            this.brplt.Name = "brplt";
            this.brplt.Size = new System.Drawing.Size(111, 32);
            this.brplt.TabIndex = 5;
            this.brplt.Text = "Rename";
            this.brplt.UseVisualStyleBackColor = true;
            this.brplt.Click += new System.EventHandler(this.brplt_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(178, 41);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(122, 21);
            this.textBox1.TabIndex = 6;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(178, 100);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(122, 21);
            this.textBox2.TabIndex = 7;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(178, 153);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(122, 21);
            this.textBox3.TabIndex = 8;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 292);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.brplt);
            this.Controls.Add(this.bselPlt);
            this.Controls.Add(this.brlin);
            this.Controls.Add(this.bselLine);
            this.Controls.Add(this.brp);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button brp;
        private System.Windows.Forms.Button bselLine;
        private System.Windows.Forms.Button brlin;
        private System.Windows.Forms.Button bselPlt;
        private System.Windows.Forms.Button brplt;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
    }
}

