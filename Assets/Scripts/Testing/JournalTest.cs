using NaughtyAttributes;
using UnityEngine;

public class JournalTest : MonoBehaviour
{
    [SerializeField] private JournalData journalData;

    [Button]
    void ShutDownLaptop1()
    {
        journalData.AddNoteToCurrentDay("asdfmjhgsadfkjhasgdfkjsahdgfskadhfgsakjdfvsadfsadfsadfasdfasdfasdsdkfhgdskjhfhgsdjkfgsdfg");
    }
    
    [Button]
    void ShutDownLaptop2()
    {
        journalData.AddNoteToCurrentDay("dfsgdsfgdsfgdfgdsfgdsfjghsdfdkgjhdsgfgkjsahdfgmnasfhglkjasjbfdnglkjsjdfbglsdksjfghsdljfkghsdlfjkgdsdfgsdjfkgjsdbfgdskjfghasdasdsdfg");
    }
}
