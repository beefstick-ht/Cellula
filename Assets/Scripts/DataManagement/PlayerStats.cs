using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats",
    menuName = "Scriptable Objects/PlayerStats")]
public class PlayerStats : ScriptableObject
{
    public float maxHealth = 100f;
    public float moveSpeed = 5f;
 
}