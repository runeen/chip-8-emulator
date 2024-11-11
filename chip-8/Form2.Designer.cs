
namespace chip_8
{
    partial class Form2
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
            this.Registrii = new System.Windows.Forms.Label();
            this.InstrRulata = new System.Windows.Forms.Label();
            this.InstUrm = new System.Windows.Forms.Label();
            this.memorie = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.ButtonStep = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Registrii
            // 
            this.Registrii.AutoSize = true;
            this.Registrii.Location = new System.Drawing.Point(36, 18);
            this.Registrii.Name = "Registrii";
            this.Registrii.Size = new System.Drawing.Size(67, 17);
            this.Registrii.TabIndex = 0;
            this.Registrii.Text = "Registrii: ";
            this.Registrii.Click += new System.EventHandler(this.label1_Click);
            // 
            // InstrRulata
            // 
            this.InstrRulata.AutoSize = true;
            this.InstrRulata.Location = new System.Drawing.Point(36, 79);
            this.InstrRulata.Name = "InstrRulata";
            this.InstrRulata.Size = new System.Drawing.Size(129, 17);
            this.InstrRulata.TabIndex = 1;
            this.InstrRulata.Text = "Instructiune rulata: ";
            this.InstrRulata.Click += new System.EventHandler(this.label2_Click);
            // 
            // InstUrm
            // 
            this.InstUrm.AutoSize = true;
            this.InstUrm.Location = new System.Drawing.Point(36, 147);
            this.InstUrm.Name = "InstUrm";
            this.InstUrm.Size = new System.Drawing.Size(168, 17);
            this.InstUrm.TabIndex = 2;
            this.InstUrm.Text = "Urmatoarea instructiune: ";
            this.InstUrm.Click += new System.EventHandler(this.label3_Click);
            // 
            // memorie
            // 
            this.memorie.AutoSize = true;
            this.memorie.Location = new System.Drawing.Point(39, 213);
            this.memorie.Name = "memorie";
            this.memorie.Size = new System.Drawing.Size(66, 17);
            this.memorie.TabIndex = 5;
            this.memorie.Text = "Memorie:";
            this.memorie.Click += new System.EventHandler(this.label6_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(39, 230);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(24, 17);
            this.label7.TabIndex = 6;
            this.label7.Text = "10";
            // 
            // ButtonStep
            // 
            this.ButtonStep.Location = new System.Drawing.Point(760, 207);
            this.ButtonStep.Name = "ButtonStep";
            this.ButtonStep.Size = new System.Drawing.Size(75, 23);
            this.ButtonStep.TabIndex = 7;
            this.ButtonStep.Text = "Step";
            this.ButtonStep.UseVisualStyleBackColor = true;
            this.ButtonStep.Click += new System.EventHandler(this.ButtonStep_Click);
            // 
            // Form2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(887, 638);
            this.Controls.Add(this.ButtonStep);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.memorie);
            this.Controls.Add(this.InstUrm);
            this.Controls.Add(this.InstrRulata);
            this.Controls.Add(this.Registrii);
            this.Name = "Form2";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.Label Registrii;
        public System.Windows.Forms.Label InstrRulata;
        public System.Windows.Forms.Label InstUrm;
        public System.Windows.Forms.Label memorie;
        public System.Windows.Forms.Label label7;
        public System.Windows.Forms.Button ButtonStep;
    }
}