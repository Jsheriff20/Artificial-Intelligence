﻿namespace AI_house_location_scorer
{
    partial class enter_details
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
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_house_postcode = new System.Windows.Forms.TextBox();
            this.txt_works_postcode = new System.Windows.Forms.TextBox();
            this.txt_download_speed = new System.Windows.Forms.TextBox();
            this.txt__upload_speed = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_next = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(55, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(796, 32);
            this.label1.TabIndex = 1;
            this.label1.Text = "Please fill in the sections below to get the most accurate result";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(189, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Future house\'s postcode:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(51, 157);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 20);
            this.label3.TabIndex = 3;
            this.label3.Text = "Work\'s postcode:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(51, 228);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(408, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Wanted broadband dowload speed (average is 54Mbps):";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(51, 298);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(397, 20);
            this.label5.TabIndex = 5;
            this.label5.Text = "Wanted broadband upload speed (average is 22Mbps):";
            // 
            // txt_house_postcode
            // 
            this.txt_house_postcode.Location = new System.Drawing.Point(55, 109);
            this.txt_house_postcode.Name = "txt_house_postcode";
            this.txt_house_postcode.Size = new System.Drawing.Size(127, 26);
            this.txt_house_postcode.TabIndex = 6;
            // 
            // txt_works_postcode
            // 
            this.txt_works_postcode.Location = new System.Drawing.Point(55, 180);
            this.txt_works_postcode.Name = "txt_works_postcode";
            this.txt_works_postcode.Size = new System.Drawing.Size(127, 26);
            this.txt_works_postcode.TabIndex = 7;
            // 
            // txt_download_speed
            // 
            this.txt_download_speed.Location = new System.Drawing.Point(55, 251);
            this.txt_download_speed.Name = "txt_download_speed";
            this.txt_download_speed.Size = new System.Drawing.Size(73, 26);
            this.txt_download_speed.TabIndex = 8;
            // 
            // txt__upload_speed
            // 
            this.txt__upload_speed.Location = new System.Drawing.Point(55, 321);
            this.txt__upload_speed.Name = "txt__upload_speed";
            this.txt__upload_speed.Size = new System.Drawing.Size(73, 26);
            this.txt__upload_speed.TabIndex = 9;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(128, 254);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(48, 20);
            this.label6.TabIndex = 10;
            this.label6.Text = "Mbps";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(128, 324);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(48, 20);
            this.label7.TabIndex = 11;
            this.label7.Text = "Mbps";
            // 
            // btn_next
            // 
            this.btn_next.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_next.Location = new System.Drawing.Point(354, 436);
            this.btn_next.Name = "btn_next";
            this.btn_next.Size = new System.Drawing.Size(186, 73);
            this.btn_next.TabIndex = 12;
            this.btn_next.Text = "Next";
            this.btn_next.UseVisualStyleBackColor = true;
            this.btn_next.Click += new System.EventHandler(this.btn_next_Click);
            // 
            // enter_details
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 521);
            this.Controls.Add(this.btn_next);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt__upload_speed);
            this.Controls.Add(this.txt_download_speed);
            this.Controls.Add(this.txt_works_postcode);
            this.Controls.Add(this.txt_house_postcode);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "enter_details";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_house_postcode;
        private System.Windows.Forms.TextBox txt_works_postcode;
        private System.Windows.Forms.TextBox txt_download_speed;
        private System.Windows.Forms.TextBox txt__upload_speed;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btn_next;
    }
}

