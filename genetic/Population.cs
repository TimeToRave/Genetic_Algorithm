using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace genetic
{
    class Population
    {
        private List<Person> personList;
        private List<double> fitnessList;

        public Population(Person [] _personList)
        {
            personList = new List<Person>();
            for (int i = 0; i < _personList.Count(); i++)
            {
                personList.Add(_personList[i]);
            }
        }

        public void Mix()
        {
            Random RND = new Random();
            for (int i = 0; i < personList.Count; i++)
            {
                Person tmp = personList.ElementAt(i);
                personList.RemoveAt(i);
                personList.Insert(RND.Next(personList.Count), tmp);
            }
        }
        
        public Population(Population population)
        {
            personList = new List<Person>();
            for(int i = 0; i < population.Size(); i++)
            {
                personList.Add(population.GetPerson(i));
            }
        }

        
        public Population()
        {
            personList = new List<Person>();
        }

        public Population(List<Person> _personList)
        {
            for (int i = 0; i < _personList.Count(); i++)
            {
                personList.Add(_personList.ElementAt(i));
            }
        }

        //Инициализация популяции
        public Population (int populationSize, int personSize, double leftLimit, double rightLimit)
        {
            personList = new List<Person>();
            for (int i = 0; i < populationSize; i++)
            {
                personList.Add(new Person(personSize, leftLimit, rightLimit));
            }
        }

        public void Fitness(string function)
        {
            fitnessList = new List<double>();
            for(int i=0; i < personList.Count; i++)
            {
                fitnessList.Add(MathFunctions.y(personList.ElementAt(i), function));
            }
        }

        public double AverageFitness()
        {
            double avg = 0;
            for (int i = 0; i < personList.Count; i++)
            {
                avg += fitnessList.ElementAt(i);
            }
            avg /= personList.Count;
            return avg;
        }
        public int Size()
        {
            return personList.Count;
        }
        public double GetFitness(int index)
        {
            return fitnessList.ElementAt(index);
        }

        public void Add(Person person)
        {
            personList.Add(person);
        }

        public int FindBest()
        {
            double bestFitness = double.MaxValue;
            int bestIndex = 0;
            for (int i = 0; i < personList.Count; i++)
            {
                if (fitnessList.ElementAt(i) < bestFitness)
                {
                    bestFitness = fitnessList.ElementAt(i);
                    bestIndex = i;
                }
            }
            return bestIndex;
        }

        public Person GetPerson(int index)
        {
            return personList.ElementAt(index);
        }

        public void Clear()
        {
            personList.Clear();
        }

        public Population Tournament(int count, string function)
        {
            Population bestPopulation = new Population();

            Random random = new Random(Guid.NewGuid().GetHashCode());
            int[] indexes = new int[count];
            
            Population tournamentPopulation = new Population();
            for (int i = 0; i < personList.Count / count; i++)
            {
                //Формирование списка индексов для турнира
                for (int i1 = 0; i1 < count; i1++)
                {
                    indexes[i1] = random.Next(personList.Count);
                    //System.Threading.Thread.Sleep(20);
                }
                //Выбираем особей на "поединок"
                for(int j = 0; j < count; j++)
                {
                    tournamentPopulation.Add(personList.ElementAt(indexes[j]));
                }
                tournamentPopulation.Fitness(function);
                int winnerIndex = tournamentPopulation.FindBest();
                bestPopulation.Add(tournamentPopulation.GetPerson(winnerIndex));
                tournamentPopulation.Clear();
            }

            return bestPopulation;
        }

        public Population Tournament2(int count, string function)
        {
            Population bestPopulation = new Population();

            Population tournamentPopulation = new Population();
            for (int i = 0; i < personList.Count / count; i++)
            {
                
                //Выбираем особей на "поединок"
                for (int j = 0; j < count; j++)
                {
                    tournamentPopulation.Add(personList.ElementAt(i+j*count));
                }
                tournamentPopulation.Fitness(function);
                int winnerIndex = tournamentPopulation.FindBest();
                bestPopulation.Add(tournamentPopulation.GetPerson(winnerIndex));
                tournamentPopulation.Clear();
            }

            return bestPopulation;
        }

        public Population Crossing(Person parent1, Person parent2)
        {
            Population tempPopulation = new Population();
            if (parent1.Size() == 2)
            {
                List<Gen> tempPerson = new List<Gen>();
                tempPerson.Add(new Gen(parent1.GetGen(0)));
                tempPerson.Add(new Gen(parent2.GetGen(1)));
                tempPopulation.Add(new Person(tempPerson));
                tempPerson.Clear();
                tempPerson.Add(new Gen(parent2.GetGen(0)));
                tempPerson.Add(new Gen(parent1.GetGen(1)));
                tempPopulation.Add(new Person(tempPerson));
                return tempPopulation;
            }
            else
            {
                return new Population();
            }
            
        }
        public Population Crossover(int sizeOfPopualtion, double crossoverProbability)
        {
            Population newPopulation = new Population();
            Random random = new Random(Guid.NewGuid().GetHashCode());
            while (newPopulation.Size() < sizeOfPopualtion)
            {
                int index1 = random.Next(personList.Count);
                //System.Threading.Thread.Sleep(20);
                int index2 = random.Next(personList.Count);
                //System.Threading.Thread.Sleep(20);
                if (crossoverProbability > random.NextDouble())
                {
                    Population tempPopulation = new Population(Crossing(personList.ElementAt(index1), personList.ElementAt(index2)));
                    for(int i = 0; i < tempPopulation.Size(); i ++)
                    {
                        newPopulation.Add(tempPopulation.GetPerson(i));
                    }
                }                   

            }
            return newPopulation;
        }

        public void Mutation(double mutationProbability)
        {
            Random random = new Random(Guid.NewGuid().GetHashCode());
            for(int i =0; i < personList.Count; i++)
            {
                for (int j = 0; j < personList.ElementAt(0).Size(); j++)
                {
                    double chance = random.NextDouble();
                    //System.Threading.Thread.Sleep(20);
                    if (mutationProbability > chance)
                    {
                        personList.ElementAt(i).SetGen(j, personList.ElementAt(i).GetGen(j) + random.NextDouble()*0.5);
                        //System.Threading.Thread.Sleep(20);
                    }
                }
            }
        }

       
    }
}
