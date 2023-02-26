namespace ObjectChecker
{
    partial class ObjectChecker
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ObjectChecker));
            this.RunBtn = new System.Windows.Forms.Button();
            this.UploadBtn = new System.Windows.Forms.Button();
            this.TrainingGroupBox = new System.Windows.Forms.GroupBox();
            this.SamplesLocationTxtBoxB = new System.Windows.Forms.TextBox();
            this.IterationCountLbl = new System.Windows.Forms.Label();
            this.samplesLbl = new System.Windows.Forms.Label();
            this.SamplesLocationTxtBoxA = new System.Windows.Forms.TextBox();
            this.iterationCountTxtBox = new System.Windows.Forms.TextBox();
            this.TrainBtn = new System.Windows.Forms.Button();
            this.ObjectCheckerGroupBox = new System.Windows.Forms.GroupBox();
            this.imageLbl = new System.Windows.Forms.Label();
            this.picBox = new System.Windows.Forms.PictureBox();
            this.SettingsGroupBox = new System.Windows.Forms.GroupBox();
            this.outputLogChkBox = new System.Windows.Forms.CheckBox();
            this.displayAccuracyChkBox = new System.Windows.Forms.CheckBox();
            this.outputGroupBox = new System.Windows.Forms.GroupBox();
            this.resultLbl = new System.Windows.Forms.Label();
            this.accuracyLbl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.TrainingGroupBox.SuspendLayout();
            this.ObjectCheckerGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            this.SettingsGroupBox.SuspendLayout();
            this.outputGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // RunBtn
            // 
            this.RunBtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.RunBtn.Location = new System.Drawing.Point(566, 354);
            this.RunBtn.Name = "RunBtn";
            this.RunBtn.Size = new System.Drawing.Size(111, 40);
            this.RunBtn.TabIndex = 21;
            this.RunBtn.Text = "Run";
            this.RunBtn.UseVisualStyleBackColor = true;
            this.RunBtn.Click += new System.EventHandler(this.RunBtn_Click);
            // 
            // UploadBtn
            // 
            this.UploadBtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.UploadBtn.Location = new System.Drawing.Point(440, 354);
            this.UploadBtn.Name = "UploadBtn";
            this.UploadBtn.Size = new System.Drawing.Size(111, 40);
            this.UploadBtn.TabIndex = 20;
            this.UploadBtn.Text = "Upload Image";
            this.UploadBtn.UseVisualStyleBackColor = true;
            this.UploadBtn.Click += new System.EventHandler(this.UploadBtn_Click);
            // 
            // TrainingGroupBox
            // 
            this.TrainingGroupBox.Controls.Add(this.SamplesLocationTxtBoxB);
            this.TrainingGroupBox.Controls.Add(this.IterationCountLbl);
            this.TrainingGroupBox.Controls.Add(this.samplesLbl);
            this.TrainingGroupBox.Controls.Add(this.SamplesLocationTxtBoxA);
            this.TrainingGroupBox.Controls.Add(this.iterationCountTxtBox);
            this.TrainingGroupBox.Controls.Add(this.TrainBtn);
            this.TrainingGroupBox.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TrainingGroupBox.Location = new System.Drawing.Point(3, 26);
            this.TrainingGroupBox.Name = "TrainingGroupBox";
            this.TrainingGroupBox.Size = new System.Drawing.Size(232, 170);
            this.TrainingGroupBox.TabIndex = 19;
            this.TrainingGroupBox.TabStop = false;
            this.TrainingGroupBox.Text = "Training";
            // 
            // SamplesLocationTxtBoxB
            // 
            this.SamplesLocationTxtBoxB.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SamplesLocationTxtBoxB.Location = new System.Drawing.Point(8, 81);
            this.SamplesLocationTxtBoxB.Name = "SamplesLocationTxtBoxB";
            this.SamplesLocationTxtBoxB.Size = new System.Drawing.Size(212, 28);
            this.SamplesLocationTxtBoxB.TabIndex = 28;
            this.SamplesLocationTxtBoxB.Visible = false;
            this.SamplesLocationTxtBoxB.Click += new System.EventHandler(this.SamplesLocationTxtBoxB_Click);
            // 
            // IterationCountLbl
            // 
            this.IterationCountLbl.AutoSize = true;
            this.IterationCountLbl.Font = new System.Drawing.Font("Segoe UI", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.IterationCountLbl.Location = new System.Drawing.Point(8, 113);
            this.IterationCountLbl.Name = "IterationCountLbl";
            this.IterationCountLbl.Size = new System.Drawing.Size(103, 19);
            this.IterationCountLbl.TabIndex = 27;
            this.IterationCountLbl.Text = "Iteration Count";
            // 
            // samplesLbl
            // 
            this.samplesLbl.AutoSize = true;
            this.samplesLbl.Font = new System.Drawing.Font("Segoe UI", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.samplesLbl.Location = new System.Drawing.Point(8, 25);
            this.samplesLbl.Name = "samplesLbl";
            this.samplesLbl.Size = new System.Drawing.Size(59, 19);
            this.samplesLbl.TabIndex = 10;
            this.samplesLbl.Text = "Samples";
            // 
            // SamplesLocationTxtBoxA
            // 
            this.SamplesLocationTxtBoxA.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SamplesLocationTxtBoxA.Location = new System.Drawing.Point(8, 47);
            this.SamplesLocationTxtBoxA.Name = "SamplesLocationTxtBoxA";
            this.SamplesLocationTxtBoxA.Size = new System.Drawing.Size(212, 28);
            this.SamplesLocationTxtBoxA.TabIndex = 8;
            this.SamplesLocationTxtBoxA.Click += new System.EventHandler(this.SamplesLocationTxtBoxA_Click);
            // 
            // iterationCountTxtBox
            // 
            this.iterationCountTxtBox.Font = new System.Drawing.Font("Segoe UI", 11.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.iterationCountTxtBox.Location = new System.Drawing.Point(8, 133);
            this.iterationCountTxtBox.Name = "iterationCountTxtBox";
            this.iterationCountTxtBox.Size = new System.Drawing.Size(103, 28);
            this.iterationCountTxtBox.TabIndex = 26;
            // 
            // TrainBtn
            // 
            this.TrainBtn.Enabled = false;
            this.TrainBtn.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.TrainBtn.Location = new System.Drawing.Point(129, 133);
            this.TrainBtn.Name = "TrainBtn";
            this.TrainBtn.Size = new System.Drawing.Size(91, 28);
            this.TrainBtn.TabIndex = 6;
            this.TrainBtn.Text = "Train";
            this.TrainBtn.UseVisualStyleBackColor = true;
            this.TrainBtn.Click += new System.EventHandler(this.TrainBtn_Click);
            // 
            // ObjectCheckerGroupBox
            // 
            this.ObjectCheckerGroupBox.Controls.Add(this.imageLbl);
            this.ObjectCheckerGroupBox.Controls.Add(this.picBox);
            this.ObjectCheckerGroupBox.Font = new System.Drawing.Font("Segoe UI", 12.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ObjectCheckerGroupBox.Location = new System.Drawing.Point(9, 8);
            this.ObjectCheckerGroupBox.Name = "ObjectCheckerGroupBox";
            this.ObjectCheckerGroupBox.Size = new System.Drawing.Size(424, 386);
            this.ObjectCheckerGroupBox.TabIndex = 18;
            this.ObjectCheckerGroupBox.TabStop = false;
            this.ObjectCheckerGroupBox.Text = "Title";
            // 
            // imageLbl
            // 
            this.imageLbl.AutoSize = true;
            this.imageLbl.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.imageLbl.Location = new System.Drawing.Point(167, 360);
            this.imageLbl.Name = "imageLbl";
            this.imageLbl.Size = new System.Drawing.Size(51, 20);
            this.imageLbl.TabIndex = 32;
            this.imageLbl.Text = "Image";
            this.imageLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.imageLbl.Visible = false;
            // 
            // picBox
            // 
            this.picBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.picBox.Location = new System.Drawing.Point(6, 33);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(412, 323);
            this.picBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBox.TabIndex = 3;
            this.picBox.TabStop = false;
            this.picBox.DragDrop += new System.Windows.Forms.DragEventHandler(this.picBox_DragDrop);
            this.picBox.DragEnter += new System.Windows.Forms.DragEventHandler(this.picBox_DragEnter);
            // 
            // SettingsGroupBox
            // 
            this.SettingsGroupBox.Controls.Add(this.outputLogChkBox);
            this.SettingsGroupBox.Controls.Add(this.displayAccuracyChkBox);
            this.SettingsGroupBox.Controls.Add(this.TrainingGroupBox);
            this.SettingsGroupBox.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SettingsGroupBox.Location = new System.Drawing.Point(439, 8);
            this.SettingsGroupBox.Name = "SettingsGroupBox";
            this.SettingsGroupBox.Size = new System.Drawing.Size(238, 265);
            this.SettingsGroupBox.TabIndex = 29;
            this.SettingsGroupBox.TabStop = false;
            this.SettingsGroupBox.Text = "Settings";
            // 
            // outputLogChkBox
            // 
            this.outputLogChkBox.AutoSize = true;
            this.outputLogChkBox.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.outputLogChkBox.Location = new System.Drawing.Point(9, 235);
            this.outputLogChkBox.Name = "outputLogChkBox";
            this.outputLogChkBox.Size = new System.Drawing.Size(100, 24);
            this.outputLogChkBox.TabIndex = 21;
            this.outputLogChkBox.Text = "Output log";
            this.outputLogChkBox.UseVisualStyleBackColor = true;
            // 
            // displayAccuracyChkBox
            // 
            this.displayAccuracyChkBox.AutoSize = true;
            this.displayAccuracyChkBox.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.displayAccuracyChkBox.Location = new System.Drawing.Point(9, 209);
            this.displayAccuracyChkBox.Name = "displayAccuracyChkBox";
            this.displayAccuracyChkBox.Size = new System.Drawing.Size(178, 24);
            this.displayAccuracyChkBox.TabIndex = 20;
            this.displayAccuracyChkBox.Text = "Display result accuracy";
            this.displayAccuracyChkBox.UseVisualStyleBackColor = true;
            // 
            // outputGroupBox
            // 
            this.outputGroupBox.Controls.Add(this.resultLbl);
            this.outputGroupBox.Controls.Add(this.accuracyLbl);
            this.outputGroupBox.Controls.Add(this.label1);
            this.outputGroupBox.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.outputGroupBox.Location = new System.Drawing.Point(439, 273);
            this.outputGroupBox.Name = "outputGroupBox";
            this.outputGroupBox.Size = new System.Drawing.Size(238, 75);
            this.outputGroupBox.TabIndex = 30;
            this.outputGroupBox.TabStop = false;
            this.outputGroupBox.Text = "Output";
            // 
            // resultLbl
            // 
            this.resultLbl.AutoSize = true;
            this.resultLbl.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.resultLbl.Location = new System.Drawing.Point(17, 24);
            this.resultLbl.Name = "resultLbl";
            this.resultLbl.Size = new System.Drawing.Size(19, 20);
            this.resultLbl.TabIndex = 31;
            this.resultLbl.Text = "A";
            this.resultLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.resultLbl.Visible = false;
            // 
            // accuracyLbl
            // 
            this.accuracyLbl.AutoSize = true;
            this.accuracyLbl.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.accuracyLbl.Location = new System.Drawing.Point(17, 48);
            this.accuracyLbl.Name = "accuracyLbl";
            this.accuracyLbl.Size = new System.Drawing.Size(18, 20);
            this.accuracyLbl.TabIndex = 30;
            this.accuracyLbl.Text = "B";
            this.accuracyLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.accuracyLbl.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 20);
            this.label1.TabIndex = 0;
            // 
            // ObjectChecker
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(686, 406);
            this.Controls.Add(this.outputGroupBox);
            this.Controls.Add(this.ObjectCheckerGroupBox);
            this.Controls.Add(this.SettingsGroupBox);
            this.Controls.Add(this.RunBtn);
            this.Controls.Add(this.UploadBtn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ObjectChecker";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Object Detection";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ObjectChecker_FormClosed);
            this.Load += new System.EventHandler(this.ObjectChecker_Load);
            this.TrainingGroupBox.ResumeLayout(false);
            this.TrainingGroupBox.PerformLayout();
            this.ObjectCheckerGroupBox.ResumeLayout(false);
            this.ObjectCheckerGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            this.SettingsGroupBox.ResumeLayout(false);
            this.SettingsGroupBox.PerformLayout();
            this.outputGroupBox.ResumeLayout(false);
            this.outputGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Button RunBtn;
        private Button UploadBtn;
        private GroupBox TrainingGroupBox;
        private Label samplesLbl;
        private TextBox SamplesLocationTxtBoxA;
        private Button TrainBtn;
        private GroupBox ObjectCheckerGroupBox;
        private PictureBox picBox;
        private Label IterationCountLbl;
        private TextBox iterationCountTxtBox;
        private GroupBox SettingsGroupBox;
        private GroupBox outputGroupBox;
        private Label resultLbl;
        private Label accuracyLbl;
        private Label label1;
        private CheckBox displayAccuracyChkBox;
        private TextBox SamplesLocationTxtBoxB;
        private CheckBox outputLogChkBox;
        private Label imageLbl;
    }
}