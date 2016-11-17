namespace WindowsFormsApplication3
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
            this.tbItemName = new System.Windows.Forms.TextBox();
            this.cbRarity = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbSpriteID = new System.Windows.Forms.TextBox();
            this.chBStackable = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbValue = new System.Windows.Forms.TextBox();
            this.chBTradeable = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbDescription = new System.Windows.Forms.TextBox();
            this.bSave = new System.Windows.Forms.Button();
            this.DisplayLabel = new System.Windows.Forms.Label();
            this.bLoad = new System.Windows.Forms.Button();
            this.bClear = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cbItemID = new System.Windows.Forms.ComboBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 69);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Item Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 108);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Rarity:";
            // 
            // tbItemName
            // 
            this.tbItemName.Location = new System.Drawing.Point(92, 66);
            this.tbItemName.Name = "tbItemName";
            this.tbItemName.Size = new System.Drawing.Size(159, 20);
            this.tbItemName.TabIndex = 2;
            // 
            // cbRarity
            // 
            this.cbRarity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbRarity.FormattingEnabled = true;
            this.cbRarity.Items.AddRange(new object[] {
            "Common",
            "Rare",
            "Legendary",
            "Unique"});
            this.cbRarity.Location = new System.Drawing.Point(92, 105);
            this.cbRarity.Name = "cbRarity";
            this.cbRarity.Size = new System.Drawing.Size(159, 21);
            this.cbRarity.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(324, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Sprite ID:";
            // 
            // tbSpriteID
            // 
            this.tbSpriteID.Location = new System.Drawing.Point(384, 67);
            this.tbSpriteID.Name = "tbSpriteID";
            this.tbSpriteID.Size = new System.Drawing.Size(131, 20);
            this.tbSpriteID.TabIndex = 3;
            this.tbSpriteID.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // chBStackable
            // 
            this.chBStackable.AutoSize = true;
            this.chBStackable.Location = new System.Drawing.Point(25, 147);
            this.chBStackable.Name = "chBStackable";
            this.chBStackable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chBStackable.Size = new System.Drawing.Size(77, 17);
            this.chBStackable.TabIndex = 6;
            this.chBStackable.Text = ":Stackable";
            this.chBStackable.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(324, 108);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(37, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Value:";
            // 
            // tbValue
            // 
            this.tbValue.Location = new System.Drawing.Point(384, 105);
            this.tbValue.Name = "tbValue";
            this.tbValue.Size = new System.Drawing.Size(131, 20);
            this.tbValue.TabIndex = 5;
            this.tbValue.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // chBTradeable
            // 
            this.chBTradeable.AutoSize = true;
            this.chBTradeable.Location = new System.Drawing.Point(327, 147);
            this.chBTradeable.Name = "chBTradeable";
            this.chBTradeable.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chBTradeable.Size = new System.Drawing.Size(77, 17);
            this.chBTradeable.TabIndex = 7;
            this.chBTradeable.Text = ":Tradeable";
            this.chBTradeable.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(26, 188);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 10;
            this.label5.Text = "Description:";
            // 
            // tbDescription
            // 
            this.tbDescription.Location = new System.Drawing.Point(31, 204);
            this.tbDescription.Multiline = true;
            this.tbDescription.Name = "tbDescription";
            this.tbDescription.Size = new System.Drawing.Size(484, 244);
            this.tbDescription.TabIndex = 8;
            // 
            // bSave
            // 
            this.bSave.Location = new System.Drawing.Point(440, 462);
            this.bSave.Name = "bSave";
            this.bSave.Size = new System.Drawing.Size(75, 23);
            this.bSave.TabIndex = 11;
            this.bSave.Text = "Save";
            this.bSave.UseVisualStyleBackColor = true;
            this.bSave.Click += new System.EventHandler(this.button1_Click);
            // 
            // DisplayLabel
            // 
            this.DisplayLabel.AutoSize = true;
            this.DisplayLabel.Location = new System.Drawing.Point(216, 467);
            this.DisplayLabel.Name = "DisplayLabel";
            this.DisplayLabel.Size = new System.Drawing.Size(0, 13);
            this.DisplayLabel.TabIndex = 11;
            // 
            // bLoad
            // 
            this.bLoad.Location = new System.Drawing.Point(31, 462);
            this.bLoad.Name = "bLoad";
            this.bLoad.Size = new System.Drawing.Size(75, 23);
            this.bLoad.TabIndex = 9;
            this.bLoad.Text = "Load";
            this.bLoad.UseVisualStyleBackColor = true;
            this.bLoad.Click += new System.EventHandler(this.bLoad_Click);
            // 
            // bClear
            // 
            this.bClear.Location = new System.Drawing.Point(121, 462);
            this.bClear.Name = "bClear";
            this.bClear.Size = new System.Drawing.Size(75, 23);
            this.bClear.TabIndex = 10;
            this.bClear.Text = "Clear";
            this.bClear.UseVisualStyleBackColor = true;
            this.bClear.Click += new System.EventHandler(this.bClear_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 26);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(44, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Item ID:";
            // 
            // cbItemID
            // 
            this.cbItemID.FormattingEnabled = true;
            this.cbItemID.Location = new System.Drawing.Point(92, 23);
            this.cbItemID.Name = "cbItemID";
            this.cbItemID.Size = new System.Drawing.Size(161, 21);
            this.cbItemID.TabIndex = 1;
            this.cbItemID.TextUpdate += new System.EventHandler(this.cbItemID_TextUpdate);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 505);
            this.Controls.Add(this.cbItemID);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.bClear);
            this.Controls.Add(this.bLoad);
            this.Controls.Add(this.DisplayLabel);
            this.Controls.Add(this.bSave);
            this.Controls.Add(this.tbDescription);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.chBTradeable);
            this.Controls.Add(this.tbValue);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chBStackable);
            this.Controls.Add(this.tbSpriteID);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbRarity);
            this.Controls.Add(this.tbItemName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbItemName;
        private System.Windows.Forms.ComboBox cbRarity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbSpriteID;
        private System.Windows.Forms.CheckBox chBStackable;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbValue;
        private System.Windows.Forms.CheckBox chBTradeable;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbDescription;
        private System.Windows.Forms.Button bSave;
        private System.Windows.Forms.Label DisplayLabel;
        private System.Windows.Forms.Button bLoad;
        private System.Windows.Forms.Button bClear;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cbItemID;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}

