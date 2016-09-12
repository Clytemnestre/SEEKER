using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Seeker
{
    class Globals
    {
        //public static int JobSeekerID;
        public static JobSeeker CurrentJobSeeker = new JobSeeker();
        
        public static Employer CurrentEmployer = new Employer();

        public static Offer CurrentOffer = new Offer();
    }
}
