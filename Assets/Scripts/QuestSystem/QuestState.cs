using UnityEngine;

public enum QuestState 
{
  REQUIREMENTS_NOT_MET,
  CAN_START,
  IN_PROGRESS,
  CAN_FINISH,
  FINISHED
}

public enum QuestType
{
    NPC_GIVEN, //requires npc
    DIRECTIVE  //starts automatically based on game events
}
