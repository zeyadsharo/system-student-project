﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace project_student1
{
    public abstract class Person 
    {
        public abstract bool Age( ref int a);
        public abstract bool this[float index,string Str] { get; }    
        //add indexer here 
    }
    class student
    {
        public string subject;
        public int stnumber = 0;
        public int studAge;
        public string stname;
        public string sex;
        public float quizz1;
        public float quizz2;
        public float assigment;
        public float midterm;
        public float final;
        public float total;
        static public int itemcount = -1;
        public student()
        {
            subject = null;
            stnumber = 0;
            studAge = 0;
            stname = "no name";
            sex = null;
            quizz1 = 0;
            quizz2 = 0;
            assigment = 0;
            midterm = 0;
            final = 0;
            total = 0;
        }
        public student(int stnumber, int studAge, string stname, string sex, float quizz1, float quizz2, float assigment, float midterm, float final, float total)
        {
            //this.subject = subject;
            this.stnumber = stnumber;
            this.studAge = studAge;
            this.stname = stname;
            this.sex = sex;
            this.quizz1 = quizz1;
            this.quizz2 = quizz2;
            this.assigment = assigment;
            this.midterm = midterm;
            this.final = final;
            this.total = total;
        }//end the prametrized constractor   
    }//end the class student 
    class Initial:Person
    {
        student[] st = new student[30];
        public int itemcount = 0;
        public override bool Age( ref int a)
        {
            if (a<0&&a>-30)
            {
                a = a * -1;
                return true;
            }
            else
            {
                if (a > 30||a <-30)
                {
                    Console.Write("You have enter a wrong age!!\n ReEnter student's Age:");
                    return false;
                }
            }
            return true;
          
        }
            public override bool this[float index, string Str]
        {
            get
            {
                if ((index >= 0 && index <= 5) && Str == "quizz1" || Str == "quizz2")
                { return true; }
                else if ((index >= 0 && index <= 10) && Str == "assigment")
                { return true; }
                else if ((index >= 0 && index <= 20) && Str == "midterm")
                { return true; }
                else if ((index >= 0 && index <= 60) && Str == "final")
                { return true; }
                else
                { Console.Write("You have enter a wrong num!ReEnter the score:"); return false; }
            }
        }   
        
        public  bool ID(string STR,int id=-1)
        {
            bool flag = true;
            for (int i = 0; i < STR.Length; i++)
            {
              
                if (!(STR[i]>='0'&&STR[i]<='9'))
                {
                    Console.Write("You have enter a wrong ID must be a integer number!!\nReEnter student's ID:");
                    flag = false;
                    break;
                }
                else
                { flag = true;}
            }
            if (itemcount>0)
                {
                for (int i = 0; i < itemcount; i++)
                {
                    if(st[i].stnumber==id)
                    {
                        Console.Write("the ID already Exists!!\nReEnter student's ID:");
                        flag = false;
                        break;
                    }
                    else
                    { flag = true; }
                }
                }
            return flag;         
        }
        public void displaymenu()
        {
            WriteFile(@"D:\project\test.doc", "\n----------------Students-----------------\n");
            Console.WriteLine("======================================================\n                         MENU                         \n======================================================");
            Console.WriteLine(" 1.Add student records");
            Console.WriteLine(" 2.Delete student records");
            Console.WriteLine(" 3.Update student records");
            Console.WriteLine(" 4.View all student records");
            Console.WriteLine(" 5.Calculate an average of a selected student's scores");
            Console.WriteLine(" 6.Show student who get the max total score");
            Console.WriteLine(" 7.Show student who get the min total score");
            Console.WriteLine(" 8.Find a student by ID");
            Console.WriteLine(" 9.Sort students by TOTAL");
            Console.WriteLine(" 10.if students IsSucceed or not");

            //create an array to store only 30 students'records for testing.
            int choice;
            string confirm;

            do
            {
                Console.Write("Enter your choice(1-10):");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {

                    case 1:
                        add(ref itemcount);
                        break;
                    case 2:
                        delete(st, ref itemcount);
                        break;
                    case 3:
                        update(st, itemcount);
                        break;
                    case 4:
                        viewall(st, itemcount);
                        break;
                    case 5:
                        average(st, itemcount);
                        break;
                    case 6:
                        showmax(st, itemcount);
                        break;
                    case 7:
                        showmin(st, itemcount);
                        break;
                    case 8:
                        find(st, itemcount);
                        break;
                    case 9:
                        bubblesort(st, itemcount);
                        break;
                    case 10:
                        string m = (IsSucceed) ? "The Student is Succeed" : "failing";
                        Console.WriteLine(m);
                        break;

                    default: Console.WriteLine("invalid"); break;
                }
                Console.Write("Press y or Y to continue:");

                confirm = Console.ReadLine().ToString();
            } while (confirm == "y" || confirm == "Y");
        }//end the desplay menu
        public static void Readfile(string path)
        {
            StreamReader readfile = new StreamReader(path);
            string filedata = readfile.ReadToEnd();
            if(File.Exists(path))
            {
                Console.WriteLine(filedata);
            }
        }
        public static void WriteFile(string path, string data)
        {
            StreamWriter writedata = new StreamWriter(path, true);
            writedata.WriteAsync(data);
            writedata.Close();
        }
        public bool IsSucceed // to now if the student is Succeed or not
        {
            get
            {
                Console.Write("Enter student's ID:");
                int id = Convert.ToInt16(Console.ReadLine());   
                int i = search(st, id, itemcount);
                if (i >= 0 || i != -1)
                {
                    if (st[i].total >= 50)
                        return true;
                    else
                        return false;
                }
                else
                {
                    Console.WriteLine("The record doesn't exits.");
                    return false;
                }
                    
            }
        }
        public void add(ref int itemcount)
        {
            Initial n1 = new Initial();
            Console.Write("Enter student's ID:");
            
            agian: string id =Console.ReadLine();
            
            if (!ID(id, int.Parse(id)))
            {
                goto agian;
            }          
            int stnumber = int.Parse(id);
            Console.Write("Enter student's Age:");
            Ask:  int stage = int.Parse(Console.ReadLine());
            if (!Age(ref stage))
            {
                goto Ask;
            }
            Do:     
            Console.Write("Enter student's Name:");
            string stname = Console.ReadLine().ToString();
            bool result = stname.All(x => char.IsLetter(x) || x == ' ' || x == '.');
            if (result != true)
            {
                Console.Write("You have entered a wrong Name !! , Re");
                goto Do;
            }
            Agan: Console.Write("Enter student's Sex(F or M):");
            string sex = Console.ReadLine().ToUpper();
            if (!(sex == "M" || sex == "F"))
            {
                Console.Write("You have entered wrong !! Re");
                goto Agan;
            }
            Console.Write("Enter student's quizz1 score from 5:");
           Ask1:    float quizz1 = float.Parse(Console.ReadLine());
            if(!n1[quizz1,"quizz1"])
            {
                goto Ask1;
            }
            Console.Write("Enter student's quizz2 score from 5:");
           Ask2: float quizz2 = float.Parse(Console.ReadLine());
            if(!n1[quizz2, "quizz2"])
            {
                 goto Ask2;
            }
            Console.Write("Enter student's assigment score from 10:");
           Ask3: float assigment = float.Parse(Console.ReadLine());
            if (!n1[assigment, "assigment"])
            {
                goto Ask3;
            }

            Console.Write("Enter student's mid term score from 20:");
           Ask4: float midterm = float.Parse(Console.ReadLine());
            if (!n1[midterm, "midterm"])
            {
                goto Ask4;
            }
            Console.Write("Enter student's final score from 60:");
         Ask5:   float final = float.Parse(Console.ReadLine());
            if (!n1[final, "final"])
            {
                goto Ask5;
            }
            float total = quizz1 + quizz2 + assigment + midterm + final;           
            WriteFile(@"D:\project\test.doc","\nStudent Number:("+(itemcount+1)+")\nID: "+id+"\nName: " +stname +"\nsex: "+sex+"\nAge: "+ stage+"\nQuizz1: "+quizz1+"\nQuizz2: "+quizz2+ "\nassigment: "+assigment+ "\nmidterm: "+ midterm+ "\nfinal: "+ final+ "\ntotal: "+total+"\n" );          
            st[itemcount] = new student(stnumber, stage, stname, sex, quizz1, quizz2, assigment, midterm, final, total);
            itemcount++;         
        }//end the added information 
        public int search(student[] st, int id, int itemcount)
        {
            int found = -1;
            for (int i = 0; i < itemcount && found == -1; i++)
            {
                if (st[i].stnumber == id) found = i;
                else found = -1;
            }
            return found;
        }//end the search function 
        static void clean(student[] st, int index)
        {
            st[index].stnumber = 0;
            st[index].studAge = 0;
            st[index].stname = null;
            st[index].sex = null;
            st[index].quizz1 = 0;
            st[index].quizz2 = 0;
            st[index].assigment = 0;
            st[index].midterm = 0;
            st[index].final = 0;
            st[index].total = 0;
        }
        public void delete(student[] st, ref int itemcount)
        {
            int id;
            int index;
            if (itemcount > 0)
            {
                Console.Write("Enter student's ID:");
                id = int.Parse(Console.ReadLine());
                index = search(st, id, itemcount);
                if ((index != -1) && (itemcount != 0))
                {
                    if (index == (itemcount - 1))
                    {
                        clean(st, index);
                        this.itemcount = itemcount--;
                        Console.WriteLine("The record was deleted.");
                    }
                    else
                    {
                        for (int i = index; i < itemcount - 1; i++)
                        {
                            st[i] = st[i + 1];
                            clean(st, itemcount);
                            --itemcount;
                        }
                    }
                }
                else Console.WriteLine("The record doesn't exist.Check the ID and try again.");
            }
            else Console.WriteLine("No record to delete");
        }
        public void viewall(student[] st, int itemcount)
        {

            int i = 0;

            Console.WriteLine("{0,-5}{1,-20}{2,-5}{3,-5}{4,-5}{5,-5}{6,-5}{7,-5}{8,-5}{9}(column index)", "0", "1", "2", "3", "4", "5", "6", "7", "8","9");
            Console.WriteLine("{0,-5}{1,-20}{2,-5}{3,-5}{4,-5}{5,-5}{6,-5}{7,-5}{8,-5}{9,-5}", "ID", "NAME","Age","SEX", "Q1", "Q2", "As", "Mi", "Fi", "TOTAL");
            Console.WriteLine("=====================================================");
            while (i < itemcount)
            {
                if (st[i].stnumber != -1)
                {
                    Console.Write("{0,-5}{1,-20}{2,-5}{3,-5}", st[i].stnumber, st[i].stname, st[i].studAge,st[i].sex);
                    Console.Write("{0,-5}{1,-5}{2,-5}", st[i].quizz1, st[i].quizz2, st[i].assigment);
                    Console.Write("{0,-5}{1,-5}{2,-5}", st[i].midterm, st[i].final, st[i].total);
                    Console.Write("\n");
                }
                i = i + 1;
            }
        }
        public void update(student[] st, int itemcount)
        {
            int id;
            int column_index;
            Console.Write("Enter student's ID:");
            id = int.Parse(Console.ReadLine());
            Console.WriteLine("1.Name 2.Sex 3. quizz1 4.quizz2 5.assigment score 6.midterm score 7.final score 8.Age");
            Console.Write("Which field you want to update(1-8)?:");
            column_index = int.Parse(Console.ReadLine());
            int index = search(st, id, itemcount);
            if ((index != -1) && (itemcount != 0))
            {
                if (column_index == 1)
                {
                    Console.Write("Enter student's Name:");
                    st[index].stname = Console.ReadLine().ToString();
                }
                else if (column_index == 2)
                {
                    Console.Write("Enter student's Sex(F or M):");
                    st[index].sex = Console.ReadLine().ToString();
                }
                else if (column_index == 3)
                {
                    Console.Write("Enter student's quizz1 score:");
                    st[index].quizz1 = float.Parse(Console.ReadLine());
                }
                else if (column_index == 4)
                {
                    Console.Write("Enter student's quizz2 score:");
                    st[index].quizz2 = float.Parse(Console.ReadLine());
                }
                else if (column_index == 5)
                {
                    Console.Write("Enter student's assigment score:");
                    st[index].assigment = float.Parse(Console.ReadLine());
                }
                else if (column_index == 6)
                {
                    Console.Write("Enter student's midterm score:");
                    st[index].midterm = float.Parse(Console.ReadLine());
                }
                else if (column_index == 7)
                {
                    Console.Write("Enter student's final score:");
                    st[index].final = float.Parse(Console.ReadLine());
                }
                else if (column_index == 8)
                {
                    Console.Write("Enter student's Age:");
                    st[index].studAge = int.Parse(Console.ReadLine());
                }
                else Console.WriteLine("Invalid column index");
                st[index].total = st[index].quizz1 + st[index].quizz2 + st[index].assigment + st[index].midterm + st[index].final + st[index].studAge;
            }
            else Console.WriteLine("The record deosn't exits.Check the ID and try again.");
        }
        public void average(student[] st, int itemcount)
        {
            int id;
            float avg = 0;
            Console.Write("Enter students'ID:");
            id = int.Parse(Console.ReadLine());
            int index = search(st, id, itemcount);
            if (index != -1 && itemcount > 0)
            {
                st[index].total = st[index].quizz1 + st[index].quizz2 + st[index].assigment + st[index].midterm + st[index].final;
                avg = st[index].total / 5;
            }
            Console.WriteLine("The average score is {0}.", avg);
        }
        public void showmax(student[] st, int itemcount)
        {
            float max = st[0].total;
            int index = 0;
            Console.WriteLine(itemcount);
            if (itemcount >= 2)
            {
                for (int j = 0; j < itemcount - 1; ++j)
                    if (max < st[j + 1].total)
                    {
                        max = st[j + 1].total;
                        index = j + 1;
                    }
            }
            else if (itemcount == 1)
            {
                index = 0;
                max = st[0].total;
            }
            else Console.WriteLine("Not record found!");
            if (index != -1) Console.WriteLine("The student with ID:{0} gets the highest score {1}.", st[index].stnumber, max);
        }
        //method to show min total score
        public void showmin(student[] st, int itemcount)
        {
            float min = st[0].total;
            int index = 0;
            if (itemcount >= 2)
            {
                for (int j = 0; j < itemcount - 1; ++j)
                    if (min > st[j + 1].total)
                    {
                        min = st[j + 1].total;
                        index = j + 1;
                    }
            }
            else if (itemcount == 1)
            {
                index = 0;
                min = st[0].total;
            }
            else Console.WriteLine("No record found!");
            if (index != -1) Console.WriteLine("The student with ID:{0} gets the lowest score {1}.", st[index].stnumber, min);
        }
        //method to find record
        public void find(student[] st, int itemcount)
        {
            int id;
            Console.Write("Enter student's ID:");
            id = int.Parse(Console.ReadLine());
            int index = search(st, id, itemcount);
            if (index != -1)
            {
                Console.Write("{0,-5}{1,-20}{2,-5}", st[index].stnumber, st[index].stname, st[index].sex);
                Console.Write("{0,-5}{1,-5}{2,-5}", st[index].quizz1, st[index].quizz2, st[index].assigment);
                Console.Write("{0,-5}{1,-5}{2,-5}", st[index].midterm, st[index].final, st[index].total);
                Console.WriteLine();
            }
            else Console.WriteLine("The record doesn't exits.");
        }
        public void bubblesort(student[] dataset, int n)
        {
            int i, j;
            for (i = 0; i < n; i++)
                for (j = n - 1; j > i; j--)
                    if (dataset[j].total < dataset[j - 1].total)
                    {
                        student temp = dataset[j];
                        dataset[j] = dataset[j - 1];
                        dataset[j - 1] = temp;
                    }
        }
    } //end the intial student 
    class Teacher
    {
        public string teachId;
        public string teachname;
        public int teachAge;
        public string teachDept;
        public string teachingstage;
        public string teachsex;
        public Teacher()
        {
            this.teachId = null;
            this.teachname = null;
            this.teachAge = -1;
            this.teachDept = null;
            this.teachingstage = null;
            this.teachsex = null;

        }
        public Teacher(string teachId, string teachname, int teachAge, string teachDept, string teachingstage, string teachsex)
        {
            this.teachId = teachId;
            this.teachname = teachname;
            this.teachAge = teachAge;
            this.teachDept = teachDept;
            this.teachingstage = teachingstage;
            this.teachsex = teachsex;
        }
    }

    class InitialTeacher 
    {
        Teacher[] te = new Teacher[20];
        public int itemcount = 0;
        public void displaymenu()
        {
            Initial.WriteFile(@"D:\project\test.doc", "\n----------------Teachers-----------------\n");
            Console.WriteLine("======================================================\n                         MENU                         \n======================================================");
            Console.WriteLine(" 1.Add Teacher records");
            Console.WriteLine(" 2.Delete Teacher records");
            Console.WriteLine(" 3.Update Teacher records");
            Console.WriteLine(" 4.View all Teacher records");
            Console.WriteLine(" 5.Find the Teacher Sex and Department by ID");
            Console.WriteLine(" 6.Find the Teacher Age and Stage that teaching by ID");
            Console.WriteLine(" 7.Find a Teacher name by ID");


            //create an array to store only 20 Teachers'records for testing.
            //show menu
            int choice;
            string confirm;

            do
            {
                Console.Write("\nEnter your choice(1-7):");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        add(ref itemcount);
                        break;
                    case 2:
                        delete(te, ref itemcount);
                        break;
                    case 3:
                        update(te, ref itemcount);
                        break;
                    case 4:
                        viewall(te, ref itemcount);
                        break;
                    case 5:
                        showteachsex(te, ref itemcount);
                        break;
                    case 6:
                        showteachAge(te, ref itemcount);
                        break;
                    case 7:
                        ID(te, ref itemcount);
                        break;


                        //default: Console.WriteLine("invalid"); break;

                }
                Console.Write("Press y or Y to continue:");
                confirm = Console.ReadLine().ToString();
            } while (confirm == "y" || confirm == "Y");
        }
        //method add/append a new record
        public int search(Teacher[] te, string id, int itemcount)
        {
            int found = -1;
            for (int i = 0; i < itemcount && found == -1; i++)
            {
                if (te[i].teachId == id)
                    found = i;
                else
                    found = -1;
            }
            return found;
        }
        public  bool Age(ref int a)
        {

            ag:
            if (a <= 60 && a > 19)
            {
                return true;
            }
            else
            {
                Console.Write("You have enter a wrong age!!\nReEnter student's Age:");
                Console.Write("Enter The age again : ");
                a = Convert.ToInt16(Console.ReadLine());
                goto ag;
            }
        }
        public bool ID(string STR)
        {
            bool flag = true;
            for (int i = 0; i < STR.Length; i++)
            {
                if (!(STR[i] >= '0' && STR[i] <= '9'))
                {
                    Console.Write("You have enter a wrong ID must be a integer number!!\nReEnter student's ID:");
                    flag = false;
                    break;
                }
                else
                { flag = true; }
            }
            return flag;
        }
        public void add(ref int itemcount)
        {
            Again:
            Console.WriteLine();
            Console.Write("Enter Teacher's ID:");             
           AGIN: string teachId = Console.ReadLine().ToString();
            if(!ID(teachId))
            {
                goto AGIN;
            }
            if (search(te, teachId, itemcount) != -1)
            {
                Console.WriteLine("This ID already exists.");
                goto Again;
            }
            Doagain:
            Console.Write("Enter Teacher's Name:");
            string teachname = Console.ReadLine();
            bool resulte = teachname.All(x => char.IsLetter(x) || x == ' '|| x == '.');
            if (resulte != true)
            {
                Console.Write("\nYou have entered a wrong Name !! , Re");
                goto Doagain;
            }
            Agan: Console.Write("Enter Teacher's Sex(F or M):");
            string teachsex = Console.ReadLine();
            if (!(teachsex == "f" || teachsex == "F") && !(teachsex == "m" || teachsex == "M"))
            {
                Console.Write("You have entered wrong !! Re");
                goto Agan;
            }
            Console.Write("Enter Teacher's Age:");
            int teachAge = int.Parse(Console.ReadLine());
            Age(ref teachAge);
            Console.Write("Enter Teacher's dept:");
            string teachDept = Console.ReadLine();
            Console.Write("Enter Teacher's teaching stage:");
            string teachingstage = Console.ReadLine();
            Initial.WriteFile(@"D:\project\test.doc","Teacher Number:("+(itemcount+1)+")\nTeacher's ID:" + teachId + "\nTeacher's Name: " + teachname + "\nTeacher's Sex: " + teachsex + "\nTeacher's Age: " + teachsex + "\nTeacher's dept: " + teachDept + "\nteaching stage: " + teachingstage);
           
            te[itemcount] = new Teacher(teachId, teachname, teachAge, teachDept, teachingstage, teachsex);
            ++itemcount;
        }
        public void delete(Teacher[] te, ref int itemcount)
        {
            string id;
            int index;
            if (itemcount >= 0)
            {
                Console.Write("Enter Teacher's ID:");
                id = Console.ReadLine();
                index = search(te, id.ToString(), itemcount);
                if ((index != -1) && (itemcount != 0))
                {
                    if (index == (itemcount - 1))
                    {
                        clean(te, ref index);
                        --itemcount;
                        Console.WriteLine("The record was deleted.");
                    }
                    else
                    {
                        for (int i = index; i < itemcount - 1; i++)
                        {
                            te[i] = te[i + 1];
                            clean(te, ref itemcount);
                            --itemcount;
                        }
                    }
                }
                else Console.WriteLine("The record doesn't exist.Check the ID and try again.");
            }
            else Console.WriteLine("No record to delete");
        }

        public void clean(Teacher[] te, ref int itemcount)
        {
            te[itemcount].teachId = null;
            te[itemcount].teachname = null;
            te[itemcount].teachsex = null;
            te[itemcount].teachAge = -1;
            te[itemcount].teachDept = null;
            te[itemcount].teachingstage = null;
        }
        public void update(Teacher[] te, ref int itemcount)
        {
            string id;
            int column_index;
            Console.Write("Enter Teacher's ID:");
            id = Console.ReadLine();
            Console.WriteLine("1.Name 2.sex 3.Age 4.Dept 5.Stage");
            Console.Write("Which field you want to update(1-5)?:");
            column_index = int.Parse(Console.ReadLine());

            int index = search(te, id.ToString(), itemcount);

            if ((index != -1) && (itemcount != 0))
            {
                if (column_index == 1)
                {
                    Console.Write("Enter Teacher's Name:");

                    te[index].teachname = Console.ReadLine().ToString();
                }

                else if (column_index == 2)
                {
                    Console.Write("Enter Teacher's Sex(F or M):");
                    te[index].teachsex = Console.ReadLine().ToString();
                }
                else if (column_index == 3)
                {
                    Console.Write("Enter Teacher's Age:");
                    te[index].teachAge = int.Parse(Console.ReadLine());
                    Age(ref te[index].teachAge);
                }
                else if (column_index == 4)
                {
                    Console.Write("Enter Teacher's dept:");
                    te[index].teachDept = Console.ReadLine().ToString();
                }
                else if (column_index == 5)
                {
                    Console.Write("Enter Teacher's teaching stage:");
                    te[index].teachingstage = Console.ReadLine().ToString();
                }
            }
            else Console.WriteLine("The record deosn't exits.Check the ID and try again.");
        }
        public void ID(Teacher[] te, ref int itemcount)
        {
            string m = "This teacher Id deosn't exits";
            string id = Console.ReadLine().ToString();
            int i = search(te, id, itemcount);
            if (i != -1)
                m = te[i].teachname;
            Console.WriteLine(m);
        }
        public void showteachAge(Teacher[] te, ref int itemcount)
        {

            string m = "This teacher Id deosn't exits";
            string la = "This teacher Id deosn't exits";
            Console.Write("Enter teacher ID: ");
            string id = Console.ReadLine().ToString();
            int i = search(te, id, itemcount);
            if (i != -1)
            {
                la = te[i].teachingstage;
                m = te[i].teachname;
            }
            if (m == la)
                Console.WriteLine(m);
            else
                Console.WriteLine("The teacher name is : " + m + " , teaching stage is : " + la);
        }
        public void showteachsex(Teacher[] te, ref int itemcount)
        {
            string m = "This teacher Id deosn't exits";
            string la = "This teacher Id deosn't exits";
            Console.Write("Enter teacher ID: ");
            string id = Console.ReadLine().ToString();
            int i = search(te, id, itemcount);
            if (i != -1)
            {
                la = te[i].teachDept;
                m = te[i].teachsex;
            }
            if (m == la)
                Console.WriteLine(m);
            else
                Console.WriteLine("teacher department  is : " + la + " ,The teacher sex is : " + m);
        }
        public void viewall(Teacher[] te, ref int itemcount)
        {
            int i = 0;
            Console.WriteLine("{0,-5}{1,-20}{2,-5}{3,-5}{4,-5}{5,-5}(column index)", "0", "1", "2", "3", "4", "5");
            Console.WriteLine("{0,-5}{1,-20}{2,-5}{3,-5}{4,-5}{5,-5}", "ID", "NAME", "SEX", "Age", "dept", "stage");
            Console.WriteLine("=====================================================");
            while (i < itemcount)
            {
                if (te[i].teachId != null)
                {
                    Console.Write("{0,-5}{1,-20}{2,-5}", te[i].teachId, te[i].teachname, te[i].teachsex);
                    Console.Write("{0,-5}{1,-5}{2,-5}", te[i].teachAge, te[i].teachDept, te[i].teachingstage);
                    Console.Write("\n");
                }
                i = i + 1;
            }
        }

    } //end the initial student

    class SchoolInformation   //////////////
    {
        // return a string consisting of four or five lines of school infromation

        public static string school = null;
        public static string motto = null;
        public static string colors = null;
        public static string master = null;
        public static int year = 0;
        public SchoolInformation()
        {
            Initial.WriteFile(@"D:\project\test.doc", "\n----------------Welcome------------------\n");
            Doagan:
            Console.Write("Enter the school name : ");
            school = Console.ReadLine();
            bool resul = school.All(x => char.IsLetter(x) || x == ' ' || x == '.');
            if (resul != true)
            {
                Console.Write("\nYou have entered a Wrong Name !! , Re");
                goto Doagan;
            }
            Console.Write("Enter the school motto : ");
            motto = Console.ReadLine();
            Console.Write("Enter the school Uniforms Colors : ");
            colors = Console.ReadLine();
            Console.Write("Enter the school master name : ");
            master = Console.ReadLine();
            Console.Write("Enter The school first year was established : ");
            year = int.Parse(Console.ReadLine());
           Initial. WriteFile(@"D:\project\test.doc","\nSchool Name: " + school + "\nSchool Motto: " + motto + "\nUniforms Colors: " + colors + "\nschool master: " + master + "\nThe school first year was established: " + year+"\n");
            
           SchoolInformation.PrintSchoolInformation();
        }      
        public SchoolInformation(string school, string motto, string colors, string master, int year)
        {
            SchoolInformation.school = school;
            SchoolInformation.motto = motto;
            SchoolInformation.colors = colors;
            SchoolInformation.master = master;
            SchoolInformation.year = year;
            SchoolInformation.PrintSchoolInformation();
        }
        public static void PrintSchoolInformation()
        {
            string information = "\n\n" + "#  The Schoole Name:  " + school +
"\n" +
                                 "#  The SchooleMotto:         " + motto + "\n"
+
                                 "#  The Schoole Uniforms Colors: " + colors +
"\n" +
                                 "#  The School First Year was established : " +
year + "\n" +
                                 "#  The School Master: " + master;
            Console.WriteLine(information);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {

            SchoolInformation school = new SchoolInformation();
            student s1 = new student();
            Initial I1 = new Initial();
            I1.displaymenu();           
            Teacher TE = new Teacher();
            InitialTeacher T = new InitialTeacher();
            T.displaymenu();
            Console.ReadKey();
        }
    }
}
