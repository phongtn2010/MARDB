using DomainModel.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA0200025
{
    public class CLogNSW
    {
        public static void Add(string sChucNang, string sID_MaNguoiDung, string sNoiDung, String sUserName, String sIP)
        {
            try
            {
                Bang bang = new Bang("CNN25_Log_NSW");
                bang.MaNguoiDungSua = sUserName;
                bang.IPSua = sIP;
                bang.DuLieuMoi = true;
                bang.CmdParams.Parameters.AddWithValue("@sChucNang", sChucNang);
                bang.CmdParams.Parameters.AddWithValue("@sID_MaNguoiDung", sID_MaNguoiDung);
                bang.CmdParams.Parameters.AddWithValue("@sNoiDung", sNoiDung);

                bang.Save();
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
