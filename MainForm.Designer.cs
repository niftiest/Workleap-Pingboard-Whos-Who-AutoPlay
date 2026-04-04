namespace PingboardWhosWhoAutoPlay
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            TitleLabel = new Label();
            LogTextBox = new TextBox();
            ToggleButton = new Button();
            FooterLink = new LinkLabel();
            SuspendLayout();
            //
            // TitleLabel
            //
            TitleLabel.Font = new Font("Segoe UI Semibold", 12F, FontStyle.Bold);
            TitleLabel.ForeColor = Color.White;
            TitleLabel.Location = new Point(12, 13);
            TitleLabel.Name = "TitleLabel";
            TitleLabel.Size = new Size(332, 23);
            TitleLabel.TabIndex = 0;
            TitleLabel.Text = "Pingboard Who's Who AutoPlay";
            TitleLabel.TextAlign = ContentAlignment.MiddleCenter;
            //
            // LogTextBox
            //
            LogTextBox.Location = new Point(12, 50);
            LogTextBox.Multiline = true;
            LogTextBox.Name = "LogTextBox";
            LogTextBox.ReadOnly = true;
            LogTextBox.ScrollBars = ScrollBars.Vertical;
            LogTextBox.Size = new Size(332, 373);
            LogTextBox.TabIndex = 1;
            //
            // ToggleButton
            //
            ToggleButton.BackColor = SystemColors.Highlight;
            ToggleButton.FlatAppearance.BorderSize = 0;
            ToggleButton.FlatStyle = FlatStyle.Flat;
            ToggleButton.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            ToggleButton.ForeColor = Color.White;
            ToggleButton.Location = new Point(12, 429);
            ToggleButton.Name = "ToggleButton";
            ToggleButton.Size = new Size(332, 32);
            ToggleButton.TabIndex = 2;
            ToggleButton.Text = "START PLAYING";
            ToggleButton.UseVisualStyleBackColor = false;
            ToggleButton.Click += ToggleButton_Click;
            //
            // FooterLink
            //
            FooterLink.ActiveLinkColor = Color.FromArgb(100, 180, 255);
            FooterLink.LinkColor = Color.FromArgb(224, 224, 224);
            FooterLink.VisitedLinkColor = Color.FromArgb(224, 224, 224);
            FooterLink.Location = new Point(12, 475);
            FooterLink.Name = "FooterLink";
            FooterLink.Size = new Size(332, 23);
            FooterLink.TabIndex = 3;
            FooterLink.Text = "Created by Niftiest";
            FooterLink.TextAlign = ContentAlignment.MiddleCenter;
            FooterLink.LinkClicked += FooterLink_LinkClicked;
            //
            // MainForm
            //
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(37, 36, 35);
            ClientSize = new Size(356, 509);
            Controls.Add(FooterLink);
            Controls.Add(ToggleButton);
            Controls.Add(LogTextBox);
            Controls.Add(TitleLabel);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MainForm";
            SizeGripStyle = SizeGripStyle.Hide;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Pingboard Who's Who Autoplay";
            TopMost = true;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label TitleLabel;
        private LinkLabel FooterLink;
        private TextBox LogTextBox;
        private Button ToggleButton;
    }
}
