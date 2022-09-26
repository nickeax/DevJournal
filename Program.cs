// See https://aka.ms/new-console-template for more information
using DevJournal.Models;
using System.Linq;

// Start the app, clear the console
var ui = new UI();
// Check if a Journal can deserialised

ui.Message($"Welcome to Dev Journal 0.1");
var JM = new JournalManager(new UI());

if (JM.AllAvailable().Length > 0)
{
  ui.Message($"Journals to choose from: {JM.AllAvailable().Count()}");
  ui.Message(JM.ListAllAvailble());
}
else
{
  await JM.CMD.CreateJournal(ui.StringPrompt("Please enter a name for the Journal:"));
}

JM.CMD.ProcessCommand("LoadJournal", "ARG");