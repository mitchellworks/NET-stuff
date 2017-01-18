using System;
using System.IO;

namespace MegaEscritorio
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Welcome to Mega Escritorio ordering system.\n Press any key to continue.");
            Console.ReadLine();

            int[] rushPrices = new int[9];
            ReadPrices(rushPrices);

            int width = PromptMeta("Please enter the width of the desk in inches","Width must be between 12 and 144 inches.\n", 12, 144);
            int length = PromptMeta("\nPlease enter the length of the desk in inches: ", "Length must be between 12 and 144 inches.\n", 12, 144);
            int drawers = PromptMeta("\nPlease enter the number of drawers between 0-7: ", "Drawers must be a number 0 - 7.\n",0,7);
            string material = PromptMaterial();
            int rushDays = PromptRush();

            int surfacePrice = 0;
            int drawerPrice = 0;
            int materialPrice = 0;
            int rushPrice = 0;

            int totalPrice = CalcPrice(width, length, drawers, material, rushDays, ref surfacePrice, ref drawerPrice, ref materialPrice, ref rushPrice, rushPrices);

            DisplayOrder(width, length, drawers, material, rushDays, surfacePrice, drawerPrice, materialPrice, rushPrice, totalPrice);

            WriteOrder(width, length, surfacePrice, drawers, drawerPrice, material, materialPrice, rushDays, rushPrice, totalPrice);

            // Keep the console window open in debug mode.
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();


        }


        static int PromptMeta(string prompt, string eMsg, int min, int max)
        {
            int theNumber = 0;
            string theString;

            do
            {
                Console.WriteLine(prompt);
                theString = Console.ReadLine();
                try
                {
                    theNumber = int.Parse(theString);

                    if (theNumber < min || theNumber > max)
                        throw new Exception(eMsg);
                }
                catch (Exception eDeskW)
                {
                    Console.Write(eDeskW.Message);
                }
            }
            while (theNumber < min || theNumber > max);

            return theNumber;

        }
        static string PromptMaterial()
        {
            string material = "";

            do
            {
                Console.WriteLine("\nPlease enter the type of material (Laminate, Oak or Pine): ");
                try
                {
                    material = Console.ReadLine();
                    material = material.ToLower();

                    if (material != "laminate" && material != "oak" && material != "pine")
                        throw new Exception("Material must be Laminate, Oak or Pine.\n");
                }
                catch (Exception eMaterial)
                {
                    Console.WriteLine(eMaterial.Message);
                }
            }
            while (material != "laminate" && material != "oak" && material != "pine");

            return material;
        }

        static void ReadPrices(int[] rushPrices)
        {
            try
            { 
            StreamReader reader = new StreamReader(@"C:\Users\mitchellworks\Documents\am_workspace\BYUI\net-stuff\NET-stuff\Mega Escritorio\MegaE-RushOrders.txt");

            for (int i = 0; i < 9; i++)
            {
                string priceString = reader.ReadLine();
                string[] priceInfo = priceString.Split(',');
                rushPrices[i] = int.Parse(priceInfo[2]);
            }
            reader.Close();
            }
            catch (Exception eRead)
            {
                Console.WriteLine(eRead.Message);
            }
        }

        static int PromptRush()
        {
            int rushDays = 0;

            do
            {
                Console.WriteLine("\nDo you need your ordere rushed? Please enter a deadline of 3, 5, or 7 days. \nOtherwise please hit ENTER for the standard 14 days.");
                string rushString = Console.ReadLine();
                try
                {
                    if (rushString == "") {
                        rushString = "14";
                    }
                    rushDays = int.Parse(rushString);
                    if (rushDays != 3 && rushDays != 5 && rushDays != 7 && rushDays != 14)
                        throw new Exception("Please enter either 3, 5, or 7, or hit ENTER\n");
                }
                catch (Exception eRush)
                {
                    Console.WriteLine(eRush.Message);
                }
            }
            while (rushDays != 3 && rushDays != 5 && rushDays != 7 && rushDays != 14);

            return rushDays;
        }

        static int CalcPrice(int width, int length, int drawers, string material, int rushDays, ref int surfacePrice, ref int drawerPrice, ref int materialPrice, ref int rushPrice, int [] rushPrices)
        {
            surfacePrice = (((length * width) - 1000) * 5);
            if (surfacePrice < 0)
                surfacePrice = 0;

            double deskSize = (width * length); 

            drawerPrice = (drawers * 50);

            switch (material)
            {
                case "Oak":
                    materialPrice = 200;
                    break;
                case "Laminate":
                    materialPrice = 100;
                    break;
                case "Pine":
                    materialPrice = 50;
                    break;
            }

            switch (rushDays)
            {
                case 3:
                    if (deskSize < 1000)
                        rushPrice = rushPrices[0];
                    else if (deskSize > 1000 && deskSize < 2000)
                        rushPrice = rushPrices[1];
                    else if (deskSize >= 2000)
                        rushPrice = rushPrices[2];
                    break;
                case 5:
                    if (deskSize < 1000)
                        rushPrice = rushPrices[3];
                    else if (deskSize > 1000 && deskSize < 2000)
                        rushPrice = rushPrices[4];
                    else if (deskSize >= 2000)
                        rushPrice = rushPrices[5];
                    break;
                case 7:
                    if (deskSize < 1000)
                        rushPrice = rushPrices[6];
                    else if (deskSize > 1000 && deskSize < 2000)
                        rushPrice = rushPrices[7];
                    else if (deskSize >= 2000)
                        rushPrice = rushPrices[8];
                    break;
                default:
                    rushPrice = 0;
                    break;
                
            }

            int totalPrice = (200 + surfacePrice + drawerPrice + materialPrice + rushPrice);

            return totalPrice;

        }

        static void DisplayOrder(int width, int length, int drawers, string material, int rushDays, int surfacePrice, int drawerPrice, int materialPrice, int rushPrice, int totalPrice)
        {
            Console.WriteLine("\nYour Order is: ");
            Console.WriteLine("Width: " + width + "\"");
            Console.WriteLine("Length: " + length + "\"");
            Console.WriteLine("Extra Surface Price: $" + surfacePrice + "\n");

            Console.WriteLine("Number of Drawers: " + drawers);
            Console.WriteLine("  Drawer Price: $" + drawerPrice + "\n");

            Console.WriteLine("Material: " + material);
            Console.WriteLine("  Material Price: $" + materialPrice + "\n");

            Console.WriteLine("Rush Dealine: " + rushDays + " days");
            Console.WriteLine("  Rush Price: $" + rushPrice + "\n");

            Console.WriteLine("Total Price: $" + totalPrice);

        }

         static void WriteOrder(int width, int length, int surfacePrice, int drawers, int drawerPrice, string material, int materialPrice, int rushDays, int rushPrice, int totalPrice)
        {
            StreamWriter writer;
            writer = new StreamWriter(@"C:\Users\mitchellworks\Documents\am_workspace\BYUI\net-stuff\NET-stuff\Mega Escritorio\MegaE-OrderFile.txt");

            writer.WriteLine("{");
            writer.WriteLine("\t\"order\":");
            writer.WriteLine("\t{");
            writer.WriteLine("\t\t\"width\":\"" + width + " in\"");
            writer.WriteLine("\t\t\"length\":\"" + length + " in\"");
            writer.WriteLine("\t\t\"extraSurfacePrice\":\"$" + surfacePrice + "\"\n");

            writer.WriteLine("\t\t\"numberOfDrawers\":\"" + drawers + "\"");
            writer.WriteLine("\t\t\"drawerPrice\":\"$" + drawerPrice + "\"\n");

            writer.WriteLine("\t\t\"material\":\"" + material + "\"");
            writer.WriteLine("\t\t\"materialPrice\":\"$" + materialPrice + "\"\n");

            writer.WriteLine("\t\t\"rushDeadline\":" + rushDays + "days\"");
            writer.WriteLine("\t\t\"rushPrice\":\"$" + rushPrice + "\"\n");

            writer.WriteLine("\t\t\"totalPrice\":\"$" + totalPrice + "\"");
            writer.WriteLine("\t}");
            writer.WriteLine("}");
            writer.Close();
        }

    }
}