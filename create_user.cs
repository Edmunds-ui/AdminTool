using System;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Net;
using System.Security;
using System.Text;

namespace AdminTool
{
    public partial class create_user : Form
    {
        public create_user()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            password_txtbx.Text = GlobalMethods.GeneratePassword(12);
        }

        private bool ValidateInputs()
        {
            StringBuilder errorMsg = new StringBuilder(); // Izmanto StringBuilder, lai efektīvi veidotu kļūdu ziņojumus

            // Pārbauda, vai ir ievadīts vārds vai uzvārds
            if (string.IsNullOrWhiteSpace(Name_txtbx.Text) && string.IsNullOrWhiteSpace(lastname_txtbx.Text))
            {
                errorMsg.AppendLine("Lūdzu, aizpildiet vismaz Vārdu vai Uzvārdu."); // Pievieno kļūdu ziņojumu
            }

            // Pārbauda, vai parole ir uzģenerēta
            if (string.IsNullOrWhiteSpace(password_txtbx.Text))
            {
                errorMsg.AppendLine("Lūdzu, uzģenerējiet paroli."); // Pievieno kļūdu ziņojumu
            }

            // Pārbauda, vai ir izvēlēts departaments
            if (depart_cmbbx.SelectedIndex == -1 || string.IsNullOrEmpty(depart_cmbbx.SelectedItem?.ToString()))
            {
                errorMsg.AppendLine("Lūdzu, atlasiet Departamentu no saraksta."); // Pievieno kļūdu ziņojumu
            }

            // Ja ir kādas kļūdas, parāda tās lietotājam
            if (errorMsg.Length > 0)
            {
                MessageBox.Show(errorMsg.ToString(), "Validācijas kļūda", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false; // Validācija neizdevās
            }

            return true; // Visi pārbaudes ir izturēti
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Get domain name dynamically
            string domainName = Environment.UserDomainName;

            // Set the organizational unit (OU) for the user
            string container = "OU=" + depart_cmbbx.Text + ",OU=Lietotāji,OU=Riga,DC=bakalaura,DC=darbs"; // target organizational unit

            var result = GlobalMethods.GenerateUserNames(Name_txtbx.Text, lastname_txtbx.Text);

            // Update user Info values
            string Name = result.genAccount; //Lietotājs "Vārds Uzvārds"
            string userDisplayName = result.genDisplayName; //DisplayName "Vārds Uzvārds"
            string userName = result.genSAMAcc; //SAMAccountName "vards.uzvards"
            string userPassword = password_txtbx.Text; //Parole
            string userEmail = result.genEmailText; //e-pasta adrese
            DateTime expirationDate = userExpirationDateTimePicker1.Value; //Expiration date
            string userDescription = Description_txtbx.Text; //Description

            // Pārbaude vai nav tāda lietotāja
            if (!GlobalMethods.IsSamAccountNameUnique(userName))
            {
                MessageBox.Show($"Lietotājs '{userName}' jau eksistē.", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Izsauc validācijas metodi
            if (!ValidateInputs())
            {
                return; // Izejiet, ja validācija neizturēta
            }

            try
            {
                // Create a PrincipalContext for the domain
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain, domainName, container))
                {
                    // Create the user object in Active Directory
                    using (UserPrincipal user = new UserPrincipal(context))
                    {
                        // Set user properties
                        if (!string.IsNullOrWhiteSpace(Name_txtbx.Text))
                            user.GivenName = Name_txtbx.Text; // First name
                        if (!string.IsNullOrWhiteSpace(lastname_txtbx.Text))
                            user.Surname = lastname_txtbx.Text; // Last name
                        user.Name = Name; // Full Name
                        user.SamAccountName = userName; // SAM account name
                        user.UserPrincipalName = userName + "@bakalaura.darbs"; // UPN
                        user.DisplayName = userDisplayName; // Display name
                        user.EmailAddress = userEmail; // Email address
                        user.SetPassword(userPassword); // Password
                        user.Enabled = true; // Enable the account
                        if (!string.IsNullOrWhiteSpace(Description_txtbx.Text))
                            user.Description = Description_txtbx.Text; // Description

                        // Set the expiration date
                        user.AccountExpirationDate = expirationDate;

                        // Force the user to change the password at next login
                        user.ExpirePasswordNow();  // Expire the password immediately

                        // Save the user to Active Directory
                        user.Save();
                    }

                    MessageBox.Show($"Lietotājs '{userName}' izveidots. Termiņš līdz {expirationDate}.", "Success");

                    // Īerakstīt žurnālā
                    DateTime eventTime = DateTime.Now;
                    string eventType = "Jauna Lietotāja izveide";
                    string eventMessage = $"Lietotājs {GlobalMethods.ReplaceLatvianSymbols(userName)} izveidots. Departaments: {depart_cmbbx.Text}. Termiņš līdz: {expirationDate}. Admins: {Environment.UserName}";
                    GlobalMethods.InsertLog(eventTime, eventType, eventMessage);

                    // Pievienot e-pastu uz exchange servera
                    EnableMailboxRemote(userName, "Mailbox Database 0344064873"); // e-pasta datubaze

                    // Kopēt buferī
                    CopyToBuffer($"Lietotāja vārds: {GlobalMethods.ReplaceLatvianSymbols(userName)}\nParole: {userPassword}\ne-pasts: {userEmail}\nDepartaments: {depart_cmbbx.Text}\nTermiņš: {expirationDate}\nPiezīmes: {Description_txtbx.Text}");
                    CleanFields();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kļūda veidojot lietotāju: {ex.Message}", "Kļūda");

                // Īerakstīt žurnālā
                DateTime eventTime = DateTime.Now;
                string eventType = "Kļūda";
                string eventMessage = $"User {userName} neizdevas izveidot {depart_cmbbx.Text} by: {Environment.UserName}";
                GlobalMethods.InsertLog(eventTime, eventType, eventMessage);
            }
        }

        public void EnableMailboxRemote(string userName, string mailboxDatabase)
        {
            // Remote PowerShell connection details
            string exchangeServerUri = "http://exchange/PowerShell/";  // Replace with the actual Exchange server FQDN
            string username = "admin22"; // Replace with the admin username
            string password = "Darbam123"; // Replace with the admin password

            // Convert the password to a SecureString
            SecureString securePassword = new SecureString();
            foreach (char c in password)
            {
                securePassword.AppendChar(c);
            }

            // Create the PSCredential object
            PSCredential credentials = new PSCredential(username, securePassword);

            // Create a remote session to Exchange server
            WSManConnectionInfo connectionInfo = new WSManConnectionInfo(new Uri(exchangeServerUri), "http://schemas.microsoft.com/powershell/Microsoft.Exchange", credentials);

            // Create and configure a Runspace to use the remote session
            Runspace runspace = RunspaceFactory.CreateRunspace(connectionInfo);
            runspace.Open();

            // Create the PowerShell instance
            PowerShell ps = PowerShell.Create();
            ps.Runspace = runspace;

            // Add the PowerShell command to the pipeline
            ps.AddCommand("Enable-Mailbox")
                .AddParameter("Identity", userName)
                .AddParameter("Database", mailboxDatabase);

            try
            {
                // Execute the command
                ps.Invoke();

                // Handle the results if needed
                if (ps.Streams.Error.Count > 0)
                {
                    foreach (ErrorRecord error in ps.Streams.Error)
                    {
                        Console.WriteLine($"Error: {error.Exception.Message}");
                    }
                }
                else
                {
                    Console.WriteLine("Mailbox enabled successfully.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing PowerShell script: {ex.Message}");
            }
            finally
            {
                // Cleanup
                ps.Dispose();
                runspace.Close();
                runspace.Dispose();
            }
        }

        private void create_user_Load(object sender, EventArgs e)
        {
            // Populate ComboBox with Organizational Units (OUs) under a specific parent OU
            string parentOU = "OU=Lietotāji,OU=Riga,DC=bakalaura,DC=darbs";  // Example LDAP path
            GlobalMethods.DisplayOUsInComboBox(parentOU, depart_cmbbx);  // 'depart_cmbbx' is the ComboBox
        }

        private void Name_txtbx_TextChanged(object sender, EventArgs e)
        {
            var result = GlobalMethods.GenerateUserNames(Name_txtbx.Text, lastname_txtbx.Text);
            // Atjaunina e-pasta un displeja vārda lauciņus ar uzģenerētam vērtībām
            email_txtbx.Text = result.genEmailText;     // Jauna e-pasta adrese
            displayname_txtbx.Text = result.genDisplayName; // Jauns Displeja Vārds
        }

        private void lastname_txtbx_TextChanged(object sender, EventArgs e)
        {
            var result = GlobalMethods.GenerateUserNames(Name_txtbx.Text, lastname_txtbx.Text);
            // Atjaunina tekstlodziņus ar ģenerētajām vērtībām
            email_txtbx.Text = result.genEmailText;     // Jauna e-pasta adrese
            displayname_txtbx.Text = result.genDisplayName; // Jauns Displeja vārds
        }

        private void displayname_txtbx_TextChanged(object sender, EventArgs e)
        {
            displayname_txtbx.Text = GlobalMethods.ConvertDisplayName(displayname_txtbx.Text);
        }

        private void CopyToBuffer(string message)
        {
            // Pārbauda, vai ziņojums nav tukšs
            if (!string.IsNullOrWhiteSpace(message))
            {
                try
                {
                    // Kopē tekstu buferī
                    Clipboard.SetText(message);
                }
                catch (Exception ex)
                {
                    // Ja rodas kļūda, parāda ziņojumu ar kļūdu
                    MessageBox.Show($"Kļūda kopējot tekstu: {ex.Message}", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                // Ja ziņojums ir tukšs vai satur tikai atstarpes, parāda paziņojumu
                MessageBox.Show("Ziņojums ir tukšs", "Brīdinājums", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void CleanFields()
        {
            // Iztīra visus ievades laukus
            Name_txtbx.Clear();
            lastname_txtbx.Clear();
            password_txtbx.Clear();
            Description_txtbx.Clear();

            // Atjauno termiņa izvēli uz pašreizējo datumu
            userExpirationDateTimePicker1.Value = DateTime.Now;

            // Atjauno departamenta izvēli (izvēlas pirmo elementu)
            depart_cmbbx.SelectedIndex = -1;
        }

        private void clearbtn_Click(object sender, EventArgs e)
        {
            CleanFields();
        }
    }
}
