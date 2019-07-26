using UnityEngine;

public class HighBonus : Bonus {        //Большой бонусы
    protected override void Start()
    {
        base.Start();

        bonusHeightJump = 8.0f;
        bonusRunningSpeed = 6.0f;
        nameOfBonus = "HighBonus";
    }

    protected override void RandomBonusSpawn()
    {
        int r = Random.Range(0, 10);

        if ((r >= 0) && (r <= 8))
        {
            Destroy(gameObject);
        }
    }
}
