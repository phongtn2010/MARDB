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
   public class clBaoCao
    {
        public static DataTable BaoCaoTinhHinhXuLyHoSo(string tuNgay,string denNgay,string tenDoanhNghiep)
        {
            string SQL = @"SELECT SUM(c3) as c3,SUM(c4) as c4,SUM(c5) as c5,SUM(c7) as c7,SUM(c8) as c8,SUM(c9) as c9
                        ,SUM(c11) as c11,SUM(c12) as c12,SUM(c15) as c15,SUM(c16) as c16,SUM(c18) as c18,SUM(c19) as c19
                        FROM (
                        SELECT c3=0,c4=0,c5=0,c7=0,c8=0,c9=0,c11=0,c12=0,c15=0,c16=0,c18=0, count(iID_MaHoSo) as c19 FROM CNN25_HoSo WHERE (iID_MaTrangThai=9 OR iID_MaTrangThai=15) AND DATEDIFF(DAY,dNgayTiepNhan,GETDATE())>1
                        UNION
                        SELECT  c3=0,c4=0,c5=0,c7=0,c8=0,c9=0,c11=0,c12=0,c15=0,c16=0,count(iID_MaHoSo) as c18,c19=0 FROM CNN25_HoSo WHERE  (iID_MaTrangThai=9 OR iID_MaTrangThai=15) AND DATEDIFF(DAY,dNgayTiepNhan,GETDATE())<=1
                        UNION
                        SELECT  c3=0,c4=0,c5=0,c7=0,c8=0,c9=0,c11=0,c12=0,c15=0,count(iID_MaHoSo) as c16,c18=0,c19=0 FROM CNN25_HoSo WHERE (iID_MaTrangThai=8 OR iID_MaTrangThai=21) AND DATEDIFF(DAY,dNgayTiepNhan,GETDATE())>1
                        UNION
                        SELECT c3=0,c4=0,c5=0,c7=0,c8=0,c9=0,c11=0,c12=0,count(iID_MaHoSo) as c15,c16=0,c18=0,c19=0 FROM CNN25_HoSo WHERE  (iID_MaTrangThai=8 OR iID_MaTrangThai=21) AND DATEDIFF(DAY,dNgayTiepNhan,GETDATE())<=1
                        UNION

                        SELECT c3=0,c4=0,c5=0,c7=0,c8=0,c9=0,c11=0,count(iID_MaHoSo) as c12,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HoSo WHERE 
                        (iID_MaTrangThai=7 OR (iID_MaTrangThai>=10 AND iID_MaTrangThai<=14) 
                        OR (iID_MaTrangThai>=16 AND iID_MaTrangThai<=20) OR (iID_MaTrangThai>=22 AND iID_MaTrangThai<=25)) 
                        AND DATEDIFF(DAY,dNgayTiepNhan,GETDATE())<=1
                        UNION

                        SELECT c3=0,c4=0,c5=0,c7=0,c8=0,c9=0,count(iID_MaHoSo) as c11,c12=0,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HoSo WHERE 
                        (iID_MaTrangThai=7 OR (iID_MaTrangThai>=10 AND iID_MaTrangThai<=14) 
                        OR (iID_MaTrangThai>=16 AND iID_MaTrangThai<=20) OR (iID_MaTrangThai>=22 AND iID_MaTrangThai<=25)) 
                        AND DATEDIFF(DAY,dNgayTiepNhan,GETDATE())>=1
                        UNION
                        SELECT c3=0,c4=0,c5=0,c7=0,c8=0,count(iID_MaHangHoa) as c9,c11=0,c12=0,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HangHoa WHERE iID_MaTrangThai=47
                        UNION
                        SELECT c3=0,c4=0,c5=0,c7=0,count(iID_MaHangHoa) as c8,c9=0,c11=0,c12=0,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HangHoa 
                        WHERE (iID_MaTrangThai=32 AND iID_MaHoSo IN (SELECT iID_MaHoSo FROM CNN25_HoSo WHERE iID_MaLoaiHoSo=1 OR iID_MaLoaiHoSo=2)) OR iID_MaTrangThai=44
                        UNION
                        SELECT c3=0,c4=0,c5=0,count(iID_MaHangHoa) as c7,c8=0,c9=0,c11=0,c12=0,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HangHoa 
                        WHERE (iID_MaTrangThai>=27 AND iID_MaTrangThai<44 AND iID_MaHoSo IN (SELECT iID_MaHoSo FROM CNN25_HoSo WHERE iID_MaLoaiHoSo=3 OR iID_MaLoaiHoSo=2))
                        OR (((iID_MaTrangThai>=29 AND iID_MaTrangThai<32 ) OR iID_MaTrangThai=33) 
                        AND iID_MaHoSo IN (SELECT iID_MaHoSo FROM CNN25_HoSo WHERE iID_MaLoaiHoSo=1 OR iID_MaLoaiHoSo=2))
                        UNION
                        SELECT  c3=0,c4=0,count(iID_MaHangHoa) as c5,c7=0,c8=0,c9=0,c11=0,c12=0,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HangHoa WHERE iID_MaTrangThai=48 
                        UNION
                        SELECT c3=0,count(iID_MaHoSo) as c4,c5=0,c7=0,c8=0,c9=0,c11=0,c12=0,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HoSo WHERE iID_MaTrangThai=26 AND DATEDIFF(DAY,dNgayTiepNhan,GETDATE())>1
                        UNION
                        SELECT count(iID_MaHoSo) as c3,c4=0,c5=0,c7=0,c8=0,c9=0,c11=0,c12=0,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HoSo WHERE iID_MaTrangThai=26 AND DATEDIFF(DAY,dNgayTiepNhan,GETDATE())<=1
                        ) tb";
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = SQL;
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();
            return dt;
        }
    }
}