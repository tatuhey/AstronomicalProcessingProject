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
            rbLight.Checked = true;
        }


        private void btnVelocity_Click(object sender, EventArgs e)
        {
            double obs;
            double rest;

            if (double.TryParse(tbObsWave.Text, out obs) && double.TryParse(tbRestWave.Text, out rest)) 
            {
                string address = "net.pipe://localhost/pipemynumbers";
                NetNamedPipeBinding binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
                EndpointAddress ep = new EndpointAddress(address);
                IAstroContract channel = ChannelFactory<IAstroContract>.CreateChannel(binding, ep);

                double starvelocity = channel.StarVelocity(obs, rest);
                tbStarVelocity.Text = starvelocity.ToString();

                ListViewItem item = new ListViewItem("Velocity");
                item.SubItems.Add(starvelocity.ToString());

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
                string address = "net.pipe://localhost/pipemynumbers";
                NetNamedPipeBinding binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
                EndpointAddress ep = new EndpointAddress(address);
                IAstroContract channel = ChannelFactory<IAstroContract>.CreateChannel(binding, ep);

                double distance = channel.StarDistance(par);
                tbDistance.Text = distance.ToString();

                ListViewItem item = new ListViewItem("Distance");
                item.SubItems.Add(distance.ToString());

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
                string address = "net.pipe://localhost/pipemynumbers";
                NetNamedPipeBinding binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
                EndpointAddress ep = new EndpointAddress(address);
                IAstroContract channel = ChannelFactory<IAstroContract>.CreateChannel(binding, ep);

                double kelvin = channel.TempInKelvin(cel);
                tbKelvin.Text = kelvin.ToString();

                ListViewItem item = new ListViewItem("Kelvin");
                item.SubItems.Add(kelvin.ToString());

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
                string address = "net.pipe://localhost/pipemynumbers";
                NetNamedPipeBinding binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
                EndpointAddress ep = new EndpointAddress(address);
                IAstroContract channel = ChannelFactory<IAstroContract>.CreateChannel(binding, ep);

                double rad = channel.EventHorizon(mass);
                tbScwarzchild.Text = rad.ToString();

                ListViewItem item = new ListViewItem("Scwarzchild Radius");
                item.SubItems.Add(rad.ToString());

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

        private void btnFrench_Click(object sender, EventArgs e)
        {
            ChangeLanguage("French");
        }

        private void btnEnglish_Click(object sender, EventArgs e)
        {
            ChangeLanguage("English");
        }

        private void btnGerman_Click(object sender, EventArgs e)
        {
            ChangeLanguage("German");
        }
    }


}
