using System;

namespace mamad
{
  class Program
  {
    static void Main(string[] args)
    {
        //Hangman.Play();
        Connect connect = new Connect(10,10);
        connect.play();
    }
  }
}