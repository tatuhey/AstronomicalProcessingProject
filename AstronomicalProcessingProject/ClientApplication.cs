using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Resources;
using System.Globalization;
using AstronomicalProcessingProject.Properties;
using System.Runtime.InteropServices;

// Raihan Khalil Abdillah
// 30065695
// Assessment 02 of Complex Data Structures - Astronomical Processing Project
// 28/08/23 - 

namespace AstronomicalProcessingProject
{
    public partial class ClientApplication : Form
    {
        public ClientApplication()
        {
            InitializeComponent();
            ConnectToServer();
            rbLight.Checked = true;
        }

        private IAstroContract channel;

        private void ConnectToServer()
        {
            string address = "net.pipe://localhost/pipemynumbers";
            NetNamedPipeBinding binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            EndpointAddress ep = new EndpointAddress(address);
            channel = ChannelFactory<IAstroContract>.CreateChannel(binding, ep);
        }

        private void btnVelocity_Click(object sender, EventArgs e)
        {
            double obs;
            double rest;

            if (double.TryParse(tbObsWave.Text, out obs) && double.TryParse(tbRestWave.Text, out rest)) 
            {

                double starvelocity = channel.StarVelocity(obs, rest);
                tbStarVelocity.Text = starvelocity.ToString();

                ListViewItem item = new ListViewItem();
                item.SubItems[0].Text = starvelocity.ToString();

                lvData.Items.Add(item);

            }
            else
            {
                MessageBox.Show("Invalid input. Please enter valid numeric values.");
            }


        }

        private void btnDistance_Click(object sender, EventArgs e)
        {
            double par;

            if (double.TryParse(tbParallaxAngle.Text, out par))
            {
                double distance = channel.StarDistance(par);
                tbDistance.Text = distance.ToString();

                ListViewItem item = new ListViewItem();
                item.SubItems.Add(""); // Empty for lvColumn1
                item.SubItems.Add("");
                item.SubItems.Add(distance.ToString());
                item.SubItems.Add("");
                //item.SubItems[2].Text = distance.ToString();


                lvData.Items.Add(item);
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter valid numeric values.");
            }
        }

        private void btnKelvin_Click(object sender, EventArgs e)
        {
            double cel;

            if (double.TryParse(tbCelcius.Text, out cel))
            {
                double kelvin = channel.TempInKelvin(cel);
                tbKelvin.Text = kelvin.ToString();

                ListViewItem item = new ListViewItem();
                item.SubItems.Add("");
                item.SubItems.Add(kelvin.ToString());
                item.SubItems.Add("");
                item.SubItems.Add("");

                //item.SubItems[1].Text = kelvin.ToString();
                lvData.Items.Add(item);
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter valid numeric values.");
            }
        }

        private void btnRadius_Click(object sender, EventArgs e)
        {
            double mass;

            if (double.TryParse(tbMassBlackhole.Text, out mass))
            {
                double rad = channel.EventHorizon(mass);
                tbScwarzchild.Text = rad.ToString();

                ListViewItem item = new ListViewItem();
                item.SubItems.Add(""); // Empty for lvColumn2
                item.SubItems.Add(""); // Empty for lvColumn3
                item.SubItems.Add("");
                item.SubItems.Add(rad.ToString());

                //item.SubItems[3].Text = rad.ToString();

                lvData.Items.Add(item);
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter valid numeric values.");
            }
        }

        private void rbDark_CheckedChanged(object sender, EventArgs e)
        {
            SetDarkMode();
        }

        private void rbLight_CheckedChanged(object sender, EventArgs e)
        {
            SetLightMode();
        }

