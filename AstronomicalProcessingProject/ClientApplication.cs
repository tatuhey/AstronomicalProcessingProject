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

        private void AddToListView (int column, string result)
        {
            bool added = false;
            foreach(ListViewItem item in lvData.Items)
            {
                if (string.IsNullOrEmpty(item.SubItems[column - 1].Text))
                {
                    item.SubItems[column - 1].Text = result;
                    added = true;
                    break;
                }
            }
            if (!added)
            {
                ListViewItem item = new ListViewItem(new string[4]);
                item.SubItems[column - 1].Text = result;
                lvData.Items.Add(item);
            }
        }

        private void btnVelocity_Click(object sender, EventArgs e)
        {
            double obs;
            double rest;
            
            if (double.TryParse(tbObsWave.Text, out obs) && double.TryParse(tbRestWave.Text, out rest)) 
            {
                double starvelocity = channel.StarVelocity(obs, rest);

                AddToListView(1, starvelocity.ToString());
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

                AddToListView(2, distance.ToString());
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
                
                AddToListView(3, kelvin.ToString());
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

                AddToListView(4, rad.ToString());
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter valid numeric values.");
            }
        }

        private void rbDark_CheckedChanged(object sender, EventArgs e)
        {
            SetDarkMode();
            darkModeToolStripMenuItem.Checked = true;
            lightModeToolStripMenuItem.Checked = false;
        }

        private void rbLight_CheckedChanged(object sender, EventArgs e)
        {
            SetLightMode();
            lightModeToolStripMenuItem.Checked = true;
            darkModeToolStripMenuItem.Checked = false;
        }

        private void lightModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetLightMode();
            rbLight.Checked = true;
        }

        private void darkModeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SetDarkMode();
            rbDark.Checked = true;
        }

        private void ColourButtons (string colour)
        {
            switch (colour)
            {
                case "light":
                    btnBackgroundColour.BackColor = SystemColors.Control;
                    btnDistance.BackColor = SystemColors.Control;
                    btnFontColour.BackColor = SystemColors.Control;
                    btnKelvin.BackColor = SystemColors.Control;
                    btnRadius.BackColor = SystemColors.Control;
                    btnVelocity.BackColor = SystemColors.Control;
                    break;
                case "dark":
                    btnBackgroundColour.BackColor = SystemColors.ControlDark;
                    btnDistance.BackColor= SystemColors.ControlDark;
                    btnFontColour.BackColor= SystemColors.ControlDark;
                    btnKelvin.BackColor= SystemColors.ControlDark;
                    btnRadius.BackColor = SystemColors.ControlDark;
                    btnVelocity.BackColor= SystemColors.ControlDark;
                    break;
            }
        }

        private void SetDarkMode()
        {
            ColourButtons("dark");
            this.BackColor = SystemColors.ControlDarkDark;

            gbCalculation.ForeColor = SystemColors.ControlLight;
            gbLanguages.ForeColor = SystemColors.ControlLight;
            gbVisualStyle.ForeColor = SystemColors.ControlLight;

            gbCalculation.BackColor = SystemColors.ControlDarkDark;
            gbLanguages.BackColor = SystemColors.ControlDarkDark;
            gbVisualStyle.BackColor = SystemColors.ControlDarkDark;

            lvData.BackColor = SystemColors.ControlDark;

            lvData.ForeColor = SystemColors.ControlLight;

        }


        private void SetLightMode()
        {
            ColourButtons("light");
            this.BackColor = SystemColors.Control;

            gbCalculation.ForeColor = SystemColors.ControlText;
            gbLanguages.ForeColor = SystemColors.ControlText;
            gbVisualStyle.ForeColor = SystemColors.ControlText;

            gbCalculation.BackColor = SystemColors.Control;
            gbLanguages.BackColor = SystemColors.Control;
            gbVisualStyle.BackColor = SystemColors.Control;

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

        private void btnEnglish_Click(object sender, EventArgs e)
        {
            ChangeLanguage("English");
            rbLight.Checked = true;
            lightModeToolStripMenuItem.Checked = true;
            darkModeToolStripMenuItem.Checked = false;

            englishToolStripMenuItem.Checked = true;
            frenchToolStripMenuItem.Checked = false;
            germanToolStripMenuItem.Checked = false;
        }

        private void btnFrench_Click(object sender, EventArgs e)
        {
            ChangeLanguage("French");
            rbLight.Checked = true;
            lightModeToolStripMenuItem.Checked = true;
            darkModeToolStripMenuItem.Checked = false;

            frenchToolStripMenuItem.Checked = true;
            germanToolStripMenuItem.Checked = false;
            englishToolStripMenuItem.Checked = false;
        }

        private void btnGerman_Click(object sender, EventArgs e)
        {
            ChangeLanguage("German");
            rbLight.Checked = true;
            lightModeToolStripMenuItem.Checked = true;
            darkModeToolStripMenuItem.Checked = false;

            germanToolStripMenuItem.Checked = true;
            englishToolStripMenuItem.Checked = false;
            frenchToolStripMenuItem.Checked = false;
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeLanguage("English");
            rbLight.Checked = true;
            lightModeToolStripMenuItem.Checked = true;
            darkModeToolStripMenuItem.Checked = false;

            englishToolStripMenuItem.Checked = true;
            frenchToolStripMenuItem.Checked = false;
            germanToolStripMenuItem.Checked = false;
        }

        private void frenchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeLanguage("French");
            rbLight.Checked = true;
            lightModeToolStripMenuItem.Checked = true;
            darkModeToolStripMenuItem.Checked = false;

            frenchToolStripMenuItem.Checked = true;
            germanToolStripMenuItem.Checked = false;
            englishToolStripMenuItem.Checked = false;
        }

        private void germanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeLanguage("German");
            rbLight.Checked = true;
            lightModeToolStripMenuItem.Checked = true;
            darkModeToolStripMenuItem.Checked = false;

            germanToolStripMenuItem.Checked = true;
            englishToolStripMenuItem.Checked = false;
            frenchToolStripMenuItem.Checked = false;
        }

        private void FontColour()
        {
            ColorDialog dlg = new ColorDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Color selectedColour = dlg.Color;

                gbCalculation.ForeColor = selectedColour;
                gbLanguages.ForeColor = selectedColour;
                gbVisualStyle.ForeColor = selectedColour;
                lvData.ForeColor = selectedColour;
            }
        }

        private void BackgroundColour()
        {
            ColorDialog dlg = new ColorDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                Color selectedColour = dlg.Color;

                this.BackColor = selectedColour;
                gbCalculation.BackColor = selectedColour;
                gbLanguages.BackColor = selectedColour;
                gbVisualStyle.BackColor = selectedColour;
                lvData.BackColor = selectedColour;

            }
        }

        private void btnFontColour_Click(object sender, EventArgs e)
        {
            FontColour();
        }

        private void btnBackgroundColour_Click(object sender, EventArgs e)
        {
            BackgroundColour();
        }

        private void fontColourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontColour();
        }

        private void backgroundColourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackgroundColour();
        }
    }


}
