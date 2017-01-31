using System;
using System.IO;

enum woodMaterial
{
    Oak,
    Laminate,
    Pine,
    Maple,
    Cherry,
    Aspen,
}

struct DeskOrder
{
    public int width;
    public int length;
    public int drawers;
    public string mat;
    public int time;
    public int totalPrice;
}
namespace MegaEscritorio
{
    class MegaDeskOrder
    {

        /**************************************************************************
         * This method prompts the user for the width of the desk.
         * ***********************************************************************/
        static void promptDeskWidth(ref DeskOrder deskOrder)
        {
            string widthString;
            do
            {
                Console.WriteLine("Please enter the width of the desk in inches: ");
                widthString = Console.ReadLine();
                try
                {
                    deskOrder.width = int.Parse(widthString);

                    if (deskOrder.width <= 0)
                        throw new Exception("Width cannot be less than 1 inch.\n");
                }
                catch (Exception eDeskW)
                {
                    Console.Write(eDeskW.Message);
                }
            }
            while (deskOrder.width <= 0);

            
        }

        /**************************************************************************
         * This method prompts the user for the length of the desk.
         * ***********************************************************************/
        static void promptDeskLength(ref DeskOrder deskOrder)
        {
            string lengthString;
            do
            {
                Console.WriteLine("\nPlease enter the length of the desk in inches: ");
                lengthString = Console.ReadLine();
                try
                {
                    deskOrder.length = int.Parse(lengthString);

                    if (deskOrder.length <= 0)
                        throw new Exception("Length cannot be less than 1 inch.\n");
                }
                catch (Exception eDeskL)
                {
                    Console.Write(eDeskL.Message);
                }
            }
            while (deskOrder.length <= 0);

        }
        /**************************************************************************
         * This method prompts the user for the number of drawers
         * ***********************************************************************/
        static void promptDrawers(ref DeskOrder deskOrder)
        {
            string drawerString;
            do
            {
                Console.WriteLine("\nPlease enter the number of drawers between 0-7: ");
                drawerString = Console.ReadLine();
                try
                {
                    deskOrder.drawers = int.Parse(drawerString);

                    if (deskOrder.drawers > 7 || deskOrder.drawers < 0)
                        throw new Exception("Drawers must be a number 0 - 7.\n");
                }
                catch (Exception eDrawer)
                {
                    Console.WriteLine(eDrawer.Message);
                }

            }

            while (deskOrder.drawers > 7 || deskOrder.drawers < 0);
            
        }

        /**************************************************************************
         * This method prompts the user for the type of material.
         * ***********************************************************************/
        static void promptMaterial(ref DeskOrder deskOrder)
        {
            do
            {
                Console.WriteLine("\nPlease enter the type of material from the following: \nLaminate \nOak \nPine \nMaple \nCherry \nAspen \n ");
                try
                {
                    deskOrder.mat = Console.ReadLine();

                    if (deskOrder.mat != "Laminate" && deskOrder.mat != "Oak" && deskOrder.mat != "Pine" && deskOrder.mat != "Maple" && deskOrder.mat != "Cherry" && deskOrder.mat != "Aspen")
                        throw new Exception("Material must be one of the above stated.\n");
                }
                catch (Exception eMat)
                {
                    Console.WriteLine(eMat.Message);
                }
            }
            while (deskOrder.mat != "Laminate" && deskOrder.mat != "Oak" && deskOrder.mat != "Pine" && deskOrder.mat != "Maple" && deskOrder.mat != "Cherry" && deskOrder.mat != "Aspen");

        }

        /**************************************************************************
         * This method reads in the prices for the rush orders
         * ***********************************************************************/
        static void readPrices(int[] rushPrices)
        {
            StreamReader reader = new StreamReader(@"C:\Users\metauser\Documents\am_workspace\BYUI\MegaE-RushOrders.txt");

            for (int i = 0; i < 9; i++)
            {
                string priceString = reader.ReadLine();
                rushPrices[i] = int.Parse(priceString);
            }
            reader.Close();
        }

        /**************************************************************************
         * This method asks the user if they want a rush order
         * ***********************************************************************/
        static void promptRush(ref DeskOrder deskOrder)
        {

            do
            {
                Console.WriteLine("\nIf applicable, please enter a rush order time of 3, 5, or 7 days. \nOtherwise please enter the normal production time of 14 days.");
                string rushStream = Console.ReadLine();
                try
                {
                    deskOrder.time = int.Parse(rushStream);

                    if (deskOrder.time != 3 && deskOrder.time != 5 && deskOrder.time != 7 && deskOrder.time != 14)
                        throw new Exception("Please enter either 3, 5, 7 or 14\n");
                }
                catch (Exception eRush)
                {
                    Console.WriteLine(eRush.Message);
                }
            }
            while (deskOrder.time != 3 && deskOrder.time != 5 && deskOrder.time != 7 && deskOrder.time != 14);


        }

