using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderboardEntry
{
    public int Position;
    public int Score;
    
    // Note: we're using ID here as Dreamlo's "name" field, since it needs to be unique, and Name here is
    // Dreamlo's "message" field, since it doesn't.  This allows for duplicate names, and we can pretty safely use
    // a random int for the ID.
    public string ID;
    public string Name;
    
    public int Time;
    public string Date;

    public LeaderboardEntry()
    {
        ID = Random.Range(0, int.MaxValue).ToString();
    }
}
