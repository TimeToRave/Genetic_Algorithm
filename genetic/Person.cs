using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace genetic
{
    class Person
    {
        private List<Gen> person;

        public Person(Gen[] _gens)
        {
            for (int i = 0; i < _gens.Length; i++)
            {
                person.Add(_gens[i]);
            }
        }

        public Person(List<Gen> _gens)
        {
            person = new List<Gen>();
            for (int i = 0; i < _gens.Count(); i++)
            {
                person.Add(_gens.ElementAt(i));
            }
        }

        public Person(int personSize, double leftLimit, double rightLimit)
        {
            person = new List<Gen>();
            for (int i = 0; i < personSize; i++)
            {
                person.Add(new Gen(leftLimit, rightLimit));
            }
        }

        public Person()
        {
            person = new List<Gen>();
        }

        public void PrintPerson()
        {
            Console.WriteLine("Person: ");
            for (int i = 0; i < person.Count; i++)
            {
                person.ElementAt(i).PrintGen();
            }
        }

        public double GetGen(int index)
        {
            return person.ElementAt(index).GetValue();
        }

        public void SetGen(int index, double value)
        {
            person.RemoveAt(index);
            person.Insert(index, new Gen(value));
        }

        public int Size()
        {
            return person.Count();
        }
    }
}
