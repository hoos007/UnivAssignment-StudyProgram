using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace ex_1
{
    public partial class Form1 : Form
    {
        int login = 0;
        BackgroundWorker worker; //백그라운드 관련

        delegate void DsetLabel(string data); //스레드 관련 오류 해결법

        private void SetLabel1(string data) //스레드 관련 오류 해결법
        {
            if (label1.InvokeRequired)
            {
                DsetLabel call = new DsetLabel(SetLabel1);
                this.Invoke(call, data);
            }
            else
            {
                label1.Text = data;
            }
        }

        public Form1()
        {
            InitializeComponent();
            worker = new BackgroundWorker();
            worker.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CompleteBackgroundwork);
        }

        Stopwatch sw = new Stopwatch();
        int back_check=0;

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            back_check = 1;
            string[] studyprocess = File.ReadAllLines(@".\test\study.txt");
            string[] gameprocess = File.ReadAllLines(@".\test\game.txt");//하나라도 켜지면 시작 모두 종료되면 끝.
            string time;
            int c1;
            int c2;
            int gameflag = 0;
            int gamep1 = 0;
            int gamep2 = 0;
            int gamepcheck = 0;
            for (; ; )
            {
                for (int i = 0; i <= studyprocess.Length - 1; i++)
                {
                    Process[] studyprocessList = Process.GetProcessesByName(studyprocess[i]);
                    if (studyprocessList.Length >= 1)
                    {
                        SetLabel1("지정한 프로세스가 한개이상 실행중입니다. 카운트를 시작합니다.");
                        sw.Reset();
                        sw.Start();
                        goto aa;//무한 포문 빠저나가
                    }
                }
            }
        aa:
            for (; ; )
            {
                c1 = 0;
                if (gamepcheck == 0)
                {
                    for (int i = 0; i <= gameprocess.Length - 1; i++)
                    {
                        Process[] gameprocessList = Process.GetProcessesByName(gameprocess[i]);
                        if (gameprocessList.Length >= 1)
                        {
                            SetLabel1("게임이 실행중입니다. 카운트가 일시정지 됩니다.");
                            sw.Stop();
                            gameflag = 1;
                            gamep1 = 1;
                            gamep2 = 0;
                            gamepcheck = 1;
                            goto cc;
                        }
                    }
                }
            cc:
                if (gamepcheck == 1)
                {
                    c2 = 0;
                    for (int i = 0; i <= gameprocess.Length - 1; i++)
                    {
                        Process[] gameprocessList = Process.GetProcessesByName(gameprocess[i]);
                        if (gameprocessList.Length < 1)
                        {
                            c2++;
                        }
                    }
                    if (c2 == gameprocess.Length)
                    {
                        gameflag = 0;
                    }
                }
                for (int i = 0; i <= studyprocess.Length - 1; i++)
                {

                    Process[] studyprocessList = Process.GetProcessesByName(studyprocess[i]);
                    if (studyprocessList.Length < 1)
                    {
                        c1++;
                    }
                }
                if (c1 == studyprocess.Length)
                {
                    SetLabel1("지정한 모든 프로세스가 종료되었습니다.");
                    sw.Stop();
                    goto bb;
                }
                else if (gameflag == 0 && c1 != studyprocess.Length && gamep1 == 1 && gamep2 == 0)
                {
                    SetLabel1("게임이 종료되었습니다. 지정 프로세스가 동작중이므로 카운트를 다시 시작합니다.");
                    sw.Start();
                    gamep2 = 1;
                    gamepcheck = 0;
                }
            }
        bb:
            time = sw.Elapsed.ToString();
            SetLabel1("공부시간 : " + time);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(back_check != 1)
            {
                worker.RunWorkerAsync();
            }
        }

        private void studyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@".\test\study.txt");
        }

        private void gameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@".\test\game.txt");
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] name = File.ReadAllLines(@".\test\name.txt");
            int c;
            if (textBox1.Text.Length >= 1)
            {
                c = 0;
                for (int i = 0; i <= name.Length - 1; i++)
                {
                    if (name[i] == textBox1.Text)
                    {
                        login = i + 1; //검색한 이름의 줄 체크(스코어 파일 같은줄에 점수를 저장하고 불러오기 위함)
                        string num = login.ToString();
                        label1.Text = num;
                        panel1.Visible = false;
                        break;
                    }
                    else
                    {
                        c++;
                    }
                }
                if (c == name.Length)
                {
                    StreamWriter outputFile1 = new StreamWriter(@".\test\name.txt", true);
                    outputFile1.WriteLine(textBox1.Text);
                    outputFile1.Close();

                    StreamWriter outputFile2 = new StreamWriter(@".\test\score.txt", true);
                    outputFile2.WriteLine("1");
                    outputFile2.Close();

                    StreamWriter outputFile3 = new StreamWriter(@".\test\index1.txt", true);
                    outputFile3.WriteLine("0");
                    outputFile3.Close();

                    StreamWriter outputFile4 = new StreamWriter(@".\test\index2.txt", true);
                    outputFile4.WriteLine("0");
                    outputFile4.Close();

                    login = name.Length + 1;
                    string num = (name.Length + 1).ToString();
                    label1.Text = num;
                    panel1.Visible = false;
                }
            }
        }

        private void nameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@".\test\name.txt");
        }

        private void scoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start(@".\test\score.txt");
        }

        private void Index_Check()
        {
            string[] index01 = File.ReadAllLines(@".\test\index1.txt");
            index1 = Int32.Parse(index01[login - 1]);
            string[] index02 = File.ReadAllLines(@".\test\index2.txt");
            index2 = Int32.Parse(index02[login - 1]);
        }
        int index1;
        int index2;

        private void level()
        {
            int ex;
            int level;
            long sec = sw.ElapsedMilliseconds / 1000;
            int rank=1;
            panel2.Visible = true;

            ex = (int)sec;

            string[] score1 = File.ReadAllLines(@".\test\score.txt");
            StreamWriter outputFile1 = new StreamWriter(@".\test\score.txt", false);
            for(int i=0;i<=score1.Length-1;i++)
            {
                if(i== login - 1)
                {
                    outputFile1.WriteLine(Int32.Parse(score1[i])+ex);
                }
                else
                {
                    outputFile1.WriteLine(score1[i]);
                }
            }
            outputFile1.Close();

            string[] score2 = File.ReadAllLines(@".\test\score.txt");
            for (int i = 0; i <= score1.Length - 1; i++)
            {
                if(Int32.Parse(score2[i])> Int32.Parse(score2[login-1]))
                {
                    rank++;
                }
            }

            string[] score = File.ReadAllLines(@".\test\score.txt");
            progressBar1.Style = ProgressBarStyle.Continuous;
            progressBar1.Minimum = 0;
            progressBar1.Maximum = 100;
            progressBar1.Step = 1;

            ex = Int32.Parse(score[login - 1])%100;
            progressBar1.Value = ex;
            label4.Text = "경험치 : " + ex;
            label5.Text = "100";
            level = (Int32.Parse(score[login - 1]) / 100)+1;
            label6.Text = "레벨 : " + level;
            label7.Text = "랭크 : " + rank+"/"+score.Length;
            sec = sw.ElapsedMilliseconds / 1000;
            long h = sec / 3600;
            long m = sec / 60;
            long s = sec % 60;
            label8.Text = "공부시간 "+h.ToString()+":"+m.ToString()+":"+s.ToString();

            string[] item1 = File.ReadAllLines(@".\test\ItemList1.txt");
            listBox1.Items.Add(item1[0]);
            for (int i=1;i<=item1.Length-1;i++)
            {
                if(i==level)
                {
                    break;
                }
                listBox1.Items.Add(item1[i]);
            }
            string[] item2 = File.ReadAllLines(@".\test\ItemList2.txt");
            listBox2.Items.Add(item2[0]);
            for (int i = 1; i <= item2.Length - 1; i++)
            {
                if (i >= (level/3)+1)
                {
                    break;
                }
                listBox2.Items.Add(item2[i]);
            }
            Index_Check();
            pictureBox1.Load(@".\test\" + index1 + "-" + index2 + ".jpg");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void CompleteBackgroundwork(object sender, RunWorkerCompletedEventArgs e)
        {
            back_check = 0;
            level();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            index1 = listBox1.SelectedIndex;
            pictureBox1.Load(@".\test\" + index1+"-"+index2+".jpg");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            string[] index01 = File.ReadAllLines(@".\test\index1.txt");
            StreamWriter outputFile1 = new StreamWriter(@".\test\index1.txt", false);
            for (int i = 0; i <= index01.Length - 1; i++)
            {
                if (i == login - 1)
                {
                    outputFile1.WriteLine(index1);
                }
                else
                {
                    outputFile1.WriteLine(index01[i]);
                }
            }
            outputFile1.Close();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            index2 = listBox2.SelectedIndex;
            pictureBox1.Load(@".\test\" + index1 + "-" + index2 + ".jpg");
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

            string[] index02 = File.ReadAllLines(@".\test\index2.txt");
            StreamWriter outputFile1 = new StreamWriter(@".\test\index2.txt", false);
            for (int i = 0; i <= index02.Length - 1; i++)
            {
                if (i == login - 1)
                {
                    outputFile1.WriteLine(index2);
                }
                else
                {
                    outputFile1.WriteLine(index02[i]);
                }
            }
            outputFile1.Close();
        }
    }
}
