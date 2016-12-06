namespace EveryonesHellEditor
{
    partial class DialogEditor
    {
        /// <summary> 
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Komponenten-Designer generierter Code

        /// <summary> 
        /// Erforderliche Methode für die Designerunterstützung. 
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tbText = new System.Windows.Forms.TextBox();
            this.btn_add = new System.Windows.Forms.Button();
            this.lbAnswers = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbAnswerId = new System.Windows.Forms.ComboBox();
            this.cbNextId = new System.Windows.Forms.ComboBox();
            this.btn_show = new System.Windows.Forms.Button();
            this.btn_remove = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.cbId = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lblPreviewNextId = new System.Windows.Forms.Label();
            this.lblPreviewText = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(22, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "DialogId:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "DialogText:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(22, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "NextDialogId:";
            // 
            // tbText
            // 
            this.tbText.Location = new System.Drawing.Point(118, 92);
            this.tbText.Name = "tbText";
            this.tbText.Size = new System.Drawing.Size(251, 20);
            this.tbText.TabIndex = 4;
            // 
            // btn_add
            // 
            this.btn_add.Location = new System.Drawing.Point(206, 119);
            this.btn_add.Name = "btn_add";
            this.btn_add.Size = new System.Drawing.Size(75, 23);
            this.btn_add.TabIndex = 6;
            this.btn_add.Text = "Add";
            this.btn_add.UseVisualStyleBackColor = true;
            this.btn_add.Click += new System.EventHandler(this.btn_add_Click);
            // 
            // lbAnswers
            // 
            this.lbAnswers.FormattingEnabled = true;
            this.lbAnswers.Location = new System.Drawing.Point(25, 177);
            this.lbAnswers.Name = "lbAnswers";
            this.lbAnswers.Size = new System.Drawing.Size(294, 186);
            this.lbAnswers.TabIndex = 7;
            this.lbAnswers.SelectedIndexChanged += new System.EventHandler(this.lbAnswers_SelectedIndexChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(22, 124);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(90, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Answer Dialog Id:";
            // 
            // cbAnswerId
            // 
            this.cbAnswerId.FormattingEnabled = true;
            this.cbAnswerId.Location = new System.Drawing.Point(118, 121);
            this.cbAnswerId.Name = "cbAnswerId";
            this.cbAnswerId.Size = new System.Drawing.Size(82, 21);
            this.cbAnswerId.TabIndex = 9;
            // 
            // cbNextId
            // 
            this.cbNextId.FormattingEnabled = true;
            this.cbNextId.Location = new System.Drawing.Point(118, 60);
            this.cbNextId.Name = "cbNextId";
            this.cbNextId.Size = new System.Drawing.Size(82, 21);
            this.cbNextId.TabIndex = 10;
            // 
            // btn_show
            // 
            this.btn_show.Location = new System.Drawing.Point(206, 58);
            this.btn_show.Name = "btn_show";
            this.btn_show.Size = new System.Drawing.Size(75, 23);
            this.btn_show.TabIndex = 11;
            this.btn_show.Text = "Show";
            this.btn_show.UseVisualStyleBackColor = true;
            this.btn_show.Click += new System.EventHandler(this.btn_show_Click);
            // 
            // btn_remove
            // 
            this.btn_remove.Location = new System.Drawing.Point(325, 177);
            this.btn_remove.Name = "btn_remove";
            this.btn_remove.Size = new System.Drawing.Size(75, 23);
            this.btn_remove.TabIndex = 12;
            this.btn_remove.Text = "remove";
            this.btn_remove.UseVisualStyleBackColor = true;
            this.btn_remove.Click += new System.EventHandler(this.btn_remove_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(24, 151);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(47, 13);
            this.label5.TabIndex = 13;
            this.label5.Text = "Answers";
            // 
            // cbId
            // 
            this.cbId.FormattingEnabled = true;
            this.cbId.Location = new System.Drawing.Point(119, 29);
            this.cbId.Name = "cbId";
            this.cbId.Size = new System.Drawing.Size(81, 21);
            this.cbId.TabIndex = 14;
            this.cbId.SelectedIndexChanged += new System.EventHandler(this.cbId_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lblPreviewNextId);
            this.groupBox1.Controls.Add(this.lblPreviewText);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(25, 369);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(373, 100);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Preview";
            // 
            // lblPreviewNextId
            // 
            this.lblPreviewNextId.AutoSize = true;
            this.lblPreviewNextId.Location = new System.Drawing.Point(88, 65);
            this.lblPreviewNextId.Name = "lblPreviewNextId";
            this.lblPreviewNextId.Size = new System.Drawing.Size(0, 13);
            this.lblPreviewNextId.TabIndex = 3;
            // 
            // lblPreviewText
            // 
            this.lblPreviewText.AutoSize = true;
            this.lblPreviewText.Location = new System.Drawing.Point(88, 32);
            this.lblPreviewText.Name = "lblPreviewText";
            this.lblPreviewText.Size = new System.Drawing.Size(0, 13);
            this.lblPreviewText.TabIndex = 2;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 65);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(71, 13);
            this.label7.TabIndex = 1;
            this.label7.Text = "NextDialogId:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(9, 32);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(61, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "DialogText:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(136, 475);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(89, 23);
            this.btnSave.TabIndex = 16;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(312, 475);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(89, 23);
            this.btnClear.TabIndex = 17;
            this.btnClear.Text = "Delete";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(231, 475);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(75, 23);
            this.btnNew.TabIndex = 18;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // DialogEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cbId);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_remove);
            this.Controls.Add(this.btn_show);
            this.Controls.Add(this.cbNextId);
            this.Controls.Add(this.cbAnswerId);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lbAnswers);
            this.Controls.Add(this.btn_add);
            this.Controls.Add(this.tbText);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "DialogEditor";
            this.Size = new System.Drawing.Size(421, 515);
            this.Load += new System.EventHandler(this.DialogEditor_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbText;
        private System.Windows.Forms.Button btn_add;
        private System.Windows.Forms.ListBox lbAnswers;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbAnswerId;
        private System.Windows.Forms.ComboBox cbNextId;
        private System.Windows.Forms.Button btn_show;
        private System.Windows.Forms.Button btn_remove;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbId;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lblPreviewNextId;
        private System.Windows.Forms.Label lblPreviewText;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnNew;
    }
}
