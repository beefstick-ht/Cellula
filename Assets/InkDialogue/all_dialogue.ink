=== QuestionQueer ===
I want to tell you something.
* [Okay.]
    You're gay.
    -> END
* [What is it?]
    I'm gay.
    -> END

=== npcQuest ===
{ questCompleted:
    Wow, I didn't know you were a girl kisser...
    Maybe you can find a gun to help me?
- else:
    Why are you talking to me?
}
-> END
VAR questCompleted =  true