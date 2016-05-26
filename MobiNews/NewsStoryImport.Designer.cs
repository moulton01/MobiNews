namespace MobiNews
{
    partial class NewsStoryImport
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
            this.GetNewsStories = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // GetNewsStories
            // 
            this.GetNewsStories.Location = new System.Drawing.Point(266, 90);
            this.GetNewsStories.Name = "GetNewsStories";
            this.GetNewsStories.Size = new System.Drawing.Size(449, 46);
            this.GetNewsStories.TabIndex = 0;
            this.GetNewsStories.Text = "Run Import";
            this.GetNewsStories.UseVisualStyleBackColor = true;
            this.GetNewsStories.Click += new System.EventHandler(this.GetNewsStories_Click);
            // 
            // NewsStoryImport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1115, 612);
            this.Controls.Add(this.GetNewsStories);
            this.Name = "NewsStoryImport";
            this.Text = "News Story Import";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button GetNewsStories;
    }
}

