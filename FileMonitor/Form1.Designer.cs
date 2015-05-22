namespace FileMonitor
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
            this.components = new System.ComponentModel.Container();
            this.textBoxPath = new System.Windows.Forms.TextBox();
            this.labelPath = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.button = new System.Windows.Forms.Button();
            this.labelActivity = new System.Windows.Forms.Label();
            this.listViewActivity = new System.Windows.Forms.ListView();
            this.columnEvents = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SuspendLayout();
            // 
            // textBoxPath
            // 
            this.textBoxPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxPath.Location = new System.Drawing.Point(101, 6);
            this.textBoxPath.Name = "textBoxPath";
            this.textBoxPath.Size = new System.Drawing.Size(339, 20);
            this.textBoxPath.TabIndex = 0;
            this.textBoxPath.DoubleClick += new System.EventHandler(this.textBoxPath_DoubleClick);
            // 
            // labelPath
            // 
            this.labelPath.AutoSize = true;
            this.labelPath.Location = new System.Drawing.Point(12, 9);
            this.labelPath.Name = "labelPath";
            this.labelPath.Size = new System.Drawing.Size(83, 13);
            this.labelPath.TabIndex = 1;
            this.labelPath.Text = "Path To Monitor";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "File Monitor";
            this.notifyIcon1.Visible = true;
            // 
            // button
            // 
            this.button.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button.Location = new System.Drawing.Point(446, 4);
            this.button.Name = "button";
            this.button.Size = new System.Drawing.Size(75, 23);
            this.button.TabIndex = 2;
            this.button.Text = "Monitor";
            this.button.UseVisualStyleBackColor = true;
            this.button.Click += new System.EventHandler(this.button_Click);
            // 
            // labelActivity
            // 
            this.labelActivity.AutoSize = true;
            this.labelActivity.Location = new System.Drawing.Point(12, 47);
            this.labelActivity.Name = "labelActivity";
            this.labelActivity.Size = new System.Drawing.Size(41, 13);
            this.labelActivity.TabIndex = 3;
            this.labelActivity.Text = "Activity";
            // 
            // listViewActivity
            // 
            this.listViewActivity.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.listViewActivity.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnEvents,
            this.columnPath});
            this.listViewActivity.Location = new System.Drawing.Point(12, 63);
            this.listViewActivity.Name = "listViewActivity";
            this.listViewActivity.Size = new System.Drawing.Size(509, 231);
            this.listViewActivity.TabIndex = 4;
            this.listViewActivity.UseCompatibleStateImageBehavior = false;
            this.listViewActivity.View = System.Windows.Forms.View.Details;
            // 
            // columnEvents
            // 
            this.columnEvents.Text = "Events";
            // 
            // columnPath
            // 
            this.columnPath.Text = "Path";
            this.columnPath.Width = 430;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(530, 306);
            this.Controls.Add(this.listViewActivity);
            this.Controls.Add(this.labelActivity);
            this.Controls.Add(this.button);
            this.Controls.Add(this.labelPath);
            this.Controls.Add(this.textBoxPath);
            this.Name = "Form1";
            this.Text = "File Monitor";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBoxPath;
        private System.Windows.Forms.Label labelPath;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.Button button;
        private System.Windows.Forms.Label labelActivity;
        private System.Windows.Forms.ListView listViewActivity;
        private System.Windows.Forms.ColumnHeader columnEvents;
        private System.Windows.Forms.ColumnHeader columnPath;
    }
}

