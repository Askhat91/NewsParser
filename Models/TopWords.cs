namespace Models
{
    public class TopWords
    {
        public string Word { get; set; }
        public int Count { get; set; }
        public TopWords(string word, int count)
        {
            Word = word;
            Count = count;
        }
    }
}
