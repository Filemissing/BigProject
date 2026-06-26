using UnityEngine;

public class DayAdvancer : MonoBehaviour
{
    public void AdvanceDay()
    {
        GameManager.instance.AdvanceDay();
    }
}
