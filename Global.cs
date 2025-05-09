using System;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AdminTool
{
    internal class Global
    {
    }
    public static class GlobalMethods
    {
        public static string ReplaceLatvianSymbols(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;

            return input
                .Replace("ā", "a")
                .Replace("č", "c")
                .Replace("ē", "e")
                .Replace("ģ", "g")
                .Replace("ī", "i")
                .Replace("ķ", "k")
                .Replace("ļ", "l")
                .Replace("ņ", "n")
                .Replace("š", "s")
                .Replace("ū", "u")
                .Replace("ž", "z")
                .Replace("Ā", "A")
                .Replace("Č", "C")
                .Replace("Ē", "E")
                .Replace("Ģ", "G")
                .Replace("Ī", "I")
                .Replace("Ķ", "K")
                .Replace("Ļ", "L")
                .Replace("Ņ", "N")
                .Replace("Š", "S")
                .Replace("Ū", "U")
                .Replace("Ž", "Z");
        }

        public static string GeneratePassword(int length)
        {
            const string validChars = "ABCDEFGHJKLMNPQRSTUVWXYZabcdefghjkmnpqrstuvwxyz123456789!@#$%&*()[]";
            Random random = new Random();

            char[] password = new char[length];
            for (int i = 0; i < length; i++)
            {
                password[i] = validChars[random.Next(validChars.Length)];
            }

            return new string(password);
        }

       
        public static bool InsertLog(DateTime eventTime, string eventType, string eventMessage)
        {
            string query = "INSERT INTO Logi ( EventTime, EventType, EventMessage) VALUES (@EventTime, @EventType, @EventMessage)";
            string connectionString = @"Server=mssql;Database=bakalaura_darbs;Trusted_Connection=True;";
            try
            {
                // Establish the SQL connection
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    // Open the connection
                    connection.Open();

                    // Create the SQL command
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        // Add parameters to prevent SQL injection
                        command.Parameters.AddWithValue("@EventTime", eventTime);
                        command.Parameters.AddWithValue("@EventType", eventType);
                        command.Parameters.AddWithValue("@EventMessage", eventMessage);

                        // Execute the command
                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0; // Return true if at least one row was inserted
                    }
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception
                MessageBox.Show($"Error with Logs: {ex.Message}", "Error");
                return false;
            }
        }
       
        public static bool IsSamAccountNameUnique(string samAccountName)
        {
            try
            {
                // Connect to the domain
                using (PrincipalContext context = new PrincipalContext(ContextType.Domain))
                {
                    // Search for a user with the given sAMAccountName
                    UserPrincipal user = UserPrincipal.FindByIdentity(context, IdentityType.SamAccountName, samAccountName);

                    // If a user is found, the sAMAccountName is not unique
                    return user == null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                return false;
            }
        }

        public static (string genEmailText, string genAccount, string genDisplayName, string genSAMAcc) GenerateUserNames(string name, string lastname)
        {
            string genEmailText;
            string genAccount;
            string genDisplayName;
            string genSAMAcc;

            if (name != "" && lastname != "")
            {
                genEmailText = name + "." + lastname + "@bakalaura.darbs";
                genAccount = name + " " + lastname;
                genSAMAcc = name + "." + lastname; // SAMAccountName

                if (genSAMAcc.Length > 20)
                {
                    // If it's too long, create a shortened version (first char of name + lastname)
                    genSAMAcc = name.Substring(0, 1) + "."+ lastname;
                }


            }
            else if (name != "" && lastname == "")
            {
                genEmailText = name + "@bakalaura.darbs";
                genAccount = name;
                genSAMAcc = name; // SAMAccountName
            }
            else if (name == "" && lastname != "")
            {
                // Replace Latvian characters with English equivalents
                genEmailText = lastname + "@bakalaura.darbs";
                genAccount = lastname;
                genSAMAcc = lastname; // SAMAccountName
            }
            else
            {
                genEmailText = "";
                genAccount = "";
                genSAMAcc = "."; // SAMAccountName
            }

            genEmailText = GlobalMethods.ReplaceLatvianSymbols(genEmailText);
            genDisplayName = genAccount;
            genSAMAcc = GlobalMethods.ReplaceLatvianSymbols(genSAMAcc);

            return (genEmailText, genAccount, genDisplayName, genSAMAcc);


        }

        public static string ConvertDisplayName(string displayName)
        {
            //Ignore simbolus un ciparus
            string pattern = @"[^a-zA-ZāčēģīķļņšūžĀČĒĢĪĶĻŅŠŪŽ\s]";
            displayName = Regex.Replace(displayName, pattern, "");

            return displayName;
        }

        public static void DisplayOUsInComboBox(string parentOU, ComboBox comboBox)
        {
            comboBox.Items.Clear(); // Clear existing items

            try
            {
                // Define the path to the parent OU
                string ldapPath = $"LDAP://{parentOU}";

                // Create a DirectoryEntry for the parent OU
                using (DirectoryEntry parentEntry = new DirectoryEntry(ldapPath))
                {
                    // Iterate through child entries
                    foreach (DirectoryEntry childEntry in parentEntry.Children)
                    {
                        // Check if the child entry is an OU
                        if (childEntry.SchemaClassName == "organizationalUnit")
                        {
                            comboBox.Items.Add(childEntry.Name.Replace("OU=", "")); // Add OU name only
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
        }

    }
}
