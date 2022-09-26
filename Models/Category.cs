using System;
using System.Collections.Generic;

namespace DevJournal.Models;
public class Category
{
  public Guid Id { get; set; }
  public string Name { get; set; } = string.Empty;
  public List<Entry> Entries { get; set; } = new List<Entry>();
  public Category() { }
  public Category(string name)
  {
    Name = name;
  }
  public override string ToString()
  {
    return $"{Name}";
  }
}
