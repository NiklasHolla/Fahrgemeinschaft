using DataBaseWrapper;
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mail;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Net;
using MailMessage = System.Net.Mail.MailMessage;
using System.IO;

namespace Fahrgemeinschaft
{
    public partial class login : System.Web.UI.Page
    {
        public int verifyCode;
        public string email;
        string connStrg = "Driver={MySQL ODBC 8.0 Unicode Driver};Server=students-db.htlvb.at;Port=3306;Database=2223_5ahwii_hollerwoeger;Uid=2223_5ahwii_hollerwoeger;Pwd=14.02.2004;";
        protected void Page_Load(object sender, EventArgs e)
        {

        }

     
        


        /*
        passwordSalt=getSalt(passwordSalt);
        string passwordHashAndSalt = getHashSha256(passwordSalt);
        if(passwordAndSalt==passwordHashAndSalt)
        {
            FormsAuthentication.RedirectFromLoginPage(txtEmailLogin.Text, false);

        }
        else
        {
            lblInfo_Message.Text = "invalid user or password";
        }
        */
        /*
        sqlCmd = $"SELECT fahrgemeinschaft_user.PasswortSalt FROM fahrgemeinschaft_user WHERE fahrgemeinschaft_user.EMail LIKE '{txtEmailLogin.Text}';";
        string salt = (string)db.RunQueryScalar(sqlCmd);



        if (getHashSha256(txtPasswordLogin.Text + salt) == passwordAndSalt)
        {
            FormsAuthentication.RedirectFromLoginPage(txtEmailLogin.Text, false);
        }
        else
        {
            lblInfo_Message.Text = "invalid user or password";
        }
        */
    

        private List<object> createAccountData(string email, string password, string telefonnummerRoh)
        {
            string telefonnummer;
            string rolle = "";
            List<object> returnArray = new List<object>();

            try
            {
                email = txtEMailCreate.Text;
                 
                telefonnummerRoh = txtPhonenumberCreate.Text;
                if (telefonnummerRoh[0].Equals('+'))
                {
                    //telefonnummerRoh = telefonnummerRoh.Substring(0, 1);
                    telefonnummerRoh = telefonnummerRoh.Remove(0, 1);
                }
                else
                {
                    telefonnummer = telefonnummerRoh;
                }
                telefonnummer = telefonnummerRoh;



                if (RadioButtonList1.Items[0].Selected == true)
                {
                    rolle = RadioButtonList1.Items[0].Text;
                }
                else
                {
                    if (RadioButtonList1.Items[1].Selected == true)
                    {
                        rolle = RadioButtonList1.Items[1].Text;
                    }
                }

                if (String.IsNullOrEmpty(rolle))
                {
                    lblInfo.Text = "Please select a role";
                }

                returnArray.Add(email);
                returnArray.Add(password);
                returnArray.Add(telefonnummer);
                returnArray.Add(rolle);

            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }


            return returnArray;


        }

        

        public string getSalt()
        {
            var random = new RNGCryptoServiceProvider();

            // Maximum length of salt
            int max_length = 32;

            // Empty salt array
            byte[] salt = new byte[max_length];

            // Build the random bytes
            random.GetNonZeroBytes(salt);

            // Return the string encoded salt
            return Convert.ToBase64String(salt);
        }

        public static string getHashSha256(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            string hashString = string.Empty;
            foreach (byte x in hash)
            {
                hashString += String.Format("{0:x2}", x);
            }
            return hashString;
        }

