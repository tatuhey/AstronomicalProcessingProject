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
    }
}
