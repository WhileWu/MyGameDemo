using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCharacter : Character {

    public override void OnDie()
    {
        base.OnDie();
        //掉落道具
        ItemData item = WorldItemManager.Instance.CreateItem(Random.Range(1001, 1005));
        ModelInfo prefabModel = Resources.Load<ModelInfo>(item.Json.modelPath);
        ModelInfo model = Instantiate(prefabModel, transform.position, Quaternion.identity);
        model.itemData = item;
    }
}
