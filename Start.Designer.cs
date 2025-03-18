namespace MyToode
{
    partial class Start
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
            this.buttonForm = new System.Windows.Forms.Button();
            this.buttonKassa = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonForm
            // 
            this.buttonForm.Location = new System.Drawing.Point(212, 296);
            this.buttonForm.Name = "buttonForm";
            this.buttonForm.Size = new System.Drawing.Size(131, 48);
            this.buttonForm.TabIndex = 0;
            this.buttonForm.Text = "button1";
            this.buttonForm.UseVisualStyleBackColor = true;
            this.buttonForm.Click += new System.EventHandler(this.buttonForm_Click);
            // 
            // buttonKassa
            // 
            this.buttonKassa.Location = new System.Drawing.Point(422, 296);
            this.buttonKassa.Name = "buttonKassa";
            this.buttonKassa.Size = new System.Drawing.Size(131, 48);
            this.buttonKassa.TabIndex = 1;
            this.buttonKassa.Text = "button2";
            this.buttonKassa.UseVisualStyleBackColor = true;
            this.buttonKassa.Click += new System.EventHandler(this.buttonKassa_Click);
            // 
            // Start
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.buttonKassa);
            this.Controls.Add(this.buttonForm);
            this.Name = "Start";
            this.Text = "Start";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button buttonForm;
        private System.Windows.Forms.Button buttonKassa;
    }
}