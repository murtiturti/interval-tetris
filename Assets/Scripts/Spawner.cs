using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;

    public Block Spawn(Vector3 position, string targetNote, string noteName)
    {
        var spawned = Instantiate(blockPrefab, position, Quaternion.identity);
        var spawnedScript = spawned.GetComponent<Block>();
        var block = new BlockData(130f, noteName, targetNote);
        spawnedScript.blockData = block;
        return spawnedScript;
    }
}
