using ExcelDataReader;
using System.IO;
using System.Windows;

namespace WinnerApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<string> applicants = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnUpload_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog
            {
                //  Filter = "Excel Files|*.xls;*.xlsx|Text Files|*.txt|All Files|*.*"
                Filter = "Text Files|*.txt|All Files|*.*"

            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                if (filePath.EndsWith(".txt"))
                {
                    LoadFromTextFile(filePath);
                }
                //else if (filePath.EndsWith(".xls") || filePath.EndsWith(".xlsx"))
                //{
                //    //LoadFromExcelFile(filePath);
                //    if (!IsFileLocked(filePath))
                //    {
                //        LoadFromExcelFile(filePath);
                //    }
                //    else
                //    {
                //        MessageBox.Show("The file is currently in use by another process.");
                //    }
                //}
            }
        }

        private void LoadFromTextFile(string filePath)
        {
            applicants = File.ReadAllLines(filePath).ToList();
            listBoxApplicants.ItemsSource = applicants;
        }
        private bool IsFileLocked(string filePath)
        {
            try
            {
                using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return true;
            }
            return false;
        }
        private void LoadFromExcelFile(string filePath)
        {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        applicants.Add(reader.GetString(0));
                    }
                }
            }
            listBoxApplicants.ItemsSource = applicants;
        }

        private void btnSelectWinner_Click(object sender, RoutedEventArgs e)
        {
            //if (applicants.Count > 0)
            //{
            //    Random random = new Random();
            //    int winnerIndex = random.Next(applicants.Count);
            //    lblWinner.Content = $"Winner: {applicants[winnerIndex]}";
            //}
            //else
            //{
            //    MessageBox.Show("No applicants to select from.");
            //}

            //Random random = new Random();
            //HashSet<int> numbers = new HashSet<int>();

            //while (numbers.Count < 500)
            //{
            //    numbers.Add(random.Next(100, 1000)); // Generates a random number between 100 and 999
            //}

            //// Define the file path
            ////   string filePath = "random_numbers.txt";
            //string directoryPath = @"E:\";
            //string filePath = Path.Combine(directoryPath, "random_numbers.txt");

            //// Write the numbers to the file
            //using (StreamWriter writer = new StreamWriter(filePath))
            //{
            //    foreach (int number in numbers)
            //    {
            //        writer.WriteLine(number);
            //    }
            //}

            //Console.WriteLine($"Random numbers saved to {filePath}");


            if (applicants.Count > 0)
            {
                Random random = new Random();
                int winnerIndex = random.Next(applicants.Count);
                string winnerName = applicants[winnerIndex];

                // Open the WinnerWindow with the winner's name
                WinnerWindow winnerWindow = new WinnerWindow(winnerName);
                winnerWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("No applicants to select from.");
            }
        }
    }
}