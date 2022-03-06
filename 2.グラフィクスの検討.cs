/****************************
C#には図形描画のためにグラフィックスクラスが用意されている
作りたいライフゲームのイメージはグリッド内で黒ドットと白ドットがパタパタ動いているようなものなので
標準で用意されているグラフィックスが使えるかの検討をする
******************************/
using System;
using System.Drawing;
using System.Windows.Forms;

class LifeGame : Form
{
	//タイマーの宣言と、黒四角の座標及びタイムカウンター変数の宣言はここ
	private static Timer timer = new Timer();
	private int x=0, y=0, counter=0;

	public static void Main()
	{
		Application.Run(new LifeGame());
	}

	public LifeGame()
	{
		this.Text = "Life Game";
		this.Width = 600; this.Height = 500;

		//タイマー発動間隔の指定(下記では100ms毎にタイマー発動)
		timer.Interval = 100;   // [ms]
		//発動時の処理内容を記載している関数の登録
		timer.Tick += new EventHandler(timer_Tick);
		//タイマーON
		timer.Enabled = true;

		//フォーム画面にグラフィックス描画を登録
		this.Paint += new PaintEventHandler(_Paint);
	}

    private void _Paint(Object sender, PaintEventArgs e)
    {
    	Graphics g = e.Graphics;
    	SolidBrush br = new SolidBrush(Color.White);

        // 白四角を縦30,横30個描画 (四角は幅高さ10pxで12px間隔で配置)
        for(int i=0; i<30; i++)
        {
        	for(int j=0; j<30; j++)
        	{
		        e.Graphics.FillRectangle(br, j*12, i*12, 10, 10);
        	}
        }

        //黒四角の描画 (配置座標はx,y変数で指定→タイマー発動毎に数値変更)
        br = new SolidBrush(Color.Black);
        e.Graphics.FillRectangle(br, x, y, 10, 10);
    }

    private void timer_Tick(Object sender, EventArgs e)
    {
    	counter++;

    	if(counter%30 != 0)
    	{
    		x += 12;
    	}
    	else
    	{
    		x = 0;
    		y += 12;
    	}

    	if(y == 30*12)
    	{
    		x = 0;
    		y = 0;
    	}

    	//画面の再描画
    	this.Invalidate();
    }
}