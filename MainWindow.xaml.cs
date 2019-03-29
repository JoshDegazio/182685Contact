/* Josh Degazio
 * Started: March 25th, 2019. Due: March 29th, 2019.
 * Unit Two Culminating
 */
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.IO;

namespace _182685Contact
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>

    public partial class MainWindow : Window
    {
        //Initiliaze Contact in order to access from a global scope
        Contact contact = new Contact("", "", "", 0, 0, 0);

        //Initialize other variables
        string[] bDayArray;
        char[] monthChars;
        string bDay;
        string year;
        string month;
        string day;

        public MainWindow()
        {
            InitializeComponent();

            //As the program starts, read date from file and fill textboxes with data from file.
            contact.ReadFromFile(inpt_FirstName, inpt_LastName, inpt_Email, inpt_Bday);
            contact.DisplayInfo(outpt_Contact);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Create bool
            bool allInts = false;

            //Set Values
            bDay = inpt_Bday.Text;
            //Split "YYYY/MM/DD"
            bDayArray = bDay.Split('/');
            //year = first split or "YYYY"
            year = bDayArray[0];
            //month = second split or "MM"
            month = bDayArray[1];
            //allow for validation of inputted month
            monthChars = month.ToCharArray();
            //day = third split or "DD"
            day = bDayArray[2];

            //If month contains more than 0 characters, check if they are all digits
            if (monthChars.Length > 0)
            {
                if (char.IsDigit(monthChars[0]))
                {
                    for (int x = 0; x < monthChars.Length; x++)
                    {
                        if (char.IsDigit(monthChars[x]))
                        {
                            //all characters are digits
                            allInts = true;
                        }
                        else
                        {
                            //not all characters are digits
                            allInts = false;
                            MessageBox.Show("The birthday format you have entered is currently not accepted. Please enter in the YYYY/MM/DD format, or YYYY/Month/DD \nError Message: 2");
                        }
                    }
                }
            }
            else
            {
                //month does not contain any characters
                MessageBox.Show("The birthday format you have entered is currently not accepted. Please enter in the YYYY/MM/DD format, or YYYY/Month/DD \nError Message: 1");
            }

            //if month doesn't contain all digits, check if the characters spell a month
            if (allInts == false)
            {
                if (month == "January")
                {
                    month = "01";
                    CreateContact(year, month, day);
                    contact.WriteToFile();
                }
                else if (month == "February")
                {
                    month = "02";
                    CreateContact(year, month, day);
                    contact.WriteToFile();
                }
                else if (month == "March")
                {
                    month = "03";
                    CreateContact(year, month, day);
                    contact.WriteToFile();

                }
                else if (month == "April")
                {
                    month = "04";
                    CreateContact(year, month, day);
                    contact.WriteToFile();

                }
                else if (month == "May")
                {
                    month = "05";
                    CreateContact(year, month, day);
                    contact.WriteToFile();
                }
                else if (month == "June")
                {
                    month = "06";
                    CreateContact(year, month, day);
                    contact.WriteToFile();
                }
                else if (month == "July")
                {
                    month = "07";
                    CreateContact(year, month, day);
                    contact.WriteToFile();
                }
                else if (month == "August")
                {
                    month = "08";
                    CreateContact(year, month, day);
                    contact.WriteToFile();
                }
                else if (month == "September")
                {
                    month = "09";
                    CreateContact(year, month, day);
                    contact.WriteToFile();
                }
                else if (month == "October")
                {
                    month = "10";
                    CreateContact(year, month, day);
                    contact.WriteToFile();
                }
                else if (month == "November")
                {
                    month = "11";
                    CreateContact(year, month, day);
                    contact.WriteToFile();
                }
                else if (month == "December")
                {
                    month = "12";
                    CreateContact(year, month, day);
                    contact.WriteToFile();
                }
                else
                {
                    //otherwise they typed something they shouldn't have. Let them know!

                    MessageBox.Show("The birthday format you have entered is currently not accepted. Please enter in the YYYY/MM/DD format, or YYYY/Month/DD \nError Message: 3");
                }
            }
            //else if month does contain all digits, run normally
            else if (allInts == true)
            {
                CreateContact(year, month, day);
                contact.WriteToFile();
            }
        }
        
        //Make code neater
        private void CreateContact(string year, string month, string day)
        {
            contact.ReadFromInputForm(inpt_FirstName, inpt_LastName, inpt_Email, year, month, day, outpt_Contact);
        }
    }

    /// <summary>
    /// Public class containing information and methods regarding a single contact.
    /// </summary>
    public class Contact
    {
        //Initialize global variables
        //public
        public string firstName;
        public string lastName;
        //private
        private int age;
        private int yearBorn;
        private int monthBorn;
        private int dayBorn;
        private string email;
        private string fileText;
        //array
        private string[] fileVariables = new string[6];
        private int[] fileValues = new int[3];

        /// <summary>
        /// Constructor for class "Contact"
        /// </summary>
        /// <param name="fN">First Name</param>
        /// <param name="lN">Last Name</param>
        /// <param name="e">Email</param>
        /// <param name="yB">Year Born</param>
        /// <param name="mB">Month Born</param>
        /// <param name="dB">Day Born</param>
        public Contact(string fN, string lN, string e, int yB, int mB, int dB)
        {
            firstName = fN;
            lastName = lN;
            email = e;
            yearBorn = yB;
            monthBorn = mB;
            dayBorn = dB;
        }

        /// <summary>
        /// Reads data from file named "contact.txt"
        /// </summary>
        /// <param name="fName">First Name.</param>
        /// <param name="lName">Last Name.</param>
        /// <param name="em">Email</param>
        /// <param name="bDay">Birthday</param>
        public void ReadFromFile(TextBox fName, TextBox lName, TextBox em, TextBox bDay)
        {
            //if possible
            try
            {
                //only have streamreader exist while reading from stream
                using (StreamReader contactReader = new StreamReader("contact.txt"))
                {
                    //read
                    fileText = contactReader.ReadLine();
                    //close
                    contactReader.Close();
                }

                //Split comma seperated list
                fileVariables = fileText.Split(',');

                //split integers and strings
                for (int x = 3; x < 6; x++)
                {
                    int.TryParse(fileVariables[x], out fileValues[x-3]);
                }
                
                //set global class strings
                firstName = fileVariables[0];
                lastName = fileVariables[1];
                email = fileVariables[2];

                //set global class integers
                yearBorn = fileValues[0];
                monthBorn = fileValues[1];
                dayBorn = fileValues[2];

                //set input textbox text
                fName.Text = firstName;
                lName.Text = lastName;
                em.Text = email;
                bDay.Text = yearBorn + "/" + monthBorn + "/" + dayBorn;
            }
            catch (Exception ex)
            {
                //uh-oh, error
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Method that accurately returns the age
        /// </summary>
        /// <returns>age of contact</returns>
        public int GetAge()
        {
            string birthdaySTR = "";

            //Debug problem with tryparse getting rid of leading 0 in string when month = "01"-"09"
            if (monthBorn.ToString().Length == 1)
            {
                 birthdaySTR = yearBorn.ToString() + "0" + monthBorn.ToString() + dayBorn.ToString();
            }
            //month isn't "01"-"09"
            else
            {
                birthdaySTR = yearBorn.ToString() + monthBorn.ToString() + dayBorn.ToString();
            }
            int.TryParse(birthdaySTR, out int birthdayINT);

            //what day is it?
            string currentDateTime = DateTime.Today.ToString();
            //oh ok, it's today
            string[] currentDay = currentDateTime.Split(' ');
            
            //make 2019-03-27 = 20190307 for easier math
            while (currentDay[0].Contains('-'))
            {
                int index = currentDay[0].IndexOf('-');
                currentDay[0] = currentDay[0].Remove(index, 1);
            }

            int.TryParse(currentDay[0], out int currentDayInt);

            //Find age by subtracting YYYYMMDD by YYYYMMDD. and dropping last 4 digits
            age = currentDayInt - birthdayINT;

            string calculateAge = age.ToString();

            for (int x = calculateAge.Length - 1; x > (calculateAge.Length - 5); x--)
            {
                string ageStr = age.ToString();
                int.TryParse(ageStr.Remove(x, 1), out age);
            }

            //here is your age, congrats!
            return age;
        }

        /// <summary>
        /// Updates user interface output
        /// </summary>
        /// <param name="output">Textblock containing information regarding contact</param>
        public void DisplayInfo(TextBlock output)
        {
            //yeah, that's my contact. But why does he have a picture of steve buscemi?
            output.Text = firstName + ", " + lastName + ", \n" + email + "\n " + GetAge() + "(" + yearBorn + "-" + monthBorn + "-" + dayBorn + ")\n"  ;
        }

        /// <summary>
        /// Want to change Steve Buscemi's name to Steven Buscemeth? Now you can!
        /// </summary>
        /// <param name="fName">First Name</param>
        /// <param name="lName">Last Name</param>
        /// <param name="em">Email</param>
        /// <param name="yBorn">Year Born</param>
        /// <param name="mBorn">Month Born</param>
        /// <param name="dBorn">Day Born</param>
        /// <param name="output">Textblock containing information regarding contact</param>
        public void ReadFromInputForm(TextBox fName, TextBox lName, TextBox em, string yBorn, string mBorn, string dBorn, TextBlock output)
        {
            //Literally all input boxes added become the text of the output textblock
            firstName = fName.Text;
            lastName = lName.Text;
            email = em.Text;
            int.TryParse(yBorn, out yearBorn);
            int.TryParse(mBorn, out monthBorn);
            int.TryParse(dBorn, out dayBorn);

            DisplayInfo(output);
        }

        /// <summary>
        /// Save inputted text from textbox
        /// </summary>
        public void WriteToFile()
        {
            //The computer is trying it's best
            try
            {
                using (StreamWriter fileWriter = new StreamWriter("contact.txt"))
                {
                    //comma seperated list!
                    fileWriter.WriteLine(firstName + "," + lastName + "," + email + "," + yearBorn + "," + monthBorn + "," + dayBorn);
                    fileWriter.Flush();
                    fileWriter.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

}
