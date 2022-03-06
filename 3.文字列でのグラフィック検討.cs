/****************************
C#で図形描画できるグラフィックスクラスを用いたが、動作が遅くちらつきが発生した
そのため今回は文字列(テキスト)をうまく利用することでグラフィカルな動作を試みる
******************************/
using System;
using System.Text;  //StringBuilder の使用に必要
using System.Drawing;
using System.Windows.Forms;

class LifeGame : Form
{
	//タイマーの宣言と、ラベル変数等の宣言はここ
	private static Timer timer = new Timer();
    private int counter=0;

	private Label lb;
    //文字列操作をするためにStringBuilderを使用
    private StringBuilder sb;
    private string strT="■", strF="□";

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
		//発動時の処理内容を記載している関数の登録
		timer.Tick += new EventHandler(timer_Tick);
		//タイマーON
		timer.Enabled = true;

        //ラベルオブジェクト生成
        lb = new Label();
        //テキストでグラフィック表現するため、フォントやフォントサイズをあらかじめ定めておく
        //Font(フォント名, フォントサイズ, フォントサイズをピクセル単位で指定)
        lb.Font = new Font("MS UI Gothic", 20, GraphicsUnit.Pixel);
        //ラベルオブジェクト(テキストボックス的なイメージ)の左上座標を指定
        lb.Location = new Point(0,0);
        //ラベルサイズはウィンドウサイズと同じにしておく
        lb.Size = new Size(this.Width, this.Height);

        //30x30の□マトリクス生成
        string text="";
        for(int i=0; i<30; i++)
        {
            for(int j=0; j<30; j++)
            {
                text += strF;
            }
            text += "\n"; //改行コマンド
        }

        //生成したテキストをStringBuilderに登録
        sb = new StringBuilder(text);

        //ラベル(テキストボックス的なの)内のテキストを登録
        lb.Text = sb.ToString();

        //ラベルオブジェクトをthis(このウィンドウ)に配置
        lb.Parent = this;
	}

    private void timer_Tick(Object sender, EventArgs e)
    {
    	if(sb.ToString()[counter] != '\n')
    	{
    		sb.Insert(counter, strT, 1);
    		sb.Remove(counter+1, 1);

    		if(counter>0 && counter%31 != 0)
    		{
    			sb.Insert(counter-1, strF, 1);
    			sb.Remove(counter, 1);
    		}
    		else if(counter>0 && counter%31 == 0)
    		{
    			sb.Insert(counter-2, strF, 1);
    			sb.Remove(counter-1, 1);
    		}
    	}

    	//ラベルに文字列を再登録し、再配置(再描画)
		lb.Text = sb.ToString();
		lb.Parent = this;

    	counter++;
    	if(counter > 870) counter = 0;
    }
}