﻿using DomainModel.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATA0200025.DTO;
using DATA0200025.Models;
using System.Data.SqlClient;
using DomainModel;
using System.Data;
using DATA0200025.WebServices.XmlType.Request;
using DATA0200025.WebServices;
using DomainModel.Controls;

namespace DATA0200025
{
    public class CHoSo
    {
        public static long ThemHoSo(long iID_MaHoSo_Sua, int iID_MaTrangThai, int iID_MaLoaiHoSo, long iID_MaHoSo_ThayThe,
            string sMaHoSo, string sMaHoSo_ThayThe, string sSoXacNhan_ThayThe, string sMaDinhKem_ThayThe, string sTenDinhKem_ThayThe, string sLinkDinhKem_ThayThe,
            string sCoQuanXuLy_Ma, string sCoQuanXuLy_Ten,
            DateTime dNgayTaoHoSo, bool bDaTiepNhan, string sSoTiepNhan, DateTime? dNgayTiepNhan,
            string sSoGDK, string sSoGDK_ThayThe, DateTime? dNgayXacNhan,
            string sMaSoThue, string sTenDoanhNghiep, string sLoaiHinhThucKiemTra, string sDonViThucHienDanhGia, string sTenHangHoa,
            string sBan_Name, string sBan_DiaChi, string sBan_Tel, string sBan_Fax, string sBan_Email, string sBan_MaQuocGia, string sBan_QuocGia, string sBan_NoiXuat,
            string sMua_Name, string sMua_DiaChi, string sMua_Tel, string sMua_Fax, string sMua_Email, string sMua_NoiNhan, DateTime? sMua_FromDate, DateTime? sMua_ToDate,
            string sDiaDiemTapKet, string sDiaDiemDangKy, DateTime dDangKy_FromDate, DateTime dDangKy_ToDate,
            string sLienHe_Name, string sLieHe_DiaChi, string sLienHe_Tel, string sLienHe_Email,
            string sKyHoSo_MaTinh, string sKyHoSo_Tinh, string sKyHoSo_NguoiKy, string sKyHoSo_NguoiKy_ChucDanh,
            String sUserName, String sIP)
        {
            long vR = 0;

            try
            {
                String sHashCode = "";

                long iID_MaHoSo = 0;
                Bang bang = new Bang("CNN25_HoSo");
                bang.MaNguoiDungSua = sUserName;
                bang.IPSua = sIP;

                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", iID_MaTrangThai);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaLoaiHoSo", iID_MaLoaiHoSo);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaHoSo_ThayThe", iID_MaHoSo_ThayThe);
                bang.CmdParams.Parameters.AddWithValue("@sMaHoSo", sMaHoSo);
                bang.CmdParams.Parameters.AddWithValue("@sMaHoSo_ThayThe", sMaHoSo_ThayThe);
                bang.CmdParams.Parameters.AddWithValue("@sSoXacNhan_ThayThe", sSoXacNhan_ThayThe);
                bang.CmdParams.Parameters.AddWithValue("@sMaDinhKem_ThayThe", sMaDinhKem_ThayThe);
                bang.CmdParams.Parameters.AddWithValue("@sTenDinhKem_ThayThe", sTenDinhKem_ThayThe);
                bang.CmdParams.Parameters.AddWithValue("@sLinkDinhKem_ThayThe", sLinkDinhKem_ThayThe);
                bang.CmdParams.Parameters.AddWithValue("@sCoQuanXuLy_Ma", sCoQuanXuLy_Ma);
                bang.CmdParams.Parameters.AddWithValue("@sCoQuanXuLy_Ten", sCoQuanXuLy_Ten);
                if (dNgayTaoHoSo != null)
                {
                    bang.CmdParams.Parameters.AddWithValue("@dNgayTaoHoSo", dNgayTaoHoSo);
                }
                bang.CmdParams.Parameters.AddWithValue("@bDaTiepNhan", bDaTiepNhan);
                bang.CmdParams.Parameters.AddWithValue("@sSoTiepNhan", sSoTiepNhan);
                if (dNgayTiepNhan != null)
                {
                    bang.CmdParams.Parameters.AddWithValue("@dNgayTiepNhan", dNgayTiepNhan);
                }
                bang.CmdParams.Parameters.AddWithValue("@sSoGDK", sSoGDK);
                bang.CmdParams.Parameters.AddWithValue("@sSoGDK_ThayThe", sSoGDK_ThayThe);
                if (dNgayXacNhan != null)
                {
                    bang.CmdParams.Parameters.AddWithValue("@dNgayXacNhan", dNgayXacNhan);
                }
                bang.CmdParams.Parameters.AddWithValue("@sMaSoThue", sMaSoThue);
                bang.CmdParams.Parameters.AddWithValue("@sTenDoanhNghiep", sTenDoanhNghiep);
                bang.CmdParams.Parameters.AddWithValue("@sLoaiHinhThucKiemTra", sLoaiHinhThucKiemTra);
                bang.CmdParams.Parameters.AddWithValue("@sDonViThucHienDanhGia", sDonViThucHienDanhGia);
                bang.CmdParams.Parameters.AddWithValue("@sTenTACN", sTenHangHoa);
                bang.CmdParams.Parameters.AddWithValue("@sBan_Name", sBan_Name);
                bang.CmdParams.Parameters.AddWithValue("@sBan_DiaChi", sBan_DiaChi);
                bang.CmdParams.Parameters.AddWithValue("@sBan_Tel", sBan_Tel);
                bang.CmdParams.Parameters.AddWithValue("@sBan_Fax", sBan_Fax);
                bang.CmdParams.Parameters.AddWithValue("@sBan_Email", sBan_Email);
                bang.CmdParams.Parameters.AddWithValue("@sBan_MaQuocGia", sBan_MaQuocGia);
                bang.CmdParams.Parameters.AddWithValue("@sBan_QuocGia", sBan_QuocGia);
                bang.CmdParams.Parameters.AddWithValue("@sBan_NoiXuat", sBan_NoiXuat);
                bang.CmdParams.Parameters.AddWithValue("@sMua_Name", sMua_Name);
                bang.CmdParams.Parameters.AddWithValue("@sMua_DiaChi", sMua_DiaChi);
                bang.CmdParams.Parameters.AddWithValue("@sMua_Tel", sMua_Tel);
                bang.CmdParams.Parameters.AddWithValue("@sMua_Fax", sMua_Fax);
                bang.CmdParams.Parameters.AddWithValue("@sMua_Email", sMua_Email);
                bang.CmdParams.Parameters.AddWithValue("@sMua_NoiNhan", sMua_NoiNhan);
                if (sMua_FromDate != null)
                {
                    bang.CmdParams.Parameters.AddWithValue("@sMua_FromDate", sMua_FromDate);
                }
                if (sMua_ToDate != null)
                {
                    bang.CmdParams.Parameters.AddWithValue("@sMua_ToDate", sMua_ToDate);
                }
                bang.CmdParams.Parameters.AddWithValue("@sDiaDiemTapKet", sDiaDiemTapKet);
                bang.CmdParams.Parameters.AddWithValue("@sDiaDiemDangKy", sDiaDiemDangKy);
                if (dDangKy_FromDate != null)
                {
                    if (dDangKy_FromDate.Year > 2000)
                    {
                        bang.CmdParams.Parameters.AddWithValue("@dDangKy_FromDate", dDangKy_FromDate);
                    }
                }
                if (dDangKy_ToDate != null)
                {
                    if (dDangKy_ToDate.Year > 2000)
                    {
                        bang.CmdParams.Parameters.AddWithValue("@dDangKy_ToDate", dDangKy_ToDate);
                    }
                }
                bang.CmdParams.Parameters.AddWithValue("@sLienHe_Name", sLienHe_Name);
                bang.CmdParams.Parameters.AddWithValue("@sLieHe_DiaChi", sLieHe_DiaChi);
                bang.CmdParams.Parameters.AddWithValue("@sLienHe_Tel", sLienHe_Tel);
                bang.CmdParams.Parameters.AddWithValue("@sLienHe_Email", sLienHe_Email);
                bang.CmdParams.Parameters.AddWithValue("@sKyHoSo_MaTinh", sKyHoSo_MaTinh);
                bang.CmdParams.Parameters.AddWithValue("@sKyHoSo_Tinh", sKyHoSo_Tinh);
                bang.CmdParams.Parameters.AddWithValue("@sKyHoSo_NguoiKy", sKyHoSo_NguoiKy);
                bang.CmdParams.Parameters.AddWithValue("@sKyHoSo_NguoiKy_ChucDanh", sKyHoSo_NguoiKy_ChucDanh);
                if (dNgayTaoHoSo != null)
                {
                    bang.CmdParams.Parameters.AddWithValue("@dKyHoSo_NgayKy", dNgayTaoHoSo);
                }
                bang.CmdParams.Parameters.AddWithValue("@sHashCode", sHashCode);

                if (iID_MaHoSo_Sua > 0)
                {
                    bang.DuLieuMoi = false;
                    bang.GiaTriKhoa = iID_MaHoSo_Sua;
                    bang.Save();

                    iID_MaHoSo = iID_MaHoSo_Sua;
                }
                else
                {
                    bang.DuLieuMoi = true;
                    iID_MaHoSo = Convert.ToInt64(bang.Save());
                }

                vR = iID_MaHoSo;
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static HoSoModels Get_HoSo_ChiTiet(string sMaHoSo)
        {
            HoSoModels objData = null;



            return objData;
        }

        public static void Delete_HoSo(long iID_MaHoSo)
        {
            try
            {
                SqlCommand cmd;

                cmd = new SqlCommand("DELETE FROM CNN25_HoSo WHERE iID_MaHoSo=@iID_MaHoSo");
                cmd.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
                Connection.UpdateDatabase(cmd);
                cmd.Dispose();
            }
            catch (Exception ex)
            {

            }
        }

        public static int UpDate_TrangThai(long iID_MaHoSo, int iTrangThai)
        {
            int vR = 0;
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("UPDATE CNN25_HoSo SET iID_MaTrangThai=@iID_MaTrangThai WHERE iID_MaHoSo=@iID_MaHoSo");
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


        public static int UpDate_DanhGia(long iID_MaHoSo, string sDonViThucHienDanhGia, string sThoiGianDanhGia)
        {
            int vR = 0;
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("UPDATE CNN25_HoSo SET sDonViThucHienDanhGia=@sDonViThucHienDanhGia, sThoiGianDanhGia=@sThoiGianDanhGia WHERE iID_MaHoSo=@iID_MaHoSo");
                cmd.Parameters.AddWithValue("@sDonViThucHienDanhGia", sDonViThucHienDanhGia);
                cmd.Parameters.AddWithValue("@sThoiGianDanhGia", sThoiGianDanhGia);
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

        public static int UpDate_QuaHan(long iID_MaHoSo)
        {
            int vR = 0;
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("UPDATE CNN25_HoSo SET bPublic=1 WHERE iID_MaHoSo=@iID_MaHoSo");
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

        public static int XuLy_TuDong_HoSo_2D(long iID_MaHoSo, string sMaHoSo, string sNguoiXuLy, string sUserName, string sIP)
        {
            int vR = 0;

            SendService _sendService = new SendService();

            DaTiepNhanHoSo2d resultConfirm = new DaTiepNhanHoSo2d();
            resultConfirm.NSWFileCode = sMaHoSo;
            resultConfirm.ResponseDateString = DateTime.Now;
            string error = _sendService.DaTiepNhanHoSo2d(sMaHoSo, resultConfirm);
            
            if (error == "99")
            {
                Bang bang = new Bang("CNN25_HoSo");
                bang.MaNguoiDungSua = sUserName;
                bang.IPSua = sIP;
                bang.DuLieuMoi = false;
                bang.GiaTriKhoa = iID_MaHoSo;
                bang.CmdParams.Parameters.AddWithValue("@sSoTiepNhan", clTaoMaTiepNhan.GetSoTiepNhan());
                bang.CmdParams.Parameters.AddWithValue("@sUserTiepNhan", sUserName);
                bang.CmdParams.Parameters.AddWithValue("@dNgayTiepNhan", DateTime.Now);
                bang.CmdParams.Parameters.AddWithValue("@sTenNguoiTiepNhan", CPQ_NGUOIDUNG.Get_TenNguoiDung(sUserName));
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", 4);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", 1);
                bang.Save();

                clLichSuHoSo.InsertLichSu(iID_MaHoSo, sUserName, (int)clDoiTuong.DoiTuong.HeThongBNN, (int)clHanhDong.HanhDong.TiepNhanHoSo, "", "", 1, 4);

                vR = 1;
            }

            return vR;
        }

        public static long ThemHoSoTCCD(long iID_MaHoSo_TCCD_Sua, long iID_MaHoSo, string iID_MaHangHoa, string iID_MaToChuc, string sMaHoSo, string sTenToChuc,
            String sUserName, String sIP)
        {
            long vR = 0;

            try
            {
                long iID_MaHoSoXNCL = 0;
                Bang bang = new Bang("CNN25_HoSo_TCCD");
                bang.MaNguoiDungSua = sUserName;
                bang.IPSua = sIP;

                bang.CmdParams.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
                bang.CmdParams.Parameters.AddWithValue("@sMaHangHoa", iID_MaHangHoa);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaToChuc", iID_MaToChuc);
                bang.CmdParams.Parameters.AddWithValue("@sMaHoSo", sMaHoSo);
                bang.CmdParams.Parameters.AddWithValue("@sTenToChuc", sTenToChuc);

                if (iID_MaHoSo_TCCD_Sua > 0)
                {
                    bang.DuLieuMoi = false;
                    bang.GiaTriKhoa = iID_MaHoSo_TCCD_Sua;
                    bang.Save();

                    iID_MaHoSoXNCL = iID_MaHoSo_TCCD_Sua;
                }
                else
                {
                    bang.DuLieuMoi = true;
                    iID_MaHoSoXNCL = Convert.ToInt64(bang.Save());
                }

                vR = iID_MaHoSoXNCL;
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static DataTable Get_HoSo_TCCD(long iID_MaHoSo)
        {
            DataTable vR = null;

            string SQL = "SELECT TOP 1 * FROM CNN25_HoSo_TCCD WHERE iID_MaHoSo=@iID_MaHoSo ORDER BY dNgayTao DESC";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
            vR = Connection.GetDataTable(cmd);
            cmd.Dispose();

            return vR;
        }

        public static long KiemTra_HoSo_TCCD(long iID_MaHoSo, string sMaHangHoa, string iID_MaToChuc)
        {
            long vR = 0;

            string SQL = "SELECT * FROM CNN25_HoSo_TCCD WHERE iID_MaHoSo=@iID_MaHoSo AND sMaHangHoa=@sMaHangHoa AND iID_MaToChuc=@iID_MaToChuc ORDER BY dNgayTao DESC";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
            cmd.Parameters.AddWithValue("@sMaHangHoa", sMaHangHoa);
            cmd.Parameters.AddWithValue("@iID_MaToChuc", iID_MaToChuc);
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();

            if(dt.Rows.Count > 0)
            {
                vR = Convert.ToInt64(dt.Rows[0]["iID_MaHoSoTCCD"]);
            }

            return vR;
        }

        public static long ThemHoSoXNCL(long iID_MaHoSo_XNCL_Sua, int iID_MaTrangThai, long iID_MaHoSo, long iID_MaHangHoa, string iID_MaToChuc,
            string sMaHoSo, string sTenHangHoa, string sTenToChuc, string sGiayChungNhan, DateTime dNgayCap,
            int iKetQua, string sMaFileChungNhan, string sTenFileChungNhan, string sLinkFileChungNhan,
            String sUserName, String sIP)
        {
            long vR = 0;

            try
            {
                long iID_MaHoSoXNCL = 0;
                Bang bang = new Bang("CNN25_HoSo_XNCL");
                bang.MaNguoiDungSua = sUserName;
                bang.IPSua = sIP;

                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", iID_MaTrangThai);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaToChuc", iID_MaToChuc);
                bang.CmdParams.Parameters.AddWithValue("@sMaHoSo", sMaHoSo);
                bang.CmdParams.Parameters.AddWithValue("@sTenHangHoa", sTenHangHoa);
                bang.CmdParams.Parameters.AddWithValue("@sTenToChuc", sTenToChuc);
                bang.CmdParams.Parameters.AddWithValue("@sGiayChungNhan", sGiayChungNhan);
                if (dNgayCap != null)
                {
                    if (dNgayCap.Year > 2000)
                    {
                        bang.CmdParams.Parameters.AddWithValue("@dNgayCap", dNgayCap);
                    }
                }
                bang.CmdParams.Parameters.AddWithValue("@iKetQua", iKetQua);
                bang.CmdParams.Parameters.AddWithValue("@sMaFileChungNhan", sMaFileChungNhan);
                bang.CmdParams.Parameters.AddWithValue("@sTenFileChungNhan", sTenFileChungNhan);
                bang.CmdParams.Parameters.AddWithValue("@sLinkFileChungNhan", sLinkFileChungNhan);

                if (iID_MaHoSo_XNCL_Sua > 0)
                {
                    bang.DuLieuMoi = false;
                    bang.GiaTriKhoa = iID_MaHoSo_XNCL_Sua;
                    bang.Save();

                    iID_MaHoSoXNCL = iID_MaHoSo_XNCL_Sua;
                }
                else
                {
                    bang.DuLieuMoi = true;
                    iID_MaHoSoXNCL = Convert.ToInt64(bang.Save());
                }

                vR = iID_MaHoSoXNCL;
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static DataTable Get_HoSo_XNCL(long iID_MaHoSo, long iID_MaHangHoa)
        {
            DataTable vR = null;

            string SQL = "SELECT TOP 1 * FROM CNN25_HoSo_XNCL WHERE iID_MaHoSo=@iID_MaHoSo AND iID_MaHangHoa=@iID_MaHangHoa ORDER BY dNgayTao DESC";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
            cmd.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
            vR = Connection.GetDataTable(cmd);
            cmd.Dispose();

            return vR;
        }

        public static long ThemHoSoHuy(long iID_MaHoSo, long iID_MaHangHoa,
            string sMaHoSo, DateTime dNgayHuy, string sLyDo,
            string sMaFile, string sTenFile, string sLinkFile,
            String sUserName, String sIP)
        {
            long vR = 0;

            try
            {
                long iID_MaHoSoHuy = 0;
                Bang bang = new Bang("CNN25_HoSo_Huy");
                bang.MaNguoiDungSua = sUserName;
                bang.IPSua = sIP;
                bang.DuLieuMoi = true;
                bang.CmdParams.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
                bang.CmdParams.Parameters.AddWithValue("@sMaHoSo", sMaHoSo);
                if (dNgayHuy != null)
                {
                    if (dNgayHuy.Year > 2000)
                    {
                        bang.CmdParams.Parameters.AddWithValue("@dNgayHuy", dNgayHuy);
                    }
                }
                bang.CmdParams.Parameters.AddWithValue("@sLyDo", sLyDo);
                bang.CmdParams.Parameters.AddWithValue("@sMaFile", sMaFile);
                bang.CmdParams.Parameters.AddWithValue("@sTenFile", sTenFile);
                bang.CmdParams.Parameters.AddWithValue("@sLinkFile", sLinkFile);

                iID_MaHoSoHuy = Convert.ToInt64(bang.Save());

                vR = iID_MaHoSoHuy;
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static DataTable Get_DanhSach_DoanhNghiep()
        {
            DataTable vR;

            string SQL = "SELECT DISTINCT sMaSoThue, sTenDoanhNghiep FROM CNN25_HoSo ORDER BY sTenDoanhNghiep";
            SqlCommand cmd = new SqlCommand(SQL);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();

            return vR;
        }

        public static SelectOptionList DDLDoanhNghiep(bool TatCa = true)
        {
            DataTable dt = Get_DanhSach_DoanhNghiep();
            DataRow r;
            if (TatCa)
            {
                dt.Rows.InsertAt(dt.NewRow(), 0);
                dt.Rows[0]["sMaSoThue"] = "";
                dt.Rows[0]["sTenDoanhNghiep"] = "--- Tất cả ---";
            }

            SelectOptionList DDL = new SelectOptionList(dt, "sMaSoThue", "sTenDoanhNghiep");
            dt.Dispose();

            return DDL;
        }
    }
}
