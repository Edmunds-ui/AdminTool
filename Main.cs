using System;
using System.DirectoryServices.AccountManagement;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Windows.Forms;

namespace AdminTool
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            CheckDomainConnection();

            // Pārbauda, vai lietotājs ir Helpdesk grupā. Ja nē, atspējo pogas un parāda kļūdas ziņojumu
            if (!IsCurrentUserInHelpdeskGroup())
            {
                DisableControls(); // Atspējo pogas
                MessageBox.Show("Jūs neesat Help desk grupas dalibnieks.", "Kļūda", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            InitializeTimer(); // Inicializē taimeri domēna statusa pārbaudīšanai
        }

        private void DisableControls()
        {
            //Palīgfunkcija, lai atspējotu pogas
            button1.Enabled = false;
            change_user.Enabled = false;
        }

        private void OpenFormIfNotOpen<T>(T form) where T : Form, new()
        {
            // Pārbauda, vai forma jau ir atvērta
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is T)
                {
                    openForm.BringToFront(); // Ja forma atvērta, uzliek to priekšplānā
                    return;
                }
            }
            form.Show(); // Ja forma nav atvērta, izveido un parāda jaunu formu
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

        private void button2_Click(object sender, EventArgs e)
        {
            // Atver Logi formu, ja tā vēl nav atvērta
            OpenFormIfNotOpen(new Logi());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Atver create_user formu, ja tā vēl nav atvērta
            OpenFormIfNotOpen(new create_user());
        }

        private void change_user_Click(object sender, EventArgs e)
        {
            // Atver change_user formu, ja tā vēl nav atvērta
            OpenFormIfNotOpen(new change_user());
        }

        private void CheckDomainConnection()
        {
            // Funkcija, lai pārbaudītu savienojumu ar domēnu
            try
            {
                Domain domain = Domain.GetCurrentDomain(); //Iegūst pašreizējo domēnu
                statusLabel.Text = $"Connected to domain: {domain.Name}"; //Parāda savienojuma statusu
                button1.Enabled = true; //Aktivizē pogas
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

        private void blockuser_btn_Click(object sender, EventArgs e)
        {
            //Atver block_user formu, ja tā vēl nav atvērta
            OpenFormIfNotOpen(new block_user());
        }

        private void CreateGroup_btn_Click(object sender, EventArgs e)
        {
            //Atver create_group formu, ja tā vēl nav atvērta
            OpenFormIfNotOpen(new create_group());
        }

        private void group_membership_btn_Click(object sender, EventArgs e)
        {
            //Atver create_group formu, ja tā vēl nav atvērta
            OpenFormIfNotOpen(new group_membership());
        }
    }
}
