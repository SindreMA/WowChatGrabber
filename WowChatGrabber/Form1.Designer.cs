namespace ResponseChecker
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("test");
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("test2");
            this.buttonstart = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.Xlabel = new System.Windows.Forms.Label();
            this.Ylabel = new System.Windows.Forms.Label();
            this.XValue = new System.Windows.Forms.Label();
            this.YValue = new System.Windows.Forms.Label();
            this.listView1 = new System.Windows.Forms.ListView();
            this.SuspendLayout();
            // 
            // buttonstart
            // 
            this.buttonstart.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonstart.Location = new System.Drawing.Point(215, 6);
            this.buttonstart.Margin = new System.Windows.Forms.Padding(0);
            this.buttonstart.Name = "buttonstart";
            this.buttonstart.Size = new System.Drawing.Size(141, 41);
            this.buttonstart.TabIndex = 13;
            this.buttonstart.Text = "Select area";
            this.buttonstart.UseVisualStyleBackColor = true;
            this.buttonstart.Click += new System.EventHandler(this.buttonstart_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Selected area";
            // 
            // Xlabel
            // 
            this.Xlabel.AutoSize = true;
            this.Xlabel.Location = new System.Drawing.Point(12, 22);
            this.Xlabel.Name = "Xlabel";
            this.Xlabel.Size = new System.Drawing.Size(17, 13);
            this.Xlabel.TabIndex = 15;
            this.Xlabel.Text = "X:";
            // 
            // Ylabel
            // 
            this.Ylabel.AutoSize = true;
            this.Ylabel.Location = new System.Drawing.Point(104, 22);
            this.Ylabel.Name = "Ylabel";
            this.Ylabel.Size = new System.Drawing.Size(17, 13);
            this.Ylabel.TabIndex = 16;
            this.Ylabel.Text = "Y:";
            // 
            // XValue
            // 
            this.XValue.AutoSize = true;
            this.XValue.Location = new System.Drawing.Point(35, 22);
            this.XValue.Name = "XValue";
            this.XValue.Size = new System.Drawing.Size(0, 13);
            this.XValue.TabIndex = 17;
            // 
            // YValue
            // 
            this.YValue.AutoSize = true;
            this.YValue.Location = new System.Drawing.Point(121, 22);
            this.YValue.Name = "YValue";
            this.YValue.Size = new System.Drawing.Size(0, 13);
            this.YValue.TabIndex = 18;
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2});
            this.listView1.Location = new System.Drawing.Point(12, 50);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(344, 159);
            this.listView1.TabIndex = 19;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.List;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(368, 221);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.YValue);
            this.Controls.Add(this.XValue);
            this.Controls.Add(this.Ylabel);
            this.Controls.Add(this.Xlabel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonstart);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Form1";
            this.Text = "WowChatGrabber";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button buttonstart;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Xlabel;
        private System.Windows.Forms.Label Ylabel;
        private System.Windows.Forms.Label XValue;
        private System.Windows.Forms.Label YValue;
        private System.Windows.Forms.ListView listView1;
    }
}

