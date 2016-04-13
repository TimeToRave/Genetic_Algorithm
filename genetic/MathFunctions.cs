using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using info.lundin.math;
using System.Collections;

namespace genetic
{
    class MathFunctions
    {
        static public double y(Person person, string function)
        {
            ExpressionParser parser = new ExpressionParser();
            double[] vec = new double[person.Size()];
            for (int i = 0; i < vec.Length; i++)
            {
                vec[i] = person.GetGen(i);
            }
            Hashtable h = new Hashtable();

            if (vec.Length > 4)
            {
                return 0;
            }
            else
            {
                double[] tempArray = vec;
                if (vec.Length == 1)
                {
                    h.Add("x1", tempArray[0].ToString());
                }
                if (vec.Length == 2)
                {
                    h.Add("x1", tempArray[0].ToString());
                    h.Add("x2", tempArray[1].ToString());
                }
                if (vec.Length == 3)
                {
                    h.Add("x1", tempArray[0].ToString());
                    h.Add("x2", tempArray[1].ToString());
                    h.Add("x3", tempArray[2].ToString());
                }
                if (vec.Length == 4)
                {
                    h.Add("x1", tempArray[0].ToString());
                    h.Add("x2", tempArray[1].ToString());
                    h.Add("x3", tempArray[2].ToString());
                    h.Add("x4", tempArray[3].ToString());
                }
            }
            return (parser.Parse(function, h));
        }


    }
}
