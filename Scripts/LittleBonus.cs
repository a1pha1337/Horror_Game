using UnityEngine;

public class LittleBonus : Bonus {          //Маленький бонус
    protected override void Start()
    {
        base.Start();

        bonusHeightJump = 4.0f;
        bonusRunningSpeed = 2.0f;
        nameOfBonus = "LittleBonus";
    }

    protected override void RandomBonusSpawn()
    {
        int r = Random.Range(0, 10);

        if ((r >= 0) && (r <= 3))
        {
            Destroy(gameObject);
        }
    }
}
