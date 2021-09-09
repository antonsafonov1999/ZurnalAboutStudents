using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.SymbolStore;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Zurnal;

namespace WindowsFormsApp1Vlad
{
    public partial class Form2 : Form
    {
        public List<Student> Students = new List<Student>();
        ClassDataBase db = new ClassDataBase();

        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Students.Clear();
            string s = @"SELECT * FROM table1 GROUP BY groopa ;";
            db.Execute<Student>("student.db", s, ref Students);
           
			for (int i = 0; i < Students.Count; i++)
			{
				cbGroup.Items.Add(Students[i].group);				
				cbGroup.SelectedItem = cbGroup.Items[0].ToString();
			}
			if (Students.Count != 0)
				cbGroup.SelectedItem = Students[0].Subject;
			

			if (cbGroup.Items.Count == 0)
			{
				textBox2.Enabled = false;
				AddFIO.Enabled = false;

			}
			else
			{
				if (cbGroup.Items.Count != 0)
				{
					textBox2.Enabled = true;
					AddFIO.Enabled = true;
				}
			}
			Students.Clear();
			string c = @"SELECT * FROM table1 GROUP BY subject ;";
			db.Execute<Student>("student.db", c, ref Students);

			comboBox1.Items.Clear();
			Column3.Items.Clear();
			for (int i = 0; i < Students.Count; i++)
            {
				comboBox1.Items.Add(Students[i].Subject);
				Column3.Items.Add(Students[i].Subject);
			}
				
				Students.Clear();
		
		}

        private void cbGroup_TextChanged(object sender, EventArgs e)
        {
			ShowDatagridview();
		}
		private void ShowDatagridview()
		{
			dataGrid.Rows.Clear();
			Students.Clear();
			string s = @"SELECT * FROM table1 WHERE groopa='" + cbGroup.Text + "';";
			db.Execute<Student>("student.db", s, ref Students);
			for (int i = 0; i < Students.Count; i++)
			{
				dataGrid.Rows.Add(Students[i].nameStud, Students[i].Subject, Students[i].assessment);
			}
			Students.Clear();
			
		}



		
		private void AddFIO_Click(object sender, EventArgs e)
        {
			
			if (textBox2.Text != "")
				{
				string s = @"SELECT * FROM table1 WHERE nameStud ='" + textBox2.Text + "' AND groopa ='" + cbGroup.SelectedItem + "' ;";
				db.Execute<Student>("student.db", s, ref Students);
				if (Students.Count == 0)
				{
                    
					string g = "INSERT INTO table1(nameStud, groopa,assessment) values('" + textBox2.Text + "', '" + cbGroup.SelectedItem + "','" +"Не відомо"+ "')";
					db.ExecuteNonQuery("student.db", g);
				}
				else
				{
					MessageBox.Show("Такий студент вже існує");
				}
				Students.Clear();
			}
			else
			{
				MessageBox.Show("Поле \"ПІБ\" порожнє");
			}
			textBox2.Text = "";
			Students.Clear();

			Update();
			ShowDatagridview();
		}

        private void AddGroup_Click(object sender, EventArgs e)
        {
			Update();
			if (textBox1.Text != "")
			{

				string s = @"SELECT * FROM table1 WHERE groopa='" + textBox1.Text + "';";
				db.Execute<Student>("student.db", s, ref Students);
				if (Students.Count == 0)
				{
					cbGroup.Items.Add(textBox1.Text);
					cbGroup.SelectedItem = textBox1.Text;
				}
				else
				{
					MessageBox.Show("Така група вже існує");
				}
				Students.Clear();
			}
			else
			{
				MessageBox.Show("Поле \"Група\" порожнє");
			}

			textBox1.Text = "";
			Students.Clear();

			proverka();
		}
		public void proverka()
		{
			if (cbGroup.Items.Count == 0)
			{
				textBox2.Enabled = false;
				AddFIO.Enabled = false;
				
			}
			else
			{
				if (cbGroup.Items.Count != 0)
				{
					textBox2.Enabled = true;
					AddFIO.Enabled = true;
				
				}
			}
		}

