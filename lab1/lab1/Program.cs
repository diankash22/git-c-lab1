using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
                Console.WriteLine("Убедитесь, что файл находится в папке с исполняемым файлом (bin/Debug/netX.X/).");
                return;
            }
            string text = GetText(filePath);
            var result = GetColors(text);
            Console.WriteLine("Найденные цвета:");
            foreach (var (word, color) in result.Words.Zip(result.Colors, (w, c) => (w, c)))
            {
                Console.WriteLine($"{word,-20} {color}");
            }
            string outputPath = Path.ChangeExtension(filePath, ".png");
            CreateBookPortrait(result.Colors, outputPath);
            Console.WriteLine($"\nИзображение сохранено в: {outputPath}");
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
        static void CreateBookPortrait(List<Color> colors, string outputPath, int columns = 0, int squareSize = 20)
        {
            if (colors == null || !colors.Any())
            {
                Console.WriteLine("Нет данных для создания изображения.");
                return;
            }
            if (columns == 0)
            {
                columns = (int)Math.Ceiling(Math.Sqrt(colors.Count));
            }
            int rows = (int)Math.Ceiling((double)colors.Count / columns);
            int imageWidth = columns * squareSize;
            int imageHeight = rows * squareSize;
            using (var bitmap = new Bitmap(imageWidth, imageHeight))
            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.Clear(Color.White);
                for (int i = 0; i < colors.Count; i++)
                {
                    int row = i / columns;
                    int col = i % columns;
                    int x = col * squareSize;
                    int y = row * squareSize;
                    using (var brush = new SolidBrush(colors[i]))
                    {
                        graphics.FillRectangle(brush, x, y, squareSize, squareSize);
                    }
                }
                bitmap.Save(outputPath, ImageFormat.Png);
            }
        }
    }
}
