using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace hw06
{
    public partial class Form1 : Form
    {
        private static double your=0,com=0;
        private static String msg="";
        private static PictureBox[] pic = new PictureBox[10];
        private static int cont=0,locat=0;
        private static int money=1000,stake;
        private static int[] poker = new int[52];

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pic[0] = pictureBox1;
            pic[1] = pictureBox2;
            pic[2] = pictureBox3;
            pic[3] = pictureBox4;
            pic[4] = pictureBox5;
            pic[5] = pictureBox6;
            pic[6] = pictureBox7;
            pic[7] = pictureBox8;
            pic[8] = pictureBox9;
            pic[9] = pictureBox10;
            newgame();
            unavailable();
            reset();
            shuffle();
        }
        //重玩
        private void button6_Click(object sender, EventArgs e)
        {
            newgame(); //遊戲的介面與數值回歸初始
            unavailable();  //讓主要控制鍵無法互動
            reset();    //重設遊戲回合
            shuffle();  //洗牌
            show();     //顯示訊息
        }
        //繼續
        private void button3_Click(object sender, EventArgs e)
        {
            reset();    //重設遊戲回合
            shuffle();  //洗牌
            show();     //顯示訊息
        }      
        //確定
        private void button2_Click(object sender, EventArgs e)
        {
            competitor();   //對手動作
            decide();   //決定輸贏
            show(); //顯示結果
            judge(); //判斷手頭上的錢
            unavailable();  //讓確定鍵無法在被按

        }
        //抽牌
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            draw();     //按下進行翻牌
            showdown();     //按完後不讓使用者在與剛按下的圖片互動
            show();     //顯示訊息
        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {
            draw();     
            showdown();
            show();
        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            draw();     
            showdown();
            show();
        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {
            draw();     
            showdown();
            show();
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            draw();     
            showdown();
            show();
        }
        //下注賭金
        private void button1_Click(object sender, EventArgs e)
        {
            if (money > 0)  //如果現金大於0則每次下注可為100
                stake = 100;
            else
                stake = 500;    //反之
            bet();  //下注
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (money > 0)
                stake = 150;
            else
                stake = 1000;
            bet();
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (money > 0)
                stake = 200;
            else
                stake = 1500;
            bet();
        }
        //遊戲的介面與數值初始化
        public void newgame()
        {
            money = 1000;
            label3.Text = "賭金：" + money;
            label4.Text = "一般模式";
            button1.Text = "100";
            button4.Text = "150";
            button5.Text = "200";
        }
        //確定鍵關閉並開啟繼續鍵和重玩鍵
        //所有牌都會變得不可互動
        public void unavailable()
        {
            button2.Enabled = false;
            button3.Enabled = true;
            button6.Enabled = true;
            for (int i = 0; i < 5; i++)
            {
                pic[i].Enabled = false; 
            }
        }
        //重設遊戲回合
        public void reset()
        {
            msg = "遊戲開始";
            com = 0;
            your = 0;
            cont = 0;
            locat = 0;
            //全部蓋牌
            for (int i = 0; i < 10; i++)
            {
                if (i <= 5)
                {
                    pic[i].Image = Image.FromFile("poker\\52.jpg");
                    pic[i].Enabled = false;
                }
                else
                {
                    pic[i].Image = null;
                }
            }
            //賭金按鈕開啟
            button1.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            //繼續重玩鍵關閉
            button3.Enabled = false;
            button6.Enabled = false;
        }
        //洗牌
        public void shuffle()
        {
            int p, q;
            Random rand = new Random();
            for (int i = 0; i < 52; i++)
            {
                poker[i] = i;
            }
            for (int i = 0; i <= 1000; i++)   // Randomly select two numbers for exchange
            {
                p = rand.Next(0, 52);
                q = rand.Next(0, 52);
                int tmp = poker[p];
                poker[p] = poker[q];
                poker[q] = tmp;
            }
        }
        //秀出提示訊息
        public void show()
        {
            label2.Text = msg;
        }
        //抽牌並判斷加點數
        public void draw()
        {

            string st;  //將牌顯示在圖框裡
            st = "poker\\" + poker[cont].ToString() + ".jpg";
            pic[locat].Image = Image.FromFile(st);

            if (poker[cont] % 13 == 10 || poker[cont] % 13 == 11 || poker[cont] % 13 == 12)   //當牌等於JQK加半點
                your += 0.50;
            else
                your += poker[cont] % 13 + 1;

            msg = "目前你的點數為" + your + "\n";
            cont++; //第幾張牌
            locat++; //第幾個位置
        }
       //攤開覆蓋的牌
        public void showdown()
        {
            //將原本按下的牌設為不互動，未攤開的下張牌設為互動
            pic[locat - 1].Enabled = false;
            pic[locat].Enabled = true;
            //確定鍵打開
            button2.Enabled = true;
        }
        //決定輸贏
        public void decide()
        {   //當使用者>電腦且<=10.5
            //電腦爆掉且使用者<=10.5
            if ((your > com && your <= 10.5) || (com > 10.5 && your <= 10.5))
            {
                msg = "電腦點數:" + com + " 你的點數:" + your + "\n恭喜!你贏了";
                money += stake * 2; //賭金會加上下注的賭金*2倍
                label3.Text = "賭金：" + money;
            }
            //使用者爆掉，電腦沒爆
            //使用者<電腦且電腦沒爆
            else if ((your < com && com <= 10.5) || (your > 10.5 && com <= 10.5))
            {
                msg = "電腦點數:" + com + " 你的點數:" + your + "\n你輸了";    //因賭金已付出故不需再扣
            }
            //使用者=電腦或兩者一起爆
            else if (your == com || (your > 10.5 && com > 10.5))
            {
                msg = "電腦點數:" + com + " 你的點數:" + your + "\n和局";
                money += stake; //賭金加回下注金額
                label3.Text = "賭金：" + money;
            }
        }
        //判斷賭金
        public void judge()
        {
            if (money <= 0)
            {
                label4.Text = "借錢模式";
                button1.Text = "500";
                button4.Text = "1000";
                button5.Text = "1500";
            }
            else
            {
                button1.Text = "100";
                button4.Text = "150";
                button5.Text = "200";
            }

            if (money >= 2000) //當賭金大於2000則勝利
            {                  //玩家可繼續在玩
                label2.Text = "勝利!!!";
                label4.Text = "勝利!!!";
                label1.Text = "勝利!!!";
                label3.Text = "勝利!!!";
            }
        }
        //下注
        public void bet()
        {
            money -= stake; //先收錢
            label3.Text = "賭金：" + money;
            pictureBox1.Enabled = true; //讓玩家可進行攤牌動作
            button1.Enabled = false;    //屏蔽所有下注按鈕
            button4.Enabled = false;
            button5.Enabled = false;
        }
        

        //對手行為
        public void competitor()
        {
            Random r = new Random();
            int p = r.Next(10); //對手做任何行動的機率
            //locat一定要等於5，因為對手的圖片位置從5開始
            locat = 5;
            action();   //對手行動(抽牌)
            cont++; //第幾張牌
            locat++; //第幾個位置
            //當玩家賭金>=800和<=100且大於-200時，玩家完勝的機率為30%
            if (money >= 800 || (money <= 100 && money>=-200))
            {
                if (p < 3)
                {
                    win();
                    label4.Text = "完勝模式";
                }
                else 
                {
                    general();
                    label4.Text = "一般模式";
                }
            }
            else if (money >= 1700)     //當玩家賭金大於1700時對手絕對會完勝
            {
                win();
                label4.Text = "完勝模式";
            }
            else    //其餘時候有80%對手都是完勝
            {
                if (p < 8)
                {
                    win();
                    label4.Text = "完勝模式";
                }
                else
                {
                    general();
                    label4.Text = "一般模式";
                }
            }
        }
        //對手抽牌行動
        public void action()
        {
            string st;  //將牌顯示在圖框裡
            st = "poker\\" + poker[cont].ToString() + ".jpg";
            pic[locat].Image = Image.FromFile(st);

            if (poker[cont] % 13 == 10 || poker[cont] % 13 == 11 || poker[cont] % 13 == 12)   //當牌等於JQK加半點
                com += 0.50;
            else
                com += poker[cont] % 13 + 1;
        }
        //對手一般動作
        public void general()
        {   //如果<6.5時一定抽牌
            while (com < 6.5)
            {
                action();
                cont++; //這張抽完換下一張
                locat++; //換下一個位置
            }
        }
        //完勝模式:對手一定贏或平手的動作
        public void win()
        {
            double temp = 0;    //暫存變數
            while ((com<=your && com<10.5) && your<=10.5)   //當對手贏或者平手的情形則跳出迴圈
            {
                if (cont == 52 || locat == 10)  //當牌數和位置超過範圍則跳出迴圈
                {                               //避免無窮迴圈
                    label4.Text = cont+"/"+locat;
                    break;
                }
                    //先看看牌的點數
                if (poker[cont] % 13 == 10 || poker[cont] % 13 == 11 || poker[cont] % 13 == 12)   //當牌等於JQK加半點
                    temp = 0.50;
                else
                    temp = poker[cont] % 13 + 1;
                //如果原先點數加上目前牌的點數未超過10.5則進入判斷
                if ((com + temp) <= 10.5)
                {
                    if ((locat == 8) && (your==10.5))   //這是當使用者絕對贏的情形做判斷
                    {
                        if (((com + temp) % 1 == 0.5))  //如果再倒數第二張牌的位置此時點數加起來沒有"半點"了話   
                        {                               //那麼這個位置一定要有個"半點"
                            action();       //如果確定加起來有半點則將牌分配給對手
                            locat++;        //進到下個位置
                        }
                        else
                            cont++;     //沒了話換下一張
                    }
                    else if (locat == 9)    //這是在倒數最後一個位置的情形
                    {
                        if ((com + temp) >= your && (com + temp) <= 10.5)   //最後一張抽到的牌一定要等於或大於使用者
                        {                                                   //而且要小於等於10.5
                            action();       //若是的話則將牌指定給對手
                            locat++;
                        }
                    }
                    else        //不是已上情形了話直接將牌指定給對手
                    {           //因為在最前面的判斷已經過濾了讓對手牌爆掉的情形故可以直接指定
                        action();
                        locat++; //進入下個位置
                    }
                }
                cont++; //下張牌
            }
            //為了不讓對手看起來很威而顯的太假所以偶爾也會有平手的情形
            //當然是在確定使用者一定輸的情況
            if (your > 10.5)
            {
                general();
            }
        }
    }
}
