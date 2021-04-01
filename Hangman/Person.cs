using System.Collections.Generic;

namespace Hangman
{
    internal partial class Program
    {
        class Player
        {
            public Player(string name)
            {
                this.Name = name;
            }
            public string Name { get; private set; }
            private int score;
            public int Score
            {
                get { return score; }
                set
                {
                    if (value > 0)
                        score = value;
                }
            }
            public IList<char> GuessedLetters { get; } = new List<char>();
        }
    }
}
