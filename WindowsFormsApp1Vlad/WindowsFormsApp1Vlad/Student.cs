using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1Vlad
{
   public class Student
    {
        private int Id;
        private string NameStud;
        private string Group;
        private string Assessment;
        private string subject;


        public string Subject
        {
            set { subject = value; }
            get { return subject; }
        }
        public int id
        {
            set { Id = value; }
            get { return Id; }
        }
        public string nameStud
        {
            set { NameStud = value; }
            get { return NameStud; }
        }
        public string group
        {
            set { Group = value; }
            get { return Group; }
        }
        public string assessment
        {
            set { Assessment = value; }
            get { return Assessment; }
        }

      


        public Student() { id = -1; NameStud = ""; Group = ""; Assessment = ""; Subject = ""; }
        public Student(int id, string NameStud, string Group, string Assessment, string Subject)
        { this.id = id; this.NameStud = NameStud; this.Group = Group; this.Assessment = Assessment; this.Subject = Subject; }
        public Student(string info)
        {
            if (info.Length > 0)
            {
                string[] val = info.Split('|');
                id = Convert.ToInt32(val[0]);
                NameStud = val[1];
                Group = val[2];
                Assessment =val[3];
                Subject = val[4];
               
            }
        }
    }
}
