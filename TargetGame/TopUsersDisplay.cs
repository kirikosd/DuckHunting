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
    public partial class TopUsersDisplay : Form
    {
        //database
        String connectionString = "Data source=targetGame.db;Version=3";
        SQLiteConnection connection;
        public TopUsersDisplay()
        {
            InitializeComponent();
        }

        private void TopUsersDisplay_Load(object sender, EventArgs e)
        {
            connection= new SQLiteConnection(connectionString);
            connection.Open();

            bool d = false;

            String select1SQL = "Select Distinct username from TopUsers";           // travame ta stoixeia twn koryfaiwn paiktwn apo ton pinaka
            SQLiteCommand command1 = new SQLiteCommand(select1SQL, connection);     // TopUsers , afairwntas ta duplicates (distinct)
            SQLiteDataReader reader = command1.ExecuteReader();                     //
            while (reader.Read())                                                   //
            {
                int rowId = dataGridView1.Rows.Add();                               // kai ta prosthetoumai sto datagridview

                DataGridViewRow row = dataGridView1.Rows[rowId];

                row.Cells["Username"].Value = reader.GetString(0);

                String select3SQL = "Select * from Users";
                SQLiteCommand command3 = new SQLiteCommand(select3SQL, connection); // To max score to travame apo to MaxScores 
                SQLiteDataReader reader3 = command3.ExecuteReader();                // kai oxi apo to TopUsers
                while (reader3.Read())                                              //
                {
                    if (reader3.GetString(1) == reader.GetString(0))
                    {
                        row.Cells["HighScore_HARD"].Value = reader3.GetInt32(5);

                        break;
                    }
                }
            }
        }
    }
}
