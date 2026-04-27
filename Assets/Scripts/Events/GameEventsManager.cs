using UnityEngine;

public class GameEventsManager : MonoBehaviour
{
   public static GameEventsManager instance { get; private set; }

    public DialogueEvents dialogueEvents;
    public QuestEvents questEvents;
    public PlayerEvents playerEvents;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject); // destroy duplicate
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        //initialize all events
        dialogueEvents = new DialogueEvents();
        questEvents = new QuestEvents();
       playerEvents =  new PlayerEvents();
}
}
