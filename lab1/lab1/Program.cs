using System;
using System.IO;

namespace LabWork{
    class Program{
        static void Main(string[] args){
            string filePath = "Podarok.txt";
            if (!File.Exists(filePath)){
                Console.WriteLine($"Ошибка: Файл '{filePath}' не найден.");
                return;
            }
            string text = GetText(filePath);
            Console.WriteLine(text);
        }

        static string GetText(string filePath){
            using (StreamReader reader = new StreamReader(filePath, System.Text.Encoding.UTF8)){
                return reader.ReadToEnd();
            }
        }
    }
}
