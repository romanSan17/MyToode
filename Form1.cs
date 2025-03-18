using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace MyToode
{
    public partial class Form1 : Form
    {
        private string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Pood;Integrated Security=True";
        private NumericUpDown numQuantity => txtQuantity;
        private NumericUpDown numPrice => txtPrice;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridView1.CellClick += dataGridView1_CellContentClick;
            SetupDataGridView();
            LoadData();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM Toode", conn);
                DataTable table = new DataTable();
                adapter.Fill(table);

                // Выводим все имена столбцов для проверки
                foreach (DataColumn column in table.Columns)
                {
                    Console.WriteLine($"Column: {column.ColumnName}");
                }

                // Добавляем колонку для изображений, если её нет
                if (!table.Columns.Contains("pilt_image"))
                {
                    table.Columns.Add("pilt_image", typeof(Image));
                }

                foreach (DataRow row in table.Rows)
                {
                    string imagePath = row["pilt"]?.ToString(); // Используем безопасное обращение
                    if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                    {
                        using (var imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                        {
                            row["pilt_image"] = Image.FromStream(imgStream);
                        }
                    }
                    else
                    {
                        row["pilt_image"] = new Bitmap(100, 100); // Заглушка, если файла нет
                    }
                }

                dataGridView1.DataSource = table;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtName.Text) ||
                numQuantity.Value <= 0 ||  // Проверка на отрицательные значения
                numPrice.Value <= 0 ||
                string.IsNullOrWhiteSpace(txtCategory.Text) ||
                pictureBoxPreview.Image == null)
            {
                MessageBox.Show("Заполните все поля и убедитесь, что количество и цена больше 0!");
                return;
            }

            string imagesFolder = Path.Combine(Application.StartupPath, "images");
            if (!Directory.Exists(imagesFolder))
            {
                Directory.CreateDirectory(imagesFolder);
            }

            string fileName = Path.GetFileName(txtImagePath.Text);
            string newImagePath = Path.Combine(imagesFolder, fileName);
            if (!File.Exists(newImagePath))
            {
                File.Copy(txtImagePath.Text, newImagePath);
            }

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(
                    "INSERT INTO Toode (toodenimi, kogus, hind, pilt, kategooria) VALUES (@toodenimi, @kogus, @hind, @pilt, @kategooria)", conn);
                cmd.Parameters.AddWithValue("@toodenimi", txtName.Text);
                cmd.Parameters.AddWithValue("@kogus", (int)numQuantity.Value);
                cmd.Parameters.AddWithValue("@hind", (decimal)numPrice.Value);
                cmd.Parameters.AddWithValue("@pilt", newImagePath);
                cmd.Parameters.AddWithValue("@kategooria", txtCategory.Text);
                cmd.ExecuteNonQuery();
            }
            LoadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells["id"].Value);
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Toode WHERE id = @id", conn);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.ExecuteNonQuery();
                }
                LoadData();
            }
            else
            {
                MessageBox.Show("Выберите строку для удаления.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Проверяем, что кликнули не по заголовку
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];

                // Проверка на null для "pilt"
                var imagePath = row.Cells["pilt"].Value?.ToString(); // Получаем путь к изображению, если значение не null

                if (!string.IsNullOrEmpty(imagePath) && File.Exists(imagePath))
                {
                    using (var imgStream = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                    {
                        pictureBoxPreview.Image = Image.FromStream(imgStream);
                        pictureBoxPreview.SizeMode = PictureBoxSizeMode.Zoom;
                    }
                }
                else
                {
                    pictureBoxPreview.Image = new Bitmap(100, 100); // Заглушка, если файла нет или путь пустой
                    pictureBoxPreview.SizeMode = PictureBoxSizeMode.Zoom;
                }
            }
        }

        private void SetupDataGridView()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.Columns.Clear();

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "id",
                HeaderText = "ID",
                DataPropertyName = "id",
                Visible = false // Скрываем ID
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "toodenimi",
                HeaderText = "Название",
                DataPropertyName = "toodenimi"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "kogus",
                HeaderText = "Количество",
                DataPropertyName = "kogus"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "hind",
                HeaderText = "Цена",
                DataPropertyName = "hind"
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "pilt", // Здесь указываем имя столбца, которое будет отображаться в DataGridView
                HeaderText = "Изображение",
                DataPropertyName = "pilt" // Имя столбца в базе данных
            });

            dataGridView1.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "kategooria",
                HeaderText = "Категория",
                DataPropertyName = "kategooria"
            });

        }

        private void btnSelectImage_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Изображения (*.jpg;*.png;*.bmp)|*.jpg;*.png;*.bmp";
                openFileDialog.Title = "Выберите изображение товара";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    txtImagePath.Text = openFileDialog.FileName; // Сохраняем путь к файлу
                    pictureBoxPreview.Image = Image.FromFile(openFileDialog.FileName); // Показываем превью
                }
            }
        }

        private void buttonexit_Click(object sender, EventArgs e)
        {
            Start startForm = new Start();
            startForm.Show();
            this.Close(); 
        }
    }
}
