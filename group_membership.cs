using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Linq;
using System.Windows.Forms;

namespace AdminTool
{
    public partial class group_membership : Form
    {
        public group_membership()
        {
            InitializeComponent();
            IeladetGrupas();
        }

        private void IeladetGrupas()
        {
            // Ielādē grupas no noteiktā OU
            string grupasOU = "OU=Grupas,OU=Riga,DC=bakalaura,DC=darbs";

            using (DirectoryEntry entry = new DirectoryEntry("LDAP://" + grupasOU))
            {
                foreach (DirectoryEntry child in entry.Children)
                {
                    if (child.SchemaClassName == "group")
                    {
                        grps_lstbx.Items.Add(child.Properties["name"].Value.ToString());
                    }
                }
            }
        }

        private List<string> IegutVisusLietotajus()
        {
            List<string> lietotaji = new List<string>();
            string lietotajiOU = "OU=Lietotāji,OU=Riga,DC=bakalaura,DC=darbs";

            using (DirectoryEntry entry = new DirectoryEntry("LDAP://" + lietotajiOU))
            using (DirectorySearcher searcher = new DirectorySearcher(entry))
            {
                searcher.Filter = "(objectClass=user)";
                searcher.SearchScope = SearchScope.Subtree; // Meklē arī apakš-OU

                foreach (SearchResult result in searcher.FindAll())
                {
                    if (result.Properties.Contains("sAMAccountName"))
                    {
                        lietotaji.Add(result.Properties["sAMAccountName"][0].ToString());
                    }
                }
            }

            return lietotaji;
        }

        private List<string> IegutNekurienesLietotajus()
        {
            // Atgriež lietotājus no "Nekuriene"
            List<string> lietotaji = new List<string>();
            string nekurienesOU = "OU=Nekuriene,OU=Lietotāji,OU=Riga,DC=bakalaura,DC=darbs";

            using (DirectoryEntry entry = new DirectoryEntry("LDAP://" + nekurienesOU))
            {
                foreach (DirectoryEntry child in entry.Children)
                {
                    if (child.SchemaClassName == "user")
                    {
                        lietotaji.Add(child.Properties["sAMAccountName"].Value.ToString());
                    }
                }
            }
            return lietotaji;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grps_lstbx.SelectedItem == null) return;

            string groupName = grps_lstbx.SelectedItem.ToString();

            groupmembers_lstbx.Items.Clear();
            allusers_lstbox.Items.Clear();

            List<string> grupasLietotaji = new List<string>();
            List<string> visiLietotaji = IegutVisusLietotajus();
            List<string> nekurienesLietotaji = IegutNekurienesLietotajus();

            // Iegūst grupas lietotājus
            using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
            {
                GroupPrincipal group = GroupPrincipal.FindByIdentity(context, groupName);
                if (group != null)
                {
                    foreach (var user in group.GetMembers())
                    {
                        groupmembers_lstbx.Items.Add(user.SamAccountName);
                        grupasLietotaji.Add(user.SamAccountName);
                    }
                }
            }

            // Atrodam visus, kuri NAV grupā un NAV "Nekuriene"
            var atlikusieLietotaji = visiLietotaji
                .Except(grupasLietotaji)
                .Except(nekurienesLietotaji);

            foreach (string lietotajs in atlikusieLietotaji)
            {
                allusers_lstbox.Items.Add(lietotajs);
            }
        }

        private void add_btn_Click(object sender, EventArgs e)
        {
            // Pārbauda, vai ir izvēlēts lietotājs no allusers_lstbox un grupa no grps_lstbx
            if (allusers_lstbox.SelectedItem != null && grps_lstbx.SelectedItem != null)
            {
                string selectedUser = allusers_lstbox.SelectedItem.ToString(); // Lietotājs no allusers_lstbox
                string selectedGroup = grps_lstbx.SelectedItem.ToString(); // Grupa no grps_lstbx

                try
                {
                    // Iegūstam grupu un lietotāju no Active Directory
                    using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
                    {
                        // Atrodam izvēlēto grupu
                        GroupPrincipal group = GroupPrincipal.FindByIdentity(context, selectedGroup);
                        if (group != null)
                        {
                            // Atrodam lietotāju
                            UserPrincipal user = UserPrincipal.FindByIdentity(context, selectedUser);
                            if (user != null)
                            {
                                // Pievienojam lietotāju grupai
                                group.Members.Add(user);
                                group.Save();

                                MessageBox.Show($"Lietotājs {selectedUser} veiksmīgi pievienots grupai {selectedGroup}.", "Veiksmīgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Atjaunojam grupas un lietotāju sarakstus
                                listBox1_SelectedIndexChanged(sender, e);
                            }
                            else
                            {
                                MessageBox.Show($"Lietotājs {selectedUser} nav atrasts.", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Grupa {selectedGroup} nav atrasta.", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Kļūda pievienojot lietotāju grupai: {ex.Message}", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lūdzu, izvēlieties lietotāju un grupu.", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void rem_btn_Click(object sender, EventArgs e)
        {
            // Pārbauda, vai ir izvēlēts lietotājs no groupmembers_lstbx un grupa no grps_lstbx
            if (groupmembers_lstbx.SelectedItem != null && grps_lstbx.SelectedItem != null)
            {
                string selectedUser = groupmembers_lstbx.SelectedItem.ToString(); // Lietotājs no groupmembers_lstbx
                string selectedGroup = grps_lstbx.SelectedItem.ToString(); // Grupa no grps_lstbx

                try
                {
                    // Iegūstam grupu un lietotāju no Active Directory
                    using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
                    {
                        // Atrodam izvēlēto grupu
                        GroupPrincipal group = GroupPrincipal.FindByIdentity(context, selectedGroup);
                        if (group != null)
                        {
                            // Atrodam lietotāju
                            UserPrincipal user = UserPrincipal.FindByIdentity(context, selectedUser);
                            if (user != null)
                            {
                                // Noņemam lietotāju no grupas
                                group.Members.Remove(user);
                                group.Save();

                                MessageBox.Show($"Lietotājs {selectedUser} veiksmīgi noņemts no grupas {selectedGroup}.", "Veiksmīgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // Atjaunojam grupas un lietotāju sarakstus
                                listBox1_SelectedIndexChanged(sender, e);
                            }
                            else
                            {
                                MessageBox.Show($"Lietotājs {selectedUser} nav atrasts.", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                        else
                        {
                            MessageBox.Show($"Grupa {selectedGroup} nav atrasta.", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Kļūda noņemot lietotāju no grupas: {ex.Message}", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Lūdzu, izvēlieties grupas biedru un grupu.", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
