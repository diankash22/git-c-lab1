using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
namespace LabWork
{
    class Program
    {
        static Dictionary<string, Color> ColorMap = new Dictionary<string, Color>(StringComparer.OrdinalIgnoreCase)
        {
            {"красн", Color.Red },
            {"ал", Color.Crimson },
            {"бар", Color.DarkRed },
            {"зелен", Color.Green },
            {"изумруд", Color.MediumSeaGreen },
            {"малахит", Color.MediumSeaGreen },
            {"син", Color.Blue },
            {"голуб", Color.LightBlue },
            {"лазур", Color.LightSkyBlue },
            {"ультрамарин", Color.Blue },
            {"жел", Color.Yellow },
            {"золот", Color.Gold },
            {"лимон", Color.LemonChiffon },
            {"бел", Color.White },
            {"черн", Color.Black },
            {"сер", Color.Gray },
            {"фиолетов", Color.Purple },
            {"лилов", Color.Purple },
            {"оранжев", Color.Orange },
            {"коричнев", Color.Brown },
            {"розов", Color.Pink },
            {"бирюз", Color.Turquoise }
        };
        static void Main(string[] args)
        {
            string filePath = "Podarok.txt";
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Ошибка: Файл '{filePath}' не найден.");
                return;
            }
            string text = GetText(filePath);
            var result = GetColors(text);
            Console.WriteLine($"Количество найденных цветов: {result.Colors.Count}");
        }
        static string GetText(string filePath)
        {
            using (StreamReader reader = new StreamReader(filePath, System.Text.Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
        static (List<string> Words, List<Color> Colors) GetColors(string text)
        {
            var colors = new List<Color>();
            var coloredWords = new List<string>();
            var words = Regex.Matches(text, @"\b[\p{IsCyrillic}a-zA-Z]+\b");
            foreach (Match wordMatch in words)
            {
                string word = wordMatch.Value.ToLower();
                var baseColor = ColorMap.FirstOrDefault(kvp => word.StartsWith(kvp.Key));
                if (!baseColor.Equals(default(KeyValuePair<string, Color>)))
                {
                    colors.Add(baseColor.Value);
                    coloredWords.Add(word);
                }
            }
            return (coloredWords, colors);
        }
    }
}
