/****************************
テキストでのグラフィックが上手くいったので、今度はそれをクラスに落とし込んでいく
******************************/
using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

class LifeGame : Form
{
	//タイマーの宣言と、ラベル変数等の宣言はここ
	private static Timer timer = new Timer();
    private int counter=0;

	private Label lb;

    //ここで作成したクラスのオブジェクトを作成(タイマー操作の記述中でも使用したいため)
    private Map mp = new Map(30, 20);
    //テキストサイズ(ドットサイズ)を変数として宣言
    private int dotSize=20;

	public static void Main()
	{
		Application.Run(new LifeGame());
	}

	public LifeGame()
	{
		this.Text = "Life Game";
		this.Width = 800; this.Height = 600;

		//タイマー発動間隔の指定(下記では100ms毎にタイマー発動)
		timer.Interval = 100;   // [ms]
		timer.Tick += new EventHandler(timer_Tick);
		timer.Enabled = true;

        //ラベルオブジェクト生成
        lb = new Label();
        lb.Font = new Font("MS UI Gothic", dotSize, GraphicsUnit.Pixel);
        lb.Location = new Point(0,0);
        lb.Size = new Size(this.Width, this.Height);

        //テキストマップをstring型にしてラベルのテキストそして登録
        lb.Text = mp.textMap();
        //ラベルオブジェクトをthis(このウィンドウ)に配置
        lb.Parent = this;
	}

    private void timer_Tick(Object sender, EventArgs e)
    {
        //dorReverseメソッドでエラー処理はしているので記載はこれだけでOK
    	mp.dotReverse(counter%(mp.Width+1), counter/(mp.Width+1));
        mp.dotReverse(counter%(mp.Width+1) - 1, counter/(mp.Width+1));

    	//ラベルに文字列を再登録し、再配置(再描画)
		lb.Text = mp.textMap();
		lb.Parent = this;

    	counter++;
    	if(counter > (mp.Width+1)*mp.Height) counter = 0;
    }
}

class Map
{
    private string strT="■", strF="□", strN="\n";

    public string text;
    public string mapString;
    public StringBuilder sbmap;
    public int Width, Height;
    public int maxLength;

    //オブジェクト生成時に縦横のドット数からテキストマップ情報をstringbuilderに格納する
    public Map(int m, int n)
    {
        Width = m;
        Height = n;
        maxLength = m * n - 1;

        text = "";
        for(int i=0; i<n; i++)
        {
            for(int j=0; j<m; j++)
            {
                text += strF;
            }
            text += strN;
        }

        mapString = text;
        sbmap = new StringBuilder(mapString);
    }

    //指定したドット位置(x方向, y方向)を反転させるメソッド
    public void dotReverse(int m, int n)
    {
        //エラー処理
        if(m>Width-1 || n>Height-1) return;
        if(0>m || 0>n) return;

        int changePoint = (Width+1) * n + m;

        if(sbmap.ToString()[changePoint] == strF[0])
        {
            sbmap.Insert(changePoint, strT, 1);
        }
        else
        {
            sbmap.Insert(changePoint, strF, 1); 
        }
        sbmap.Remove(changePoint+1, 1);
    }

    //指定されたドット情報(■か□)を判定し1or0で返す
    public int dotStatus(int m, int n)
    {
        if(m>Width-1 || n>Height-1) return 0;
        if(0>m || 0>n) return 0;

        int getPoint = (Width+1) * n + m;

        if(sbmap.ToString()[getPoint] == strT[0]) return 1;
        else return 0;
    }

    //テキストのマップ情報をリセット
    public void Reset()
    {
        text = "";
        for(int i=0; i<Height; i++)
        {
            for(int j=0; j<Width; j++)
            {
                text += strF;
            }
            text += strN;
        }

        mapString = text;
        sbmap = new StringBuilder(mapString);
    }

    //マップの情報をランダムに入力するメソッド
    public void randomInput()
    {
        Random r = new Random();
        int randomN;

        text = "";
        for(int i=0; i<Height; i++)
        {
            for(int j=0; j<Width; j++)
            {
                randomN = (int)r.Next(0,2);

                if(randomN == 0) text += strF;
                else text += strT;
            }
            text += strN;
        }

        mapString = text;
        sbmap = new StringBuilder(mapString);
    }

    //操作は全てstringbuilderで行ったため、string型を返すメソッドも用意しておく
    //他のメソッドで全て返り値stringを返すようにしてしまってもいいかもしれない。。。
    public string textMap()
    {
        return sbmap.ToString();
    }
}