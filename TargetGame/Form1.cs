using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TargetGame
{
    public partial class Form1 : Form
    {
        public static String username = "";
        String here_since = "";
        public static String level = "";
        public static int user_hs_easy; // highscore tou xrhsth
        public static int user_hs_medium;
        public static int user_hs_hard;
        public static int hs_easy;          // geniko highscore 
        public static int hs_medium;
        public static int hs_hard;

        public static bool player_exists = false;

        //database
        String connectionString = "Data source=targetGame.db;Version=3";
        SQLiteConnection connection;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            connection= new SQLiteConnection(connectionString);
            connection.Open();
            
            //dhmiourgia twn tables
            String createSQLusers  = "Create table if not exists Users(user_ID int auto increment primary key,username Text,hereSince Text," +
                                     "highscoreEasy int,highscoreMedium int,highscoreHard int)";
            String createSQLscores = "Create table if not exists MaxScores(maxScore_easy int,maxScore_medium int,maxScore_hard int)";

            String createSQLtopusers = "Create table if not exists TopUsers(user_ID int auto increment primary key,username Text)";

            SQLiteCommand command1 = new SQLiteCommand(createSQLusers,connection);
            command1.ExecuteNonQuery();

            SQLiteCommand command2 = new SQLiteCommand(createSQLscores,connection);
            command2.ExecuteNonQuery();

            SQLiteCommand command3 = new SQLiteCommand(createSQLtopusers, connection);
            command3.ExecuteNonQuery();

            String selectSQL = "Select * from MaxScores";
            SQLiteCommand command4 = new SQLiteCommand(selectSQL, connection);
            SQLiteDataReader reader = command4.ExecuteReader();
            int count = 0;
            while (reader.Read()) // elegxos an o pinakas MaxScores exei eggrafes
            {                     //  
                count++;          //
            }
            if(count == 0)        // an den exei mpainei pantou 0
            {
                String insertSQL = "Insert into MaxScores(maxScore_easy,maxScore_medium,maxScore_hard) values(0,0,0)";
                SQLiteCommand command5 = new SQLiteCommand(insertSQL, connection);
                command5.ExecuteNonQuery();
            }
            String select2SQL = "Select * from MaxScores";
            SQLiteCommand command = new SQLiteCommand(select2SQL, connection);
            SQLiteDataReader reader2 = command.ExecuteReader();
            while (reader2.Read()) // pairnoume tis times tou pinaka kai tis dinoume stis antistoixes metavlhtes
            {
                hs_easy = reader2.GetInt32(0);
                hs_medium = reader2.GetInt32(1);
                hs_hard = reader2.GetInt32(2);
            }
            
            connection.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            username = textBox1.Text;
            
            if (username != "")
            {
                connection.Open();
                String selectSQL = "Select * from Users"; // travame apo thn vash ta stoixeia tou xrhsth pou ekane login
                SQLiteCommand command6 = new SQLiteCommand(selectSQL,connection);
                SQLiteDataReader reader= command6.ExecuteReader();
                while (reader.Read())
                {
                    if(username == reader.GetString(1))
                    {
                        here_since = reader.GetString(2);
                        user_hs_easy = reader.GetInt32(3);
                        user_hs_medium = reader.GetInt32(4);
                        user_hs_hard = reader.GetInt32(5);

                        player_exists = true;
                        break;
                    }
                }

                if(player_exists == false) // an den yparxei sthn bash xrhsths me ayto to username ftiaxnoume kainourio
                {
                    here_since = DateTime.Today.ToShortDateString();
                    user_hs_easy = 0 ;
                    user_hs_medium = 0 ;
                    user_hs_hard = 0 ;

                    String insertSQL = "Insert into Users(username,hereSince,highscoreEasy,highscoreMedium,highscoreHard) values('" 
                                        + username + "','" + here_since +"',0,0,0)";
                    SQLiteCommand command7 = new SQLiteCommand(insertSQL,connection);
                    command7.ExecuteNonQuery();
                }

                connection.Close();

                textBox1.Visible = false;
                label1.Text = "Welcome " + username + "!";
                button1.Visible = false;
                button2.Visible = true;
                groupBox1.Visible = true;
                groupBox1.Text = username + "'s " + "info";
                label3.Text = "Here Since : " + here_since;
            }
            else
            {
                MessageBox.Show("Please enter your username to continue!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //logout code
            username = "";
            groupBox1.Visible= false;
            button1.Visible = true;
            button2.Visible= false;
            textBox1.Visible = true;
            textBox1.ResetText();
            label1.Text = "Enter Username:";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) { label2.Text = "High Score : " + hs_easy; label4.Text = "Personal Best : " + user_hs_easy; level = "Easy"; }
            if (radioButton2.Checked) { label2.Text = "High Score : " + hs_medium; label4.Text = "Personal Best : " + user_hs_medium; level = "Medium"; }
            if (radioButton3.Checked) { label2.Text = "High Score : " + hs_hard; label4.Text = "Personal Best : " + user_hs_hard; level = "Hard"; }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) { label2.Text = "High Score : " + hs_easy; label4.Text = "Personal Best : " + user_hs_easy; level = "Easy"; }
            if (radioButton2.Checked) { label2.Text = "High Score : " + hs_medium; label4.Text = "Personal Best : " + user_hs_medium; level = "Medium"; }
            if (radioButton3.Checked) { label2.Text = "High Score : " + hs_hard; label4.Text = "Personal Best : " + user_hs_hard; level = "Hard"; }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked) { label2.Text = "High Score : " + hs_easy; label4.Text = "Personal Best : " + user_hs_easy; level = "Easy"; }
            if (radioButton2.Checked) { label2.Text = "High Score : " + hs_medium; label4.Text = "Personal Best : " + user_hs_medium; level = "Medium"; }
            if (radioButton3.Checked) { label2.Text = "High Score : " + hs_hard; label4.Text = "Personal Best : " + user_hs_hard; level = "Hard"; }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (username != "")
            {
                if (radioButton1.Checked || radioButton2.Checked || radioButton3.Checked)
                {
                    Form f2 = new Form2();
                    f2.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Please choose a difficulty level!");
                }
            }
            else
            {
                MessageBox.Show("Please enter your username to continue!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form tu= new TopUsersDisplay();
            tu.ShowDialog();
        }
    }
}
