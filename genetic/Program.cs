using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace genetic
{
    class Program
    {
        static void Main(string[] args)
        {
            int choise;
            Console.Write("Введите номер функции от 1 до ");
            choise = Convert.ToInt32(Console.ReadLine());

            string function;
            double leftLimit;
            double rightLimit;
            int maxPopulation;
            double crossoverChance;
            double mutationChance;
            double inversionChance;

            maxPopulation = 100;
            crossoverChance = 0.7;
            mutationChance = 0.1;
            inversionChance = 0.05;
            
            switch (choise)
            {
                case 1: 
                        function = "100*(x2-x1^2)^2+(1-x1)^2";
                        leftLimit = -10;
                        rightLimit = 10;

                        break;
                case 2:
                        function = "(x1-1)^2+(x2-3)^2+4*(x3+5)";
                        leftLimit = -10;
                        rightLimit = 10;

                        break;
                case 3:
                        function = "8*x1^2+4*x1*x2+5*x2^2";
                        leftLimit = -5.12;
                        rightLimit = 5.12;

                        break;
                case 4:
                        function = "4*(x1-5)^2+(x2-6)^2";
                        leftLimit = 0;
                        rightLimit = 10;

                        break;
                default:
                        return;

            }
           

            Population population = new Population(maxPopulation, 2, leftLimit, rightLimit);
            for (int i = 0; i < 100; i++)
            {
                population.Fitness(function);
                population.Mix();
                Population bestPopulation = new Population(population.Tournament2(2, function));
                bestPopulation.Fitness(function);
                //Console.WriteLine("Средний фитнес по общей популяции " + population.AverageFitness().ToString());
                Console.WriteLine(population.AverageFitness().ToString());
                //Console.WriteLine("Средний фитнес по отобранной популяции " + bestPopulation.AverageFitness().ToString());
                
                Population childPopulation = new Population(bestPopulation.Crossover(maxPopulation, crossoverChance));
                childPopulation.Mutation(mutationChance);
                population = new Population(childPopulation);
            }
            population.Fitness(function);
            Person bestOfTheBest = population.GetPerson(population.FindBest());
            bestOfTheBest.PrintPerson();
            Console.ReadKey();
        }
    }
}
