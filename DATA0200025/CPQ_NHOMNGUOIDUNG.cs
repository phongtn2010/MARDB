using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DomainModel;
using DomainModel.Controls;

namespace DATA0200025
{
    public class CPQ_NHOMNGUOIDUNG
    {
        public static DataTable Get_Table(String MaNhomNguoiDung)
        {
            DataTable vR = null;
            
            //String SQL = String.Format("SELECT * FROM QT_NHOMNGUOIDUNG WHERE ITRANGTHAI=1 AND (IID_MANHOMNGUOIDUNG = '{0}' OR LEFT(IID_MANHOMNGUOIDUNG,{1})='{0}-') ORDER BY IID_MANHOMNGUOIDUNG", MaNhomNguoiDung, MaNhomNguoiDung.Length + 1);
            String SQL = "SELECT * FROM QT_NHOMNGUOIDUNG ORDER BY IID_MANHOMNGUOIDUNG";

            SqlCommand cmd = new SqlCommand(SQL);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();

            return vR;
        }

        public static DataTable Get_One_QT_NhomNguoiDung(String iID_MaNhomNguoiDung)
        {
            DataTable vR = null;

            string SQL = "SELECT * FROM QT_NHOMNGUOIDUNG WHERE IID_MANHOMNGUOIDUNG=@IID_MANHOMNGUOIDUNG";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@IID_MANHOMNGUOIDUNG", iID_MaNhomNguoiDung);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();

            return vR;
        }

        public static String Get_Name(string iID_MaNhomNguoiDung)
        {
            String vR = "";

            DataTable dt = Get_One_QT_NhomNguoiDung(iID_MaNhomNguoiDung);
            if (dt.Rows.Count > 0)
            {
                vR = Convert.ToString(dt.Rows[0]["STEN"]);
            }
            dt.Dispose();

            return vR;
        }

        public static int Get_SoNhom(String MaNhomNguoiDungCha)
        {
            int vR = 0;

            string SQL = "SELECT ISOLUONGNHOMCON FROM QT_NHOMNGUOIDUNG WHERE IID_MANHOMNGUOIDUNG=@IID_MANHOMNGUOIDUNGCHA";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@IID_MANHOMNGUOIDUNGCHA", MaNhomNguoiDungCha);
            vR = Convert.ToInt32(Connection.GetValue(cmd, 0, CThamSo.iKetNoi));
            cmd.Dispose();

            return vR;
        }

