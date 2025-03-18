namespace MyToode
{
    partial class Kassa
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
            this.flowLayoutPanel_Tovary = new System.Windows.Forms.FlowLayoutPanel();
            this.label_SelectedTovar = new System.Windows.Forms.Label();
            this.numericUpDown_Quantity = new System.Windows.Forms.NumericUpDown();
            this.button_AddToCheck = new System.Windows.Forms.Button();
            this.button_RemoveFromCheck = new System.Windows.Forms.Button();
            this.listBox_Chek = new System.Windows.Forms.ListBox();
            this.button_Buy = new System.Windows.Forms.Button();
            this.button_Cancel = new System.Windows.Forms.Button();
            this.exit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Quantity)).BeginInit();
            this.SuspendLayout();
            // 
            // flowLayoutPanel_Tovary
            // 
            this.flowLayoutPanel_Tovary.AutoScroll = true;
            this.flowLayoutPanel_Tovary.Location = new System.Drawing.Point(448, 12);
            this.flowLayoutPanel_Tovary.Name = "flowLayoutPanel_Tovary";
            this.flowLayoutPanel_Tovary.Size = new System.Drawing.Size(305, 272);
            this.flowLayoutPanel_Tovary.TabIndex = 0;
            // 
            // label_SelectedTovar
            // 
            this.label_SelectedTovar.AutoSize = true;
            this.label_SelectedTovar.Location = new System.Drawing.Point(40, 123);
            this.label_SelectedTovar.Name = "label_SelectedTovar";
            this.label_SelectedTovar.Size = new System.Drawing.Size(0, 13);
            this.label_SelectedTovar.TabIndex = 1;
            // 
            // numericUpDown_Quantity
            // 
            this.numericUpDown_Quantity.Location = new System.Drawing.Point(35, 155);
            this.numericUpDown_Quantity.Name = "numericUpDown_Quantity";
            this.numericUpDown_Quantity.Size = new System.Drawing.Size(120, 20);
            this.numericUpDown_Quantity.TabIndex = 2;
            // 
            // button_AddToCheck
            // 
            this.button_AddToCheck.Location = new System.Drawing.Point(35, 282);
            this.button_AddToCheck.Name = "button_AddToCheck";
            this.button_AddToCheck.Size = new System.Drawing.Size(174, 41);
            this.button_AddToCheck.TabIndex = 3;
            this.button_AddToCheck.Text = "button_AddToCheck";
            this.button_AddToCheck.UseVisualStyleBackColor = true;
            this.button_AddToCheck.Click += new System.EventHandler(this.button_AddToCheck_Click);
            // 
            // button_RemoveFromCheck
            // 
            this.button_RemoveFromCheck.Location = new System.Drawing.Point(35, 343);
            this.button_RemoveFromCheck.Name = "button_RemoveFromCheck";
            this.button_RemoveFromCheck.Size = new System.Drawing.Size(174, 41);
            this.button_RemoveFromCheck.TabIndex = 4;
            this.button_RemoveFromCheck.Text = "button_RemoveFromCheck";
            this.button_RemoveFromCheck.UseVisualStyleBackColor = true;
            this.button_RemoveFromCheck.Click += new System.EventHandler(this.button_RemoveFromCheck_Click);
            // 
            // listBox_Chek
            // 
            this.listBox_Chek.FormattingEnabled = true;
            this.listBox_Chek.Location = new System.Drawing.Point(35, 12);
            this.listBox_Chek.Name = "listBox_Chek";
            this.listBox_Chek.Size = new System.Drawing.Size(383, 95);
            this.listBox_Chek.TabIndex = 5;
            // 
            // button_Buy
            // 
            this.button_Buy.Location = new System.Drawing.Point(448, 315);
            this.button_Buy.Name = "button_Buy";
            this.button_Buy.Size = new System.Drawing.Size(156, 69);
            this.button_Buy.TabIndex = 6;
            this.button_Buy.Text = "button_Buy";
            this.button_Buy.UseVisualStyleBackColor = true;
            this.button_Buy.Click += new System.EventHandler(this.button_Buy_Click);
            // 
            // button_Cancel
            // 
            this.button_Cancel.Location = new System.Drawing.Point(622, 315);
            this.button_Cancel.Name = "button_Cancel";
            this.button_Cancel.Size = new System.Drawing.Size(157, 69);
            this.button_Cancel.TabIndex = 7;
            this.button_Cancel.Text = "button_Cancel";
            this.button_Cancel.UseVisualStyleBackColor = true;
            this.button_Cancel.Click += new System.EventHandler(this.button_Cancel_Click);
            // 
            // exit
            // 
            this.exit.Location = new System.Drawing.Point(687, 403);
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(92, 35);
            this.exit.TabIndex = 8;
            this.exit.Text = "exit";
            this.exit.UseVisualStyleBackColor = true;
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // Kassa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.exit);
            this.Controls.Add(this.label_SelectedTovar);
            this.Controls.Add(this.button_Cancel);
            this.Controls.Add(this.button_Buy);
            this.Controls.Add(this.listBox_Chek);
            this.Controls.Add(this.button_RemoveFromCheck);
            this.Controls.Add(this.button_AddToCheck);
            this.Controls.Add(this.numericUpDown_Quantity);
            this.Controls.Add(this.flowLayoutPanel_Tovary);
            this.Name = "Kassa";
            this.Text = "Kassa";
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_Quantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_Tovary;
        private System.Windows.Forms.Label label_SelectedTovar;
        private System.Windows.Forms.NumericUpDown numericUpDown_Quantity;
        private System.Windows.Forms.Button button_AddToCheck;
        private System.Windows.Forms.Button button_RemoveFromCheck;
        private System.Windows.Forms.ListBox listBox_Chek;
        private System.Windows.Forms.Button button_Buy;
        private System.Windows.Forms.Button button_Cancel;
        private System.Windows.Forms.Button exit;
    }
}