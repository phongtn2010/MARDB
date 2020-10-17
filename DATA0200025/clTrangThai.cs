﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DomainModel;
using DATA0200025.Models;
using DomainModel.Controls;
using System.Data;
using Dapper;

namespace DATA0200025
{
    public class clTrangThai
    {
        public static int GetTrangThaiIdTiepTheo(int iID_MaDoiTuong, int iID_MaHanhDong, string sMessageType, string sMessageFunction)
        {
            int vR = 0;
            string SQL = @"SELECT iID_MaTrangThaiSau FROM CNN25_ChuyenTrangThai 
                            WHERE iID_MaDoiTuong=@iID_MaDoiTuong AND iID_MaHanhDong=@iID_MaHanhDong 
                            AND sMessageType=@sMessageType AND sMessageFunction=@sMessageFunction";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaDoiTuong", iID_MaDoiTuong);
            cmd.Parameters.AddWithValue("@iID_MaHanhDong", iID_MaHanhDong);
            cmd.Parameters.AddWithValue("@sMessageType", sMessageType);
            cmd.Parameters.AddWithValue("@sMessageFunction", sMessageFunction);
            vR = Convert.ToInt32(Connection.GetValue(cmd, 0, CThamSo.iKetNoi));
            cmd.Dispose();         
            return vR;
        }
        public static int GetTrangThaiIdTiepTheo(int iID_MaDoiTuong, int iID_MaHanhDong, int iID_MaTrangThai,int iID_MaTrangThaiTruoc)
        {
            int vR = 0;
            string SQL = @"SELECT iID_MaTrangThaiSau FROM CNN25_ChuyenTrangThai 
                            WHERE iID_MaDoiTuong=@iID_MaDoiTuong AND iID_MaHanhDong=@iID_MaHanhDong 
                            AND iID_MaTrangThaiTruoc=@iID_MaTrangThaiTruoc";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaDoiTuong", iID_MaDoiTuong);
            cmd.Parameters.AddWithValue("@iID_MaHanhDong", iID_MaHanhDong);
            cmd.Parameters.AddWithValue("@iID_MaTrangThaiTruoc", iID_MaTrangThai);
            vR = Convert.ToInt32(Connection.GetValue(cmd, 0, CThamSo.iKetNoi));
            cmd.Dispose();
            //sw dụng khi Chuyên viên	10,11,12	Thu hồi và xử lý lại Trở lại mã trạng thái hồ sơ trước đó (7,14,16,18,23)
            if (vR == -1) return iID_MaTrangThaiTruoc;

            return vR;
        }

        public static TrangThaiModels GetTrangThaiModelsTiepTheo(int iID_MaDoiTuong, int iID_MaHanhDong, int iID_MaTrangThai,int iID_MaTrangThaiTruoc)
        {
            int TrangThai = GetTrangThaiIdTiepTheo(iID_MaDoiTuong, iID_MaHanhDong, iID_MaTrangThai, iID_MaTrangThaiTruoc);
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT *  FROM CNN25_TrangThai 
                            WHERE iID_MaTrangThai=@iID_MaTrangThai";
                TrangThaiModels results = connect.Query<TrangThaiModels>(SQL, new { iID_MaTrangThai = TrangThai }).FirstOrDefault();
                return results;
            }
        }
        public static TrangThaiModels GetTrangThaiModelsTiepTheo(int iID_MaDoiTuong, int iID_MaHanhDong, string sMessageType, string sMessageFunction)
        {
            int iID_MaTrangThai = GetTrangThaiIdTiepTheo(iID_MaDoiTuong, iID_MaHanhDong, sMessageType, sMessageFunction);
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT *  FROM CNN25_TrangThai 
                            WHERE iID_MaTrangThai=@iID_MaTrangThai";
                TrangThaiModels results = connect.Query<TrangThaiModels>(SQL, new { iID_MaTrangThai = iID_MaTrangThai }).FirstOrDefault();
                return results;
            }
        }

        public static TrangThaiModels GetTrangThaiById(int iID_MaTrangThai)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT *  FROM CNN25_TrangThai 
                            WHERE iID_MaTrangThai=@iID_MaTrangThai";
                TrangThaiModels results = connect.Query<TrangThaiModels>(SQL, new { iID_MaTrangThai = iID_MaTrangThai }).FirstOrDefault();
                return results;
            }
        }

        public static DataTable GetDataTable()
        {
            string SQL = @"SELECT *  FROM CNN25_TrangThai";
            SqlCommand cmd = new SqlCommand(SQL);
            DataTable dt = Connection.GetDataTable(cmd,CThamSo.iKetNoi);
            cmd.Dispose();
            return dt;
        }
        public static SelectOptionList GetDDL()
        {
            DataTable dt = GetDataTable();
            SelectOptionList DDL = new SelectOptionList(dt, "iID_MaTrangThai", "sTen");
            return DDL;
        }
    }
}
