namespace MBN_DotNet
{
    partial class MBN_Form
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
            this.cmbRadio = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnScan = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkRegMode = new System.Windows.Forms.CheckBox();
            this.lblIMSI = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblRegState = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblRegId = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblRegName = new System.Windows.Forms.Label();
            this.lblRegMode = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lvNetwork = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colType = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.lblLastScan = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnDisconnect = new System.Windows.Forms.Button();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.txtLog = new System.Windows.Forms.RichTextBox();
            this.btnPcConnect = new System.Windows.Forms.Button();
            this.btnPing = new System.Windows.Forms.Button();
            this.btnPcDisconnect = new System.Windows.Forms.Button();
            this.label8 = new System.Windows.Forms.Label();
            this.cmbLocalNetwork = new System.Windows.Forms.ComboBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtAPN = new System.Windows.Forms.TextBox();
            this.grpScan = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.grpScan.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbRadio
            // 
            this.cmbRadio.FormattingEnabled = true;
            this.cmbRadio.Location = new System.Drawing.Point(54, 16);
            this.cmbRadio.Name = "cmbRadio";
            this.cmbRadio.Size = new System.Drawing.Size(230, 21);
            this.cmbRadio.TabIndex = 0;
            this.cmbRadio.SelectedIndexChanged += new System.EventHandler(this.cmbRadio_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Radio";
            // 
            // btnScan
            // 
            this.btnScan.Location = new System.Drawing.Point(6, 144);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(73, 23);
            this.btnScan.TabIndex = 2;
            this.btnScan.Text = "Scan";
            this.btnScan.UseVisualStyleBackColor = true;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label2.Location = new System.Drawing.Point(6, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(37, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Mode:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkRegMode);
            this.groupBox1.Controls.Add(this.lblIMSI);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.lblRegState);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lblRegId);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lblRegName);
            this.groupBox1.Controls.Add(this.lblRegMode);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(13, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(498, 87);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Registration";
            // 
            // chkRegMode
            // 
            this.chkRegMode.AutoSize = true;
            this.chkRegMode.Location = new System.Drawing.Point(9, 65);
            this.chkRegMode.Name = "chkRegMode";
            this.chkRegMode.Size = new System.Drawing.Size(73, 17);
            this.chkRegMode.TabIndex = 20;
            this.chkRegMode.Text = "Automatic";
            this.chkRegMode.UseVisualStyleBackColor = true;
            this.chkRegMode.CheckedChanged += new System.EventHandler(this.chkRegMode_CheckedChanged);
            // 
            // lblIMSI
            // 
            this.lblIMSI.AutoSize = true;
            this.lblIMSI.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblIMSI.Location = new System.Drawing.Point(49, 19);
            this.lblIMSI.Name = "lblIMSI";
            this.lblIMSI.Size = new System.Drawing.Size(53, 13);
            this.lblIMSI.TabIndex = 19;
            this.lblIMSI.Text = "Unknown";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label9.Location = new System.Drawing.Point(6, 19);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(32, 13);
            this.label9.TabIndex = 18;
            this.label9.Text = "IMSI:";
            // 
            // lblRegState
            // 
            this.lblRegState.AutoSize = true;
            this.lblRegState.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblRegState.Location = new System.Drawing.Point(49, 49);
            this.lblRegState.Name = "lblRegState";
            this.lblRegState.Size = new System.Drawing.Size(53, 13);
            this.lblRegState.TabIndex = 17;
            this.lblRegState.Text = "Unknown";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label7.Location = new System.Drawing.Point(6, 49);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 13);
            this.label7.TabIndex = 16;
            this.label7.Text = "State:";
            // 
            // lblRegId
            // 
            this.lblRegId.AutoSize = true;
            this.lblRegId.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblRegId.Location = new System.Drawing.Point(271, 49);
            this.lblRegId.Name = "lblRegId";
            this.lblRegId.Size = new System.Drawing.Size(53, 13);
            this.lblRegId.TabIndex = 15;
            this.lblRegId.Text = "Unknown";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label6.Location = new System.Drawing.Point(228, 49);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(19, 13);
            this.label6.TabIndex = 14;
            this.label6.Text = "Id:";
            // 
            // lblRegName
            // 
            this.lblRegName.AutoSize = true;
            this.lblRegName.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblRegName.Location = new System.Drawing.Point(271, 33);
            this.lblRegName.Name = "lblRegName";
            this.lblRegName.Size = new System.Drawing.Size(53, 13);
            this.lblRegName.TabIndex = 13;
            this.lblRegName.Text = "Unknown";
            // 
            // lblRegMode
            // 
            this.lblRegMode.AutoSize = true;
            this.lblRegMode.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lblRegMode.Location = new System.Drawing.Point(49, 34);
            this.lblRegMode.Name = "lblRegMode";
            this.lblRegMode.Size = new System.Drawing.Size(53, 13);
            this.lblRegMode.TabIndex = 12;
            this.lblRegMode.Text = "Unknown";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label3.Location = new System.Drawing.Point(228, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "Name:";
            // 
            // lvNetwork
            // 
            this.lvNetwork.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colNumber,
            this.colType,
            this.colStatus});
            this.lvNetwork.FullRowSelect = true;
            this.lvNetwork.Location = new System.Drawing.Point(6, 19);
            this.lvNetwork.Name = "lvNetwork";
            this.lvNetwork.Size = new System.Drawing.Size(483, 119);
            this.lvNetwork.TabIndex = 10;
            this.lvNetwork.UseCompatibleStateImageBehavior = false;
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 120;
            // 
            // colNumber
            // 
            this.colNumber.Text = "Number";
            this.colNumber.Width = 120;
            // 
            // colType
            // 
            this.colType.Text = "LTE/GSM";
            // 
            // colStatus
            // 
            this.colStatus.Text = "Status";
            this.colStatus.Width = 120;
            // 
            // lblLastScan
            // 
            this.lblLastScan.AutoSize = true;
            this.lblLastScan.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastScan.Location = new System.Drawing.Point(143, 149);
            this.lblLastScan.Name = "lblLastScan";
            this.lblLastScan.Size = new System.Drawing.Size(60, 13);
            this.lblLastScan.TabIndex = 9;
            this.lblLastScan.Text = "Unknown";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(85, 149);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Last Scan: ";
            // 
            // btnDisconnect
            // 
            this.btnDisconnect.Location = new System.Drawing.Point(337, 144);
            this.btnDisconnect.Name = "btnDisconnect";
            this.btnDisconnect.Size = new System.Drawing.Size(73, 23);
            this.btnDisconnect.TabIndex = 7;
            this.btnDisconnect.Text = "Deregister";
            this.btnDisconnect.UseVisualStyleBackColor = true;
            this.btnDisconnect.Click += new System.EventHandler(this.btnDisconnect_Click);
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(416, 144);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(73, 23);
            this.btnConnect.TabIndex = 6;
            this.btnConnect.Text = "Register";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(825, 382);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(109, 23);
            this.btnClear.TabIndex = 6;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // txtLog
            // 
            this.txtLog.Location = new System.Drawing.Point(517, 16);
            this.txtLog.Name = "txtLog";
            this.txtLog.Size = new System.Drawing.Size(417, 360);
            this.txtLog.TabIndex = 4;
            this.txtLog.Text = "";
            // 
            // btnPcConnect
            // 
            this.btnPcConnect.Location = new System.Drawing.Point(9, 51);
            this.btnPcConnect.Name = "btnPcConnect";
            this.btnPcConnect.Size = new System.Drawing.Size(76, 23);
            this.btnPcConnect.TabIndex = 12;
            this.btnPcConnect.Text = "Connect";
            this.btnPcConnect.UseVisualStyleBackColor = true;
            this.btnPcConnect.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnPing
            // 
            this.btnPing.Location = new System.Drawing.Point(91, 51);
            this.btnPing.Name = "btnPing";
            this.btnPing.Size = new System.Drawing.Size(76, 23);
            this.btnPing.TabIndex = 13;
            this.btnPing.Text = "Ping";
            this.btnPing.UseVisualStyleBackColor = true;
            this.btnPing.Click += new System.EventHandler(this.Ping_Click);
            // 
            // btnPcDisconnect
            // 
            this.btnPcDisconnect.Location = new System.Drawing.Point(173, 51);
            this.btnPcDisconnect.Name = "btnPcDisconnect";
            this.btnPcDisconnect.Size = new System.Drawing.Size(76, 23);
            this.btnPcDisconnect.TabIndex = 14;
            this.btnPcDisconnect.Text = "Disconnect";
            this.btnPcDisconnect.UseVisualStyleBackColor = true;
            this.btnPcDisconnect.Click += new System.EventHandler(this.Disconnect_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(87, 13);
            this.label8.TabIndex = 16;
            this.label8.Text = "Network Adapter";
            // 
            // cmbLocalNetwork
            // 
            this.cmbLocalNetwork.FormattingEnabled = true;
            this.cmbLocalNetwork.Location = new System.Drawing.Point(94, 19);
            this.cmbLocalNetwork.Name = "cmbLocalNetwork";
            this.cmbLocalNetwork.Size = new System.Drawing.Size(230, 21);
            this.cmbLocalNetwork.TabIndex = 15;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnPcConnect);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Controls.Add(this.btnPing);
            this.groupBox2.Controls.Add(this.cmbLocalNetwork);
            this.groupBox2.Controls.Add(this.btnPcDisconnect);
            this.groupBox2.Location = new System.Drawing.Point(13, 322);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(498, 83);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Connect and Ping";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.label5.Location = new System.Drawing.Point(376, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "APN";
            // 
            // txtAPN
            // 
            this.txtAPN.Location = new System.Drawing.Point(411, 17);
            this.txtAPN.Name = "txtAPN";
            this.txtAPN.Size = new System.Drawing.Size(100, 20);
            this.txtAPN.TabIndex = 21;
            // 
            // grpScan
            // 
            this.grpScan.Controls.Add(this.lvNetwork);
            this.grpScan.Controls.Add(this.btnDisconnect);
            this.grpScan.Controls.Add(this.label4);
            this.grpScan.Controls.Add(this.btnConnect);
            this.grpScan.Controls.Add(this.lblLastScan);
            this.grpScan.Controls.Add(this.btnScan);
            this.grpScan.Enabled = false;
            this.grpScan.Location = new System.Drawing.Point(13, 141);
            this.grpScan.Name = "grpScan";
            this.grpScan.Size = new System.Drawing.Size(498, 175);
            this.grpScan.TabIndex = 22;
            this.grpScan.TabStop = false;
            this.grpScan.Text = "Scan";
            // 
            // MBN_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(949, 413);
            this.Controls.Add(this.grpScan);
            this.Controls.Add(this.txtAPN);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cmbRadio);
            this.Name = "MBN_Form";
            this.Text = "Mobile Test";
            this.Load += new System.EventHandler(this.MBN_Form_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.grpScan.ResumeLayout(false);
            this.grpScan.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbRadio;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnDisconnect;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblLastScan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView lvNetwork;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colNumber;
        private System.Windows.Forms.ColumnHeader colType;
        private System.Windows.Forms.ColumnHeader colStatus;
        private System.Windows.Forms.Label lblRegId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblRegName;
        private System.Windows.Forms.Label lblRegMode;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblRegState;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.RichTextBox txtLog;
        private System.Windows.Forms.Button btnPcConnect;
        private System.Windows.Forms.Button btnPing;
        private System.Windows.Forms.Button btnPcDisconnect;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox cmbLocalNetwork;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lblIMSI;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtAPN;
        private System.Windows.Forms.GroupBox grpScan;
        private System.Windows.Forms.CheckBox chkRegMode;
    }
}

