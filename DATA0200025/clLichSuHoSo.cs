﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
namespace DATA0200025
{
    public class clLichSuHoSo
    {
        /// <summary>
        /// Insert lịch sử hồ sơ
        /// </summary>
        /// <param name="MaND">user name đăng nhập</param>
        /// <param name="iID_MaDoiTuong"></param>
        /// <param name="iID_MaHanhDong"></param>
        /// <param name="sNoiDung"></param>
        /// <param name="sFile"></param>
        /// <param name="iID_MaTrangThai"></param>
        public void InsertLichSu(string MaND,int iID_MaDoiTuong,int iID_MaHanhDong,string sNoiDung,string sFile, int iID_MaTrangThai)
        {

            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@sMaND",MaND);
            cmd.Parameters.AddWithValue("@sNguoiXuLy", CPQ_NGUOIDUNG.Get_TenNguoiDung(MaND));
            cmd.Parameters.AddWithValue("@iID_MaDoiTuong", iID_MaDoiTuong);
            cmd.Parameters.AddWithValue("@sTenDoiTuong", clCommon.GetDoiTuongById(iID_MaDoiTuong).sTen);
            cmd.Parameters.AddWithValue("@iID_MaHanhDong", iID_MaHanhDong);
            cmd.Parameters.AddWithValue("@sTenHanhDong", clCommon.GetHanhDongById(iID_MaHanhDong).sTen);
            cmd.Parameters.AddWithValue("@sNoiDung", sNoiDung);
            cmd.Parameters.AddWithValue("@sFile", sFile);
            cmd.Parameters.AddWithValue("@iID_MaTrangThai", iID_MaTrangThai);
            cmd.Parameters.AddWithValue("@sTenTrangThai", clTrangThai.GetTrangThaiById(iID_MaTrangThai).sTen);
            Connection.InsertRecord("CNN25_LichSuHoSo", cmd, CThamSo.iKetNoi);
            cmd.Dispose();
        }
    }
}