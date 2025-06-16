using System;
using System.Data.SqlClient;
using System.Data;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Text;
using System.Management.Automation.Runspaces;
using System.Management.Automation;
using System.Security;
using System.Text.RegularExpressions;


namespace AdminTool
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            this.BackColor = Color.FromArgb(243, 244, 246);
            this.Font = new Font("Segoe UI", 10);

            CheckDomainConnection();

            if (!IsCurrentUserInHelpdeskGroup())
            {
                DisableControls();
                MessageBox.Show("Jūs neesat Help desk grupas dalībnieks.", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            InitializeTimer();
            string parentOU = "OU=Lietotāji,OU=Riga,DC=bakalaura,DC=darbs";  // Example LDAP path
            GlobalMethods.DisplayOUsInComboBox(parentOU, depart_cmbbx);  // 'depart_cmbbx' is the ComboBox
        }

        private void DisableControls()
        {
            //Palīgfunkcija, lai atspējotu pogas
            change_user.Enabled = false;
        }

        private bool IsCurrentUserInHelpdeskGroup()
        {
            //Funkcija, kas pārbauda, vai lietotājs ir Helpdesk grupā
            try
            {
                string username = Environment.UserName; //Paņem pašreizējo lietotājvārdu

                using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
                {
                    UserPrincipal user = UserPrincipal.FindByIdentity(context, username); //Meklē lietotāju AD
                    if (user == null) return false;

                    GroupPrincipal helpdeskGroup = GroupPrincipal.FindByIdentity(context, "Help desk"); //Meklē Help desk grupu AD
                    if (helpdeskGroup == null) return false;

                    return user.IsMemberOf(helpdeskGroup); //Pārbauda, vai lietotājs ir grupā
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Radās kļūda, pārbaudot piederību grupai: {ex.Message}", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void CheckDomainConnection()
        {
            // Funkcija, lai pārbaudītu savienojumu ar domēnu
            try
            {
                Domain domain = Domain.GetCurrentDomain(); //Iegūst pašreizējo domēnu
                statusLabel.Text = $"Connected to domain: {domain.Name}"; //Parāda savienojuma statusu
                change_user.Enabled = true;
                statusLabel.ForeColor = Color.Green; //Zaļš statuss
            }
            catch (ActiveDirectoryObjectNotFoundException)
            {
                statusLabel.Text = "Nav savienojuma ar domēnu."; // Ja nav savienojuma, parāda paziņojumu
                DisableControls(); //Atspējo pogas
                statusLabel.ForeColor = Color.Red; //Sarkans statuss
            }
            catch (Exception ex)
            {
                statusLabel.Text = $"Kļūda pārbaudot savienojumu ar domēnu: {ex.Message}"; //Ja notikusi kļūda
                DisableControls(); //Atspējo pogas
                statusLabel.ForeColor = Color.Blue; //Zils statuss kļūdas gadījumā
            }
        }

        private void InitializeTimer()
        {
            //inicializē taimeri domēna statusa pārbaudīšanai ik pēc noteikta laika
            timer = new Timer
            {
                Interval = 10 * 1000 // 10 sekundes
            };
            timer.Tick += Timer_Tick; // Pievieno notikumu, kas tiks izsaukts katrā taimerī
            timer.Start(); // Sāk taimeri
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            CheckDomainConnection(); // Pārbauda savienojumu ar domēnu
        }

        private void aizvertToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_SQLQueryLogs_Click(object sender, EventArgs e)
        {
            SQLQueryForLogs(SQLQueryFilter_valueTXT.Text);
        }

        private void SQLQueryForLogs(string filter_value)
        {
            string connectionString = @"Server=mssql;Database=bakalaura_darbs;Integrated Security=True;";

            string selectQuery = "SELECT * FROM dbo.logi WHERE EventMessage LIKE @filter ORDER BY LogID DESC";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    using (SqlCommand command = new SqlCommand(selectQuery, connection))
                    {
                        // Добавляем параметр для фильтрации
                        command.Parameters.AddWithValue("@filter", "%" + filter_value + "%");

                        SqlDataAdapter adapter = new SqlDataAdapter(command);
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show("SQL Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_ResetSQLQuery_Click(object sender, EventArgs e)
        {
            SQLQueryForLogs("");
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                // Populate ComboBox with Organizational Units (OUs) under a specific parent OU
                string parentOU = "OU=Lietotāji,OU=Riga,DC=bakalaura,DC=darbs";  // Example LDAP path
                GlobalMethods.DisplayOUsInComboBox(parentOU, depart_cmbbx);  // 'depart_cmbbx' is the ComboBox
            }
            if (tabControl1.SelectedIndex == 1)
            {
                // Aizpildīt ComboBox ar organizatoriskajām vienībām (OU) noteiktā 'parent OU'
                string parentOU = "OU=Lietotāji,OU=Riga,DC=bakalaura,DC=darbs";  // LDAP ceļš
                GlobalMethods.DisplayOUsInComboBox(parentOU, chUserDpt_cmbx); // Combobox'ā parāda norādītās OU
            }
            if (tabControl1.SelectedIndex == 2)
            {
                LoadGroupsFromOU();
            }
            if (tabControl1.SelectedIndex == 3)
            {
                IeladetGrupas();
            }
            if (tabControl1.SelectedIndex == 4)
            {
                SQLQueryForLogs(SQLQueryFilter_valueTXT.Text);
            }
        }

        //Bloks group_membership
        private void IeladetGrupas()
        {
            grps_lstbx.Items.Clear();
            string grupasOU = "OU=Grupas,OU=Riga,DC=bakalaura,DC=darbs";

            try
            {
                using (DirectoryEntry entry = new DirectoryEntry("LDAP://" + grupasOU))
                {
                    foreach (DirectoryEntry child in entry.Children)
                    {
                        if (child.SchemaClassName?.ToLower() == "group")
                        {
                            var nameObj = child.Properties["name"].Value;
                            if (nameObj != null)
                            {
                                grps_lstbx.Items.Add(nameObj.ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kļūda ielādējot grupas: {ex.Message}", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<string> IegutVisusLietotajus()
        {
            List<string> lietotaji = new List<string>();
            string lietotajiOU = "OU=Lietotāji,OU=Riga,DC=bakalaura,DC=darbs";

            try
            {
                using (DirectoryEntry entry = new DirectoryEntry("LDAP://" + lietotajiOU))
                using (DirectorySearcher searcher = new DirectorySearcher(entry))
                {
                    searcher.Filter = "(objectClass=user)";
                    searcher.SearchScope = SearchScope.Subtree;

                    foreach (SearchResult result in searcher.FindAll())
                    {
                        if (result.Properties.Contains("sAMAccountName") && result.Properties["sAMAccountName"].Count > 0)
                        {
                            lietotaji.Add(result.Properties["sAMAccountName"][0].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kļūda ielādējot lietotājus: {ex.Message}", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return lietotaji;
        }

        private List<string> IegutNekurienesLietotajus()
        {
            List<string> lietotaji = new List<string>();
            string nekurienesOU = "OU=Nekuriene,OU=Lietotāji,OU=Riga,DC=bakalaura,DC=darbs";

            try
            {
                using (DirectoryEntry entry = new DirectoryEntry("LDAP://" + nekurienesOU))
                using (DirectorySearcher searcher = new DirectorySearcher(entry))
                {
                    searcher.Filter = "(objectClass=user)";
                    searcher.SearchScope = SearchScope.OneLevel;

                    foreach (SearchResult result in searcher.FindAll())
                    {
                        if (result.Properties.Contains("sAMAccountName") && result.Properties["sAMAccountName"].Count > 0)
                        {
                            lietotaji.Add(result.Properties["sAMAccountName"][0].ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kļūda ielādējot Nekurienes lietotājus: {ex.Message}", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return lietotaji;
        }

        private void add_btn_Click_1(object sender, EventArgs e)
        {
            if (allusers_lstbox.SelectedItem == null || grps_lstbx.SelectedItem == null)
            {
                MessageBox.Show("Lūdzu, izvēlieties lietotāju un grupu.", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedUser = allusers_lstbox.SelectedItem.ToString();
            string selectedGroup = grps_lstbx.SelectedItem.ToString();

            try
            {
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
                {
                    GroupPrincipal group = GroupPrincipal.FindByIdentity(context, selectedGroup);
                    if (group == null)
                    {
                        MessageBox.Show($"Grupa {selectedGroup} nav atrasta.", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    UserPrincipal user = UserPrincipal.FindByIdentity(context, selectedUser);
                    if (user == null)
                    {
                        MessageBox.Show($"Lietotājs {selectedUser} nav atrasts.", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (!group.Members.Contains(user))
                    {
                        group.Members.Add(user);
                        group.Save();

                        GlobalMethods.InsertLog(DateTime.Now, "Add to Group", $"Lietotājs {selectedUser} tika pievienots grupai {selectedGroup}.");
                    }
                    else
                    {
                        MessageBox.Show("Lietotājs jau ir šajā grupā.");
                    }
                }

                grps_lstbx_SelectedIndexChanged(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kļūda pievienojot lietotāju grupai: {ex.Message}", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void rem_btn_Click_1(object sender, EventArgs e)
        {
            if (groupmembers_lstbx.SelectedItem == null || grps_lstbx.SelectedItem == null)
            {
                MessageBox.Show("Lūdzu, izvēlieties grupas biedru un grupu.", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string selectedUser = groupmembers_lstbx.SelectedItem.ToString();
            string selectedGroup = grps_lstbx.SelectedItem.ToString();

            try
            {
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
                {
                    GroupPrincipal group = GroupPrincipal.FindByIdentity(context, selectedGroup);
                    if (group == null)
                    {
                        MessageBox.Show($"Grupa {selectedGroup} nav atrasta.", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    UserPrincipal user = UserPrincipal.FindByIdentity(context, selectedUser);
                    if (user == null)
                    {
                        MessageBox.Show($"Lietotājs {selectedUser} nav atrasts.", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    if (group.Members.Contains(user))
                    {
                        group.Members.Remove(user);
                        group.Save();

                        GlobalMethods.InsertLog(DateTime.Now, "Remove from Group", $"Lietotājs {selectedUser} tika noņemts no grupas {selectedGroup}.");
                    }
                    else
                    {
                        MessageBox.Show("Lietotājs nav šajā grupā.");
                    }
                }

                grps_lstbx_SelectedIndexChanged(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kļūda noņemot lietotāju no grupas: {ex.Message}", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void grps_lstbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grps_lstbx.SelectedItem == null)
                return;

            string groupName = grps_lstbx.SelectedItem.ToString();

            groupmembers_lstbx.Items.Clear();
            allusers_lstbox.Items.Clear();

            List<string> grupasLietotaji = new List<string>();
            List<string> visiLietotaji = IegutVisusLietotajus();
            List<string> nekurienesLietotaji = IegutNekurienesLietotajus();

            try
            {
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
                {
                    GroupPrincipal group = GroupPrincipal.FindByIdentity(context, groupName);
                    if (group != null)
                    {
                        foreach (UserPrincipal user in group.GetMembers())
                        {
                            if (user?.SamAccountName != null)
                            {
                                groupmembers_lstbx.Items.Add(user.SamAccountName);
                                grupasLietotaji.Add(user.SamAccountName);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show($"Grupa {groupName} nav atrasta.", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kļūda ielādējot grupas biedrus: {ex.Message}", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Lietotāji, kuri NAV grupā un NAV "Nekuriene" OU
            var atlikusieLietotaji = visiLietotaji
                .Except(grupasLietotaji)
                .Except(nekurienesLietotaji);

            foreach (string lietotajs in atlikusieLietotaji)
            {
                allusers_lstbox.Items.Add(lietotajs);
            }
        }


        //Bloks group_membership


        //Bloks create groups
        // Проверка валидности имени группы
        private bool IsValidGroupName(string groupName)
        {
            // Запрещаем символы: \ / [ ] : | < > + = ; , ? * " '
            string invalidCharsPattern = @"[\\\/\[\]:|<>+=;,?*\""'']";
            return !string.IsNullOrWhiteSpace(groupName) && !Regex.IsMatch(groupName, invalidCharsPattern);
        }

        private void LoadGroupsFromOU()
        {
            string ouPath = "OU=Grupas,OU=Riga,DC=bakalaura,DC=darbs";
            string ldapPath = $"LDAP://{ouPath}";

            try
            {
                using (DirectoryEntry entry = new DirectoryEntry(ldapPath))
                using (DirectorySearcher searcher = new DirectorySearcher(entry))
                {
                    searcher.Filter = "(objectClass=group)";
                    searcher.PropertiesToLoad.Add("cn");

                    groups_listBox.Items.Clear();

                    foreach (SearchResult result in searcher.FindAll())
                    {
                        if (result.Properties["cn"]?.Count > 0)
                        {
                            string groupName = result.Properties["cn"][0].ToString();
                            groups_listBox.Items.Add(groupName);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kļūda ielādējot grupas: {ex.Message}", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void create_group_btn_Click(object sender, EventArgs e)
        {
            string groupName = newGroupName.Text.Trim();

            if (!IsValidGroupName(groupName))
            {
                MessageBox.Show("Lūdzu, ievadiet derīgu grupas nosaukumu bez nepieļaujamiem simboliem.");
                return;
            }

            try
            {
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain, null, "OU=Grupas,OU=Riga,DC=bakalaura,DC=darbs"))
                {
                    GroupPrincipal existingGroup = GroupPrincipal.FindByIdentity(context, groupName);
                    if (existingGroup != null)
                    {
                        MessageBox.Show("Grupa ar šādu nosaukumu jau eksistē.", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    GroupPrincipal newGroup = new GroupPrincipal(context)
                    {
                        SamAccountName = groupName,
                        Name = groupName,
                        Description = "Izveidots automātiski",
                        IsSecurityGroup = true,
                        GroupScope = GroupScope.Universal
                    };

                    newGroup.Save();
                }

                MessageBox.Show("Grupa veiksmīgi izveidota.", "Veiksmīgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadGroupsFromOU();

                groups_listBox.SelectedItem = groupName;

                try
                {
                    GlobalMethods.InsertLog(DateTime.Now, "Group Created", $"Group '{groupName}' created by {Environment.UserName}");
                }
                catch (Exception logEx)
                {
                    //File.AppendAllText("log_errors.txt", $"{DateTime.Now}: Failed to log group creation: {logEx.Message}\n");
                }

                newGroupName.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kļūda: {ex.Message}", "Izņēmums", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void delete_group_btn_Click(object sender, EventArgs e)
        {
            if (groups_listBox.SelectedItem == null)
            {
                MessageBox.Show("Lūdzu, izvēlieties grupu no saraksta.");
                return;
            }

            string groupName = groups_listBox.SelectedItem.ToString();
            string ouPath = "OU=Grupas,OU=Riga,DC=bakalaura,DC=darbs";
            string ldapPath = $"LDAP://CN={groupName},{ouPath}";

            var dialogResult = MessageBox.Show(
                $"Vai tiešām vēlaties izdzēst grupu '{groupName}'?",
                "Apstiprinājums",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (dialogResult == DialogResult.No)
                return;

            try
            {
                using (DirectoryEntry groupEntry = new DirectoryEntry(ldapPath))
                {
                    if (groupEntry.SchemaClassName?.ToLower() != "group")
                    {
                        MessageBox.Show("Izvēlētais objekts nav grupa.", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    groupEntry.DeleteTree();
                }

                MessageBox.Show("Grupa veiksmīgi izdzēsta.", "Veiksmīgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGroupsFromOU();

                try
                {
                    GlobalMethods.InsertLog(DateTime.Now, "Group Deleted", $"Group '{groupName}' deleted by {Environment.UserName}");
                }
                catch (Exception logEx)
                {
                    //File.AppendAllText("log_errors.txt", $"{DateTime.Now}: Failed to log group deletion: {logEx.Message}\n");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kļūda dzēšot grupu: {ex.Message}", "Izņēmums", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        //Bloks create groups


        //Bloks change user
        private static string ExtractLastOUFromDistinguishedName(string distinguishedName)
        {
            if (string.IsNullOrEmpty(distinguishedName)) return string.Empty;
            var ouParts = distinguishedName.Split(',').Where(part => part.StartsWith("OU=", StringComparison.OrdinalIgnoreCase));
            return ouParts.FirstOrDefault()?.Substring(3);
        }

        private class UserInfo
        {
            public string DisplayName { get; set; }
            public string Email { get; set; }
            public string Description { get; set; }
            public string SamAccountName { get; set; }
            public string GivenName { get; set; }
            public string Surname { get; set; }
            public string LastOU { get; set; }
            public DateTime? ExpirationDate { get; set; }
            public bool IsLocked { get; set; }
            public bool IsEnabled { get; set; }
        }

        // Atlasīt OU ComboBox'ā
        private void SelectOUInComboBox(string lastOu)
        {
            if (!string.IsNullOrEmpty(lastOu))
            {
                chUserDpt_cmbx.Text = lastOu;
            }
        }

        private void genPassword_btn_Click(object sender, EventArgs e)
        {
            password_txtbx.Text = GlobalMethods.GeneratePassword(12);
        }
        //Bloks change user


        //Bloks create user
        private void createUserbtn_Click(object sender, EventArgs e)
        {
            string domainName = Environment.UserDomainName;
            string container = $"OU={depart_cmbbx.Text},OU=Lietotāji,OU=Riga,DC=bakalaura,DC=darbs";

            var result = GlobalMethods.GenerateUserNames(Name_txtbx.Text, lastname_txtbx.Text);

            string Name = result.genAccount;
            string userDisplayName = result.genDisplayName;
            string userName = result.genSAMAcc;
            string userPassword = password_txtbx.Text;
            string userEmail = result.genEmailText;
            DateTime expirationDate = userExpirationDateTimePicker1.Value;
            string userDescription = Description_txtbx.Text;

            if (!GlobalMethods.IsSamAccountNameUnique(userName))
            {
                MessageBox.Show($"User '{userName}' already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!ValidateInputs())
                return;

            try
            {
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain, domainName, container))
                using (UserPrincipal user = new UserPrincipal(context))
                {
                    if (!string.IsNullOrWhiteSpace(Name_txtbx.Text))
                        user.GivenName = Name_txtbx.Text;
                    if (!string.IsNullOrWhiteSpace(lastname_txtbx.Text))
                        user.Surname = lastname_txtbx.Text;

                    user.Name = Name;
                    user.SamAccountName = userName;
                    user.UserPrincipalName = $"{userName}@bakalaura.darbs";
                    user.DisplayName = userDisplayName;
                    user.EmailAddress = userEmail;
                    user.SetPassword(userPassword);
                    user.Enabled = true;
                    if (!string.IsNullOrWhiteSpace(userDescription))
                        user.Description = userDescription;

                    user.AccountExpirationDate = expirationDate;
                    user.ExpirePasswordNow();

                    user.Save();
                }

                MessageBox.Show($"User '{userName}' created. Expiration date: {expirationDate}.", "Success");

                try
                {
                    GlobalMethods.InsertLog(
                        DateTime.Now,
                        "User Created",
                        $"User {GlobalMethods.ReplaceLatvianSymbols(userName)} created. Department: {depart_cmbbx.Text}. Expiration: {expirationDate}. Admin: {Environment.UserName}");
                }
                catch (Exception logEx)
                {
                    // Optionally handle logging errors here, e.g. write to a file
                }

                EnableMailboxRemote(userName, "Mailbox Database 0344064873");

                CopyToBuffer(
                    $"Username: {GlobalMethods.ReplaceLatvianSymbols(userName)}\n" +
                    $"Password: {userPassword}\n" +
                    $"Email: {userEmail}\n" +
                    $"Department: {depart_cmbbx.Text}\n" +
                    $"Expiration: {expirationDate}\n" +
                    $"Notes: {userDescription}");

                CleanFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error creating user: {ex.Message}", "Error");

                try
                {
                    GlobalMethods.InsertLog(
                        DateTime.Now,
                        "Error",
                        $"Failed to create user {userName} in {depart_cmbbx.Text} by {Environment.UserName}");
                }
                catch (Exception logEx)
                {
                    // Optionally handle logging errors here, e.g. write to a file
                }
            }
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

        private void clearbtn_Click(object sender, EventArgs e)
        {
            CleanFields();
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

        private void button1_Click(object sender, EventArgs e)
        {
            string username = chUserSrch_txt.Text.Trim();
            chUserDpt_cmbx.SelectedIndex = -1;
            chUserPswd_txt.Clear();

            string container = "OU=Riga,DC=bakalaura,DC=darbs";

            try
            {
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain, null, container))
                {
                    UserPrincipal queryUser;

                    if (string.IsNullOrEmpty(username))
                    {
                        // Ищем всех пользователей в OU Riga
                        queryUser = new UserPrincipal(context);
                    }
                    else
                    {
                        // Ищем пользователей по шаблону SamAccountName в OU Riga
                        queryUser = new UserPrincipal(context) { SamAccountName = $"*{username}*" };
                    }

                    using (PrincipalSearcher searcher = new PrincipalSearcher(queryUser))
                    {
                        List<UserInfo> users = new List<UserInfo>();

                        foreach (var result in searcher.FindAll())
                        {
                            UserPrincipal user = result as UserPrincipal;
                            if (user == null) continue;

                            UserInfo info = MapUserPrincipalToUserInfo(user);
                            if (info != null)
                            {
                                users.Add(info);
                            }
                        }

                        if (users.Count == 0)
                        {
                            MessageBox.Show("Nav atrasts neviens lietotājs.", "Meklēšanas rezultāti", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (users.Count == 1)
                        {
                            DisplayUserInfoInTextboxes(users[0]);
                        }
                        else
                        {
                            ShowUserSelectionForm(users);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Piekļūstot Active Directory, radās kļūda: {ex.Message}", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private UserInfo MapUserPrincipalToUserInfo(UserPrincipal user)
        {
            try
            {
                DirectoryEntry de = user.GetUnderlyingObject() as DirectoryEntry;
                if (de == null) return null;

                return new UserInfo
                {
                    DisplayName = user.DisplayName,
                    GivenName = user.GivenName,
                    Surname = user.Surname,
                    Email = user.EmailAddress,
                    Description = de.Properties["description"].Value?.ToString(),
                    SamAccountName = user.SamAccountName,
                    LastOU = ExtractLastOUFromDistinguishedName(de.Properties["distinguishedName"].Value?.ToString()),
                    ExpirationDate = user.AccountExpirationDate,
                    IsLocked = user.IsAccountLockedOut(),
                    IsEnabled = user.Enabled ?? false
                };
            }
            catch
            {
                return null;
            }
        }

        private UserInfo currentUser;

        private void DisplayUserInfoInTextboxes(UserInfo user)
        {
            currentUser = user;
            currentUserLastOU = user.LastOU; // Запоминаем текущий OU
            // Заполнение текстбоксов
            chUserDNm_txt.Text = user.DisplayName;
            chUserMail_txt.Text = user.Email;
            chUserDesc_txt.Text = user.Description;
            chUserSrch_txt.Text = chUseraSamAccountNm_txt.Text = user.SamAccountName;
            chUserNm_txt.Text = user.GivenName;
            chUserSn_txt.Text = user.Surname;

            // Отметить OU в ComboBox
            SelectOUInComboBox(user.LastOU);

            // Подготовка статуса
            var statusLines = new List<string>();

            // Установка даты истечения
            if (user.ExpirationDate.HasValue)
            {
                chUserExp_datetime.Value = user.ExpirationDate.Value;

                if (user.ExpirationDate.Value < DateTime.Now)
                    statusLines.Add("- Account is expired");
            }
            else
            {
                chUserExp_datetime.Value = DateTime.Today;
            }

            // Сброс чекбоксов
            chBx_enableUsr.Enabled = false;
            chBx_unlockUsr.Enabled = false;

            // Проверка OU = "Nekuriene"
            bool isEditable = !string.Equals(chUserDpt_cmbx.Text, "Nekuriene", StringComparison.OrdinalIgnoreCase);
            SetUserFieldsEditable(isEditable);

            if (!isEditable)
            {
                statusLines.Add("- Department: Nekuriene (modifications disabled)");
            }
            else
            {
                if (!user.IsEnabled)
                {
                    statusLines.Add("- Account is disabled");
                    chBx_enableUsr.Enabled = true;
                }

                if (user.IsLocked)
                {
                    statusLines.Add("- Account is locked out");
                    chBx_unlockUsr.Enabled = true;
                }
            }

            // Если нет других статусов, и SamAccountName непустой — считаем, что всё ок
            if (statusLines.Count == 0 && !string.IsNullOrWhiteSpace(user?.SamAccountName))
            {
                statusLines.Add("- Account is active");
            }
            else if (string.IsNullOrWhiteSpace(user?.SamAccountName))
            {
                statusLines.Add("- No user selected");
            }


            // Отображение
            chUserStatus_txt.Text = "User Status:" + Environment.NewLine + string.Join(Environment.NewLine, statusLines);
            chUserDpt_cmbx.SelectedItem = user.LastOU; // или chUserDpt_cmbx.Text = user.LastOU;
            chUserDpt_cmbx_SelectedIndexChanged(null, null);
        }

        private void SetUserFieldsEditable(bool editable)
        {
            chUserNm_txt.Enabled = editable;
            chUserSn_txt.Enabled = editable;
            chUserMail_txt.Enabled = editable;
            chUserDNm_txt.Enabled = editable;
            chUserPswd_txt.Enabled = editable;
            chUserExp_datetime.Enabled = editable;
            chUserDesc_txt.Enabled = editable;
            chUseraSamAccountNm_txt.Enabled = editable;
            chBx_enableUsr.Enabled = editable;
            chBx_unlockUsr.Enabled = editable;
        }

        private void ShowUserSelectionForm(List<UserInfo> users)
        {
            Form selectionForm = new Form
            {
                Text = "Izvēlies lietotāju",
                Width = 680,
                Height = 450,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                MinimizeBox = false
            };

            // TextBox для поиска
            TextBox searchBox = new TextBox
            {
                Dock = DockStyle.Top,
                //PlaceholderText = "Meklēt pēc vārda, e-pasta, apraksta vai lietotājvārda...",
                Height = 30
            };

            ListView listView = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                GridLines = true,
                MultiSelect = false
            };

            listView.Columns.Add("Name", 150);
            listView.Columns.Add("Email", 150);
            listView.Columns.Add("Description", 150);
            listView.Columns.Add("SamAccountName", 150);

            // Локальная копия списка для фильтрации
            List<UserInfo> allUsers = new List<UserInfo>(users);

            void PopulateListView(IEnumerable<UserInfo> filteredUsers)
            {
                listView.Items.Clear();
                foreach (var user in filteredUsers)
                {
                    var item = new ListViewItem(user.DisplayName ?? "(nav vārda)");
                    item.SubItems.Add(user.Email ?? "");
                    item.SubItems.Add(user.Description ?? "");
                    item.SubItems.Add(user.SamAccountName ?? "");
                    item.Tag = user;

                    listView.Items.Add(item);
                }
            }

            // Сначала заполним весь список
            PopulateListView(allUsers);

            // Обработка изменения текста в поисковом поле
            searchBox.TextChanged += (s, e) =>
            {
                string filter = searchBox.Text.ToLower();

                var filtered = allUsers.Where(u =>
                    (u.DisplayName?.ToLower().Contains(filter) ?? false) ||
                    (u.Email?.ToLower().Contains(filter) ?? false) ||
                    (u.Description?.ToLower().Contains(filter) ?? false) ||
                    (u.SamAccountName?.ToLower().Contains(filter) ?? false));

                PopulateListView(filtered);
            };

            listView.DoubleClick += (s, e) =>
            {
                SelectUser();
            };

            Button selectButton = new Button
            {
                Text = "Izvēlēties",
                Dock = DockStyle.Bottom,
                Height = 35
            };
            selectButton.Click += (s, e) => SelectUser();

            void SelectUser()
            {
                if (listView.SelectedItems.Count > 0)
                {
                    var selectedUser = listView.SelectedItems[0].Tag as UserInfo;
                    if (selectedUser != null)
                    {
                        selectionForm.DialogResult = DialogResult.OK;
                        DisplayUserInfoInTextboxes(selectedUser);
                        selectionForm.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Lūdzu, izvēlies lietotāju.", "Nav izvēles", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }

            selectionForm.Controls.Add(listView);
            selectionForm.Controls.Add(selectButton);
            selectionForm.Controls.Add(searchBox);

            selectionForm.ShowDialog();
        }


        private string currentUserLastOU;

        private void chUserDpt_cmbx_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool selectedIsNekuriene = string.Equals(chUserDpt_cmbx.Text, "Nekuriene", StringComparison.OrdinalIgnoreCase);
            bool userAlreadyInNekuriene = string.Equals(currentUserLastOU, "Nekuriene", StringComparison.OrdinalIgnoreCase);

            // Если выбрали Nekuriene, но пользователь ещё НЕ в Nekuriene — показать предупреждение
            if (selectedIsNekuriene && !userAlreadyInNekuriene)
            {
                var result = MessageBox.Show(
                    "Lietotājs tiks pārvietots uz Nekuriene. Tas nozīmē, ka viņa konts tiks bloķēts, parole mainīta un derīguma termiņš iestatīts uz vakar.",
                    "Uzmanību!", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                if (result != DialogResult.OK)
                {
                    // Отмена: возвращаем выбор обратно на предыдущий OU, чтобы не менять на Nekuriene
                    chUserDpt_cmbx.SelectedIndexChanged -= chUserDpt_cmbx_SelectedIndexChanged; // Отключаем обработчик временно
                    chUserDpt_cmbx.Text = currentUserLastOU;
                    chUserDpt_cmbx.SelectedIndexChanged += chUserDpt_cmbx_SelectedIndexChanged; // Включаем обратно
                    return;
                }
            }

            // Блокируем поля, только если пользователь уже в Nekuriene и выбрана Nekuriene
            bool isEditable = !(selectedIsNekuriene && userAlreadyInNekuriene);

            SetUserFieldsEditable(isEditable);

            var statusLines = new List<string>();

            if (selectedIsNekuriene && userAlreadyInNekuriene)
            {
                statusLines.Add("- Department: Nekuriene (modifications disabled)");
            }
            else
            {
                if (currentUser != null)
                {
                    if (!currentUser.IsEnabled)
                    {
                        statusLines.Add("- Account is disabled");
                        chBx_enableUsr.Enabled = true;
                    }

                    if (currentUser.IsLocked)
                    {
                        statusLines.Add("- Account is locked out");
                        chBx_unlockUsr.Enabled = true;
                    }

                    if (currentUser.ExpirationDate.HasValue && currentUser.ExpirationDate.Value < DateTime.Now)
                    {
                        statusLines.Add("- Account is expired");
                    }
                }
            }

            if (statusLines.Count == 0)
                statusLines.Add("- Account is active");

            chUserStatus_txt.Text = "User Status:" + Environment.NewLine + string.Join(Environment.NewLine, statusLines);
        }


        private void changeUsrInfoBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(chUserSrch_txt.Text))
            {
                MessageBox.Show("Nav izvēlēts lietotājs", "Nepieciešama ievade", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrWhiteSpace(chUserNm_txt.Text) || string.IsNullOrWhiteSpace(chUserSn_txt.Text))
            {
                MessageBox.Show("Lūdzu, ievadiet gan vārdu, gan uzvārdu.", "Nepilnīga informācija", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var result = GlobalMethods.GenerateUserNames(chUserNm_txt.Text.Trim(), chUserSn_txt.Text.Trim());
            string newSamAccountName = result.genSAMAcc;
            string newCN = $"{chUserNm_txt.Text.Trim()} {chUserSn_txt.Text.Trim()}";

            UpdateADUser(
                samAccountName: chUserSrch_txt.Text.Trim(),
                newCN: newCN,
                newGivenName: chUserNm_txt.Text.Trim(),
                newSurname: chUserSn_txt.Text.Trim(),
                newEmail: chUserMail_txt.Text.Trim(),
                newDisplayName: chUserDNm_txt.Text.Trim(),
                newPassword: string.IsNullOrWhiteSpace(chUserPswd_txt.Text) ? null : chUserPswd_txt.Text,
                newExpirationDate: chUserExp_datetime.Checked ? chUserExp_datetime.Value : (DateTime?)null,
                newDescription: chUserDesc_txt.Text.Trim(),
                newSamAccountName: newSamAccountName
            );

            // 🔄 Повторная загрузка по новому SamAccountName
            try
            {
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
                using (UserPrincipal user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, newSamAccountName))
                {
                    if (user != null)
                    {
                        UserInfo updatedInfo = MapUserPrincipalToUserInfo(user);
                        DisplayUserInfoInTextboxes(updatedInfo);
                        chUserSrch_txt.Text = newSamAccountName; // Обновляем текст поиска тоже
                        //MessageBox.Show("Lietotāja informācija ir veiksmīgi atjaunināta.", "Gatavs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Neizdevās atrast lietotāju pēc atjaunošanas.", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kļūda, ielādējot lietotāja jaunus datus: {ex.Message}", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        public void UpdateADUser(string samAccountName, string newCN, string newGivenName, string newSurname, string newEmail, string newDisplayName, string newPassword, DateTime? newExpirationDate, string newDescription, string newSamAccountName)
        {
            try
            {
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
                {
                    UserPrincipal user = UserPrincipal.FindByIdentity(context, samAccountName);
                    if (user == null)
                    {
                        MessageBox.Show("Lietotājs nav atrasts.");
                        return;
                    }

                    bool changesMade = false;

                    using (DirectoryEntry userEntry = (DirectoryEntry)user.GetUnderlyingObject())
                    {
                        // CN maiņa
                        if (!string.IsNullOrEmpty(newCN) && !userEntry.Properties["cn"].Value?.ToString().Equals(newCN, StringComparison.OrdinalIgnoreCase) == true)
                        {
                            userEntry.Rename($"CN={newCN}");
                            GlobalMethods.InsertLog(DateTime.Now, "CN Change", $"Lietotājam {samAccountName} CN mainīts uz '{newCN}'");
                            changesMade = true;
                        }

                        if (!string.IsNullOrEmpty(newGivenName) && user.GivenName != newGivenName)
                        {
                            user.GivenName = newGivenName;
                            GlobalMethods.InsertLog(DateTime.Now, "GivenName Change", $"Lietotājam {samAccountName} vārds mainīts uz '{newGivenName}'");
                            changesMade = true;
                        }

                        if (!string.IsNullOrEmpty(newSurname) && user.Surname != newSurname)
                        {
                            user.Surname = newSurname;
                            GlobalMethods.InsertLog(DateTime.Now, "Surname Change", $"Lietotājam {samAccountName} uzvārds mainīts uz '{newSurname}'");
                            changesMade = true;
                        }

                        if (!string.IsNullOrEmpty(newEmail) && user.EmailAddress != newEmail)
                        {
                            user.EmailAddress = newEmail;

                            var proxyAddresses = userEntry.Properties["proxyAddresses"];
                            string newProxyEmail = "smtp:" + newEmail;
                            if (!proxyAddresses.Contains(newProxyEmail))
                            {
                                proxyAddresses.Add(newProxyEmail);
                            }

                            GlobalMethods.InsertLog(DateTime.Now, "Email Change", $"Lietotājam {samAccountName} e-pasts mainīts uz '{newEmail}'");
                            changesMade = true;
                        }

                        if (!string.IsNullOrEmpty(newDisplayName) && user.DisplayName != newDisplayName)
                        {
                            user.DisplayName = newDisplayName;
                            GlobalMethods.InsertLog(DateTime.Now, "DisplayName Change", $"Lietotājam {samAccountName} DisplayName mainīts uz '{newDisplayName}'");
                            changesMade = true;
                        }

                        if (!string.IsNullOrEmpty(newSamAccountName) && user.SamAccountName != newSamAccountName)
                        {
                            var duplicateUser = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, newSamAccountName);
                            if (duplicateUser != null && duplicateUser.Sid != user.Sid)
                            {
                                MessageBox.Show("Lietotājs ar tādu sAMAccountName jau eksistē.");
                                return;
                            }

                            user.SamAccountName = newSamAccountName;
                            userEntry.Properties["userPrincipalName"].Value = newSamAccountName + "@bakalaura.darbs";
                            GlobalMethods.InsertLog(DateTime.Now, "SamAccountName Change", $"Lietotāja {samAccountName} SamAccountName mainīts uz '{newSamAccountName}'");
                            changesMade = true;
                        }

                        if (!string.IsNullOrEmpty(newPassword))
                        {
                            try
                            {
                                user.SetPassword(newPassword);
                                user.ExpirePasswordNow();
                                GlobalMethods.InsertLog(DateTime.Now, "Password Change", $"Lietotājam {samAccountName} parole tika nomainīta.");
                                changesMade = true;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Neizdevās iestatīt paroli: " + ex.Message);
                                return;
                            }
                        }

                        if (newExpirationDate.HasValue && (!user.AccountExpirationDate.HasValue || user.AccountExpirationDate.Value != newExpirationDate.Value))
                        {
                            user.AccountExpirationDate = newExpirationDate;
                            GlobalMethods.InsertLog(DateTime.Now, "Expiration Change", $"Lietotājam {samAccountName} termiņš iestatīts uz {newExpirationDate.Value}");
                            changesMade = true;
                        }

                        if (!string.Equals(user.Description, newDescription, StringComparison.OrdinalIgnoreCase))
                        {
                            user.Description = newDescription;
                            GlobalMethods.InsertLog(DateTime.Now, "Description Change", $"Lietotājam {samAccountName} apraksts mainīts uz '{newDescription}'");
                            changesMade = true;
                        }

                        // OU pārvietošana
                        string targetDN = $"OU={chUserDpt_cmbx.Text},OU=Lietotāji,OU=Riga,DC=bakalaura,DC=darbs";
                        string currentOU = userEntry.Parent.Properties["distinguishedName"].Value.ToString();

                        if (!currentOU.StartsWith(targetDN, StringComparison.OrdinalIgnoreCase))
                        {
                            using (DirectoryEntry newLocation = new DirectoryEntry("LDAP://" + targetDN))
                            {
                                userEntry.MoveTo(newLocation);
                                GlobalMethods.InsertLog(DateTime.Now, "Move OU", $"Lietotājs {samAccountName} pārvietots uz {targetDN}");
                                changesMade = true;
                            }
                        }

                        if (chBx_enableUsr.Checked && user.Enabled == false)
                        {
                            user.Enabled = true;
                            GlobalMethods.InsertLog(DateTime.Now, "Account Enabled", $"Lietotājs {samAccountName} tika aktivizēts.");
                            changesMade = true;
                        }

                        if (chBx_unlockUsr.Checked && user.IsAccountLockedOut())
                        {
                            user.UnlockAccount();
                            GlobalMethods.InsertLog(DateTime.Now, "Account Unlocked", $"Lietotāja {samAccountName} konts tika atbloķēts.");
                            changesMade = true;
                        }

                        if (changesMade)
                        {
                            user.Save();
                            MessageBox.Show("Lietotāja informācija ir veiksmīgi atjaunināta.");
                        }
                        else
                        {
                            MessageBox.Show("Izmaiņas nav konstatētas.");
                        }
                    }
                }
            }
            catch (DirectoryServicesCOMException ex)
            {
                if (ex.ExtendedErrorMessage != null && ex.ExtendedErrorMessage.Contains("ENTRY_ALREADY_EXISTS"))
                {
                    MessageBox.Show("Kļūda: lietotājs ar tādiem datiem eksistē.");
                }
                else
                {
                    MessageBox.Show("Active Directory kļūda: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Radās kļūda: " + ex.Message);
            }
        }

        private void GnrtUsrPswd_btn_Click(object sender, EventArgs e)
        {
            const int passwordLength = 12;
            chUserPswd_txt.Text = GlobalMethods.GeneratePassword(passwordLength);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string aboutText =
            "🛠️ Active Directory Lietotāju Pārvaldības Rīks 🛠️\n\n" +
            "Versija: 1.1.0\n" +
            "Izstrādātājs: Edmunds Matusēvičs\n\n" +
            "Šī programma ir izveidota, lai atvieglotu Active Directory lietotāju " +
            "pārvaldību: rediģē lietotāju datus, maina paroles, bloķē kontus un pārvieto lietotājus " +
            "uz atbilstošām organizatoriskajām vienībām (OU).\n\n" +
            "Lietotājam draudzīgs interfeiss un uzticama darbība padara ikdienas AD administrēšanu " +
            "vienkāršāku un efektīvāku.\n\n" +
            "© 2025 Visas tiesības aizsargātas.";

            MessageBox.Show(aboutText, "Par programmu", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        //Bloks create user

    }
}
