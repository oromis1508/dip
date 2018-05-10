﻿using System;
using System.Drawing;
using System.Windows.Forms;

namespace ReportApp
{
    partial class ServerMainForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.serverInfoArea = new ReportApp.ServerInfoArea();
            this.btnAddUser = new System.Windows.Forms.Button();
            btnOpenClientInfo = new System.Windows.Forms.Button();
            flowLayoutPanel = new FlowLayoutPanel();
            this.SuspendLayout();

            this.btnAddUser.Name = "btnAddUser";
            this.btnAddUser.Text = "Добавить пользователя";
            btnAddUser.AutoSize = true;

            this.btnAddUser.Name = "btnOpenClientInfo";
            btnOpenClientInfo.Text = "Открыть лог клиента";
            btnOpenClientInfo.AutoSize = true;

            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 320);
            this.Controls.Add(this.serverInfoArea);
            this.Name = "ServerMainForm";
            this.Text = "Server";
            this.ResumeLayout(false);
            this.PerformLayout();

            flowLayoutPanel.Dock = DockStyle.Bottom;
            flowLayoutPanel.AutoSize = true;
            flowLayoutPanel.Controls.Add(btnAddUser);
            flowLayoutPanel.Controls.Add(btnOpenClientInfo);
            this.Controls.Add(this.flowLayoutPanel);
        }

        #endregion
        private ServerInfoArea serverInfoArea;
        private Button btnAddUser;
        private Button btnOpenClientInfo;
        private FlowLayoutPanel flowLayoutPanel;
    }
}

