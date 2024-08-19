using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Threading;
using System.Security.Policy;
using System.IO;

namespace usersettings_test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        
        public void Load_settings_collection()
        {
            StringCollection selected_res_string = Properties.Settings.Default.USR_SET;
            string sText = txtInput.Text;

            for (int i = 0; i < selected_res_string.Count; i++) 
            { 
                string iText = selected_res_string[i];
                string[] stCol;
                char splitchar = ',';
                if (iText != null) {
                    stCol = iText.Split(splitchar);
                    if (stCol[1].Contains(sText))
                    {
                        txtSetting.Text = stCol[2];
                        txtOutput.Text = stCol[3];
                        lblDesc.Text = stCol[0];
                        break;
                    }
                    else 
                    {
                        txtSetting.Text = null;
                        txtOutput.Text = null;
                        lblDesc.Text = null;
                    }
                }
            }

        }

        public void Load_textfile_settings() {
            string filepath = Application.StartupPath + @"\UserConfig\User_Config.txt";
            if (File.Exists(filepath))
            {
                List<string> lines = new List<string>();
                lines = File.ReadAllLines(filepath).ToList();
                string sText = txtInput.Text;
                for (int i = 0; i < lines.Count; i++)
                {
                    string iText = lines[i];
                    string[] stCol;
                    char splitchar = ',';
                    if (iText != null)
                    {
                        stCol = iText.Split(splitchar);
                        if (stCol[1].Contains(sText))
                        {
                            txtSetting.Text = stCol[2];
                            txtOutput.Text = stCol[3];
                            lblDesc.Text = stCol[0];
                            File.OpenRead(filepath).Dispose();
                            break;
                        }
                        else
                        {
                            txtSetting.Text = null;
                            txtOutput.Text = null;
                            lblDesc.Text = null;
                        }
                    }
                }
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            if (chkTxtconfig.Checked)
            {
                Load_textfile_settings();
            }
            else
            {
                Load_settings_collection();
            }
        }

        private void chkTxtconfig_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTxtconfig.Checked)
            {
                string filepath = Application.StartupPath + @"\UserConfig\User_Config.txt";
                if (File.Exists(filepath))
                {
                    string txtContext = File.ReadAllText(filepath);
                    txtList.Text = txtContext;
                    txtList.Visible = true;
                    btnSave.Visible = true;
                    File.OpenRead(filepath).Dispose();
                }
            }
            else {
                txtList.Text = null;
                txtList.Visible = false;
                btnSave.Visible = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string filepath = Application.StartupPath + @"\UserConfig\User_Config.txt";
            if (File.Exists(filepath)) {
                if (txtList.Text != null) { 
                    StreamWriter sw = new StreamWriter(filepath);
                    sw.Write(txtList.Text);
                    sw.Close();
                }
            }

        }
    }
}
