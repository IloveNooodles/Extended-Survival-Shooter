using UnityEngine;

public interface IFactory
{
    GameObject FactoryMethod(int tag, Vector3 position, Quaternion rotation);
    GameObject FactoryMethodLimitedEnemy(int tag, Vector3 position, Quaternion rotation);
}
