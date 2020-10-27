﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using DATA0200025.Models;
using System.Data;
using DATA0200025.SearchModels;
using Dapper;
using DomainModel.Controls;

namespace DATA0200025
{
    public class clHangHoa
    {
        public static HangHoaModels GetHangHoaById(string iID_MaHangHoa)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT *  FROM CNN25_HangHoa 
                            WHERE iID_MaHangHoa=@iID_MaHangHoa";
                HangHoaModels results = connect.Query<HangHoaModels>(SQL, new { iID_MaHangHoa = iID_MaHangHoa }).FirstOrDefault();
                return results;
            }
        }
        public static HangHoaModels GetHangHoaById(int iID_MaHangHoa)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT *  FROM CNN25_HangHoa 
                            WHERE iID_MaHangHoa=@iID_MaHangHoa";
                HangHoaModels results = connect.Query<HangHoaModels>(SQL, new { iID_MaHangHoa = iID_MaHangHoa }).FirstOrDefault();
                return results;
            }
        }
        public static DataTable Get_HangHoaTheoHoSo(int iID_MaHoSo,string iID_MaPhanLoai)
        {
            string SQL = "SELECT *  FROM CNN25_HangHoa WHERE iID_MaHoSo=@iID_MaHoSo AND iID_MaPhanLoai=@iID_MaPhanLoai";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
            cmd.Parameters.AddWithValue("@iID_MaPhanLoai", iID_MaPhanLoai);
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();
            return dt;
        }

        public static SelectOptionList DDL_PhanLoaiTheoHoSo(int iID_MaHoSo)
        {
            string SQL = "SELECT * FROM CNN25_DanhMuc WHERE iID_MaDanhMuc IN( SELECT iID_MaPhanLoai FROM CNN25_HangHoa WHERE iID_MaHoSo=@iID_MaHoSo) ORDER By sTen";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();
            SelectOptionList ddl = new SelectOptionList(dt, "iID_MaDanhMuc", "sTen");
            dt.Dispose();
            return ddl;
        }

        public static DataTable Get_ThongTinChiTieuChatLuong(int iID_MaHangHoa)
        {
            string SQL = "SELECT * FROM CNN25_HangHoa_ChatLuong  WHERE iID_MaHangHoa=@iID_MaHangHoa";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();
            return dt;
        }
        public static DataTable Get_ThongTinChiTieuAnToanDN(int iID_MaHangHoa)
        {
            string SQL = "SELECT * FROM CNN25_HangHoa_AnToan WHERE iID_MaHangHoa=@iID_MaHangHoa";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();
            return dt;
        }

        public static DataTable Get_ThongTinChiTieuAnToanKyThuat(int iID_MaHangHoa)
        {
            string SQL = "SELECT * FROM CNN25_HangHoa_AnToan_KyThuat WHERE iID_MaHangHoa=@iID_MaHangHoa";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();
            return dt;
        }

        public static void Update_ResetbChon(string iID_MaHangHoa)
        {
            string SQL = "Update CNN25_HangHoa_AnToan_KyThuat SET bChon=0,sGhiChu='' WHERE iID_MaHangHoa=@iID_MaHangHoa";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
            Connection.UpdateDatabase(cmd);
            cmd.CommandText= "Update CNN25_HangHoa_AnToan SET bChon=0,sGhiChu='' WHERE iID_MaHangHoa=@iID_MaHangHoa";
            Connection.UpdateDatabase(cmd);
            cmd.CommandText = "Update CNN25_HangHoa_ChatLuong SET bChon=0,sGhiChu='' WHERE iID_MaHangHoa=@iID_MaHangHoa";
            Connection.UpdateDatabase(cmd);
            cmd.Dispose();
        }
        public static void UpdateNguoiXem(string iID_MaHangHoa, string MaND)
        {
            string SQL = "UPDATE CNN25_HangHoa SET sTenNguoiXem=@sTenNguoiXem,sUserNguoiXem=@sUserNguoiXem WHERE iID_MaHangHoa=@iID_MaHangHoa AND (sUserNguoiXem IS NULL OR sUserNguoiXem='')";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
            cmd.Parameters.AddWithValue("@sTenNguoiXem", CPQ_NGUOIDUNG.Get_TenNguoiDung(MaND));
            cmd.Parameters.AddWithValue("@sUserNguoiXem", MaND);
            Connection.UpdateDatabase(cmd);
            cmd.Dispose();
        }

        public static ResultModels CleanNguoiXem(string iID_MaHangHoa)
        {
            int vR = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@iID_MaHangHoa", iID_MaHangHoa);
            cmd.Parameters.AddWithValue("@sTenNguoiXem", "");
            cmd.Parameters.AddWithValue("@sUserNguoiXem", "");
            vR = Connection.UpdateRecord("CNN25_HangHoa", "iID_MaHangHoa", cmd);
            cmd.Dispose();
            return new ResultModels
            {
                success = vR > 0 ? true : false
            };

        }
    }
}