using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DomainModel;
using DomainModel.Controls;

namespace DATA0200026
{
    public class CPQ_BANG
    {
        public static DataTable Get_Table_DataBase()
        {
            DataTable vR;
            string SQL = "SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_NAME";
            vR = Connection.GetDataTable(SQL, CThamSo.iKetNoi);
            return vR;
        }

        public static DataTable Get_Table_Column_DataBase(String TABLE_NAME)
        {
            DataTable vR;
            string SQL = string.Format("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{0}' ORDER BY COLUMN_NAME", TABLE_NAME);
            vR = Connection.GetDataTable(SQL, CThamSo.iKetNoi);
            return vR;
        }   

        public static DataTable Get_Table()
        {
            DataTable vR;
            string SQL = "SELECT * FROM PQ_DANHMUCBANG ORDER BY STENBANG";
            vR = Connection.GetDataTable(SQL, CThamSo.iKetNoi);
            return vR;
        }

        public static DataTable Get_One_Table(string iID_MaDanhMucBang)
        {
            DataTable vR = null;

            string SQL = "SELECT * FROM PQ_DANHMUCBANG WHERE iID_MaDanhMucBang = @iID_MaDanhMucBang";

            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@iID_MaDanhMucBang", iID_MaDanhMucBang);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();

            return vR;
        }

        public static bool Check_Table(String sTenBang)
        {
            bool vR = false;
            string SQL = "SELECT * FROM PQ_DANHMUCBANG WHERE sTenBang = @sTenBang";

            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@sTenBang", sTenBang);
            DataTable dt = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();
            if(dt.Rows.Count > 0)
            {
                vR = true;
            }
            dt.Dispose();
            return vR;
        }

