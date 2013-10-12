using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment1ATM
{
    class Program
    {
        static void Main(string[] args)
        {
            bool runATM = true;



            while (runATM)
            {
                int userSelection = 0;
                Console.Out.WriteLine("**PALMER CREDIT UNION ATM**");
                Console.Out.WriteLine("---------------------------");
                Console.Out.WriteLine("Select From the Following Options:");
                Console.Out.WriteLine("1: Open A New Account");
                Console.Out.WriteLine("2: Login Using An Existing Account");
                Console.Out.WriteLine("3: Login As An Adminstrator  ");
                userSelection = int.Parse(Console.In.ReadLine());

                if (userSelection == 1)
                {
                    Console.Clear();
                    GetNewAccountInfo();
                }
                else if (userSelection == 2)
                {
                    Console.Clear();
                    GetUserLoginInfo();
                }
                else if (userSelection == 3)
                {
                    GetAdminLoginInfo();
                }
                else if (userSelection == 4)
                {
                    runATM = false;
                }
            }
        }

        private static void GetNewAccountInfo()
        {

            People newPerson = new People();

            Console.Out.WriteLine("**PALMER CREDIT UNION ATM**");
            Console.Out.WriteLine("---------------------------");
            Console.Out.WriteLine(" **USER ACCOUNT CREATION** ");
            Console.Out.WriteLine("Enter Your First Name:");
            newPerson.FirstName = Console.ReadLine();
            while (newPerson.FirstName.Length > 50)
            {
                Console.Out.WriteLine("First Name Must Be Less Than 50 Characters.");
                Console.Out.WriteLine("Please Re-Enter Your First Name:");
                newPerson.FirstName = Console.ReadLine();
            }
            Console.Out.WriteLine("Enter Your Last Name:");
            newPerson.LastName = Console.ReadLine();
            while (newPerson.LastName.Length > 50)
            {
                Console.Out.WriteLine("Last Name Must Be Less Than 50 Characters.");
                Console.Out.WriteLine("Please Re-Enter Your Last Name:");
                newPerson.LastName = Console.ReadLine();
            }
            Console.Out.WriteLine("Enter Your Email Address:");
            newPerson.EmailAddress = Console.ReadLine();
            while (!newPerson.EmailAddress.Contains('@') || !newPerson.EmailAddress.EndsWith(".com") || newPerson.EmailAddress.Length > 255)
            {
                Console.Out.WriteLine("Invalid Email Address.");
                Console.Out.WriteLine("Please Re-Enter Your Email Address:");
                newPerson.EmailAddress = Console.ReadLine();
            }
            Console.Out.WriteLine("Enter Your Desired User Password:");
            newPerson.Password = Console.ReadLine();
            while ( newPerson.Password.Length > 10)
            {
                Console.Out.WriteLine("Password Cannot Be Longer Than 10 Characters");
                Console.Out.WriteLine("Please Choose A New Password:");
                newPerson.Password = Console.ReadLine();
            }
            Console.Out.WriteLine("Enter Your Social Insurance Number:");
            newPerson.SocialInsuranceNumber = Console.ReadLine();
            while (newPerson.SocialInsuranceNumber.Length > 11)
            {
                Console.Out.WriteLine("Invalid Social Insurance Number.");
                Console.Out.WriteLine("Please Re-Enter Your Social Insurance Number:");
                newPerson.SocialInsuranceNumber = Console.ReadLine();
            }
            if (!newPerson.DoesAccountExist())
            {
                newPerson.CreateAccount();
                Console.Out.WriteLine("Account Creation Successful. Press Enter to Return to Main Menu...");
                Console.ReadLine();
                Console.Clear();
            }

        }

        private static void GetUserLoginInfo()
        {
            People newPerson = new People();

            Console.Out.WriteLine("**PALMER CREDIT UNION ATM**");
            Console.Out.WriteLine("---------------------------");
            Console.Out.WriteLine("  **USER ACCOUNT LOGIN** ");
            Console.Out.WriteLine("Enter Your Email Address:");
            newPerson.EmailAddress = Console.ReadLine();
            while (!newPerson.EmailAddress.Contains('@') || !newPerson.EmailAddress.EndsWith(".com") || newPerson.EmailAddress.Length > 255)
            {
                Console.Out.WriteLine("Invalid Email Address.");
                Console.Out.WriteLine("Please Re-Enter Your Email Address:");
                newPerson.EmailAddress = Console.ReadLine();
            }
            Console.Out.WriteLine("Enter Your Password:");
            newPerson.Password = Console.ReadLine();
            while (newPerson.Password.Length > 10)
            {
                Console.Out.WriteLine("Password Cannot Be Longer Than 10 Characters");
                Console.Out.WriteLine("Please Choose A New Password:");
                newPerson.Password = Console.ReadLine();
            }

            if (!newPerson.ValidateCredentials())
            {
                int newchoice = 0;
                Console.Out.WriteLine("Email Address or Password is Incorrect. Please 1 To Try Again Or 2 to Create Account...");
                string choice = Console.ReadLine();
                int.TryParse(choice, out newchoice);
                while ( newchoice != 1 && newchoice != 2)
                {
                    Console.Out.WriteLine("Press 1 to Try Again or 2 to Return to Main Menu...");
                    try
                    {
                        choice = Console.ReadLine();
                    }
                    catch (Exception ex)
                    {
                        Console.Out.WriteLine(ex.Message);
                    }

                    int.TryParse(choice, out newchoice);
                }
               
                if (newchoice == 1)
                {
                    
                    GetUserLoginInfo();
                }
                else
                {
                    
                    GetNewAccountInfo();
                }
            }
            else
            {
                Console.Clear();
                GetLoggedInOptions(newPerson);
            }
        }

        private static void GetLoggedInOptions(People newPerson)
        {
            Transactions userTransactions = new Transactions();
            TransactionType transactionType = new TransactionType();
            int userSelection = 0;
            int recordsToShow = 0;
            while (userSelection != 6)
            {
                Console.Out.WriteLine("**PALMER CREDIT UNION ATM**");
                Console.Out.WriteLine("---------------------------");
                Console.Out.WriteLine("    **ACCOUNT OPTIONS** ");
                Console.Out.WriteLine("1: Check Account Balance");
                Console.Out.WriteLine("2: Make a Deposit");
                Console.Out.WriteLine("3: Make a Withdrawal");
                Console.Out.WriteLine("4: View Transactions");
                Console.Out.WriteLine("5: Delete Account");
                Console.Out.WriteLine("6: Logout");
                userSelection = int.Parse(Console.In.ReadLine());

                switch (userSelection)
                {
                    case 1:
                        decimal accountBalance = userTransactions.CheckAccountBalance(newPerson.SelectPersonID());
                        Console.Out.WriteLine("Your Current Account Balance Is: " + accountBalance);
                        Console.Out.WriteLine("Press Any Key to Return to the Menu...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 2:
                        userTransactions.TransactionTypeId = 1;
                        Console.Out.WriteLine("Enter The Amount To Deposit:");
                        userTransactions.AmountTransferred = Decimal.Parse(Console.In.ReadLine());
                        userTransactions.PerformTransaction(newPerson.SelectPersonID());
                        Console.Out.WriteLine("Deposited " + userTransactions.AmountTransferred + " Into Account.");
                        Console.Out.WriteLine("Press Any Key to Return to the Menu...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 3:
                        userTransactions.TransactionTypeId = 2;
                        Console.Out.WriteLine("Enter The Amount To Withdraw:");
                        userTransactions.AmountTransferred = Decimal.Parse(Console.In.ReadLine());
                        userTransactions.PerformTransaction(newPerson.SelectPersonID());
                        Console.Out.WriteLine("Withdrew " + userTransactions.AmountTransferred + " from Account.");
                        Console.Out.WriteLine("Press Any Key to Return to the Menu...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 4:
                        Console.Out.WriteLine("Amount Of Transactions(5,10, or 15);");
                        recordsToShow = int.Parse(Console.In.ReadLine());
                        userTransactions.ReportTransactions(recordsToShow, newPerson.SelectPersonID());
                        Console.Out.WriteLine("Press Any Key to Return to the Menu...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 5:
                        Console.Out.WriteLine("Verify Your Email Address:");
                        newPerson.EmailAddress = Console.ReadLine();
                        while (!newPerson.EmailAddress.Contains('@') || !newPerson.EmailAddress.EndsWith(".com") || newPerson.EmailAddress.Length > 255)
                        {
                            Console.Out.WriteLine("Invalid Email Address.");
                            Console.Out.WriteLine("Please Re-Enter Your Email Address:");
                            newPerson.EmailAddress = Console.ReadLine();
                        }
                        Console.Out.WriteLine("Verify Your Password:");
                        newPerson.Password = Console.ReadLine();
                        while (newPerson.Password.Length > 10)
                        {
                            Console.Out.WriteLine("Password Cannot Be Longer Than 10 Characters");
                            Console.Out.WriteLine("Please Re-Enter Your Password:");
                            newPerson.Password = Console.ReadLine();
                        }

                        if (!newPerson.ValidateCredentials())
                        {
                            Console.Out.WriteLine("Email Address or Password is Incorrect. Please Try Again.");
                        }
                        else
                        {
                            Console.Out.WriteLine("Validation Correct. Proceeding With Account Deletion.");
                            Console.Out.WriteLine("Deleting All Transaction Records...");
                            userTransactions.DeleteTransactions(newPerson.SelectPersonID());
                            Console.Out.WriteLine("Transaction Records Deleted.");
                            Console.Out.WriteLine("Deleting All Account Info...");
                            newPerson.DeleteUserAccount(newPerson.SelectPersonID());
                            Console.Out.WriteLine("Account Deleted. Exiting To Main Menu.");
                            Console.ReadLine();
                            Console.Clear();
                            userSelection = 6;
                            goto case 6;
                        }
                        break;
                    case 6:
                        break;
                    default:
                        Console.Out.WriteLine("Please Choose From The Menu Options");
                        Console.Out.WriteLine("Press Any Key to Return to the Menu...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                }
            }


        }

        private static void GetAdminLoginInfo()
        {
            Administrators admin = new Administrators();
            Console.Clear();
           
            Console.Out.WriteLine("**PALMER CREDIT UNION ATM**");
            Console.Out.WriteLine("---------------------------");
            Console.Out.WriteLine("    **ADMIN LOGIN** ");
            Console.Out.WriteLine("Enter Your Admin Username:");
            admin.Username = Console.ReadLine();
            while (admin.Username.Length > 20)
            {
                Console.Out.WriteLine("Username Cannot Be Longer Than 20 Characters");
                Console.Out.WriteLine("Please Re-Enter Your Username:");
                admin.Username = Console.ReadLine();
            }
            Console.Out.WriteLine("Enter Your Admin Password:");
            admin.Password = Console.ReadLine();
            while (admin.Password.Length > 20)
            {
                Console.Out.WriteLine("Password Cannot Be Longer Than 20 Characters");
                Console.Out.WriteLine("Please Re-enter Your Password");
                admin.Password = Console.ReadLine();
            }
           
            if (admin.loginAsAdmin())
            {
                DisplayAdminOptions(admin);
            }
            else
            {
                int adminChoice = 0;
                Console.Out.WriteLine("Incorrect Login Credentials. Press 1 to Try Again or 2 to Return to Main Menu...");
                string adminString = Console.ReadLine();
                int.TryParse(adminString, out adminChoice);
                while ( adminChoice != 1 && adminChoice != 2)
                {
                    Console.Out.WriteLine("Press 1 to Try Again or 2 to Return to Main Menu...");
                    adminString = Console.ReadLine();
                    int.TryParse(adminString, out adminChoice);
                }
                if (adminChoice == 1)
                {
                    GetAdminLoginInfo();
                }
                else
                {
                    Console.Clear();
                }
            }

        }





        private static void DisplayAdminOptions(Administrators admin)
        {
            int adminInput = 0;
            bool isDatabaseBackedUp = false;
            Console.Clear();
            while (adminInput != 4)
            {
                Console.Out.WriteLine("**PALMER CREDIT UNION ATM**");
                Console.Out.WriteLine("---------------------------");
                Console.Out.WriteLine("    **ADMIN OPTIONS** ");
                Console.Out.WriteLine("1: BACKUP DATABASE");
                Console.Out.WriteLine("2: DELETE DATABASE");
                Console.Out.WriteLine("3: RESTORE DATABASE");
                Console.Out.WriteLine("4: LOGOUT");
                adminInput = int.Parse(Console.ReadLine());

                switch (adminInput)
                {
                    case 1:
                        Console.Out.WriteLine("Backing Up Database");
                        admin.backupDatabase();
                        isDatabaseBackedUp = true;
                        Console.Out.WriteLine("Database backed up. Press enter to return to menu...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 2:
                        if (!isDatabaseBackedUp)
                        {
                            Console.Out.WriteLine("Please backup the database prior to deletion....");
                            Console.ReadLine();
                            Console.Clear();
                            break;
                        }
                        else
                        {
                            Console.Out.WriteLine("Deleting Database...");
                            admin.deleteDatabase();
                            Console.Out.WriteLine("Database deleted. Press enter to return to menu...");
                            Console.ReadLine();
                            Console.Clear();
                        }
                        break;
                    case 3:
                        Console.Out.WriteLine("Restoring Database...");
                        admin.restoreDatabase();
                        Console.Out.WriteLine("Database restored. Press enter to return to menu...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    case 4:
                        Console.Out.WriteLine("Logging Out and Returning to Main Menu...");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                    default:
                        Console.Out.WriteLine("Please Enter a Valid Option");
                        Console.ReadLine();
                        Console.Clear();
                        break;
                }
            }
        }
    }
}
 






