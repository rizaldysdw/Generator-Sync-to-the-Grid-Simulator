[System.Serializable]

public class Objective
{
    public string description;
    public bool isCompleted;

    public Objective(string description)
    {
        this.description = description;
        this.isCompleted = false;
    }
}
