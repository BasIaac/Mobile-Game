using System.Numerics;

public interface IBoardable
{
    void SetMaster(LevelInitializer gridMaster, Vector2 pos); //TODO : Unifier les Grids
    void SetPosition(Vector2 newPos);
    Vector3 GetWorldPosition();
}