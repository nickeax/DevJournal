namespace DevJournal.Models;// See https://aka.ms/new-console-template for more information
public class UI
{
  public string StringPrompt(string text, bool clear = false)
  {
    if (clear) System.Console.Clear();

    string ret = string.Empty;

    System.Console.WriteLine($"{text}");
    ret = System.Console.ReadLine() ?? "";

    return ret ?? string.Empty;
  }

  public void Message(string text = "", bool clear = false)
  {
    if (clear) System.Console.Clear();
    System.Console.WriteLine($"{text}");
  }
}
