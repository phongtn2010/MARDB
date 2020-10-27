using DomainModel.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA0200025
{
    public class CHangHoa
    {
        public static long ThemHangHoa(long iID_MaHoSo, long iID_MaHangHoaNSW, int iID_MaNhom, int iID_MaPhanNhom, int iID_MaLoai, int iID_MaPhanLoai, int iID_MaDonViTinh, int iID_MaTrangThai,
            string sTenPhanNhom, string sTenLoaiHangHoa, string sTenPhanLoai,
            string sMaHoSo, string sTenHangHoa, string sMaSoCongNhan, string sHangSanXuat, string sMaQuocGia, string sTenQuocGia, string sBanChat, string sDonViTinh,
            string sThanhPhan, string sMauSac, string sSoHieu, string sQuyChuan,
            decimal rGiaVN, decimal rGiaUSD, string sHashCode,
            String sUserName, String sIP)
        {
            long vR = 0;

            try
            {
                String sThamSo = "";

                long iID_MaHangHoa = 0;
                Bang bang = new Bang("CNN25_HangHoa");
                bang.MaNguoiDungSua = sUserName;
                bang.IPSua = sIP;
                bang.DuLieuMoi = true;
                bang.CmdParams.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaHangHoaNSW", iID_MaHangHoaNSW);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaNhom", iID_MaNhom);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaPhanNhom", iID_MaPhanNhom);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaLoai", iID_MaLoai);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaPhanLoai", iID_MaPhanLoai);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaDonViTinh", iID_MaDonViTinh);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", iID_MaTrangThai);
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

                iID_MaHangHoa = Convert.ToInt64(bang.Save());

                vR = iID_MaHangHoa;
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static int ThemhangHoaChatLuong(long iID_MaHangHoa, string sChiTieu, string sHinhThuc, string sHamLuong, string sMaDonViTinh, string sDonViTinh, string sGhiChu, bool bChon,
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

        public static int ThemhangHoaAnToan(long iID_MaHangHoa, int iID_MaLoaiAnToan, string sChiTieu, string sHinhThuc, string sHamLuong, string sMaDonViTinh, string sDonViTinh, string sGhiChu, bool bChon,
            String sUserName, String sIP)
        {
            int vR = 0;

            try
            {
                Bang bang = new Bang("CNN25_HangHoa_AnToan");
                bang.MaNguoiDungSua = sUserName;
                bang.IPSua = sIP;
                bang.DuLieuMoi = true;
                bang.CmdParams.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaLoaiAnToan", iID_MaLoaiAnToan);
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