        public static int Insert(String IID_MANHOMNGUOIDUNG, String STEN,  int ITRANGTHAI)
        {
            int vR = -1;

            try
            {
                String SQL = "INSERT INTO QT_NHOMNGUOIDUNG(IID_MANHOMNGUOIDUNG, STEN, ITRANGTHAI) VALUES(@IID_MANHOMNGUOIDUNG, @STEN, @ITRANGTHAI)";

                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@IID_MANHOMNGUOIDUNG", IID_MANHOMNGUOIDUNG);
                cmd.Parameters.AddWithValue("@STEN", STEN);
                cmd.Parameters.AddWithValue("@ITRANGTHAI", ITRANGTHAI);
                vR = Connection.UpdateDatabase(cmd, CThamSo.iKetNoi);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static int Update(String id, String STEN, int ITRANGTHAI)
        {
            int vR = -1;

            try
            {
                String SQL = "UPDATE QT_NHOMNGUOIDUNG SET STEN=@STEN, ITRANGTHAI=@ITRANGTHAI WHERE IID_MANHOMNGUOIDUNG =@id";

                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@STEN", STEN);
                cmd.Parameters.AddWithValue("@ITRANGTHAI", ITRANGTHAI);
                cmd.Parameters.AddWithValue("@id", id);
                vR = Connection.UpdateDatabase(cmd, CThamSo.iKetNoi);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }
        public static int Update_SoNhomCon(String MaNhomNguoiDungCha)
        {
            int vR = -1;

            try
            {
                String SQL = "UPDATE QT_NHOMNGUOIDUNG SET ISOLUONGNHOMCON=ISOLUONGNHOMCON+1 WHERE iID_MaNhomNguoiDung = @id";

                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@id", MaNhomNguoiDungCha);
                vR = Connection.UpdateDatabase(cmd, CThamSo.iKetNoi);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }
        public static int Delete(String id)
        {
            int vR = -1;

            try
            {
                String SQL = "DELETE FROM QT_NHOMNGUOIDUNG WHERE IID_MANHOMNGUOIDUNG = @id";

                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@id", id);
                vR = Connection.UpdateDatabase(cmd, CThamSo.iKetNoi);
                cmd.Dispose();

                vR = 1;
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static SelectOptionList Get_Dropdown_NhomNguoiDung(string sTitle = "")
        {
            DataTable dt = Get_Table("");
            dt.Rows.InsertAt(dt.NewRow(), 0);
            dt.Rows[0]["iID_MaNhomNguoiDung"] = "";
            dt.Rows[0]["sTen"] = sTitle;
            SelectOptionList list = new SelectOptionList(dt, "iID_MaNhomNguoiDung", "sTen");
            dt.Dispose();
            return list;
        }

        #region NHOMNGUOIDUNG_LUAT
        public static DataTable Get_Table_NhomNguoiDung_Luat(String iID_MaNhomNguoiDung)
        {
            DataTable vR = null;

            string SQL = "SELECT PQ_LUAT.* FROM PQ_NHOMNGUOIDUNG_LUAT INNER JOIN PQ_LUAT ON PQ_NHOMNGUOIDUNG_LUAT.IID_MALUAT = PQ_LUAT.IID_MALUAT WHERE PQ_LUAT.bTrangThai = 1 AND PQ_NHOMNGUOIDUNG_LUAT.IID_MANHOMNGUOIDUNG=@IID_MANHOMNGUOIDUNG ORDER BY PQ_LUAT.sTen";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@IID_MANHOMNGUOIDUNG", iID_MaNhomNguoiDung);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();

            return vR;
        }
        public static int Check_NhomNguoiDung_Luat(String iID_MaNhomNguoiDung, String iID_MaLuat)
        {
            int vR = 0;

            string SQL = "SELECT COUNT(*) FROM PQ_NHOMNGUOIDUNG_LUAT WHERE IID_MANHOMNGUOIDUNG=@IID_MANHOMNGUOIDUNG AND IID_MALUAT=@IID_MALUAT";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@IID_MANHOMNGUOIDUNG", iID_MaNhomNguoiDung);
            cmd.Parameters.AddWithValue("@IID_MALUAT", iID_MaLuat);
            vR = Convert.ToInt32(Connection.GetValue(cmd, 0, CThamSo.iKetNoi));
            cmd.Dispose();

            return vR;
        }
        public static int Insert_NhomNguoiDung_Luat(String IID_MANHOMNGUOIDUNG, string IID_MALUAT, int ITRANGTHAI)
        {
            int vR = -1;

            try
            {
                String SQL = "INSERT INTO PQ_NHOMNGUOIDUNG_LUAT(IID_MANHOMNGUOIDUNG, IID_MALUAT, ITRANGTHAI) VALUES(@IID_MANHOMNGUOIDUNG, @IID_MALUAT, @ITRANGTHAI)";

                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@IID_MANHOMNGUOIDUNG", IID_MANHOMNGUOIDUNG);
                cmd.Parameters.AddWithValue("@IID_MALUAT", IID_MALUAT);
                cmd.Parameters.AddWithValue("@ITRANGTHAI", ITRANGTHAI);
                vR = Connection.UpdateDatabase(cmd, CThamSo.iKetNoi);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }
        public static int Delete_NhonNguoiDung_Luat(String MaNhomNguoiDung, string MaLuat)
        {
            int vR = -1;

            try
            {
                String SQL = "DELETE FROM PQ_NHOMNGUOIDUNG_LUAT WHERE IID_MANHOMNGUOIDUNG=@IID_MANHOMNGUOIDUNG AND IID_MALUAT=@IID_MALUAT";

                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@IID_MANHOMNGUOIDUNG", MaNhomNguoiDung);
                cmd.Parameters.AddWithValue("@IID_MALUAT", MaLuat);
                vR = Connection.UpdateDatabase(cmd, CThamSo.iKetNoi);
                cmd.Dispose();

                vR = 1;
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static SelectOptionList Get_Dropdown_NhomNguoiDung()
        {
            DataTable dt = Get_Table("");
            SelectOptionList list = new SelectOptionList(dt, "IID_MANHOMNGUOIDUNG", "STEN");
            dt.Dispose();
            return list;
        }
        #endregion
    }
}
