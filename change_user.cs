using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AdminTool
{
    public partial class change_user : Form
    {
        public change_user()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string username = txtSearch.Text.Trim();
            cmbxDepartment.SelectedIndex = -1;
            changeUsrPswrd_txtbx.Clear();

            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Lūdzu, ievadiet lietotājvārdu, lai meklētu.", "Nepieciešama ievade", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
                using (PrincipalSearcher searcher = new PrincipalSearcher(new UserPrincipal(context) { SamAccountName = $"*{username}*" }))
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

        private static string ExtractLastOUFromDistinguishedName(string distinguishedName)
        {
            if (string.IsNullOrEmpty(distinguishedName)) return string.Empty;

            // Sadalīt DistinguishedName un atrast pēdējo "OU=" daļu
            var ouParts = distinguishedName.Split(',').Where(part => part.StartsWith("OU=", StringComparison.OrdinalIgnoreCase));
            return ouParts.FirstOrDefault()?.Substring(3); // Noņem prefiksu "OU="
        }

        private void DisplayUserInfoInTextboxes(UserInfo user)
        {
            // Aizpildām laukus
            txtDisplayName.Text = user.DisplayName;
            txtEmail.Text = user.Email;
            txtDescription.Text = user.Description;
            txtSearch.Text = txtSamAccountName.Text = user.SamAccountName;
            UsrNm2change_txtbx.Text = user.GivenName;
            UsrSn2change_txtbx.Text = user.Surname;

            // Iezīmējam OU izvēlnē
            SelectOUInComboBox(user.LastOU);

            // Sagatavo statusa ziņojumu
            var statusLines = new List<string>();

            if (user.ExpirationDate.HasValue)
            {
                dateTimePickerExpiration.Value = user.ExpirationDate.Value;

                if (user.ExpirationDate.Value < DateTime.Now)
                    statusLines.Add("- Account is expired");
            }
            else
            {
                dateTimePickerExpiration.Value = DateTime.Today;
            }

            if (!user.IsEnabled)
                statusLines.Add("- Account is disabled");

            if (user.IsLocked)
                statusLines.Add("- Account is locked out");

            if (statusLines.Count == 0)
                statusLines.Add("- Account is active");

            // Attēlo statusu
            textBox1.Text = "User Status:" + Environment.NewLine + string.Join(Environment.NewLine, statusLines);
        }

        private void SelectOUInComboBox(string lastOu)
        {
            // Ja OU nav tukšs un tas eksistē sarakstā, atlasa to
            if (!string.IsNullOrEmpty(lastOu) && cmbxDepartment.Items.Contains(lastOu))
                cmbxDepartment.SelectedItem = lastOu;
        }

        private void ShowUserSelectionForm(List<UserInfo> users)
        {
            Form selectionForm = new Form
            {
                Text = "Izvēlies lietotāju",
                Width = 680,
                Height = 400
            };

            ListView listView = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true
            };

            listView.Columns.Add("Name", 150);
            listView.Columns.Add("Email", 150);
            listView.Columns.Add("Description", 150);
            listView.Columns.Add("SamAccountName", 150);

            foreach (var user in users)
            {
                var item = new ListViewItem(user.DisplayName);
                item.SubItems.Add(user.Email ?? "");
                item.SubItems.Add(user.Description ?? "");
                item.SubItems.Add(user.SamAccountName ?? "");
                item.Tag = user;

                listView.Items.Add(item);
            }

            listView.DoubleClick += (s, e) =>
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
            };

            selectionForm.Controls.Add(listView);
            selectionForm.ShowDialog();
        }

        private class UserInfo
        {
            public string DisplayName { get; set; }        // Pilns vārds
            public string GivenName { get; set; }          // Vārds
            public string Surname { get; set; }            // Uzvārds
            public string Email { get; set; }              // E-pasta adrese
            public string Description { get; set; }        // Apraksts no AD
            public string SamAccountName { get; set; }     // Lietotājvārds (SAM)
            public string LastOU { get; set; }             // Pēdējais OU no DN
            public DateTime? ExpirationDate { get; set; }  // Konta derīguma termiņš
            public bool IsLocked { get; set; }             // Vai konts ir bloķēts
            public bool IsEnabled { get; set; }            // Vai konts ir aktīvs
        }

        private void GnrtUsrPswd_btn_Click(object sender, EventArgs e)
        {
            const int passwordLength = 12;
            changeUsrPswrd_txtbx.Text = GlobalMethods.GeneratePassword(passwordLength);
        }

        private void change_user_Load(object sender, EventArgs e)
        {
            // Aizpildīt ComboBox ar organizatoriskajām vienībām (OU) noteiktā 'parent OU'
            string parentOU = "OU=Lietotāji,OU=Riga,DC=bakalaura,DC=darbs";  // LDAP ceļš
            GlobalMethods.DisplayOUsInComboBox(parentOU, cmbxDepartment); // Combobox'ā parāda norādītās OU
        }

        private void changeUsrInfoBtn_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                MessageBox.Show("Nav izvēlēts lietotājs", "Nepieciešama ievade", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ģenerē jauno SAMAccountName
            var result = GlobalMethods.GenerateUserNames(UsrNm2change_txtbx.Text, UsrSn2change_txtbx.Text);
            string newSamAccountName = result.genSAMAcc;

            // Apvieno vārdu un uzvārdu Common Name laukam
            string newCN = $"{UsrNm2change_txtbx.Text} {UsrSn2change_txtbx.Text}";

            // Veic atjaunināšanu Active Directory
            UpdateADUser(
                samAccountName: txtSearch.Text,
                newCN: newCN,
                newGivenName: UsrNm2change_txtbx.Text,
                newSurname: UsrSn2change_txtbx.Text,
                newEmail: txtEmail.Text,
                newDisplayName: txtDisplayName.Text,
                newPassword: changeUsrPswrd_txtbx.Text,
                newExpirationDate: dateTimePickerExpiration.Value,
                newDescription: txtDescription.Text,
                newSamAccountName: newSamAccountName
            );
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
                            changesMade = true;
                        }

                        // GivenName
                        if (!string.IsNullOrEmpty(newGivenName) && user.GivenName != newGivenName)
                        {
                            user.GivenName = newGivenName;
                            changesMade = true;
                        }

                        // Surname
                        if (!string.IsNullOrEmpty(newSurname) && user.Surname != newSurname)
                        {
                            user.Surname = newSurname;
                            changesMade = true;
                        }

                        // Email
                        if (!string.IsNullOrEmpty(newEmail) && user.EmailAddress != newEmail)
                        {
                            user.EmailAddress = newEmail;

                            var proxyAddresses = userEntry.Properties["proxyAddresses"];
                            string newProxyEmail = "smtp:" + newEmail;

                            if (!proxyAddresses.Contains(newProxyEmail))
                            {
                                proxyAddresses.Add(newProxyEmail);
                                changesMade = true;
                            }

                            changesMade = true;
                        }

                        // DisplayName
                        if (!string.IsNullOrEmpty(newDisplayName) && user.DisplayName != newDisplayName)
                        {
                            user.DisplayName = newDisplayName;
                            changesMade = true;
                        }

                        // sAMAccountName maiņa ar dublikāta pārbaudi
                        if (!string.IsNullOrEmpty(newSamAccountName) && user.SamAccountName != newSamAccountName)
                        {
                            var duplicateUser = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, newSamAccountName);
                            if (duplicateUser != null && duplicateUser.Sid != user.Sid)
                            {
                                MessageBox.Show("lietotājs ar tādiem datiem eksistē.");
                                return;
                            }

                            user.SamAccountName = newSamAccountName;
                            userEntry.Properties["userPrincipalName"].Value = newSamAccountName + "@bakalaura.darbs";
                            changesMade = true;
                        }

                        // Parole
                        if (!string.IsNullOrEmpty(newPassword))
                        {
                            user.SetPassword(newPassword);
                            user.ExpirePasswordNow();
                            changesMade = true;
                        }

                        // Expiration Date
                        if (newExpirationDate.HasValue && (!user.AccountExpirationDate.HasValue || user.AccountExpirationDate.Value != newExpirationDate.Value))
                        {
                            user.AccountExpirationDate = newExpirationDate;
                            changesMade = true;
                        }

                        // Apraksts
                        if (userEntry.Properties["description"].Value?.ToString() != newDescription)
                        {
                            userEntry.Properties["description"].Value = newDescription;
                            changesMade = true;
                        }

                        // OU pārvietošana
                        string targetOU = "LDAP://OU=" + cmbxDepartment.Text + ",OU=Lietotāji,OU=Riga,DC=bakalaura,DC=darbs";
                        if (userEntry.Parent.Path != targetOU)
                        {
                            using (DirectoryEntry newLocation = new DirectoryEntry(targetOU))
                            {
                                userEntry.MoveTo(newLocation);
                                changesMade = true;
                            }
                        }

                        // Aktivizēt lietotāju, ja nepieciešams
                        if (chBx_enableUsr.Checked && user.Enabled == false)
                        {
                            user.Enabled = true;
                            changesMade = true;
                        }

                        // Atslēgt lietotāju, ja nepieciešams
                        if (chBx_unlockUsr.Checked && user.IsAccountLockedOut())
                        {
                            user.UnlockAccount();
                            changesMade = true;
                        }

                        if (changesMade)
                        {
                            user.Save();
                            userEntry.CommitChanges();
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

        private void UsrNm2change_txtbx_TextChanged(object sender, EventArgs e)
        {

            // Iegūst 'tuple' rezultātu no metodes
            var result = GlobalMethods.GenerateUserNames(UsrNm2change_txtbx.Text, UsrSn2change_txtbx.Text);

            // Atjaunina vertības ar 'tuple'
            txtEmail.Text = result.genEmailText;     // Jauna e-pasta adrese
            txtSamAccountName.Text = result.genAccount;  // Jauns AMAccountName
            txtDisplayName.Text = result.genDisplayName; // Jauns Display Name
        }

        private void UsrSn2change_txtbx_TextChanged(object sender, EventArgs e)
        {

            // Iegūst 'tuple' rezultātu no metodes
            var result = GlobalMethods.GenerateUserNames(UsrNm2change_txtbx.Text, UsrSn2change_txtbx.Text);

            // Update the textboxes with the tuple values
            txtEmail.Text = result.genEmailText;     // Jauna e-pasta adrese
            txtSamAccountName.Text = result.genAccount;  // Jauns AMAccountName
            txtDisplayName.Text = result.genDisplayName; // Jauns Display Name
        }

        private void txtDisplayName_TextChanged(object sender, EventArgs e)
        {
            txtDisplayName.Text = GlobalMethods.ConvertDisplayName(txtDisplayName.Text);
        }

        private void txtSearch_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow letters, digits, dot, space, and control characters (like Backspace)
            if (!char.IsLetterOrDigit(e.KeyChar) && e.KeyChar != '.' && e.KeyChar != ' ' && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Suppress the key press
            }
        }

        private void txtEmail_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow letters, digits, dot, space, and control characters (like Backspace)
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Suppress the key press
            }
        }

        private void UsrNm2change_txtbx_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow letters, digits, dot, space, and control characters (like Backspace)
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Suppress the key press
            }
        }

        private void UsrSn2change_txtbx_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow letters, digits, dot, space, and control characters (like Backspace)
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Suppress the key press
            }
        }

        private void txtSamAccountName_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow letters, digits, dot, space, and control characters (like Backspace)
            if (!char.IsLetterOrDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true; // Suppress the key press
            }
        }
    }
}
