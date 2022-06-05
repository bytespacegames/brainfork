using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace brainfork
{
    class Program
    {

        static string bfc = "+[----->+++<]>+.+.";
        static int pos = 0;

        static int length = 30000;

        static List<int> values = new List<int>();
        static void Main(string[] args)
        {
            TextReader tr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + @"brainfork.runtimeconfig.json");
            tr.ReadLine(); tr.ReadLine(); tr.ReadLine(); tr.ReadLine(); tr.ReadLine(); tr.ReadLine(); tr.ReadLine(); tr.ReadLine(); tr.ReadLine();
            string readBFC = tr.ReadLine();
            if (readBFC != null) { bfc = readBFC; }
            string readLength = tr.ReadLine();
            if (readLength != null) { length =  int.Parse(readLength); }
            for (int i = 0; i < length; i++)
            {
                values.Add(0);
            }

            string[] code = Regex.Split(bfc, string.Empty);
            int ci = 0;

            int ignorenext = 0;

            foreach (String s in code)
            {
                if (ignorenext == 0)
                {
                    ci += 1;
                    if (s == ">")
                    {
                        pos += 1;
                    }
                    if (s == "<")
                    {
                        pos -= 1;

                    }
                    if (s == "+")
                    {
                        ;
                        values[pos] += 1;
                        if (values[pos] > 127) { values[pos] = -128; }
                    }
                    if (s == "-")
                    {
                        values[pos] -= 1;
                        if (values[pos] < -128) { values[pos] = 127; }
                    }
                    if (s == ".")
                    {
                        //Console.WriteLine(values[pos]);
                        Console.Write(Encoding.ASCII.GetString(new byte[] { (byte)(values[pos]) }));
                    }
                    if (s == ",")
                    {
                        values[pos] = (int)Encoding.ASCII.GetBytes(Console.ReadLine().Substring(0, 1))[0];
                    }
                    if (s == "[")
                    {
                        string lc = "";
                        int tempi = ci;
                        int length = 0;
                        while (code[tempi] != "]")
                        {

                            lc += code[tempi];
                            tempi += 1;
                            length += 1;
                        }

                        ignorenext += length;

                        //int ranpos = pos;
                        while (values[pos] != 0)
                        {
                            RunBF(lc);

                        }
                    }
                } else
                {
                    ignorenext -= 1;
                }

            }
            Console.WriteLine(" ");
            Console.WriteLine("This Brainfork application has abruptly stopped. This is likely not an error, but if you believe it is, contact the software vendor.");
            Over();
        }

        static void RunBF(string rbfc)
        {

            string[] code = Regex.Split(rbfc, string.Empty);
            int ci = 0;
            foreach (String s in code)
            {
                ci += 1;
                if (s == ">")
                {
                    pos += 1;
                }
                if (s == "<")
                {
                    pos -= 1;

                }
                if (s == "+")
                {
                    
                    values[pos] += 1;
                    if (values[pos] > 127) { values[pos] = -128; }
                }
                if (s == "-")
                {
                    values[pos] -= 1;
                    if (values[pos] < -128) { values[pos] = 127; }
                }
                if (s == ".")
                {
                    //Console.WriteLine(values[pos]);
                    Console.Write(Encoding.ASCII.GetString(new byte[] { (byte)(values[pos]) }));
                }
                if (s == ",")
                {
                    values[pos] = (int)Encoding.ASCII.GetBytes(Console.ReadLine().Substring(0, 1))[0];
                }
                if (s == "[")
                {
                    string lc = "";
                    int tempi = ci;
                    while (code[tempi] != "]")
                    {
                        lc += code[tempi];
                        tempi += 1;
                    }
                    //int ranpos = pos;
                    while (values[pos] != 0)
                    {

                        RunBF(lc);
                    }
                }
                

            }

        }
        static void Over()
        {
            Console.ReadLine();
            Over();
        }
    }
}