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

                    // 1. Створення папок
                    Directory.CreateDirectory(folder1);
                    Directory.CreateDirectory(folder2);

                    Console.WriteLine("\n[1] Створено папки:");
                    Console.WriteLine(folder1);
                    Console.WriteLine(folder2);

                    // 2. Створення t1.txt у folder1
                    string t1Path = Path.Combine(folder1, "t1.txt");

                    File.WriteAllText(t1Path,
                        "Лабораторні роботи. Мова програмування C#. Лазорик ВВ\n" +
                        "Шевченко Степан Іванович, 2001 року народження, місце проживання м. Суми"
                    );

                    Console.WriteLine("\n[2] Створено t1.txt:");
                    Console.WriteLine(t1Path);

                    // 3. Створення t2.txt у folder1
                    string t2Path = Path.Combine(folder1, "t2.txt");

                    File.WriteAllText(t2Path,
                        "Комар Сергій Федорович, 2000 року народження, місце проживання м. Київ"
                    );

                    Console.WriteLine("\n[3] Створено t2.txt:");
                    Console.WriteLine(t2Path);

                    // 4. Створення t3.txt у folder2 (t1 + t2)
                    string t3Path = Path.Combine(folder2, "t3.txt");

                    string t1Text = File.ReadAllText(t1Path);
                    string t2Text = File.ReadAllText(t2Path);

                    File.WriteAllText(t3Path, t1Text + Environment.NewLine + t2Text);

                    Console.WriteLine("\n[4] Створено t3.txt (t1 + t2):");
                    Console.WriteLine(t3Path);

                    // 5. Розгорнута інформація про файли ДО операцій
                    Console.WriteLine("\n[5] Інформація про файли (до переміщень):");

                    PrintFiles(folder1);
                    PrintFiles(folder2);

                    // 6. Переміщення t2.txt у folder2
                    string newT2Path = Path.Combine(folder2, "t2.txt");
                    File.Move(t2Path, newT2Path);

                    Console.WriteLine("\n[6] t2.txt переміщено у folder2");

                    // 7. Копіювання t1.txt у folder2
                    string copyT1Path = Path.Combine(folder2, "t1.txt");
                    File.Copy(t1Path, copyT1Path, true);

                    Console.WriteLine("\n[7] t1.txt скопійовано у folder2");

                    // 8. Перейменування folder2 -> ALL
                    string allFolder = Path.Combine(basePath, "ALL");

                    if (Directory.Exists(allFolder))
                        Directory.Delete(allFolder, true);

                    Directory.Move(folder2, allFolder);

                    Console.WriteLine("\n[8] folder2 перейменовано в ALL");

                    // 9. Видалення folder1
                    Directory.Delete(folder1, true);

                    Console.WriteLine("\n[9] folder1 видалено");

                    // 10. Фінальна інформація
                    Console.WriteLine("\n[10] Фінальна інформація про ALL:");

                    PrintFiles(allFolder);

                    Console.WriteLine("\nГотово.");
                    break;
            }
            
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
        static void PrintFiles(string folder)
        {
            Console.WriteLine($"\n--- Папка: {folder} ---");

            foreach (string file in Directory.GetFiles(folder))
            {
                FileInfo info = new FileInfo(file);

                Console.WriteLine($"Ім'я: {info.Name}");
                Console.WriteLine($"Розмір: {info.Length} байт");
                Console.WriteLine($"Шлях: {info.FullName}");
                Console.WriteLine($"Створено: {info.CreationTime}");
                Console.WriteLine();
            }
        }
}
