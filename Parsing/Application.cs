using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;

namespace Parsing
{
    class Application
    {        
        public string RSSFile { get; set; } = "http://timeua.com/rss.xml?r=4";
        public string PathFile { get; set; } = "RssFile.txt";
        public List<Item> Items { get; set; } = new List<Item>();
        public Notebook Pages { get; set; }
        public int CurrentPage { get; set; } = 0;

        public void Start()
        {            
            ReadRSSFile();
            FillItems();
            Pages = new Notebook(Items);
            while (Action(GetCommand())) ;
        }

        private void ReadRSSFile()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding Code = Encoding.GetEncoding(1251);

            using WebClient wc = new WebClient();
            File.WriteAllText(PathFile, ToString(Code.GetString(wc.DownloadData(RSSFile))));
        }
        private string ToString(string data)
        {
            char[] line = new char[data.Length];

            for (int i = 0; i < data.Length; ++i)
            {
                line[i] = data[i];

                if (line[i] == '\n')
                {
                    line[i] = ' ';
                }
            }

            return new string(line);
        }
        private void FillItems()
        {
            var parseElement = new ParseElement();
            string pattern = "<item>(.*?)</item>";

            var matches = Regex.Matches(File.ReadAllText(PathFile), pattern);            

            foreach (Match m in matches)
            {
                Items.Add(new Item()
                {
                    Title = parseElement.FillElement(m.Groups[1].Value, "<title>"),
                    Description = parseElement.FillElement(m.Groups[1].Value, "<description>"),
                    Category = parseElement.FillElement(m.Groups[1].Value, "<category>"),
                    PubDate = parseElement.FillElement(m.Groups[1].Value, "<pubDate>")
                });
            }
        }

        private void PrintMenu()
        {
            PrintPage();

            Console.Write($"\n[1 - {Items.Count}] - выбор новости для просмотра!\n" +
                $"[/1 - /{Pages.Pages.Length}] - выбор страницы для просмотра!\n" +
                $"\n[/0] Выход;\n" + "->");
        }
        private void PrintPage()
        {
            Console.Clear();

            try
            {
                Console.WriteLine($"Текущая страница: {CurrentPage + 1}\n");
                Pages.ShowPage(CurrentPage);
            }
            catch (IndexOutOfRangeException)
            {
                Console.Clear();
                Console.WriteLine("Страница не найдена!\n");
            }
        }        

        private string GetCommand()
        {
            PrintMenu();
            return Console.ReadLine();            
        }       

        private bool Action(string command)
        {
            try
            {
                Console.Clear();

                if (command[0] != '/')
                {                    
                    ShowNews(int.Parse(command));
                }
                else
                {
                    int parseCommand = int.Parse(ParseCommand(command)) - 1;                   

                    if (parseCommand == -1) return false;
                    CurrentPage = parseCommand;
                }
            }
            catch(FormatException)
            {
                Console.WriteLine("Неверная комманда!");
                Console.ReadLine();
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Неверная комманда!");
                Console.ReadLine();
            }

            return true;
        }
        private void ShowNews(int numberNews)
        {
            --numberNews;

            try
            {
                Items[numberNews].Show();
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine("Новость не найдена!");                
            }

            Console.ReadLine();
        }
        private char[] ParseCommand(string command)
        {
            char[] ch = new char[command.Length - 1];

            for (int i = 0; i < ch.Length; ++i)
            {
                ch[i] = command[i + 1];
            }

            return ch;
        }        
    }
}
