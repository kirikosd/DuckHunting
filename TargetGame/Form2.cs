using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace TargetGame
{
    public partial class Form2 : Form
    {
        int t = 30;  // ta deyterolepta tou timer
        int score = 0; 
        int speed; // arithmos pixel pou metakinountai ta picturebox kathe fora
        int ps_record; // proswpiko recor tou xrhsth
        int gn_record; // geniko recor
        Random r = new Random();

        bool easy = false;
        bool medium = false;
        bool hard = false;

        bool pr = false;
        bool gr = false;

        int angle;
        Double radian;

        //database
        String connectionString = "Data source=targetGame.db;Version=3";
        SQLiteConnection connection;

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            connection = new SQLiteConnection(connectionString);
            connection.Open();

            label1.Text = Form1.username;
            if (Form1.level == "Easy") { label2.Text = "Personal Best:" + Form1.user_hs_easy; label6.Text = "High Score:" + Form1.hs_easy.ToString(); easy = true; }
            if (Form1.level == "Medium") { label2.Text = "Personal Best:" + Form1.user_hs_medium; label6.Text = "High Score:" + Form1.hs_medium.ToString(); medium = true; }
            if (Form1.level == "Hard") { label2.Text = "Personal Best:" + Form1.user_hs_hard; label6.Text = "High Score:" + Form1.hs_hard.ToString(); hard = true; }
            label3.Text = Form1.level + " Mode";
            label5.Text = score.ToString();

            if (easy)
            {
                speed = 6;
                ps_record = Form1.user_hs_easy;     // metafora twn katallhlwn dedomenwn 
                gn_record = Form1.hs_easy;          // apo thn arxikh forma
            }else if (medium)
            {
                speed = 8;
                ps_record = Form1.user_hs_medium;   // stis antistoixes 
                gn_record = Form1.hs_medium;        // metavlhtes
            }
            else
            {
                speed = 10;
                ps_record = Form1.user_hs_hard;     // analoga me to level
                gn_record = Form1.hs_hard;          //
            }
            
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox)
                {
                    x.Top = r.Next(100, 420);                           // topothetei ta picturebox(stoxous) deksia ths formas 
                    x.Left = r.Next(this.Width, this.Width + 50);       // kai se tyxaio ypsos
                }
            }

        }

        private void countdown_timer_Tick(object sender, EventArgs e)
        {
            label4.Text = t.ToString(); // xronometrhsh
            t -= 1;                     //
            if (t == -1)                // 
            {
                countdown_timer.Stop();

                if (score > ps_record) // elegxos an to score einai prwsopiko recor tou xrhsth
                {
                    pr = true;
                    ps_record = score;

                    //enhmerwsh vashs me ta nea dedomena
                    if (easy)
                    {
                        String updateSQL = "Update Users Set highscoreEasy = '" + ps_record + "' where username = '" + Form1.username + "'";
                        SQLiteCommand command = new SQLiteCommand(updateSQL, connection);
                        command.ExecuteNonQuery();
                    }
                    if (medium)
                    {
                        String updateSQL = "Update Users Set highscoreMedium = '" + ps_record + "' where username = '" + Form1.username + "'";
                        SQLiteCommand command = new SQLiteCommand(updateSQL, connection);
                        command.ExecuteNonQuery();
                    }
                    if (hard)
                    {
                        String updateSQL = "Update Users Set highscoreHard = '" + ps_record + "' where username = '" + Form1.username + "'";
                        SQLiteCommand command = new SQLiteCommand(updateSQL, connection);
                        command.ExecuteNonQuery();
                    }
                }
                if (score > gn_record) // elegxos an to score einai to neo geniko highscore
                {
                    gr = true;
                    gn_record = score;

                    // enhmerwsh vashs me ta nea dedomena
                    if (easy)
                    {
                        String updateSQL = "Update MaxScores Set maxScore_easy ='" + gn_record + "'";
                        SQLiteCommand command = new SQLiteCommand(updateSQL, connection);
                        command.ExecuteNonQuery();
                    }
                    if (medium)
                    {
                        String updateSQL = "Update MaxScores Set maxScore_medium ='" + gn_record + "'";
                        SQLiteCommand command = new SQLiteCommand(updateSQL, connection);
                        command.ExecuteNonQuery();
                    }
                    if (hard)
                    {
                        String updateSQL = "Update MaxScores Set maxScore_Hard ='" + gn_record + "'";
                        SQLiteCommand command = new SQLiteCommand(updateSQL, connection);
                        command.ExecuteNonQuery();

                        if (gn_record > 20)
                        {
                            String insertSQL = "Insert into TopUsers(username) values('" + Form1.username + "')";
                            SQLiteCommand command2 = new SQLiteCommand(insertSQL, connection);
                            command2.ExecuteNonQuery();
                        }
                    }
                }

                // emfanish tou katallhlou mynhmatos analoga me to teliko score 
                if (!pr) { MessageBox.Show("Congrats!You scored: " + score.ToString()); }
                if(pr && !gr) { MessageBox.Show("Congrats!New Personal Best: " + score.ToString()); }
                if(gr) { MessageBox.Show("Congrats!New General Best: " + score.ToString()); }
                connection.Close();
                this.Close();
            }
        }

        private void target1Move_timer_Tick(object sender, EventArgs e)
        {
            // timer gia thn metakinhsh twn stoxwn sthn othonh
            if(easy)
            {
                foreach (Control x in this.Controls)        
                {
                    if (x is PictureBox)
                    {
                        if (x.Left < 30) // an ftasoun sta oria thw othonhsmetatopizontai pali sthn arxikh tous thesh
                        {
                            x.Top = r.Next(100, 420);
                            x.Left = r.Next(this.Width, this.Width + 50);
                        }
                        else
                        {
                            x.Left -= speed; // an den ftasoun sta oria metakinountai pros ta aristera diathrwntas to idio ypsos (easy level)
                        }
                        
                    }
                }
            }
            if(medium)
            {
                foreach (Control x in this.Controls)
                {
                    if (x is PictureBox)
                    {
                        if (x.Left < 30) // an ftasoun sta oria thw othonhsmetatopizontai pali sthn arxikh tous thesh
                        {
                            x.Top = r.Next(100, 420);
                            x.Left = r.Next(this.Width, this.Width + 50);
                        }
                        else            
                        {
                            x.Left -= speed;        // an den ftasoun sta oria metakinountai pros ta aristera
                            x.Top += r.Next(-8, 8); // kai allazoun kai tyxaia to ypsos tous (medium level)
                        }
                    }
                }
            }
            if (hard)
            {
                foreach (Control x in this.Controls)
                {
                    if (x is PictureBox)
                    {
                        if (x.Left < 30)    // an ftasoun sta oria thw othonhsmetatopizontai pali sthn arxikh tous thesh
                        {
                            x.Top = r.Next(100, 420);
                            x.Left = r.Next(this.Width, this.Width + 50);
                        }
                        else
                        {
                            angle = r.Next(120, 240);           // an den ftasoun sta oria metakinountai pros ta aristera
                            radian = (angle * Math.PI) / 180;   // kai allazoun kai tyxaia to ypsos tous me random gwnies (hard level)

                            x.Top -= (int)(10 * Math.Sin(radian));
                            x.Left += (int)(10 * Math.Cos(radian));
                        }
                    }
                }
            }
        }

        private void HitBird1(object sender, EventArgs e)
        {
            var bird = (PictureBox)sender;

            bird.Top = r.Next(100, 420);                        // energopoihte otan petyxainoume kapoion stoxo(click sto picturebox)
            bird.Left = r.Next(this.Width, this.Width + 50);    // kai ton epanaferei sthn arxikh tou thesh gia na synexisei to paixnidi

            score += 1;
            label5.Text = score.ToString();
        }
    }
}
