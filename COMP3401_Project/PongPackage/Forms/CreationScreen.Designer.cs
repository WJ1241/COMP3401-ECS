
namespace COMP3401_Project.PongPackage.Forms
{
    partial class CreationScreen
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
            this.CreateBttn = new System.Windows.Forms.Button();
            this.EntityCount = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.TerminateBttn = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.EntityCount)).BeginInit();
            this.SuspendLayout();
            // 
            // CreateBttn
            // 
            this.CreateBttn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CreateBttn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.CreateBttn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreateBttn.Location = new System.Drawing.Point(318, 360);
            this.CreateBttn.Margin = new System.Windows.Forms.Padding(0);
            this.CreateBttn.Name = "CreateBttn";
            this.CreateBttn.Size = new System.Drawing.Size(150, 50);
            this.CreateBttn.TabIndex = 0;
            this.CreateBttn.Text = "Create";
            this.CreateBttn.UseVisualStyleBackColor = true;
            this.CreateBttn.Click += new System.EventHandler(this.CreateBttn_Click);
            // 
            // EntityCount
            // 
            this.EntityCount.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EntityCount.Location = new System.Drawing.Point(318, 277);
            this.EntityCount.Maximum = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.EntityCount.Name = "EntityCount";
            this.EntityCount.Size = new System.Drawing.Size(150, 39);
            this.EntityCount.TabIndex = 1;
            this.EntityCount.ValueChanged += new System.EventHandler(this.EntityCount_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(210, 134);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(358, 44);
            this.label1.TabIndex = 2;
            this.label1.Text = "Enter No. of Entities";
            // 
            // TerminateBttn
            // 
            this.TerminateBttn.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TerminateBttn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.TerminateBttn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.TerminateBttn.Location = new System.Drawing.Point(318, 432);
            this.TerminateBttn.Margin = new System.Windows.Forms.Padding(0);
            this.TerminateBttn.Name = "TerminateBttn";
            this.TerminateBttn.Size = new System.Drawing.Size(150, 50);
            this.TerminateBttn.TabIndex = 3;
            this.TerminateBttn.Text = "Terminate";
            this.TerminateBttn.UseVisualStyleBackColor = true;
            this.TerminateBttn.Click += new System.EventHandler(this.TerminateBttn_Click);
            // 
            // CreationScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(776, 536);
            this.Controls.Add(this.TerminateBttn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EntityCount);
            this.Controls.Add(this.CreateBttn);
            this.Name = "CreationScreen";
            this.Padding = new System.Windows.Forms.Padding(20);
            this.Text = "CreationScreen";
            ((System.ComponentModel.ISupportInitialize)(this.EntityCount)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button CreateBttn;
        private System.Windows.Forms.NumericUpDown EntityCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button TerminateBttn;
    }
}