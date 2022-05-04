
using System;

namespace DataClasses
{
    /// <summary>
    /// The class encapsulating a review's data
    /// </summary>
    public class Review
    {
        private string _title, _content;
        public string Author { get; set; }
        public string Anime { get; set; }
        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                if (value.Length == 0)
                    throw new ArgumentException("Titlul nu poate fi necompletat!");
                if (value.Length > 30)
                    throw new ArgumentException("Titlul poate avea lungimea de maxim 30 de caractere!");
                _title = value;
            }
        }
        public string Content
        {
            get
            {
                return _content;
            }
            set
            {
                if (value.Length == 0)
                    throw new ArgumentException("Continutul review-ului nu poate fi necompletat!");
                if (value.Length > 10000)
                    throw new ArgumentException($"Continutul review-ului dumneavoastra are {value.Length} caractere iar limita este de 10000 caractere!");
                _content = value;
            }
        }
        public int Rating { get; set; }
    }
}
