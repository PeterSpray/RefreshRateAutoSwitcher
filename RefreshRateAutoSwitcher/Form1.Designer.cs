namespace RefreshRateAutoSwitcher
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.SaveSettings = new System.Windows.Forms.Button();
            this.AddTask = new System.Windows.Forms.Button();
            this.freq_plugged = new System.Windows.Forms.ComboBox();
            this.freq_batt = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(18, 14);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(153, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "Monitor :";
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(18, 64);
            this.comboBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(1182, 46);
            this.comboBox1.TabIndex = 1;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // ApplyButton
            // 
            this.ApplyButton.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.ApplyButton.Location = new System.Drawing.Point(828, 168);
            this.ApplyButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(264, 69);
            this.ApplyButton.TabIndex = 2;
            this.ApplyButton.Text = "Test run";
            this.ApplyButton.UseVisualStyleBackColor = true;
            this.ApplyButton.Click += new System.EventHandler(this.ApplyButton_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(20, 172);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(364, 45);
            this.label2.TabIndex = 4;
            this.label2.Text = "Refresh rate plugged in:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(20, 348);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(124, 45);
            this.label3.TabIndex = 5;
            this.label3.Text = "Result: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(159, 348);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(124, 45);
            this.label4.TabIndex = 6;
            this.label4.Text = "Label 4";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(18, 255);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(357, 45);
            this.label5.TabIndex = 7;
            this.label5.Text = "Refresh rate on battery:";
            // 
            // SaveSettings
            // 
            this.SaveSettings.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.SaveSettings.Location = new System.Drawing.Point(828, 255);
            this.SaveSettings.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.SaveSettings.Name = "SaveSettings";
            this.SaveSettings.Size = new System.Drawing.Size(264, 72);
            this.SaveSettings.TabIndex = 9;
            this.SaveSettings.Text = "Save Settings";
            this.SaveSettings.UseVisualStyleBackColor = true;
            this.SaveSettings.Click += new System.EventHandler(this.SaveSettings_Click);
            // 
            // AddTask
            // 
            this.AddTask.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AddTask.Location = new System.Drawing.Point(828, 336);
            this.AddTask.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.AddTask.Name = "AddTask";
            this.AddTask.Size = new System.Drawing.Size(264, 130);
            this.AddTask.TabIndex = 10;
            this.AddTask.Text = "Add/remove task";
            this.AddTask.UseVisualStyleBackColor = true;
            this.AddTask.Click += new System.EventHandler(this.AddTask_Click);
            // 
            // freq_plugged
            // 
            this.freq_plugged.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.freq_plugged.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.freq_plugged.FormattingEnabled = true;
            this.freq_plugged.Location = new System.Drawing.Point(500, 174);
            this.freq_plugged.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.freq_plugged.Name = "freq_plugged";
            this.freq_plugged.Size = new System.Drawing.Size(186, 53);
            this.freq_plugged.TabIndex = 11;
            // 
            // freq_batt
            // 
            this.freq_batt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.freq_batt.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.freq_batt.FormattingEnabled = true;
            this.freq_batt.Location = new System.Drawing.Point(500, 264);
            this.freq_batt.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.freq_batt.Name = "freq_batt";
            this.freq_batt.Size = new System.Drawing.Size(186, 53);
            this.freq_batt.TabIndex = 12;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 30F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1220, 484);
            this.Controls.Add(this.freq_batt);
            this.Controls.Add(this.freq_plugged);
            this.Controls.Add(this.AddTask);
            this.Controls.Add(this.SaveSettings);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.ApplyButton);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "Form1";
            this.Text = "RefreshRateAutoSwitcher";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Label label1;
        private ComboBox comboBox1;
        private Button ApplyButton;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Button SaveSettings;
        private Button AddTask;
        private ComboBox freq_plugged;
        private ComboBox freq_batt;
    }
}