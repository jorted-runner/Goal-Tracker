using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.IO;

public class UserInterface {
    private string _menu = ("Menu Options:\n1. Create New Goal\n2. List Goals\n3. Save Goals\n4. Load Goals\n5. Record Event\n6. Quit\nSelect a choice from the menu: ");
    private int _userScore = 0;
    private List<Goal> _goals = new List<Goal> ();

    public string DisplayMenu() {
        DisplayScore();
        Console.Write(_menu);
        string _userChoice = Console.ReadLine();
        return _userChoice;
    }

    private void DisplayScore() {
        Console.WriteLine($"You have {_userScore} points.\n");
    }

    public bool InterfaceBrain(string choice) {
        switch (choice) {
            case "1":
                NewGoal();
                return true;
            case "2":
                DisplayGoals();
                return true;
            case "3":
                SaveGoals();
                return true;
            case "4":
                LoadGoals();
                return true;
            case "5":
                RecordEvent();
                return true;
            case "6":
                return false;
        }
        Console.WriteLine("\nInvalid Input, try again.\n");
        return true;
    }
    private void NewGoal() {
        Console.WriteLine("\nThe types of Goals are:\n1. One-Time Goal\n2. Eternal Goal\n3. Checklist Goal");
        Console.Write("Which type of goal would you like to create? ");
        string _userGoalChoice = Console.ReadLine();
        string _goalType;
        switch (_userGoalChoice) {
            case "1":
                _goalType = "Simple";
                SimpleGoal _simpleGoal = new SimpleGoal(_goalType);
                _goals.Add(_simpleGoal);
                break;
            case "2":
                _goalType = "Eternal";
                EternalGoal _eternalgoal = new EternalGoal(_goalType);
                _goals.Add(_eternalgoal);
                break;
            case "3":
                _goalType = "Checklist";
                ChecklistGoal _checklistGoal = new ChecklistGoal(_goalType);
                _goals.Add(_checklistGoal);
                break;
            default:
                Console.WriteLine("Not A Valid Choice");
                break;
        }
    }

    private void SaveGoals() {
        Console.Write("\nWhat do you want to name the file? ");
        string _name = Console.ReadLine();
        string _filename = $"{_name }.txt";

        if (File.Exists(_filename)) {
            Console.Write($"{_filename} already exists. Would you like to overwrite? (Y/N) ");
            string _userResponse = Console.ReadLine().ToLower();
            if (_userResponse == "n") {
                Console.WriteLine("Save  Canceled.");
            } else {
                using (StreamWriter outputFile = new StreamWriter(_filename)) {
                    outputFile.WriteLine(_userScore);
                    foreach (var _goal in _goals) {
                        outputFile.WriteLine(_goal.FormatGoalForSave());
                    }
                }
                Console.WriteLine("Goals successfully saved");
            }
        } else {
            using (StreamWriter outputFile = new StreamWriter(_filename)) {
                outputFile.WriteLine(_userScore);
                foreach (var _goal in _goals) {
                    outputFile.WriteLine(_goal.FormatGoalForSave());
                }
            }
            Console.WriteLine("Goals successfully saved");
        }   
    }

    private void LoadGoals() {
        string[] lines;
        Console.Write("\nWhat is the name of the file you want to open? ");
        string _name = Console.ReadLine();
        string _filename = $"{_name }.txt";
        if (File.Exists(_filename)) {
            Console.WriteLine("Loading");
            lines = File.ReadAllLines(_filename);
            _userScore = int.Parse(lines[0]);
            Console.WriteLine($"score: {_userScore}");
            
            foreach (string line in lines.Skip(1).ToArray())
            {   
                string [] _splitline = line.Split(":");
                string _goalType = _splitline[0];
                string [] _goalData = _splitline[1].Split("|");

                string _goalName = _goalData[0];
                string _description = _goalData[1];
                int _pointValue = int.Parse(_goalData[2]);
                bool _complete = bool.Parse(_goalData[3]);
                switch (_goalType) {
                    case "Checklist":
                        int _bonusPoints = int.Parse(_goalData[4]);
                        int _timesCompleted = int.Parse(_goalData[5]);
                        int _timesToCompletion = int.Parse(_goalData[6]);
                        ChecklistGoal _checklistLoad = new ChecklistGoal(_goalName, _description, _pointValue, _complete, _timesCompleted, _timesToCompletion, _goalType, _bonusPoints);
                        _goals.Add(_checklistLoad);
                        break;
                    case "Simple":
                        SimpleGoal _simpleLoad = new SimpleGoal(_goalName, _description, _pointValue, _complete, _goalType);
                        _goals.Add(_simpleLoad);
                        break;
                    case "Eternal":
                        EternalGoal _eternalLoad = new EternalGoal(_goalName, _description, _pointValue, _complete, _goalType);
                        _goals.Add(_eternalLoad);
                        break;
                }
            }
        } else {
            Console.WriteLine($"File ({_filename}) does not exists");
        }
    }

    public void DisplayGoals() {
        Console.WriteLine();
        int i = 1;
        foreach (var _goal in _goals) {
            Console.WriteLine(_goal.FormatGoalForDisplay(i));
            i++;
        }
        Console.WriteLine();
    }
    private void RecordEvent() {
        Console.WriteLine("\nHere are the Goals:");
        DisplayGoals();
        Console.Write("Which goal did you complete? ");
        int _choice = int.Parse(Console.ReadLine()) - 1;
        int _goalScore = _goals[_choice].RecordEvent();
        _userScore += _goalScore;
        Console.WriteLine($"\nCongratulations! You have earned {_goalScore} points!\nYou now have {_userScore} points.\n");
    }
}