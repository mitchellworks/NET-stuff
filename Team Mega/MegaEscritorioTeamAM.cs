using System;
using System.IO;
using System.Windows.Forms;

//set up the possible wood types
enum woodMaterial
{
    oak,
    laminate,
    pine,
    maple,
    cherry,
    fir
}

namespace MegaEscritorio
{
    //set up the class so we can have desk order objects
    public class DeskOrder
    {
        public int width;
        public int length;
        public int drawers;
        public string material;
        public int rushDays;
        public int totalPrice;
    }

    //here's where the main program runs
    public class MegaProgram
    {
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());

            // before we start, give a welcome message
            Console.WriteLine("Welcome to the Mega Escritorio custom desk store.\n Press any key to continue.");
            Console.ReadLine();

            // start a new instance of the desk order class for this order
            DeskOrder deskOrder = new DeskOrder();

            // set up the rush prices array and then read the file into it
            int[] rushPrices = new int[9];
            ReadPrices(rushPrices);

            // ask for user input
            deskOrder.width = PromptMeta("Please enter the width of the desk in inches", "Width must be between 12 and 144 inches.\n", 12, 144);
            deskOrder.length = PromptMeta("\nPlease enter the length of the desk in inches: ", "Length must be between 12 and 144 inches.\n", 12, 144);
            deskOrder.drawers = PromptMeta("\nPlease enter the number of drawers between 0-7: ", "Drawers must be a number 0 - 7.\n", 0, 7);
            deskOrder.material = PromptMaterial();
            deskOrder.rushDays = PromptRush();

            // set up buckets for the evaluated prices
            int surfacePrice = 0;
            int drawerPrice = 0;
            int materialPrice = 0;
            int rushPrice = 0;

            // calculate all the prices into a total price
            CalcPrice(ref deskOrder, ref surfacePrice, ref drawerPrice, ref materialPrice, ref rushPrice, rushPrices);

            // show the price details to the user
            DisplayOrder(deskOrder, surfacePrice, drawerPrice, materialPrice, rushPrice);

            // write the price details to the text file
            WriteOrder(deskOrder, surfacePrice, drawerPrice, materialPrice, rushPrice);

