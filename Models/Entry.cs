using System;

namespace DevJournal.Models;
public class Entry
{
  public Guid Id;
  public DateTime DateTime { get; set; }
  public string Text = string.Empty;
  public Entry() { }
  public Entry(string text)
  {
    Id = Guid.NewGuid();
    Text = text;
    DateTime = DateTime.Now;
  }
}
