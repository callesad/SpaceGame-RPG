using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Person", menuName = "People")]
public class PersonStatsSO : ScriptableObject
{
    public string characterName;
    public int age;
    public Gender gender;
    public float relationshipPointsWithPlayer;
    public PersonState state;
    public RelationshipState RelationshipState;
}

public enum Gender
{
    Male = 0,
    Female = 1,
    AttackHellocopter = 2,
    Transformer = 3,
    //Beaver = 4, //
    //OompaLoompa = 5, //
    SpaceOctopus = 6,
    //Jeff = 7, //
    //TomasTheTankEngine = 8, //
    //KeyboardWarrior = 9, //
    //RainbowPerson = 10, //
    Unvaccinated = 11,
    god = 12

}