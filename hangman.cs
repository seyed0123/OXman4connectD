using System;
using System.Collections.Generic;
using System.Threading;

public class Hangman
{
 private static string hangman = "";
    private static int step;
    private static int remainingGuesses;
    private static string wordlist = "Unfortunately, you caused the death of an innocent person due to your incompetence and laziness in performing your duties";
    private static string GetRandomWord()
    {
        Console.WriteLine("\x1b[36mhost section");
        Thread.Sleep(2000);
        Console.WriteLine("enter the secret word");
        string password = "";
        while (true)
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            if (key.Key == ConsoleKey.Enter)
            {
                break;
            }
            password += key.KeyChar;
            Console.Write("*");
        }
        Console.WriteLine();
        return password;
    }

    private static void GetMaskedWord(string word, HashSet<char> guessedLetters)
    {
        foreach (char c in word)
        {
            if (guessedLetters.Contains(c))
            {
                Console.Write("\x1b[32m{0}\x1b[0m",c);
            }
            else
            {
                Console.Write("\x1b[35m_\x1b[0m");
            }
            Console.Write(" ");
        }
        Console.WriteLine("");
    }

    private static bool finish(string word,HashSet<char> guessedLetters)
    {
        foreach(char c in word)
        {
            if(!guessedLetters.Contains(c))
                return false;
        }
        return true;
    }

    private static void numGuess(string secretWord)
    {
        remainingGuesses = secretWord.Length / 5 + 6;
        step = wordlist.Length / remainingGuesses;
    }

    public static void Play()
    {
        string secretWord = GetRandomWord();
        HashSet<char> guessedLetters = new HashSet<char>();
        numGuess(secretWord);
        Console.Write("\x1b[0m");

        for (int iter = 0 ; iter <=remainingGuesses;)
        {
            Console.WriteLine("\x1b[36;1mGuess a letter ({0} guesses remaining):\x1b[0m", remainingGuesses-iter);
            GetMaskedWord(secretWord, guessedLetters);
            Console.WriteLine("\x1b[31m"+hangman+"\x1b[0m");
            char guess;
            try
            {
                string input = Console.ReadLine()?.ToLower();
                guess = input != null && input.Length > 0 ? input[0] : default(char);
                if(guess=='\0')
                    throw new Exception("the input is empty");
            }catch(Exception e)
            {
                Console.WriteLine(e);
                continue;
            }
            Console.Clear();

            if (guessedLetters.Contains(guess))
            {
                Console.WriteLine("\x1b[38;2;255;165;0mYou already guessed that letter!\x1b[0m");
                continue;
            }

            guessedLetters.Add(guess);

            if (secretWord.Contains(guess))
            {
                Console.WriteLine("\x1b[32mCorrect!\x1b[0m");
                if (finish(secretWord,guessedLetters))
                {
                    Console.WriteLine("\x1b[33mCongratulations, you guessed the word!\nthe word as you say , is {0}\x1b[0m",secretWord);
                    return;
                }
            }
            else
            {
                iter++;
                DrawHangman();
                Console.WriteLine("\x1b[38;2;255;255;153mIncorrect!\x1b[0m");
            }
        }
        Console.WriteLine("\x1b[31m"+hangman+"\x1b[0m");
        Console.WriteLine("\x1b[31;1mSorry, you ran out of guesses. The word was {0}.\x1b[0m", secretWord);
    }

    private static void DrawHangman()
    {
        int length = Math.Min(step, wordlist.Length - hangman.Length);
        hangman += wordlist.Substring(hangman.Length, length);
    }
}