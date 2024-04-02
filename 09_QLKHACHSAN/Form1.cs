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

namespace _09_QLKHACHSAN
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
           
        }
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataAdapter adapter = new SqlDataAdapter();
        DataTable dt = new DataTable();
        string chuoiketnoi = "Data Source=DESKTOP-GE1C4PK\\SQLEXPRESS;Initial Catalog=QLKhachSan;Integrated Security=True";
        
        void loadData()
        {
            cmd = conn.CreateCommand();
            cmd.CommandText = "select * from khachhang";
            adapter.SelectCommand = cmd;
            dt.Clear();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;

        }
        void runcommand(string query)
        {
            cmd = conn.CreateCommand();
            cmd.CommandText= query;
            cmd.ExecuteNonQuery();
            loadData();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            conn = new SqlConnection(chuoiketnoi);
            conn.Open();
            loadData();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string gioitinh;
            if (radioButton1.Checked)
            {
                gioitinh = "Nam";
            }
            else gioitinh = "Nu";

            string query = "insert into khachhang ([Họ tên] ,[Giới tính],[Loại phòng],[Số phòng thuê]  ) " +
                "values (@hoten, @gioitinh, @loaiphong, @sophongthue)";
            cmd = conn.CreateCommand();
            cmd.Parameters.Add("@hoten", tb_tenkhach.Text);
            cmd.Parameters.Add("@gioitinh", gioitinh);
            cmd.Parameters.Add("@loaiphong", cb_loaiphong.Text);
            cmd.Parameters.Add("@sophongthue", Convert.ToInt32(tb_sophongthue.Text));
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            loadData();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            
                int ma = Convert.ToInt32( dataGridView1.Rows[i].Cells[0].Value);
                cmd = conn.CreateCommand();

                string query = "delete from khachhang where [Mã KH] =  @ma";
                cmd.Parameters.Add("@ma", ma);

                cmd.CommandText = query;
                cmd.ExecuteNonQuery();
                loadData();
            

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            tb_tenkhach.Text = row.Cells[1].Value.ToString();
            string gioitinh = row.Cells[2].Value.ToString();
            if (gioitinh == "Nam") radioButton1.Checked = true;
            else radioButton2.Checked = true;
            cb_loaiphong.Text = row.Cells[3].Value.ToString();
            tb_sophongthue.Text =row.Cells[4].Value.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {

            string gioitinh;
            if (radioButton1.Checked)
            {
                gioitinh = "Nam";
            }
            else gioitinh = "Nu";
            int i = dataGridView1.CurrentRow.Index;

            int ma = Convert.ToInt32(dataGridView1.Rows[i].Cells[0].Value);
            cmd = conn.CreateCommand();
            string query = "update khachhang set [Họ tên] = @hoten , [Giới tính] = @gioitinh, [Loại phòng] = @loaiphong , [Số phòng thuê]  =  @sophongthue  where [Mã KH] =  @ma";
            cmd.Parameters.Add("@hoten", tb_tenkhach.Text);
            cmd.Parameters.Add("@gioitinh", gioitinh);
            cmd.Parameters.Add("@loaiphong",  cb_loaiphong.Text);
            cmd.Parameters.Add("@sophongthue",Convert.ToInt32(tb_sophongthue.Text));
            cmd.Parameters.Add("@ma", ma);

            cmd.CommandText = query;
            cmd.ExecuteNonQuery();
            loadData();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            cmd = conn.CreateCommand();
            string query = "select * from khachhang where [Họ tên] = @hoten";
            cmd.Parameters.AddWithValue("@hoten", tb_timkiemten.Text);
            cmd.CommandText = query;
            adapter.SelectCommand = cmd;
            dt.Clear();
            adapter.Fill(dt);
            dataGridView1.DataSource = dt;
        }
    }
}
