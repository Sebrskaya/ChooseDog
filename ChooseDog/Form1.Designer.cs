namespace ChooseDog
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.button_Yes = new System.Windows.Forms.Button();
            this.button_IDK = new System.Windows.Forms.Button();
            this.button_No = new System.Windows.Forms.Button();
            this.label_Question = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button_Yes
            // 
            this.button_Yes.Location = new System.Drawing.Point(68, 291);
            this.button_Yes.Name = "button_Yes";
            this.button_Yes.Size = new System.Drawing.Size(171, 68);
            this.button_Yes.TabIndex = 0;
            this.button_Yes.Text = "Да";
            this.button_Yes.UseVisualStyleBackColor = true;
            this.button_Yes.Click += new System.EventHandler(this.button_Yes_Click);
            // 
            // button_IDK
            // 
            this.button_IDK.Location = new System.Drawing.Point(317, 291);
            this.button_IDK.Name = "button_IDK";
            this.button_IDK.Size = new System.Drawing.Size(171, 68);
            this.button_IDK.TabIndex = 1;
            this.button_IDK.Text = "Не знаю";
            this.button_IDK.UseVisualStyleBackColor = true;
            this.button_IDK.Click += new System.EventHandler(this.button_IDK_Click);
            // 
            // button_No
            // 
            this.button_No.Location = new System.Drawing.Point(567, 291);
            this.button_No.Name = "button_No";
            this.button_No.Size = new System.Drawing.Size(171, 68);
            this.button_No.TabIndex = 2;
            this.button_No.Text = "Нет";
            this.button_No.UseVisualStyleBackColor = true;
            this.button_No.Click += new System.EventHandler(this.button_No_Click);
            // 
            // label_Question
            // 
            this.label_Question.AutoSize = true;
            this.label_Question.Location = new System.Drawing.Point(378, 131);
            this.label_Question.Name = "label_Question";
            this.label_Question.Size = new System.Drawing.Size(35, 13);
            this.label_Question.TabIndex = 3;
            this.label_Question.Text = "label1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label_Question);
            this.Controls.Add(this.button_No);
            this.Controls.Add(this.button_IDK);
            this.Controls.Add(this.button_Yes);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_Yes;
        private System.Windows.Forms.Button button_IDK;
        private System.Windows.Forms.Button button_No;
        private System.Windows.Forms.Label label_Question;
    }
}

