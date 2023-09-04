using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace AstronomicalProcessingProject
{
    [ServiceContract]
    internal interface IAstroContract
    {
        // Server Application
        // 1.	Create the ServiceContract file called “IAstroContract.cs” which will require an Interface
        //      that references the AstroMath.DLL and four OperationContract (one for each calculation).

        [OperationContract]
        double StarVelocity(double ObsWave, double RestWave);

        [OperationContract]
        double StarDistance(double Parallax);

        [OperationContract]
        double TempInKelvin(double Celcius);

        [OperationContract]
        double EventHorizon(double BlackholeMass);

    }
}
