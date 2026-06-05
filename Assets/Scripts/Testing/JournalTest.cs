using NaughtyAttributes;
using UnityEngine;

public class JournalTest : MonoBehaviour
{
    [Button]
    void ShutDownLaptop1()
    {
        GameManager.instance.journalData.AddNoteToCurrentDay("asdfmjhgsadfkjhasgdfkjsahdgfskadhfgsakjdfvsadfsadfsadfasdfasdfasdsdkfhgdskjhfhgsdjkfgsdfg");
    }
    
    [Button]
    void ShutDownLaptop2()
    {
        GameManager.instance.journalData.AddNoteToCurrentDay("dfsgdsfgdsfgdfgdsfgdsfjghsdfdkgjhdsgfgkjsahdfgmnasfhglkjasjbfdnglkjsjdfbglsdksjfghsdljfkghsdlfjkgdsdfgsdjfkgjsdbfgdskjfghasdasdsdfg");
    }
}