        /**************************************************************************
         * This method calculates the prices and total price of the order.
         * ***********************************************************************/
        static void calcPrice(ref DeskOrder deskOrder, ref int surfacePrice, ref int drawerPrice, ref int matPrice, ref int rushPrice, int [] rushPrices)
        {
            surfacePrice = (((deskOrder.length * deskOrder.width) - 1000) * 5);
            if (surfacePrice < 0)
                surfacePrice = 0;

            double deskSize = (deskOrder.width * deskOrder.length); 

            drawerPrice = (deskOrder.drawers * 50);

            switch (deskOrder.mat)
            {
                case "Oak":
                    matPrice = 200;
                    break;
                case "Laminate":
                    matPrice = 100;
                    break;
                case "Pine":
                    matPrice = 50;
                    break;
                case "Maple":
                    matPrice = 210;
                    break;
                case "Cherry":
                    matPrice = 180;
                    break;
                case "Aspen":
                    matPrice = 120;
                    break;
            }

            switch (deskOrder.time)
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

            deskOrder.totalPrice = (200 + surfacePrice + drawerPrice + matPrice + rushPrice);



        }

        /**************************************************************************
         * This method displays the complete order to the screen.
         * ***********************************************************************/
        static void displayOrder(DeskOrder deskOrder, int surfacePrice, int drawerPrice, int matPrice, int rushPrice)
        {
            Console.WriteLine("\nYour Order is as follows: ");
            Console.WriteLine("Width: " + deskOrder.width + " in");
            Console.WriteLine("Length: " + deskOrder.length + " in");
            Console.WriteLine("Oversize Surface Price: $" + surfacePrice + "\n");

            Console.WriteLine("Number of Drawers: " + deskOrder.drawers);
            Console.WriteLine("  Drawer Price: $" + drawerPrice + "\n");

            Console.WriteLine("Material: " + deskOrder.mat);
            Console.WriteLine("  Material Price: $" + matPrice + "\n");

            Console.WriteLine("Order Time: " + deskOrder.time + " days");
            Console.WriteLine("  Rush Price: $" + rushPrice + "\n");

            Console.WriteLine("Total Price: $" + deskOrder.totalPrice);

        }

        /**************************************************************************
         * This method will write the order to a file
         * ***********************************************************************/
         static void writeFile(DeskOrder deskOrder, int surfacePrice, int drawerPrice, int matPrice, int rushPrice)
        {
            StreamWriter writer;
            writer = new StreamWriter(@"C:\Users\sligy\Desktop\BYU-I\Spring 2016\orderReference.txt");

            writer.WriteLine("{");
            writer.WriteLine("\t\"order\":");
            writer.WriteLine("\t{");
            writer.WriteLine("\t\t\"width\":\"" + deskOrder.width + " in\"");
            writer.WriteLine("\t\t\"length\":\"" + deskOrder.length + " in\"");
            writer.WriteLine("\t\t\"oversizeSurfacePrice\":\"$" + surfacePrice + "\"\n");

            writer.WriteLine("\t\t\"numberOfDrawers\":\"" + deskOrder.drawers + "\"");
            writer.WriteLine("\t\t\"drawerPrice\":\"$" + drawerPrice + "\"\n");

            writer.WriteLine("\t\t\"material\":\"" + deskOrder.mat + "\"");
            writer.WriteLine("\t\t\"materialPrice\":\"$" + matPrice + "\"\n");

            writer.WriteLine("\t\t\"orderTime\":" + deskOrder.time + "days\"");
            writer.WriteLine("\t\t\"rushPrice\":\"$" + rushPrice + "\"\n");

            writer.WriteLine("\t\t\"totalPrice\":\"$" + deskOrder.totalPrice + "\"");
            writer.WriteLine("\t}");
            writer.WriteLine("}");
            writer.Close();
        }

        /**************************************************************************
         * This is the Main method of the program.
         * ***********************************************************************/
        static void Main(string[] args)
        {
            DeskOrder deskOrder = new DeskOrder();
            int[] rushPrices = new int[9];
            readPrices(rushPrices);

            promptDeskWidth(ref deskOrder);
            promptDeskLength(ref deskOrder);
            promptDrawers(ref deskOrder);
            promptMaterial(ref deskOrder);
            promptRush(ref deskOrder);

            int surfacePrice = 0;
            int drawerPrice = 0;
            int materialPrice = 0;
            int rushPrice = 0;

            calcPrice(ref deskOrder, ref surfacePrice, ref drawerPrice, ref materialPrice, ref rushPrice, rushPrices);

            displayOrder(deskOrder, surfacePrice, drawerPrice, materialPrice, rushPrice);

            writeFile(deskOrder, surfacePrice, drawerPrice, materialPrice, rushPrice);

        }
    }
}