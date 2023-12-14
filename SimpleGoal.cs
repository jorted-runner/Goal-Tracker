public class SimpleGoal : Goal {

    public SimpleGoal (string name, string description, int pointValue, bool complete, string type) : base(name, description, pointValue, type, complete)
    {

    }
    public SimpleGoal (string _type) : base(_type) {

    }
}