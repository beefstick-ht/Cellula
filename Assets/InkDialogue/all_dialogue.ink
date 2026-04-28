EXTERNAL StartQuest(questId)
EXTERNAL AdvanceQuest(questId)
EXTERNAL FinishQuest(questId)

//quest Ids (quest id + "Id" for variable name
VAR FindKeyQuest1Id = "CollectItemQuest1"

//quest states (quest Id + "State" for variable name)
VAR FindKeyQuest1State = "REQUIREMENTS_NOT_MET"
=== QuestionQueer ===
I want to tell you something.
* [Okay.]
    You're gay.
    -> END
* [What is it?]
    I'm gay.
    -> END

=== npcQuest ===
{FindKeyQuest1Id :
    - "REQUIREMENTS_NOT_MET": -> requirementsNotMet
    - "CAN_START" : -> canStart
    - "IN_PROGRESS": -> inProgress
    - "CAN_FINISH": -> canFinish
    - "FINISH": -> finished
    - else: -> END
    }
    
= requirementsNotMet
-> END

= canStart
Want to help me find a key?
*[Sure]
    ~StartQuest(FindKeyQuest1Id)
    I am not sure where it is
    -> END
* [No]
    -> END
-> END

= inProgress
Did you find me that key?
-> END

= canFinish
//not possible for this quest
-> END

= finished
Thank you for finding my key! Here is a reward.
-> END

