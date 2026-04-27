//-> QuestionQueer
//-> npcQuest
VAR questCompleted = true

// ~ questCompleted = false, variable modification during dialogue
=== QuestionQueer ===
I want to tell you something
* [Okay.]
    You're gay.
  
+ [What is it?]
    I'm gay.
 - -> END
-> QuestionQueer

=== npcQuest ===
{questCompleted:
    Wow, I didn't know you were a girl kisser...
    Maybe you can find a gun to help me?
  - else:
    Why are you talking to me?
}
-> DONE

