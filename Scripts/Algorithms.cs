using UnityEngine;

public static class Algorithms {
    public static int[] UnicRandom(int start, int end, int total)       //Ф-ия для получения уникальных случайных значений от start до end в количестве total
    {
        int[] temp = new int[end - start];          //Массив повторяющихся чисел
        int[] randUnic = new int[total];            //Массив уникальных чисел

        int j = 0;
        int l = 0;
        bool check;

        for (int i = 0; i < total; i++)
        {
            check = true;
            int r = Random.Range(start, end);

            if (j == 0)
            {
                temp[j++] = r;
                randUnic[l++] = r;
                continue;
            }

            for (int k = 0; k < j; k++)
            {
                if (temp[k] == r)
                {
                    check = false;
                    i--;
                    break;
                }
            }

            if (check)
            {
                temp[j++] = r;
                randUnic[l++] = r;
            }
        }

        return randUnic;
    }

    public static string FormatTime(int time)     //Преобразование time (в секундах) в формат времени ММ : CC
    {
        int minutes = 0;
        int seconds = 0;

        while ((minutes * 60 + 59) < time)
        {
            minutes += 1;
        }

        seconds = time - minutes * 60;

        string t = "";

        if (minutes < 10)
            t = "0" + minutes.ToString() + " : ";
        else
            t = minutes.ToString() + " : ";

        if (seconds < 10)
            t += "0" + seconds.ToString();
        else
            t += seconds.ToString();

        return t;
    }
}
