using System.Runtime.CompilerServices;

public class ChecklistGoal : Goal {
    private int _bonusPoints;
    private int _timesToCompletion;
    private int _timesCompleted;

    public ChecklistGoal (string name, string description, int pointValue, bool complete, int timesCompleted, int timesToComplete, string type, int bonusPoints) : base(name, description, pointValue, type, complete)
    {
        _timesCompleted = timesCompleted;
        _timesToCompletion = timesToComplete;
        _bonusPoints = bonusPoints;
    }
    public ChecklistGoal (string _type) : base(_type) {
        _isComplete = false;
        _timesCompleted = 0;
    }
    protected override void SetPoints()
    {
        base.SetPoints();
        Console.Write("How many times do you need to do this goal to earn bonus points? ");
        _timesToCompletion = int.Parse(Console.ReadLine());
        Console.Write("How many bonus points? ");
        _bonusPoints = int.Parse(Console.ReadLine());

    }
    public override string FormatGoalForDisplay(int goalNum)
    {
        if (CheckCompletion()) {
            return $"{goalNum}. [X] {_goalName} ({_goalDesc}) Completed {_timesCompleted}/{_timesToCompletion}";
        } else {
            return $"{goalNum}. [ ] {_goalName} ({_goalDesc}) Completed {_timesCompleted}/{_timesToCompletion}";
        }
        
    }

    public override bool CheckCompletion() {
        if (_timesCompleted == _timesToCompletion) {
            SetIsComplete();
        }
        return _isComplete;
    }

    private void SetIsComplete() {
        _isComplete = true;
    }
    public override int RecordEvent() {
        if (!CheckCompletion()) {
            _timesCompleted += 1;
            if (_timesCompleted == _timesToCompletion) {
                return _bonusPoints;
            } else {
                return _goalPointValue;
            }
        } else {
            Console.WriteLine("Goal already complete");
            return 0;
        }

    }

    public override string FormatGoalForSave()
    {
         return $"{_goalType}:{_goalName}|{_goalDesc}|{_goalPointValue}|{_isComplete}|{_bonusPoints}|{_timesCompleted}|{_timesToCompletion}";
    }
}