        public static int Insert(String STENBANG, String STENBANGHT, int BXEM, int BTHEM, int BXOA, int BSUA, int BCHIASE, int BGIAOPHUTRACH, int BHOATDONG)
        {
            int vR = -1;
            
            try
            {
                String SQL = "INSERT INTO PQ_DANHMUCBANG(STENBANG, STENBANGHT, BXEM, BTHEM, BXOA, BSUA, BCHIASE, BGIAOPHUTRACH, BHOATDONG) VALUES(@STENBANG, @STENBANGHT, @BXEM, @BTHEM, @BXOA, @BSUA, @BCHIASE, @BGIAOPHUTRACH, @BHOATDONG)";

                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@STENBANG", STENBANG);
                cmd.Parameters.AddWithValue("@STENBANGHT", STENBANGHT);
                cmd.Parameters.AddWithValue("@BXEM", BXEM);
                cmd.Parameters.AddWithValue("@BTHEM", BTHEM);
                cmd.Parameters.AddWithValue("@BXOA", BXOA);
                cmd.Parameters.AddWithValue("@BSUA", BSUA);
                cmd.Parameters.AddWithValue("@BCHIASE", BCHIASE);
                cmd.Parameters.AddWithValue("@BGIAOPHUTRACH", BGIAOPHUTRACH);
                cmd.Parameters.AddWithValue("@BHOATDONG", BHOATDONG);
                vR = Connection.UpdateDatabase(cmd, CThamSo.iKetNoi);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static int Update(int id, String STENBANG, String STENBANGHT, int BXEM, int BTHEM, int BXOA, int BSUA, int BCHIASE, int BGIAOPHUTRACH, int BHOATDONG)
        {
            int vR = -1;
            
            try
            {
                String SQL = "UPDATE PQ_DANHMUCBANG SET STENBANG=@STENBANG, STENBANGHT=@STENBANGHT, BXEM=@BXEM, BTHEM=@BTHEM, BXOA=@BXOA, BSUA=@BSUA, BCHIASE=@BCHIASE, BGIAOPHUTRACH=@BGIAOPHUTRACH, BHOATDONG=@BHOATDONG WHERE IID_MADANHMUCBANG = @id";

                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@STENBANG", STENBANG);
                cmd.Parameters.AddWithValue("@STENBANGHT", STENBANGHT);
                cmd.Parameters.AddWithValue("@BXEM", BXEM);
                cmd.Parameters.AddWithValue("@BTHEM", BTHEM);
                cmd.Parameters.AddWithValue("@BXOA", BXOA);
                cmd.Parameters.AddWithValue("@BSUA", BSUA);
                cmd.Parameters.AddWithValue("@BCHIASE", BCHIASE);
                cmd.Parameters.AddWithValue("@BGIAOPHUTRACH", BGIAOPHUTRACH);
                cmd.Parameters.AddWithValue("@BHOATDONG", BHOATDONG);
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

        public static int Delete(int id)
        {
            int vR = -1;

            try
            {
                String SQL = "DELETE FROM PQ_DANHMUCBANG WHERE IID_MADANHMUCBANG = @id";

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



        public static DataTable Get_Table_ChucNang_Cam(string iID_MaLuat)
        {
            DataTable vR;
            string SQL = "SELECT * FROM PQ_BANG_CHUCNANGCAM WHERE IID_MALUAT=@IID_MALUAT ORDER BY STENBANG";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@IID_MALUAT", iID_MaLuat);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();

            return vR;
        }

        public static int Insert_Table_ChucNang_Cam(string IID_MALUAT, String STENBANG, String SCHUCNANG)
        {
            int vR = -1;

            try
            {
                String SQL = "INSERT INTO PQ_BANG_CHUCNANGCAM(IID_MALUAT, STENBANG, SCHUCNANG) VALUES(@IID_MALUAT, @STENBANG, @SCHUCNANG)";

                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@IID_MALUAT", IID_MALUAT);
                cmd.Parameters.AddWithValue("@STENBANG", STENBANG);
                cmd.Parameters.AddWithValue("@SCHUCNANG", SCHUCNANG);
                vR = Connection.UpdateDatabase(cmd, CThamSo.iKetNoi);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static int Delete_Table_ChucNang_Cam(string iID_MaLuat)
        {
            int vR = -1;

            try
            {
                String SQL = "DELETE FROM PQ_BANG_CHUCNANGCAM WHERE IID_MALUAT = @IID_MALUAT";

                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@IID_MALUAT", iID_MaLuat);
                vR = Connection.UpdateDatabase(cmd, CThamSo.iKetNoi);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }



        public static DataTable Get_Table_Truong(string TenBang)
        {
            DataTable vR;
            string SQL = "SELECT * FROM PQ_DANHMUCTRUONG WHERE STENBANG=@STENBANG ORDER BY ISTT";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@STENBANG", TenBang);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();

            return vR;
        }
        public static DataTable Get_One_Table_Truong(string iID_MaDanhMucTruong)
        {
            DataTable vR;
            string SQL = "SELECT * FROM PQ_DANHMUCTRUONG WHERE IID_MADANHMUCTRUONG=@IID_MADANHMUCTRUONG";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@IID_MADANHMUCTRUONG", iID_MaDanhMucTruong);
            vR = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();

            return vR;
        }
        public static bool Check_Table_Truong(String sTenBang, String sTenTruong)
        {
            bool vR = false;
            string SQL = "SELECT * FROM PQ_DANHMUCTRUONG WHERE sTenBang = @sTenBang AND sTenTruong = @sTenTruong";

            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@sTenBang", sTenBang);
            cmd.Parameters.AddWithValue("@sTenTruong", sTenTruong);
            DataTable dt = Connection.GetDataTable(cmd, CThamSo.iKetNoi);
            cmd.Dispose();
            if (dt.Rows.Count > 0)
            {
                vR = true;
            }
            dt.Dispose();
            return vR;
        }
        public static int Insert_Table_Truong(String STENBANG, String STENTRUONG, String STENTRUONGHT, int BLUONDUOCXEM)
        {
            int vR = -1;

            try
            {
                String SQL = "INSERT INTO PQ_DANHMUCTRUONG(STENBANG, STENTRUONG, STENTRUONGHT, BLUONDUOCXEM) VALUES(@STENBANG, @STENTRUONG, @STENTRUONGHT, @BLUONDUOCXEM)";

                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@STENBANG", STENBANG);
                cmd.Parameters.AddWithValue("@STENTRUONG", STENTRUONG);
                cmd.Parameters.AddWithValue("@STENTRUONGHT", STENTRUONGHT);
                cmd.Parameters.AddWithValue("@BLUONDUOCXEM", BLUONDUOCXEM);
                vR = Connection.UpdateDatabase(cmd, CThamSo.iKetNoi);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }
        public static int Update_Table_Truong(int id, String STENBANG, String STENTRUONG, String STENTRUONGHT, int BLUONDUOCXEM)
        {
            int vR = -1;

            try
            {
                String SQL = "UPDATE PQ_DANHMUCTRUONG SET STENBANG=@STENBANG, STENTRUONG=@STENTRUONG, STENTRUONGHT=@STENTRUONGHT, BLUONDUOCXEM=@BLUONDUOCXEM WHERE IID_MADANHMUCTRUONG = @id";

                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@STENBANG", STENBANG);
                cmd.Parameters.AddWithValue("@STENTRUONG", STENTRUONG);
                cmd.Parameters.AddWithValue("@STENTRUONGHT", STENTRUONGHT);
                cmd.Parameters.AddWithValue("@BLUONDUOCXEM", BLUONDUOCXEM);
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
        public static int Sort_Table_Truong(int id, int iSTT)
        {
            int vR = -1;

            try
            {
                String SQL = "UPDATE PQ_DANHMUCTRUONG SET iSTT = @iSTT WHERE IID_MADANHMUCTRUONG = @id";

                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@iSTT", iSTT);
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
        public static int Delete_Table_Truong(string IID_MADANHMUCTRUONG)
        {
            int vR = -1;

            try
            {
                String SQL = "DELETE FROM PQ_DANHMUCTRUONG WHERE IID_MADANHMUCTRUONG = @IID_MADANHMUCTRUONG";

                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@IID_MADANHMUCTRUONG", IID_MADANHMUCTRUONG);
                vR = Connection.UpdateDatabase(cmd, CThamSo.iKetNoi);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }


        public static String Get_DanhSach_Truong_Cam(string iID_MaLuat, string TenBang)
        {
            String vR = "";
            string SQL = "SELECT STENTRUONG FROM PQ_BANG_TRUONGCAM WHERE IID_MALUAT=@IID_MALUAT AND STENBANG=@STENBANG ORDER BY STENTRUONG";
            SqlCommand cmd = new SqlCommand(SQL);
            cmd.Parameters.AddWithValue("@IID_MALUAT", iID_MaLuat);
            cmd.Parameters.AddWithValue("@STENBANG", TenBang);
            vR = Convert.ToString(Connection.GetValue(cmd, "", CThamSo.iKetNoi));
            cmd.Dispose();

            return vR;
        }
        public static int Insert_Table_Truong_Cam(string IID_MALUAT, String STENBANG, String STENTRUONG)
        {
            int vR = -1;

            try
            {
                String SQL = "INSERT INTO PQ_BANG_TRUONGCAM(IID_MALUAT, STENBANG, STENTRUONG) VALUES(:IID_MALUAT, :STENBANG, :STENTRUONG)";

                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@IID_MALUAT", IID_MALUAT);
                cmd.Parameters.AddWithValue("@STENBANG", STENBANG);
                cmd.Parameters.AddWithValue("@STENTRUONG", STENTRUONG);
                vR = Connection.UpdateDatabase(cmd, CThamSo.iKetNoi);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }

        public static int Delete_Table_Truong_Cam(string iID_MaLuat, String STENBANG)
        {
            int vR = -1;

            try
            {
                String SQL = "DELETE FROM PQ_BANG_TRUONGCAM WHERE IID_MALUAT = :IID_MALUAT AND STENBANG=@STENBANG";

                SqlCommand cmd = new SqlCommand(SQL);
                cmd.Parameters.AddWithValue("@IID_MALUAT", iID_MaLuat);
                cmd.Parameters.AddWithValue("@STENBANG", STENBANG);
                vR = Connection.UpdateDatabase(cmd, CThamSo.iKetNoi);
                cmd.Dispose();
            }
            catch (Exception ex)
            {
                vR = -1;
            }

            return vR;
        }
    }
}
