public class EternalGoal : Goal {

    public EternalGoal (string name, string description, int pointValue, bool complete, string type) : base(name, description, pointValue, type, complete)
    {

    }
    public EternalGoal (string _type) : base(_type) {

    }
    public override int RecordEvent() {
        return _goalPointValue;
    }
}