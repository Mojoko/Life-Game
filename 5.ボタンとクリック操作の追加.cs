/****************************
ボタンとクリック操作の追加
******************************/
using System;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

class LifeGame : Form
{
	private Button startBtn, resetBtn, randomBtn;
	private static Timer timer = new Timer();
	private int counter=0;

	private Map mp = new Map(20, 20);
	private Label lb;
	private int dotSize=20;

	public static void Main()
	{
		Application.Run(new LifeGame());
	}

	public LifeGame()
	{
		this.Text = "LifeGame";
		this.Width = 1200; this.Height = 1000;

		timer.Interval = 100;
		timer.Tick += new EventHandler(timer_Tick);

		lb = new Label();
		lb.Font = new Font("MS UI Gothic", dotSize, GraphicsUnit.Pixel);
		lb.Location = new Point(0,0);
		lb.Size = new Size(this.Width, this.Height - 100);

		lb.Text = mp.textMap();
		lb.Parent = this;
		
		lb.MouseClick += Form_MouseClick;

		startBtn = new Button();
		startBtn.Text = "▶";
		startBtn.Location = new Point(0,this.Height - 100);
		this.Controls.Add(startBtn);
		startBtn.Click += new EventHandler(startBtn_Click);

		resetBtn = new Button();
		resetBtn.Text = "Reset";
		resetBtn.Location = new Point(startBtn.Location.X+startBtn.Width+10,this.Height - 100);
		this.Controls.Add(resetBtn);
		resetBtn.Click += new EventHandler(resetBtn_Click);

		randomBtn = new Button();
		randomBtn.Text = "Random";
		randomBtn.Location = new Point(resetBtn.Location.X+resetBtn.Width+10,this.Height - 100);
		this.Controls.Add(randomBtn);
		randomBtn.Click += new EventHandler(randomBtn_Click);
	}

	private void Form_MouseClick(Object sender, MouseEventArgs e)
	{
		int pointX, pointY;
		if(e.Button==MouseButtons.Left)
		{
			pointX = (int)(e.X - 0.2*dotSize) / dotSize;
			pointY = (int)(e.Y - 0.025*dotSize) / dotSize;
			//Console.WriteLine("({0} , {1})", pointX, pointY);
			mp.dotReverse(pointX, pointY);
			lb.Text = mp.textMap();
			lb.Parent = this;
		}
	}

	private void startBtn_Click(Object sender, EventArgs e)
	{
		if(startBtn.Text == "▶")
		{
			startBtn.Text = "■";
			timer.Enabled = true;
		}
		else
		{
			startBtn.Text = "▶";
			timer.Enabled = false;
		}
	}

	private void resetBtn_Click(Object sender, EventArgs e)
	{
		mp.Reset();
		lb.Text = mp.textMap();
		lb.Parent = this;
	}

	private void randomBtn_Click(Object sender, EventArgs e)
	{
		mp.randomInput();
		lb.Text = mp.textMap();
		lb.Parent = this;
	}

	private void timer_Tick(Object sender, EventArgs e)
	{
        //dorReverseメソッドでエラー処理はしているので記載はこれだけでOK
    	mp.dotReverse(counter%(mp.Width+1), counter/(mp.Width+1));
        mp.dotReverse(counter%(mp.Width+1) - 1, counter/(mp.Width+1));

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

	public void dotReverse(int m, int n)
	{
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

	public int dotStatus(int m, int n)
	{
		if(m>Width-1 || n>Height-1) return 0;
		if(0>m || 0>n) return 0;

		int getPoint = (Width+1) * n + m;

		if(sbmap.ToString()[getPoint] == strT[0]) return 1;
		else return 0;
	}

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

	public string textMap()
	{
		return sbmap.ToString();
	}
}