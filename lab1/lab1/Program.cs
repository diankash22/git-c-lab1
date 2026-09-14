using System;
using System.IO;

namespace LabWork
{
    class Program
    {
        static void Main(string[] args)
        {
            string filePath = "Podarok.txt";
            string text = File.ReadAllText(filePath);
            Console.WriteLine(text);
        }
    }
}﻿
