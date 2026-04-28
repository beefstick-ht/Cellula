EXTERNAL StartQuest(questId)
EXTERNAL AdvanceQuest(questId)
EXTERNAL FinishQuest(questId)

=== QuestionQueer ===
I want to tell you something.
* [Okay.]
    You're gay.
    -> END
* [What is it?]
    I'm gay.
    -> END

=== npcQuest ===
Want to help me find a key?
*[Sure]
    ~StartQuest("FindKeyQuest1")
    I am not sure where it is
    -> END
* [No]
    -> END