        private void delStud_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
			proverka();
			Update();

		}

        private void delGroup_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
			try
			{
				if (MessageBox.Show("Ви впевнені?", "Видалення групи", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{

					string delG = "DELETE FROM table1 WHERE groopa = '" + cbGroup.Text + "' ";
					db.ExecuteNonQuery("student.db", delG);
					cbGroup.Items.Clear();
					string s = @"SELECT * FROM table1 GROUP BY groopa;";
					db.Execute<Student>("student.db", s, ref Students);
					for (int i = 0; i < Students.Count; i++)
					{
						cbGroup.Items.Add(Students[i].group);
					}
					if (Students.Count != 0)
					{
						cbGroup.SelectedItem = Students[0].group;
					}
					Students.Clear();
					ShowDatagridview();
					proverka();
				}
			}
			catch (Exception)
			{
				MessageBox.Show("Что-то пошло не так");
			}
			Update();
		}

        private void delStud_Click(object sender, EventArgs e)
        {

			Students.Clear();
			string s = @"SELECT * FROM table1 WHERE groopa='" + cbGroup.SelectedItem + "';";
			db.Execute<Student>("student.db", s, ref Students);
			if (Students.Count == 0)
			{
				MessageBox.Show("Група " + cbGroup.SelectedItem + " порожня. Видалення неможливе");
			}
			else
			{
				if (MessageBox.Show("Ви впевнені?", "Видалення студента",
				MessageBoxButtons.YesNo,
				MessageBoxIcon.Question,
				MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{
					int ind = dataGrid.SelectedCells[0].RowIndex;
					string fioStud = dataGrid.CurrentCell.Value.ToString();
					string delS = "DELETE FROM table1 WHERE nameStud = '" + fioStud + "' ";
					db.ExecuteNonQuery("student.db", delS);
					dataGrid.Rows.RemoveAt(ind);
				}
			}
			Students.Clear();
		}
	
	
		
	private void Update()
        {
			Students.Clear();
			string s = @"SELECT * FROM table1 WHERE groopa = '" + cbGroup.SelectedItem + "'";
			db.Execute<Student>("student.db", s, ref Students);
			for (int i = 0; i < (dataGrid.RowCount); i++)
			{
				string assessment =Convert.ToString( dataGrid[2,i].Value);
				 string save = "UPDATE table1 SET assessment = '" + assessment + "', subject ='" + Convert.ToString(dataGrid.Rows[i].Cells[1].Value) + "' WHERE nameStud = '" + Convert.ToString(dataGrid.Rows[i].Cells[0].Value) + "' AND groopa = '" + cbGroup.Text + "' ;";
				 db.ExecuteNonQuery("student.db", save);
				
			}
			Students.Clear();
		}
		private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
			Update();
		}

        private void cbGroup_Click(object sender, EventArgs e)
        {
			Update();
		}

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
			Update();
			try
			{
				if (MessageBox.Show("Ви впевнені?", "Видалення предмета", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
				{

					string delG = "DELETE FROM table1 WHERE subject = '" + comboBox1.Text + "' ";
					db.ExecuteNonQuery("student.db", delG);
					comboBox1.Items.Clear();
					Column3.Items.Clear();
					string s = @"SELECT * FROM table1 GROUP BY subject;";
					db.Execute<Student>("student.db", s, ref Students);
					for (int i = 0; i < Students.Count; i++)
					{
						comboBox1.Items.Add(Students[i].Subject);
						Column3.Items.Add(Students[i].Subject);
					}
					
					if (Students.Count != 0)
						comboBox1.SelectedItem = Students[0].Subject;
					
					Students.Clear();
					ShowDatagridview();
					proverka();
				}
			}
			catch (Exception)
			{
				MessageBox.Show("Что-то пошло не так");
			}
			
		}

        private void button1_Click(object sender, EventArgs e)
        {
			
			if (textBox3.Text != "" & cbGroup.Text !="")
			{
				
				string s = @"SELECT * FROM table1 WHERE subject ='" + textBox3.Text + "' ;";
				db.Execute<Student>("student.db", s, ref Students);
				if (Students.Count == 0)
				{
					Column3.Items.Add(textBox3.Text);
					comboBox1.Items.Add(textBox3.Text);
				}

				else
				{
					MessageBox.Show("Такий предмет вже існує");
				}
				Students.Clear();
			}
			else
			{
				MessageBox.Show("в групе нет студентов");
			}
			textBox3.Text = "";
			
			

			
		}

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void dataGrid_MouseLeave(object sender, EventArgs e)
        {
			Update();
        }
    }
}
