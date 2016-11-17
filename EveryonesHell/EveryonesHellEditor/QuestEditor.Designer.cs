namespace QuestEditor
{
    partial class Form1
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

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.btSave = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lbName = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.tbID = new System.Windows.Forms.TextBox();
            this.lbID = new System.Windows.Forms.Label();
            this.lbQuestitem = new System.Windows.Forms.Label();
            this.tbQuestitem = new System.Windows.Forms.TextBox();
            this.lbDescription = new System.Windows.Forms.Label();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.lbBasedOn = new System.Windows.Forms.Label();
            this.tbBasedOn = new System.Windows.Forms.TextBox();
            this.lbRequire = new System.Windows.Forms.Label();
            this.tbRequired = new System.Windows.Forms.TextBox();
            this.lbError = new System.Windows.Forms.Label();
            this.btLoad = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(82, 513);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(163, 95);
            this.btSave.TabIndex = 7;
            this.btSave.Text = "Speichern";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(82, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 25);
            this.label1.TabIndex = 1;
            this.label1.Text = "Quests";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(82, 123);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(68, 25);
            this.lbName.TabIndex = 2;
            this.lbName.Text = "Name";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(82, 151);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(566, 31);
            this.tbName.TabIndex = 1;
            // 
            // tbID
            // 
            this.tbID.Location = new System.Drawing.Point(82, 240);
            this.tbID.Name = "tbID";
            this.tbID.Size = new System.Drawing.Size(246, 31);
            this.tbID.TabIndex = 2;
            this.tbID.TextChanged += new System.EventHandler(this.tbID_TextChanged);
            // 
            // lbID
            // 
            this.lbID.AutoSize = true;
            this.lbID.Location = new System.Drawing.Point(82, 212);
            this.lbID.Name = "lbID";
            this.lbID.Size = new System.Drawing.Size(96, 25);
            this.lbID.TabIndex = 5;
            this.lbID.Text = "Quest-ID";
            // 
            // lbQuestitem
            // 
            this.lbQuestitem.AutoSize = true;
            this.lbQuestitem.Location = new System.Drawing.Point(404, 212);
            this.lbQuestitem.Name = "lbQuestitem";
            this.lbQuestitem.Size = new System.Drawing.Size(148, 25);
            this.lbQuestitem.TabIndex = 6;
            this.lbQuestitem.Text = "Questitem - ID";
            // 
            // tbQuestitem
            // 
            this.tbQuestitem.Location = new System.Drawing.Point(402, 240);
            this.tbQuestitem.Name = "tbQuestitem";
            this.tbQuestitem.Size = new System.Drawing.Size(246, 31);
            this.tbQuestitem.TabIndex = 3;
            this.tbQuestitem.TextChanged += new System.EventHandler(this.tbID_TextChanged);
            // 
            // lbDescription
            // 
            this.lbDescription.AutoSize = true;
            this.lbDescription.Location = new System.Drawing.Point(82, 394);
            this.lbDescription.Name = "lbDescription";
            this.lbDescription.Size = new System.Drawing.Size(120, 25);
            this.lbDescription.TabIndex = 8;
            this.lbDescription.Text = "Description";
            // 
            // tbDescription
            // 
            this.tbDescription.Location = new System.Drawing.Point(82, 423);
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(566, 31);
            this.tbDescription.TabIndex = 6;
            // 
            // lbBasedOn
            // 
            this.lbBasedOn.AutoSize = true;
            this.lbBasedOn.Location = new System.Drawing.Point(82, 302);
            this.lbBasedOn.Name = "lbBasedOn";
            this.lbBasedOn.Size = new System.Drawing.Size(233, 25);
            this.lbBasedOn.TabIndex = 10;
            this.lbBasedOn.Text = "Based on Dialogue - ID";
            // 
            // tbBasedOn
            // 
            this.tbBasedOn.Location = new System.Drawing.Point(82, 330);
            this.tbBasedOn.Name = "tbBasedOn";
            this.tbBasedOn.Size = new System.Drawing.Size(246, 31);
            this.tbBasedOn.TabIndex = 4;
            this.tbBasedOn.TextChanged += new System.EventHandler(this.tbID_TextChanged);
            // 
            // lbRequire
            // 
            this.lbRequire.AutoSize = true;
            this.lbRequire.Location = new System.Drawing.Point(402, 302);
            this.lbRequire.Name = "lbRequire";
            this.lbRequire.Size = new System.Drawing.Size(145, 25);
            this.lbRequire.TabIndex = 12;
            this.lbRequire.Text = "Required item";
            // 
            // tbRequired
            // 
            this.tbRequired.Location = new System.Drawing.Point(402, 330);
            this.tbRequired.Name = "tbRequired";
            this.tbRequired.Size = new System.Drawing.Size(246, 31);
            this.tbRequired.TabIndex = 5;
            this.tbRequired.TextChanged += new System.EventHandler(this.tbID_TextChanged);
            // 
            // lbError
            // 
            this.lbError.AutoSize = true;
            this.lbError.Location = new System.Drawing.Point(277, 513);
            this.lbError.Name = "lbError";
            this.lbError.Size = new System.Drawing.Size(0, 25);
            this.lbError.TabIndex = 14;
            // 
            // btLoad
            // 
            this.btLoad.Location = new System.Drawing.Point(82, 636);
            this.btLoad.Name = "btLoad";
            this.btLoad.Size = new System.Drawing.Size(163, 95);
            this.btLoad.TabIndex = 8;
            this.btLoad.Text = "Laden";
            this.btLoad.UseVisualStyleBackColor = true;
            this.btLoad.Click += new System.EventHandler(this.btLoad_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(726, 780);
            this.Controls.Add(this.btLoad);
            this.Controls.Add(this.lbError);
            this.Controls.Add(this.tbRequired);
            this.Controls.Add(this.lbRequire);
            this.Controls.Add(this.tbBasedOn);
            this.Controls.Add(this.lbBasedOn);
            this.Controls.Add(this.tbDescription);
            this.Controls.Add(this.lbDescription);
            this.Controls.Add(this.tbQuestitem);
            this.Controls.Add(this.lbQuestitem);
            this.Controls.Add(this.lbID);
            this.Controls.Add(this.tbID);
            this.Controls.Add(this.tbName);
            this.Controls.Add(this.lbName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btSave);
            this.Name = "Form1";
            this.Text = "Form 1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btSave;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbName;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.TextBox tbID;
        private System.Windows.Forms.Label lbID;
        private System.Windows.Forms.Label lbQuestitem;
        private System.Windows.Forms.TextBox tbQuestitem;
        private System.Windows.Forms.Label lbDescription;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.Label lbBasedOn;
        private System.Windows.Forms.TextBox tbBasedOn;
        private System.Windows.Forms.Label lbRequire;
        private System.Windows.Forms.TextBox tbRequired;
        private System.Windows.Forms.Label lbError;
        private System.Windows.Forms.Button btLoad;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

