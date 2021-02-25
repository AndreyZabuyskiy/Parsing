using System;
using System.Collections.Generic;

namespace Parsing
{
    class Notebook
    {
        public List<string>[] Pages { get; set; }

        private const int SIZE_PAGE = 10;

        public Notebook(List<Item> items)
        {
            FillPages(items);
        }

        private void FillPages(List<Item> items)
        {
            Pages = new List<string>[items.Count / 10];

            int iterator = 0;

            for (int i = 0; i < Pages.Length; ++i)
            {
                Pages[i] = new List<string>();

                for (int j = 0; j < SIZE_PAGE; ++j)
                {
                    if (items[iterator] != null)
                    {
                        Pages[i].Add(items[iterator++].Title);
                    }
                }
            }
        }

        public void ShowPage(int numberPage)
        {
            int iterator = numberPage * 10;            

            for (int i = 0; i < Pages[numberPage].Count; ++i)
            {
                Console.WriteLine($"[{++iterator}] {Pages[numberPage][i]}");
            }
        }
    }
}
