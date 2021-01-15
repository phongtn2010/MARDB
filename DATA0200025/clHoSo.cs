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
    public class clHoSo
    {
        public static HoSoModels GetHoSoById(string iID_MaHoSo)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT *  FROM CNN25_HoSo 
                            WHERE iID_MaHoSo=@iID_MaHoSo";
                HoSoModels results = connect.Query<HoSoModels>(SQL, new { iID_MaHoSo = iID_MaHoSo }).FirstOrDefault();
                return results;
            }
        }
        public static HoSoModels GetHoSoById(long iID_MaHoSo)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT *  FROM CNN25_HoSo 
                            WHERE iID_MaHoSo=@iID_MaHoSo";
                HoSoModels results = connect.Query<HoSoModels>(SQL, new { iID_MaHoSo = iID_MaHoSo }).FirstOrDefault();
                return results;
            }
        }

        public static HoSoModels GetHoSo_ChiTiet_Theo_Ma(string sMaHoSo)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT * FROM CNN25_HoSo 
                            WHERE sMaHoSo=@sMaHoSo";
                HoSoModels results = connect.Query<HoSoModels>(SQL, new { sMaHoSo = sMaHoSo }).FirstOrDefault();
                return results;
            }
        }

        public static int GetThongKe_BieuDo_Ngay(string sNgay)
        {
            int vR = 0;
            try
            {
                String SQL = "SELECT COUNT(*) AS TongSo FROM CNN25_HoSo WHERE Cast(datediff(day, 0, dNgayTao) as datetime) >= @dNgayTao";   // _FromDate = 'yyyy-MM-dd'

                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@dNgayTao", CommonFunction.LayNgayTuXau_YYYYMMDD(sNgay));
                vR = Convert.ToInt32(Connection.GetValue(cmd, 0, CThamSo.iKetNoi));
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                vR = -1;
            }
            return vR;
        }
        public static int GetCount_Dashboard(int LoaiDanhSach, int iID_MaTrangThai)
        {
            SqlCommand cmd = new SqlCommand();
            string DK = "1=1";
            List<TrangThaiModels> TrangThais;
            switch (LoaiDanhSach)
            {
                case 1:
                    TrangThais = BPMC_ChoTiepNhanHoSo();
                    break;
                case 2:
                    TrangThais = BPMC_ChoTiepNhanHoSoGuiBoSung();
                    break;
                case 3:
                    TrangThais = BPMC_DaTuChoiXacNhanGDK();
                    break;
                case 4:
                    TrangThais = BPMC_PhongTACNYeuCauBoSungHoSo();
                    break;
                case 5:
                    TrangThais = BPMC_DaPheDuyetGDK();
                    break;
                case 6:
                    TrangThais = BPMC_DaTraGDK();
                    break;
                case 7:
                    TrangThais = ChuyenVien_ChoXuLyHoSo();
                    break;
                case 8:
                    TrangThais = ChuyenVien_ChoXuLyKetQua();
                    break;
                case 10:
                    TrangThais = LDP_ChoXemXetHoSo();
                    break;
                case 11:
                    TrangThais = LDP_ChoXemXetKetQua();
                    break;
                case 12:
                    TrangThais = LDC_ChoXacNhanGDK();
                    break;
                case 13:
                    TrangThais = LDC_ChoPheDuyetKetQua();
                    break;
                case 14:
                    TrangThais = BPMC_ChoTiepNhanKetQua();
                    break;
                case 15:
                    TrangThais = BPMC_ChoTiepNhanKetQuaGuiBoSung();
                    break;
                case 16:
                    TrangThais = BPMC_PhongTACNYeuCauBoSungKetQua();
                    break;
                case 17:
                    TrangThais = BPMC_DaPheDuyetThongBaoKetQua();
                    break;
                case 18:
                    TrangThais = BPMC_DaCapThongBaoKetQua();
                    break;
                case 50:
                    TrangThais = ToChucChiDinh(null);
                    break;
                case 51:
                    TrangThais = ToChucChiDinh(27);
                    break;
                case 52:
                    TrangThais = ToChucChiDinh(28);
                    break;
                default:
                    TrangThais = new List<TrangThaiModels>();
                    break;
            }
            TrangThaiModels trangThai;
            if (iID_MaTrangThai == 0)
            {
                string sTrangThai = "";
                for (int i = 0; i < TrangThais.Count; i++)
                {
                    trangThai = TrangThais[i];

                    if (sTrangThai.Length > 0) sTrangThai += ",";
                    sTrangThai += trangThai.iID_MaTrangThai;

                }
                if (sTrangThai.Length > 0)
                {
                    DK += string.Format(" AND iID_MaTrangThai IN({0})", sTrangThai);
                }
            }
            else
            {
                if (LoaiDanhSach == 10 || LoaiDanhSach == 7)//Ho so ldp=10 ; chuyên viên=7
                {
                    switch (iID_MaTrangThai)
                    {
                        case 10://ldp-Chuyenvien
                        case 11:
                        case 12:
                            DK += " AND (iID_MaTrangThai=@iID_MaTrangThai OR iID_MaTrangThai=@iID_MaTrangThai1 OR  iID_MaTrangThai=@iID_MaTrangThai2)";
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai", 10);
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai1", 11);
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai2", 12);
                            break;
                        case 14://Chuyên vien
                        case 18:
                        case 23:
                            DK += " AND (iID_MaTrangThai=@iID_MaTrangThai OR iID_MaTrangThai=@iID_MaTrangThai1 OR  iID_MaTrangThai=@iID_MaTrangThai2)";
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai", 14);
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai1", 16);
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai2", 23);
                            break;
                        case 17:
                        case 22:
                            DK += " AND (iID_MaTrangThai=@iID_MaTrangThai OR iID_MaTrangThai=@iID_MaTrangThai1)";
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai", 17);
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai1", 22);
                            break;
                        case 20:
                        case 25:
                            DK += " AND (iID_MaTrangThai=@iID_MaTrangThai OR iID_MaTrangThai=@iID_MaTrangThai1)";
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai", 20);
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai1", 25);
                            break;
                        default:
                            DK += " AND iID_MaTrangThai=@iID_MaTrangThai";
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai", iID_MaTrangThai);
                            break;
                    }


                }
                else
                {
                    DK += " AND iID_MaTrangThai=@iID_MaTrangThai";
                    cmd.Parameters.AddWithValue("@iID_MaTrangThai", iID_MaTrangThai);
                }
            }
                        
            string SQL = string.Format("SELECT count(iID_MaHoSo) as value FROM CNN25_HoSo WHERE {0} ", DK);
            cmd.CommandText = SQL;
            int vR = Convert.ToInt32(Connection.GetValue(cmd, 0));
            cmd.Dispose();
            return vR;
        }
        public static int GetCount(sHoSoModels models)
        {
            SqlCommand cmd = new SqlCommand();
            string DK = "1=1";
            List<TrangThaiModels> TrangThais;
            switch (models.LoaiDanhSach)
            {
                case 1:
                    TrangThais = BPMC_ChoTiepNhanHoSo();
                    break;
                case 2:
                    TrangThais = BPMC_ChoTiepNhanHoSoGuiBoSung();
                    break;
                case 3:
                    TrangThais = BPMC_DaTuChoiXacNhanGDK();
                    break;
                case 4:
                    TrangThais = BPMC_PhongTACNYeuCauBoSungHoSo();
                    break;
                case 5:
                    TrangThais = BPMC_DaPheDuyetGDK();
                    break;
                case 6:
                    TrangThais = BPMC_DaTraGDK();
                    break;
                case 7:
                    TrangThais = ChuyenVien_ChoXuLyHoSo();
                    break;
                case 8:
                    TrangThais = ChuyenVien_ChoXuLyKetQua();
                    break;
                case 10:
                    TrangThais = LDP_ChoXemXetHoSo();
                    break;
                case 11:
                    TrangThais = LDP_ChoXemXetKetQua();
                    break;
                case 12:
                    TrangThais = LDC_ChoXacNhanGDK();
                    break;
                case 13:
                    TrangThais = LDC_ChoPheDuyetKetQua();
                    break;
                case 14:
                    TrangThais = BPMC_ChoTiepNhanKetQua();
                    break;
                case 15:
                    TrangThais = BPMC_ChoTiepNhanKetQuaGuiBoSung();
                    break;
                case 16:
                    TrangThais = BPMC_PhongTACNYeuCauBoSungKetQua();
                    break;
                case 17:
                    TrangThais = BPMC_DaPheDuyetThongBaoKetQua();
                    break;
                case 18:
                    TrangThais = BPMC_DaCapThongBaoKetQua();
                    break;
                case 50:
                    TrangThais = ToChucChiDinh(null);
                    break;
                case 51:
                    TrangThais = ToChucChiDinh(27);
                    break;
                case 52:
                    TrangThais = ToChucChiDinh(28);
                    break;
                default:
                    TrangThais = new List<TrangThaiModels>();
                    break;
            }
            TrangThaiModels trangThai;
            if (models.iID_MaTrangThai == 0)
            {
                string sTrangThai = "";
                for (int i = 0; i < TrangThais.Count; i++)
                {
                    trangThai = TrangThais[i];

                    if (sTrangThai.Length > 0) sTrangThai += ",";
                    sTrangThai += trangThai.iID_MaTrangThai;

                }
                if (sTrangThai.Length > 0)
                {
                    DK += string.Format(" AND iID_MaTrangThai IN({0})", sTrangThai);
                }
            }
            else
            {
                if (models.LoaiDanhSach == 10|| models.LoaiDanhSach == 7)//Ho so ldp=10 ; chuyên viên=7
                {
                    switch (models.iID_MaTrangThai)
                    {
                        case 10://ldp-Chuyenvien
                        case 11:
                        case 12:
                            DK += " AND (iID_MaTrangThai=@iID_MaTrangThai OR iID_MaTrangThai=@iID_MaTrangThai1 OR  iID_MaTrangThai=@iID_MaTrangThai2)";
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai", 10);
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai1", 11);
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai2", 12);
                            break;
                        case 14://Chuyên vien
                        case 18:
                        case 23:
                            DK += " AND (iID_MaTrangThai=@iID_MaTrangThai OR iID_MaTrangThai=@iID_MaTrangThai1 OR  iID_MaTrangThai=@iID_MaTrangThai2)";
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai", 14);
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai1", 16);
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai2", 23);
                            break;
                        case 17:
                        case 22:
                            DK += " AND (iID_MaTrangThai=@iID_MaTrangThai OR iID_MaTrangThai=@iID_MaTrangThai1)";
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai", 17);
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai1", 22);
                            break;
                        case 20:
                        case 25:
                            DK += " AND (iID_MaTrangThai=@iID_MaTrangThai OR iID_MaTrangThai=@iID_MaTrangThai1)";
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai", 20);
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai1", 25);
                            break;
                        default:
                            DK += " AND iID_MaTrangThai=@iID_MaTrangThai";
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai", models.iID_MaTrangThai);
                            break;
                    }


                }
                else
                {
                    DK += " AND iID_MaTrangThai=@iID_MaTrangThai";
                    cmd.Parameters.AddWithValue("@iID_MaTrangThai", models.iID_MaTrangThai);
                }
            }

            if (!string.IsNullOrEmpty(models.sMaHoSo))
            {
                DK += " AND sMaHoSo LIKE @sMaHoSo";
                cmd.Parameters.AddWithValue("@sMaHoSo", "%" + models.sMaHoSo.Trim() + "%");
            }
            if (!string.IsNullOrEmpty(models.sMaSoThue))
            {
                DK += " AND sMaSoThue LIKE @sMaSoThue";
                cmd.Parameters.AddWithValue("@sMaSoThue", "%" + models.sMaSoThue.Trim() + "%");
            }
            if (!string.IsNullOrEmpty(models.sTenDoanhNghiep))
            {
                DK += " AND sTenDoanhNghiep LIKE @sTenDoanhNghiep";
                cmd.Parameters.AddWithValue("@sTenDoanhNghiep", "%" + models.sTenDoanhNghiep.Trim() + "%");
            }

            if (!string.IsNullOrEmpty(models.TuNgayDen))
            {
                //DK += " AND dNgayTaoHoSo >= @dTuNgay";
                //cmd.Parameters.AddWithValue("@dTuNgay", CommonFunction.LayNgayTuXau(models.TuNgayDen));

                DK += " AND Cast(datediff(day, 0, dNgayTaoHoSo) as datetime) >= @dTuNgay";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@dTuNgay", CommonFunction.LayNgayTuXau_YYYYMMDD(models.TuNgayDen));
            }
            if (!string.IsNullOrEmpty(models.DenNgayDen))
            {
                //DK += " AND dNgayTaoHoSo <= @dDenNgay";
                //cmd.Parameters.AddWithValue("@dDenNgay", CommonFunction.LayNgayTuXau(models.DenNgayDen));

                DK += " AND Cast(datediff(day, 0, dNgayTaoHoSo) as datetime) <= @dDenNgay";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@dDenNgay", CommonFunction.LayNgayTuXau_YYYYMMDD(models.DenNgayDen));
            }

            if (!string.IsNullOrEmpty(models.TuNgayTiepNhan))
            {
                //DK += " AND dNgayTiepNhan >= @TuNgayTiepNhan";
                //cmd.Parameters.AddWithValue("@TuNgayTiepNhan", CommonFunction.LayNgayTuXau(models.TuNgayTiepNhan));

                DK += " AND Cast(datediff(day, 0, dNgayTiepNhan) as datetime) >= @TuNgayTiepNhan";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@TuNgayTiepNhan", CommonFunction.LayNgayTuXau_YYYYMMDD(models.TuNgayTiepNhan));
            }
            if (!string.IsNullOrEmpty(models.DenNgayTiepNhan))
            {
                //DK += " AND dNgayTiepNhan <= @DenNgayTiepNhan";
                //cmd.Parameters.AddWithValue("@DenNgayTiepNhan", CommonFunction.LayNgayTuXau(models.DenNgayTiepNhan));

                DK += " AND Cast(datediff(day, 0, dNgayTiepNhan) as datetime) <= @DenNgayTiepNhan";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@DenNgayTiepNhan", CommonFunction.LayNgayTuXau_YYYYMMDD(models.DenNgayTiepNhan));
            }

            if (!string.IsNullOrEmpty(models.TuNgayXacNhan))
            {
                //DK += " AND dNgayXacNhan >= @TuNgayXacNhan";
                //cmd.Parameters.AddWithValue("@TuNgayXacNhan", CommonFunction.LayNgayTuXau(models.TuNgayXacNhan));

                DK += " AND Cast(datediff(day, 0, dNgayXacNhan) as datetime) >= @TuNgayXacNhan";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@TuNgayXacNhan", CommonFunction.LayNgayTuXau_YYYYMMDD(models.TuNgayXacNhan));
            }
            if (!string.IsNullOrEmpty(models.DenNgayXacNhan))
            {
                //DK += " AND dNgayXacNhan <= @DenNgayXacNhan";
                //cmd.Parameters.AddWithValue("@DenNgayXacNhan", CommonFunction.LayNgayTuXau(models.DenNgayXacNhan));

                DK += " AND Cast(datediff(day, 0, dNgayXacNhan) as datetime) <= @DenNgayXacNhan";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@DenNgayXacNhan", CommonFunction.LayNgayTuXau_YYYYMMDD(models.DenNgayXacNhan));
            }

            if (models.iID_KetQuaXuLy > 0)
            {
                DK += " AND iID_KetQuaXuLy = @iID_KetQuaXuLy";
                cmd.Parameters.AddWithValue("@iID_KetQuaXuLy", models.iID_KetQuaXuLy);
            }
            if (!string.IsNullOrEmpty(models.sTenTACN))
            {
                DK += " AND sTenTACN LIKE @sTenTACN";
                cmd.Parameters.AddWithValue("@sTenTACN", "%" + models.sTenTACN.Trim() + "%");
            }

            if (!string.IsNullOrEmpty(models.sSoTiepNhan))
            {
                DK += " AND sSoTiepNhan LIKE @sSoTiepNhan";
                cmd.Parameters.AddWithValue("@sSoTiepNhan", "%" + models.sSoTiepNhan.Trim() + "%");
            }

            if (!string.IsNullOrEmpty(models.sSoGDK))
            {
                DK += " AND sSoGDK LIKE @sSoGDK";
                cmd.Parameters.AddWithValue("@sSoGDK", "%" + models.sSoGDK.Trim() + "%");
            }
            string SQL = string.Format("SELECT count(iID_MaHoSo) as value FROM CNN25_HoSo WHERE {0} ", DK);
            cmd.CommandText = SQL;
            int vR = Convert.ToInt32(Connection.GetValue(cmd, 0));
            cmd.Dispose();
            return vR;
        }
        public static DataTable GetDataTable(sHoSoModels models, int page, int numrecord, string sOrderInput = "iID_MaHoSo DESC")
        {
            SqlCommand cmd = new SqlCommand();
            string DK = "1=1";
            List<TrangThaiModels> TrangThais;
            switch (models.LoaiDanhSach)
            {
                case 1:
                    TrangThais = BPMC_ChoTiepNhanHoSo();
                    break;
                case 2:
                    TrangThais = BPMC_ChoTiepNhanHoSoGuiBoSung();
                    break;
                case 3:
                    TrangThais = BPMC_DaTuChoiXacNhanGDK();
                    break;
                case 4:
                    TrangThais = BPMC_PhongTACNYeuCauBoSungHoSo();
                    break;
                case 5:
                    TrangThais = BPMC_DaPheDuyetGDK();
                    break;
                case 6:
                    TrangThais = BPMC_DaTraGDK();
                    break;
                case 7:
                    TrangThais = ChuyenVien_ChoXuLyHoSo();
                    break;
                case 8:
                    TrangThais = ChuyenVien_ChoXuLyKetQua();
                    break;
                case 10:
                    TrangThais = LDP_ChoXemXetHoSo();
                    break;
                case 11:
                    TrangThais = LDP_ChoXemXetKetQua();
                    break;
                case 12:
                    TrangThais = LDC_ChoXacNhanGDK();
                    break;
                case 13:
                    TrangThais = LDC_ChoPheDuyetKetQua();
                    break;
                case 14:
                    TrangThais = BPMC_ChoTiepNhanKetQua();
                    break;
                case 15:
                    TrangThais = BPMC_ChoTiepNhanKetQuaGuiBoSung();
                    break;
                case 16:
                    TrangThais = BPMC_PhongTACNYeuCauBoSungKetQua();
                    break;
                case 17:
                    TrangThais = BPMC_DaPheDuyetThongBaoKetQua();
                    break;
                case 18:
                    TrangThais = BPMC_DaCapThongBaoKetQua();
                    break;
                case 50:
                    TrangThais = ToChucChiDinh(null);
                    break;
                case 51:
                    TrangThais = ToChucChiDinh(27);
                    break;
                case 52:
                    TrangThais = ToChucChiDinh(28);
                    break;
                default:
                    TrangThais = new List<TrangThaiModels>();
                    break;
            }
            TrangThaiModels trangThai;
            String sTrangThai = "";
            if (models.iID_MaTrangThai == 0)
            {
                for (int i = 0; i < TrangThais.Count; i++)
                {
                    trangThai = TrangThais[i];

                    if (sTrangThai.Length > 0) sTrangThai += ",";
                    sTrangThai += trangThai.iID_MaTrangThai;

                }
                if (sTrangThai.Length > 0)
                {
                    DK += string.Format(" AND iID_MaTrangThai IN({0})", sTrangThai);
                }
            }
            else
            {
                if (models.LoaiDanhSach == 10 || models.LoaiDanhSach == 7)//Ho so ldp=10 ; chuyên viên=7
                {
                    switch (models.iID_MaTrangThai)
                    {
                        case 10://ldp-Chuyenvien
                        case 11:
                        case 12:
                            DK += " AND (iID_MaTrangThai=@iID_MaTrangThai OR iID_MaTrangThai=@iID_MaTrangThai1 OR  iID_MaTrangThai=@iID_MaTrangThai2)";
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai", 10);
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai1", 11);
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai2", 12);
                            break;
                        case 14://Chuyên vien
                        case 18:
                        case 23:
                            DK += " AND (iID_MaTrangThai=@iID_MaTrangThai OR iID_MaTrangThai=@iID_MaTrangThai1 OR  iID_MaTrangThai=@iID_MaTrangThai2)";
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai", 14);
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai1", 16);
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai2", 23);
                            break;
                        case 17:
                        case 22:
                            DK += " AND (iID_MaTrangThai=@iID_MaTrangThai OR iID_MaTrangThai=@iID_MaTrangThai1)";
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai", 17);
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai1", 22);
                            break;
                        case 20:
                        case 25:
                            DK += " AND (iID_MaTrangThai=@iID_MaTrangThai OR iID_MaTrangThai=@iID_MaTrangThai1)";
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai", 20);
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai1", 25);
                            break;
                        default:
                            DK += " AND iID_MaTrangThai=@iID_MaTrangThai";
                            cmd.Parameters.AddWithValue("@iID_MaTrangThai", models.iID_MaTrangThai);
                            break;
                    }
                }
                else
                {
                    DK += " AND iID_MaTrangThai=@iID_MaTrangThai";
                    cmd.Parameters.AddWithValue("@iID_MaTrangThai", models.iID_MaTrangThai);
                }
            }
            if (!string.IsNullOrEmpty(models.sMaHoSo))
            {
                DK += " AND sMaHoSo LIKE @sMaHoSo";
                cmd.Parameters.AddWithValue("@sMaHoSo", "%" + models.sMaHoSo.Trim() + "%");
            }
            if (!string.IsNullOrEmpty(models.sMaSoThue))
            {
                DK += " AND sMaSoThue LIKE @sMaSoThue";
                cmd.Parameters.AddWithValue("@sMaSoThue", "%" + models.sMaSoThue.Trim() + "%");
            }
            if (!string.IsNullOrEmpty(models.sTenDoanhNghiep))
            {
                DK += " AND sTenDoanhNghiep LIKE @sTenDoanhNghiep";
                cmd.Parameters.AddWithValue("@sTenDoanhNghiep", "%" + models.sTenDoanhNghiep.Trim() + "%");
            }

            if (!string.IsNullOrEmpty(models.TuNgayDen))
            {
                //DK += " AND dNgayTaoHoSo >= @dTuNgay";
                //cmd.Parameters.AddWithValue("@dTuNgay", CommonFunction.LayNgayTuXau(models.TuNgayDen));

                DK += " AND Cast(datediff(day, 0, dNgayTaoHoSo) as datetime) >= @dTuNgay";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@dTuNgay", CommonFunction.LayNgayTuXau_YYYYMMDD(models.TuNgayDen));
            }
            if (!string.IsNullOrEmpty(models.DenNgayDen))
            {
                //DK += " AND dNgayTaoHoSo <= @dDenNgay";
                //cmd.Parameters.AddWithValue("@dDenNgay", CommonFunction.LayNgayTuXau(models.DenNgayDen));

                DK += " AND Cast(datediff(day, 0, dNgayTaoHoSo) as datetime) <= @dDenNgay";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@dDenNgay", CommonFunction.LayNgayTuXau_YYYYMMDD(models.DenNgayDen));
            }

            if (!string.IsNullOrEmpty(models.TuNgayTiepNhan))
            {
                //DK += " AND dNgayTiepNhan >= @TuNgayTiepNhan";
                //cmd.Parameters.AddWithValue("@TuNgayTiepNhan", CommonFunction.LayNgayTuXau(models.TuNgayTiepNhan));

                DK += " AND Cast(datediff(day, 0, dNgayTiepNhan) as datetime) >= @TuNgayTiepNhan";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@TuNgayTiepNhan", CommonFunction.LayNgayTuXau_YYYYMMDD(models.TuNgayTiepNhan));
            }
            if (!string.IsNullOrEmpty(models.DenNgayTiepNhan))
            {
                //DK += " AND dNgayTiepNhan <= @DenNgayTiepNhan";
                //cmd.Parameters.AddWithValue("@DenNgayTiepNhan", CommonFunction.LayNgayTuXau(models.DenNgayTiepNhan));

                DK += " AND Cast(datediff(day, 0, dNgayTiepNhan) as datetime) <= @DenNgayTiepNhan";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@DenNgayTiepNhan", CommonFunction.LayNgayTuXau_YYYYMMDD(models.DenNgayTiepNhan));
            }

            if (!string.IsNullOrEmpty(models.TuNgayXacNhan))
            {
                //DK += " AND dNgayXacNhan >= @TuNgayXacNhan";
                //cmd.Parameters.AddWithValue("@TuNgayXacNhan", CommonFunction.LayNgayTuXau(models.TuNgayXacNhan));

                DK += " AND Cast(datediff(day, 0, dNgayXacNhan) as datetime) >= @TuNgayXacNhan";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@TuNgayXacNhan", CommonFunction.LayNgayTuXau_YYYYMMDD(models.TuNgayXacNhan));
            }
            if (!string.IsNullOrEmpty(models.DenNgayXacNhan))
            {
                //DK += " AND dNgayXacNhan <= @DenNgayXacNhan";
                //cmd.Parameters.AddWithValue("@DenNgayXacNhan", CommonFunction.LayNgayTuXau(models.DenNgayXacNhan));

                DK += " AND Cast(datediff(day, 0, dNgayXacNhan) as datetime) <= @DenNgayXacNhan";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@DenNgayXacNhan", CommonFunction.LayNgayTuXau_YYYYMMDD(models.DenNgayXacNhan));
            }

            if (models.iID_KetQuaXuLy>0)
            {
                DK += " AND iID_KetQuaXuLy = @iID_KetQuaXuLy";
                cmd.Parameters.AddWithValue("@iID_KetQuaXuLy", models.iID_KetQuaXuLy);
            }
            if (!string.IsNullOrEmpty(models.sTenTACN))
            {
                DK += " AND sTenTACN LIKE @sTenTACN";
                cmd.Parameters.AddWithValue("@sTenTACN", "%" + models.sTenTACN.Trim() + "%");
            }
                        
            if (!string.IsNullOrEmpty(models.sSoTiepNhan))
            {
                DK += " AND sSoTiepNhan LIKE @sSoTiepNhan";
                cmd.Parameters.AddWithValue("@sSoTiepNhan", "%" + models.sSoTiepNhan.Trim() + "%");
            }

            if (!string.IsNullOrEmpty(models.sSoGDK))
            {
                DK += " AND sSoGDK LIKE @sSoGDK";
                cmd.Parameters.AddWithValue("@sSoGDK", "%" + models.sSoGDK.Trim() + "%");
            }
            string SQL = string.Format("SELECT * FROM CNN25_HoSo WHERE {0} ", DK);
            cmd.CommandText = SQL;
            string sOrder = sOrderInput;
            DataTable dt = CommonFunction.dtData(cmd, sOrder, page, numrecord);
            cmd.Dispose();
            return dt;
        }


        public static int GetCount_TCCD(sHoSoModels models, string iID_MaToChuc)
        {
            SqlCommand cmd = new SqlCommand();
            string DK = "1=1", DKHH = "1=1";
            List<TrangThaiModels> TrangThais;
            switch (models.LoaiDanhSach)
            {
                case 1:
                    TrangThais = BPMC_ChoTiepNhanHoSo();
                    break;
                case 2:
                    TrangThais = BPMC_ChoTiepNhanHoSoGuiBoSung();
                    break;
                case 3:
                    TrangThais = BPMC_DaTuChoiXacNhanGDK();
                    break;
                case 4:
                    TrangThais = BPMC_PhongTACNYeuCauBoSungHoSo();
                    break;
                case 5:
                    TrangThais = BPMC_DaPheDuyetGDK();
                    break;
                case 6:
                    TrangThais = BPMC_DaTraGDK();
                    break;
                case 7:
                    TrangThais = ChuyenVien_ChoXuLyHoSo();
                    break;
                case 8:
                    TrangThais = ChuyenVien_ChoXuLyKetQua();
                    break;
                case 10:
                    TrangThais = LDP_ChoXemXetHoSo();
                    break;
                case 11:
                    TrangThais = LDP_ChoXemXetKetQua();
                    break;
                case 12:
                    TrangThais = LDC_ChoXacNhanGDK();
                    break;
                case 13:
                    TrangThais = LDC_ChoPheDuyetKetQua();
                    break;
                case 14:
                    TrangThais = BPMC_ChoTiepNhanKetQua();
                    break;
                case 15:
                    TrangThais = BPMC_ChoTiepNhanKetQuaGuiBoSung();
                    break;
                case 16:
                    TrangThais = BPMC_PhongTACNYeuCauBoSungKetQua();
                    break;
                case 17:
                    TrangThais = BPMC_DaPheDuyetThongBaoKetQua();
                    break;
                case 18:
                    TrangThais = BPMC_DaCapThongBaoKetQua();
                    break;
                case 50:
                    TrangThais = ToChucChiDinh(null);
                    break;
                case 51:
                    TrangThais = ToChucChiDinh(27);
                    break;
                case 52:
                    TrangThais = ToChucChiDinh(28);
                    break;
                default:
                    TrangThais = new List<TrangThaiModels>();
                    break;
            }
            TrangThaiModels trangThai;
            if (models.iID_MaTrangThai == 0)
            {
                string sTrangThai = "";
                for (int i = 0; i < TrangThais.Count; i++)
                {
                    trangThai = TrangThais[i];

                    if (sTrangThai.Length > 0) sTrangThai += ",";
                    sTrangThai += trangThai.iID_MaTrangThai;

                }
                if (sTrangThai.Length > 0)
                {
                    DK += string.Format(" AND iID_MaTrangThai IN({0})", sTrangThai);
                }
            }
            else
            {
                DK += " AND iID_MaTrangThai=@iID_MaTrangThai";
                cmd.Parameters.AddWithValue("@iID_MaTrangThai", models.iID_MaTrangThai);
            }
            if (String.IsNullOrEmpty(iID_MaToChuc) == false)
            {
                DKHH += " AND iID_MaToChuc=@iID_MaToChuc";
                cmd.Parameters.AddWithValue("@iID_MaToChuc", iID_MaToChuc);
            }
            if (!string.IsNullOrEmpty(models.sMaHoSo))
            {
                DK += " AND sMaHoSo LIKE @sMaHoSo";
                cmd.Parameters.AddWithValue("@sMaHoSo", "%" + models.sMaHoSo.Trim() + "%");
            }
            if (!string.IsNullOrEmpty(models.sMaSoThue))
            {
                DK += " AND sMaSoThue LIKE @sMaSoThue";
                cmd.Parameters.AddWithValue("@sMaSoThue", "%" + models.sMaSoThue.Trim() + "%");
            }
            if (!string.IsNullOrEmpty(models.sTenDoanhNghiep))
            {
                DK += " AND sTenDoanhNghiep LIKE @sTenDoanhNghiep";
                cmd.Parameters.AddWithValue("@sTenDoanhNghiep", "%" + models.sTenDoanhNghiep + "%");
            }

            if (!string.IsNullOrEmpty(models.TuNgayDen))
            {
                //DK += " AND dNgayTaoHoSo >= @dTuNgay";
                //cmd.Parameters.AddWithValue("@dTuNgay", CommonFunction.LayNgayTuXau(models.TuNgayDen));

                DK += " AND Cast(datediff(day, 0, dNgayTaoHoSo) as datetime) >= @dTuNgay";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@dTuNgay", CommonFunction.LayNgayTuXau_YYYYMMDD(models.TuNgayDen));
            }
            if (!string.IsNullOrEmpty(models.DenNgayDen))
            {
                //DK += " AND dNgayTaoHoSo <= @dDenNgay";
                //cmd.Parameters.AddWithValue("@dDenNgay", CommonFunction.LayNgayTuXau(models.DenNgayDen));

                DK += " AND Cast(datediff(day, 0, dNgayTaoHoSo) as datetime) <= @dDenNgay";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@dDenNgay", CommonFunction.LayNgayTuXau_YYYYMMDD(models.DenNgayDen));
            }

            if (!string.IsNullOrEmpty(models.TuNgayTiepNhan))
            {
                //DK += " AND dNgayTiepNhan >= @TuNgayTiepNhan";
                //cmd.Parameters.AddWithValue("@TuNgayTiepNhan", CommonFunction.LayNgayTuXau(models.TuNgayTiepNhan));

                DK += " AND Cast(datediff(day, 0, dNgayTiepNhan) as datetime) >= @TuNgayTiepNhan";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@TuNgayTiepNhan", CommonFunction.LayNgayTuXau_YYYYMMDD(models.TuNgayTiepNhan));
            }
            if (!string.IsNullOrEmpty(models.DenNgayTiepNhan))
            {
                //DK += " AND dNgayTiepNhan <= @DenNgayTiepNhan";
                //cmd.Parameters.AddWithValue("@DenNgayTiepNhan", CommonFunction.LayNgayTuXau(models.DenNgayTiepNhan));

                DK += " AND Cast(datediff(day, 0, dNgayTiepNhan) as datetime) <= @DenNgayTiepNhan";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@DenNgayTiepNhan", CommonFunction.LayNgayTuXau_YYYYMMDD(models.DenNgayTiepNhan));
            }

            if (!string.IsNullOrEmpty(models.TuNgayXacNhan))
            {
                //DK += " AND dNgayXacNhan >= @TuNgayXacNhan";
                //cmd.Parameters.AddWithValue("@TuNgayXacNhan", CommonFunction.LayNgayTuXau(models.TuNgayXacNhan));

                DK += " AND Cast(datediff(day, 0, dNgayXacNhan) as datetime) >= @TuNgayXacNhan";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@TuNgayXacNhan", CommonFunction.LayNgayTuXau_YYYYMMDD(models.TuNgayXacNhan));
            }
            if (!string.IsNullOrEmpty(models.DenNgayXacNhan))
            {
                //DK += " AND dNgayXacNhan <= @DenNgayXacNhan";
                //cmd.Parameters.AddWithValue("@DenNgayXacNhan", CommonFunction.LayNgayTuXau(models.DenNgayXacNhan));

                DK += " AND Cast(datediff(day, 0, dNgayXacNhan) as datetime) <= @DenNgayXacNhan";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@DenNgayXacNhan", CommonFunction.LayNgayTuXau_YYYYMMDD(models.DenNgayXacNhan));
            }

            if (models.iID_KetQuaXuLy > 0)
            {
                DK += " AND iID_KetQuaXuLy = @iID_KetQuaXuLy";
                cmd.Parameters.AddWithValue("@iID_KetQuaXuLy", models.iID_KetQuaXuLy);
            }
            if (!string.IsNullOrEmpty(models.sTenTACN))
            {
                DK += " AND sTenTACN LIKE @sTenTACN";
                cmd.Parameters.AddWithValue("@sTenTACN", "%" + models.sTenTACN + "%");
            }

            if (!string.IsNullOrEmpty(models.sSoTiepNhan))
            {
                DK += " AND sSoTiepNhan LIKE @sSoTiepNhan";
                cmd.Parameters.AddWithValue("@sSoTiepNhan", "%" + models.sSoTiepNhan.Trim() + "%");
            }

            if (!string.IsNullOrEmpty(models.sSoGDK))
            {
                DK += " AND sSoGDK LIKE @sSoGDK";
                cmd.Parameters.AddWithValue("@sSoGDK", "%" + models.sSoGDK.Trim() + "%");
            }
            //string SQL = string.Format("SELECT count(iID_MaHoSo) as value FROM CNN25_HoSo AS A INNER JOIN CNN25_HoSo_TCCD AS B ON  iID_MaHoSo = B.iID_MaHoSo WHERE {0} ", DK);
            string SQL = string.Format(@"SELECT * FROM (SELECT COUNT(*) as count FROM(SELECT * FROM CNN25_HoSo WHERE {0}) A
                                        INNER JOIN(SELECT * FROM CNN25_HoSo_TCCD WHERE {1}) B ON A.iID_MaHoSo = B.iID_MaHoSo) TB", DK, DKHH);
            cmd.CommandText = SQL;
            int vR = Convert.ToInt32(Connection.GetValue(cmd, 0));
            cmd.Dispose();
            return vR;
        }
        public static DataTable GetDataTable_TCCD(sHoSoModels models, string iID_MaToChuc, int page, int numrecord)
        {
            SqlCommand cmd = new SqlCommand();
            string DK = "1=1", DKHH = "1=1";
            List<TrangThaiModels> TrangThais;
            switch (models.LoaiDanhSach)
            {
                case 1:
                    TrangThais = BPMC_ChoTiepNhanHoSo();
                    break;
                case 2:
                    TrangThais = BPMC_ChoTiepNhanHoSoGuiBoSung();
                    break;
                case 3:
                    TrangThais = BPMC_DaTuChoiXacNhanGDK();
                    break;
                case 4:
                    TrangThais = BPMC_PhongTACNYeuCauBoSungHoSo();
                    break;
                case 5:
                    TrangThais = BPMC_DaPheDuyetGDK();
                    break;
                case 6:
                    TrangThais = BPMC_DaTraGDK();
                    break;
                case 7:
                    TrangThais = ChuyenVien_ChoXuLyHoSo();
                    break;
                case 8:
                    TrangThais = ChuyenVien_ChoXuLyKetQua();
                    break;
                case 10:
                    TrangThais = LDP_ChoXemXetHoSo();
                    break;
                case 11:
                    TrangThais = LDP_ChoXemXetKetQua();
                    break;
                case 12:
                    TrangThais = LDC_ChoXacNhanGDK();
                    break;
                case 13:
                    TrangThais = LDC_ChoPheDuyetKetQua();
                    break;
                case 14:
                    TrangThais = BPMC_ChoTiepNhanKetQua();
                    break;
                case 15:
                    TrangThais = BPMC_ChoTiepNhanKetQuaGuiBoSung();
                    break;
                case 16:
                    TrangThais = BPMC_PhongTACNYeuCauBoSungKetQua();
                    break;
                case 17:
                    TrangThais = BPMC_DaPheDuyetThongBaoKetQua();
                    break;
                case 18:
                    TrangThais = BPMC_DaCapThongBaoKetQua();
                    break;
                case 50:
                    TrangThais = ToChucChiDinh(null);
                    break;
                case 51:
                    TrangThais = ToChucChiDinh(27);
                    break;
                case 52:
                    TrangThais = ToChucChiDinh(28);
                    break;
                default:
                    TrangThais = new List<TrangThaiModels>();
                    break;
            }
            TrangThaiModels trangThai;
            String sTrangThai = "";
            if (models.iID_MaTrangThai == 0)
            {
                for (int i = 0; i < TrangThais.Count; i++)
                {
                    trangThai = TrangThais[i];

                    if (sTrangThai.Length > 0) sTrangThai += ",";
                    sTrangThai += trangThai.iID_MaTrangThai;

                }
                if (sTrangThai.Length > 0)
                {
                    DK += string.Format(" AND iID_MaTrangThai IN({0})", sTrangThai);
                }
            }
            else
            {
                DK += " AND iID_MaTrangThai=@iID_MaTrangThai";
                cmd.Parameters.AddWithValue("@iID_MaTrangThai", models.iID_MaTrangThai);
            }
            if (String.IsNullOrEmpty(iID_MaToChuc) == false)
            {
                DKHH += " AND iID_MaToChuc=@iID_MaToChuc";
                cmd.Parameters.AddWithValue("@iID_MaToChuc", iID_MaToChuc);
            }
            if (!string.IsNullOrEmpty(models.sMaHoSo))
            {
                DK += " AND sMaHoSo LIKE @sMaHoSo";
                cmd.Parameters.AddWithValue("@sMaHoSo", "%" + models.sMaHoSo.Trim() + "%");
            }
            if (!string.IsNullOrEmpty(models.sMaSoThue))
            {
                DK += " AND sMaSoThue LIKE @sMaSoThue";
                cmd.Parameters.AddWithValue("@sMaSoThue", "%" + models.sMaSoThue.Trim() + "%");
            }
            if (!string.IsNullOrEmpty(models.sTenDoanhNghiep))
            {
                DK += " AND sTenDoanhNghiep LIKE @sTenDoanhNghiep";
                cmd.Parameters.AddWithValue("@sTenDoanhNghiep", "%" + models.sTenDoanhNghiep + "%");
            }

            if (!string.IsNullOrEmpty(models.TuNgayDen))
            {
                //DK += " AND dNgayTaoHoSo >= @dTuNgay";
                //cmd.Parameters.AddWithValue("@dTuNgay", CommonFunction.LayNgayTuXau(models.TuNgayDen));

                DK += " AND Cast(datediff(day, 0, dNgayTaoHoSo) as datetime) >= @dTuNgay";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@dTuNgay", CommonFunction.LayNgayTuXau_YYYYMMDD(models.TuNgayDen));
            }
            if (!string.IsNullOrEmpty(models.DenNgayDen))
            {
                //DK += " AND dNgayTaoHoSo <= @dDenNgay";
                //cmd.Parameters.AddWithValue("@dDenNgay", CommonFunction.LayNgayTuXau(models.DenNgayDen));

                DK += " AND Cast(datediff(day, 0, dNgayTaoHoSo) as datetime) <= @dDenNgay";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@dDenNgay", CommonFunction.LayNgayTuXau_YYYYMMDD(models.DenNgayDen));
            }

            if (!string.IsNullOrEmpty(models.TuNgayTiepNhan))
            {
                //DK += " AND dNgayTiepNhan >= @TuNgayTiepNhan";
                //cmd.Parameters.AddWithValue("@TuNgayTiepNhan", CommonFunction.LayNgayTuXau(models.TuNgayTiepNhan));

                DK += " AND Cast(datediff(day, 0, dNgayTiepNhan) as datetime) >= @TuNgayTiepNhan";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@TuNgayTiepNhan", CommonFunction.LayNgayTuXau_YYYYMMDD(models.TuNgayTiepNhan));
            }
            if (!string.IsNullOrEmpty(models.DenNgayTiepNhan))
            {
                //DK += " AND dNgayTiepNhan <= @DenNgayTiepNhan";
                //cmd.Parameters.AddWithValue("@DenNgayTiepNhan", CommonFunction.LayNgayTuXau(models.DenNgayTiepNhan));

                DK += " AND Cast(datediff(day, 0, dNgayTiepNhan) as datetime) <= @DenNgayTiepNhan";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@DenNgayTiepNhan", CommonFunction.LayNgayTuXau_YYYYMMDD(models.DenNgayTiepNhan));
            }

            if (!string.IsNullOrEmpty(models.TuNgayXacNhan))
            {
                //DK += " AND dNgayXacNhan >= @TuNgayXacNhan";
                //cmd.Parameters.AddWithValue("@TuNgayXacNhan", CommonFunction.LayNgayTuXau(models.TuNgayXacNhan));

                DK += " AND Cast(datediff(day, 0, dNgayXacNhan) as datetime) >= @TuNgayXacNhan";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@TuNgayXacNhan", CommonFunction.LayNgayTuXau_YYYYMMDD(models.TuNgayXacNhan));
            }
            if (!string.IsNullOrEmpty(models.DenNgayXacNhan))
            {
                //DK += " AND dNgayXacNhan <= @DenNgayXacNhan";
                //cmd.Parameters.AddWithValue("@DenNgayXacNhan", CommonFunction.LayNgayTuXau(models.DenNgayXacNhan));

                DK += " AND Cast(datediff(day, 0, dNgayXacNhan) as datetime) <= @DenNgayXacNhan";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@DenNgayXacNhan", CommonFunction.LayNgayTuXau_YYYYMMDD(models.DenNgayXacNhan));
            }

            if (models.iID_KetQuaXuLy > 0)
            {
                DK += " AND iID_KetQuaXuLy = @iID_KetQuaXuLy";
                cmd.Parameters.AddWithValue("@iID_KetQuaXuLy", models.iID_KetQuaXuLy);
            }
            if (!string.IsNullOrEmpty(models.sTenTACN))
            {
                DK += " AND sTenTACN LIKE @sTenTACN";
                cmd.Parameters.AddWithValue("@sTenTACN", "%" + models.sTenTACN + "%");
            }

            if (!string.IsNullOrEmpty(models.sSoTiepNhan))
            {
                DK += " AND sSoTiepNhan LIKE @sSoTiepNhan";
                cmd.Parameters.AddWithValue("@sSoTiepNhan", "%" + models.sSoTiepNhan.Trim() + "%");
            }

            if (!string.IsNullOrEmpty(models.sSoGDK))
            {
                DK += " AND sSoGDK LIKE @sSoGDK";
                cmd.Parameters.AddWithValue("@sSoGDK", "%" + models.sSoGDK.Trim() + "%");
            }
            //string SQL = string.Format("SELECT A.iID_MaHoSo, A.sMaHoSo, A.sSoTiepNhan, A.dNgayTaoHoSo, A.dNgayTiepNhan, A.sTenDoanhNghiep, A.sTenTACN, A.iID_MaTrangThai, B.iID_MaToChuc, B.sTenToChuc FROM CNN25_HoSo AS A INNER JOIN CNN25_HoSo_TCCD AS B ON  A.iID_MaHoSo = B.iID_MaHoSo WHERE {0} ", DK);
            string SQL = string.Format(@"SELECT * FROM (SELECT A.*,
                                        B.iID_MaToChuc, B.sTenToChuc
                                        FROM(SELECT * FROM CNN25_HoSo WHERE {0}) A
                                        INNER JOIN(SELECT * FROM CNN25_HoSo_TCCD WHERE {1}) B ON A.iID_MaHoSo = B.iID_MaHoSo) TB", DK, DKHH);
            cmd.CommandText = SQL;
            string sOrder = "iID_MaHoSo DESC";
            DataTable dt = CommonFunction.dtData(cmd, sOrder, page, numrecord);
            cmd.Dispose();
            return dt;
        }


        #region getDataTable hàng hóa
        public static int GetThongKe_XNCL_BieuDo_Ngay(string sNgay)
        {
            int vR = 0;
            try
            {
                String SQL = "SELECT COUNT(*) AS TongSo FROM CNN25_HoSo_XNCL WHERE Cast(datediff(day, 0, dNgayTao) as datetime) >= @dNgayTao";   // _FromDate = 'yyyy-MM-dd'

                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@dNgayTao", CommonFunction.LayNgayTuXau_YYYYMMDD(sNgay));
                vR = Convert.ToInt32(Connection.GetValue(cmd, 0, CThamSo.iKetNoi));
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                vR = -1;
            }
            return vR;
        }
        public static int GetCountHH_Dashboard(int LoaiDanhSach, int iID_MaTrangThai)
        {
            SqlCommand cmd = new SqlCommand();
            string DK = "1=1", DKHH = "1=1";
            List<TrangThaiModels> TrangThais;
            switch (LoaiDanhSach)
            {
                case 1:
                    TrangThais = BPMC_ChoTiepNhanHoSo();
                    break;
                case 2:
                    TrangThais = BPMC_ChoTiepNhanHoSoGuiBoSung();
                    break;
                case 3:
                    TrangThais = BPMC_DaTuChoiXacNhanGDK();
                    break;
                case 4:
                    TrangThais = BPMC_PhongTACNYeuCauBoSungHoSo();
                    break;
                case 5:
                    TrangThais = BPMC_DaPheDuyetGDK();
                    break;
                case 6:
                    TrangThais = BPMC_DaTraGDK();
                    break;
                case 7:
                    TrangThais = ChuyenVien_ChoXuLyHoSo();
                    break;
                case 8:
                    TrangThais = ChuyenVien_ChoXuLyKetQua();
                    break;
                case 10:
                    TrangThais = LDP_ChoXemXetHoSo();
                    break;
                case 11:
                    TrangThais = LDP_ChoXemXetKetQua();
                    break;
                case 12:
                    TrangThais = LDC_ChoXacNhanGDK();
                    break;
                case 13:
                    TrangThais = LDC_ChoPheDuyetKetQua();
                    break;
                case 14:
                    TrangThais = BPMC_ChoTiepNhanKetQua();
                    break;
                case 15:
                    TrangThais = BPMC_ChoTiepNhanKetQuaGuiBoSung();
                    break;
                case 16:
                    TrangThais = BPMC_PhongTACNYeuCauBoSungKetQua();
                    break;
                case 17:
                    TrangThais = BPMC_DaPheDuyetThongBaoKetQua();
                    break;
                case 18:
                    TrangThais = BPMC_DaCapThongBaoKetQua();
                    break;
                default:
                    TrangThais = new List<TrangThaiModels>();
                    break;
            }
            TrangThaiModels trangThai;
            if (iID_MaTrangThai == 0)
            {
                string sTrangThai = "";
                for (int i = 0; i < TrangThais.Count; i++)
                {
                    trangThai = TrangThais[i];

                    if (sTrangThai.Length > 0) sTrangThai += ",";
                    sTrangThai += trangThai.iID_MaTrangThai;

                }
                if (sTrangThai.Length > 0)
                {
                    DKHH += string.Format(" AND iID_MaTrangThai IN({0})", sTrangThai);
                }
            }
            else
            {
                if (LoaiDanhSach == 11 && (iID_MaTrangThai == 34 || iID_MaTrangThai == 35))//xncl_ldp
                {
                    // xử lý trường hợp trạng thái 34, 35 trùng tên nhưng combo chỉ cần chọn 1 trạng thái 34 là select cả 35
                    DKHH += " AND (iID_MaTrangThai=@iID_MaTrangThai OR iID_MaTrangThai=@iID_MaTrangThai1)";
                    cmd.Parameters.AddWithValue("@iID_MaTrangThai", 34);
                    cmd.Parameters.AddWithValue("@iID_MaTrangThai1", 35);
                }
                else
                {
                    DKHH += " AND iID_MaTrangThai=@iID_MaTrangThai";
                    cmd.Parameters.AddWithValue("@iID_MaTrangThai", iID_MaTrangThai);
                }
            }            
            string SQL = string.Format(@"SELECT * FROM (SELECT COUNT(iID_MaHangHoa) as count FROM(select * from CNN25_HangHoa WHERE {0}) hh
                                        INNER JOIN(SELECT * FROM CNN25_HoSo WHERE {1}) hs ON hs.iID_MaHoSo = hh.iID_MaHoSo) TB", DKHH, DK);
            cmd.CommandText = SQL;
            int vR = Convert.ToInt32(Connection.GetValue(cmd, 0));
            cmd.Dispose();
            return vR;
        }
        public static int GetCountHH(sHoSoModels models)
        {
            SqlCommand cmd = new SqlCommand();
            string DK = "1=1", DKHH = "1=1";
            List<TrangThaiModels> TrangThais;
            switch (models.LoaiDanhSach)
            {
                case 1:
                    TrangThais = BPMC_ChoTiepNhanHoSo();
                    break;
                case 2:
                    TrangThais = BPMC_ChoTiepNhanHoSoGuiBoSung();
                    break;
                case 3:
                    TrangThais = BPMC_DaTuChoiXacNhanGDK();
                    break;
                case 4:
                    TrangThais = BPMC_PhongTACNYeuCauBoSungHoSo();
                    break;
                case 5:
                    TrangThais = BPMC_DaPheDuyetGDK();
                    break;
                case 6:
                    TrangThais = BPMC_DaTraGDK();
                    break;
                case 7:
                    TrangThais = ChuyenVien_ChoXuLyHoSo();
                    break;
                case 8:
                    TrangThais = ChuyenVien_ChoXuLyKetQua();
                    break;
                case 10:
                    TrangThais = LDP_ChoXemXetHoSo();
                    break;
                case 11:
                    TrangThais = LDP_ChoXemXetKetQua();
                    break;
                case 12:
                    TrangThais = LDC_ChoXacNhanGDK();
                    break;
                case 13:
                    TrangThais = LDC_ChoPheDuyetKetQua();
                    break;
                case 14:
                    TrangThais = BPMC_ChoTiepNhanKetQua();
                    break;
                case 15:
                    TrangThais = BPMC_ChoTiepNhanKetQuaGuiBoSung();
                    break;
                case 16:
                    TrangThais = BPMC_PhongTACNYeuCauBoSungKetQua();
                    break;
                case 17:
                    TrangThais = BPMC_DaPheDuyetThongBaoKetQua();
                    break;
                case 18:
                    TrangThais = BPMC_DaCapThongBaoKetQua();
                    break;
                default:
                    TrangThais = new List<TrangThaiModels>();
                    break;
            }
            TrangThaiModels trangThai;
            if (models.iID_MaTrangThai == 0)
            {
                string sTrangThai = "";
                for (int i = 0; i < TrangThais.Count; i++)
                {
                    trangThai = TrangThais[i];

                    if (sTrangThai.Length > 0) sTrangThai += ",";
                    sTrangThai += trangThai.iID_MaTrangThai;

                }
                if (sTrangThai.Length > 0)
                {
                    DKHH += string.Format(" AND iID_MaTrangThai IN({0})", sTrangThai);
                }
            }
            else
            {
                if (models.LoaiDanhSach == 11 && (models.iID_MaTrangThai == 34 || models.iID_MaTrangThai == 35))//xncl_ldp
                {
                    // xử lý trường hợp trạng thái 34, 35 trùng tên nhưng combo chỉ cần chọn 1 trạng thái 34 là select cả 35
                    DKHH += " AND (iID_MaTrangThai=@iID_MaTrangThai OR iID_MaTrangThai=@iID_MaTrangThai1)";
                    cmd.Parameters.AddWithValue("@iID_MaTrangThai", 34);
                    cmd.Parameters.AddWithValue("@iID_MaTrangThai1", 35);
                }
                else
                {
                    DKHH += " AND iID_MaTrangThai=@iID_MaTrangThai";
                    cmd.Parameters.AddWithValue("@iID_MaTrangThai", models.iID_MaTrangThai);
                }
            }
            if (!string.IsNullOrEmpty(models.sTenTACN))
            {
                DKHH += " AND sTenHangHoa LIKE @sTenHangHoa";
                cmd.Parameters.AddWithValue("@sTenHangHoa", "%" + models.sTenTACN.Trim() + "%");
            }
            if (!string.IsNullOrEmpty(models.sMaHoSo))
            {
                DK += " AND sMaHoSo LIKE @sMaHoSo";
                cmd.Parameters.AddWithValue("@sMaHoSo", "%" + models.sMaHoSo.Trim() + "%");
            }
            if (!string.IsNullOrEmpty(models.sMaSoThue))
            {
                DK += " AND sMaSoThue LIKE @sMaSoThue";
                cmd.Parameters.AddWithValue("@sMaSoThue", "%" + models.sMaSoThue.Trim() + "%");
            }
            if (!string.IsNullOrEmpty(models.sTenDoanhNghiep))
            {
                DK += " AND sTenDoanhNghiep LIKE @sTenDoanhNghiep";
                cmd.Parameters.AddWithValue("@sTenDoanhNghiep", "%" + models.sTenDoanhNghiep.Trim() + "%");
            }

            if (!string.IsNullOrEmpty(models.TuNgayDen))
            {
                //DK += " AND dNgayTaoHoSo >= @dTuNgay";
                //cmd.Parameters.AddWithValue("@dTuNgay", CommonFunction.LayNgayTuXau(models.TuNgayDen));

                DK += " AND Cast(datediff(day, 0, dNgayTaoHoSo) as datetime) >= @dTuNgay";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@dTuNgay", CommonFunction.LayNgayTuXau_YYYYMMDD(models.TuNgayDen));
            }
            if (!string.IsNullOrEmpty(models.DenNgayDen))
            {
                //DK += " AND dNgayTaoHoSo <= @dDenNgay";
                //cmd.Parameters.AddWithValue("@dDenNgay", CommonFunction.LayNgayTuXau(models.DenNgayDen));

                DK += " AND Cast(datediff(day, 0, dNgayTaoHoSo) as datetime) <= @dDenNgay";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@dDenNgay", CommonFunction.LayNgayTuXau_YYYYMMDD(models.DenNgayDen));
            }

            if (!string.IsNullOrEmpty(models.TuNgayTiepNhan))
            {
                //DK += " AND dNgayTiepNhan >= @TuNgayTiepNhan";
                //cmd.Parameters.AddWithValue("@TuNgayTiepNhan", CommonFunction.LayNgayTuXau(models.TuNgayTiepNhan));

                DK += " AND Cast(datediff(day, 0, dNgayTiepNhan) as datetime) >= @TuNgayTiepNhan";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@TuNgayTiepNhan", CommonFunction.LayNgayTuXau_YYYYMMDD(models.TuNgayTiepNhan));
            }
            if (!string.IsNullOrEmpty(models.DenNgayTiepNhan))
            {
                //DK += " AND dNgayTiepNhan <= @DenNgayTiepNhan";
                //cmd.Parameters.AddWithValue("@DenNgayTiepNhan", CommonFunction.LayNgayTuXau(models.DenNgayTiepNhan));

                DK += " AND Cast(datediff(day, 0, dNgayTiepNhan) as datetime) <= @DenNgayTiepNhan";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@DenNgayTiepNhan", CommonFunction.LayNgayTuXau_YYYYMMDD(models.DenNgayTiepNhan));
            }

            if (!string.IsNullOrEmpty(models.TuNgayXacNhan))
            {
                //DK += " AND dNgayXacNhan >= @TuNgayXacNhan";
                //cmd.Parameters.AddWithValue("@TuNgayXacNhan", CommonFunction.LayNgayTuXau(models.TuNgayXacNhan));

                DK += " AND Cast(datediff(day, 0, dNgayXacNhan) as datetime) >= @TuNgayXacNhan";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@TuNgayXacNhan", CommonFunction.LayNgayTuXau_YYYYMMDD(models.TuNgayXacNhan));
            }
            if (!string.IsNullOrEmpty(models.DenNgayXacNhan))
            {
                //DK += " AND dNgayXacNhan <= @DenNgayXacNhan";
                //cmd.Parameters.AddWithValue("@DenNgayXacNhan", CommonFunction.LayNgayTuXau(models.DenNgayXacNhan));

                DK += " AND Cast(datediff(day, 0, dNgayXacNhan) as datetime) <= @DenNgayXacNhan";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@DenNgayXacNhan", CommonFunction.LayNgayTuXau_YYYYMMDD(models.DenNgayXacNhan));
            }

            if (!string.IsNullOrEmpty(models.sSoTiepNhan))
            {
                DK += " AND sSoTiepNhan LIKE @sSoTiepNhan";
                cmd.Parameters.AddWithValue("@sSoTiepNhan", "%" + models.sSoTiepNhan.Trim() + "%");
            }
            if (!string.IsNullOrEmpty(models.sSoGDK))
            {
                DK += " AND sSoGDK LIKE @sSoGDK";
                cmd.Parameters.AddWithValue("@sSoGDK", "%" + models.sSoGDK.Trim() + "%");
            }
            if (!string.IsNullOrEmpty(models.sSoThongBaoKetQua))
            {
                DKHH += " AND sSoThongBaoKetQua LIKE @sSoThongBaoKetQua";
                cmd.Parameters.AddWithValue("@sSoThongBaoKetQua", "%" + models.sSoThongBaoKetQua.Trim() + "%");
            }
            if (!string.IsNullOrEmpty(models.TuNgayThongBaoKetQua))
            {
                //DKHH += " AND dSoThongBaoKetQua_NgayKy >= @TuNgayThongBaoKetQua";
                //cmd.Parameters.AddWithValue("@TuNgayThongBaoKetQua", CommonFunction.LayNgayTuXau(models.TuNgayThongBaoKetQua));

                DKHH += " AND Cast(datediff(day, 0, dSoThongBaoKetQua_NgayKy) as datetime) >= @TuNgayThongBaoKetQua";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@TuNgayThongBaoKetQua", CommonFunction.LayNgayTuXau_YYYYMMDD(models.TuNgayThongBaoKetQua));
            }
            if (!string.IsNullOrEmpty(models.DenNgayThongBaoKetQua))
            {
                //DKHH += " AND dSoThongBaoKetQua_NgayKy <= @DenNgayThongBaoKetQua";
                //cmd.Parameters.AddWithValue("@DenNgayThongBaoKetQua", CommonFunction.LayNgayTuXau(models.DenNgayThongBaoKetQua));

                DKHH += " AND Cast(datediff(day, 0, dSoThongBaoKetQua_NgayKy) as datetime) <= @DenNgayThongBaoKetQua";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@DenNgayThongBaoKetQua", CommonFunction.LayNgayTuXau_YYYYMMDD(models.DenNgayThongBaoKetQua));
            }
            if (models.iID_MaLoaiHoSo > 0)
            {
                DK += " AND iID_MaLoaiHoSo = @iID_MaLoaiHoSo";
                cmd.Parameters.AddWithValue("@iID_MaLoaiHoSo", models.iID_MaLoaiHoSo);
            }
            string SQL = string.Format(@"SELECT * FROM (SELECT COUNT(iID_MaHangHoa) as count FROM(select * from CNN25_HangHoa WHERE {0}) hh
                                        INNER JOIN(SELECT * FROM CNN25_HoSo WHERE {1}) hs ON hs.iID_MaHoSo = hh.iID_MaHoSo) TB", DKHH, DK);
            cmd.CommandText = SQL;
            int vR = Convert.ToInt32(Connection.GetValue(cmd, 0));
            cmd.Dispose();
            return vR;
        }
        public static DataTable GetDataTableHH(sHoSoModels models, int page, int numrecord, string sOrderInput = "iID_MaHoSo DESC")
        {
            SqlCommand cmd = new SqlCommand();
            string DK = "1=1",DKHH="1=1";

            List<TrangThaiModels> TrangThais;
            switch (models.LoaiDanhSach)
            {
                //case 1:
                //    TrangThais = BPMC_ChoTiepNhanHoSo();
                //    break;
                //case 2:
                //    TrangThais = BPMC_ChoTiepNhanHoSoGuiBoSung();
                //    break;
                //case 3:
                //    TrangThais = BPMC_DaTuChoiXacNhanGDK();
                //    break;
                //case 4:
                //    TrangThais = BPMC_PhongTACNYeuCauBoSungHoSo();
                //    break;
                //case 5:
                //    TrangThais = BPMC_DaPheDuyetGDK();
                //    break;
                //case 6:
                //    TrangThais = BPMC_DaTraGDK();
                //    break;
                //case 7:
                //    TrangThais = ChuyenVien_ChoXuLyHoSo();
                //    break;
                case 8:
                    TrangThais = ChuyenVien_ChoXuLyKetQua();
                    break;
                //case 10:
                //    TrangThais = LDP_ChoXemXetHoSo();
                //    break;
                case 11:
                    TrangThais = LDP_ChoXemXetKetQua();
                    break;
                //case 12:
                //    TrangThais = LDC_ChoXacNhanGDK();
                //    break;
                case 13:
                    TrangThais = LDC_ChoPheDuyetKetQua();
                    break;
                case 14:
                    TrangThais = BPMC_ChoTiepNhanKetQua();
                    break;
                case 15:
                    TrangThais = BPMC_ChoTiepNhanKetQuaGuiBoSung();
                    break;
                case 16:
                    TrangThais = BPMC_PhongTACNYeuCauBoSungKetQua();
                    break;
                case 17:
                    TrangThais = BPMC_DaPheDuyetThongBaoKetQua();
                    break;
                case 18:
                    TrangThais = BPMC_DaCapThongBaoKetQua();
                    break;
                case 50:
                    TrangThais = ToChucChiDinh(null);
                    break;
                case 51:
                    TrangThais = ToChucChiDinh(27);
                    break;
                case 52:
                    TrangThais = ToChucChiDinh(28);
                    break;
                default:
                    TrangThais = new List<TrangThaiModels>();
                    break;
            }
            TrangThaiModels trangThai;
            String sTrangThai = "";
            if (models.iID_MaTrangThai == 0)
            {
                for (int i = 0; i < TrangThais.Count; i++)
                {
                    trangThai = TrangThais[i];

                    if (sTrangThai.Length > 0) sTrangThai += ",";
                    sTrangThai += trangThai.iID_MaTrangThai;

                }
                if (sTrangThai.Length > 0)
                {
                    DKHH += string.Format(" AND iID_MaTrangThai IN({0})", sTrangThai);
                }
            }
            else
            {
                if(models.LoaiDanhSach==11 && (models.iID_MaTrangThai==34 || models.iID_MaTrangThai == 35))//xncl_ldp
                {
                    // xử lý trường hợp trạng thái 34, 35 trùng tên nhưng combo chỉ cần chọn 1 trạng thái 34 là select cả 35
                    DKHH += " AND (iID_MaTrangThai=@iID_MaTrangThai OR iID_MaTrangThai=@iID_MaTrangThai1)";
                    cmd.Parameters.AddWithValue("@iID_MaTrangThai", 34);
                    cmd.Parameters.AddWithValue("@iID_MaTrangThai1", 35);
                }
                else
                {
                    DKHH += " AND iID_MaTrangThai=@iID_MaTrangThai";
                    cmd.Parameters.AddWithValue("@iID_MaTrangThai", models.iID_MaTrangThai);
                }
              
            }
            if(!string.IsNullOrEmpty(models.sTenTACN))
            {
                DKHH += " AND sTenHangHoa LIKE @sTenHangHoa";
                cmd.Parameters.AddWithValue("@sTenHangHoa", "%" + models.sTenTACN.Trim() + "%");
            }
            if (!string.IsNullOrEmpty(models.sMaHoSo))
            {
                DK += " AND sMaHoSo LIKE @sMaHoSo";
                cmd.Parameters.AddWithValue("@sMaHoSo", "%" + models.sMaHoSo.Trim() + "%");
            }
            if (!string.IsNullOrEmpty(models.sMaSoThue))
            {
                DK += " AND sMaSoThue LIKE @sMaSoThue";
                cmd.Parameters.AddWithValue("@sMaSoThue", "%" + models.sMaSoThue.Trim() + "%");
            }
            if (!string.IsNullOrEmpty(models.sTenDoanhNghiep))
            {
                DK += " AND sTenDoanhNghiep LIKE @sTenDoanhNghiep";
                cmd.Parameters.AddWithValue("@sTenDoanhNghiep", "%" + models.sTenDoanhNghiep.Trim() + "%");
            }

            if (!string.IsNullOrEmpty(models.TuNgayDen))
            {
                //DK += " AND dNgayTaoHoSo >= @dTuNgay";
                //cmd.Parameters.AddWithValue("@dTuNgay", CommonFunction.LayNgayTuXau(models.TuNgayDen));

                DK += " AND Cast(datediff(day, 0, dNgayTaoHoSo) as datetime) >= @dTuNgay";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@dTuNgay", CommonFunction.LayNgayTuXau_YYYYMMDD(models.TuNgayDen));
            }
            if (!string.IsNullOrEmpty(models.DenNgayDen))
            {
                //DK += " AND dNgayTaoHoSo <= @dDenNgay";
                //cmd.Parameters.AddWithValue("@dDenNgay", CommonFunction.LayNgayTuXau(models.DenNgayDen));

                DK += " AND Cast(datediff(day, 0, dNgayTaoHoSo) as datetime) <= @dDenNgay";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@dDenNgay", CommonFunction.LayNgayTuXau_YYYYMMDD(models.DenNgayDen));
            }

            if (!string.IsNullOrEmpty(models.TuNgayTiepNhan))
            {
                //DK += " AND dNgayTiepNhan >= @TuNgayTiepNhan";
                //cmd.Parameters.AddWithValue("@TuNgayTiepNhan", CommonFunction.LayNgayTuXau(models.TuNgayTiepNhan));

                DK += " AND Cast(datediff(day, 0, dNgayTiepNhan) as datetime) >= @TuNgayTiepNhan";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@TuNgayTiepNhan", CommonFunction.LayNgayTuXau_YYYYMMDD(models.TuNgayTiepNhan));
            }
            if (!string.IsNullOrEmpty(models.DenNgayTiepNhan))
            {
                //DK += " AND dNgayTiepNhan <= @DenNgayTiepNhan";
                //cmd.Parameters.AddWithValue("@DenNgayTiepNhan", CommonFunction.LayNgayTuXau(models.DenNgayTiepNhan));

                DK += " AND Cast(datediff(day, 0, dNgayTiepNhan) as datetime) <= @DenNgayTiepNhan";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@DenNgayTiepNhan", CommonFunction.LayNgayTuXau_YYYYMMDD(models.DenNgayTiepNhan));
            }

            if (!string.IsNullOrEmpty(models.TuNgayXacNhan))
            {
                //DK += " AND dNgayXacNhan >= @TuNgayXacNhan";
                //cmd.Parameters.AddWithValue("@TuNgayXacNhan", CommonFunction.LayNgayTuXau(models.TuNgayXacNhan));

                DK += " AND Cast(datediff(day, 0, dNgayXacNhan) as datetime) >= @TuNgayXacNhan";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@TuNgayXacNhan", CommonFunction.LayNgayTuXau_YYYYMMDD(models.TuNgayXacNhan));
            }
            if (!string.IsNullOrEmpty(models.DenNgayXacNhan))
            {
                //DK += " AND dNgayXacNhan <= @DenNgayXacNhan";
                //cmd.Parameters.AddWithValue("@DenNgayXacNhan", CommonFunction.LayNgayTuXau(models.DenNgayXacNhan));

                DK += " AND Cast(datediff(day, 0, dNgayXacNhan) as datetime) <= @DenNgayXacNhan";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@DenNgayXacNhan", CommonFunction.LayNgayTuXau_YYYYMMDD(models.DenNgayXacNhan));
            }

            if (!string.IsNullOrEmpty(models.sSoTiepNhan))
            {
                DK += " AND sSoTiepNhan LIKE @sSoTiepNhan";
                cmd.Parameters.AddWithValue("@sSoTiepNhan", "%" + models.sSoTiepNhan.Trim() + "%");
            }
            if (!string.IsNullOrEmpty(models.sSoGDK))
            {
                DK += " AND sSoGDK LIKE @sSoGDK";
                cmd.Parameters.AddWithValue("@sSoGDK", "%" + models.sSoGDK.Trim() + "%");
            }
            if (!string.IsNullOrEmpty(models.sSoThongBaoKetQua))
            {
                DKHH += " AND sSoThongBaoKetQua LIKE @sSoThongBaoKetQua";
                cmd.Parameters.AddWithValue("@sSoThongBaoKetQua", "%" + models.sSoThongBaoKetQua.Trim() + "%");
            }
            if (!string.IsNullOrEmpty(models.TuNgayThongBaoKetQua))
            {
                //DKHH += " AND dSoThongBaoKetQua_NgayKy >= @TuNgayThongBaoKetQua";
                //cmd.Parameters.AddWithValue("@TuNgayThongBaoKetQua", CommonFunction.LayNgayTuXau(models.TuNgayThongBaoKetQua));

                DKHH += " AND Cast(datediff(day, 0, dSoThongBaoKetQua_NgayKy) as datetime) >= @TuNgayThongBaoKetQua";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@TuNgayThongBaoKetQua", CommonFunction.LayNgayTuXau_YYYYMMDD(models.TuNgayThongBaoKetQua));
            }
            if (!string.IsNullOrEmpty(models.DenNgayThongBaoKetQua))
            {
                //DKHH += " AND dSoThongBaoKetQua_NgayKy <= @DenNgayThongBaoKetQua";
                //cmd.Parameters.AddWithValue("@DenNgayThongBaoKetQua", CommonFunction.LayNgayTuXau(models.DenNgayThongBaoKetQua));

                DKHH += " AND Cast(datediff(day, 0, dSoThongBaoKetQua_NgayKy) as datetime) <= @DenNgayThongBaoKetQua";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@DenNgayThongBaoKetQua", CommonFunction.LayNgayTuXau_YYYYMMDD(models.DenNgayThongBaoKetQua));
            }
            if(models.iID_MaLoaiHoSo>0)
            {
                DK += " AND iID_MaLoaiHoSo = @iID_MaLoaiHoSo";
                cmd.Parameters.AddWithValue("@iID_MaLoaiHoSo", models.iID_MaLoaiHoSo);
            }    
            string SQL = string.Format(@"SELECT * FROM (SELECT hh.*,
                                        hs.sTenDoanhNghiep,hs.dNgayTaoHoSo,hs.sSoTiepNhan,hs.dNgayTiepNhan,hs.sSoGDK,hs.dNgayXacNhan
                                        FROM(select * from CNN25_HangHoa WHERE {0}) hh
                                        INNER JOIN(SELECT * FROM CNN25_HoSo WHERE {1}) hs ON hs.iID_MaHoSo = hh.iID_MaHoSo) TB", DKHH ,DK);
            cmd.CommandText = SQL;
            string sOrder = sOrderInput;
            DataTable dt = CommonFunction.dtData(cmd, sOrder, page, numrecord);
            cmd.Dispose();
            return dt;
        }
        #endregion

        #region list trạng thai theo màn hình

        #region danh sách màn hình BPMC Xử lý hồ sơ giấy đăng ký
        /// <summary>
        /// Loại danh sách =1
        /// </summary>
        /// <returns></returns>
        private static List<TrangThaiModels> BPMC_ChoTiepNhanHoSo()
        {
            List<TrangThaiModels> lst = new List<TrangThaiModels>();
            TrangThaiModels trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 1
            };
            lst.Add(trangThai);

            return lst;
        }
        /// <summary>
        /// Loại danh sách =2
        /// </summary>
        /// <returns></returns>
        private static List<TrangThaiModels> BPMC_ChoTiepNhanHoSoGuiBoSung()
        {
            List<TrangThaiModels> lst = new List<TrangThaiModels>();
            TrangThaiModels trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 2,
                sTen = "Chờ tiếp nhận hồ sơ gửi bổ sung theo BPMC"
            };
            lst.Add(trangThai);
            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 3,
                sTen = "Chờ tiếp nhận hồ sơ gửi bổ sung theo Phòng TACN",
            };
            lst.Add(trangThai);
            return lst;
        }
        /// <summary>
        /// Loại danh sách =3
        /// </summary>
        /// <returns></returns>
        private static List<TrangThaiModels> BPMC_DaTuChoiXacNhanGDK()
        {
            List<TrangThaiModels> lst = new List<TrangThaiModels>();
            TrangThaiModels trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 19,
                sTen = "Lãnh đạo cục đã phê duyệt"
            };
            lst.Add(trangThai);

            return lst;
        }
        /// <summary>
        /// Loại danh sách =4
        /// </summary>
        /// <returns></returns>
        private static List<TrangThaiModels> BPMC_PhongTACNYeuCauBoSungHoSo()
        {
            List<TrangThaiModels> lst = new List<TrangThaiModels>();
            TrangThaiModels trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 13,
                sTen = "Đã gửi BPM",
                sKetQuaXuLy = "Yêu cầu bổ sung hồ sơ"

            };
            lst.Add(trangThai);

            return lst;
        }
        // <summary>
        /// Loại danh sách =5
        /// </summary>
        /// <returns></returns>
        private static List<TrangThaiModels> BPMC_DaPheDuyetGDK()
        {
            List<TrangThaiModels> lst = new List<TrangThaiModels>();
            TrangThaiModels trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 24,
                sTen = "Lãnh đạo cục đã phê duyệt"

            };
            lst.Add(trangThai);

            return lst;
        }

        /// Loại danh sách =6
        /// </summary>
        /// <returns></returns>
        private static List<TrangThaiModels> BPMC_DaTraGDK()
        {
            List<TrangThaiModels> lst = new List<TrangThaiModels>();
            TrangThaiModels trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 26,
                sTen = "Đã xác nhận GĐK"

            };
            lst.Add(trangThai);

            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 27,
                sTen = "Chờ kết quả đánh giá sự phù hợp"

            };
            lst.Add(trangThai);
            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 28,
                sTen = "Đã có kết quả đánh giá sự phù hợp"

            };
            lst.Add(trangThai);

            return lst;
        }
        #endregion

        #region danh sách màn hình BPMC Xử lý hồ sơ xác nhận chất lượng
        /// <summary>
        /// Loại danh sách =14
        /// </summary>
        /// <returns></returns>
        private static List<TrangThaiModels> BPMC_ChoTiepNhanKetQua()
        {
            List<TrangThaiModels> lst = new List<TrangThaiModels>();
            TrangThaiModels trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 29,
                sTen = "Chờ tiếp nhận kết quả đánh giá sự phù hợp",
            };
            lst.Add(trangThai);

            return lst;
        }
        /// <summary>
        /// Loại danh sách =15
        /// </summary>
        /// <returns></returns>
        private static List<TrangThaiModels> BPMC_ChoTiepNhanKetQuaGuiBoSung()
        {
            List<TrangThaiModels> lst = new List<TrangThaiModels>();
            TrangThaiModels trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 30,
                sTen = "Chờ tiếp nhận kết quả đánh giá sự phù hợp gửi bổ sung theo BPMC",
            };
            lst.Add(trangThai);
            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 31,
                sTen = "Chờ tiếp nhận kết quả đánh giá sự phù hợp gửi bổ sung theo phòng TACN",
            };
            lst.Add(trangThai);
            return lst;
        }
        /// <summary>
        /// Loại danh sách =16
        /// </summary>
        /// <returns></returns>
        private static List<TrangThaiModels> BPMC_PhongTACNYeuCauBoSungKetQua()
        {
            List<TrangThaiModels> lst = new List<TrangThaiModels>();
            TrangThaiModels trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 36,
                sTen = "Đã gửi BPMC",
                iID_KetQuaXuLy = 4,
                sKetQuaXuLy = "Yêu cầu bổ sung kết quả đánh giá sự phù hợp"
            };
            lst.Add(trangThai);

            return lst;
        }
        /// <summary>
        /// Loại danh sách =17
        /// </summary>
        /// <returns></returns>
        private static List<TrangThaiModels> BPMC_DaPheDuyetThongBaoKetQua()
        {
            List<TrangThaiModels> lst = new List<TrangThaiModels>();
            TrangThaiModels trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 42,
                sTen = "Lãnh đạo cục đã phê duyệt",
                iID_KetQuaXuLy = 5,
                sKetQuaXuLy = "Thông báo kết quả kiểm tra"
            };
            lst.Add(trangThai);

            return lst;
        }
        /// <summary>
        /// Loại danh sách =18
        /// </summary>
        /// <returns></returns>
        private static List<TrangThaiModels> BPMC_DaCapThongBaoKetQua()
        {
            List<TrangThaiModels> lst = new List<TrangThaiModels>();
            TrangThaiModels trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 44,
                sTen = "Đã cấp thông báo kết quả kiểm tra"
            };
            lst.Add(trangThai);

            return lst;
        }
        /// <summary>
        /// Loại danh sách = 50
        /// Tổ chức chỉ định
        /// </summary>
        /// <returns></returns>
        private static List<TrangThaiModels> ToChucChiDinh(int? id)
        {
            List<TrangThaiModels> lst = new List<TrangThaiModels>();
            TrangThaiModels trangThai_27 = new TrangThaiModels
            {
                iID_MaTrangThai = 27,
                sTen = "Chờ kết quả đánh giá sự phù hợp"
            };

            TrangThaiModels trangThai_28 = new TrangThaiModels
            {
                iID_MaTrangThai = 28,
                sTen = "Đã có kết quả đánh giá sự phù hợp",
            };
            if (id != null)
            {
                if (id == 27)
                {
                    lst.Add(trangThai_27);
                }
                else if (id == 28)
                {
                    lst.Add(trangThai_28);
                }
            }
            else
            {
                lst.Add(trangThai_27);
                lst.Add(trangThai_28);
            }

            return lst;
        }
        #endregion

        #region Chuyên viên
        /// <summary>
        /// Chuyen viên loại danh sach=7
        /// </summary>
        /// <returns></returns>
        private static List<TrangThaiModels> ChuyenVien_ChoXuLyHoSo()
        {
            List<TrangThaiModels> lst = new List<TrangThaiModels>();
            TrangThaiModels trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 7,
                sTen = "Đã tiếp nhận"

            };
            lst.Add(trangThai);
            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 10,
                sTen = "Chờ lãnh đạo phòng xem xét",
                iID_KetQuaXuLy = 1,
                sKetQuaXuLy = "Đạt cấp giấy đăng ký"

            };
            lst.Add(trangThai);
            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 11,
                sTen = "Chờ lãnh đạo phòng xem xét",
                iID_KetQuaXuLy = 2,
                sKetQuaXuLy = "Từ chối cấp GĐK"

            };
            lst.Add(trangThai);
            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 12,
                sTen = "Chờ lãnh đạo phòng xem xét",
                iID_KetQuaXuLy = 3,
                sKetQuaXuLy = "Yêu cầu bổ sung hồ sơ"

            };
            lst.Add(trangThai);
            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 14,
                sTen = "Lãnh đạo phòng yêu cầu thẩm định lại",
                iID_KetQuaXuLy = 3,
                sKetQuaXuLy = "Yêu cầu bổ sung hồ sơ"
            };
            lst.Add(trangThai);
            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 16,
                sTen = "BPMC trả thẩm định lại",
                iID_KetQuaXuLy = 3,
                sKetQuaXuLy = "Yêu cầu bổ sung hồ sơ"

            };
            lst.Add(trangThai);
            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 18,
                sTen = "Lãnh đạo phòng yêu cầu thẩm định lại",
                iID_KetQuaXuLy = 3,
                sKetQuaXuLy = "Từ chối cấp GĐK"

            };
            lst.Add(trangThai);
            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 23,
                sTen = "Lãnh đạo phòng yêu cầu thẩm định lại",
                iID_KetQuaXuLy = 1,
                sKetQuaXuLy = "Đạt cấp giấy đăng ký"

            };
            lst.Add(trangThai);

            return lst;
        }
        /// <summary>
        /// Chuyen viên loại danh sach=8
        /// </summary>
        /// <returns></returns>
        private static List<TrangThaiModels> ChuyenVien_ChoXuLyKetQua()
        {
            List<TrangThaiModels> lst = new List<TrangThaiModels>();
            TrangThaiModels trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 32,
                sTen = "Đã tiếp nhận kết quả đánh giá sự phù hợp"

            };
            lst.Add(trangThai);
            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 34,
                sTen = "Chờ lãnh đạo phòng xem xét ",
                iID_KetQuaXuLy = 4,
                sKetQuaXuLy = "Yêu cầu bổ sung kết quả đánh giá sự phù hợp"
            };
            lst.Add(trangThai);
            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 35,
                sTen = "Chờ lãnh đạo phòng xem xét ",
                iID_KetQuaXuLy = 5,
                sKetQuaXuLy = "Thông báo kết quả kiểm tra"
            };
            lst.Add(trangThai);
            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 37,
                sTen = "Lãnh đạo phòng yêu cầu thẩm định lại",
                iID_KetQuaXuLy = 4,
                sKetQuaXuLy = "Yêu cầu bổ sung kết quả đánh giá sự phù hợp"
            };
            lst.Add(trangThai);
            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 39,
                sTen = "BPMC trả thẩm định lại",
                iID_KetQuaXuLy = 4,
                sKetQuaXuLy = "Yêu cầu bổ sung kết quả đánh giá sự phù hợp"

            };
            lst.Add(trangThai);
            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 41,
                sTen = "Lãnh đạo phòng yêu cầu thẩm định lại",
                iID_KetQuaXuLy = 5,
                sKetQuaXuLy = "Thông báo kết quả kiểm tra"

            };
            lst.Add(trangThai);


            return lst;
        }
        #endregion chuyên viên

        #region Màn hình Lãnh đạo phòng
        /// <summary>
        /// Lãnh đạo- Loại danh sách =10
        /// </summary>
        /// <returns></returns>
        private static List<TrangThaiModels> LDP_ChoXemXetHoSo()
        {
            List<TrangThaiModels> lst = new List<TrangThaiModels>();
            TrangThaiModels trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 10,
                sTen = "Chờ lãnh đạo phòng xem xét",
                iID_KetQuaXuLy = 1,
                sKetQuaXuLy = "Đạt cấp giấy đăng ký"
            };
            lst.Add(trangThai);
            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 11,
                sTen = "Chờ lãnh đạo phòng xem xét",
                iID_KetQuaXuLy = 2,
                sKetQuaXuLy = "Từ chối cấp GĐK"
            };
            lst.Add(trangThai);

            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 12,
                sTen = "Chờ lãnh đạo phòng xem xét",
                iID_KetQuaXuLy = 3,
                sKetQuaXuLy = "Yêu cầu bổ sung hồ sơ"
            };
            lst.Add(trangThai);

            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 13,
                sTen = "Đã gửi BPMC",
                iID_KetQuaXuLy = 3,
                sKetQuaXuLy = "Yêu cầu bổ sung hồ sơ"
            };
            lst.Add(trangThai);

            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 17,
                sTen = "Chờ lãnh đạo cục phê duyệt",
                iID_KetQuaXuLy = 2,
                sKetQuaXuLy = "Từ chối cấp GĐK"
            };
            lst.Add(trangThai);

            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 20,
                sTen = "Lãnh đạo cục yêu cầu xem xét lại",
                iID_KetQuaXuLy = 2,
                sKetQuaXuLy = "Từ chối cấp GĐK"
            };
            lst.Add(trangThai);
            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 22,
                sTen = "Chờ lãnh đạo cục phê duyệt",
                iID_KetQuaXuLy = 1,
                sKetQuaXuLy = "Đạt cấp giấy đăng ký"
            };
            lst.Add(trangThai);

            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 25,
                sTen = "Lãnh đạo cục yêu cầu xem xét lại",
                iID_KetQuaXuLy = 1,
                sKetQuaXuLy = "Đạt cấp giấy đăng ký"
            };
            lst.Add(trangThai);
            return lst;
        }
        /// <summary>
        /// Lãnh đạo- Loại danh sách =11
        /// </summary>
        /// <returns></returns>
        private static List<TrangThaiModels> LDP_ChoXemXetKetQua()
        {
            List<TrangThaiModels> lst = new List<TrangThaiModels>();
            TrangThaiModels trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 34,
                sTen = "Chờ lãnh đạo phòng xem xét",
                iID_KetQuaXuLy = 4,
                sKetQuaXuLy = "Yêu cầu bổ sung kết quả đánh giá sự phù hợp"
            };
            lst.Add(trangThai);
            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 35,
                sTen = "Chờ lãnh đạo phòng xem xét",
                iID_KetQuaXuLy = 5,
                sKetQuaXuLy = "Thông báo kết quả kiểm tra"
            };
            lst.Add(trangThai);

            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 36,
                sTen = "Đã gửi BPMC",
                iID_KetQuaXuLy = 4,
                sKetQuaXuLy = "Yêu cầu bổ sung kết quả đánh giá sự phù hợp"
            };
            lst.Add(trangThai);

            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 40,
                sTen = "Chờ lãnh đạo cục phê duyệt",
                iID_KetQuaXuLy = 5,
                sKetQuaXuLy = "Thông báo kết quả kiểm tra"
            };
            lst.Add(trangThai);

            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 43,
                sTen = "Lãnh đạo cục yêu cầu xem xét lại",
                iID_KetQuaXuLy = 5,
                sKetQuaXuLy = "Thông báo kết quả kiểm tra"
            };
            lst.Add(trangThai);
            return lst;
        }
        #endregion

        #region Màn hình Lãnh đạo cục
        /// <summary>
        /// Lãnh đạo- Loại danh sách =12
        /// </summary>
        /// <returns></returns>
        private static List<TrangThaiModels> LDC_ChoXacNhanGDK()
        {
            List<TrangThaiModels> lst = new List<TrangThaiModels>();
            TrangThaiModels trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 17,
                sTen = "Chờ lãnh đạo cục phê duyệt",
                iID_KetQuaXuLy = 2,
                sKetQuaXuLy = "Từ chối cấp GĐK"
            };
            lst.Add(trangThai);
            trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 22,
                sTen = "Chờ lãnh đạo cục phê duyệt",
                iID_KetQuaXuLy = 1,
                sKetQuaXuLy = "Đạt cấp giấy đăng ký"
            };
            lst.Add(trangThai);
            return lst;
        }
        /// <summary>
        /// Lãnh đạo- Loại danh sách =13
        /// </summary>
        /// <returns></returns>
        private static List<TrangThaiModels> LDC_ChoPheDuyetKetQua()
        {
            List<TrangThaiModels> lst = new List<TrangThaiModels>();
            TrangThaiModels trangThai = new TrangThaiModels
            {
                iID_MaTrangThai = 40,
                sTen = "Chờ lãnh đạo cục phê duyệt",
                iID_KetQuaXuLy = 5,
                sKetQuaXuLy = "Thông báo kết quả kiểm tra"
            };
            lst.Add(trangThai);
            return lst;
        }


        #endregion

        #endregion

        #region DDL trạng thái search
        private static SelectOptionList DDLTrangThaiSearch(List<TrangThaiModels> trangThais, bool TatCa = true)
        {
            DataTable dataTable = new DataTable();
            dataTable.Columns.Add("iID_MaTrangThai", typeof(int));
            dataTable.Columns.Add("sTen", typeof(string));
            DataRow r;
            if (TatCa)
            {
                r = dataTable.NewRow();
                r[0] = 0;
                r[1] = "--Tất cả--";
                dataTable.Rows.Add(r);
            }
            foreach (var item in trangThais)
            {
                r = dataTable.NewRow();
                r[0] = item.iID_MaTrangThai;
                r[1] = item.sTen;
                dataTable.Rows.Add(r);
            }
            SelectOptionList DDL = new SelectOptionList(dataTable, "iID_MaTrangThai", "sTen");
            dataTable.Dispose();
            return DDL;
        }

        public static SelectOptionList DDL_ChoTiepNhan(bool TatCa = true)
        {
            return DDLTrangThaiSearch(BPMC_ChoTiepNhanHoSo(), TatCa);
        }
        public static SelectOptionList DDL_ChoTiepNhanHoSoGuiBoSung(bool TatCa = true)
        {
            return DDLTrangThaiSearch(BPMC_ChoTiepNhanHoSoGuiBoSung(), TatCa);
        }
        public static SelectOptionList DDL_ChoTiepNhanKetQua(bool TatCa = true)
        {
            return DDLTrangThaiSearch(BPMC_ChoTiepNhanKetQua(), TatCa);
        }
        public static SelectOptionList DDL_ChoTiepNhanKetQuaGuiBoSung(bool TatCa = true)
        {
            return DDLTrangThaiSearch(BPMC_ChoTiepNhanKetQuaGuiBoSung(), TatCa);
        }
        public static SelectOptionList DDL_PhongTACNYeuCauBoSungKetQua(bool TatCa = true)
        {
            return DDLTrangThaiSearch(BPMC_PhongTACNYeuCauBoSungKetQua(), TatCa);
        }
        public static SelectOptionList DDL_DaPheDuyetThongBaoKetQua(bool TatCa = true)
        {
            return DDLTrangThaiSearch(BPMC_DaPheDuyetThongBaoKetQua(), TatCa);
        }
        public static SelectOptionList DDL_DaCapThongBaoKetQua(bool TatCa = true)
        {
            return DDLTrangThaiSearch(BPMC_DaCapThongBaoKetQua(), TatCa);
        }

        #endregion

        #region Update
        public static void UpdateNguoiXem(string iID_MaHoSo, string MaND)
        {
            string SQL = "UPDATE CNN25_HoSo SET sTenNguoiXem=@sTenNguoiXem,sUserNguoiXem=@sUserNguoiXem WHERE iID_MaHoSo=@iID_MaHoSo AND (sUserNguoiXem IS NULL OR sUserNguoiXem='')";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
            cmd.Parameters.AddWithValue("@sTenNguoiXem", CPQ_NGUOIDUNG.Get_TenNguoiDung(MaND));
            cmd.Parameters.AddWithValue("@sUserNguoiXem", MaND);
            Connection.UpdateDatabase(cmd);
            cmd.Dispose();
        }

        public static ResultModels CleanNguoiXem(string iID_MaHoSo)
        {
            int vR = 0;
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
            cmd.Parameters.AddWithValue("@sTenNguoiXem", "");
            cmd.Parameters.AddWithValue("@sUserNguoiXem", "");
            vR = Connection.UpdateRecord("CNN25_HoSo", "iID_MaHoSo", cmd);
            cmd.Dispose();
            return new ResultModels
            {
                success = vR > 0 ? true : false
            };

        }
        #endregion

        #region LayThongTinHopDong
        public static DataTable Get_ThongTin_HopDong(long iID_MaHoSo)
        {
            string SQL = "SELECT * FROM CNN25_DinhKem WHERE iID_MaHoSo=@iID_MaHoSo AND iID_MaLoaiFile = 100";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
            DataTable dt = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();
            return dt;
        }
        #endregion

        #region LayThongTinHoaDon
        public static DataTable Get_ThongTin_HoaDon(long iID_MaHoSo)
        {
            string SQL = "SELECT * FROM CNN25_DinhKem WHERE iID_MaHoSo=@iID_MaHoSo AND iID_MaLoaiFile = 101";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
            DataTable dt = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();
            return dt;
        }
        #endregion

        #region LayThongTinPhieuDongGoi
        public static DataTable Get_ThongTin_PhieuDongGoi(long iID_MaHoSo)
        {
            string SQL = "SELECT * FROM CNN25_DinhKem WHERE iID_MaHoSo=@iID_MaHoSo AND iID_MaLoaiFile = 102";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaHoSo", iID_MaHoSo);
            DataTable dt = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();
            return dt;
        }
        #endregion
    }
}

