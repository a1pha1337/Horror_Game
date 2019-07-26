using UnityEngine;
using System.IO;
using System;

public abstract class Bonus : MonoBehaviour {       //Абстрактный клвсс бонусов
    protected float bonusRunningSpeed;          //Бонусная скорость бега
    protected float bonusHeightJump;            //Бонусная высота прыжка
    protected bool activated = false;           //Активирован ли бонус
    protected float countTime = 0f;             //Время действия бонуса
    protected string nameOfBonus;               //Имя бонуса

    protected virtual void Start()
    {
        RandomBonusSpawn();
    }

    protected void Update()
    {
        if (activated)
            countTime -= Time.deltaTime;

        if (countTime < 0)
            StartCountBonus();
    }

    protected abstract void RandomBonusSpawn();      //Рандомное удаление бонусов с игрового мира

    protected void OnTriggerEnter(Collider col)     //Собитие при колизии
    {
        if (col.tag == "Player")
        {
            activated = true;
            countTime = 20f;
            StartCountBonus();
            Destroy(GetComponent<MeshRenderer>());
        }
    }

    protected void StartCountBonus()            //Работа бонуса
    {
        if (countTime < 0)
        {

            StreamWriter log = new StreamWriter("log.dat", true);

            activated = false;

            Debug.Log("Bonus " + nameOfBonus + " has been disactivated!");
            log.WriteLine("[GAME](" + DateTime.Now + "): Bonus " + nameOfBonus + " has been disactivated!");

            StaticValues.jumpHeight -= bonusHeightJump;
            StaticValues.runningSpeed -= bonusRunningSpeed;

            log.Close();
            Destroy(gameObject);
        }
        else
        {
            StreamWriter log = new StreamWriter("log.dat", true);

            Debug.Log("Bonus " + nameOfBonus + " has been activated for 20 seconds!");
            log.WriteLine("[GAME](" + DateTime.Now + "): Bonus " + nameOfBonus + " has been activated for 20 seconds!");

            StaticValues.jumpHeight += bonusHeightJump;
            StaticValues.runningSpeed += bonusRunningSpeed;

            log.Close();
        }
    }
}