            // keep the console window open in debug mode :)
            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();


        }

        public static int PromptMeta(string prompt, string eMsg, int min, int max)
        {
            // user input is a string, which we'll change to an integer within the min/max
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
        public static string PromptMaterial()
        {
            //this one only has a string, no number, so I made it a different method
            string material = "";

            do
            {
                Console.WriteLine("\nPlease enter the type of material (oak, laminate, pine, maple, cherry, or fir): ");
                try
                {
                    material = Console.ReadLine();
                    material = material.ToLower();

                    if (material != "oak" && material != "laminate" && material != "pine" && material != "maple" && material != "cherry" && material != "fir")
                        throw new Exception("Material must be oak, laminate, pine, maple, cherry, or fir.\n");
                }
                catch (Exception eMaterial)
                {
                    Console.WriteLine(eMaterial.Message);
                }
            }
            while (material != "oak" && material != "laminate" && material != "pine" && material != "maple" && material != "cherry" && material != "fir");

            return material;
        }
        public static void ReadPrices(int[] rushPrices)
        {
            try
            {
                // read in the rush prices file and make an array out of the data
                StreamReader reader = new StreamReader(@"C:\Users\mitchellworks\Documents\am_workspace\BYUI\net-stuff\NET-stuff\Team Mega\MegaE-RushOrders.txt");

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
        public static int PromptRush()
        {
            // calculate the extra rush pricing, if any. Allow the user to hit ENTER for regular pricing
            int rushDays = 0;

            do
            {
                Console.WriteLine("\nDo you need your order rushed? Please enter a deadline of 3, 5, or 7 days. \nOtherwise please hit ENTER for the standard 14 days.");
                string rushString = Console.ReadLine();
                try
                {
                    if (rushString == "")
                    {
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
        public static void CalcPrice(
            ref DeskOrder deskOrder, ref int surfacePrice, ref int drawerPrice, ref int materialPrice, ref int rushPrice, int[] rushPrices)
        {
            // take all the selections and use switches and calcs to get total price. Write this price directly to the deskOrder object (other prices are temporary so not kept there)
            surfacePrice = (((deskOrder.length * deskOrder.width) - 1000) * 5);
            if (surfacePrice < 0)
                surfacePrice = 0;

            double deskSize = (deskOrder.width * deskOrder.length);

            drawerPrice = (deskOrder.drawers * 50);

            switch (deskOrder.material)
            {
                case "oak":
                    materialPrice = 200;
                    break;
                case "laminate":
                    materialPrice = 100;
                    break;
                case "pine":
                    materialPrice = 50;
                    break;
                case "maple":
                    materialPrice = 200;
                    break;
                case "cherry":
                    materialPrice = 220;
                    break;
                case "fir":
                    materialPrice = 140;
                    break;
            }

            switch (deskOrder.rushDays)
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

            deskOrder.totalPrice = (200 + surfacePrice + drawerPrice + materialPrice + rushPrice);

        }
        public static void DisplayOrder(DeskOrder deskOrder, int surfacePrice, int drawerPrice, int materialPrice, int rushPrice)
        {
            // tell the user what we did
            Console.WriteLine("\nYour Order is: ");
            Console.WriteLine("Width: " + deskOrder.width + "\"");
            Console.WriteLine("Length: " + deskOrder.length + "\"");
            Console.WriteLine("Extra Surface Price: $" + surfacePrice + "\n");

            Console.WriteLine("Number of Drawers: " + deskOrder.drawers);
            Console.WriteLine("  Drawer Price: $" + drawerPrice + "\n");

            Console.WriteLine("Material: " + deskOrder.material);
            Console.WriteLine("  Material Price: $" + materialPrice + "\n");

            Console.WriteLine("Rush Dealine: " + deskOrder.rushDays + " days");
            Console.WriteLine("  Rush Price: $" + rushPrice + "\n");

            Console.WriteLine("Total Price: $" + deskOrder.totalPrice);

        }
        public static void WriteOrder(DeskOrder deskOrder, int surfacePrice, int drawerPrice, int materialPrice, int rushPrice)
        {
            // tell the file what we did
            StreamWriter writer;
            writer = new StreamWriter(@"C:\Users\mitchellworks\Documents\am_workspace\BYUI\net-stuff\NET-stuff\Team Mega\MegaE-OrderFile.txt");

            writer.WriteLine("{");
            writer.WriteLine("\t\"order\":");
            writer.WriteLine("\t{");
            writer.WriteLine("\t\t\"width\":\"" + deskOrder.width + " in\"");
            writer.WriteLine("\t\t\"length\":\"" + deskOrder.length + " in\"");
            writer.WriteLine("\t\t\"extraSurfacePrice\":\"$" + surfacePrice + "\"\n");

            writer.WriteLine("\t\t\"numberOfDrawers\":\"" + deskOrder.drawers + "\"");
            writer.WriteLine("\t\t\"drawerPrice\":\"$" + drawerPrice + "\"\n");

            writer.WriteLine("\t\t\"material\":\"" + deskOrder.material + "\"");
            writer.WriteLine("\t\t\"materialPrice\":\"$" + materialPrice + "\"\n");

            writer.WriteLine("\t\t\"rushDeadline\":" + deskOrder.rushDays + "days\"");
            writer.WriteLine("\t\t\"rushPrice\":\"$" + rushPrice + "\"\n");

            writer.WriteLine("\t\t\"totalPrice\":\"$" + deskOrder.totalPrice + "\"");
            writer.WriteLine("\t}");
            writer.WriteLine("}");
            writer.Close();
        }

    }
}