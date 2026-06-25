using UnityEngine;

[CreateAssetMenu]
public class TrialData : ScriptableObject
{
    public int winThreshold;
    public int currentPoints;

    private void OnEnable()
    {
        currentPoints = 0;
    }

    public void AddPoint(int amount)
    {
        currentPoints += amount;
    }
    public void RemovePoint(int amount)
    {
        currentPoints -= amount;
    }

    public bool HasEnoughPoints()
    {
        return currentPoints >= winThreshold;
    }
}
