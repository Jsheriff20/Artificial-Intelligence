namespace AI_house_location_scorer
{
    partial class aspects_to_monitor
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
        /// 
        private void InitializeComponent()
        {
            this.btn_get_score = new System.Windows.Forms.Button();
            this.btn_back = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.nud_distance_from_school = new System.Windows.Forms.NumericUpDown();
            this.nud_illegal_activity = new System.Windows.Forms.NumericUpDown();
            this.nud_upload_speed = new System.Windows.Forms.NumericUpDown();
            this.nud_average_price = new System.Windows.Forms.NumericUpDown();
            this.nud_distance_from_work = new System.Windows.Forms.NumericUpDown();
            this.nud_download_speed = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.txt_total_percentage = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.nud_distance_from_school)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_illegal_activity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_upload_speed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_average_price)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_distance_from_work)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_download_speed)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_get_score
            // 
            this.btn_get_score.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_get_score.Location = new System.Drawing.Point(452, 436);
            this.btn_get_score.Name = "btn_get_score";
            this.btn_get_score.Size = new System.Drawing.Size(186, 73);
            this.btn_get_score.TabIndex = 1;
            this.btn_get_score.Text = "Score my location";
            this.btn_get_score.UseVisualStyleBackColor = true;
            // 
            // btn_back
            // 
            this.btn_back.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_back.Location = new System.Drawing.Point(253, 436);
            this.btn_back.Name = "btn_back";
            this.btn_back.Size = new System.Drawing.Size(186, 73);
            this.btn_back.TabIndex = 13;
            this.btn_back.Text = "Back";
            this.btn_back.UseVisualStyleBackColor = true;
            this.btn_back.Click += new System.EventHandler(this.btn_back_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(206, 237);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(127, 20);
            this.label1.TabIndex = 14;
            this.label1.Text = "Distance to work";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(206, 278);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(208, 20);
            this.label2.TabIndex = 15;
            this.label2.Text = "Broadband download speed";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(206, 319);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(188, 20);
            this.label3.TabIndex = 16;
            this.label3.Text = "Broadband upload speed";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(206, 114);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(166, 20);
            this.label4.TabIndex = 17;
            this.label4.Text = "Distance from schools";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(206, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(101, 20);
            this.label5.TabIndex = 18;
            this.label5.Text = "Illegal activity";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(206, 196);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(225, 20);
            this.label6.TabIndex = 19;
            this.label6.Text = "Average prices in recent years ";
            // 
            // nud_distance_from_school
            // 
            this.nud_distance_from_school.Location = new System.Drawing.Point(452, 114);
            this.nud_distance_from_school.Name = "nud_distance_from_school";
            this.nud_distance_from_school.Size = new System.Drawing.Size(76, 26);
            this.nud_distance_from_school.TabIndex = 20;
            this.nud_distance_from_school.Tag = "";
            this.nud_distance_from_school.ValueChanged += new System.EventHandler(this.nud_weighting_ValueChanged);
            // 
            // nud_illegal_activity
            // 
            this.nud_illegal_activity.Location = new System.Drawing.Point(452, 155);
            this.nud_illegal_activity.Name = "nud_illegal_activity";
            this.nud_illegal_activity.Size = new System.Drawing.Size(76, 26);
            this.nud_illegal_activity.TabIndex = 21;
            this.nud_illegal_activity.ValueChanged += new System.EventHandler(this.nud_weighting_ValueChanged);
            // 
            // nud_upload_speed
            // 
            this.nud_upload_speed.Location = new System.Drawing.Point(452, 319);
            this.nud_upload_speed.Name = "nud_upload_speed";
            this.nud_upload_speed.Size = new System.Drawing.Size(76, 26);
            this.nud_upload_speed.TabIndex = 22;
            this.nud_upload_speed.ValueChanged += new System.EventHandler(this.nud_weighting_ValueChanged);
            // 
            // nud_average_price
            // 
            this.nud_average_price.Location = new System.Drawing.Point(452, 196);
            this.nud_average_price.Name = "nud_average_price";
            this.nud_average_price.Size = new System.Drawing.Size(76, 26);
            this.nud_average_price.TabIndex = 23;
            this.nud_average_price.ValueChanged += new System.EventHandler(this.nud_weighting_ValueChanged);
            // 
            // nud_distance_from_work
            // 
            this.nud_distance_from_work.Location = new System.Drawing.Point(452, 237);
            this.nud_distance_from_work.Name = "nud_distance_from_work";
            this.nud_distance_from_work.Size = new System.Drawing.Size(76, 26);
            this.nud_distance_from_work.TabIndex = 24;
            this.nud_distance_from_work.ValueChanged += new System.EventHandler(this.nud_weighting_ValueChanged);
            // 
            // nud_download_speed
            // 
            this.nud_download_speed.Location = new System.Drawing.Point(452, 278);
            this.nud_download_speed.Name = "nud_download_speed";
            this.nud_download_speed.Size = new System.Drawing.Size(76, 26);
            this.nud_download_speed.TabIndex = 25;
            this.nud_download_speed.ValueChanged += new System.EventHandler(this.nud_weighting_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(533, 114);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 26);
            this.label7.TabIndex = 26;
            this.label7.Text = "%";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(533, 155);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 26);
            this.label8.TabIndex = 27;
            this.label8.Text = "%";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(533, 237);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(33, 26);
            this.label9.TabIndex = 29;
            this.label9.Text = "%";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(533, 196);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(33, 26);
            this.label10.TabIndex = 28;
            this.label10.Text = "%";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(533, 319);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(33, 26);
            this.label11.TabIndex = 31;
            this.label11.Text = "%";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(533, 278);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(33, 26);
            this.label12.TabIndex = 30;
            this.label12.Text = "%";
            // 
            // txt_total_percentage
            // 
            this.txt_total_percentage.AutoSize = true;
            this.txt_total_percentage.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_total_percentage.Location = new System.Drawing.Point(447, 376);
            this.txt_total_percentage.Name = "txt_total_percentage";
            this.txt_total_percentage.Size = new System.Drawing.Size(46, 26);
            this.txt_total_percentage.TabIndex = 32;
            this.txt_total_percentage.Text = "0%";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label14.Location = new System.Drawing.Point(205, 376);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(129, 26);
            this.label14.TabIndex = 33;
            this.label14.Text = "Total score";
            // 
            // aspects_to_monitor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(894, 521);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.txt_total_percentage);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.nud_download_speed);
            this.Controls.Add(this.nud_distance_from_work);
            this.Controls.Add(this.nud_average_price);
            this.Controls.Add(this.nud_upload_speed);
            this.Controls.Add(this.nud_illegal_activity);
            this.Controls.Add(this.nud_distance_from_school);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_back);
            this.Controls.Add(this.btn_get_score);
            this.Name = "aspects_to_monitor";
            this.Text = "aspects_to_monitor";
            ((System.ComponentModel.ISupportInitialize)(this.nud_distance_from_school)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_illegal_activity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_upload_speed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_average_price)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_distance_from_work)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.nud_download_speed)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_get_score;
        private System.Windows.Forms.Button btn_back;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown nud_distance_from_school;
        private System.Windows.Forms.NumericUpDown nud_illegal_activity;
        private System.Windows.Forms.NumericUpDown nud_upload_speed;
        private System.Windows.Forms.NumericUpDown nud_average_price;
        private System.Windows.Forms.NumericUpDown nud_distance_from_work;
        private System.Windows.Forms.NumericUpDown nud_download_speed;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label txt_total_percentage;
        private System.Windows.Forms.Label label14;
    }
}