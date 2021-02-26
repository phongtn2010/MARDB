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
   public class clBaoCao
    {
        public static DataTable BaoCaoTinhHinhXuLyHoSo(ReportSearchModels model)
        {
            string DK = "", DKHH="";
            //string SQL = @"SELECT SUM(c3) as c3,SUM(c4) as c4,SUM(c5) as c5,SUM(c7) as c7,SUM(c8) as c8,SUM(c9) as c9
            //            ,SUM(c11) as c11,SUM(c12) as c12,SUM(c15) as c15,SUM(c16) as c16,SUM(c18) as c18,SUM(c19) as c19
            //            FROM (
            //            SELECT c3=0,c4=0,c5=0,c7=0,c8=0,c9=0,c11=0,c12=0,c15=0,c16=0,c18=0, count(iID_MaHoSo) as c19 FROM CNN25_HoSo WHERE (iID_MaTrangThai=9 OR iID_MaTrangThai=15) AND DATEDIFF(DAY,dNgayTiepNhan,GETDATE())>1 {0}
            //            UNION
            //            SELECT  c3=0,c4=0,c5=0,c7=0,c8=0,c9=0,c11=0,c12=0,c15=0,c16=0,count(iID_MaHoSo) as c18,c19=0 FROM CNN25_HoSo WHERE  (iID_MaTrangThai=9 OR iID_MaTrangThai=15) AND DATEDIFF(DAY,dNgayTiepNhan,GETDATE())<=1  {0}
            //            UNION
            //            SELECT  c3=0,c4=0,c5=0,c7=0,c8=0,c9=0,c11=0,c12=0,c15=0,count(iID_MaHoSo) as c16,c18=0,c19=0 FROM CNN25_HoSo WHERE (iID_MaTrangThai=8 OR iID_MaTrangThai=21) AND DATEDIFF(DAY,dNgayTiepNhan,GETDATE())>1  {0}
            //            UNION
            //            SELECT c3=0,c4=0,c5=0,c7=0,c8=0,c9=0,c11=0,c12=0,count(iID_MaHoSo) as c15,c16=0,c18=0,c19=0 FROM CNN25_HoSo WHERE  (iID_MaTrangThai=8 OR iID_MaTrangThai=21) AND DATEDIFF(DAY,dNgayTiepNhan,GETDATE())<=1  {0}
            //            UNION

            //            SELECT c3=0,c4=0,c5=0,c7=0,c8=0,c9=0,c11=0,count(iID_MaHoSo) as c12,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HoSo WHERE 
            //            (iID_MaTrangThai=7 OR (iID_MaTrangThai>=10 AND iID_MaTrangThai<=14) 
            //            OR (iID_MaTrangThai>=16 AND iID_MaTrangThai<=20) OR (iID_MaTrangThai>=22 AND iID_MaTrangThai<=25)) 
            //            AND DATEDIFF(DAY,dNgayTiepNhan,GETDATE())>1  {0}
            //            UNION

            //            SELECT c3=0,c4=0,c5=0,c7=0,c8=0,c9=0,count(iID_MaHoSo) as c11,c12=0,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HoSo WHERE 
            //            (iID_MaTrangThai=7 OR (iID_MaTrangThai>=10 AND iID_MaTrangThai<=14) 
            //            OR (iID_MaTrangThai>=16 AND iID_MaTrangThai<=20) OR (iID_MaTrangThai>=22 AND iID_MaTrangThai<=25)) 
            //            AND DATEDIFF(DAY,dNgayTiepNhan,GETDATE())<=1  {0}
            //            UNION
            //            SELECT c3=0,c4=0,c5=0,c7=0,c8=0,count(iID_MaHangHoa) as c9,c11=0,c12=0,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HangHoa WHERE iID_MaTrangThai=47 {1}
            //            UNION
            //            SELECT c3=0,c4=0,c5=0,c7=0,count(iID_MaHangHoa) as c8,c9=0,c11=0,c12=0,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HangHoa 
            //            WHERE (iID_MaTrangThai=32 AND iID_MaHoSo IN (SELECT iID_MaHoSo FROM CNN25_HoSo WHERE iID_MaLoaiHoSo=1 OR iID_MaLoaiHoSo=2)) OR iID_MaTrangThai=44  {1}
            //            UNION
            //            SELECT c3=0,c4=0,c5=0,count(iID_MaHangHoa) as c7,c8=0,c9=0,c11=0,c12=0,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HangHoa 
            //            WHERE (iID_MaTrangThai>=27 AND iID_MaTrangThai<44 AND iID_MaHoSo IN (SELECT iID_MaHoSo FROM CNN25_HoSo WHERE iID_MaLoaiHoSo=3 OR iID_MaLoaiHoSo=2)) 
            //            OR (((iID_MaTrangThai>=29 AND iID_MaTrangThai<32 ) OR iID_MaTrangThai=33) 
            //            AND iID_MaHoSo IN (SELECT iID_MaHoSo FROM CNN25_HoSo WHERE iID_MaLoaiHoSo=1 OR iID_MaLoaiHoSo=2)) {1}
            //            UNION
            //            SELECT  c3=0,c4=0,count(iID_MaHangHoa) as c5,c7=0,c8=0,c9=0,c11=0,c12=0,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HangHoa WHERE iID_MaTrangThai=48 {1}
            //            UNION
            //            SELECT c3=0,count(iID_MaHoSo) as c4,c5=0,c7=0,c8=0,c9=0,c11=0,c12=0,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HoSo WHERE iID_MaTrangThai>=26 AND sSoTiepNhan != '' AND bPublic = 1 {0}
            //            UNION
            //            SELECT count(iID_MaHoSo) as c3,c4=0,c5=0,c7=0,c8=0,c9=0,c11=0,c12=0,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HoSo WHERE iID_MaTrangThai>=26 AND sSoTiepNhan != '' AND bPublic = 0 {0}
            //            ) tb";

            string SQL = @"SELECT SUM(c3) as c3,SUM(c4) as c4,SUM(c5) as c5,SUM(c7) as c7,SUM(c8) as c8,SUM(c9) as c9
                        ,SUM(c11) as c11,SUM(c12) as c12,SUM(c15) as c15,SUM(c16) as c16,SUM(c18) as c18,SUM(c19) as c19
                        FROM (
                        SELECT c3=0,c4=0,c5=0,c7=0,c8=0,c9=0,c11=0,c12=0,c15=0,c16=0,c18=0, count(iID_MaHoSo) as c19 FROM CNN25_HoSo WHERE (iID_MaTrangThai=9 OR iID_MaTrangThai=15) AND sSoTiepNhan != '' AND bPublic = 1 {0}
                        UNION
                        SELECT  c3=0,c4=0,c5=0,c7=0,c8=0,c9=0,c11=0,c12=0,c15=0,c16=0,count(iID_MaHoSo) as c18,c19=0 FROM CNN25_HoSo WHERE (iID_MaTrangThai=9 OR iID_MaTrangThai=15) AND sSoTiepNhan != '' AND bPublic = 0  {0}
                        UNION

                        SELECT  c3=0,c4=0,c5=0,c7=0,c8=0,c9=0,c11=0,c12=0,c15=0,count(iID_MaHoSo) as c16,c18=0,c19=0 FROM CNN25_HoSo WHERE (iID_MaTrangThai=8 OR iID_MaTrangThai=21) AND sSoTiepNhan != '' AND bPublic = 1  {0}
                        UNION
                        SELECT c3=0,c4=0,c5=0,c7=0,c8=0,c9=0,c11=0,c12=0,count(iID_MaHoSo) as c15,c16=0,c18=0,c19=0 FROM CNN25_HoSo WHERE (iID_MaTrangThai=8 OR iID_MaTrangThai=21) AND sSoTiepNhan != '' AND bPublic = 0  {0}
                        UNION

                        SELECT c3=0,c4=0,c5=0,c7=0,c8=0,c9=0,c11=0,count(iID_MaHoSo) as c12,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HoSo WHERE (iID_MaTrangThai=7 OR (iID_MaTrangThai>=10 AND iID_MaTrangThai<=14) 
                        OR (iID_MaTrangThai>=16 AND iID_MaTrangThai<=20) OR (iID_MaTrangThai>=22 AND iID_MaTrangThai<=25)) AND sSoTiepNhan != '' AND bPublic = 1  {0}
                        UNION
                        SELECT c3=0,c4=0,c5=0,c7=0,c8=0,c9=0,count(iID_MaHoSo) as c11,c12=0,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HoSo WHERE (iID_MaTrangThai=7 OR (iID_MaTrangThai>=10 AND iID_MaTrangThai<=14) 
                        OR (iID_MaTrangThai>=16 AND iID_MaTrangThai<=20) OR (iID_MaTrangThai>=22 AND iID_MaTrangThai<=25)) AND sSoTiepNhan != '' AND bPublic = 0  {0}
                        UNION

                        SELECT c3=0,c4=0,c5=0,c7=0,c8=0,count(iID_MaHangHoa) as c9,c11=0,c12=0,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HangHoa WHERE iID_MaTrangThai=47 {1}  
                        UNION
                        SELECT c3=0,c4=0,c5=0,c7=0,count(iID_MaHangHoa) as c8,c9=0,c11=0,c12=0,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HangHoa WHERE iID_MaTrangThai=44  {1}
                        UNION
                        SELECT c3=0,c4=0,c5=0,count(iID_MaHangHoa) as c7,c8=0,c9=0,c11=0,c12=0,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HangHoa WHERE iID_MaTrangThai>=27 AND iID_MaTrangThai<44 {1}
                        UNION

                        SELECT  c3=0,c4=0,count(iID_MaHangHoa) as c5,c7=0,c8=0,c9=0,c11=0,c12=0,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HangHoa WHERE iID_MaTrangThai=48 {1}
                        UNION
                        SELECT c3=0,count(iID_MaHoSo) as c4,c5=0,c7=0,c8=0,c9=0,c11=0,c12=0,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HoSo WHERE iID_MaTrangThai>=26 AND sSoTiepNhan != '' AND bPublic = 1 {0}
                        UNION
                        SELECT count(iID_MaHoSo) as c3,c4=0,c5=0,c7=0,c8=0,c9=0,c11=0,c12=0,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HoSo WHERE iID_MaTrangThai>=26 AND sSoTiepNhan != '' AND bPublic = 0 {0}
                        ) tb";

            SqlCommand cmd = new SqlCommand();
            if (!string.IsNullOrEmpty(model.TuNgay))
            {
                DK += " AND Cast(datediff(day, 0, dNgayTiepNhan) as datetime) >= @dTuNgay";   // _FromDate = 'yyyy-MM-dd'
                DKHH += " AND Cast(datediff(day, 0, dNgayTiepNhanHoSo) as datetime) >= @dTuNgay";
                cmd.Parameters.AddWithValue("@dTuNgay", CommonFunction.LayNgayTuXau_YYYYMMDD(model.TuNgay));

            }
            if (!string.IsNullOrEmpty(model.DenNgay))
            {
                DK += " AND Cast(datediff(day, 0, dNgayTiepNhan) as datetime) <= @dDenNgay";   // _FromDate = 'yyyy-MM-dd'
                DKHH += " AND Cast(datediff(day, 0, dNgayTiepNhanHoSo) as datetime) <= @dDenNgay";
                cmd.Parameters.AddWithValue("@dDenNgay", CommonFunction.LayNgayTuXau_YYYYMMDD(model.DenNgay));
            }
            if (!string.IsNullOrEmpty(model.sMaSoThue))
            {
                DK += " AND sMaSoThue=@sMaSoThue";
                DKHH += " AND iID_MaHoSo IN(SELECT iID_MaHoSo FROM CNN25_HoSo WHERE sMaSoThue=@sMaSoThue)";
                cmd.Parameters.AddWithValue("@sMaSoThue", model.sMaSoThue);
            }
            SQL = string.Format(SQL, DK, DKHH);
                cmd.CommandText = SQL;
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();
            return dt;
        }

        public static DataTable CVBaoCaoTinhHinhXuLyHoSo(ReportSearchModels model)
        {
            string DK = "", DKHH = "";
            string SQL = @"SELECT SUM(c3) as c3,SUM(c4) as c4,SUM(c5) as c5,SUM(c7) as c7,SUM(c8) as c8,SUM(c9) as c9
                        ,SUM(c11) as c11,SUM(c12) as c12,SUM(c15) as c15,SUM(c16) as c16,SUM(c18) as c18,SUM(c19) as c19
                        FROM (
                        SELECT c3=0,c4=0,c5=0,c7=0,c8=0,c9=0,c11=0,c12=0,c15=0,c16=0,c18=0, count(iID_MaHoSo) as c19 FROM CNN25_HoSo WHERE (iID_MaTrangThai=9 OR iID_MaTrangThai=15) AND sSoTiepNhan != '' AND bPublic = 1 {0}
                        UNION
                        SELECT  c3=0,c4=0,c5=0,c7=0,c8=0,c9=0,c11=0,c12=0,c15=0,c16=0,count(iID_MaHoSo) as c18,c19=0 FROM CNN25_HoSo WHERE (iID_MaTrangThai=9 OR iID_MaTrangThai=15) AND sSoTiepNhan != '' AND bPublic = 0  {0}
                        UNION

                        SELECT  c3=0,c4=0,c5=0,c7=0,c8=0,c9=0,c11=0,c12=0,c15=0,count(iID_MaHoSo) as c16,c18=0,c19=0 FROM CNN25_HoSo WHERE (iID_MaTrangThai=8 OR iID_MaTrangThai=21) AND sSoTiepNhan != '' AND bPublic = 1  {0}
                        UNION
                        SELECT c3=0,c4=0,c5=0,c7=0,c8=0,c9=0,c11=0,c12=0,count(iID_MaHoSo) as c15,c16=0,c18=0,c19=0 FROM CNN25_HoSo WHERE (iID_MaTrangThai=8 OR iID_MaTrangThai=21) AND sSoTiepNhan != '' AND bPublic = 0  {0}
                        UNION

                        SELECT c3=0,c4=0,c5=0,c7=0,c8=0,c9=0,c11=0,count(iID_MaHoSo) as c12,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HoSo WHERE (iID_MaTrangThai=7 OR (iID_MaTrangThai>=10 AND iID_MaTrangThai<=14) 
                        OR (iID_MaTrangThai>=16 AND iID_MaTrangThai<=20) OR (iID_MaTrangThai>=22 AND iID_MaTrangThai<=25)) AND sSoTiepNhan != '' AND bPublic = 1  {0}
                        UNION
                        SELECT c3=0,c4=0,c5=0,c7=0,c8=0,c9=0,count(iID_MaHoSo) as c11,c12=0,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HoSo WHERE (iID_MaTrangThai=7 OR (iID_MaTrangThai>=10 AND iID_MaTrangThai<=14) 
                        OR (iID_MaTrangThai>=16 AND iID_MaTrangThai<=20) OR (iID_MaTrangThai>=22 AND iID_MaTrangThai<=25)) AND sSoTiepNhan != '' AND bPublic = 0  {0}
                        UNION

                        SELECT c3=0,c4=0,c5=0,c7=0,c8=0,count(iID_MaHangHoa) as c9,c11=0,c12=0,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HangHoa WHERE iID_MaTrangThai=47 {1}  
                        UNION
                        SELECT c3=0,c4=0,c5=0,c7=0,count(iID_MaHangHoa) as c8,c9=0,c11=0,c12=0,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HangHoa WHERE iID_MaTrangThai=44  {1}
                        UNION
                        SELECT c3=0,c4=0,c5=0,count(iID_MaHangHoa) as c7,c8=0,c9=0,c11=0,c12=0,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HangHoa WHERE iID_MaTrangThai>=27 AND iID_MaTrangThai<44 {1}
                        UNION

                        SELECT  c3=0,c4=0,count(iID_MaHangHoa) as c5,c7=0,c8=0,c9=0,c11=0,c12=0,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HangHoa WHERE iID_MaTrangThai=48 {1}
                        UNION
                        SELECT c3=0,count(iID_MaHoSo) as c4,c5=0,c7=0,c8=0,c9=0,c11=0,c12=0,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HoSo WHERE iID_MaTrangThai>=26 AND sSoTiepNhan != '' AND bPublic = 1 {0}
                        UNION
                        SELECT count(iID_MaHoSo) as c3,c4=0,c5=0,c7=0,c8=0,c9=0,c11=0,c12=0,c15=0,c16=0,c18=0,c19=0 FROM CNN25_HoSo WHERE iID_MaTrangThai>=26 AND sSoTiepNhan != '' AND bPublic = 0 {0}
                        ) tb";

            SqlCommand cmd = new SqlCommand();
            if (!string.IsNullOrEmpty(model.TuNgay))
            {
                DK += " AND Cast(datediff(day, 0, dNgayTiepNhan) as datetime) >= @dTuNgay";   // _FromDate = 'yyyy-MM-dd'
                DKHH += " AND Cast(datediff(day, 0, dNgayTiepNhanHoSo) as datetime) >= @dTuNgay";
                cmd.Parameters.AddWithValue("@dTuNgay", CommonFunction.LayNgayTuXau_YYYYMMDD(model.TuNgay));

            }
            if (!string.IsNullOrEmpty(model.DenNgay))
            {
                DK += " AND Cast(datediff(day, 0, dNgayTiepNhan) as datetime) <= @dDenNgay";   // _FromDate = 'yyyy-MM-dd'
                DKHH += " AND Cast(datediff(day, 0, dNgayTiepNhanHoSo) as datetime) <= @dDenNgay";
                cmd.Parameters.AddWithValue("@dDenNgay", CommonFunction.LayNgayTuXau_YYYYMMDD(model.DenNgay));
            }
            if (!string.IsNullOrEmpty(model.sMaSoThue))
            {
                DK += " AND sMaSoThue=@sMaSoThue";
                DKHH += " AND iID_MaHoSo IN(SELECT iID_MaHoSo FROM CNN25_HoSo WHERE sMaSoThue=@sMaSoThue)";
                cmd.Parameters.AddWithValue("@sMaSoThue", model.sMaSoThue);
            }
            SQL = string.Format(SQL, DK, DKHH);
            cmd.CommandText = SQL;
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();
            return dt;
        }

        #region BaoCao01
        public static DataTable CVBaoCao01(ReportSearchModels model, int page, int numrecord, string sOrderInput = "iID_MaHoSo DESC")
        {
            string DK = "1=1", DKHH = "1=1";
            SqlCommand cmd = new SqlCommand();

            DKHH += " AND iID_MaTrangThai=@iID_MaTrangThai";
            cmd.Parameters.AddWithValue("@iID_MaTrangThai", 44);

            if (!string.IsNullOrEmpty(model.TuNgay))
            {
                DKHH += " AND Cast(datediff(day, 0, dSoThongBaoKetQua_NgayKy) as datetime) >= @TuNgayThongBaoKetQua";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@TuNgayThongBaoKetQua", CommonFunction.LayNgayTuXau_YYYYMMDD(model.TuNgay));
            }
            if (!string.IsNullOrEmpty(model.DenNgay))
            {
                DKHH += " AND Cast(datediff(day, 0, dSoThongBaoKetQua_NgayKy) as datetime) <= @DenNgayThongBaoKetQua";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@DenNgayThongBaoKetQua", CommonFunction.LayNgayTuXau_YYYYMMDD(model.DenNgay));
            }

            string SQL = string.Format(@"SELECT * FROM (SELECT hh.*,
                                        hs.sLoaiHinhThucKiemTra,hs.sTenDoanhNghiep,hs.sMua_DiaChi,hs.sMua_NoiNhan,hs.sMua_FromDate,hs.sMua_ToDate,hs.dNgayTaoHoSo,hs.sSoTiepNhan,hs.dNgayTiepNhan,hs.sSoGDK,hs.dNgayXacNhan,hs.sBan_NoiXuat
                                        FROM(select * from CNN25_HangHoa WHERE {0}) hh
                                        INNER JOIN(SELECT * FROM CNN25_HoSo WHERE {1}) hs ON hs.iID_MaHoSo = hh.iID_MaHoSo) TB", DKHH, DK);
            cmd.CommandText = SQL;
            DataTable dt = CommonFunction.dtData(cmd, sOrderInput, page, numrecord);
            cmd.Dispose();

            return dt;
        }
        public static int CVBaoCao01_Count(ReportSearchModels model)
        {
            string DK = "1=1", DKHH = "1=1";
            SqlCommand cmd = new SqlCommand();

            DKHH += " AND iID_MaTrangThai=@iID_MaTrangThai";
            cmd.Parameters.AddWithValue("@iID_MaTrangThai", 44);

            if (!string.IsNullOrEmpty(model.TuNgay))
            {
                DKHH += " AND Cast(datediff(day, 0, dSoThongBaoKetQua_NgayKy) as datetime) >= @TuNgayThongBaoKetQua";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@TuNgayThongBaoKetQua", CommonFunction.LayNgayTuXau_YYYYMMDD(model.TuNgay));
            }
            if (!string.IsNullOrEmpty(model.DenNgay))
            {
                DKHH += " AND Cast(datediff(day, 0, dSoThongBaoKetQua_NgayKy) as datetime) <= @DenNgayThongBaoKetQua";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@DenNgayThongBaoKetQua", CommonFunction.LayNgayTuXau_YYYYMMDD(model.DenNgay));
            }

            string SQL = string.Format(@"SELECT * FROM (SELECT COUNT(iID_MaHangHoa) as count FROM(select * from CNN25_HangHoa WHERE {0}) hh
                                        INNER JOIN(SELECT * FROM CNN25_HoSo WHERE {1}) hs ON hs.iID_MaHoSo = hh.iID_MaHoSo) TB", DKHH, DK);
            cmd.CommandText = SQL;
            int vR = Convert.ToInt32(Connection.GetValue(cmd, 0));
            cmd.Dispose();
            return vR;
        }

        public static DataTable CVBaoCao01_Print(ReportSearchModels model)
        {
            string DK = "1=1", DKHH = "1=1";
            SqlCommand cmd = new SqlCommand();

            DKHH += " AND iID_MaTrangThai=@iID_MaTrangThai";
            cmd.Parameters.AddWithValue("@iID_MaTrangThai", 44);

            if (!string.IsNullOrEmpty(model.TuNgay))
            {
                DKHH += " AND Cast(datediff(day, 0, dSoThongBaoKetQua_NgayKy) as datetime) >= @TuNgayThongBaoKetQua";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@TuNgayThongBaoKetQua", CommonFunction.LayNgayTuXau_YYYYMMDD(model.TuNgay));
            }
            if (!string.IsNullOrEmpty(model.DenNgay))
            {
                DKHH += " AND Cast(datediff(day, 0, dSoThongBaoKetQua_NgayKy) as datetime) <= @DenNgayThongBaoKetQua";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@DenNgayThongBaoKetQua", CommonFunction.LayNgayTuXau_YYYYMMDD(model.DenNgay));
            }

            string SQL = string.Format(@"SELECT * FROM (SELECT hh.*,
                                        hs.sLoaiHinhThucKiemTra,hs.sTenDoanhNghiep,hs.sMua_DiaChi,hs.sMua_NoiNhan,hs.sMua_FromDate,hs.sMua_ToDate,hs.dNgayTaoHoSo,hs.sSoTiepNhan,hs.dNgayTiepNhan,hs.sSoGDK,hs.dNgayXacNhan,hs.sBan_NoiXuat
                                        FROM(select * from CNN25_HangHoa WHERE {0}) hh
                                        INNER JOIN(SELECT * FROM CNN25_HoSo WHERE {1}) hs ON hs.iID_MaHoSo = hh.iID_MaHoSo) TB", DKHH, DK);
            cmd.CommandText = SQL;
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();

            return dt;
        }
        #endregion

        #region BaoCao02
        public static DataTable CVBaoCao02(ReportSearchModels model, int page, int numrecord, string sOrderInput = "iID_MaHoSo DESC")
        {
            string DK = "1=1", DKHH = "1=1";
            SqlCommand cmd = new SqlCommand();

            DKHH += " AND iID_MaTrangThai=@iID_MaTrangThai";
            cmd.Parameters.AddWithValue("@iID_MaTrangThai", 44);

            if (!string.IsNullOrEmpty(model.sNuocSanXuat))
            {
                DKHH += " AND sMaQuocGia = @sMaQuocGia";   
                cmd.Parameters.AddWithValue("@sMaQuocGia", model.sNuocSanXuat);
            }
            if (!string.IsNullOrEmpty(model.TuNgay))
            {
                DKHH += " AND Cast(datediff(day, 0, dSoThongBaoKetQua_NgayKy) as datetime) >= @TuNgayThongBaoKetQua";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@TuNgayThongBaoKetQua", CommonFunction.LayNgayTuXau_YYYYMMDD(model.TuNgay));
            }
            if (!string.IsNullOrEmpty(model.DenNgay))
            {
                DKHH += " AND Cast(datediff(day, 0, dSoThongBaoKetQua_NgayKy) as datetime) <= @DenNgayThongBaoKetQua";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@DenNgayThongBaoKetQua", CommonFunction.LayNgayTuXau_YYYYMMDD(model.DenNgay));
            }

            string SQL = string.Format(@"SELECT * FROM (SELECT hh.*,
                                        hs.sLoaiHinhThucKiemTra,hs.sTenDoanhNghiep,hs.sMua_DiaChi,hs.sMua_NoiNhan,hs.sMua_FromDate,hs.sMua_ToDate,hs.dNgayTaoHoSo,hs.sSoTiepNhan,hs.dNgayTiepNhan,hs.sSoGDK,hs.dNgayXacNhan,hs.sBan_NoiXuat
                                        FROM(select * from CNN25_HangHoa WHERE {0}) hh
                                        INNER JOIN(SELECT * FROM CNN25_HoSo WHERE {1}) hs ON hs.iID_MaHoSo = hh.iID_MaHoSo) TB", DKHH, DK);
            cmd.CommandText = SQL;
            DataTable dt = CommonFunction.dtData(cmd, sOrderInput, page, numrecord);
            cmd.Dispose();

            return dt;
        }
        public static int CVBaoCao02_Count(ReportSearchModels model)
        {
            string DK = "1=1", DKHH = "1=1";
            SqlCommand cmd = new SqlCommand();

            DKHH += " AND iID_MaTrangThai=@iID_MaTrangThai";
            cmd.Parameters.AddWithValue("@iID_MaTrangThai", 44);

            if (!string.IsNullOrEmpty(model.sNuocSanXuat))
            {
                DKHH += " AND sMaQuocGia = @sMaQuocGia";
                cmd.Parameters.AddWithValue("@sMaQuocGia", model.sNuocSanXuat);
            }
            if (!string.IsNullOrEmpty(model.TuNgay))
            {
                DKHH += " AND Cast(datediff(day, 0, dSoThongBaoKetQua_NgayKy) as datetime) >= @TuNgayThongBaoKetQua";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@TuNgayThongBaoKetQua", CommonFunction.LayNgayTuXau_YYYYMMDD(model.TuNgay));
            }
            if (!string.IsNullOrEmpty(model.DenNgay))
            {
                DKHH += " AND Cast(datediff(day, 0, dSoThongBaoKetQua_NgayKy) as datetime) <= @DenNgayThongBaoKetQua";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@DenNgayThongBaoKetQua", CommonFunction.LayNgayTuXau_YYYYMMDD(model.DenNgay));
            }

            string SQL = string.Format(@"SELECT * FROM (SELECT COUNT(iID_MaHangHoa) as count FROM(select * from CNN25_HangHoa WHERE {0}) hh
                                        INNER JOIN(SELECT * FROM CNN25_HoSo WHERE {1}) hs ON hs.iID_MaHoSo = hh.iID_MaHoSo) TB", DKHH, DK);
            cmd.CommandText = SQL;
            int vR = Convert.ToInt32(Connection.GetValue(cmd, 0));
            cmd.Dispose();
            return vR;
        }

        public static DataTable CVBaoCao02_Print(ReportSearchModels model)
        {
            string DK = "1=1", DKHH = "1=1";
            SqlCommand cmd = new SqlCommand();

            DKHH += " AND iID_MaTrangThai=@iID_MaTrangThai";
            cmd.Parameters.AddWithValue("@iID_MaTrangThai", 44);

            if (!string.IsNullOrEmpty(model.sNuocSanXuat))
            {
                DKHH += " AND sMaQuocGia = @sMaQuocGia";
                cmd.Parameters.AddWithValue("@sMaQuocGia", model.sNuocSanXuat);
            }
            if (!string.IsNullOrEmpty(model.TuNgay))
            {
                DKHH += " AND Cast(datediff(day, 0, dSoThongBaoKetQua_NgayKy) as datetime) >= @TuNgayThongBaoKetQua";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@TuNgayThongBaoKetQua", CommonFunction.LayNgayTuXau_YYYYMMDD(model.TuNgay));
            }
            if (!string.IsNullOrEmpty(model.DenNgay))
            {
                DKHH += " AND Cast(datediff(day, 0, dSoThongBaoKetQua_NgayKy) as datetime) <= @DenNgayThongBaoKetQua";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@DenNgayThongBaoKetQua", CommonFunction.LayNgayTuXau_YYYYMMDD(model.DenNgay));
            }

            string SQL = string.Format(@"SELECT * FROM (SELECT hh.*,
                                        hs.sLoaiHinhThucKiemTra,hs.sTenDoanhNghiep,hs.sMua_DiaChi,hs.sMua_NoiNhan,hs.sMua_FromDate,hs.sMua_ToDate,hs.dNgayTaoHoSo,hs.sSoTiepNhan,hs.dNgayTiepNhan,hs.sSoGDK,hs.dNgayXacNhan,hs.sBan_NoiXuat
                                        FROM(select * from CNN25_HangHoa WHERE {0}) hh
                                        INNER JOIN(SELECT * FROM CNN25_HoSo WHERE {1}) hs ON hs.iID_MaHoSo = hh.iID_MaHoSo) TB", DKHH, DK);
            cmd.CommandText = SQL;
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();

            return dt;
        }
        #endregion

        #region BaoCao03
        public static DataTable CVBaoCao03(ReportSearchModels model)
        {
            string DK = "1=1";
            SqlCommand cmd = new SqlCommand();

            DK += " AND A.iID_MaTrangThai=@iID_MaTrangThai";
            cmd.Parameters.AddWithValue("@iID_MaTrangThai", 44);

            if (!string.IsNullOrEmpty(model.sNuocSanXuat))
            {
                DK += " AND A.sMaQuocGia = @sMaQuocGia";
                cmd.Parameters.AddWithValue("@sMaQuocGia", model.sNuocSanXuat);
            }
            if (!string.IsNullOrEmpty(model.TuNgay))
            {
                DK += " AND Cast(datediff(day, 0, A.dSoThongBaoKetQua_NgayKy) as datetime) >= @TuNgayThongBaoKetQua";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@TuNgayThongBaoKetQua", CommonFunction.LayNgayTuXau_YYYYMMDD(model.TuNgay));
            }
            if (!string.IsNullOrEmpty(model.DenNgay))
            {
                DK += " AND Cast(datediff(day, 0, A.dSoThongBaoKetQua_NgayKy) as datetime) <= @DenNgayThongBaoKetQua";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@DenNgayThongBaoKetQua", CommonFunction.LayNgayTuXau_YYYYMMDD(model.DenNgay));
            }

            string SQL = string.Format(@"SELECT A.sMaQuocGia, A.sTenQuocGia, SUM(S.rKhoiLuongTan) sKhoiLuongTan, SUM(A.rGiaVN) sGiaTriUSD
                                        FROM CNN25_HangHoa A Inner Join CNN25_HangHoa_SoLuong S On A.iID_MaHangHoa = S.iID_MahangHoa
                                        WHERE {0}
                                        GROUP BY sMaQuocGia, sTenQuocGia ORDER BY sTenQuocGia", DK);
            cmd.CommandText = SQL;
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();

            return dt;
        }
        #endregion

        #region BaoCao04
        public static DataTable CVBaoCao04(ReportSearchModels model)
        {
            string DK = "1=1";
            SqlCommand cmd = new SqlCommand();

            DK += " AND A.iID_MaTrangThai=@iID_MaTrangThai";
            cmd.Parameters.AddWithValue("@iID_MaTrangThai", 44);

            if (!string.IsNullOrEmpty(model.TuNgay))
            {
                DK += " AND Cast(datediff(day, 0, A.dSoThongBaoKetQua_NgayKy) as datetime) >= @TuNgayThongBaoKetQua";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@TuNgayThongBaoKetQua", CommonFunction.LayNgayTuXau_YYYYMMDD(model.TuNgay));
            }
            if (!string.IsNullOrEmpty(model.DenNgay))
            {
                DK += " AND Cast(datediff(day, 0, A.dSoThongBaoKetQua_NgayKy) as datetime) <= @DenNgayThongBaoKetQua";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@DenNgayThongBaoKetQua", CommonFunction.LayNgayTuXau_YYYYMMDD(model.DenNgay));
            }

            string SQL = string.Format(@"SELECT iID_MaToChuc, sTenToChuc, COUNT(iID_MaToChuc) sTongXNCL, SUM(sSoLuong) sSoLuong, SUM(sGiaTriUSD) sGiaTriUSD
                                        FROM CNN25_HoSo_XNCL C INNER JOIN (
                                                SELECT A.iID_MaHangHoa, SUM(S.rSoLuong) sSoLuong, SUM(A.rGiaVN) sGiaTriUSD
                                                        FROM CNN25_HangHoa A Inner Join CNN25_HangHoa_SoLuong S On A.iID_MaHangHoa = S.iID_MahangHoa
                                                        WHERE {0}
                                                        GROUP BY A.iID_MaHangHoa) D ON C.iID_MaHangHoa=D.iID_MaHangHoa
                                        GROUP BY iID_MaToChuc, sTenToChuc
                                        ORDER BY iID_MaToChuc", DK);
            cmd.CommandText = SQL;
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();

            return dt;
        }
        #endregion

        #region BaoCao05
        public static DataTable CVBaoCao05View(ReportSearchModels model, string sMaNhom)
        {
            string DK = "1=1";
            SqlCommand cmd = new SqlCommand();

            DK += " AND A.iID_MaTrangThai=@iID_MaTrangThai AND A.iID_MaNhom=@iID_MaNhom";
            cmd.Parameters.AddWithValue("@iID_MaTrangThai", 44);
            cmd.Parameters.AddWithValue("@iID_MaNhom", sMaNhom);

            if (!string.IsNullOrEmpty(model.TuNgay))
            {
                DK += " AND Cast(datediff(day, 0, A.dSoThongBaoKetQua_NgayKy) as datetime) >= @TuNgayThongBaoKetQua";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@TuNgayThongBaoKetQua", CommonFunction.LayNgayTuXau_YYYYMMDD(model.TuNgay));
            }
            if (!string.IsNullOrEmpty(model.TuNgay))
            {
                DK += " AND Cast(datediff(day, 0, A.dSoThongBaoKetQua_NgayKy) as datetime) <= @DenNgayThongBaoKetQua";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@DenNgayThongBaoKetQua", CommonFunction.LayNgayTuXau_YYYYMMDD(model.DenNgay));
            }

            string SQL = string.Format(@"SELECT A.iID_MaNhom iID_MaNhom, A.iID_MaPhanNhom iID_MaPhanNhom, A.sTenPhanNhom sTenPhanNhom, SUM(S.rSoLuong) sSoLuongTan, SUM(A.rGiaVN) sGiaTriUSD
                                        FROM CNN25_HangHoa A Inner Join CNN25_HangHoa_SoLuong S On A.iID_MaHangHoa = S.iID_MahangHoa
                                        WHERE {0}
                                        GROUP BY iID_MaNhom, iID_MaPhanNhom, sTenPhanNhom ORDER BY iID_MaNhom", DK);
            cmd.CommandText = SQL;
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();

            return dt;
        }
        public static DataTable CVBaoCao05(ReportSearchModels model)
        {
            string DK = "1=1";
            SqlCommand cmd = new SqlCommand();

            DK += " AND A.iID_MaTrangThai=@iID_MaTrangThai";
            cmd.Parameters.AddWithValue("@iID_MaTrangThai", 44);

            if (!string.IsNullOrEmpty(model.TuNgay))
            {
                DK += " AND Cast(datediff(day, 0, A.dSoThongBaoKetQua_NgayKy) as datetime) >= @TuNgayThongBaoKetQua";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@TuNgayThongBaoKetQua", CommonFunction.LayNgayTuXau_YYYYMMDD(model.TuNgay));
            }
            if (!string.IsNullOrEmpty(model.DenNgay))
            {
                DK += " AND Cast(datediff(day, 0, A.dSoThongBaoKetQua_NgayKy) as datetime) <= @DenNgayThongBaoKetQua";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@DenNgayThongBaoKetQua", CommonFunction.LayNgayTuXau_YYYYMMDD(model.DenNgay));
            }

            string SQL = string.Format(@"SELECT A.iID_MaNhom sMa, A.iID_MaPhanNhom iID_MaPhanNhom, A.sTenPhanNhom sTenPhanNhom, SUM(S.rSoLuong) sSoLuongTan, SUM(A.rGiaVN) sGiaTriUSD
                                        FROM CNN25_HangHoa A Inner Join CNN25_HangHoa_SoLuong S On A.iID_MaHangHoa = S.iID_MahangHoa
                                        WHERE {0}
                                        GROUP BY iID_MaNhom, iID_MaPhanNhom, sTenPhanNhom ORDER BY iID_MaNhom", DK);
            cmd.CommandText = SQL;
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();

            return dt;
        }
        #endregion

        #region BaoCao06
        public static DataTable CVBaoCao06(ReportSearchModels model)
        {
            string DK = "1=1";
            SqlCommand cmd = new SqlCommand();

            DK += " AND A.iID_MaTrangThai=@iID_MaTrangThai";
            cmd.Parameters.AddWithValue("@iID_MaTrangThai", 44);

            if (!string.IsNullOrEmpty(model.TuNgay))
            {
                DK += " AND Cast(datediff(day, 0, A.dSoThongBaoKetQua_NgayKy) as datetime) >= @TuNgayThongBaoKetQua";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@TuNgayThongBaoKetQua", CommonFunction.LayNgayTuXau_YYYYMMDD(model.TuNgay));
            }
            if (!string.IsNullOrEmpty(model.DenNgay))
            {
                DK += " AND Cast(datediff(day, 0, A.dSoThongBaoKetQua_NgayKy) as datetime) <= @DenNgayThongBaoKetQua";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@DenNgayThongBaoKetQua", CommonFunction.LayNgayTuXau_YYYYMMDD(model.DenNgay));
            }

            string SQL = string.Format(@"SELECT A.iID_MaNhom, A.sTenNhom, SUM(S.rKhoiLuongTan) sKhoiLuongTan, SUM(A.rGiaVN) sGiaTriUSD
                                        FROM CNN25_HangHoa A Inner Join CNN25_HangHoa_SoLuong S On A.iID_MaHangHoa = S.iID_MahangHoa
                                        WHERE {0}
                                        GROUP BY iID_MaNhom, sTenNhom ORDER BY iID_MaNhom", DK);
            cmd.CommandText = SQL;
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();

            return dt;
        }
        #endregion

        #region BaoCao07
        
        #endregion

        #region BaoCao08
        public static DataTable CVBaoCao08(ReportSearchModels model)
        {
            string DK = "1=1";
            SqlCommand cmd = new SqlCommand();

            DK += " AND A.iID_MaTrangThai=@iID_MaTrangThai";
            cmd.Parameters.AddWithValue("@iID_MaTrangThai", 44);

            if (!string.IsNullOrEmpty(model.TuNgay))
            {
                DK += " AND Cast(datediff(day, 0, A.dSoThongBaoKetQua_NgayKy) as datetime) >= @TuNgayThongBaoKetQua";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@TuNgayThongBaoKetQua", CommonFunction.LayNgayTuXau_YYYYMMDD(model.TuNgay));
            }
            if (!string.IsNullOrEmpty(model.DenNgay))
            {
                DK += " AND Cast(datediff(day, 0, A.dSoThongBaoKetQua_NgayKy) as datetime) <= @DenNgayThongBaoKetQua";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@DenNgayThongBaoKetQua", CommonFunction.LayNgayTuXau_YYYYMMDD(model.DenNgay));
            }

            string SQL = string.Format(@"SELECT A.sMaQuocGia, A.sTenQuocGia, SUM(S.rKhoiLuongTan) sKhoiLuongTan, SUM(A.rGiaVN) sGiaTriUSD, COUNT(sMaHoSo) sSoLo
                                        FROM CNN25_HangHoa A Inner Join CNN25_HangHoa_SoLuong S On A.iID_MaHangHoa = S.iID_MahangHoa
                                        WHERE {0}
                                        GROUP BY sMaQuocGia, sTenQuocGia ORDER BY sTenQuocGia", DK);
            cmd.CommandText = SQL;
            DataTable dt = Connection.GetDataTable(cmd);
            cmd.Dispose();

            return dt;
        }
        #endregion
    }
}
