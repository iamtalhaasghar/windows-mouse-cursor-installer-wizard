namespace AndroidUI.HolyLight
{
    partial class AndroidTextField
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
            if ( disposing && ( components != null ) )
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.FromArgb(( ( int ) ( ( ( byte ) ( 249 ) ) ) ), ( ( int ) ( ( ( byte ) ( 249 ) ) ) ), ( ( int ) ( ( ( byte ) ( 249 ) ) ) ));
            this.textBox1.ForeColor = System.Drawing.Color.FromArgb(( ( int ) ( ( ( byte ) ( 51 ) ) ) ), ( ( int ) ( ( ( byte ) ( 51 ) ) ) ), ( ( int ) ( ( ( byte ) ( 51 ) ) ) ));
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox1.Font = new System.Drawing.Font("Roboto", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 238 ) ));
            this.textBox1.Location = new System.Drawing.Point(8, 2);
            this.textBox1.Margin = new System.Windows.Forms.Padding(0);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(168, 14);
            this.textBox1.TabIndex = 0;
            // 
            // AndroidTextField
            // 
            this.Controls.Add(this.textBox1);
            this.Name = "AndroidTextField";
            this.Padding = new System.Windows.Forms.Padding(8, 2, 8, 5);
            this.Size = new System.Drawing.Size(184, 20);
            this.ForeColor = System.Drawing.Color.FromArgb(( ( int ) ( ( ( byte ) ( 51 ) ) ) ), ( ( int ) ( ( ( byte ) ( 51 ) ) ) ), ( ( int ) ( ( ( byte ) ( 51 ) ) ) ));
            this.Font = new System.Drawing.Font("Roboto", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 238 ) ));
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
    }
}
