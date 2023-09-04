using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            }
            else
            {
                MessageBox.Show("Invalid input. Please enter valid numeric values.");
            }
        }
    }
}
