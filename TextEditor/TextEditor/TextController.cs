namespace TextEditor
{
    /// <summary>
    /// Handles word, letter and whitespace counters
    /// </summary>
    class TextController
    {
        public string Text { set; get; }

        public int GetNumberOfLetters()
        {
            int letterCount = 0;
            //Iterates through text
            for (int i = 0; i < Text.Length; i++)
            {
                //If current element is a létter or a digit
                if (char.IsLetterOrDigit(Text[i]) == true)
                {
                    //Increase the letter count by 1
                    letterCount++;
                }

            }
            return letterCount; //Returns number of letters
        }
        //Does the same as previous function but counts whitespaces too
        public int getNumberOfLettersAndSpaces()
        {
            int letterCount = 0;
            for (int i = 0; i < Text.Length; i++)
            {
                //If current element is a letter, digit or a " "
                if (char.IsLetterOrDigit(Text[i]) == true || Text[i].Equals(' ') == true)
                {
                    letterCount++;
                }

            }
            return letterCount; //Returns the symbol count
        }
        //Counts the words
        public int getNumberOfWords()
        {
            int wordCount = 0;
            int letterCount = 0;
            for (int i = 0; i < Text.Length; i++)
            {
                if (char.IsLetterOrDigit(Text[i]) == true)
                {
                    letterCount++;
                }
                //If character is a whitespace, punctuation or if it's a last letter in the text file
                if (char.IsWhiteSpace(Text[i]) == true || char.IsPunctuation(Text[i]) == true || i + 1 == Text.Length)
                {
                    //If there is even a single letter
                    if (letterCount > 0)
                    {
                        //Count it as a word
                        wordCount++;
                        //Set letter count back to zero for counting letters for next word
                        letterCount = 0;
                    }
                }
            }
            return wordCount; //Returns number of words.
        }
    }
}
