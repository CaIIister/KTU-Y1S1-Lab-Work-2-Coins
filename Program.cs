using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace L2_2_Coins_
{
    class Coin
    {
        private string country;
        private int denomination;
        private double weight;
        public Coin()
        {
            country = "Ukraine";
            denomination = 5;
            weight = 0;
        }
        public Coin(string country, int denomination, double weight)
        {
            this.country = country;
            this.denomination = denomination;
            this.weight = weight;
        }
        public string getCountry() { return country; }
        public int getDenominator() { return denomination; }
        public double getWeight() { return weight; }
        public void setCountry(string country) { this.country = country; }
        public void setDenomination(int denomination) { this.denomination = denomination; }
        public void setWeight(double weight) { this.weight = weight; }
    }
    internal class Program
    {
        const string CFd1 = "L2_21.txt";
        const string CFd2 = "L2_22.txt";
        const string CFresult = "result.txt";
        const int CmaxSize = 100;
        /// <summary>
        /// method for reading array of objects of class Coin from text file
        /// </summary>
        /// <param name="fv"></param>
        /// <param name="arr"></param>
        /// <param name="n"></param>
        /// <param name="nameStudent"></param>
        static void Read(string fv, Coin[] arr, out int n, out string nameStudent)
        {
            string countryName;
            int denominationCoin;
            double weightCoin;
            using (StreamReader reader = new StreamReader(fv))
            {
                string line;
                line = reader.ReadLine();
                nameStudent = line;
                string[] lines = File.ReadAllLines(fv);
                lines=lines.Skip(1).ToArray();
                int i = 0;
                foreach (string lineC in lines)
                {
                    string[] parts = lineC.Split(';');
                    countryName = parts[0];
                    denominationCoin = int.Parse(parts[1]);
                    weightCoin=double.Parse(parts[2]);
                    arr[i] = new Coin(countryName, denominationCoin, weightCoin);
                    i++;
                }
                n = i;
            }
        }
        /// <summary>
        /// method for printing array of objects of class Coin to text file
        /// </summary>
        /// <param name="file"></param>
        /// <param name="nameStudent"></param>
        /// <param name="arr"></param>
        /// <param name="n"></param>
        static void printData(string file,string nameStudent, Coin[] arr, int n)
        {
            using (StreamWriter writer=new StreamWriter(file,true))
            {
                writer.WriteLine("{0,18}", nameStudent);
                writer.WriteLine(new string('-', 33));
                writer.WriteLine("|  Country  |Denomination|Weight|");
                writer.WriteLine(new string('-', 33));
                for (int i = 0; i < n; i++)
                {
                    writer.WriteLine("|{0,11}|{1,12}|{2,6}|", arr[i].getCountry(), arr[i].getDenominator(), arr[i].getWeight());
                }
                writer.WriteLine(new string('-', 33));
                writer.WriteLine();
            }
        }
        /// <summary>
        /// method for finding the heaviest coin in array of class Object
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        static Coin heaviest(Coin[] arr, int n)
        {
            Coin heaviest = new Coin();
            for(int i=0;i<n;i++)
            {
                if (arr[i].getWeight() >= heaviest.getWeight())
                    heaviest = arr[i];
            }
            return heaviest;
        }
        /// <summary>
        /// method for finding the total monetary value of array
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        static double totalValue(Coin[] arr, int n)
        {
            double sum = 0;
            for(int i=0;i<n;i++)
            {
                sum += arr[i].getDenominator();
            }
            return sum;
        }
        /// <summary>
        /// method that creates collection of two arrays
        /// and each object of collection has unique country and denomination
        /// </summary>
        /// <param name="arr1"></param>
        /// <param name="n1"></param>
        /// <param name="arr2"></param>
        /// <param name="n2"></param>
        /// <param name="nacc"></param>
        static void collectionOfDiffTreasures(Coin[] arr1,int n1, Coin[] arr2, int n2, out int nacc)
        {
            bool check = false;
            nacc = n1;
            for (int i=0;i<n2;i++)
            {
                for(int j=0;j<n1;j++)
                {
                    if ((arr2[i].getCountry() == arr1[j].getCountry()) && (arr2[i].getDenominator() == arr1[j].getDenominator()))
                        check = true;
                }
                if(!check)
                    arr1[nacc++]=arr2[i];
            }
        }
        static void Main(string[] args)
        {
            Coin[] B1 = new Coin[CmaxSize];
            Coin[] B2 = new Coin[CmaxSize];
            int n1,n2;
            string studentName1,studentName2;

            if (File.Exists(CFresult))
                File.Delete(CFresult);

            Read(CFd1,B1,out n1, out studentName1);

            printData(CFresult,studentName1, B1, n1);

            Read(CFd2,B2,out n2, out studentName2);

            printData(CFresult,studentName2, B2, n2);

            Coin heaviest1=heaviest(B1, n1);
            Coin heaviest2=heaviest(B2 ,n2);

            double ipart1, ipart2, dpart1, dpart2;
            double total1=totalValue(B1, n1);
            double total2=totalValue(B2, n2);
            using (StreamWriter writer=new StreamWriter(CFresult, true))
            {
                writer.WriteLine("The heaviest coin");
                writer.WriteLine(new String('-', 21));
                writer.WriteLine("|{0,6}|{1,5}|{2,5}|", heaviest1.getCountry(), heaviest1.getDenominator(), heaviest1.getWeight());
                writer.WriteLine(new String('-', 21));
                writer.WriteLine();

                writer.WriteLine("The heaviest coin");
                writer.WriteLine(new String('-', 21));
                writer.WriteLine("|{0,6}|{1,5}|{2,5}|", heaviest2.getCountry(), heaviest2.getDenominator(), heaviest2.getWeight());
                writer.WriteLine(new String('-', 21));
                writer.WriteLine();

                if (total1 >= 100)
                {
                    total1 = total1 / 100;
                    ipart1 = Math.Truncate(total1);
                    dpart1 = Math.Round(total1 - Math.Truncate(total1), 2) * 100;
                    writer.WriteLine("The total monetary value of {0}'s treasure = {1} euros and {2} cents", studentName1, ipart1, dpart1);
                }
                else if (total1 < 100)
                {
                    writer.WriteLine("The total monetary value of {0}'s treasure = {1} cents", studentName1, total1);
                }


                if (total2 >= 100)
                {
                    total2 = total2 / 100;
                    ipart2 = Math.Truncate(total2);
                    dpart2 = Math.Round(total2 - Math.Truncate(total2), 2) * 100;
                    writer.WriteLine("The total monetary value of {0}'s treasure = {1} euros and {2} cents", studentName2, ipart2, dpart2);
                }
                else if (total2 < 100)
                {
                    writer.WriteLine("The total monetary value of {0}'s treasure = {1} cents", studentName2, total2);
                }

                if (total1 == total2)
                {
                    writer.WriteLine("Both {0} and {1} have the highest monetary value of treasure", studentName1, studentName2);
                }
                else if (total2 > total1)
                {
                    writer.WriteLine("{0} has the highest monetary value of treasure", studentName2);
                }
                else
                {
                    writer.WriteLine("\n{0} has the highest monetary value of treasure", studentName1);
                }


            }

            int n3;
            collectionOfDiffTreasures(B1, n1, B2, n2,out n3);
            printData(CFresult,"\nThe collection of both", B1, n3);

        }
    }
}
