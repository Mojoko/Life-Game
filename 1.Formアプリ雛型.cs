using System;
using System.Windows.Forms;

//一般的なフォームアプリケーションのひな形
//クラスはFormを継承して作成
class LifeGame : Form
{
	public static void Main()
	{
		Application.Run(new LifeGame());
	}

	public LifeGame()
	{
		//"this"はフォームオブジェクトを指す
		//ウィンドウタイトルの指定
		this.Text = "LifeGame";
		//ウィンドウサイズの指定(px)
		this.Width = 800; this.Height = 600;
	}
}