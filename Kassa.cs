using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace MyToode
{
    public partial class Kassa : Form
    {
        private string connectionString = "Data Source=(localdb)\\mssqllocaldb;Initial Catalog=Pood;Integrated Security=True";
        private string selectedProduct = "";

        private void button_AddToCheck_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(label_SelectedTovar.Text) && numericUpDown_Quantity.Value > 0)
            {
                // Используем только название товара, без цены
                string selectedItem = label_SelectedTovar.Text.Replace("Выбрано: ", "");
                listBox_Chek.Items.Add($"{selectedItem} x{numericUpDown_Quantity.Value}");
            }
        }

        private void button_RemoveFromCheck_Click(object sender, EventArgs e)
        {
            if (listBox_Chek.SelectedItem != null)
            {
                listBox_Chek.Items.Remove(listBox_Chek.SelectedItem);
            }
        }

        private void button_Buy_Click(object sender, EventArgs e)
        {
            if (listBox_Chek.Items.Count == 0)
            {
                MessageBox.Show("Чек пуст!");
                return;
            }

            string receiptPath = Path.Combine(Application.StartupPath, "Arved");
            if (!Directory.Exists(receiptPath))
                Directory.CreateDirectory(receiptPath);

            string fileName = Path.Combine(receiptPath, "Чек_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt");

            bool isPurchaseSuccessful = false; // Флаг успешности покупки

            using (StreamWriter writer = new StreamWriter(fileName, false))
            {
                writer.WriteLine("Чек покупки");
                writer.WriteLine("Дата: " + DateTime.Now);
                writer.WriteLine("-------------------------------");

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction();

                    try
                    {
                        foreach (var item in listBox_Chek.Items)
                        {
                            string[] parts = item.ToString().Split('x'); // Разбиваем строку "Название xКол-во"
                            if (parts.Length < 2) continue;

                            string toodeNimi = parts[0].Trim(); // Извлекаем только название товара
                            int boughtQuantity = int.Parse(parts[1].Trim()); // Количество товара

                            // Получаем текущее количество товара
                            SqlCommand checkCmd = new SqlCommand("SELECT kogus FROM Toode WHERE toodenimi = @name COLLATE SQL_Latin1_General_CP1_CI_AS", conn, transaction);
                            checkCmd.Parameters.AddWithValue("@name", toodeNimi); // Используем только название товара

                            object result = checkCmd.ExecuteScalar();

                            // Проверяем, что результат не равен null
                            if (result != null)
                            {
                                int currentStock = Convert.ToInt32(result); // Преобразуем в int

                                // Проверяем, достаточно ли товара
                                if (currentStock >= boughtQuantity)
                                {
                                    // Обновляем количество товара в базе
                                    SqlCommand cmd = new SqlCommand("UPDATE Toode SET kogus = kogus - @bought WHERE toodenimi = @name", conn, transaction);
                                    cmd.Parameters.AddWithValue("@bought", boughtQuantity);
                                    cmd.Parameters.AddWithValue("@name", toodeNimi);
                                    int rowsAffected = cmd.ExecuteNonQuery();

                                    if (rowsAffected > 0)
                                    {
                                        writer.WriteLine(item.ToString()); // Записываем в чек
                                        isPurchaseSuccessful = true; // Устанавливаем флаг успешности
                                    }
                                    else
                                    {
                                        MessageBox.Show($"Не удалось обновить количество товара '{toodeNimi}'. Возможно, его недостаточно на складе.");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show($"Товара '{toodeNimi}' недостаточно на складе.");
                                }
                            }
                            else
                            {
                                MessageBox.Show($"Товар '{toodeNimi}' не найден в базе данных.");
                            }
                        }

                        if (isPurchaseSuccessful)
                        {
                            transaction.Commit(); // Если покупка успешна, подтверждаем транзакцию
                            writer.WriteLine("-------------------------------");
                            writer.WriteLine("Спасибо за покупку!");
                            MessageBox.Show("Покупка оформлена! Чек сохранён.");
                        }
                        else
                        {
                            transaction.Rollback(); // Если покупки не было, откатываем транзакцию
                        }
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        MessageBox.Show("Ошибка при оформлении покупки: " + ex.Message);
                    }
                }
            }

            // Очищаем список покупок
            listBox_Chek.Items.Clear();

            // Обновляем отображение товаров
            flowLayoutPanel_Tovary.Controls.Clear();
            LoadTovary();
        }

        private void button_Cancel_Click(object sender, EventArgs e)
        {
            listBox_Chek.Items.Clear();
        }

        public Kassa()
        {
            InitializeComponent();
            LoadTovary();
        }


        private void LoadTovary()
        {
            flowLayoutPanel_Tovary.Controls.Clear(); // Чистим список товаров перед загрузкой

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM Toode", conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string name = reader["toodenimi"].ToString();
                    decimal price = Convert.ToDecimal(reader["hind"]);
                    int stock = Convert.ToInt32(reader["kogus"]);
                    string imagePath = reader["pilt"].ToString();

                    Panel panel = new Panel()
                    {
                        Size = new Size(150, 220),
                        Margin = new Padding(5)
                    };

                    PictureBox pictureBox = new PictureBox()
                    {
                        Size = new Size(140, 140),
                        Image = File.Exists(imagePath) ? Image.FromFile(imagePath) : new Bitmap(140, 140),
                        SizeMode = PictureBoxSizeMode.Zoom
                    };

                    Label label = new Label()
                    {
                        Text = $"{name}\nЦена: {price}€\nОстаток: {stock}",
                        AutoSize = false,
                        Size = new Size(140, 40),
                        TextAlign = ContentAlignment.MiddleCenter
                    };

                    Button buyButton = new Button()
                    {
                        Text = "Выбрать",
                        Size = new Size(140, 30),
                        Tag = new { name, price, stock } // Храним данные о товаре в Tag
                    };

                    // Исправленный обработчик
                    buyButton.Click += (s, e) =>
                    {
                        // Извлекаем информацию из Tag
                        var productData = (dynamic)((Button)s).Tag;
                        string productName = productData.name;
                        decimal productPrice = productData.price;
                        int productStock = productData.stock;

                        // Обновляем выбранный товар
                        selectedProduct = productName; // Сохраняем только название товара
                        label_SelectedTovar.Text = $"Выбрано: {productName}"; // Обновляем Label с только названием товара
                    };

                    panel.Controls.Add(pictureBox);
                    panel.Controls.Add(label);
                    panel.Controls.Add(buyButton);

                    pictureBox.Location = new Point(5, 5);
                    label.Location = new Point(5, 150);
                    buyButton.Location = new Point(5, 190);

                    flowLayoutPanel_Tovary.Controls.Add(panel);
                }
            }
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Start startForm = new Start();
            startForm.Show();
            this.Close(); 
        }
    }

}
