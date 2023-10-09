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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;

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
            englishToolStripMenuItem.Checked = true;
        }

        private IAstroContract channel;

        private void ConnectToServer()
        {
            string address = "net.pipe://localhost/pipemynumbers";
            NetNamedPipeBinding binding = new NetNamedPipeBinding(NetNamedPipeSecurityMode.None);
            EndpointAddress ep = new EndpointAddress(address);
            channel = ChannelFactory<IAstroContract>.CreateChannel(binding, ep);
        }

        #region Calculations
        // 2.	Create a form with suitable components for UI,
        // a.Series of textboxes for large numeric data,
        // b.A listview/datagrid for display of processed information from the server,

        // c.Button(s) to initiate an event and send/receive data.
        private void AddToListView(int column, string result)
        {
            // iterate through existing listview items
            bool added = false;
            foreach (ListViewItem item in lvData.Items)
            {
                // check if the specified column is empty
                if (string.IsNullOrEmpty(item.SubItems[column - 1].Text))
                {
                    // add result to the empty column
                    item.SubItems[column - 1].Text = result;
                    added = true;
                    break;
                }
            }

            // if no empty slot was found in the specified column, create a new row
            if (!added)
            {
                ListViewItem item = new ListViewItem(new string[4]);

                // add the result to the column in the new row
                item.SubItems[column - 1].Text = result;
                
                // add new row to the listview
                lvData.Items.Add(item);
            }
        }

        private void btnVelocity_Click(object sender, EventArgs e)
        {
            // Variables to store input values
            double obs;
            double rest;

            try
            {
                // Check if the input values can be parsed as doubles
                if (double.TryParse(tbObsWave.Text, out obs) && double.TryParse(tbRestWave.Text, out rest))
                {
                    // Call a method to calculate star velocity and add it to the ListView
                    double starvelocity = channel.StarVelocity(obs, rest);
                    AddToListView(1, starvelocity.ToString() + " m/s");
                }
                else
                {
                    // Show a warning message if input parsing fails
                    MessageBox.Show(warningText, warningHeader, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions and show an error message if something goes wrong
                MessageBox.Show($"{errorText} \n{ex.Message}", errorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDistance_Click(object sender, EventArgs e)
        {
            double par;

            try
            {
                if (double.TryParse(tbParallaxAngle.Text, out par))
                {
                    double distance = channel.StarDistance(par);
                    AddToListView(2, distance.ToString() + " parsecs");
                }
                else
                {
                    MessageBox.Show(warningText, warningHeader, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{errorText} \n{ex.Message}", errorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnKelvin_Click(object sender, EventArgs e)
        {
            double cel;

            try
            {
                if (double.TryParse(tbCelcius.Text, out cel))
                {
                    double kelvin = channel.TempInKelvin(cel);
                    AddToListView(3, kelvin.ToString() + " K");
                }
                else
                {
                    MessageBox.Show(warningText, warningHeader, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{errorText} \n{ex.Message}", errorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRadius_Click(object sender, EventArgs e)
        {
            double mass;
            try
            {
                if (double.TryParse(tbMassBlackhole.Text, out mass))
                {
                    double rad = Math.Round(channel.EventHorizon(mass), 2);
                    AddToListView(4, rad.ToString("E") + " meter");
                }
                else
                {
                    MessageBox.Show(warningText, warningHeader, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{errorText} \n{ex.Message}", errorHeader, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        #endregion

        #region Languages
        // 3.	Menu/Button option(s) to change the language and layout for the three different countries.

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
            // to reset the program so that the translation shows.
            Controls.Clear();
            InitializeComponent();
        }

        // if a button is pressed, corresponding menubar is checked. Since the program has to reset, light mode (default) is checked
        // to ensure continuity of the flow of the program.

        // methods to reduce repetition
        private void LanguageMenubar(string language)
        {
            switch (language)
            {
                case "English":
                    englishToolStripMenuItem.Checked = true;
                    frenchToolStripMenuItem.Checked = false;
                    germanToolStripMenuItem.Checked = false;
                    break;
                case "French":
                    frenchToolStripMenuItem.Checked = true;
                    germanToolStripMenuItem.Checked = false;
                    englishToolStripMenuItem.Checked = false;
                    break;
                case "German":
                    germanToolStripMenuItem.Checked = true;
                    englishToolStripMenuItem.Checked = false;
                    frenchToolStripMenuItem.Checked = false;
                    break;
            }
        }
        private void DefaultReset()
        {
            rbLight.Checked = true;
            lightModeToolStripMenuItem.Checked = true;
            darkModeToolStripMenuItem.Checked = false;
        }

        private void btnEnglish_Click(object sender, EventArgs e)
        {
            ChangeLanguage("English");
            DefaultReset();
            LanguageMenubar("English");
        }

        private void btnFrench_Click(object sender, EventArgs e)
        {
            ChangeLanguage("French");
            DefaultReset();
            LanguageMenubar("French");
        }

        private void btnGerman_Click(object sender, EventArgs e)
        {
            ChangeLanguage("German");
            DefaultReset();
            LanguageMenubar("German");
        }

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeLanguage("English");
            DefaultReset();
            LanguageMenubar("English");
        }

        private void frenchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeLanguage("French");
            DefaultReset();
            LanguageMenubar("French");
        }

        private void germanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ChangeLanguage("German");
            DefaultReset();
            LanguageMenubar("German");
        }
        #endregion

        #region Light/dark modes
        // 4.	Menu option to change the form’s style (colours and visual appearance).
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

            statusStrip.BackColor = SystemColors.ControlDark;
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

            statusStrip.BackColor = SystemColors.Window;
        }

        // seperate method because group box does not change the buttons' colour
        private void ColourButtons(string colour)
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
                    btnButtonColour.BackColor = SystemColors.Control;
                    break;
                case "dark":
                    btnBackgroundColour.BackColor = SystemColors.ControlDark;
                    btnDistance.BackColor = SystemColors.ControlDark;
                    btnFontColour.BackColor = SystemColors.ControlDark;
                    btnKelvin.BackColor = SystemColors.ControlDark;
                    btnRadius.BackColor = SystemColors.ControlDark;
                    btnVelocity.BackColor = SystemColors.ControlDark;
                    btnButtonColour.BackColor= SystemColors.ControlDark;
                    break;
            }
        }

        // if radio buttons used, the menubar is checked too
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

        // if menubar is used, the radiobutton is checked too
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
        #endregion

        #region Custom colours
        // 5.	Menu/Button option to select a custom background colour from a colour palette (Color Dialogbox)
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
                statusStrip.BackColor = selectedColour;
            }
        }

        private void ButtonColour()
        {
            ColorDialog dlg = new ColorDialog();

            if (dlg.ShowDialog() == DialogResult.OK )
            {
                Color selectedColour = dlg.Color;

                btnBackgroundColour.BackColor = selectedColour;
                btnDistance.BackColor = selectedColour;
                btnFontColour.BackColor = selectedColour;
                btnKelvin.BackColor = selectedColour;
                btnRadius.BackColor = selectedColour;
                btnVelocity.BackColor = selectedColour;
                btnButtonColour.BackColor = selectedColour;
            }
        }

        // buttons and menubars
        private void btnFontColour_Click(object sender, EventArgs e)
        {
            FontColour();
        }

        private void btnBackgroundColour_Click(object sender, EventArgs e)
        {
            BackgroundColour();
        }

        private void btnButtonColour_Click(object sender, EventArgs e)
        {
            ButtonColour();
        }

        private void fontColourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FontColour();
        }

        private void backgroundColourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BackgroundColour();
        }

        private void buttonColourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ButtonColour();
        }
        #endregion

        #region Error and Warning Messages - experimental

        string errorText = "Error when processing data.";
        string errorHeader = "Error";
        string warningText = "Invalid input. Please enter valid numeric values.";
        string warningHeader = "Warning";



        #endregion

    }
}
