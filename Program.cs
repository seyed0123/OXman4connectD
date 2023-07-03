using System;

namespace mamad
{
  class Program
  {
    static void Main(string[] args)
    {
      while(true)
      {
        Console.WriteLine("\u001b[33mWelcome to the game Hub. 🤗\nchoose what game you want to play. 😎\n\u001b[32m1-hangman 💀\n\u001b[36m2-connect 4 ⌨️\n\u001b[31mexit 🚪\u001b[0m");
        string input = Console.ReadLine() ?? "exit";
        Console.Clear();
        if(input=="hangman")
          Hangman.Play();
        else if(input=="connect 4")
        {
          Console.WriteLine("enter the matrix Length or width");
          int x=10;
          try{
            x = int.Parse(Console.ReadLine()?? "10");
          }catch
          {
            Console.WriteLine("You entered an invalid integer the game is being prepared with the default size");
          }
          Connect connect = new Connect(x,x);
          connect.play();
        }else if(input=="exit")
        {
          Console.WriteLine("\u001b[38;5;208mHave a nice time. 🗿");
          return;
        }else
        {
          Console.WriteLine("\u001b[38;5;218menter a valid input. 🙄");
        }
      }
    }
  }
}