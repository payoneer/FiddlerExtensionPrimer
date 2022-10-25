namespace FiddlerExtensionPrimer
{
    partial class CodeCreatorForm
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
            this.btnCopyToClipBoard = new System.Windows.Forms.Button();
            this.rtbCode = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btnCopyToClipBoard
            // 
            this.btnCopyToClipBoard.Location = new System.Drawing.Point(13, 12);
            this.btnCopyToClipBoard.Name = "btnCopyToClipBoard";
            this.btnCopyToClipBoard.Size = new System.Drawing.Size(114, 23);
            this.btnCopyToClipBoard.TabIndex = 0;
            this.btnCopyToClipBoard.Text = "Copy to Clipboard";
            this.btnCopyToClipBoard.UseVisualStyleBackColor = true;
            this.btnCopyToClipBoard.Click += new System.EventHandler(this.btnCopyToClipBoard_Click);
            // 
            // rtbCode
            // 
            this.rtbCode.Location = new System.Drawing.Point(13, 52);
            this.rtbCode.Name = "rtbCode";
            this.rtbCode.Size = new System.Drawing.Size(748, 386);
            this.rtbCode.TabIndex = 1;
            this.rtbCode.Text = "";
            // 
            // CodeCreatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rtbCode);
            this.Controls.Add(this.btnCopyToClipBoard);
            this.Name = "CodeCreatorForm";
            this.Text = "CodeCreatorForm";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnCopyToClipBoard;
        private System.Windows.Forms.RichTextBox rtbCode;
    }
}