        protected void btnCreateAccount_Click(object sender, EventArgs e)
        {
            string email;
            string password;
            string telefonnummer;
            string rolle;

            List<object> returnArray = new List<object>();



            if (Page.IsValid)
            {


                try
                {

                    if (CheckExistingEMail(txtEMailCreate.Text) == true)
                    {
                        returnArray = createAccountData(txtEMailCreate.Text, txtPasswordCreate.Text, txtPhonenumberCreate.Text);

                        email = returnArray[0].ToString();
                        string salt = getSalt();
                        
                        password = getHashSha256(txtPasswordCreate.Text + salt);
                        //password = returnArray[1].ToString();
                        //saltpassword = returnArray[2].ToString();
                        telefonnummer = returnArray[2].ToString();
                        rolle = returnArray[3].ToString();

                        //string sqlCmd = "INSERT INTO fahrgemeinschaft_user (EMail, PasswortAndSalt, PasswortSalt, Telefonnummer, Rolle)" +
                                   // $"VALUES('{email}', '{password}','{salt} ',{telefonnummer}, '{rolle}')";

                        Random random = new Random();
                        verifyCode = random.Next(2000, 2500009);



                        string sqlCmd1 = "INSERT INTO fahrgemeinschaft_user (EMail, PasswortAndSalt, PasswortSalt, Telefonnummer, Rolle, VerifyCode, Autentifiziert)" +
                                     $"VALUES('{email}', '{password}','{salt} ',{telefonnummer}, '{rolle}', '{verifyCode}', 0)";


                        lblInfo.Text = "You were sucesfully registrated, please checck your email and insert the sent Verify Code to authenticate your account";

                        ConfirmEmail(txtEMailCreate.Text, verifyCode.ToString());




                        OdbcConnection conn = new OdbcConnection(connStrg);
                        OdbcCommand cmd = new OdbcCommand(sqlCmd1, conn);



                        conn.Open();



                        // Aufpassen erst einlogin möglich wenn Autentifizierung auf 1 ist
                        cmd.ExecuteNonQuery();
                    }





                }
                catch (Exception ex)
                {
                    lblInfo.Text = "We found something!" + ex.Message;
                    return;
                }



            }
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            

            OdbcConnection conn = new OdbcConnection(connStrg);
            DataBase db = new DataBase(connStrg);
            


            string sqlCmd;


            sqlCmd = $"SELECT fahrgemeinschaft_user.Autentifiziert FROM fahrgemeinschaft_user WHERE fahrgemeinschaft_user.EMail LIKE '{txtEmailLogin.Text}';";
            
            
            
            object result1 = (int)db.RunQueryScalar(sqlCmd);
            if ((int)result1 != 1)
            {
<<<<<<< Updated upstream
                lblInfo.Text = "Noch nicht authentifiziert";
=======
                lblInfo_Message.Text = "EMail nicht vorhanden oder nicht verifiziert!";
>>>>>>> Stashed changes
            }
            else
            {

                sqlCmd = $"SELECT * FROM fahrgemeinschaft_user WHERE fahrgemeinschaft_user.EMail LIKE '{txtEmailLogin.Text}'";

                object result = db.RunQueryScalar(sqlCmd);
                if (result == null)
                {
                    lblInfo_Message.Text = "invalid user or password";
                }
                sqlCmd = $"SELECT fahrgemeinschaft_user.PasswortAndSalt FROM fahrgemeinschaft_user WHERE fahrgemeinschaft_user.EMail LIKE '{txtEmailLogin.Text}';";
                string fertigesPW = (string)db.RunQueryScalar(sqlCmd);

                sqlCmd = $"SELECT fahrgemeinschaft_user.PasswortSalt FROM fahrgemeinschaft_user WHERE fahrgemeinschaft_user.EMail LIKE '{txtEmailLogin.Text}';";
                string saltpw = (string)db.RunQueryScalar(sqlCmd);

                string checkpw = getHashSha256(txtPasswordLogin.Text + saltpw.Trim());




                if (checkpw == fertigesPW)
                {
                    FormsAuthentication.RedirectFromLoginPage(txtEmailLogin.Text, false);

                }
                else
                {
                    lblInfo_Message.Text = "invalid user or password";
                }
            }
        }

