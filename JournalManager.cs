// See https://aka.ms/new-console-template for more information
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using DevJournal;
using DevJournal.Models;

public partial class JournalManager
{
  XmlSerializer xmlSerializer = new XmlSerializer(typeof(Journal));
  public string journalsPath = "./jmanifest.txt";
  public UI UI { get; set; }
  private List<string> JournalFiles { get; set; }
  public Journal CurrentJournal;
  public string CurrentMessage { get; set; } = string.Empty;
  public Commander CMD { get; set; }
  public JournalManager(UI ui)
  {
    UI = ui;
    JournalFiles = new List<string>();
    CMD = new Commander(this);
  }

  public List<string> ExistingJournals()
  {
    JournalFiles = new List<string>();
    if (File.Exists(journalsPath))
    {
      var bulk = File.ReadAllLines(journalsPath);
      JournalFiles = bulk.ToList();
    }

    return JournalFiles;
  }

  private void InitCategories()
  {
    if (CurrentJournal.Categories?.Count == 0)
    {
      var cName = UI?.StringPrompt("Please enter a name for your first Category:") ?? string.Empty;
      var nc = new Category(cName);
      CurrentJournal.CurrentCategory = nc;
      CurrentJournal.Categories.Add(nc);
      CurrentMessage = $"You are currently working on Journal: {CurrentJournal.CurrentCategory.Name}-{CurrentJournal.CurrentCategory.Id} and Category: {CurrentJournal.CurrentCategory.ToString()}";
    }
    else if (CurrentJournal.CurrentCategory != null)
    {
      CurrentMessage = $"You are currently working on Journal: {CurrentJournal.Name} and Category: {CurrentJournal.CurrentCategory.ToString()}";
    }
    else
    {
      CurrentMessage = "An error happened mate";
    }

    UI?.Message(CurrentMessage);
  }

  public string[] AllAvailable()
  {
    return ExistingJournals().ToArray();
  }

  public string ListAllAvailble()
  {
    var ret = string.Empty;
    var i = 1;
    foreach (var j in AllAvailable())
    {
      ret += $"{i}. {j}\n";
      i++;
    }

    return ret;
  }
}


// var fStream = new FileStream(@"./tutorialSerial.dat", FileMode.Create, FileAccess.Write);

// xmlSerializer.Serialize(fStream, entry1);

// var readStream = new FileStream(@"./tutorialSerial.dat", FileMode.Open, FileAccess.Read);
// if (readStream != null && xmlSerializer != null)
// {
//   t2 = (Tutorial)xmlSerializer?.Deserialize(readStream);
// }
// else
// {
//   t2 = new Tutorial();
// }