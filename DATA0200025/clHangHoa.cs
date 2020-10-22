using System;
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
            string SQL = "SELECT * FROM CNN25_DanhMuc WHERE iID_MaDanhMuc IN(SELECT iID_MaPhanLoai FROM CNN25_HangHoa WHERE iID_MaHoSo=@iID_MaHoSo) ORDER By sTen";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();
            SelectOptionList ddl = new SelectOptionList(dt, "iID_MaDanhMuc", "sTen");
            dt.Dispose();
            return ddl;
        }
    }
}
