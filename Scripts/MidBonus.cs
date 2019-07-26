using UnityEngine;

public class MidBonus : Bonus { //Средний бонус
    protected override void Start()
    {
        base.Start();

        bonusHeightJump = 6.0f;
        bonusRunningSpeed = 4.0f;
        nameOfBonus = "MiddleBonus";
    }

    protected override void RandomBonusSpawn()
    {
        int r = Random.Range(0, 10);

        if ((r >= 0) && (r <= 6))
        {
            Destroy(gameObject);
        }
    }
}
