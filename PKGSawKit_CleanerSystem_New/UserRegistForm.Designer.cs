namespace PKGSawKit_CleanerSystem_New
{
    partial class UserRegistForm
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.dataGridViewUserRegist = new System.Windows.Forms.DataGridView();
            this.iDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.nameDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.levelDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.passwordDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.userDataDataSet = new PKGSawKit_CleanerSystem_New.UserDataDataSet();
            this.boxUserRegist = new System.Windows.Forms.GroupBox();
            this.textBoxName = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.textBoxId = new System.Windows.Forms.TextBox();
            this.textBoxPassword = new System.Windows.Forms.TextBox();
            this.comboBoxLevel = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.boxUserChange = new System.Windows.Forms.GroupBox();
            this.label10 = new System.Windows.Forms.Label();
            this.textBoxChangeName = new System.Windows.Forms.TextBox();
            this.btnChangeComplete = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnChange = new System.Windows.Forms.Button();
            this.comboBoxChangeLevel = new System.Windows.Forms.ComboBox();
            this.textBoxChangePassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxChangeId = new System.Windows.Forms.TextBox();
            this.boxUserDel = new System.Windows.Forms.GroupBox();
            this.btnDelete = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxDeleteLevel = new System.Windows.Forms.TextBox();
            this.textBoxDeleteId = new System.Windows.Forms.TextBox();
            this.userTableTableAdapter = new PKGSawKit_CleanerSystem_New.UserDataDataSetTableAdapters.UserTableTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUserRegist)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userTableBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.userDataDataSet)).BeginInit();
            this.boxUserRegist.SuspendLayout();
            this.boxUserChange.SuspendLayout();
            this.boxUserDel.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridViewUserRegist
            // 
            this.dataGridViewUserRegist.AllowUserToAddRows = false;
            this.dataGridViewUserRegist.AllowUserToDeleteRows = false;
            this.dataGridViewUserRegist.AllowUserToResizeColumns = false;
            this.dataGridViewUserRegist.AllowUserToResizeRows = false;
            this.dataGridViewUserRegist.AutoGenerateColumns = false;
            this.dataGridViewUserRegist.BackgroundColor = System.Drawing.Color.White;
            this.dataGridViewUserRegist.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.dataGridViewUserRegist.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.Raised;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewUserRegist.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridViewUserRegist.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewUserRegist.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.iDDataGridViewTextBoxColumn,
            this.nameDataGridViewTextBoxColumn,
            this.levelDataGridViewTextBoxColumn,
            this.passwordDataGridViewTextBoxColumn});
            this.dataGridViewUserRegist.DataSource = this.userTableBindingSource;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridViewUserRegist.DefaultCellStyle = dataGridViewCellStyle5;
            this.dataGridViewUserRegist.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGridViewUserRegist.EnableHeadersVisualStyles = false;
            this.dataGridViewUserRegist.GridColor = System.Drawing.Color.White;
            this.dataGridViewUserRegist.Location = new System.Drawing.Point(12, 23);
            this.dataGridViewUserRegist.Name = "dataGridViewUserRegist";
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.Navy;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridViewUserRegist.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dataGridViewUserRegist.RowTemplate.Height = 23;
            this.dataGridViewUserRegist.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewUserRegist.Size = new System.Drawing.Size(734, 422);
            this.dataGridViewUserRegist.TabIndex = 1;
            this.dataGridViewUserRegist.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dataGridView1_CellFormatting);
            this.dataGridViewUserRegist.DataBindingComplete += new System.Windows.Forms.DataGridViewBindingCompleteEventHandler(this.dataGridView1_DataBindingComplete);
            this.dataGridViewUserRegist.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // iDDataGridViewTextBoxColumn
            // 
            this.iDDataGridViewTextBoxColumn.DataPropertyName = "ID";
            this.iDDataGridViewTextBoxColumn.HeaderText = "ID";
            this.iDDataGridViewTextBoxColumn.Name = "iDDataGridViewTextBoxColumn";
            // 
            // nameDataGridViewTextBoxColumn
            // 
            this.nameDataGridViewTextBoxColumn.DataPropertyName = "Name_";
            this.nameDataGridViewTextBoxColumn.HeaderText = "Name_";
            this.nameDataGridViewTextBoxColumn.Name = "nameDataGridViewTextBoxColumn";
            // 
            // levelDataGridViewTextBoxColumn
            // 
            this.levelDataGridViewTextBoxColumn.DataPropertyName = "Level";
            this.levelDataGridViewTextBoxColumn.HeaderText = "Level";
            this.levelDataGridViewTextBoxColumn.Name = "levelDataGridViewTextBoxColumn";
            // 
            // passwordDataGridViewTextBoxColumn
            // 
            this.passwordDataGridViewTextBoxColumn.DataPropertyName = "Password";
            this.passwordDataGridViewTextBoxColumn.HeaderText = "Password";
            this.passwordDataGridViewTextBoxColumn.Name = "passwordDataGridViewTextBoxColumn";
            // 
            // userTableBindingSource
            // 
            this.userTableBindingSource.DataMember = "UserTable";
            this.userTableBindingSource.DataSource = this.userDataDataSet;
            // 
            // userDataDataSet
            // 
            this.userDataDataSet.DataSetName = "UserDataDataSet";
            this.userDataDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // boxUserRegist
            // 
            this.boxUserRegist.BackColor = System.Drawing.Color.White;
            this.boxUserRegist.Controls.Add(this.textBoxName);
            this.boxUserRegist.Controls.Add(this.label9);
            this.boxUserRegist.Controls.Add(this.btnAdd);
            this.boxUserRegist.Controls.Add(this.textBoxId);
            this.boxUserRegist.Controls.Add(this.textBoxPassword);
            this.boxUserRegist.Controls.Add(this.comboBoxLevel);
            this.boxUserRegist.Controls.Add(this.label3);
            this.boxUserRegist.Controls.Add(this.label1);
            this.boxUserRegist.Controls.Add(this.label2);
            this.boxUserRegist.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boxUserRegist.ForeColor = System.Drawing.Color.Navy;
            this.boxUserRegist.Location = new System.Drawing.Point(752, 14);
            this.boxUserRegist.Name = "boxUserRegist";
            this.boxUserRegist.Size = new System.Drawing.Size(316, 432);
            this.boxUserRegist.TabIndex = 13;
            this.boxUserRegist.TabStop = false;
            this.boxUserRegist.Text = "사용자 등록";
            // 
            // textBoxName
            // 
            this.textBoxName.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxName.Location = new System.Drawing.Point(105, 89);
            this.textBoxName.Name = "textBoxName";
            this.textBoxName.Size = new System.Drawing.Size(193, 25);
            this.textBoxName.TabIndex = 32;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(58, 94);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(34, 17);
            this.label9.TabIndex = 31;
            this.label9.Text = "이름";
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.White;
            this.btnAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAdd.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAdd.ForeColor = System.Drawing.Color.Navy;
            this.btnAdd.Location = new System.Drawing.Point(199, 198);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(100, 42);
            this.btnAdd.TabIndex = 29;
            this.btnAdd.Text = "추가";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnAdd.UseVisualStyleBackColor = false;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // textBoxId
            // 
            this.textBoxId.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxId.Location = new System.Drawing.Point(105, 54);
            this.textBoxId.Name = "textBoxId";
            this.textBoxId.Size = new System.Drawing.Size(193, 25);
            this.textBoxId.TabIndex = 4;
            // 
            // textBoxPassword
            // 
            this.textBoxPassword.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxPassword.Location = new System.Drawing.Point(105, 163);
            this.textBoxPassword.Name = "textBoxPassword";
            this.textBoxPassword.PasswordChar = '●';
            this.textBoxPassword.Size = new System.Drawing.Size(193, 25);
            this.textBoxPassword.TabIndex = 10;
            // 
            // comboBoxLevel
            // 
            this.comboBoxLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxLevel.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxLevel.FormattingEnabled = true;
            this.comboBoxLevel.Location = new System.Drawing.Point(105, 124);
            this.comboBoxLevel.Name = "comboBoxLevel";
            this.comboBoxLevel.Size = new System.Drawing.Size(193, 25);
            this.comboBoxLevel.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(32, 167);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 17);
            this.label3.TabIndex = 9;
            this.label3.Text = "비밀번호";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(58, 128);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 17);
            this.label1.TabIndex = 7;
            this.label1.Text = "권한";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(32, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 17);
            this.label2.TabIndex = 8;
            this.label2.Text = "사원번호";
            // 
            // boxUserChange
            // 
            this.boxUserChange.BackColor = System.Drawing.Color.White;
            this.boxUserChange.Controls.Add(this.label10);
            this.boxUserChange.Controls.Add(this.textBoxChangeName);
            this.boxUserChange.Controls.Add(this.btnChangeComplete);
            this.boxUserChange.Controls.Add(this.btnCancel);
            this.boxUserChange.Controls.Add(this.btnChange);
            this.boxUserChange.Controls.Add(this.comboBoxChangeLevel);
            this.boxUserChange.Controls.Add(this.textBoxChangePassword);
            this.boxUserChange.Controls.Add(this.labelPassword);
            this.boxUserChange.Controls.Add(this.label5);
            this.boxUserChange.Controls.Add(this.label6);
            this.boxUserChange.Controls.Add(this.textBoxChangeId);
            this.boxUserChange.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boxUserChange.ForeColor = System.Drawing.Color.Navy;
            this.boxUserChange.Location = new System.Drawing.Point(317, 451);
            this.boxUserChange.Name = "boxUserChange";
            this.boxUserChange.Size = new System.Drawing.Size(430, 219);
            this.boxUserChange.TabIndex = 27;
            this.boxUserChange.TabStop = false;
            this.boxUserChange.Text = "사용자 수정";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(64, 87);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(34, 17);
            this.label10.TabIndex = 34;
            this.label10.Text = "이름";
            // 
            // textBoxChangeName
            // 
            this.textBoxChangeName.BackColor = System.Drawing.Color.Silver;
            this.textBoxChangeName.Enabled = false;
            this.textBoxChangeName.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxChangeName.Location = new System.Drawing.Point(111, 84);
            this.textBoxChangeName.Name = "textBoxChangeName";
            this.textBoxChangeName.Size = new System.Drawing.Size(193, 25);
            this.textBoxChangeName.TabIndex = 33;
            // 
            // btnChangeComplete
            // 
            this.btnChangeComplete.BackColor = System.Drawing.Color.White;
            this.btnChangeComplete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnChangeComplete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChangeComplete.FlatAppearance.BorderSize = 0;
            this.btnChangeComplete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnChangeComplete.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChangeComplete.ForeColor = System.Drawing.Color.Navy;
            this.btnChangeComplete.Location = new System.Drawing.Point(317, 119);
            this.btnChangeComplete.Name = "btnChangeComplete";
            this.btnChangeComplete.Size = new System.Drawing.Size(100, 25);
            this.btnChangeComplete.TabIndex = 32;
            this.btnChangeComplete.Text = "완료";
            this.btnChangeComplete.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnChangeComplete.UseVisualStyleBackColor = false;
            this.btnChangeComplete.Click += new System.EventHandler(this.btnChangeComplete_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.White;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FlatAppearance.BorderSize = 0;
            this.btnCancel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnCancel.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCancel.ForeColor = System.Drawing.Color.Navy;
            this.btnCancel.Location = new System.Drawing.Point(317, 84);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 25);
            this.btnCancel.TabIndex = 31;
            this.btnCancel.Text = "취소";
            this.btnCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnChange
            // 
            this.btnChange.BackColor = System.Drawing.Color.White;
            this.btnChange.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnChange.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChange.FlatAppearance.BorderSize = 0;
            this.btnChange.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnChange.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnChange.ForeColor = System.Drawing.Color.Navy;
            this.btnChange.Location = new System.Drawing.Point(317, 49);
            this.btnChange.Name = "btnChange";
            this.btnChange.Size = new System.Drawing.Size(100, 25);
            this.btnChange.TabIndex = 30;
            this.btnChange.Text = "수정";
            this.btnChange.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnChange.UseVisualStyleBackColor = false;
            this.btnChange.Click += new System.EventHandler(this.btnChange_Click);
            // 
            // comboBoxChangeLevel
            // 
            this.comboBoxChangeLevel.BackColor = System.Drawing.Color.Silver;
            this.comboBoxChangeLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxChangeLevel.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBoxChangeLevel.FormattingEnabled = true;
            this.comboBoxChangeLevel.Location = new System.Drawing.Point(111, 119);
            this.comboBoxChangeLevel.Name = "comboBoxChangeLevel";
            this.comboBoxChangeLevel.Size = new System.Drawing.Size(193, 25);
            this.comboBoxChangeLevel.TabIndex = 19;
            // 
            // textBoxChangePassword
            // 
            this.textBoxChangePassword.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxChangePassword.Location = new System.Drawing.Point(111, 157);
            this.textBoxChangePassword.Name = "textBoxChangePassword";
            this.textBoxChangePassword.PasswordChar = '●';
            this.textBoxChangePassword.Size = new System.Drawing.Size(193, 25);
            this.textBoxChangePassword.TabIndex = 18;
            this.textBoxChangePassword.Visible = false;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelPassword.Location = new System.Drawing.Point(38, 161);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(60, 17);
            this.labelPassword.TabIndex = 17;
            this.labelPassword.Text = "비밀번호";
            this.labelPassword.Visible = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(64, 123);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 17);
            this.label5.TabIndex = 15;
            this.label5.Text = "권한";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(38, 53);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(60, 17);
            this.label6.TabIndex = 16;
            this.label6.Text = "사원번호";
            // 
            // textBoxChangeId
            // 
            this.textBoxChangeId.BackColor = System.Drawing.Color.Silver;
            this.textBoxChangeId.Enabled = false;
            this.textBoxChangeId.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxChangeId.Location = new System.Drawing.Point(111, 49);
            this.textBoxChangeId.Name = "textBoxChangeId";
            this.textBoxChangeId.Size = new System.Drawing.Size(193, 25);
            this.textBoxChangeId.TabIndex = 13;
            // 
            // boxUserDel
            // 
            this.boxUserDel.BackColor = System.Drawing.Color.White;
            this.boxUserDel.Controls.Add(this.btnDelete);
            this.boxUserDel.Controls.Add(this.label7);
            this.boxUserDel.Controls.Add(this.label8);
            this.boxUserDel.Controls.Add(this.textBoxDeleteLevel);
            this.boxUserDel.Controls.Add(this.textBoxDeleteId);
            this.boxUserDel.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.boxUserDel.ForeColor = System.Drawing.Color.Navy;
            this.boxUserDel.Location = new System.Drawing.Point(317, 676);
            this.boxUserDel.Name = "boxUserDel";
            this.boxUserDel.Size = new System.Drawing.Size(431, 136);
            this.boxUserDel.TabIndex = 26;
            this.boxUserDel.TabStop = false;
            this.boxUserDel.Text = "사용자 삭제";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.Color.White;
            this.btnDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btnDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDelete.FlatAppearance.BorderSize = 0;
            this.btnDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnDelete.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.ForeColor = System.Drawing.Color.Red;
            this.btnDelete.Location = new System.Drawing.Point(317, 42);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(100, 25);
            this.btnDelete.TabIndex = 33;
            this.btnDelete.Text = "삭제";
            this.btnDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.btnDelete.UseVisualStyleBackColor = false;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(64, 83);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 17);
            this.label7.TabIndex = 17;
            this.label7.Text = "권한";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(38, 46);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(60, 17);
            this.label8.TabIndex = 18;
            this.label8.Text = "사원번호";
            // 
            // textBoxDeleteLevel
            // 
            this.textBoxDeleteLevel.BackColor = System.Drawing.Color.Silver;
            this.textBoxDeleteLevel.Enabled = false;
            this.textBoxDeleteLevel.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDeleteLevel.Location = new System.Drawing.Point(111, 79);
            this.textBoxDeleteLevel.Name = "textBoxDeleteLevel";
            this.textBoxDeleteLevel.Size = new System.Drawing.Size(193, 25);
            this.textBoxDeleteLevel.TabIndex = 12;
            // 
            // textBoxDeleteId
            // 
            this.textBoxDeleteId.BackColor = System.Drawing.Color.Silver;
            this.textBoxDeleteId.Enabled = false;
            this.textBoxDeleteId.Font = new System.Drawing.Font("Nirmala UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDeleteId.Location = new System.Drawing.Point(111, 42);
            this.textBoxDeleteId.Name = "textBoxDeleteId";
            this.textBoxDeleteId.Size = new System.Drawing.Size(193, 25);
            this.textBoxDeleteId.TabIndex = 11;
            // 
            // userTableTableAdapter
            // 
            this.userTableTableAdapter.ClearBeforeFill = true;
            // 
            // UserRegistForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1172, 824);
            this.Controls.Add(this.boxUserChange);
            this.Controls.Add(this.boxUserDel);
            this.Controls.Add(this.boxUserRegist);
            this.Controls.Add(this.dataGridViewUserRegist);
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "UserRegistForm";
            this.Activated += new System.EventHandler(this.UserRegistForm_Activated);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UserRegistForm_FormClosing);
            this.Load += new System.EventHandler(this.UserRegistForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewUserRegist)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userTableBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.userDataDataSet)).EndInit();
            this.boxUserRegist.ResumeLayout(false);
            this.boxUserRegist.PerformLayout();
            this.boxUserChange.ResumeLayout(false);
            this.boxUserChange.PerformLayout();
            this.boxUserDel.ResumeLayout(false);
            this.boxUserDel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewUserRegist;
        private System.Windows.Forms.GroupBox boxUserRegist;
        private System.Windows.Forms.TextBox textBoxId;
        private System.Windows.Forms.TextBox textBoxPassword;
        private System.Windows.Forms.ComboBox comboBoxLevel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox boxUserChange;
        private System.Windows.Forms.ComboBox comboBoxChangeLevel;
        private System.Windows.Forms.TextBox textBoxChangePassword;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxChangeId;
        private System.Windows.Forms.GroupBox boxUserDel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxDeleteLevel;
        private System.Windows.Forms.TextBox textBoxDeleteId;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnChangeComplete;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnChange;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox textBoxName;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox textBoxChangeName;
        private UserDataDataSet userDataDataSet;
        private System.Windows.Forms.BindingSource userTableBindingSource;
        private UserDataDataSetTableAdapters.UserTableTableAdapter userTableTableAdapter;
        private System.Windows.Forms.DataGridViewTextBoxColumn iDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn nameDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn levelDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn passwordDataGridViewTextBoxColumn;
    }
}