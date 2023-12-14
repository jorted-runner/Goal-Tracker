public abstract class Goal {
    protected string _goalType;
    protected string _goalName;
    protected string _goalDesc;
    protected int _goalPointValue;
    protected bool _isComplete;

    public Goal (string goalType) {
        SetName();
        SetDesc();
        SetPoints();
        _goalType = goalType; 
        _isComplete = false;
    }
    public Goal (string name, string description, int pointValue, string type, bool complete) {
        _goalName = name;
        _goalDesc = description;
        _goalPointValue = pointValue;
        _goalType = type;
        _isComplete = complete;
    }

    private void SetName() {
        Console.Write("\nWhat is the name of your goal? ");
        _goalName = Console.ReadLine();
    }
    private void SetDesc() {
        Console.Write("Give a short description of the goal: ");
        _goalDesc = Console.ReadLine();
    }
    protected virtual void SetPoints() {
        Console.Write("How many points are associated with completing this goal? ");
        _goalPointValue = int.Parse(Console.ReadLine());
    }
    public virtual string FormatGoalForDisplay(int goalNum) {
        if (CheckCompletion()) {
            return $"{goalNum}. [X] {_goalName} ({_goalDesc})";
        } else {
            return $"{goalNum}. [ ] {_goalName} ({_goalDesc})";
        }
        
    }

    public virtual string FormatGoalForSave() {
        return $"{_goalType}:{_goalName}|{_goalDesc}|{_goalPointValue}|{_isComplete}";
    }

    public virtual int RecordEvent() {
        if (!_isComplete) {
            _isComplete = true;
            return _goalPointValue;
        } else {
            Console.WriteLine("Goal already complete.");
            return 0;
        }        
    }
    public virtual bool CheckCompletion() {
        return _isComplete;
    }
}