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
    public partial class Form1 : Form
    {
        public static int[,] board = new int[10, 10];               //Размещение всех фигур на доске
        public static Button[,] kl = new Button[10, 10];                //Массив кнопок
        public static int[,] komanda = new int[10, 10];             //Размещение фигур команды
        public static int click = 0;
        public static int player = 1;
        public static int fi = 0;
        public static int x1 = 0;
        public static int y1 = 0;
        static int x2 = 0;
        static int y2 = 0;
        string name = "";
        public static int[,] whereMove = new int[200,2];               //Возможные хода для фигуры
        public static int numberWheMove = 0;                  //Колличество возможных ходов для фигуры

        static Bitmap pusto = new Bitmap("D:\\image\\pusto.jpg");
        static Bitmap peshka = new Bitmap("D:\\image\\peshka.jpg");
        static Bitmap horse = new Bitmap("D:\\image\\horse.jpg");
        static Bitmap oficer = new Bitmap("D:\\image\\oficer.jpg");
        static Bitmap ladya = new Bitmap("D:\\image\\ladya.jpg");
        static Bitmap dama = new Bitmap("D:\\image\\dama.jpg");
        static Bitmap korol = new Bitmap("D:\\image\\korol.jpg");

        static Bitmap peshka1 = new Bitmap("D:\\image\\peshka1.jpg");
        static Bitmap horse1 = new Bitmap("D:\\image\\horse1.jpg");
        static Bitmap oficer1 = new Bitmap("D:\\image\\oficer1.jpg");
        static Bitmap ladya1 = new Bitmap("D:\\image\\ladya1.jpg");
        static Bitmap dama1 = new Bitmap("D:\\image\\dama1.jpg");
        static Bitmap korol1 = new Bitmap("D:\\image\\korol1.jpg");

        Bitmap[,] figure = new Bitmap[3, 9] {{ladya, horse, oficer, dama, korol, oficer, horse, ladya, peshka},
                                             {ladya, horse, oficer, dama, korol, oficer, horse, ladya, peshka},
                                             {ladya1, horse1, oficer1, dama1, korol1, oficer1, horse1, ladya1, peshka1}};
        int[] startF = new int[] {0, 1, 2, 3, 4, 5, 3, 2, 1, 0};                //Начальное расположение фигур на доске
        


        public Form1()
        {
            InitializeComponent();
            setBut();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        void setBut()
        {
            int k = 0;
            for (int i = 0; i < 10; i++)     //добавляем кнопки
            {
                for (int j = 0; j < 10; j++)
                {
                    kl[i, j] = new Button();
                    kl[i, j].Name = Convert.ToString(i) + Convert.ToString(j);
                    kl[i, j].Size = new System.Drawing.Size(50, 50);
                    kl[i, j].Left = 21 + (j-1) * 50;
                    kl[i, j].Top = 40 + (i-1) * 50;
                    kl[i, j].MouseDown += new MouseEventHandler(this.Button_Click);
                    this.Controls.Add(kl[i, j]);
                    board[i, j] = 0;
                    if ((k-1) % 9 == 0) k++;
                    if ((k-1) % 2 == 0)
                    {
                        kl[i, j].BackColor = Color.Black;
                    }
                    else
                    {
                        kl[i, j].BackColor = Color.White;
                    }
                    k++;
                    if (i == 0 || i == 9 || j == 0 || j == 9)
                    {
                        kl[i, j].Left = 21;
                        kl[i, j].Top = 40;
                        kl[i, j].Size = new System.Drawing.Size(0, 0);
                    }
                }
            }
            startPosition();
            
            //intellectMove.ladyaSumm(5, 5);
            intellectMove.horseSumm(4, 3);
            kl[4, 3].BackColor = Color.Green;
            intellectMove.bestMove();

            for (int i = 0; i < intellectMove.levelOfDifficulty; i++)
            {
                for (int j = 0; j <= intellectMove.numberInLevel[i]; j++)
                {
                    for (int h = 0; h < intellectMove.levelOfDifficulty; h++)
                    {
                        if (intellectMove.summa[i, j, h] != intellectMove.bestM[h + 10])
                        {
                            break;
                        }
                        else if (h == intellectMove.levelOfDifficulty - 1)
                        {
                            intellectMove.testText += "{" + intellectMove.summa[i, j, 11] + "," + intellectMove.summa[i, j, 12] + "}" + "*" + intellectMove.summa[i, j, 14] + "*";
                            kl[intellectMove.summa[i, j, 11], intellectMove.summa[i, j, 12]].BackColor = Color.Blue;
                        }
                    }
                }
                intellectMove.testText += ".";
            }
            intellectMove.testText += Convert.ToString("[" + intellectMove.numberInLevel[intellectMove.levelOfDifficulty - 1] + "]");            
            MessageBox.Show(intellectMove.testText);
            kl[intellectMove.bestM[0], intellectMove.bestM[1]].BackColor = Color.Red;
        }
        void Button_Click(object sender, MouseEventArgs e)
        {
            if (player == 1) {
                Button b = (Button)sender;
                name = b.Name;
                if (click == 0)             //Первый клик; выбор фигуры
                {
                    click = 1;
                    x1 = name[0] - 48;
                    y1 = name[1] - 48;
                    fi = board[x1, y1];
                    if (fi == 0)
                    {
                        click = 0;
                        MessageBox.Show("Ничего не выбрано");
                        return;
                    }
                    else if (player != komanda[x1, y1])
                    {
                        click = 0;
                        MessageBox.Show("Это не ваша фигура");
                        return;
                    }

                    //Дополнение: возможные хода
                    //allPossibleMove();                //Поиск возможных ходов

                    /*for (int i = 0; i < numberWheMove; i++)
                    {
                        kl[whereMove[i, 0], whereMove[i, 1]].BackColor = Color.Red;
                    }
                    numberWheMove = 0;*/
                }
                else                    //Второй клик; ход фигурой
                {
                    boardRepaint();
                    click = 0;
                    x2 = name[0] - 48;
                    y2 = name[1] - 48;
                    if (komanda[x2, y2] == player)
                    {
                        click = 0;
                        MessageBox.Show("Здесь стоит ваша фигура");
                        return;
                    }

                    //Проверка возможности хода и перемещение фигуры
                    moveFigure();
                    

                    /*if (player == 1)
                    {*/
                        player = 2;
                        label1.Text = Convert.ToString("Ходят чёрные");
                    /*}
                    else
                    {
                        player = 1;
                        label1.Text = Convert.ToString("Ходят белые");
                    }*/
                    /*for (int i = 0; i < 8; i++)     //добавляем кнопки
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (komanda[i, j] == 2)
                            {
                                WindowsFormsApplication1.intellectMove.calculateTravel(i, j, board[i,j]);
                            }
                        }
                    }*/
                }
            }
            else
            {
                MessageBox.Show("Подождите, сейчас не ваш ход.");
                return;
            }
        }
        void startPosition()
        {
            for (int i = 1; i < 10; i++)
            {
                board[1, i] = startF[i];
                kl[1, i].BackgroundImage = figure[2, i-1];
                kl[1, i].BackgroundImageLayout = ImageLayout.Stretch;
                komanda[1, i] = 2;

                board[2, i] = 8;
                kl[2, i].BackgroundImage = peshka1;
                kl[2, i].BackgroundImageLayout = ImageLayout.Stretch;
                komanda[2, i] = 2;

                board[8, i] = startF[i];
                kl[8, i].BackgroundImage = figure[1, i-1]; ;
                kl[8, i].BackgroundImageLayout = ImageLayout.Stretch;
                komanda[8, i] = 1;

                board[7, i] = 8;
                kl[7, i].BackgroundImage = peshka;
                kl[7, i].BackgroundImageLayout = ImageLayout.Stretch;
                komanda[7, i] = 1;
            }
            /*--test--*/
            board[5, 2] = 1;
            kl[5, 2].BackgroundImage = figure[1, 0];
            kl[5, 2].BackgroundImageLayout = ImageLayout.Stretch;
            komanda[5, 2] = 1;
            
        }
        void boardRepaint() {
            int k = 0;
            for (int i = 0; i < 8; i++)     //Обновляем цвет кнопок
            {
                for (int j = 0; j < 8; j++)
                {
                    if (k % 9 == 0) k++;
                    if (k % 2 == 0)
                    {
                        kl[i, j].BackColor = Color.Black;
                    }
                    else
                    {
                        kl[i, j].BackColor = Color.White;
                    }
                    k++;
                }
            }
        }
        public static void allPossibleMove(int opponent)                //Поиск возможных ходов
        {
            numberWheMove = 0;
            switch (fi)
            {
                case 1:                 //ладья
                    #region
                    for (int i = 1; x1 + i < 9; i++)
                    {
                        if (board[x1 + i, y1] == 0 || komanda[x1 + i, y1] != 0)
                        {
                            whereMove[numberWheMove, 0] = x1 + i;
                            whereMove[numberWheMove, 1] = y1;
                            numberWheMove++;
                            if (komanda[x1 + i, y1] != 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int i = 1; x1 - i > 0; i++)
                    {
                        if (board[x1 - i, y1] == 0 || komanda[x1 - i, y1] != 0)
                        {
                            whereMove[numberWheMove, 0] = x1 - i;
                            whereMove[numberWheMove, 1] = y1;
                            numberWheMove++;
                            if (komanda[x1 - i, y1] != 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int i = 1; y1 + i < 9; i++)
                    {
                        if (board[x1, y1 + i] == 0 || komanda[x1, y1 + i] != 0)
                        {
                            whereMove[numberWheMove, 0] = x1;
                            whereMove[numberWheMove, 1] = y1 + i;
                            numberWheMove++;
                            if (komanda[x1, y1 + i] != 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int i = 1; y1 - i > 0; i++)
                    {
                        if (board[x1, y1 - i] == 0 || komanda[x1, y1 - i] != 0)
                        {
                            whereMove[numberWheMove, 0] = x1;
                            whereMove[numberWheMove, 1] = y1 - i;
                            numberWheMove++;
                            if (komanda[x1, y1 - i] != 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    break;
                    #endregion
                case 2:                 //лошадь
                    #region
                    for (int i = 1; i < 3; i++)
                    {
                        for (int j = 1; j < 3; j++)
                        {
                            if (i == j)
                            {
                                continue;
                            }
                            if ((x1 + i) < 9 && (y1 + j) < 9 && (board[x1 + i, y1 + j] == 0 || komanda[x1 + i, y1 + j] != 0))
                            {
                                whereMove[numberWheMove, 0] = x1 + i;
                                whereMove[numberWheMove, 1] = y1 + j;
                                numberWheMove++;
                            }
                            if ((x1 - i) > 0 && (y1 - j) > 0 && (board[x1 - i, y1 - j] == 0 || komanda[x1 - i, y1 - j] != 0))
                            {
                                whereMove[numberWheMove, 0] = x1 - i;
                                whereMove[numberWheMove, 1] = y1 - j;
                                numberWheMove++;
                            }
                            if ((x1 + i) < 9 && (y1 - j) > 0 && (board[x1 + i, y1 - j] == 0 || komanda[x1 + i, y1 - j] != 0))
                            {
                                whereMove[numberWheMove, 0] = x1 + i;
                                whereMove[numberWheMove, 1] = y1 - j;
                                numberWheMove++;
                            }
                            if ((x1 - i) > 0 && (y1 + j) < 9 && (board[x1 - i, y1 + j] == 0 || komanda[x1 - i, y1 + j] != 0))
                            {
                                whereMove[numberWheMove, 0] = x1 - i;
                                whereMove[numberWheMove, 1] = y1 + j;
                                numberWheMove++;
                            }
                        }
                    }
                    break;
                    #endregion
                case 3:                 //офицер
                    #region
                    for (int i = 1; x1 + i < 9 && y1 + i < 9; i++)
                    {
                        if (board[x1 + i, y1 + i] == 0 || komanda[x1 + i, y1 + i] != 0)
                        {
                            whereMove[numberWheMove, 0] = x1 + i;
                            whereMove[numberWheMove, 1] = y1 + i;
                            numberWheMove++;
                            if (komanda[x1 + i, y1 + i] != 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int i = 1; x1 - i > 0 && y1 - i > 0; i++)
                    {
                        if (board[x1 - i, y1 - i] == 0 || komanda[x1 - i, y1 - i] != 0)
                        {
                            whereMove[numberWheMove, 0] = x1 - i;
                            whereMove[numberWheMove, 1] = y1 - i;
                            numberWheMove++;
                            if (komanda[x1 - i, y1 - i] != 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int i = 1; y1 - i > 0 && x1 + i < 9; i++)
                    {
                        if (board[x1 + i, y1 - i] == 0 || komanda[x1 + i, y1 - i] != 0)
                        {
                            whereMove[numberWheMove, 0] = x1 + i;
                            whereMove[numberWheMove, 1] = y1 - i;
                            numberWheMove++;
                            if (komanda[x1 + i, y1 - i] != 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int i = 1; x1 - i > 0 && y1 + i < 9; i++)
                    {
                        if (board[x1 - i, y1 + i] == 0 || komanda[x1 - i, y1 + i] != 0)
                        {
                            whereMove[numberWheMove, 0] = x1 - i;
                            whereMove[numberWheMove, 1] = y1 + i;
                            numberWheMove++;
                            if (komanda[x1 - i, y1 + i] != 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    break;
                    #endregion
                case 4:                 //дамка
                    #region
                    for (int i = 1; x1 + i < 9; i++)
                    {
                        if (board[x1 + i, y1] == 0 || komanda[x1 + i, y1] != 0)
                        {
                            whereMove[numberWheMove, 0] = x1 + i;
                            whereMove[numberWheMove, 1] = y1;
                            numberWheMove++;
                            if (komanda[x1 + i, y1] != 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int i = 1; x1 - i > 0; i++)
                    {
                        if (board[x1 - i, y1] == 0 || komanda[x1 - i, y1] != 0)
                        {
                            whereMove[numberWheMove, 0] = x1 - i;
                            whereMove[numberWheMove, 1] = y1;
                            numberWheMove++;
                            if (komanda[x1 - i, y1] != 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int i = 1; y1 + i < 9; i++)
                    {
                        if (board[x1, y1 + i] == 0 || komanda[x1, y1 + i] != 0)
                        {
                            whereMove[numberWheMove, 0] = x1;
                            whereMove[numberWheMove, 1] = y1 + i;
                            numberWheMove++;
                            if (komanda[x1, y1 + i] != 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int i = 1; y1 - i > 0; i++)
                    {
                        if (board[x1, y1 - i] == 0 || komanda[x1, y1 - i] != 0)
                        {
                            whereMove[numberWheMove, 0] = x1;
                            whereMove[numberWheMove, 1] = y1 - i;
                            numberWheMove++;
                            if (komanda[x1, y1 - i] != 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int i = 1; x1 + i < 9 && y1 + i < 9; i++)
                    {
                        if (board[x1 + i, y1 + i] == 0 || komanda[x1 + i, y1 + i] != 0)
                        {
                            whereMove[numberWheMove, 0] = x1 + i;
                            whereMove[numberWheMove, 1] = y1 + i;
                            numberWheMove++;
                            if (komanda[x1 + i, y1 + i] != 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int i = 1; x1 - i > 0 && y1 - i > 0; i++)
                    {
                        if (board[x1 - i, y1 - i] == 0 || komanda[x1 - i, y1 - i] != 0)
                        {
                            whereMove[numberWheMove, 0] = x1 - i;
                            whereMove[numberWheMove, 1] = y1 - i;
                            numberWheMove++;
                            if (komanda[x1 - i, y1 - i] != 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int i = 1; y1 - i > 0 && x1 + i < 9; i++)
                    {
                        if (board[x1 + i, y1 - i] == 0 || komanda[x1 + i, y1 - i] != 0)
                        {
                            whereMove[numberWheMove, 0] = x1 + i;
                            whereMove[numberWheMove, 1] = y1 - i;
                            numberWheMove++;
                            if (komanda[x1 + i, y1 - i] != 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    for (int i = 1; x1 - i > 0 && y1 + i < 9; i++)
                    {
                        if (board[x1 - i, y1 + i] == 0 || komanda[x1 - i, y1 + i] != 0)
                        {
                            whereMove[numberWheMove, 0] = x1 - i;
                            whereMove[numberWheMove, 1] = y1 + i;
                            numberWheMove++;
                            if (komanda[x1 - i, y1 + i] != 0)
                            {
                                break;
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    break;
                    #endregion
                case 5:                 //король
                    #region
                    for (int i = -1; i < 2; i++)
                    {
                        for (int j = -1; j < 2; j++)
                        {
                            if (x1 + i < 9 && y1 + j < 9 && x1 + i > 0 && y1 + j > 0 && (board[x1 + i, y1 + j] == 0 || komanda[x1 + i, y1 + j] != 0))
                            {
                                if (i == 0 && j == 0)
                                {
                                    continue;
                                }
                                whereMove[numberWheMove, 0] = x1 + i;
                                whereMove[numberWheMove, 1] = y1 + j;
                                numberWheMove++;
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                    break;
                    #endregion
                case 8:                 //пешка
                    #region
                    if (player == 2)
                    {
                        if (x1 - 1 < 9 && x1 - 1 > 0 && y1 + 1 > 0 && y1 + 1 < 9 && (board[x1 - 1, y1 + 1] == 0 || komanda[x1 - 1, y1 + 1] == opponent))
                        {
                            whereMove[numberWheMove, 0] = x1 - 1;
                            whereMove[numberWheMove, 1] = y1 + 1;
                            numberWheMove++;
                        }
                        if (x1 - 1 < 9 && x1 - 1 > 0 && y1 - 1 > 0 && y1 - 1 < 9 && (board[x1 - 1, y1 - 1] == 0 || komanda[x1 - 1, y1 - 1] == opponent))
                        {
                            whereMove[numberWheMove, 0] = x1 - 1;
                            whereMove[numberWheMove, 1] = y1 - 1;
                            numberWheMove++;
                        }
                    }
                    else
                    {
                        if (x1 + 1 < 9 && x1 + 1 > 0 && y1 - 1 > 0 && y1 + 1 < 9 && (board[x1 + 1, y1] == 0 || komanda[x1 + 1, y1 - 1] == opponent || komanda[x1 + 1, y1 + 1] == opponent))
                        {
                            if (komanda[x1 + 1, y1 - 1] == opponent)
                            {
                                whereMove[numberWheMove, 0] = x1 + 1;
                                whereMove[numberWheMove, 1] = y1 - 1;
                                numberWheMove++;
                            }
                            if (komanda[x1 + 1, y1 + 1] == opponent)
                            {
                                whereMove[numberWheMove, 0] = x1 + 1;
                                whereMove[numberWheMove, 1] = y1 + 1;
                                numberWheMove++;
                            }
                            whereMove[numberWheMove, 0] = x1 + 1;
                            whereMove[numberWheMove, 1] = y1;
                            numberWheMove++;
                        }
                        else
                        {
                            return;
                        }
                    }
                    break;
                    #endregion

                default:
                    MessageBox.Show("Ошибка2");
                    break;
            }
        }
        void moveFigure()
        {
            switch (fi)
            {
                case 1:                 //ладья
                    #region
                    if (x1 == x2 || y1 == y2)
                    {
                        if (x1 == x2)
                        {
                            if (y1 < y2)
                            {
                                for (int i = (y1 + 1); y2 != i; i++)
                                {
                                    if (board[x1, i] != 0)
                                    {
                                        MessageBox.Show("Нельзя перепрыгнуть через фигуру");
                                        return;
                                    }
                                }
                            }
                            if (y1 > y2)
                            {
                                for (int i = (y1 - 1); y2 != i; i--)
                                {
                                    if (board[x1, i] != 0)
                                    {
                                        MessageBox.Show("Нельзя перепрыгнуть через фигуру");
                                        return;
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (x1 < x2)
                            {
                                for (int i = (x1 + 1); x2 != i; i++)
                                {
                                    if (board[i, y1] != 0)
                                    {
                                        MessageBox.Show("Нельзя перепрыгнуть через фигуру");
                                        return;
                                    }
                                }
                            }
                            if (x1 > x2)
                            {
                                for (int i = (x1 - 1); x2 != i; i--)
                                {
                                    if (board[i, y1] != 0)
                                    {
                                        MessageBox.Show("Нельзя перепрыгнуть через фигуру");
                                        return;
                                    }
                                }
                            }
                        }
                        kl[x1, y1].BackgroundImage = pusto;
                        kl[x1, y1].BackgroundImageLayout = ImageLayout.None;
                        kl[x2, y2].BackgroundImage = figure[player, 0];
                        kl[x2, y2].BackgroundImageLayout = ImageLayout.Stretch;
                        board[x1, y1] = 0;
                        board[x2, y2] = 1;
                        komanda[x1, y1] = 0;
                        komanda[x2, y2] = player;
                    }
                    else
                    {
                        MessageBox.Show("Сюда нельзя ставить эту фигуру");
                        return;
                    }
                    break;
                    #endregion
                case 2:                 //лошадь
                    #region
                    if ((y2 == y1 - 1) || (y2 == y1 + 1))
                    {
                        if ((x2 == x1 + 2) || (x2 == x1 - 2))
                        {
                            kl[x1, y1].BackgroundImage = pusto;
                            kl[x1, y1].BackgroundImageLayout = ImageLayout.None;
                            kl[x2, y2].BackgroundImage = figure[player, 1];
                            kl[x2, y2].BackgroundImageLayout = ImageLayout.Stretch;
                            board[x1, y1] = 0;
                            board[x2, y2] = 2;
                            komanda[x1, y1] = 0;
                            komanda[x2, y2] = player;
                        }
                        else
                        {
                            MessageBox.Show("Сюда нельзя ставить эту фигуру");
                            return;
                        }
                    }
                    else if ((y2 == y1 - 2) || (y2 == y1 + 2))
                    {
                        if ((x2 == x1 + 1) || (x2 == x1 - 1))
                        {
                            kl[x1, y1].BackgroundImage = pusto;
                            kl[x1, y1].BackgroundImageLayout = ImageLayout.None;
                            kl[x2, y2].BackgroundImage = figure[player, 1];
                            kl[x2, y2].BackgroundImageLayout = ImageLayout.Stretch;
                            board[x1, y1] = 0;
                            board[x2, y2] = 2;
                            komanda[x1, y1] = 0;
                            komanda[x2, y2] = player;
                        }
                        else
                        {
                            MessageBox.Show("Сюда нельзя ставить эту фигуру");
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Сюда нельзя ставить эту фигуру");
                        return;
                    }
                    break;
                    #endregion
                case 3:                 //офицер
                    #region
                    if (y1 < y2)
                    {
                        if ((x2 == x1 + y2 - y1) || (x1 == x2 + y2 - y1) || (x2 == x1 - y2 - y1) || (x1 == x2 + y2 - y1))
                        {
                            if (x1 > x2)
                            {
                                for (int i = x1 - 1, j = y1 + 1; i != x2; i--, j++)
                                {
                                    if (board[i, j] != 0)
                                    {
                                        MessageBox.Show("Нельзя перепрыгнуть через фигуру");
                                        return;
                                    }
                                }
                            }
                            if (x1 < x2)
                            {
                                for (int i = x1 + 1, j = y1 + 1; i != x2; i++, j++)
                                {
                                    if (board[i, j] != 0)
                                    {
                                        MessageBox.Show("Нельзя перепрыгнуть через фигуру");
                                        return;
                                    }
                                }
                            }
                            kl[x1, y1].BackgroundImage = pusto;
                            kl[x1, y1].BackgroundImageLayout = ImageLayout.None;
                            kl[x2, y2].BackgroundImage = figure[player, 2];
                            kl[x2, y2].BackgroundImageLayout = ImageLayout.Stretch;
                            board[x1, y1] = 0;
                            board[x2, y2] = 3;
                            komanda[x1, y1] = 0;
                            komanda[x2, y2] = player;
                        }
                        else
                        {
                            MessageBox.Show("Сюда нельзя ставить эту фигуру");
                            return;
                        }
                    }
                    else if (y1 > y2)
                    {
                        if ((x2 == x1 - y1 - y2) || (x2 == x1 + y1 - y2) || (x1 == x2 + y1 - y2) || (x1 == x2 - y1 - y2))
                        {
                            if (x1 < x2)
                            {
                                for (int i = x1 + 1, j = y1 - 1; i != x2; i++, j--)
                                {
                                    if (board[i, j] != 0)
                                    {
                                        MessageBox.Show("Нельзя перепрыгнуть через фигуру");
                                        return;
                                    }
                                }
                            }
                            if (x1 > x2)
                            {
                                for (int i = x1 - 1, j = y1 - 1; i != x2; i--, j--)
                                {
                                    if (board[i, j] != 0)
                                    {
                                        MessageBox.Show("Нельзя перепрыгнуть через фигуру");
                                        return;
                                    }
                                }
                            }

                            kl[x1, y1].BackgroundImage = pusto;
                            kl[x1, y1].BackgroundImageLayout = ImageLayout.None;
                            kl[x2, y2].BackgroundImage = figure[player, 2];
                            kl[x2, y2].BackgroundImageLayout = ImageLayout.Stretch;
                            board[x1, y1] = 0;
                            board[x2, y2] = 3;
                            komanda[x1, y1] = 0;
                            komanda[x2, y2] = player;
                        }
                        else
                        {
                            MessageBox.Show("Сюда нельзя ставить эту фигуру");
                            return;
                        }
                    }
                    break;
                    #endregion
                case 4:                 //дамка
                    #region
                    if ((x2 == x1 + y2 - y1) || (x1 == x2 + y2 - y1) || (x2 == x1 - y2 - y1) || (x1 == x2 + y2 - y1) || x1 == x2 || y1 == y2)
                    {
                        if (x1 == x2 || y1 == y2)
                        {
                            if (x1 == x2)
                            {
                                if (y1 < y2)
                                {
                                    for (int i = (y1 + 1); y2 != i; i++)
                                    {
                                        if (board[x1, i] != 0)
                                        {
                                            MessageBox.Show("Нельзя перепрыгнуть через фигуру");
                                            return;
                                        }
                                    }
                                }
                                if (y1 > y2)
                                {
                                    for (int i = (y1 - 1); y2 != i; i--)
                                    {
                                        if (board[x1, i] != 0)
                                        {
                                            MessageBox.Show("Нельзя перепрыгнуть через фигуру");
                                            return;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (x1 < x2)
                                {
                                    for (int i = (x1 + 1); x2 != i; i++)
                                    {
                                        if (board[i, y1] != 0)
                                        {
                                            MessageBox.Show("Нельзя перепрыгнуть через фигуру");
                                            return;
                                        }
                                    }
                                }
                                if (x1 > x2)
                                {
                                    for (int i = (x1 - 1); x2 != i; i--)
                                    {
                                        if (board[i, y1] != 0)
                                        {
                                            MessageBox.Show("Нельзя перепрыгнуть через фигуру");
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                        if (y1 < y2)
                        {
                            if (x1 > x2)
                            {
                                for (int i = x1 - 1, j = y1 + 1; i != x2; i--, j++)
                                {
                                    if (board[i, j] != 0)
                                    {
                                        MessageBox.Show("Нельзя перепрыгнуть через фигуру");
                                        return;
                                    }
                                }
                            }
                            if (x1 < x2)
                            {
                                for (int i = x1 + 1, j = y1 + 1; i != x2; i++, j++)
                                {
                                    if (board[i, j] != 0)
                                    {
                                        MessageBox.Show("Нельзя перепрыгнуть через фигуру");
                                        return;
                                    }
                                }
                            }
                        }
                        if (y1 > y2)
                        {
                            if (x1 < x2)
                            {
                                for (int i = x1 + 1, j = y1 - 1; i != x2; i++, j--)
                                {
                                    if (board[i, j] != 0)
                                    {
                                        MessageBox.Show("Нельзя перепрыгнуть через фигуру");
                                        return;
                                    }
                                }
                            }
                            if (x1 > x2)
                            {
                                for (int i = x1 - 1, j = y1 - 1; i != x2; i--, j--)
                                {
                                    if (board[i, j] != 0)
                                    {
                                        MessageBox.Show("Нельзя перепрыгнуть через фигуру");
                                        return;
                                    }
                                }
                            }
                        }
                        kl[x1, y1].BackgroundImage = pusto;
                        kl[x1, y1].BackgroundImageLayout = ImageLayout.None;
                        kl[x2, y2].BackgroundImage = figure[player, 3];
                        kl[x2, y2].BackgroundImageLayout = ImageLayout.Stretch;
                        board[x1, y1] = 0;
                        board[x2, y2] = 4;
                        komanda[x1, y1] = 0;
                        komanda[x2, y2] = player;
                    }
                    else
                    {
                        MessageBox.Show("Сюда нельзя ставить эту фигуру");
                        return;
                    }
                    break;
                    #endregion
                case 5:                 //король
                    #region
                    if ((x2 == x1 + 1 || x2 == x1 - 1 || x1 == x2) && (y2 == y1 + 1 || y2 == y1 - 1 || y1 == y2))
                    {
                        kl[x1, y1].BackgroundImage = pusto;
                        kl[x1, y1].BackgroundImageLayout = ImageLayout.None;
                        kl[x2, y2].BackgroundImage = figure[player, 4];
                        kl[x2, y2].BackgroundImageLayout = ImageLayout.Stretch;
                        board[x1, y1] = 0;
                        board[x2, y2] = 5;
                        komanda[x1, y1] = 0;
                        komanda[x2, y2] = player;
                    }
                    else
                    {
                        MessageBox.Show("Сюда нельзя ставить эту фигуру");
                        return;
                    }
                    break;
                    #endregion
                case 8:                 //пешка
                    #region
                    if (player == 1)
                    {
                        if ((x1 - x2 == 1) && (y1 == y2 || y1 - y2 == 1 || y2 - y1 == 1))
                        {
                            if ((y1 == y2 && komanda[x2, y2] != 0) || (((y1 - y2 == 1) || (y2 - y1 == 1)) && komanda[x2, y2] == 0))
                            {
                                MessageBox.Show("Сюда нельзя ставить эту фигуру");
                                return;
                            }
                            kl[x1, y1].BackgroundImage = pusto;
                            kl[x1, y1].BackgroundImageLayout = ImageLayout.None;
                            kl[x2, y2].BackgroundImage = figure[player, 8];
                            kl[x2, y2].BackgroundImageLayout = ImageLayout.Stretch;
                            board[x1, y1] = 0;
                            board[x2, y2] = 8;
                            komanda[x1, y1] = 0;
                            komanda[x2, y2] = player;
                            //Дополнение: возможные хода
                            /*whereMove[numberWheMove, 0] = x1;
                            whereMove[numberWheMove, 1] = y1;*/
                            numberWheMove++;
                        }
                        else
                        {
                            MessageBox.Show("Сюда нельзя ставить эту фигуру");
                            return;
                        }
                    }
                    else
                    {
                        if (x2 - x1 == 1 && (y1 == y2 || y1 - y2 == 1 || y2 - y1 == 1))
                        {
                            if ((y1 == y2 && komanda[x2, y2] != 0) || (y1 != y2 && board[x2, y2] == 0))
                            {
                                MessageBox.Show("Сюда нельзя ставить эту фигуру");
                                return;
                            }
                            kl[x1, y1].BackgroundImage = pusto;
                            kl[x1, y1].BackgroundImageLayout = ImageLayout.None;
                            kl[x2, y2].BackgroundImage = figure[player, 8];
                            kl[x2, y2].BackgroundImageLayout = ImageLayout.Stretch;
                            board[x1, y1] = 0;
                            board[x2, y2] = 8;
                            komanda[x1, y1] = 0;
                            komanda[x2, y2] = player;
                        }
                        else
                        {
                            MessageBox.Show("Сюда нельзя ставить эту фигуру");
                            return;
                        }
                    }
                    break;
                    #endregion

                default:
                    MessageBox.Show("Ошибка3");
                    break;
            }
        }
    }
}

