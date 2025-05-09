using System;
using System.Collections.Generic;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices;
using System.Windows.Forms;
using System.Linq;

namespace AdminTool
{
    public partial class block_user : Form
    {
        public block_user()
        {
            InitializeComponent();
        }

        // Lietotāja meklēšanas funkcija
        private void btnSearch_Click(object sender, EventArgs e)
        {
            string username = txtSearch.Text.Trim();

            // Ja nav ievadīts lietotājvārds
            if (string.IsNullOrEmpty(username))
            {
                MessageBox.Show("Lūdzu, ievadiet lietotājvārdu.", "Nepieciešama ievade", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // Meklē lietotāju Active Directory
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
                {
                    UserPrincipal searchTemplate = new UserPrincipal(context)
                    {
                        SamAccountName = $"*{username}*"
                    };

                    PrincipalSearcher searcher = new PrincipalSearcher(searchTemplate);
                    List<UserInfo> userList = new List<UserInfo>();

                    // Pievieno lietotājus uz sarakstu
                    foreach (var result in searcher.FindAll())
                    {
                        if (result is UserPrincipal user)
                        {
                            DirectoryEntry directoryEntry = user.GetUnderlyingObject() as DirectoryEntry;
                            if (directoryEntry != null)
                            {
                                string distinguishedName = directoryEntry.Properties["distinguishedName"]?.Value?.ToString();
                                string lastOu = ExtractLastOUFromDistinguishedName(distinguishedName);

                                userList.Add(new UserInfo
                                {
                                    DisplayName = user.DisplayName,
                                    GivenName = user.GivenName,
                                    Surname = user.Surname,
                                    Email = user.EmailAddress,
                                    Description = directoryEntry.Properties["description"]?.Value?.ToString(),
                                    SamAccountName = user.SamAccountName,
                                    LastOU = lastOu,
                                    ExpirationDate = user.AccountExpirationDate,
                                    IsLocked = user.IsAccountLockedOut(),
                                    IsEnabled = user.Enabled.Value
                                });
                            }
                        }
                    }

                    // Rādīt rezultātus
                    if (userList.Count == 0)
                    {
                        MessageBox.Show("Nav atrasts lietotājs.", "Meklēšanas rezultāti", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (userList.Count == 1)
                    {
                        DisplayUserInfoInTextboxes(userList[0]);
                    }
                    else
                    {
                        ShowUserSelectionForm(userList);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kļūda piekļūstot Active Directory: {ex.Message}", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Izņem pēdējo OU no DistinguishedName
        private static string ExtractLastOUFromDistinguishedName(string distinguishedName)
        {
            if (string.IsNullOrEmpty(distinguishedName)) return string.Empty;
            var ouParts = distinguishedName.Split(',').Where(part => part.StartsWith("OU=", StringComparison.OrdinalIgnoreCase));
            return ouParts.FirstOrDefault()?.Substring(3);
        }

        // Attēlo lietotāja informāciju laukos
        private void DisplayUserInfoInTextboxes(UserInfo user)
        {
            txtDisplayName.Text = user.DisplayName;
            txtEmail.Text = user.Email;
            txtDescription.Text = user.Description;
            txtSamAccountName.Text = user.SamAccountName;
            UsrNm2change_txtbx.Text = user.GivenName;
            UsrSn2change_txtbx.Text = user.Surname;
            SelectOUInComboBox(user.LastOU);
        }

        // Atlasīt OU ComboBox'ā
        private void SelectOUInComboBox(string lastOu)
        {
            if (!string.IsNullOrEmpty(lastOu))
            {
                cmbxDepartment.Text = lastOu;
            }
        }

        // Parāda lietotāju izvēles formu
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

            // Pievieno lietotājus uz sarakstu
            foreach (var user in users)
            {
                var item = new ListViewItem(user.DisplayName);
                item.SubItems.Add(user.Email ?? "");
                item.SubItems.Add(user.Description ?? "");
                item.SubItems.Add(user.SamAccountName ?? "");
                item.Tag = user;
                listView.Items.Add(item);
            }

            // DoubleClick lietotāja atlasei
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

        // Lietotāja bloķēšanas funkcija
        private void blockUserBtn_Click(object sender, EventArgs e)
        {
            string samAccountName = txtSamAccountName.Text.Trim();
            string newPassword = "JaunsParole123!";
            string adminName = Environment.UserName;
            string currentDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            string newDescription = $"Bloķēja administrators: {adminName} ({currentDate})";

            try
            {
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
                {
                    UserPrincipal user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, samAccountName);

                    if (user != null)
                    {
                        DirectoryEntry de = (DirectoryEntry)user.GetUnderlyingObject();

                        // 1. Deaktivizē lietotāju
                        user.Enabled = false;

                        // 2. Iestata derīguma termiņu
                        user.AccountExpirationDate = DateTime.Today.AddDays(-1);

                        // 3. Iestata jaunu paroli
                        user.SetPassword(newPassword);

                        // 4. Pievieno aprakstu
                        de.Properties["description"].Value = newDescription;

                        user.Save();

                        // 5. Noņem no grupām
                        foreach (GroupPrincipal group in user.GetGroups())
                        {
                            if (!group.Name.Equals("Domain Users", StringComparison.OrdinalIgnoreCase))
                            {
                                group.Members.Remove(user);
                                group.Save();
                            }
                        }

                        // 6. Pārvieto uz citu OU
                        string userCN = de.Properties["cn"].Value.ToString();
                        DirectoryEntry targetEntry = new DirectoryEntry("LDAP://" + "OU=Nekuriene,OU=Lietotāji,OU=Riga,DC=bakalaura,DC=darbs");
                        de.MoveTo(targetEntry, "CN=" + userCN);
                        de.CommitChanges();

                        MessageBox.Show("Lietotājs veiksmīgi bloķēts un pārvietots.", "Veiksmīgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Reģistrē notikumu
                        string eventMessage = $"Lietotājs {user.Name} bloķēts. Admin: {adminName}";
                        GlobalMethods.InsertLog(DateTime.Now, "Block", eventMessage);
                    }
                    else
                    {
                        MessageBox.Show("Lietotājs nav atrasts.", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kļūda: {ex.Message}", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Lietotāja informācijas klase
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
    }
}