        private bool CheckExistingEMail(string Email)
        {
            OdbcConnection conn = new OdbcConnection(connStrg);
            string sqlCmd = $"SELECT * FROM fahrgemeinschaft_user WHERE EMail LIKE '{Email}' ";
            OdbcCommand cmd = new OdbcCommand(sqlCmd, conn);

            bool isApproved = false;

            try
            {
                conn.Open();

                if (cmd.ExecuteNonQuery() == 0)
                {
                    isApproved = true;
                }
                else
                {
                    lblInfo.Text = $"Eingegebene EMail: {Email} ist bereits vorhanden";
                }
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
            finally
            {
                conn.Close();
            }

            return isApproved;
        }

        protected void cv_Password_ServerValidate(object source, ServerValidateEventArgs args)
        {
            string psw = txtPasswordCreate.Text;
            int countDigit = 0;
            int countLetter = 0;
            int countsymbol = 0;
            string Alphabet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";


            

            if (!String.IsNullOrEmpty(psw) || !String.IsNullOrWhiteSpace(psw))
            {
                args.IsValid = true;
            }
            else
            {
                lblInfo.Text = "Passwort ist leer";
            }



            if (psw.Length >= 8)
            {
                args.IsValid = true;
            }
            else
            {
                lblInfo.Text = "Passwortlänge ist zu klein";
            }



            foreach (char c in psw)
            {
                if (!char.IsLetterOrDigit(c) == true)
                {
                    countsymbol++;
                }
               
            }

                foreach (char ch in psw)
                {
                    if (Char.IsDigit(ch)) countDigit++;
                    if (Char.IsLetter(ch)) countLetter++;
                }


                if (countDigit < 1 || countLetter < 5 || countsymbol < 1)
                {
                    args.IsValid = false;
                    lblInfo.Text = "Passwort muss mind. 1 Zahl, 5 Buchstaben und ein Sonerzeichen beinhalten!";
                }
                /*
            if (String.IsNullOrWhiteSpace(txtVerifyCode.Text) || String.IsNullOrWhiteSpace(txtPasswordLogin.Text) || String.IsNullOrWhiteSpace(txtEmailLogin.Text))
            {
                args.IsValid = true;
            }
                */
            
            
        }

        [Obsolete]
        private void ConfirmEmail(string empfaenger, string nachricht)

        {
            StreamReader sr = new StreamReader(@"Z:\SWP\5\GitHub Projekt\codencoden.txt");
            string passwort = sr.ReadLine();
            string server = "smtp.office365.com";
            int port = 587;
            string user = "niklas.hollerwoeger@htlvb.at";
            var basciCredintial = new NetworkCredential(user, passwort);
            string absender = "niklas.hollerwoeger@htlvb.at";
            MailMessage message = new MailMessage(absender, empfaenger);








            message.Subject = "Your Verification Code";
            message.Body = nachricht;



            SmtpClient client = new SmtpClient(server);






            client.UseDefaultCredentials = false;
            client.Credentials = basciCredintial;
            client.EnableSsl = true;



            try
            {
                client.Send(message);
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }

        protected void btnVerify_Click(object sender, EventArgs e)
        {
            
            int VerifyCodeInt = Convert.ToInt32(txtVerifyCode.Text);
            string email = txtEMailCreate.Text;
            string sqlCmdTwo = $"SELECT EMail, VerifyCode FROM fahrgemeinschaft_user WHERE VerifyCode = {VerifyCodeInt} AND Email LIKE '{email}'; ";
            DataBase db = new DataBase(connStrg);


            string sqlCmdThree = $"UPDATE fahrgemeinschaft_user SET Autentifiziert = 1 WHERE EMail LIKE '{email}' AND VerifyCode = {VerifyCodeInt}; ";
            OdbcConnection conn = new OdbcConnection(connStrg);
            OdbcCommand cmd = new OdbcCommand(sqlCmdTwo, conn);



            OdbcCommand cmdThree = new OdbcCommand(sqlCmdThree, conn);
            conn.Open();
            //cmd.ExecuteScalar().ToString().Length > 0
            
            if (Convert.ToInt32(db.RunNonQuery(sqlCmdTwo).ToString()) != -250)
            {
                //cmdThree.ExecuteNonQuery();
                db.RunNonQuery(sqlCmdThree);
                lblInfo.Text = " Congrats!";
            }
            else
            {
                lblInfo.Text = "Flascher VerifyCode!";
            }



            conn.Close();
        }
    }

    
}

