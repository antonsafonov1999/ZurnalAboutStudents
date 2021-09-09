using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zurnal;

namespace WindowsFormsApp1Vlad
{
    public partial class Form1 : Form
    {
        public List<Student> Students = new List<Student>();
        ClassDataBase db = new ClassDataBase();
        public Form1()
        {
            InitializeComponent();
        }

        private void редагуванняДаннихПроГрупуТаСтудентівToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
                Form2 form2 = new Form2();
                form2.ShowDialog();

                Students.Clear();
                comboBox1.Items.Clear();
                string s = @"SELECT * FROM table1 GROUP BY groopa;";
                db.Execute<Student>("student.db", s, ref Students);
                for (int i = 0; i < Students.Count; i++)
                  comboBox1.Items.Add(Students[i].group);
                
                if (Students.Count != 0)
                {
                    comboBox1.SelectedItem = Students[0].group;
              
                }

            Students.Clear();
            string x = @"SELECT * FROM table1 GROUP BY subject;";
            db.Execute<Student>("student.db", x, ref Students);
            comboBox3.Items.Clear();
            for (int i = 0; i < Students.Count; i++)
                comboBox3.Items.Add(Students[i].Subject);
            comboBox2.SelectedIndex = -1;
            textBox1.Text = "";
            ShowDatagridview();
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            dataGridView1.Rows.Clear();
            if (radioButton2.Checked == true)
            {
                Students.Clear();
                string s = @"SELECT* FROM table1 WHERE  assessment = '" + comboBox2.SelectedItem + "'AND groopa = '" + comboBox1.SelectedItem + "' ORDER BY  assessment DESC;";
                db.Execute<Student>("student.db", s, ref Students);
                for (int i = 0; i < Students.Count; i++)
                {
                    dataGridView1.Rows.Add(Students[i].nameStud, Students[i].assessment);
                }
            }
            else
            {
                if (radioButton1.Checked == true)
                {
                    Students.Clear();
                    string s = @"SELECT* FROM table1 WHERE  assessment = '" + comboBox2.SelectedItem + "'AND groopa = '" + comboBox1.SelectedItem + "' ORDER BY  assessment ASC;";
                    db.Execute<Student>("student.db", s, ref Students);
                    for (int i = 0; i < Students.Count; i++)
                    {
                        dataGridView1.Rows.Add(Students[i].nameStud, Students[i].assessment);
                    }
                }

            }
        }
        private void ShowDatagridview()
        {
            dataGridView1.Rows.Clear();
          

            if (radioButton2.Checked ==true)
            {
                Students.Clear();
                string s = @"SELECT* FROM table1 WHERE groopa = '" + comboBox1.Text + "'AND subject ='" + comboBox3.Text + "' ORDER BY  assessment DESC;";
                db.Execute<Student>("student.db", s, ref Students);
                for (int i = 0; i < Students.Count; i++)
                {
                    dataGridView1.Rows.Add(Students[i].nameStud, Students[i].assessment);
                }
                Students.Clear();
            }
            else
            {
                if (radioButton1.Checked ==true)
                {
                    Students.Clear();
                    string s = @"SELECT* FROM table1 WHERE groopa = '" + comboBox1.Text + "'AND subject ='" + comboBox3.Text + "' ORDER BY  assessment ASC;";
                    db.Execute<Student>("student.db", s, ref Students);
                    for (int i = 0; i < Students.Count; i++)
                    {
                        dataGridView1.Rows.Add(Students[i].nameStud, Students[i].assessment);
                    }
                    Students.Clear();
                }
            }
            

           
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

            comboBox2.SelectedIndex = -1;
            textBox1.Text = "";
            if (radioButton1.Checked == true | radioButton2.Checked == true )
            {
                ShowDatagridview();
                comboBox2.Enabled = true;
                textBox1.Enabled = true;
            }
            else
            {
                ShowDatagridview();
                comboBox2.Enabled = false;
                textBox1.Enabled = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            ShowDatagridview();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            ShowDatagridview();
        }

        private void Form1_Leave(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                Students.Clear();
                string s = @"SELECT * FROM table1 GROUP BY groopa;";
                db.Execute<Student>("student.db", s, ref Students);
                for (int i = 0; i < Students.Count; i++)
                
                    comboBox1.Items.Add(Students[i].group);
                
               
            }
            catch 
            {

              
            }
            Students.Clear();
            string x = @"SELECT * FROM table1 GROUP BY subject;";
            db.Execute<Student>("student.db", x, ref Students);
            for (int i = 0; i < Students.Count; i++)

                comboBox3.Items.Add(Students[i].Subject);

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = -1;
            textBox1.Text = "";
            ShowDatagridview();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {

            comboBox2.SelectedIndex = -1;
            dataGridView1.Rows.Clear();
            if (radioButton2.Checked == true)
            {
                Students.Clear();
                string s = @"SELECT * FROM table1 WHERE nameStud = '" + textBox1.Text + "' AND groopa = '" + comboBox1.SelectedItem + "'ORDER BY  assessment DESC;";
                db.Execute<Student>("student.db", s, ref Students);
                for (int i = 0; i < Students.Count; i++)
                {
                    dataGridView1.Rows.Add(Students[i].nameStud, Students[i].assessment);
                }
            }
            else
            {
                if (radioButton1.Checked == true)
                {
                    Students.Clear();
                    string s = @"SELECT * FROM table1 WHERE nameStud = '" + textBox1.Text + "' AND groopa = '" + comboBox1.SelectedItem + "' ORDER BY  assessment ASC;";
                    db.Execute<Student>("student.db", s, ref Students);
                    for (int i = 0; i < Students.Count; i++)
                    {
                        dataGridView1.Rows.Add(Students[i].nameStud, Students[i].assessment);
                    }
                }

            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.SelectedIndex = -1;
            textBox1.Text = "";
            if (radioButton1.Checked == true | radioButton2.Checked == true)
            {
                ShowDatagridview();
                comboBox2.Enabled = true;
                textBox1.Enabled = true;
            }
            else
            {
                ShowDatagridview();
                comboBox2.Enabled = false;
                textBox1.Enabled = false;
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = -1;
            comboBox3.SelectedIndex = -1;

            dataGridView1.Rows.Clear();
        }
    }
}
