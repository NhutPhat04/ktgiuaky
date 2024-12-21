using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public class SinhVien
        {
            public string MaSV { get; set; }
            public string HoTen { get; set; }
            public DateTime NgaySinh { get; set; }
            public string Lop { get; set; }
        }
        List<SinhVien> danhSachSinhVien = new List<SinhVien>();
        private void CapNhatDataGridView()
        {
            dgvSinhVien.DataSource = null; // Làm mới nguồn dữ liệu
            dgvSinhVien.DataSource = danhSachSinhVien; // Gán lại nguồn dữ liệu
        }


        

        private void txtMaSV_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu hợp lệ
            if (string.IsNullOrEmpty(txtMaSV.Text) || string.IsNullOrEmpty(txtHotenSV.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo đối tượng sinh viên mới
            SinhVien sv = new SinhVien
            {
                MaSV = txtMaSV.Text,
                HoTen = txtHotenSV.Text,
                NgaySinh = dtNgaysinh.Value,
                Lop = cboLop.Text
            };

            // Thêm vào danh sách
            danhSachSinhVien.Add(sv);

            // Cập nhật lại DataGridView
            CapNhatDataGridView();

            // Thông báo
            MessageBox.Show("Thêm sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Xóa trắng các ô nhập
            ClearInputFields();
        }

        // Form Load
        

        private void btnSua_Click(object sender, EventArgs e)
        {
            // Lấy mã sinh viên cần sửa
            string maSV = txtMaSV.Text;

            // Tìm sinh viên trong danh sách
            SinhVien sv = danhSachSinhVien.FirstOrDefault(s => s.MaSV == maSV);

            if (sv != null)
            {
                // Cập nhật thông tin sinh viên
                sv.HoTen = txtHotenSV.Text;
                sv.NgaySinh = dtNgaysinh.Value;
                sv.Lop = cboLop.Text;

                // Cập nhật lại DataGridView
                CapNhatDataGridView();

                // Thông báo
                MessageBox.Show("Cập nhật thông tin sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Xóa trắng các ô nhập
                ClearInputFields();
            }
            else
            {
                MessageBox.Show("Không tìm thấy sinh viên với mã này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            // Lấy mã sinh viên cần xóa
            string maSV = txtMaSV.Text;

            // Tìm sinh viên trong danh sách
            SinhVien sv = danhSachSinhVien.FirstOrDefault(s => s.MaSV == maSV);

            if (sv != null)
            {
                // Hỏi xác nhận
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa sinh viên này?",
                    "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Xóa sinh viên
                    danhSachSinhVien.Remove(sv);

                    // Cập nhật lại DataGridView
                    CapNhatDataGridView();

                    // Thông báo
                    MessageBox.Show("Xóa sinh viên thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Xóa trắng các ô nhập
                    ClearInputFields();
                }
            }
            else
            {
                MessageBox.Show("Không tìm thấy sinh viên với mã này!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvSinhVien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy dòng hiện tại
                DataGridViewRow row = dgvSinhVien.Rows[e.RowIndex];

                // Gán giá trị từ DataGridView vào các TextBox
                txtMaSV.Text = row.Cells["MaSV"].Value.ToString();
                txtHotenSV.Text = row.Cells["HoTen"].Value.ToString();
                dtNgaysinh.Value = DateTime.Parse(row.Cells["NgaySinh"].Value.ToString());
                cboLop.Text = row.Cells["Lop"].Value.ToString();
            }
        }
        private void ClearInputFields()
        {
            txtMaSV.Clear();
            txtHotenSV.Clear();
            dtNgaysinh.Value = DateTime.Now;
            cboLop.SelectedIndex = -1;
        }

    }

}

