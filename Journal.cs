// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using DevJournal.Models;

[Serializable]
public class Journal
{
  public Guid Id { get; set; }
  public DateTime Created { get; set; }
  public string? Name { get; set; }
  public List<Category>? Categories { get; set; }
  public Category? CurrentCategory { get; set; }
  public Journal() { }
  public Journal(string name)
  {
    Id = Guid.NewGuid();
    Created = DateTime.Now;
    Categories = new List<Category>();
    Name = name;
  }
}
