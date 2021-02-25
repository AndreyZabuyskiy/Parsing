using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Parsing
{
    class Item
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string PubDate { get; set; }

        public void Show()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"Категория: {Category}\n\n");
            stringBuilder.Append($"Новость: \"{Title}\"\n\n");
            stringBuilder.Append($"{Description}\n\n");
            stringBuilder.Append($"\t\tДата публикации: {PubDate}");

            Console.WriteLine(stringBuilder.ToString());
        }
    }
}
