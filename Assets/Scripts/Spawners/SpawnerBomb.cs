using UnityEngine;

public class SpawnerBomb : Spawner<Bomb>
{
    public void SpawnBombAtPosition(Vector3 vector)
    {
        SpawnObject(vector);
    }
}
