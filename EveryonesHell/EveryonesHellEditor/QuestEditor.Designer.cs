namespace EveryonesHellEditor
{
    partial class QuestEditor
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
            this.lbEnemy = new System.Windows.Forms.Label();
            this.tbEnemy = new System.Windows.Forms.TextBox();
            this.tbNumber = new System.Windows.Forms.TextBox();
            this.lbNumber = new System.Windows.Forms.Label();
            this.btnNew = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btSave
            // 
            this.btSave.Location = new System.Drawing.Point(41, 316);
            this.btSave.Margin = new System.Windows.Forms.Padding(2);
            this.btSave.Name = "btSave";
            this.btSave.Size = new System.Drawing.Size(74, 42);
            this.btSave.TabIndex = 7;
            this.btSave.Text = "Speichern";
            this.btSave.UseVisualStyleBackColor = true;
            this.btSave.Click += new System.EventHandler(this.btSave_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 31);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Quests";
            // 
            // lbName
            // 
            this.lbName.AutoSize = true;
            this.lbName.Location = new System.Drawing.Point(41, 64);
            this.lbName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbName.Name = "lbName";
            this.lbName.Size = new System.Drawing.Size(35, 13);
            this.lbName.TabIndex = 2;
            this.lbName.Text = "Name";
            // 
            // tbName
            // 
            this.tbName.Location = new System.Drawing.Point(41, 79);
            this.tbName.Margin = new System.Windows.Forms.Padding(2);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(443, 20);
            this.tbName.TabIndex = 1;
            // 
            // tbID
            // 
            this.tbID.Location = new System.Drawing.Point(41, 125);
            this.tbID.Margin = new System.Windows.Forms.Padding(2);
            this.tbID.Name = "tbID";
            this.tbID.Size = new System.Drawing.Size(141, 20);
            this.tbID.TabIndex = 2;
            this.tbID.TextChanged += new System.EventHandler(this.tbID_TextChanged);
            // 
            // lbID
            // 
            this.lbID.AutoSize = true;
            this.lbID.Location = new System.Drawing.Point(41, 110);
            this.lbID.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbID.Name = "lbID";
            this.lbID.Size = new System.Drawing.Size(49, 13);
            this.lbID.TabIndex = 5;
            this.lbID.Text = "Quest-ID";
            // 
            // lbQuestitem
            // 
            this.lbQuestitem.AutoSize = true;
            this.lbQuestitem.Location = new System.Drawing.Point(193, 110);
            this.lbQuestitem.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbQuestitem.Name = "lbQuestitem";
            this.lbQuestitem.Size = new System.Drawing.Size(74, 13);
            this.lbQuestitem.TabIndex = 6;
            this.lbQuestitem.Text = "Questitem - ID";
            // 
            // tbQuestitem
            // 
            this.tbQuestitem.Location = new System.Drawing.Point(192, 125);
            this.tbQuestitem.Margin = new System.Windows.Forms.Padding(2);
            this.tbQuestitem.Name = "tbQuestitem";
            this.tbQuestitem.Size = new System.Drawing.Size(141, 20);
            this.tbQuestitem.TabIndex = 3;
            this.tbQuestitem.TextChanged += new System.EventHandler(this.tbID_TextChanged);
            // 
            // lbDescription
            // 
            this.lbDescription.AutoSize = true;
            this.lbDescription.Location = new System.Drawing.Point(41, 205);
            this.lbDescription.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbDescription.Name = "lbDescription";
            this.lbDescription.Size = new System.Drawing.Size(60, 13);
            this.lbDescription.TabIndex = 8;
            this.lbDescription.Text = "Description";
            // 
            // tbDescription
            // 
            this.tbDescription.Location = new System.Drawing.Point(41, 220);
            this.tbDescription.Margin = new System.Windows.Forms.Padding(2);
            this.tbDescription.Multiline = true;
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(443, 87);
            this.tbDescription.TabIndex = 6;
            // 
            // lbBasedOn
            // 
            this.lbBasedOn.AutoSize = true;
            this.lbBasedOn.Location = new System.Drawing.Point(41, 157);
            this.lbBasedOn.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbBasedOn.Name = "lbBasedOn";
            this.lbBasedOn.Size = new System.Drawing.Size(117, 13);
            this.lbBasedOn.TabIndex = 10;
            this.lbBasedOn.Text = "Based on Dialogue - ID";
            // 
            // tbBasedOn
            // 
            this.tbBasedOn.Location = new System.Drawing.Point(41, 172);
            this.tbBasedOn.Margin = new System.Windows.Forms.Padding(2);
            this.tbBasedOn.Name = "tbBasedOn";
            this.tbBasedOn.Size = new System.Drawing.Size(141, 20);
            this.tbBasedOn.TabIndex = 4;
            this.tbBasedOn.TextChanged += new System.EventHandler(this.tbID_TextChanged);
            // 
            // lbRequire
            // 
            this.lbRequire.AutoSize = true;
            this.lbRequire.Location = new System.Drawing.Point(192, 157);
            this.lbRequire.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbRequire.Name = "lbRequire";
            this.lbRequire.Size = new System.Drawing.Size(72, 13);
            this.lbRequire.TabIndex = 12;
            this.lbRequire.Text = "Required item";
            // 
            // tbRequired
            // 
            this.tbRequired.Location = new System.Drawing.Point(192, 172);
            this.tbRequired.Margin = new System.Windows.Forms.Padding(2);
            this.tbRequired.Name = "tbRequired";
            this.tbRequired.Size = new System.Drawing.Size(141, 20);
            this.tbRequired.TabIndex = 5;
            this.tbRequired.TextChanged += new System.EventHandler(this.tbID_TextChanged);
            // 
            // lbError
            // 
            this.lbError.AutoSize = true;
            this.lbError.Location = new System.Drawing.Point(240, 331);
            this.lbError.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbError.Name = "lbError";
            this.lbError.Size = new System.Drawing.Size(0, 13);
            this.lbError.TabIndex = 14;
            // 
            // lbEnemy
            // 
            this.lbEnemy.AutoSize = true;
            this.lbEnemy.Location = new System.Drawing.Point(344, 111);
            this.lbEnemy.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbEnemy.Name = "lbEnemy";
            this.lbEnemy.Size = new System.Drawing.Size(86, 13);
            this.lbEnemy.TabIndex = 15;
            this.lbEnemy.Text = "Enemy to kill - ID";
            // 
            // tbEnemy
            // 
            this.tbEnemy.Location = new System.Drawing.Point(343, 125);
            this.tbEnemy.Margin = new System.Windows.Forms.Padding(2);
            this.tbEnemy.Name = "tbEnemy";
            this.tbEnemy.Size = new System.Drawing.Size(141, 20);
            this.tbEnemy.TabIndex = 16;
            this.tbEnemy.TextChanged += new System.EventHandler(this.tbID_TextChanged);
            // 
            // tbNumber
            // 
            this.tbNumber.Location = new System.Drawing.Point(343, 172);
            this.tbNumber.Margin = new System.Windows.Forms.Padding(2);
            this.tbNumber.Name = "tbNumber";
            this.tbNumber.Size = new System.Drawing.Size(141, 20);
            this.tbNumber.TabIndex = 17;
            this.tbNumber.TextChanged += new System.EventHandler(this.tbID_TextChanged);
            // 
            // lbNumber
            // 
            this.lbNumber.AutoSize = true;
            this.lbNumber.Location = new System.Drawing.Point(346, 155);
            this.lbNumber.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lbNumber.Name = "lbNumber";
            this.lbNumber.Size = new System.Drawing.Size(125, 13);
            this.lbNumber.TabIndex = 18;
            this.lbNumber.Text = "Number of enemies to kill";
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(120, 316);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(91, 42);
            this.btnNew.TabIndex = 19;
            this.btnNew.Text = "New";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // QuestEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.lbNumber);
            this.Controls.Add(this.tbNumber);
            this.Controls.Add(this.tbEnemy);
            this.Controls.Add(this.lbEnemy);
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
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "QuestEditor";
            this.Size = new System.Drawing.Size(526, 406);
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
        private System.Windows.Forms.Label lbEnemy;
        private System.Windows.Forms.TextBox tbEnemy;
        private System.Windows.Forms.TextBox tbNumber;
        private System.Windows.Forms.Label lbNumber;
        private System.Windows.Forms.Button btnNew;
    }
}

