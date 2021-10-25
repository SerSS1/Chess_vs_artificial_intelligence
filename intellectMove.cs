using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public class intellectMove : Form1
    {
        public static int levelOfDifficulty = 7;                    //Колличество просчитываемых компьютером шагов наперёд (уровень сложности)
        public static int[,,] summa = new int[10, 1000000, 15];       //0-9)Номера по очерёдности сделанных ходов (путь до этой точки)
        public static string testText = "";                       //11)Х__12)У__13)Оценочная сумма__14)Сумма побитых фигур на данном этапе
        public static int[] numberInLevel = new int[10];        //Колличество возможных вариаций ходов на каждом уровне
        public static int[] bestM = new int[20];             //0)Х_1)У_2)Лучшая сумма
        public static int[,] unwantedSteps = new int[10, 10];       //Нежелательные и запрещённые хода
        public static int[] activeAddress = new int[10];
        public static int[,] killedFigure = new int[10, 10];        //Фигуры, которые уже были побиты до этого момента (что бы не побить одну фигуру дважды)


        public static void calculateTravel(int x1, int y1, int figure)
        {
            player = 2;
            click = 0;

            switch (figure)
            {
                case 1:                 //ладья
                    #region
                    break;
                    #endregion
                case 2:                 //лошадь
                    #region
                    break;
                    #endregion
                case 3:                 //офицер
                    #region
                    break;
                    #endregion
                case 4:                 //дамка
                    #region
                    break;
                    #endregion
                case 5:                 //король
                    #region
                    break;
                    #endregion
                    
                default:
                    MessageBox.Show("Ошибка1");
                    break;
            }
        }

        public static void horseSumm(int x, int y)
        {
            numberInLevel = new int[10];
            player = 2;
            int[] addr = new int[10];
            unwantedMove();

            for (int i = 1; i < 3; i++)         //Запускаем цикл проверки всех возможных ходов
            {
                for (int j = 1; j < 3; j++)
                {
                    #region
                    if (i == j)
                    {
                        continue;
                    }
                    if ((x + i) < 9 && (y + j) < 9 && (unwantedSteps[x + i, y + j] == 0 || komanda[x + i, y + j] == 1))
                    {
                        addr[0] = numberInLevel[0];
                        saveMove(x + i, y + j, 0, priceFig(x + i, y + j), addr, unwantedSteps, 3);
                        numberInLevel[0]++;
                    }
                    if ((x - i) > 0 && (y - j) > 0 && (unwantedSteps[x - i, y - j] == 0 || komanda[x - i, y - j] == 1))
                    {
                        addr[0] = numberInLevel[0];
                        saveMove(x - i, y - j, 0, priceFig(x - i, y - j), addr, unwantedSteps, 3);
                        numberInLevel[0]++;
                    }
                    if ((x + i) < 9 && (y - j) > 0 && (unwantedSteps[x + i, y - j] == 0 || komanda[x + i, y - j] == 1))
                    {
                        addr[0] = numberInLevel[0];
                        saveMove(x + i, y - j, 0, priceFig(x + i, y - j), addr, unwantedSteps, 3);
                        numberInLevel[0]++;
                    }
                    if ((x - i) > 0 && (y + j) < 9 && (unwantedSteps[x - i, y + j] == 0 || komanda[x - i, y + j] == 1))
                    {
                        addr[0] = numberInLevel[0];
                        saveMove(x - i, y + j, 0, priceFig(x - i, y + j), addr, unwantedSteps, 3);
                        numberInLevel[0]++;
                    }
                    #endregion
                }
            }
        }
        public static void horseSumm(int lev, int[] addr, int sum, int[,] unwSteps, int x, int y)
            {
                if (lev < levelOfDifficulty)
                {
                    activeAddress[lev] = 0;
                    for (int a = 0; a < lev; a++)       //Записываем в массив пройденный путь до этого места
                    {
                        activeAddress[a] = addr[a];
                    }
                    for (int i = 1; i < 3; i++)         //Запускаем цикл проверки всех возможных ходов
                    {
                        for (int j = 1; j < 3; j++)
                        {
                            #region
                            if (i == j)
                            {
                                continue;
                            }
                            if ((x + i) < 9 && (y + j) < 9 && (unwSteps[x + i, y + j] == 0 || komanda[x + i, y + j] == 1))
                            {
                                saveMove(x + i, y + j, lev, sum, addr, unwSteps, 3);
                            }
                            if ((x - i) > 0 && (y - j) > 0 && (unwSteps[x - i, y - j] == 0 || komanda[x - i, y - j] == 1))
                            {
                                saveMove(x - i, y - j, lev, sum, addr, unwSteps, 3);
                            }
                            if ((x + i) < 9 && (y - j) > 0 && (unwSteps[x + i, y - j] == 0 || komanda[x + i, y - j] == 1))
                            {
                                saveMove(x + i, y - j, lev, sum, addr, unwSteps, 3);
                            }
                            if ((x - i) > 0 && (y + j) < 9 && (unwSteps[x - i, y + j] == 0 || komanda[x - i, y + j] == 1))
                            {
                                saveMove(x - i, y + j, lev, sum, addr, unwSteps, 3);
                            }
                            #endregion
                        }
                    }
                }
                else {
                        return;
                }
            }
        public static void ladyaSumm(int x, int y)
        {
            numberInLevel = new int[10];
            player = 2;
            int[] addr = new int[10];
            unwantedMove();

            for (int i = 1; x + i < 9; i++)
            {
                if (x + i < 9 && unwantedSteps[x + i, y] > 0 && unwantedSteps[x + i, y] % 5 == 0)
                {
                    continue;
                }
                else if (x + i < 9 && (unwantedSteps[x + i, y] == 0 || komanda[x + i, y] == 1))
                {
                    addr[0] = numberInLevel[0];
                    saveMove(x + i, y, 0, priceFig(x + i, y), addr, unwantedSteps, 5);
                    if (komanda[x + i, y] == 1)
                    {
                        break;
                    }
                }
                else break;
            }
            for (int i = 1; x - i > 0; i++)
            {
                if (x - i > 0 && unwantedSteps[x - i, y] > 0 && unwantedSteps[x - i, y] % 5 == 0)
                {
                    continue;
                }
                else if (x - i > 0 && (unwantedSteps[x - i, y] == 0 || komanda[x - i, y] == 1))
                {
                    addr[0] = numberInLevel[0];
                    saveMove(x - i, y, 0, priceFig(x - i, y), addr, unwantedSteps, 5);
                    if (komanda[x - i, y] == 1)
                    {
                        break;
                    }
                }
                else break;
            }
            for (int i = 1; y + i < 9; i++)
            {
                if (y + i < 9 && unwantedSteps[x, y + i] > 0 && unwantedSteps[x, y + i] % 5 == 0)
                {
                    continue;
                }
                else if (y + i < 9 && (unwantedSteps[x, y + i] == 0 || komanda[x, y + i] == 1))
                {
                    addr[0] = numberInLevel[0];
                    saveMove(x, y + i, 0, priceFig(x, y + i), addr, unwantedSteps, 5);
                    if (komanda[x, y + i] == 1)
                    {
                        break;
                    }
                }
                else break;
            }
            for (int i = 1; y - i > 0; i++)
            {
                if (y - i > 0 && unwantedSteps[x, y - i] > 0 && unwantedSteps[x, y - i] % 5 == 0)
                {
                    continue;
                }
                else if (y - i > 0 && (unwantedSteps[x, y - i] == 0 || komanda[x, y - i] == 1))
                {
                    addr[0] = numberInLevel[0];
                    saveMove(x, y - i, 0, priceFig(x, y - i), addr, unwantedSteps, 5);
                    if (komanda[x, y - i] == 1)
                    {
                        break;
                    }
                }
                else break;
            }
        }
        public static void ladyaSumm(int lev, int[] addr, int sum, int[,] unwSteps, int x, int y)
        {
            if (lev < levelOfDifficulty)
            {
                activeAddress[lev] = 0;
                for (int a = 0; a < lev; a++)       //Записываем в массив пройденный путь до этого места
                {
                    activeAddress[a] = addr[a];
                }
                for (int i = 1; x + i < 9; i++)
                {
                    if (x + i < 9 && unwantedSteps[x + i, y] > 0 && unwantedSteps[x + i, y] % 5 == 0)
                    {
                        continue;
                    }
                    else if (x + i < 9 && (unwSteps[x + i, y] == 0 || komanda[x + i, y] == 1))
                    {
                        saveMove(x + i, y, lev, priceFig(x + i, y), addr, unwSteps, 5);
                        if (komanda[x + i, y] == 1)
                        {
                            break;
                        }
                    }
                    else break;
                }
                for (int i = 1; x - i > 0; i++)
                {
                    if (x - i > 0 && unwantedSteps[x - i, y] > 0 && unwantedSteps[x - i, y] % 5 == 0)
                    {
                        continue;
                    }
                    else if (x - i > 0 && (unwSteps[x - i, y] == 0 || komanda[x - i, y] == 1))
                    {
                        saveMove(x - i, y, lev, priceFig(x - i, y), addr, unwSteps, 5);
                        if (komanda[x - i, y] == 1)
                        {
                            break;
                        }
                    }
                    else break;
                }
                for (int i = 1; y + i < 9; i++)
                {
                    if (y + i < 9 && unwantedSteps[x, y + i] > 0 && unwantedSteps[x, y + i] % 5 == 0)
                    {
                        continue;
                    }
                    else if (y + i < 9 && (unwSteps[x, y + i] == 0 || komanda[x, y + i] == 1))
                    {
                        saveMove(x, y + i, lev, priceFig(x, y + i), addr, unwSteps, 5);
                        if (komanda[x, y + i] == 1)
                        {
                            break;
                        }
                    }
                    else break;
                }
                for (int i = 1; y - i > 0; i++)
                {
                    if (y - i > 0 && unwantedSteps[x, y - i] > 0 && unwantedSteps[x, y - i] % 5 == 0)
                    {
                        continue;
                    }
                    else if (y - i > 0 && (unwSteps[x, y - i] == 0 || komanda[x, y - i] == 1))
                    {
                        saveMove(x, y - i, lev, priceFig(x, y - i), addr, unwSteps, 5);
                        if (komanda[x, y - i] == 1)
                        {
                            break;
                        }
                    }
                    else break;
                }
            }
            else
            {
                return;
            }
        }
        public static void saveMove(int xn, int yn, int lev, int sum, int[] addr, int[,] unwSteps, int figura)
        {
            summa[lev, numberInLevel[lev], 11] = xn;               //Записываем координату Х
            summa[lev, numberInLevel[lev], 12] = yn;               //Записываем координату У
            kl[xn, yn].BackgroundImage = null;
            if (kl[xn, yn].BackColor == Color.White || kl[xn, yn].BackColor == Color.Black)
            {
                kl[xn, yn].BackColor = Color.Gray;
            }
            if (komanda[xn, yn] == 1 && unwSteps[xn, yn] == 1 && killedFigure[xn, yn] == 0)           //Если в этой клетке стоит незащищённая фигура противника и она ещё не была побита ранее
            {
                summa[lev, numberInLevel[lev], 13] = priceFig(xn, yn);                //Узнаем цену побитой фигуры
                summa[lev, numberInLevel[lev], 14] = summa[lev, numberInLevel[lev], 13] + sum;          //Записываем сумму побитых ранее фигур с побитой сейчас
                killedFigure[xn, yn] = lev;                //Записываем место побитой фигуры, что бы эту фигуру не могли побить дважды
                fi = board[xn, yn];                   //Нежелательные хода, которые были под боем этой побитой фигурой, помечаем как безопасные клетки
                x1 = xn;
                y1 = yn;
                allPossibleMove(1);
                for (int f = 0; f < numberWheMove; f++)
                {
                    if (komanda[whereMove[f, 0], whereMove[f, 1]] == 0)
                    {
                        unwSteps[whereMove[f, 0], whereMove[f, 1]] -= 5;
                        kl[whereMove[f, 0], whereMove[f, 1]].BackColor = Color.DarkOrange;
                    }
                    else if (komanda[whereMove[f, 0], whereMove[f, 1]] == 1)
                    {
                        unwSteps[whereMove[f, 0], whereMove[f, 1]] -= 7;
                        kl[whereMove[f, 0], whereMove[f, 1]].BackColor = Color.DarkGreen;
                        if (unwSteps[whereMove[f, 0], whereMove[f, 1]] <= 0)
                        {
                            unwSteps[whereMove[f, 0], whereMove[f, 1]] = 1;
                        }
                    }
                    else if (komanda[whereMove[f, 0], whereMove[f, 1]] == 2)
                    {
                        continue;
                    }
                    else
                    {
                        MessageBox.Show("error3");
                    }
                    if (unwSteps[whereMove[f, 0], whereMove[f, 1]] < 0)
                    {
                        unwSteps[whereMove[f, 0], whereMove[f, 1]] = 0;
                    }
                }
            }
            else if (komanda[xn, yn] == 1 && killedFigure[xn, yn] != 0)         //Если в этой клетке стоит фигура противника и она уже была побита ранее
            {
                summa[lev, numberInLevel[lev], 13] = 0;
                summa[lev, numberInLevel[lev], 14] = sum;          //Записываем только сумму побитых ранее фигур
            }
            else if (komanda[xn, yn] == 1 && unwSteps[xn, yn] % 7 == 0)        //Если в этой клетке стоит защищенная фигура
            {
                summa[lev, numberInLevel[lev], 10] = 1;                     //Так как эта фигура попадает под бой она не может ходить дальше(цифра 1 означает, что дальше для этой фигуры не будет просчитывать хода)
                summa[lev, numberInLevel[lev], 13] = priceFig(xn, yn);
                summa[lev, numberInLevel[lev], 14] = summa[lev, numberInLevel[lev], 13] + sum;
                summa[lev, numberInLevel[lev], 14] -= figura;                                            //Учитываем потерю своей фигуры(так как наша фигура бьёт чужую защищённую, она попадает под бой и мы её теряем)
            }
            else                                            //Если клетка пустая, то просто записываем сумму побитых фигур до этого хода
            {
                summa[lev, numberInLevel[lev], 13] = 0;
                summa[lev, numberInLevel[lev], 14] = sum;
            }
            if (lev == 0)                                               //Записываем адрес(путь) этого хода
            {
                summa[0, numberInLevel[0], 0] = numberInLevel[0];
            }
            else                                              //Записываем адрес(путь) этого хода
            {
                for (int a = 0; a < lev; a++)
                {
                    summa[lev, numberInLevel[lev], a] = addr[a];
                }
            }
            if (summa[lev, numberInLevel[lev], 10] != 1 && (lev + 1) < levelOfDifficulty)       //Запускаем рекурсию
            {
                //kl[summa[lev, numberInLevel[lev], 11], summa[lev, numberInLevel[lev], 12]].BackgroundImage = null;
                //kl[summa[lev, numberInLevel[lev], 11], summa[lev, numberInLevel[lev], 12]].BackColor = Color.Gray;
                switch (figura)                     //Рекурсия
                {
                    case 3:
                        horseSumm(lev + 1, activeAddress, summa[lev, numberInLevel[lev], 14], unwSteps, summa[lev, numberInLevel[lev], 11], summa[lev, numberInLevel[lev], 12]);
                        break;
                    case 5:
                        ladyaSumm(lev + 1, activeAddress, summa[lev, numberInLevel[lev], 14], unwSteps, summa[lev, numberInLevel[lev], 11], summa[lev, numberInLevel[lev], 12]);
                        break;
                }
                /*for (int r = 1; r < 9; r++)             //Убираем значения побитых фигур
                {
                    for (int e = 1; e < 9; e++)
                    {
                        if (killedFigure[r, e] == lev)
                        {
                            killedFigure[r, e] = 0;
                        }
                    }
                }*/
            }
            for (int r = 1; r < 9; r++)             //Убираем значения побитых фигур
            {
                for (int e = 1; e < 9; e++)
                {
                    if (killedFigure[r, e] == lev)
                    {
                        killedFigure[r, e] = 0;
                    }
                }
            }
            activeAddress[lev]++;               //Какой по счёту в данной вариации
            numberInLevel[lev]++;              //Увеличиваем общий счётчик ходов на этом уровне
        }
        static int priceFig(int prX, int prY)
        {
            switch (board[prX, prY]) {
                case 1:
                    return 5;
                case 2:
                    return 3;
                case 3:
                    return 3;
                case 4:
                    return 10;
                case 5:
                    return 15;
                case 8:
                    return 1;
                default:
                    return 0;
            }
        }
        public static void bestMove()
        {
            for (int f = levelOfDifficulty - 1; f >= 0; f--)
            {
                if (numberInLevel[f] == 0)
                {
                    continue;
                }
                else
                {
                    for (int j = numberInLevel[f]; j >= 0; j--)
                    {
                        if (summa[f, j, 14] >= bestM[2])
                        {
                            bestM[0] = summa[f, j, 0];
                            bestM[2] = summa[f, j, 14];
                            for (int u = 0; u < levelOfDifficulty; u++)
                            {
                                bestM[u + 10] = summa[f, j, u];
                            }
                            //testText += Convert.ToString("(" + summa[f, j, 14] + ")" + bestM[2]);
                        }
                    }
                }
            }
            bestM[1] = summa[0, bestM[0], 12];
            bestM[0] = summa[0, bestM[0], 11];
            testText += Convert.ToString("(" + bestM[0] + "-" + bestM[1] + ")" + bestM[2]);
        }
        public static void unwantedMove()
        {
            for (int w = 1; w < 9; w++)
            {
                for (int q = 1; q < 9; q++)
                {
                    if (komanda[w, q] == 2)             //1 - фигура пользователя
                    {                                   //2 - фигура компьютера
                        unwantedSteps[w, q] = 2;        //5 - опасный ход (попадает под бой)
                    }                                   //7 - фигура пользователя (которая стоит под защитой другой фигуры)
                    else if (komanda[w, q] == 1)
                    {
                        fi = board[w, q];
                        x1 = w;
                        y1 = q;
                        allPossibleMove(1);
                        for (int k = 0; k < numberWheMove; k++)
                        {
                            if (komanda[whereMove[k, 0], whereMove[k, 1]] == 1)
                            {
                                if (unwantedSteps[whereMove[k, 0], whereMove[k, 1]] == 1 || unwantedSteps[whereMove[k, 0], whereMove[k, 1]] == 0)
                                {
                                    unwantedSteps[whereMove[k, 0], whereMove[k, 1]] = 7;
                                }
                                else
                                {
                                    unwantedSteps[whereMove[k, 0], whereMove[k, 1]] += 7;
                                }
                            }
                            else if(komanda[whereMove[k, 0], whereMove[k, 1]] == 2)
                            {
                                continue;
                            }
                            else
                            {
                                unwantedSteps[whereMove[k, 0], whereMove[k, 1]] += 5;
                            }
                        }
                        if (unwantedSteps[w, q] == 0)
                        {
                            unwantedSteps[w, q] = 1;
                        }
                    }
                    else if (unwantedSteps[w, q] == 5 || unwantedSteps[w, q] == 2)
                    {
                        continue;
                    }
                    else
                    {
                        unwantedSteps[w, q] = 0;
                    }
                }
            }
            /**/for (int k = 1; k < 9; k++)
            {
                for (int t = 1; t < 9; t++)
                {
                    testText += unwantedSteps[k, t] + "  ";
                }
                testText += "\n";
            }
        }
        }
}
