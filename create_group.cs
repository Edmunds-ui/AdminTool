using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AdminTool
{
    public partial class create_group: Form
    {
        public create_group()
        {
            InitializeComponent();
        }

        private void create_group_Load(object sender, EventArgs e)
        {
            LoadGroupsFromOU();
        }

        private void LoadGroupsFromOU()
        {
            string ouPath = "OU=Grupas,OU=Riga,DC=bakalaura,DC=darbs";
            string ldapPath = $"LDAP://{ouPath}";

            try
            {
                DirectoryEntry entry = new DirectoryEntry(ldapPath);
                DirectorySearcher searcher = new DirectorySearcher(entry)
                {
                    Filter = "(objectClass=group)"
                };

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
            catch (Exception ex)
            {
                MessageBox.Show($"Kļūda ielādējot grupas: {ex.Message}", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void create_group_btn_Click(object sender, EventArgs e)
        {
            string groupName = newGroupName.Text.Trim();
            if (string.IsNullOrWhiteSpace(groupName))
            {
                MessageBox.Show("Lūdzu, ievadiet grupas nosaukumu.");
                return;
            }

            try
            {
                string ldapPath = "LDAP://OU=Grupas,OU=Riga,DC=bakalaura,DC=darbs";

                // Pārbaude, vai grupa jau eksistē
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain, null, "OU=Grupas,OU=Riga,DC=bakalaura,DC=darbs"))
                {
                    GroupPrincipal existingGroup = GroupPrincipal.FindByIdentity(context, groupName);
                    if (existingGroup != null)
                    {
                        MessageBox.Show("Grupa ar šādu nosaukumu jau eksistē.", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Ja nav — izveidojam
                using (DirectoryEntry ouEntry = new DirectoryEntry(ldapPath))
                {
                    DirectoryEntry newGroup = ouEntry.Children.Add($"CN={groupName}", "group");
                    newGroup.Properties["sAMAccountName"].Value = groupName;
                    newGroup.Properties["description"].Value = "Izveidots automātiski";
                    newGroup.Properties["groupType"].Value = unchecked((int)0x80000002); // Universal security group
                    newGroup.CommitChanges();

                    MessageBox.Show("Grupa veiksmīgi izveidota.", "Veiksmīgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // Data to insert
                    LoadGroupsFromOU();
                    DateTime eventTime = DateTime.Now;
                    string eventType = "Grupa";
                    string eventMessage = "Grupa " + groupName + " izveidota " + "by: " + Environment.UserName;

                    // Call the function to insert data
                    GlobalMethods.InsertLog(eventTime, eventType, eventMessage);
                    newGroupName.Clear();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kļūda: {ex.Message}", "Izņēmums", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void delete_group_btn_Click_1(object sender, EventArgs e)
        {
            // Pārbaudām, vai grupas nosaukums ir izvēlēts ListBox
            if (groups_listBox.SelectedItem == null)
            {
                MessageBox.Show("Lūdzu, izvēlieties grupu no saraksta.");
                return;
            }

            string groupName = groups_listBox.SelectedItem.ToString(); // Iegūstam izvēlēto grupas nosaukumu no ListBox
            string ouPath = "OU=Grupas,OU=Riga,DC=bakalaura,DC=darbs"; // OU ceļš, kur grupa atrodas
            string ldapPath = $"LDAP://CN={groupName},{ouPath}"; // Pilns LDAP ceļš uz grupu

            // Apstiprinājuma ziņojums pirms dzēšanas
            DialogResult dialogResult = MessageBox.Show($"Vai tiešām vēlaties izdzēst grupu '{groupName}'?",
                "Apstiprinājums", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (dialogResult == DialogResult.No)
            {
                // Ja lietotājs izvēlas "No", dzēšana tiek atcelta
                return;
            }

            try
            {
                // Izveido DirectoryEntry objektu
                DirectoryEntry groupEntry = new DirectoryEntry(ldapPath);

                // Pārliecināmies, ka grupa eksistē, un dzēšam
                groupEntry.DeleteTree();

                // Apstiprinām izmaiņas
                groupEntry.CommitChanges();

                MessageBox.Show("Grupa veiksmīgi izdzēsta.", "Veiksmīgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadGroupsFromOU();

                DateTime eventTime = DateTime.Now;
                string eventType = "Grupa";
                string eventMessage = "Grupa " + groupName + " izdzēsta " + "by: " + Environment.UserName;

                // Call the function to insert data
                GlobalMethods.InsertLog(eventTime, eventType, eventMessage);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Kļūda dzēšot grupu: {ex.Message}", "Izņēmums", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
