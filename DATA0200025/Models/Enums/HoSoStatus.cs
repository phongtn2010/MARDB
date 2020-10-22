using EnumsNET;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA0200025.Models.Enums
{
    public enum HSStatus
    {

        //  // Bộ phận một cửa
        //  [Description("Chờ tiếp nhận")] CHO_TIEP_NHAN = 1,
        //  //[Description("Chờ xem xét hồ sơ")] YEU_CAU_SUA_HO_SO = 2,
        // // [Description("Chờ xử lý hồ sơ")] CHO_XU_LY_HO_SO = 3,
        //
        //  [Description("Chờ phân công")] CHO_PHAN_CONG = 2,
        //  [Description("Yêu cầu sửa hồ sơ")] YEU_CAU_SUA_HO_SO = 3,
        //  [Description("Từ chối hồ sơ")] TU_CHOI_HO_SO = 4,
        //  [Description("END")] END = 5,
        //  [Description("Chờ chuyên viên xử lý")] CHO_CHUYEN_VIEN_XU_LY = 6,
        //
        //  [Description("Giấy phép trình Lãnh đạo phòng")] TRINH_LANH_DAO_PHONG = 7,
        //
        //  [Description("Trình lãnh đạo văn phòng")] TRINH_LANH_DAO_VAN_PHONG = 8,
        //  [Description("Trình lãnh đạo cục")] TRINH_LANH_DAO_CUC = 9,
        //  [Description("Đã cấp giấy phép")] DA_CAP_GIAY_PHEP = 10,
        //  [Description("Doanh nghiệp xin huỷ hồ sơ")]
        //  DOANH_NGHIEP_XIN_HUY_HO_SO = 11,
        //  [Description("Doanh nghiệp xin sửa hồ sơ")]
        //  DOANH_NGHIEP_XIN_SUA_HO_SO = 12,
        //
        //  [Description("Chuyên viên xử lý và trình lãnh đạo văn phòng")]
        //  CHUYEN_VIEN_XU_LY_VA_TRINH_LANH_DAO_VAN_PHONG = 13,
        //
        //  [Description("Chuyên viên sửa giấy phép và trình lãnh đạo văn phòng")]
        //  CHUYEN_VIEN_SUA_GIAY_PHEP_VA_TRINH_LANH_DAO_VAN_PHONG = 14,
        //
        //  [Description("Chuyên viên sửa giấy phép và trình lãnh đạo cục")]
        //  CHUYEN_VIEN_SUA_GIAY_PHEP_VA_TRINH_LANH_DAO_CUC = 15,
        //
        //  [Description("Chuyên viên sửa giấy phép và trình lãnh đạo văn phòng một cửa")]
        //  CHUYEN_VIEN_SUA_GIAY_PHEP_VA_TRINH_LANH_DAO_VAN_PHONG_MOT_CUA = 16,
        //      
        //  [Description("Giấy phép bị từ chối")] TU_CHOI_GIAY_PHEP = 17,
        //  
        //  [Description("Giấy phép chờ gửi doanh nghiệp")] GIAY_PHEP_CHO_GUI_DN = 18,
        //  
        //  [Description("Đang soạn giấy phép")] DANG_SOAN_GIAY_PHEP = 19,
        //  
        //  [Description("BPMC Từ chối sửa giấy phép")] TU_CHOI_SUA_GIAY_PHEP = 20,
        //  [Description("Trình lãnh đạo phòng yêu cầu huỷ hồ sơ")]TRINH_LANH_DAO_PHONG_YEU_CAU_HUY_HO_SO = 21,
        //  [Description("Chuyên viên trình lãnh đạo phòng yêu cầu huỷ hồ sơ")]CV_TRINH_LANH_DAO_PHONG_YEU_CAU_HUY_HO_SO = 22,
        //  [Description("Chuyên viên trình lãnh đạo phòng yêu cầu huỷ hồ sơ")]LDP_TRINH_LANH_DAO_VAN_PHONG_YEU_CAU_HUY_HO_SO = 23,
        //  [Description("Chuyên viên trình lãnh đạo phòng yêu cầu huỷ hồ sơ")]LDVP_TRINH_LANH_DAO_CUC_YEU_CAU_HUY_HO_SO = 24,

        [Description("END")] END = 0,

        // Bộ phận một cửa
        [Description("Chờ tiếp nhận")] CHO_TIEP_NHAN = 1,
        [Description("Yêu cầu sửa hồ sơ")] YEU_CAU_SUA_HO_SO = 2,
        [Description("Từ chối hồ sơ")] TU_CHOI_HO_SO = 3,// cho về 0
        [Description("Chờ lãnh đạo phòng phân công xử lý hồ sơ")] LDP_CHO_LANH_DAO_PHONG_PHAN_CONG_XU_LY_HO_SO = 4,
        [Description("Chờ chuyên viên xử lý hồ sơ")] CHO_CHUYEN_VIEN_XU_LY_HO_SO = 5,
        [Description("Chuyên viên yêu cầu bổ sung hồ sơ")] CV_CHUYEN_VIEN_YEU_CAU_BO_SUNG_HO_SO = 6,
        [Description("Chuyên viên từ chối hồ sơ")] CV_CHUYEN_VIEN_TU_CHOI_HO_SO = 7, // cho về 0
        [Description("Chờ lãnh đạo phòng xem xét giấy phép")] LDP_CHO_LANH_DAO_PHONG_XEM_XET_GIAY_PHEP = 8,// từ chối thì chuyển về chuyên viên
        [Description("Chờ lãnh đạo văn phòng xem xét giấy phép")] LDVP_CHO_LANH_DAO_VAN_PHONG_XEM_XET_GIAY_PHEP = 9,// từ chối thì chuyển về chuyên viên
        [Description("Chờ lãnh đạo cục phê duyệt giấy phép")] LDC_CHO_LANH_DAO_CUC_PHE_DUYET_GIAY_PHEP = 10,// từ chối thì chuyển về chuyên viên
        [Description("Lãnh đạo cục đã ký giấy phép")] LDC_LANH_DAO_CUC_DA_KY_GIAY_PHEP = 11,
        [Description("Đã gửi giấy phép cho doanh nghiệp")] BPMC_DA_GUI_GIAY_PHEP_CHO_DOANH_NGHIEP = 12,

        [Description("Chờ tiếp nhận yêu cầu sửa hồ sơ")] BPMC_CHO_TIEP_NHAN_YEU_CAU_SUA_H0_SO = 13, //nếu đồng ý thì thực hiện tiếp tục 4, nếu từ chối giữ nguyên trạng thái

        [Description("Hồ sơ doanh nghiệp đã huỷ")] TCT_HO_SO_DOANH_NGHIEP_DA_HUY = 40, // cho về 0

        [Description("Chờ tiếp nhận yêu cầu huỷ hồ sơ")] BPMC_CHO_TIEP_NHAN_YEU_CAU_HUY_H0_SO = 31,//Từ chối cho thành 30
        [Description("Chờ lãnh đạo phòng phân công xử lý yêu cầu huỷ hồ sơ")] LDP_CHO_LANH_DAO_PHONG_PHAN_CONG_XU_LY_YEU_CAU_HUY_HO_SO = 32,
        [Description("Chờ chuyên viên xử lý yêu cầu huỷ hồ sơ")] CV_CHO_CHUYEN_VIEN_XU_LY_YEU_CAU_HUY_HO_SO = 33,// đồng ý hoặc từ chối thì gửi lý do cho lãnh đạo phòng
        [Description("Chờ lãnh đạo phòng xem xét từ chối yêu cầu huỷ hồ sơ")] LDP_CHO_LANH_DAO_PHONG_XEM_XET_TU_CHOI_YEU_CAU_HUY_HO_SO = 34,// nếu từ chối thì chuyển về chuyên viên , đồng ý thì gửi lãnh đạo văn phòng
        [Description("Chờ lãnh đạo phòng xem xét đồng ý yêu cầu huỷ hồ sơ")] LDP_CHO_LANH_DAO_PHONG_XEM_XET_DONG_Y_YEU_CAU_HUY_HO_SO = 35,// nếu từ chối thì chuyển về chuyên viên , đồng ý thì gửi lãnh đạo văn phòng
        [Description("Chờ lãnh đạo văn phòng xem xét từ chối yêu cầu huỷ hồ sơ")] LDVP_CHO_LANH_DAO_VAN_PHONG_XEM_XET_TU_CHOI_YEU_CAU_HUY_HO_SO = 36,//từ chối thì gửi về chuyên viên đồng ý thì gửi lãnh đạo cục
        [Description("Chờ lãnh đạo văn phòng xem xét đồng ý yêu cầu huỷ hồ sơ")] LDVP_CHO_LANH_DAO_VAN_PHONG_XEM_XET_DONG_Y_YEU_CAU_HUY_HO_SO = 37,
        [Description("Chờ lãnh đạo cục phê duyệt từ chối yêu cầu huỷ hồ sơ")] LDC_CHO_LANH_DAO_CUC_PHE_DUYET_TU_CHOI_YEU_CAU_HUY_HO_SO = 38,// từ chối back về chuyên viên, đồng ý back về 30
        [Description("Chờ lãnh đạo cục phê duyệt đồng ý yêu cầu huỷ hồ sơ")] LDC_CHO_LANH_DAO_CUC_PHE_DUYET_DONG_Y_YEU_CAU_HUY_HO_SO = 39,// từ chối back về chuyên viên, chuyển về trạng thái 40 và về 0

        [Description("Chờ tiếp nhận yêu cầu sửa giấy phép")] BPMC_CHO_TIEP_NHAN_YEU_CAU_SUA_GIAY_PHEP = 14, // 
        //Trường hợp từ chối sửa giấy phép: lưu lịch sử hồ sơ BPMC từ chối sửa giấy phép // hồ sơ vẫn ở trạng thái đã gửi giấy phép, end luồng
        [Description("Chờ lãnh đạo phòng phân công xử lý yêu cầu sửa giấy phép")] LDP_CHO_LANH_DAO_PHONG_PHAN_CONG_XU_LY_YEU_CAU_SUA_GIAY_PHEP = 15,
        [Description("Chờ chuyên viên xử lý yêu cầu sửa giấy phép")] CV_CHO_CHUYEN_VIEN_XU_LY_YEU_CAU_SUA_GIAY_PHEP = 16,
        [Description("Chờ lãnh đạo phòng xem xét từ chối yêu cầu sửa giấy phép")] LDP_CHO_LANH_DAO_PHONG_XEM_XET_TU_CHOI_YEU_CAU_SUA_GIAY_PHEP = 17,
        [Description("Chờ lãnh đạo phòng xem xét giấy phép sửa theo yêu cầu doanh nghiệp")] LDP_CHO_LANH_DAO_PHONG_XEM_XET_GIAY_PHEP_GIAY_PHEP_SUA_THEO_YEU_CAU_DOANH_NGHIEP = 18,
        [Description("Chờ lãnh đạo văn phòng xem xét từ chối yêu cầu sửa giấy phép")] LDVP_CHO_LANH_DAO_VAN_PHONG_XEM_XET_TU_CHOI_YEU_CAU_SUA_GIAY_PHEP = 19,
        [Description("Chờ lãnh đạo văn phòng xem xét giấy phép sửa theo yêu cầu doanh nghiệp")] LDVP_CHO_LANH_DAO_VAN_PHONG_XEM_XET_GIAY_PHEP_SUA_THEO_YEU_CAU_DOANH_NGHIEP = 20,
        [Description("Chờ lãnh đạo cục phê duyệt từ chối yêu cầu sửa giấy phép")] LDC_CHO_LANH_DAO_CUC_PHE_DUYET_TU_CHOI_YEU_CAU_SUA_GIAY_PHEP = 21,// lãnh đạo cục đồng ý thì trạng thái hồ sơ chuyển về đã gửi giấy phép cho doanh nghiệp
        [Description("Chờ lãnh đạo cục phê duyệt giấy phép sửa theo yêu cầu doanh nghiệp")] LDC_CHO_LANH_DAO_CUC_PHE_DUYET_GIAY_PHEP_SUA_THEO_YEU_CAU_DOANH_NGHIEP = 22,

        [Description("Lãnh đạo cục đã ký giấy phép chỉnh sửa")] BPMC_LANH_DAO_CUC_DA_KY_GIAY_PHEP_CHINH_SUA = 23,
        [Description("Đã gửi giấy phép chỉnh sửa cho doanh nghiệp")] BPMC_DA_GUI_GIAY_PHEP_CHINH_SUA_CHO_DOANH_NGHIEP = 24,

        [Description("Chờ lãnh đạo phòng xem xét chuyên viên sửa giấy phép")] LDP_CHO_LANH_DAO_PHONG_XEM_XET_CHUYEN_VIEN_SUA_GIAY_PHEP = 25,
        [Description("Chờ lãnh đạo văn phòng xem xét chuyên viên sửa giấy phép")] LDVP_CHO_LANH_DAO_VAN_PHONG_XEM_XET_CHUYEN_VIEN_SUA_GIAY_PHEP = 26,
        [Description("Chờ lãnh đạo cục phê duyệt chuyên viên sửa giấy phép")] LDC_CHO_LANH_DAO_CUC_PHE_DUYET_CHUYEN_VIEN_SUA_GIAY_PHEP = 27,// nếu đồng ý về 23 rồi thực hiện tiếp






        // // Lãnh đạo phòng

        // [Description("Chờ lãnh đạo phòng xem xét từ chối yêu cầu sửa giấy phép")] LDP_CHO_LANH_DAO_PHONG_XEM_XET_TU_CHOI_YEU_CAU_SUA_GIAY_PHEP = 4,
        // [Description("Chờ lãnh đạo phòng xem xét giấy phép sửa theo yêu cầu doanh nghiệp)] LDP_CHO_LANH_DAO_PHONG_XEM_XET_GIAY_PHEP_GIAY_PHEP_SUA_THEO_YEU_CAU_DOANH_NGHIEP = 4,



        // trường hợp chuyên viên chủ động sửa giấy phép đã cấp

        // trường hợp lãnh đạo phòng thu hồi giấy phép đã xem xét: thì đổi trạng thái thành chờ lãnh đạo phòng xem xét giấy phép

        //Chuyên viên


        // trường hợp đồng ý: thì soạn giấy phép và gửi lên lãnh đạo phòng
        // // [Description("Chờ lãnh đạo phòng xem xét giấy phép)] LDP_HO_SO_CHO_LANH_DAO_PHONG_PHAN_CONG_XU_LY = 4,


        // trường hợp chuyên viên thu hồi giấy phép chờ lãnh đạo phòng phê duyệt: thì đổi trạng hồ sơ về chờ chuyên viên xử lý hồ sơ



        // trường hợp chuyên viên chủ động sửa giấy phép đã cấp

        // trường hợp lãnh đạo văn phòng thu hồi giấy phép đã xem xét: thì đổi trạng thái chờ lãnh đạo văn phòng xem xét giấy phép

        // lãnh đạo cục




        // trường hợp chuyên viên chủ động sửa giấy phép đã cấp


    }

    public class EnumValue
    {
        public string name { get; set; }
        public int value { get; set; }
    }

    public static class EnumExtensions
    {
        public static List<EnumValue> GetValues<T>()
        {
            var values = new List<EnumValue>();
            foreach (var itemType in Enum.GetValues(typeof(T)))
            {
                var description = ((HSStatus)itemType).AsString(EnumFormat.Description);
                //For each value of this enumeration, add a new EnumValue instance
                values.Add(new EnumValue
                {
                    // Name = Enum.GetName(typeof(T), itemType),

                    name = description,
                    value = (int)itemType
                });
            }

            return values;
        }

        public static string GetEnumDescription(Enum value)
        {
            var fi = value.GetType().GetField(value.ToString());

            var attributes = fi.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[];

            if (attributes != null && attributes.Any()) return attributes.First().Description;

            return value.ToString();
        }
    }
}