        private void SetDarkMode()
        {
            this.BackColor = SystemColors.ControlDarkDark;

            label1.BackColor = SystemColors.ControlDarkDark;
            label2.BackColor = SystemColors.ControlDarkDark;
            label3.BackColor = SystemColors.ControlDarkDark;
            label4.BackColor = SystemColors.ControlDarkDark;
            label5.BackColor = SystemColors.ControlDarkDark;
            label6.BackColor = SystemColors.ControlDarkDark;
            label7.BackColor = SystemColors.ControlDarkDark;
            label8.BackColor = SystemColors.ControlDarkDark;
            label9.BackColor = SystemColors.ControlDarkDark;
            label10.BackColor = SystemColors.ControlDarkDark;
            label11.BackColor = SystemColors.ControlDarkDark;
            label12.BackColor = SystemColors.ControlDarkDark;
            label13.BackColor = SystemColors.ControlDarkDark;

            label1.ForeColor = SystemColors.ControlLight;
            label2.ForeColor = SystemColors.ControlLight;
            label3.ForeColor = SystemColors.ControlLight;
            label4.ForeColor = SystemColors.ControlLight;
            label5.ForeColor = SystemColors.ControlLight;
            label6.ForeColor = SystemColors.ControlLight;
            label7.ForeColor = SystemColors.ControlLight;
            label8.ForeColor = SystemColors.ControlLight;
            label9.ForeColor = SystemColors.ControlLight;
            label10.ForeColor = SystemColors.ControlLight;
            label11.ForeColor = SystemColors.ControlLight;
            label12.ForeColor = SystemColors.ControlLight;
            label13.ForeColor = SystemColors.ControlLight;

            rbDark.BackColor = SystemColors.ControlDarkDark;
            rbLight.BackColor = SystemColors.ControlDarkDark;

            rbDark.ForeColor = SystemColors.ControlLight;
            rbLight.ForeColor = SystemColors.ControlLight;

            lvData.BackColor = SystemColors.ControlDark;

            lvData.ForeColor = SystemColors.ControlLight;

        }


        private void SetLightMode()
        {
            this.BackColor = SystemColors.Control;

            label1.BackColor = SystemColors.Control;
            label2.BackColor = SystemColors.Control;
            label3.BackColor = SystemColors.Control;
            label4.BackColor = SystemColors.Control;
            label5.BackColor = SystemColors.Control;
            label6.BackColor = SystemColors.Control;
            label7.BackColor = SystemColors.Control;
            label8.BackColor = SystemColors.Control;
            label9.BackColor = SystemColors.Control;
            label10.BackColor = SystemColors.Control;
            label11.BackColor = SystemColors.Control;
            label12.BackColor = SystemColors.Control;
            label13.BackColor = SystemColors.Control;

            label1.ForeColor = SystemColors.ControlText;
            label2.ForeColor = SystemColors.ControlText;
            label3.ForeColor = SystemColors.ControlText;
            label4.ForeColor = SystemColors.ControlText;
            label5.ForeColor = SystemColors.ControlText;
            label6.ForeColor = SystemColors.ControlText;
            label7.ForeColor = SystemColors.ControlText;
            label8.ForeColor = SystemColors.ControlText;
            label9.ForeColor = SystemColors.ControlText;
            label10.ForeColor = SystemColors.ControlText;
            label11.ForeColor = SystemColors.ControlText;
            label12.ForeColor = SystemColors.ControlText;
            label13.ForeColor = SystemColors.ControlText;

            rbDark.BackColor = SystemColors.Control;
            rbLight.BackColor = SystemColors.Control;

            rbDark.ForeColor = SystemColors.ControlText;
            rbLight.ForeColor = SystemColors.ControlText;

            lvData.BackColor = SystemColors.Window;

            lvData.ForeColor = SystemColors.ControlText;

        }


        private void ChangeLanguage(string language)
        {
            switch (language)
            {
                case "English":
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
                    break;
                case "French":
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr-FR");
                    break;
                case "German":
                    Thread.CurrentThread.CurrentUICulture = new CultureInfo("de-DE");
                    break;
            }
            Controls.Clear();
            InitializeComponent();
        }

        private void NewButtons()
        {
            CustomButton btnEnglish = new CustomButton();
            btnEnglish.Text = "English";
            btnEnglish.OutlineColor = Color.White; // Set the outline color
            btnEnglish.FillColor = Color.Black;    // Set the fill color
            btnEnglish.Click += btnEnglish_Click;
            // Add the button to your form's controls collection
            this.Controls.Add(btnEnglish);
        }

        private void btnFrench_Click(object sender, EventArgs e)
        {
            ChangeLanguage("French");
            rbLight.Checked = true;
        }

        private void btnEnglish_Click(object sender, EventArgs e)
        {
            ChangeLanguage("English");
            rbLight.Checked = true;
        }

        private void btnGerman_Click(object sender, EventArgs e)
        {
            ChangeLanguage("German");
            rbLight.Checked = true;
        }
    }


}
