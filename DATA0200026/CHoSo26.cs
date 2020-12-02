using Dapper;
using DomainModel;
using DomainModel.Abstract;
using DomainModel.Controls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA0200026
{
    public class CHoSo26
    {
        public static long ThemHoSo(long iID_MaHoSo_Sua, int iID_MaTrangThai, int iID_MaLoaiHoSo, 
            string sMaHoSo, DateTime dNgayTaoHoSo, bool bDaTiepNhan, string sSoTiepNhan, DateTime? dNgayTiepNhan,
            string sSoGDK, string sSoGDK_ThayThe, DateTime? dNgayXacNhan,
            string sMaSoThue, string sTenDoanhNghiep, string sLoaiHinhThucKiemTra, string sTenHangHoa,
            string sNoiLamHoSo, string sToChuc_Name, string sToChuc_DiaChi, string sToChuc_Tel, string sToChuc_Fax, string sToChuc_Email, 
            string sMaCoQuanXuLy, string sTenCoQuanXuLy,
            string sKyHoSo_MaTinh, string sKyHoSo_Tinh, string sKyHoSo_NguoiKy, string sKyHoSo_NguoiKy_ChucDanh,
            String sUserName, String sIP)
        {
            long vR = 0;

            try
            {
                String sHashCode = "";

                long iID_MaHoSo = 0;
                Bang bang = new Bang("CNN26_HoSo");
                bang.MaNguoiDungSua = sUserName;
                bang.IPSua = sIP;

                bang.CmdParams.Parameters.AddWithValue("@iID_MaTrangThai", iID_MaTrangThai);
                bang.CmdParams.Parameters.AddWithValue("@iID_MaLoaiHoSo", iID_MaLoaiHoSo);
                bang.CmdParams.Parameters.AddWithValue("@sMaHoSo", sMaHoSo);
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
                bang.CmdParams.Parameters.AddWithValue("@sTenTACN", sTenHangHoa);
                bang.CmdParams.Parameters.AddWithValue("@sNoiLamHoSo", sNoiLamHoSo);
                bang.CmdParams.Parameters.AddWithValue("@sToChuc_Name", sToChuc_Name);
                bang.CmdParams.Parameters.AddWithValue("@sToChuc_DiaChi", sToChuc_DiaChi);
                bang.CmdParams.Parameters.AddWithValue("@sToChuc_Tel", sToChuc_Tel);
                bang.CmdParams.Parameters.AddWithValue("@sToChuc_Fax", sToChuc_Fax);
                bang.CmdParams.Parameters.AddWithValue("@sToChuc_Email", sToChuc_Email);
                bang.CmdParams.Parameters.AddWithValue("@sMaCoQuanXuLy", sMaCoQuanXuLy);
                bang.CmdParams.Parameters.AddWithValue("@sTenCoQuanXuLy", sTenCoQuanXuLy);
                bang.CmdParams.Parameters.AddWithValue("@sKyHoSo_MaTinh", sKyHoSo_MaTinh);
                bang.CmdParams.Parameters.AddWithValue("@sKyHoSo_Tinh", sKyHoSo_Tinh);
                bang.CmdParams.Parameters.AddWithValue("@sKyHoSo_NguoiKy", sKyHoSo_NguoiKy);
                bang.CmdParams.Parameters.AddWithValue("@sKyHoSo_NguoiKy_ChucDanh", sKyHoSo_NguoiKy_ChucDanh);
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

        public static int UpDate_TrangThai(long iID_MaHoSo, int iTrangThai)
        {
            int vR = 0;
            try
            {
                SqlCommand cmd;
                cmd = new SqlCommand("UPDATE CNN26_HoSo SET iID_MaTrangThai=@iID_MaTrangThai WHERE iID_MaHoSo=@iID_MaHoSo");
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

        public static HoSo26Models Get_Detail(long iID_MaHoSo)
        {
            using (SqlConnection connect = new SqlConnection(Connection.ConnectionString))
            {
                string SQL = @"SELECT *  FROM CNN26_HoSo 
                            WHERE iID_MaHoSo=@iID_MaHoSo";
                HoSo26Models results = connect.Query<HoSo26Models>(SQL, new { iID_MaHoSo = iID_MaHoSo }).FirstOrDefault();
                return results;
            }
        }

        public static int GetCount(CHoSoSearch models)
        {
            SqlCommand cmd = new SqlCommand();
            string DK = "1=1";
            if (models.iID_MaTrangThai > 0)
            {
                DK += " AND iID_MaTrangThai=@iID_MaTrangThai";
                cmd.Parameters.AddWithValue("@iID_MaTrangThai", models.iID_MaTrangThai);
            }
            if (!string.IsNullOrEmpty(models.sMaHoSo))
            {
                DK += " AND sMaHoSo=@sMaHoSo";
                cmd.Parameters.AddWithValue("@sMaHoSo", models.sMaHoSo);
            }
            if (!string.IsNullOrEmpty(models.sMaSoThue))
            {
                DK += " AND sMaSoThue=@sMaSoThue";
                cmd.Parameters.AddWithValue("@sMaSoThue", models.sMaSoThue);
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

                DK += " AND Cast(datediff(day, 0, dNgayTao) as datetime) >= @dTuNgay";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@dTuNgay", CommonFunction.LayNgayTuXau_YYYYMMDD(models.TuNgayDen));
            }
            if (!string.IsNullOrEmpty(models.DenNgayDen))
            {
                //DK += " AND dNgayTaoHoSo <= @dDenNgay";
                //cmd.Parameters.AddWithValue("@dDenNgay", CommonFunction.LayNgayTuXau(models.DenNgayDen));

                DK += " AND Cast(datediff(day, 0, dNgayTao) as datetime) <= @dDenNgay";   // _FromDate = 'yyyy-MM-dd'
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
                DK += " AND sSoTiepNhan <= @sSoTiepNhan";
                cmd.Parameters.AddWithValue("@sSoTiepNhan", models.sSoTiepNhan);
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
            string SQL = string.Format("SELECT count(iID_MaHoSo) as value FROM CNN26_HoSo WHERE {0} ", DK);
            cmd.CommandText = SQL;
            int vR = Convert.ToInt32(Connection.GetValue(cmd, 0, CThamSo.iKetNoi));
            cmd.Dispose();
            return vR;
        }
        public static DataTable GetDataTable(CHoSoSearch models, int page, int numrecord)
        {
            SqlCommand cmd = new SqlCommand();
            string DK = "1=1";
            if (models.iID_MaTrangThai > 0)
            {
                DK += " AND iID_MaTrangThai=@iID_MaTrangThai";
                cmd.Parameters.AddWithValue("@iID_MaTrangThai", models.iID_MaTrangThai);
            }
            if (!string.IsNullOrEmpty(models.sMaHoSo))
            {
                DK += " AND sMaHoSo=@sMaHoSo";
                cmd.Parameters.AddWithValue("@sMaHoSo", models.sMaHoSo);
            }
            if (!string.IsNullOrEmpty(models.sMaSoThue))
            {
                DK += " AND sMaSoThue=@sMaSoThue";
                cmd.Parameters.AddWithValue("@sMaSoThue", models.sMaSoThue);
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

                DK += " AND Cast(datediff(day, 0, dNgayTao) as datetime) >= @dTuNgay";   // _FromDate = 'yyyy-MM-dd'
                cmd.Parameters.AddWithValue("@dTuNgay", CommonFunction.LayNgayTuXau_YYYYMMDD(models.TuNgayDen));
            }
            if (!string.IsNullOrEmpty(models.DenNgayDen))
            {
                //DK += " AND dNgayTaoHoSo <= @dDenNgay";
                //cmd.Parameters.AddWithValue("@dDenNgay", CommonFunction.LayNgayTuXau(models.DenNgayDen));

                DK += " AND Cast(datediff(day, 0, dNgayTao) as datetime) <= @dDenNgay";   // _FromDate = 'yyyy-MM-dd'
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
                DK += " AND sSoTiepNhan <= @sSoTiepNhan";
                cmd.Parameters.AddWithValue("@sSoTiepNhan", models.sSoTiepNhan);
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
            string SQL = string.Format("SELECT * FROM CNN26_HoSo WHERE {0} ", DK);
            cmd.CommandText = SQL;
            string sOrder = "dNgayTao DESC";
            DataTable dt = CommonFunction.dtData(cmd, sOrder, page, numrecord, CThamSo.iKetNoi);
            cmd.Dispose();
            return dt;
        }

        public static DataTable Get_DanhSach_DoanhNghiep()
        {
            DataTable vR;

            string SQL = "SELECT DISTINCT sMaSoThue, sTenDoanhNghiep FROM CNN26_HoSo ORDER BY sTenDoanhNghiep";
            SqlCommand cmd = new SqlCommand(SQL);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();

            return vR;
        }

        #region Update
        public static void UpdateNguoiXem(string iID_MaHoSo, string MaND)
        {
            string SQL = "UPDATE CNN26_HoSo SET sTenNguoiXem=@sTenNguoiXem,sUserNguoiXem=@sUserNguoiXem WHERE iID_MaHoSo=@iID_MaHoSo AND (sUserNguoiXem IS NULL OR sUserNguoiXem='')";
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
            vR = Connection.UpdateRecord("CNN26_HoSo", "iID_MaHoSo", cmd);
            cmd.Dispose();
            return new ResultModels
            {
                success = vR > 0 ? true : false
            };

        }
        #endregion

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
