using UnityEngine;

public class ObservationGiver : MonoBehaviour
{
    public void GiveObservation(string observation)
    {
        GameManager.instance.journalData.AddNoteToCurrentDay(observation);
    }
}
