namespace KeeDiceware.Forms
{
    partial class SettingsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsForm));
            this.ComboBoxWordlist = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.ButtonOK = new System.Windows.Forms.Button();
            this.ButtonCancel = new System.Windows.Forms.Button();
            this.ComboBoxGenerator = new System.Windows.Forms.ComboBox();
            this.NumericUpDownCount = new System.Windows.Forms.NumericUpDown();
            this.LabelCount = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownCount)).BeginInit();
            this.SuspendLayout();
            // 
            // ComboBoxWordlist
            // 
            this.ComboBoxWordlist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxWordlist.FormattingEnabled = true;
            this.ComboBoxWordlist.Location = new System.Drawing.Point(12, 25);
            this.ComboBoxWordlist.Name = "ComboBoxWordlist";
            this.ComboBoxWordlist.Size = new System.Drawing.Size(200, 21);
            this.ComboBoxWordlist.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Wordlist:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 49);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(57, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Generator:";
            // 
            // ButtonOK
            // 
            this.ButtonOK.Location = new System.Drawing.Point(56, 131);
            this.ButtonOK.Name = "ButtonOK";
            this.ButtonOK.Size = new System.Drawing.Size(75, 23);
            this.ButtonOK.TabIndex = 3;
            this.ButtonOK.Text = "&OK";
            this.ButtonOK.UseVisualStyleBackColor = true;
            this.ButtonOK.Click += new System.EventHandler(this.ButtonOK_Click);
            // 
            // ButtonCancel
            // 
            this.ButtonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.ButtonCancel.Location = new System.Drawing.Point(137, 131);
            this.ButtonCancel.Name = "ButtonCancel";
            this.ButtonCancel.Size = new System.Drawing.Size(75, 23);
            this.ButtonCancel.TabIndex = 4;
            this.ButtonCancel.Text = "&Cancel";
            this.ButtonCancel.UseVisualStyleBackColor = true;
            this.ButtonCancel.Click += new System.EventHandler(this.ButtonCancel_Click);
            // 
            // ComboBoxGenerator
            // 
            this.ComboBoxGenerator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ComboBoxGenerator.FormattingEnabled = true;
            this.ComboBoxGenerator.Location = new System.Drawing.Point(12, 65);
            this.ComboBoxGenerator.Name = "ComboBoxGenerator";
            this.ComboBoxGenerator.Size = new System.Drawing.Size(200, 21);
            this.ComboBoxGenerator.TabIndex = 1;
            this.ComboBoxGenerator.SelectedIndexChanged += new System.EventHandler(this.ComboBoxGenerator_SelectedIndexChanged);
            // 
            // NumericUpDownCount
            // 
            this.NumericUpDownCount.Location = new System.Drawing.Point(12, 105);
            this.NumericUpDownCount.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.NumericUpDownCount.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumericUpDownCount.Name = "NumericUpDownCount";
            this.NumericUpDownCount.Size = new System.Drawing.Size(200, 20);
            this.NumericUpDownCount.TabIndex = 2;
            this.NumericUpDownCount.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.NumericUpDownCount.ValueChanged += new System.EventHandler(this.NumericUpDownCount_ValueChanged);
            // 
            // LabelCount
            // 
            this.LabelCount.AutoSize = true;
            this.LabelCount.Location = new System.Drawing.Point(12, 89);
            this.LabelCount.Name = "LabelCount";
            this.LabelCount.Size = new System.Drawing.Size(38, 13);
            this.LabelCount.TabIndex = 9;
            this.LabelCount.Text = "Count:";
            // 
            // SettingsForm
            // 
            this.AcceptButton = this.ButtonOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.ButtonCancel;
            this.ClientSize = new System.Drawing.Size(224, 166);
            this.Controls.Add(this.ButtonCancel);
            this.Controls.Add(this.ButtonOK);
            this.Controls.Add(this.LabelCount);
            this.Controls.Add(this.NumericUpDownCount);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ComboBoxGenerator);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ComboBoxWordlist);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "SettingsForm";
            this.ShowInTaskbar = false;
            this.Text = "KeeDiceware";
            ((System.ComponentModel.ISupportInitialize)(this.NumericUpDownCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox ComboBoxWordlist;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button ButtonOK;
        private System.Windows.Forms.Button ButtonCancel;
        private System.Windows.Forms.ComboBox ComboBoxGenerator;
        private System.Windows.Forms.NumericUpDown NumericUpDownCount;
        private System.Windows.Forms.Label LabelCount;
    }
}