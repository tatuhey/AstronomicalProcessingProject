using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AstroMath;

namespace AstronomicalProcessingProject
{
    public class AstroServer : IAstroContract
    {

        // 2.	Create the server file called “AstroServer.cs” which implements the IAstroContract.
        //      Add a new instance of the class library method and then create four methods with
        //      the suitable input parameters and return types.

        public double StarVelocity(double ObsWave, double RestWave)
        {
            double result = AstroMath.AstroMath.StarVelocity(ObsWave, RestWave);
            return result;
        }

        public double StarDistance(double Parallax)
        {
            double result = AstroMath.AstroMath.StarDistance(Parallax);
            return result;
        }

        public double TempInKelvin(double Cel)
        {
            double result = AstroMath.AstroMath.TempInKelvin(Cel);
            return result;
        }

        public double EventHorizon(double mass)
        {
            double result = AstroMath.AstroMath.EventHorizon(mass);
            return result;
        }
    }
}
