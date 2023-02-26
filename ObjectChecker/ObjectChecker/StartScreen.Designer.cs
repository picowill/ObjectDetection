namespace ObjectChecker
{
    partial class StartScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartScreen));
            this.startBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.startScreenComboBox = new System.Windows.Forms.ComboBox();
            this.modeLbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // startBtn
            // 
            this.startBtn.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.startBtn.Location = new System.Drawing.Point(252, 171);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(109, 37);
            this.startBtn.TabIndex = 0;
            this.startBtn.Text = "Start";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(27, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(323, 54);
            this.label1.TabIndex = 1;
            this.label1.Text = "Object Detection";
            // 
            // startScreenComboBox
            // 
            this.startScreenComboBox.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.startScreenComboBox.FormattingEnabled = true;
            this.startScreenComboBox.Items.AddRange(new object[] {
            "-Select Mode-",
            "Checker",
            "Recognition",
            "Developer"});
            this.startScreenComboBox.Location = new System.Drawing.Point(27, 171);
            this.startScreenComboBox.Name = "startScreenComboBox";
            this.startScreenComboBox.Size = new System.Drawing.Size(202, 36);
            this.startScreenComboBox.TabIndex = 2;
            this.startScreenComboBox.SelectedIndexChanged += new System.EventHandler(this.startScreenComboBox_SelectedIndexChanged);
            // 
            // modeLbl
            // 
            this.modeLbl.AutoSize = true;
            this.modeLbl.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.modeLbl.Location = new System.Drawing.Point(27, 143);
            this.modeLbl.Name = "modeLbl";
            this.modeLbl.Size = new System.Drawing.Size(61, 25);
            this.modeLbl.TabIndex = 3;
            this.modeLbl.Text = "Mode";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(58, 84);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(253, 19);
            this.label2.TabIndex = 4;
            this.label2.Text = "Computer Science A Level Major Project";
            // 
            // StartScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(373, 254);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.modeLbl);
            this.Controls.Add(this.startScreenComboBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.startBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StartScreen";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Object Detection";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StartScreen_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Button startBtn;
        private Label label1;
        private ComboBox startScreenComboBox;
        private Label modeLbl;
        private Label label2;
    }
}