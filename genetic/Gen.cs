using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace genetic
{
    class Gen
    {
        private double gen;

        //Constructors
        public Gen(double leftLimit, double rightLimit)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            //System.Threading.Thread.Sleep(20);
            gen = random.NextDouble() * (rightLimit - leftLimit) + leftLimit;
        }


        public Gen(double _gen)
        {
            gen = _gen;
        }

        public void PrintGen()
        {
            Console.WriteLine(gen + "\t");

        }


        internal double GetValue()
        {
            return gen;
        }
    }
}
