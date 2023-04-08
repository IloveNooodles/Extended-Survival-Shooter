using UnityEngine;

public interface IEnemy
{
    int Id { get; set; }
    void Death();
    void TakeDamage(int amount, Vector3 hitPoint);
}
