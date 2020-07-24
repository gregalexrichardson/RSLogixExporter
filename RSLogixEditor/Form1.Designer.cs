namespace RSLogixEditor
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbLookIn = new System.Windows.Forms.ComboBox();
            this.cmbFindFrom = new System.Windows.Forms.ComboBox();
            this.txtFindValue = new System.Windows.Forms.TextBox();
            this.txtReplaceValue = new System.Windows.Forms.TextBox();
            this.cmbReplaceFrom = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.cmbFunction = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnRunFunction = new System.Windows.Forms.Button();
            this.txtConditions = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btnOpenFile = new System.Windows.Forms.Button();
            this.lblFilePath = new System.Windows.Forms.Label();
            this.btnSaveXML = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 90);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Look In";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Find From";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 175);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(75, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Find Value";
            // 
            // cmbLookIn
            // 
            this.cmbLookIn.FormattingEnabled = true;
            this.cmbLookIn.Location = new System.Drawing.Point(147, 86);
            this.cmbLookIn.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbLookIn.Name = "cmbLookIn";
            this.cmbLookIn.Size = new System.Drawing.Size(160, 24);
            this.cmbLookIn.TabIndex = 3;
            this.cmbLookIn.Text = "Rung";
            // 
            // cmbFindFrom
            // 
            this.cmbFindFrom.FormattingEnabled = true;
            this.cmbFindFrom.Location = new System.Drawing.Point(147, 138);
            this.cmbFindFrom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbFindFrom.Name = "cmbFindFrom";
            this.cmbFindFrom.Size = new System.Drawing.Size(160, 24);
            this.cmbFindFrom.TabIndex = 4;
            this.cmbFindFrom.Text = "Text";
            // 
            // txtFindValue
            // 
            this.txtFindValue.Location = new System.Drawing.Point(147, 171);
            this.txtFindValue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtFindValue.Name = "txtFindValue";
            this.txtFindValue.Size = new System.Drawing.Size(160, 22);
            this.txtFindValue.TabIndex = 5;
            this.txtFindValue.Text = "Test_MSG_Trigger[###]";
            // 
            // txtReplaceValue
            // 
            this.txtReplaceValue.Location = new System.Drawing.Point(147, 249);
            this.txtReplaceValue.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtReplaceValue.Name = "txtReplaceValue";
            this.txtReplaceValue.Size = new System.Drawing.Size(160, 22);
            this.txtReplaceValue.TabIndex = 9;
            this.txtReplaceValue.Text = "Message ### ";
            // 
            // cmbReplaceFrom
            // 
            this.cmbReplaceFrom.FormattingEnabled = true;
            this.cmbReplaceFrom.Location = new System.Drawing.Point(147, 215);
            this.cmbReplaceFrom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbReplaceFrom.Name = "cmbReplaceFrom";
            this.cmbReplaceFrom.Size = new System.Drawing.Size(160, 24);
            this.cmbReplaceFrom.TabIndex = 8;
            this.cmbReplaceFrom.Text = "Comment";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(35, 252);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Replace Value";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(35, 219);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "Replace From";
            // 
            // cmbFunction
            // 
            this.cmbFunction.FormattingEnabled = true;
            this.cmbFunction.Location = new System.Drawing.Point(147, 298);
            this.cmbFunction.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cmbFunction.Name = "cmbFunction";
            this.cmbFunction.Size = new System.Drawing.Size(160, 24);
            this.cmbFunction.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(35, 302);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(62, 17);
            this.label6.TabIndex = 10;
            this.label6.Text = "Function";
            // 
            // btnRunFunction
            // 
            this.btnRunFunction.Location = new System.Drawing.Point(39, 394);
            this.btnRunFunction.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnRunFunction.Name = "btnRunFunction";
            this.btnRunFunction.Size = new System.Drawing.Size(269, 28);
            this.btnRunFunction.TabIndex = 12;
            this.btnRunFunction.Text = "Run Function";
            this.btnRunFunction.UseVisualStyleBackColor = true;
            this.btnRunFunction.Click += new System.EventHandler(this.btnRunFunction_Click);
            // 
            // txtConditions
            // 
            this.txtConditions.Location = new System.Drawing.Point(147, 331);
            this.txtConditions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtConditions.Name = "txtConditions";
            this.txtConditions.Size = new System.Drawing.Size(160, 22);
            this.txtConditions.TabIndex = 14;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(35, 335);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(74, 17);
            this.label7.TabIndex = 13;
            this.label7.Text = "Conditions";
            // 
            // btnOpenFile
            // 
            this.btnOpenFile.Location = new System.Drawing.Point(39, 15);
            this.btnOpenFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOpenFile.Name = "btnOpenFile";
            this.btnOpenFile.Size = new System.Drawing.Size(269, 28);
            this.btnOpenFile.TabIndex = 15;
            this.btnOpenFile.Text = "Open File";
            this.btnOpenFile.UseVisualStyleBackColor = true;
            this.btnOpenFile.Click += new System.EventHandler(this.btnOpenFile_Click);
            // 
            // lblFilePath
            // 
            this.lblFilePath.AutoSize = true;
            this.lblFilePath.Location = new System.Drawing.Point(35, 47);
            this.lblFilePath.Name = "lblFilePath";
            this.lblFilePath.Size = new System.Drawing.Size(54, 17);
            this.lblFilePath.TabIndex = 16;
            this.lblFilePath.Text = "Look In";
            // 
            // btnSaveXML
            // 
            this.btnSaveXML.Location = new System.Drawing.Point(39, 430);
            this.btnSaveXML.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnSaveXML.Name = "btnSaveXML";
            this.btnSaveXML.Size = new System.Drawing.Size(269, 28);
            this.btnSaveXML.TabIndex = 17;
            this.btnSaveXML.Text = "Save XML";
            this.btnSaveXML.UseVisualStyleBackColor = true;
            this.btnSaveXML.Click += new System.EventHandler(this.btnSaveXML_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 474);
            this.Controls.Add(this.btnSaveXML);
            this.Controls.Add(this.lblFilePath);
            this.Controls.Add(this.btnOpenFile);
            this.Controls.Add(this.txtConditions);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnRunFunction);
            this.Controls.Add(this.cmbFunction);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtReplaceValue);
            this.Controls.Add(this.cmbReplaceFrom);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtFindValue);
            this.Controls.Add(this.cmbFindFrom);
            this.Controls.Add(this.cmbLookIn);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cmbLookIn;
        private System.Windows.Forms.ComboBox cmbFindFrom;
        private System.Windows.Forms.TextBox txtFindValue;
        private System.Windows.Forms.TextBox txtReplaceValue;
        private System.Windows.Forms.ComboBox cmbReplaceFrom;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cmbFunction;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnRunFunction;
        private System.Windows.Forms.TextBox txtConditions;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnOpenFile;
        private System.Windows.Forms.Label lblFilePath;
        private System.Windows.Forms.Button btnSaveXML;
    }
}

