using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DevJournal.Models;

namespace DevJournal;
public class Commander
{
  public int MyProperty { get; set; }
  private JournalManager _jm;
  private Dictionary<string, Func<string, Task<string>>> Commands = new Dictionary<string, Func<string, Task<string>>>();
  public Commander(JournalManager j)
  {
    _jm = j;
    Commands.Add("LoadJournal", LoadJournal);
    Commands.Add("CreateJournal", CreateJournal);
    Commands.Add("DeleteJournal", DeleteJournal);
    Commands.Add("CreateCategory", CreateCategory);
    Commands.Add("GetCurrentCategory", GetCurrentCategory);
    // Commands.Add("CreateJournal", CreateJournal);
    // Commands.Add("CreateJournal", CreateJournal);
  }

  public int ProcessCommand(string cmd, string arg)
  {
    var ret = 1;

    if (Commands.ContainsKey(cmd))
    {
      Commands[cmd](arg);
    }

    return ret;
  }

  #region Journal_Ops
  private void RefreshJournal()
  {
    // Serialize current journal and reload it into current journal
    var fStream = new FileStream(@"./tutorialSerial.dat", FileMode.Create, FileAccess.Write);
  }

  private async Task<string> LoadJournal(string name)
  {
    return $"Loaded Journal: {name}";
  }

  public async Task<string> CreateJournal(string name)
  {
    var j = new Journal(name);
    // Add to existing list or create new file and add new Journal name to it
    if (!File.Exists(_jm.journalsPath))
    {
      await File.WriteAllLinesAsync(_jm.journalsPath, new string[] { name });
    }
    else
    {
      var currentLines = await File.ReadAllLinesAsync(_jm.journalsPath);
      var currentLinesList = currentLines.ToList();
      currentLinesList.Add(name);

      File.AppendAllLines(_jm.journalsPath, currentLinesList);
    }

    return $"Added Journal: {name}.";
  }

  private async Task<string> DeleteJournal(string name)
  {
    // Remove from manifiest
    string ret = string.Empty;
    var all = _jm.AllAvailable();
    var list = all.ToList();
    list.Remove(name);
    all = list.ToArray();

    // Remove from serial file

    return ret;
  }
  #endregion

  #region Category_Ops
  public async Task<string> CreateCategory(string name = "General")
  {
    var newCat = new Category(name);

    _jm.CurrentJournal.Categories?.Add(newCat);
    _jm.CurrentJournal.CurrentCategory = newCat;

    return $"The current Category is: {_jm.CurrentJournal.CurrentCategory.ToString()} [CurrentJournal.Categories?.Where(c => c.Id == CurrentJournal.CurrentCategory).FirstOrDefault().Id]";
  }

  public async Task<string> GetCurrentCategory(string def = "noop")
  {
    var ret = string.Empty;
    ret = _jm.CurrentJournal.Categories?.Where(c => c.Id == _jm.CurrentJournal.CurrentCategory?.Id)?.FirstOrDefault()?.ToString() ?? string.Empty;

    return ret;
  }
  #endregion
}