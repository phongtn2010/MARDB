using DomainModel;
using DomainModel.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA0200025
{
    public class CHangHoa
    {
        public static long ThemHangHoa(long iID_MaHoSo, long iID_MaHangHoaNSW, int iID_MaNhom, string iID_MaPhanNhom, string iID_MaLoai, string iID_MaPhanLoai, string iID_MaDonViTinh, int iID_MaTrangThai,
            string sTenPhanNhom, string sTenLoaiHangHoa, string sTenPhanLoai,
            string sMaHoSo, string sTenHangHoa, string sMaSoCongNhan, string sHangSanXuat, string sMaQuocGia, string sTenQuocGia, string sBanChat, string sDonViTinh,
            string sThanhPhan, string sMauSac, string sSoHieu, string sQuyChuan,
            decimal rGiaVN, decimal rGiaUSD, string sHashCode,
            String sUserName, String sIP, long iID_MaHangHoa_Sua = 0)
        {
            long vR = 0;

            try
            {
                String sThamSo = "";

                String sTenNhom = clDanhMuc.Get_Name_DanhMuc("NHOMTACN", iID_MaNhom.ToString());

                long iID_MaHangHoa = 0;
                Bang bang = new Bang("CNN25_HangHoa");
                bang.MaNguoiDungSua = sUserName;
                bang.IPSua = sIP;
                
                bang.CmdParams.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaHangHoaNSW", iID_MaHangHoaNSW);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaNhom", iID_MaNhom);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaPhanNhom", iID_MaPhanNhom);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaLoai", iID_MaLoai);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaPhanLoai", iID_MaPhanLoai);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaDonViTinh", iID_MaDonViTinh);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", iID_MaTrangThai);
                bang.CmdParams.Parameters.AddWithValue("@sTenNhom", sTenNhom);
                bang.CmdParams.Parameters.AddWithValue("@sTenPhanNhom", sTenPhanNhom);
                bang.CmdParams.Parameters.AddWithValue("@sTenLoaiHangHoa", sTenLoaiHangHoa);
                bang.CmdParams.Parameters.AddWithValue("@sTenPhanLoai", sTenPhanLoai);
                bang.CmdParams.Parameters.AddWithValue("@sMaHoSo", sMaHoSo);
                bang.CmdParams.Parameters.AddWithValue("@sTenHangHoa", sTenHangHoa);
                bang.CmdParams.Parameters.AddWithValue("@sMaSoCongNhan", sMaSoCongNhan);
                bang.CmdParams.Parameters.AddWithValue("@sHangSanXuat", sHangSanXuat);
                bang.CmdParams.Parameters.AddWithValue("@sMaQuocGia", sMaQuocGia);
                bang.CmdParams.Parameters.AddWithValue("@sTenQuocGia", sTenQuocGia);
                bang.CmdParams.Parameters.AddWithValue("@sBanChat", sBanChat);
                bang.CmdParams.Parameters.AddWithValue("@sThanhPhan", sThanhPhan);
                bang.CmdParams.Parameters.AddWithValue("@sMauSac", sMauSac);
                bang.CmdParams.Parameters.AddWithValue("@sSoHieu", sSoHieu);
                bang.CmdParams.Parameters.AddWithValue("@sQuyChuan", sQuyChuan);
                bang.CmdParams.Parameters.AddWithValue("@rGiaVN", rGiaVN);
                bang.CmdParams.Parameters.AddWithValue("@rGiaUSD", rGiaUSD);
                bang.CmdParams.Parameters.AddWithValue("@sDonViTinh", sDonViTinh);
                bang.CmdParams.Parameters.AddWithValue("@sHashCode", sHashCode);

                
                if (iID_MaHangHoa_Sua > 0)
                {
                    bang.DuLieuMoi = false;
                    bang.GiaTriKhoa = iID_MaHangHoa_Sua;
                    bang.Save();

                    iID_MaHangHoa = iID_MaHangHoa_Sua;
                }
                else
                {
                    bang.CmdParams.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoaNSW);
                    bang.DuLieuMoi = true;
                    bang.Save();

                    iID_MaHangHoa = iID_MaHangHoaNSW;
                }

                vR = iID_MaHangHoa;
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static DataTable Get_HangHoa_Detail_Nsw(long iID_MaHoSo, long iID_MaHangHoaNSW)
        {
            DataTable vR = null;

            string SQL = "SELECT TOP 1 * FROM CNN25_HangHoa WHERE iID_MaHoSo=@iID_MaHoSo AND iID_MaHangHoaNSW=@iID_MaHangHoaNSW";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
            cmd.Parameters.AddWithValue("@iID_MaHangHoaNSW", iID_MaHangHoaNSW);
            vR = Connection.GetDataTable(cmd);
            cmd.Dispose();

            return vR;
        }

        public static int UpDate_TrangThai(long iID_MaHangHoaNSW, int iTrangThai)
        {
            int vR = 0;
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("UPDATE CNN25_HangHoa SET iID_MaTrangThai=@iID_MaTrangThai WHERE iID_MaHangHoaNSW=@iID_MaHangHoaNSW");
                cmd.Parameters.AddWithValue("@iID_MaTrangThai", iTrangThai);
                cmd.Parameters.AddWithValue("@iID_MaHangHoaNSW", iID_MaHangHoaNSW);
                Connection.UpdateDatabase(cmd);

                vR = 1;
            }
            catch(Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static int UpDate_PhanLoai(long iID_MaHangHoa, string iID_MaPhanLoai)
        {
            int vR = 0;
            try
            {
                String sTenPhanLoai = clDanhMuc.Get_Name_DanhMuc("PHANLOAITACN", iID_MaPhanLoai.ToString());

                SqlCommand cmd;
                cmd = new SqlCommand("UPDATE CNN25_HangHoa SET iID_MaPhanLoai=@iID_MaPhanLoai, sTenPhanLoai=@sTenPhanLoai WHERE iID_MaHangHoa=@iID_MaHangHoa");
                cmd.Parameters.AddWithValue("@iID_MaPhanLoai", iID_MaPhanLoai);
                cmd.Parameters.AddWithValue("@sTenPhanLoai", sTenPhanLoai);
                cmd.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
                Connection.UpdateDatabase(cmd);

                vR = 1;
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static int UpDate_TrangThai_TheoHoSo(long iID_MaHoSo, int iTrangThai)
        {
            int vR = 0;
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("UPDATE CNN25_HangHoa SET iID_MaTrangThai=@iID_MaTrangThai WHERE iID_MaHoSo=@iID_MaHoSo");
                cmd.Parameters.AddWithValue("@iID_MaTrangThai", iTrangThai);
                cmd.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
                Connection.UpdateDatabase(cmd);

                vR = 1;
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static void Delete_HangHoa_ThongTin(long iID_MaHangHoa)
        {
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("DELETE FROM CNN25_HangHoa_ChatLuong WHERE iID_MaHangHoa=@iID_MaHangHoa");
                cmd.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
                Connection.UpdateDatabase(cmd);

                cmd = new SqlCommand("DELETE FROM CNN25_HangHoa_AnToan WHERE iID_MaHangHoa=@iID_MaHangHoa");
                cmd.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
                Connection.UpdateDatabase(cmd);

                cmd = new SqlCommand("DELETE FROM CNN25_HangHoa_SoLuong WHERE iID_MaHangHoa=@iID_MaHangHoa");
                cmd.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
                Connection.UpdateDatabase(cmd);
            }
            catch(Exception ex)
            {

            }
        }

        public static int ThemhangHoaChatLuong(long iID_MaHangHoa, int iID_MaHinhThuc, string sChiTieu, string sHinhThuc, string sHamLuong, string sMaDonViTinh, string sDonViTinh, string sGhiChu, bool bChon,
            String sUserName, String sIP)
        {
            int vR = 0;

            try
            {
                Bang bang = new Bang("CNN25_HangHoa_ChatLuong");
                bang.MaNguoiDungSua = sUserName;
                bang.IPSua = sIP;
                bang.DuLieuMoi = true;
                bang.CmdParams.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaHinhThuc", iID_MaHinhThuc);
                bang.CmdParams.Parameters.AddWithValue("@sChiTieu", sChiTieu);
                bang.CmdParams.Parameters.AddWithValue("@sHinhThuc", sHinhThuc);
                bang.CmdParams.Parameters.AddWithValue("@sHamLuong", sHamLuong);
                bang.CmdParams.Parameters.AddWithValue("@sMaDonViTinh", sMaDonViTinh);
                bang.CmdParams.Parameters.AddWithValue("@sDonViTinh", sDonViTinh);
                bang.CmdParams.Parameters.AddWithValue("@sGhiChu", sGhiChu);
                bang.CmdParams.Parameters.AddWithValue("@bChon", bChon);

                bang.Save();

                vR = 1;
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static DataTable Get_HangHoa_AnToan_KyThuat(long iID_MaHangHoa)
        {
            DataTable vR;

            string SQL = "SELECT * FROM CNN25_HangHoa_AnToan WHERE iID_MaHangHoa=@iID_MaHangHoa AND iID_MaLoaiAnToan=2";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
            vR = Connection.GetDataTable(cmd);
            cmd.Dispose();

            return vR;
        }

        public static void Delete_HangHoa_AnToan_KyThuat(long iID_MaHangHoa, int iID_MaHinhThuc)
        {
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("DELETE FROM CNN25_HangHoa_AnToan WHERE iID_MaHangHoa=@iID_MaHangHoa AND iID_MaHinhThuc=@iID_MaHinhThuc AND iID_MaLoaiAnToan=2");
                cmd.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
                cmd.Parameters.AddWithValue("@iID_MaHinhThuc", iID_MaHinhThuc);
                Connection.UpdateDatabase(cmd);
                cmd.Dispose();
            }
            catch (Exception ex)
            {

            }
        }

        public static int ThemhangHoaAnToan(long iID_MaHangHoa, int iID_MaLoaiAnToan, int iID_MaHinhThuc, string sChiTieu, string sHinhThuc, string sHamLuong, string sMaDonViTinh, string sDonViTinh, string sGhiChu, bool bChon,
            String sUserName, String sIP, int iID_MahangHoaATKT = 0)
        {
            int vR = 0;

            try
            {
                Bang bang = new Bang("CNN25_HangHoa_AnToan");
                bang.MaNguoiDungSua = sUserName;
                bang.IPSua = sIP;
                bang.DuLieuMoi = true;
                bang.CmdParams.Parameters.AddWithValue("@iID_MahangHoaATKT", iID_MahangHoaATKT);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaLoaiAnToan", iID_MaLoaiAnToan);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaHinhThuc", iID_MaHinhThuc);
                bang.CmdParams.Parameters.AddWithValue("@sChiTieu", sChiTieu);
                bang.CmdParams.Parameters.AddWithValue("@sHinhThuc", sHinhThuc);
                bang.CmdParams.Parameters.AddWithValue("@sHamLuong", sHamLuong);
                bang.CmdParams.Parameters.AddWithValue("@sMaDonViTinh", sMaDonViTinh);
                bang.CmdParams.Parameters.AddWithValue("@sDonViTinh", sDonViTinh);
                bang.CmdParams.Parameters.AddWithValue("@sGhiChu", sGhiChu);
                bang.CmdParams.Parameters.AddWithValue("@bChon", bChon);

                bang.Save();

                vR = 1;
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static int ThemhangHoaSoLuong(long iID_MaHangHoa, decimal rKhoiLuong, string sMaDonViTinhKL, string sDonViTinhKL, decimal rKhoiLuongTan, decimal rSoLuong, string sMaDonViTinhSL, string sDonViTinhSL, string sGhiChu, bool bChon,
            String sUserName, String sIP)
        {
            int vR = 0;

            try
            {
                Bang bang = new Bang("CNN25_HangHoa_SoLuong");
                bang.MaNguoiDungSua = sUserName;
                bang.IPSua = sIP;
                bang.DuLieuMoi = true;
                bang.CmdParams.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
                bang.CmdParams.Parameters.AddWithValue("@rKhoiLuong", rKhoiLuong);
                bang.CmdParams.Parameters.AddWithValue("@sMaDonViTinhKL", sMaDonViTinhKL);
                bang.CmdParams.Parameters.AddWithValue("@sDonViTinhKL", sDonViTinhKL);
                bang.CmdParams.Parameters.AddWithValue("@rKhoiLuongTan", rKhoiLuongTan);
                bang.CmdParams.Parameters.AddWithValue("@rSoLuong", rSoLuong);
                bang.CmdParams.Parameters.AddWithValue("@sMaDonViTinhSL", sMaDonViTinhSL);
                bang.CmdParams.Parameters.AddWithValue("@sDonViTinhSL", sDonViTinhSL);
                bang.CmdParams.Parameters.AddWithValue("@sGhiChu", sGhiChu);
                bang.CmdParams.Parameters.AddWithValue("@bChon", bChon);

                bang.Save();

                vR = 1;
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }
    }
}
