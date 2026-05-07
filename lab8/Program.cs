using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;
            Console.WriteLine("Введіть номер завдання (1-5) або 'exit' для виходу: ");
            int choice = int.Parse(Console.ReadLine()!.Trim());
            switch (choice)
            {
                case 1:
                    /*
                     * Задано текстовий файл. У файлі рядки в яких
                    міститься осмислене текстове повідомлення. Слова повідомлення розділяються 
                    пробілами та розділовими знаками Записати у новий файл всі підтексти 
                    заданого формату, підрахувати їх кількість, вилучити та замінити деякі з них, 
                    за вказаними параметрами користувача.
                    */
                    string inputPath = @"D:\cs_tasks\lab8\lab8\message.txt";
                    string resultsPath = @"D:\cs_tasks\lab8\lab8\emails.txt";
                    string outputPath = @"D:\cs_tasks\lab8\lab8\result.txt";

                    string pattern = @"\b[a-zA-Z0-9._%+-]+@ukr\.net\b";

                    try
                    {
                        if (!File.Exists(inputPath))
                        {
                            Console.WriteLine("Помилка: Вхідний файл не знайдено!");
                            return;
                        }

                        string content = File.ReadAllText(inputPath);

                        var matches = Regex.Matches(content, pattern);

                        File.WriteAllLines(resultsPath, matches.Cast<Match>().Select(m => m.Value));

                        Console.WriteLine($"Знайдено адрес: {matches.Count}");
                        Console.WriteLine($"Список адрес збережено у: {resultsPath}");

                        Console.WriteLine("\nВведіть адресу, яку потрібно ВИЛУЧИТИ:");
                        string toDelete = Console.ReadLine()!;

                        Console.WriteLine("Введіть адресу, яку потрібно ЗАМІНИТИ:");
                        string toReplace = Console.ReadLine()!;
                        Console.WriteLine("На що замінити?");
                        string replacement = Console.ReadLine()!;

                        string updatedContent = content;

                        if (!string.IsNullOrEmpty(toDelete))
                        {
                            updatedContent = updatedContent.Replace(toDelete, "");
                        }

                        if (!string.IsNullOrEmpty(toReplace))
                        {
                            updatedContent = updatedContent.Replace(toReplace, replacement);
                        }

                        File.WriteAllText(outputPath, updatedContent);
                        Console.WriteLine($"\nОброблений текст збережено у: {outputPath}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Сталася помилка: {ex.Message}");
                    }
                    break;
                case 2:
                    /*
                     * Задано текстовий файл message.txt. Записати у 
                    новий файл result2.txt результат задачі. Замінити послідовність букв в алфавітному порядку на 
                    скорочений запис (наприклад: abcdf -> a-f);
                    */

                    inputPath = @"D:\cs_tasks\lab8\lab8\message.txt";
                    outputPath = @"D:\cs_tasks\lab8\lab8\result2.txt";

                    try
                    {
                        string text = File.ReadAllText(inputPath);
                        string processedText = CompressAlphabetSequences(text);

                        File.WriteAllText(outputPath, processedText);

                        Console.WriteLine("Оброблено успішно. Результат у файлі result2.txt");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Помилка: {ex.Message}");
                    }
                    break;

                case 3:
                    /*
                     * Задано текст, слова в якому розділені пробілами і розділовими 
                    знаками. Розробити програму, яка знаходить і друкує всі слова, 
                    що входять у заданий текст по одному разу.
                    */
                    inputPath = @"D:\cs_tasks\lab8\lab8\message.txt";
                    try
                    {
                        string text = File.ReadAllText(inputPath);
                        var words = Regex.Matches(text, @"\b\w+\b")
                                         .Cast<Match>()
                                         .Select(m => m.Value)
                                         .ToList();
                        var uniqueWords = words.GroupBy(w => w)
                                               .Where(g => g.Count() == 1)
                                               .Select(g => g.Key);
                        Console.WriteLine("Слова, що входять у текст по одному разу:");
                        foreach (var word in uniqueWords)
                        {
                            Console.WriteLine(word);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Помилка: {ex.Message}");
                        Console.WriteLine(ex.ToString());
                    }
                    break;
                case 4:
                    /*
                     * Робота із двійковими файлами
                     * Дана послідовність із n дійсних чисел. Записати всі ці числа у 
                     * файл. Вивести на екран всі компоненти файлу з парними 
                     * номерами, менші заданого числа.
                     */
                    Console.WriteLine("Введіть кількість чисел:");
                    int n = int.Parse(Console.ReadLine()!);

                    Console.WriteLine("Введіть числа:");
                    double[] numbers = Console.ReadLine()!
                        .Split(' ')
                        .Select(double.Parse)
                        .ToArray();

                    Console.WriteLine("Введіть межу:");
                    double limit = double.Parse(Console.ReadLine()!);

                    string binaryFilePath = @"D:\cs_tasks\lab8\lab8\data.bin";

                    using (BinaryWriter writer = new BinaryWriter(File.Open(binaryFilePath, FileMode.Create)))
                    {
                        foreach (double number in numbers)
                        {
                            writer.Write(number);
                        }
                    }

                    Console.WriteLine("Числа з парними номерами, менші за межу:");

                    using (BinaryReader reader = new BinaryReader(File.Open(binaryFilePath, FileMode.Open)))
                    {
                        int index = 0;

                        while (reader.BaseStream.Position < reader.BaseStream.Length)
                        {
                            double number = reader.ReadDouble();

                            if (index % 2 == 0 && number < limit)
                            {
                                Console.WriteLine(number);
                            }

                            index++;
                        }
                    }
                    break;
                case 5:
                    Console.WriteLine("Введіть прізвище студента:");
                    string surname = Console.ReadLine()!.Trim();

                    string basePath = @"D:\temp";

                    string folder1 = Path.Combine(basePath, $"{surname}1");
                    string folder2 = Path.Combine(basePath, $"{surname}2");

                    Directory.CreateDirectory(folder1);
                    Directory.CreateDirectory(folder2);

                    Console.WriteLine("\nСтворені папки:");
                    Console.WriteLine(folder1);
                    Console.WriteLine(folder2);

                    string t1Path = Path.Combine(folder1, "t1.txt");

                    File.WriteAllText(
                        t1Path,
                        "Лабораторні роботи. Мова програмування C#. Лазорик ВВ\n" +
                        "Шевченко Степан Іванович, 2001 року народження, місце проживання м. Суми"
                    );

                    Console.WriteLine("\nСтворено файл:");
                    Console.WriteLine(t1Path);

                    string t2Path = Path.Combine(folder1, "t2.txt");

                    File.WriteAllText(
                        t2Path,
                        "Комар Сергій Федорович, 2000 року народження, місце проживання м. Київ"
                    );

                    Console.WriteLine("\nСтворено файл:");
                    Console.WriteLine(t2Path);

                    string t3Path = Path.Combine(folder2, "t3.txt");

                    string t1Text = File.ReadAllText(t1Path);
                    string t2Text = File.ReadAllText(t2Path);

                    File.WriteAllText(
                        t3Path,
                        t1Text + Environment.NewLine + t2Text
                    );

                    Console.WriteLine("\nСтворено файл (об'єднання t1 + t2):");
                    Console.WriteLine(t3Path);

                    Console.WriteLine("\n--- Файли у folder1 ---");

                    foreach (string file in Directory.GetFiles(folder1))
                    {
                        FileInfo info = new FileInfo(file);

                        Console.WriteLine($"Ім'я: {info.Name}");
                        Console.WriteLine($"Розмір: {info.Length} байт");
                        Console.WriteLine($"Шлях: {info.FullName}");
                        Console.WriteLine($"Створено: {info.CreationTime}");
                        Console.WriteLine();
                    }

                    Console.WriteLine("\n--- Файли у folder2 ---");

                    foreach (string file in Directory.GetFiles(folder2))
                    {
                        FileInfo info = new FileInfo(file);

                        Console.WriteLine($"Ім'я: {info.Name}");
                        Console.WriteLine($"Розмір: {info.Length} байт");
                        Console.WriteLine($"Шлях: {info.FullName}");
                        Console.WriteLine($"Створено: {info.CreationTime}");
                        Console.WriteLine();
                    }

                    Console.WriteLine("\nГотово. Усі операції виконано успішно.");
                    break;
            }
        }

        static string CompressAlphabetSequences(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            StringBuilder result = new StringBuilder();
            int i = 0;

            while (i < input.Length)
            {
                int start = i;

                while (i + 1 < input.Length && input[i + 1] == input[i] + 1)
                {
                    i++;
                }

                if (i > start)
                {
                    result.Append(input[start]);
                    result.Append('-');
                    result.Append(input[i]);
                }
                else
                {
                    result.Append(input[start]);
                }

                i++;
            }

            return result.ToString();
        }
    }
}
