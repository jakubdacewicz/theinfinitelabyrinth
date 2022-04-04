using UnityEngine;

[CreateAssetMenu(fileName = "DungeonGenerationData.asset", menuName = "DungeonGenerationData/Dungeon Data")]
public class DungeonGenerationData : ScriptableObject
{
    public string worldName;
    public string nextWorldName;
    public int numberOfCrawlers;
    public int iterationMin;
    public int iterationMax;
}
