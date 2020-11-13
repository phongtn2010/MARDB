using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using DomainModel.Controls;
using System.Collections.Specialized;
using System.Reflection;
using System.Web.Mvc;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace DomainModel
{
    public class MyHtmlHelper
    {
        private static string LayGiaTriCuaTruong(string ParentID, NameValueCollection data, string TenTruong)
        {
            String vR = data[TenTruong];
            if (String.IsNullOrEmpty(vR))
            {
                vR = data[ParentID + "_" + TenTruong];
            }
            if (TenTruong.StartsWith("b"))
            {
                if (vR != null && (vR.ToUpper() == "ON" || vR.ToUpper() == "TRUE" || vR.ToUpper() == "1"))
                {
                    vR = "1";
                }
                else
                {
                    vR = "0";
                }
            }
            return vR;
        }

        #region "Label"
        public static string Label(NameValueCollection data, string TenTruong, string DanhSachTruongCam, string Attributes, int LoaiLabel)
        {
            string sNewName = TenTruong;
            if (TenTruong.ToUpper().StartsWith("D"))
            {
                sNewName = NgonNgu.MaDate + TenTruong;
            }
            return Label(data[sNewName], TenTruong, DanhSachTruongCam, Attributes, LoaiLabel);
        }
        public static string Label(NameValueCollection data, string TenTruong, string DanhSachTruongCam, string Attributes)
        {
            return Label(data, TenTruong, DanhSachTruongCam, Attributes, 0);
        }
        public static string Label(NameValueCollection data, string TenTruong, string DanhSachTruongCam)
        {
            return Label(data, TenTruong, DanhSachTruongCam, null);
        }
        public static string Label(NameValueCollection data, string TenTruong)
        {
            return Label(data, TenTruong, null, null);
        }
        public static string Label(string ParentID, NameValueCollection data, string TenTruong, string DanhSachTruongCam, string Attributes)
        {
            return Label(data, TenTruong, DanhSachTruongCam, Attributes);
        }
        public static string Label(Object Text, string TenTruong, string DanhSachTruongCam, string Attributes, int LoaiLabel)
        {
            string sR = "";
            string sGiaTri = "&nbsp;";


            if (BaoMat.ChoPhepNhap(TenTruong, DanhSachTruongCam))
            {
                if (Text != DBNull.Value)
                {
                    Boolean ok = false;
                    if (LoaiLabel == 0)
                    {
                        ok = (TenTruong.StartsWith("iDM") == false &&
                              TenTruong.StartsWith("iID") == false &&
                              TenTruong.StartsWith("i"))
                             || TenTruong.StartsWith("r");
                    }
                    else if (LoaiLabel == 1)
                    {
                        ok = true;
                    }
                    else
                    {
                        ok = false;
                    }
                    if (ok)
                    {
                        sGiaTri = CommonFunction.DinhDangSo(Convert.ToString(Text));
                    }
                    else
                    {
                        sGiaTri = Convert.ToString(Text);
                    }
                }
            }
            else
            {
                sGiaTri = "*";
            }
            sR = String.Format("<span id='{0}' {1}>{2}</span>", TenTruong, Attributes, sGiaTri);
            return sR;
        }
        public static string Label(Object Text, string TenTruong, string DanhSachTruongCam, string Attributes)
        {
            return Label(Text, TenTruong, DanhSachTruongCam, Attributes, 0);
        }
        public static string Label(Object Text, string TenTruong, string DanhSachTruongCam)
        {
            return Label(Text, TenTruong, DanhSachTruongCam, null);
        }
        public static string Label(Object Text, string TenTruong)
        {
            return Label(Text, TenTruong, null, null);
        }
        public static string Label(string ParentID, Object Text, string TenTruong, string DanhSachTruongCam, string Attributes)
        {
            string sR = "";
            string sGiaTri = "&nbsp;";


            if (BaoMat.ChoPhepNhap(TenTruong, DanhSachTruongCam))
            {
                if (Text != DBNull.Value)
                {
                    sGiaTri = Convert.ToString(Text);
                }
            }
            else
            {
                sGiaTri = "*";
            }
            sR = String.Format("<span id='{0}_{1}' {2}>{3}</span>", ParentID, TenTruong, Attributes, sGiaTri);
            return sR;
        }
        //public static string Label(string ParentID, Object Text, string TenTruong, string DanhSachTruongCam)
        //{
        //    return Label(ParentID, Text, TenTruong, DanhSachTruongCam, null);
        //}
        //public static string Label(string ParentID, Object Text, string TenTruong)
        //{
        //    return Label(ParentID, Text, TenTruong, null, null);
        //}
        #endregion

        #region "LabelCheckBox"
        public static string LabelCheckBox(string ParentID, NameValueCollection data, string TenTruong, string DanhSachTruongCam, string Attributes)
        {
            return LabelCheckBox(ParentID, LayGiaTriCuaTruong(ParentID, data, TenTruong), TenTruong, DanhSachTruongCam, Attributes);
        }
        public static string LabelCheckBox(string ParentID, NameValueCollection data, string TenTruong, string DanhSachTruongCam)
        {
            return LabelCheckBox(ParentID, data, TenTruong, DanhSachTruongCam, null);
        }
        public static string LabLabelCheckBoxel(NameValueCollection data, string TenTruong)
        {
            return Label(data, TenTruong, null, null);
        }
        public static string LabelCheckBox(string ParentID, Object Text, string TenTruong, string DanhSachTruongCam, string Attributes)
        {
            string sR = "";
            string sGiaTri = "&nbsp;";


            if (BaoMat.ChoPhepNhap(TenTruong, DanhSachTruongCam))
            {
                if (Text != DBNull.Value)
                {
                    if (Convert.ToString(Text).ToUpper() == "TRUE" || Convert.ToString(Text).ToUpper() == "1" || Convert.ToString(Text).ToUpper() == "ON")
                    {
                        Attributes = "class='checktick'";
                    }
                    else
                    {
                        Attributes = "class='cancelcheck'";
                    }
                }
            }
            else
            {
                sGiaTri = "*";
            }
            sR = String.Format("<span id='{0}_{1}' {2}>&nbsp;</span>", ParentID, TenTruong, Attributes, sGiaTri);
            return sR;
        }
        public static string LabelCheckBox(string ParentID, Object Text, string TenTruong, string DanhSachTruongCam)
        {
            return LabelCheckBox(ParentID, Text, TenTruong, DanhSachTruongCam, null);
        }
        public static string LabelCheckBox(string ParentID, Object Text, string TenTruong)
        {
            return LabelCheckBox(ParentID, Text, TenTruong, null, null);
        }
        #endregion

        #region "Image"
        public static string Image(NameValueCollection data, string URLPath, string TenTruong, string DanhSachTruongCam, string Attributes)
        {
            return Image(data[TenTruong], URLPath, TenTruong, DanhSachTruongCam, Attributes);
        }
        public static string Image(NameValueCollection data, string URLPath, string TenTruong, string DanhSachTruongCam)
        {
            return Image(data, URLPath, TenTruong, DanhSachTruongCam, null);
        }
        public static string Image(NameValueCollection data, string URLPath, string TenTruong)
        {
            return Image(data, URLPath, TenTruong, null, null);
        }
        public static string Image(Object Source, string URLPath, string TenTruong, string DanhSachTruongCam, string Attributes)
        {
            string sR = "";
            string sGiaTri = "";
            string sNewName = TenTruong;
            if (TenTruong.ToUpper().StartsWith("D"))
            {
                sNewName = NgonNgu.MaDate + TenTruong;
            }

            if (BaoMat.ChoPhepNhap(TenTruong, DanhSachTruongCam))
            {
                if (Source != DBNull.Value)
                {
                    sGiaTri = Convert.ToString(Source);
                }
                sR = String.Format("<img src=\"{3}{2}\" {1} />", TenTruong, Attributes, sGiaTri, URLPath);
            }
            return sR;
        }
        public static string Image(Object Source, string URLPath, string TenTruong, string DanhSachTruongCam)
        {
            return Image(Source, URLPath, TenTruong, DanhSachTruongCam, null);
        }
        public static string Image(Object Source, string URLPath)
        {
            return Image(Source, URLPath, null, null, null);
        }
        #endregion

        #region "LabelForDropDownList"
        public static string LabelForDropDownList(SelectOptionList Options, NameValueCollection data, string TenTruong, string DanhSachTruongCam, string Attributes)
        {
            string sR = "";
            object Value = data[TenTruong];
            string sGiaTri = "";

            if (BaoMat.ChoPhepNhap(TenTruong, DanhSachTruongCam))
            {
                int i;
                DataTable dt = Options.dtData;
                string DisplayMember = Options.DisplayMember.ToString();
                string ValueMember = Options.ValueMember.ToString();

                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    if (Value != null && dt.Rows[i][ValueMember].ToString() == Value.ToString())
                    {
                        sGiaTri = dt.Rows[i][DisplayMember].ToString();
                    }
                }
            }
            else
            {
                sGiaTri = "*";
            }
            sR = String.Format("<span id=\"{0}\" name=\"{0}\" {1}>{2}</span>", TenTruong, Attributes, sGiaTri);
            return sR;
        }
        public static string LabelForDropDownList(SelectOptionList Options, NameValueCollection data, string TenTruong, string DanhSachTruongCam)
        {
            return LabelForDropDownList(Options, data, TenTruong, DanhSachTruongCam, null);
        }
        public static string LabelForDropDownList(SelectOptionList Options, NameValueCollection data, string TenTruong)
        {
            return LabelForDropDownList(Options, data, TenTruong, null, null);
        }
        public static string LabelForDropDownList(SelectOptionList Options, object Value, string TenTruong, string DanhSachTruongCam, string Attributes)
        {
            string sR = "";
            // object Value = value;
            string sGiaTri = "";

            if (BaoMat.ChoPhepNhap(TenTruong, DanhSachTruongCam))
            {
                int i;
                DataTable dt = Options.dtData;
                string DisplayMember = Options.DisplayMember.ToString();
                string ValueMember = Options.ValueMember.ToString();

                for (i = 0; i <= dt.Rows.Count - 1; i++)
                {
                    if (Value != null && dt.Rows[i][ValueMember].ToString() == Value.ToString())
                    {
                        sGiaTri = dt.Rows[i][DisplayMember].ToString();
                    }
                }
            }
            else
            {
                sGiaTri = "*";
            }
            sR = String.Format("<span id=\"{0}\" name=\"{0}\" {1}>{2}</span>", TenTruong, Attributes, sGiaTri);
            return sR;
        }
        public static string LabelForDropDownList(SelectOptionList Options, object Value, string TenTruong, string DanhSachTruongCam)
        {
            return LabelForDropDownList(Options, Value, TenTruong, DanhSachTruongCam, null);
        }
        public static string LabelForDropDownList(SelectOptionList Options, object Value, string TenTruong)
        {
            return LabelForDropDownList(Options, Value, TenTruong, null, null);
        }
        #endregion

        #region "DropDownList"
        public static string DropDownList(string ParentID, SelectOptionList Options, string Value, string TenTruong, string DanhSachTruongCam, string Attributes, Boolean DuLieuCoLuu = true)
        {
            string ControlID = String.Format("{0}_{1}", ParentID, TenTruong);
            string sR = "";
            string sDisabled = "";
            string sOptions = "";

            if (string.IsNullOrEmpty(ControlID))
            {
                ControlID = TenTruong;
            }
            if (BaoMat.ChoPhepNhap(TenTruong, DanhSachTruongCam))
            {
                int i;
                DataTable dt = Options.dtData;
                if (dt != null)
                {
                    string DisplayMember = Options.DisplayMember.ToString();
                    string ValueMember = Options.ValueMember.ToString();
                    string sAttributes;
                    string sSelected = "";
                    for (i = 0; i <= dt.Rows.Count - 1; i++)
                    {
                        sSelected = "";
                        sAttributes = "";
                        if (Value != null && dt.Rows[i][ValueMember].ToString() == Value)
                        {
                            sSelected = "selected=\"selected\"";
                        }
                        if (Options.AttributeMember != null)
                        {
                            string AttributeMember = Options.AttributeMember.ToString();
                            sAttributes = dt.Rows[i][AttributeMember].ToString();
                        }
                        sOptions += String.Format("<option value=\"{0}\" {2} {3}>{1}</option>", dt.Rows[i][ValueMember], dt.Rows[i][DisplayMember], sSelected, sAttributes);
                    }
                }
            }
            else
            {
                sDisabled = "disabled=\"disabled\"";
                sOptions = "<option value=\"*\">*</option>";
            }
            if (DuLieuCoLuu)
            {
                sR = String.Format("<select id=\"{0}\" name=\"{0}\" {1} {2}>{3}</select>", ControlID, sDisabled, Attributes, sOptions);
            }
            else
            {
                sR = String.Format("<select id=\"{0}\" {1} {2}>{3}</select>", ControlID, sDisabled, Attributes, sOptions);
            }
            return sR;
        }
        public static string DropDownList(string ParentID, SelectOptionList Options, string Value, string TenTruong, string DanhSachTruongCam)
        {
            return DropDownList(ParentID, Options, Value, TenTruong, DanhSachTruongCam, null);
        }
        public static string DropDownList(string ParentID, SelectOptionList Options, string Value)
        {
            return DropDownList(ParentID, Options, Value, null, null, null);
        }
        public static string DropDownList(string ParentID, SelectOptionList Options, NameValueCollection data, string TenTruong, string DanhSachTruongCam, string Attributes)
        {
            String value = null;
            if (data != null)
            {
                value = LayGiaTriCuaTruong(ParentID, data, TenTruong);
            }
            return DropDownList(ParentID, Options, LayGiaTriCuaTruong(ParentID, data, TenTruong), TenTruong, DanhSachTruongCam, Attributes);
        }
        public static string DropDownList(string ParentID, SelectOptionList Options, NameValueCollection data, string TenTruong, string DanhSachTruongCam)
        {
            String value = null;
            if (data != null)
            {
                value = LayGiaTriCuaTruong(ParentID, data, TenTruong);
            }
            return DropDownList(ParentID, Options, LayGiaTriCuaTruong(ParentID, data, TenTruong), TenTruong, DanhSachTruongCam, null);
        }
        public static string DropDownList(string ParentID, SelectOptionList Options, NameValueCollection data, string TenTruong)
        {
            String value = null;
            if (data != null)
            {
                value = LayGiaTriCuaTruong(ParentID, data, TenTruong);
            }
            return DropDownList(ParentID, Options, value, TenTruong, null, null);
        }
        #endregion

        #region "TextBox"
        /// <summary>
        /// TextBox
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="Value"></param>
        /// <param name="TenTruong"></param>
        /// <param name="DanhSachTruongCam"></param>
        /// <param name="Attributes"></param>
        /// <param name="LoaiTextBox">0(Mặc định):Nhập số và chữ, 1:Chỉ nhập số, 2:Nhập thông thường</param>
        /// <returns></returns>
        public static string TextBox(Object Attrs)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(Attrs);
            string ParentID = (props["ParentID"] == null) ? "" : Convert.ToString(props["ParentID"].GetValue(Attrs));
            object Value = (props["Value"] == null) ? null : props["Value"].GetValue(Attrs);
            string TenTruong = (props["TenTruong"] == null) ? "" : Convert.ToString(props["TenTruong"].GetValue(Attrs));
            string DanhSachTruongCam = (props["DanhSachTruongCam"] == null) ? "" : Convert.ToString(props["DanhSachTruongCam"].GetValue(Attrs));
            string Attributes = (props["Attributes"] == null) ? "" : Convert.ToString(props["Attributes"].GetValue(Attrs));
            int LoaiTextBox = (props["LoaiTextBox"] == null) ? 0 : Convert.ToInt32(props["LoaiTextBox"].GetValue(Attrs));
            int SoSauDauPhay = (props["SoSauDauPhay"] == null) ? -1 : Convert.ToInt32(props["SoSauDauPhay"].GetValue(Attrs));
            Boolean DuLieuCoLuu = (props["DuLieuCoLuu"] == null) ? true : Convert.ToBoolean(props["DuLieuCoLuu"].GetValue(Attrs));
            NameValueCollection data = (props["data"] == null) ? null : (NameValueCollection)(props["data"].GetValue(Attrs));
            if (data != null)
            {
                Value = LayGiaTriCuaTruong(ParentID, data, TenTruong);
            }
            return TextBox(ParentID, Value, TenTruong, DanhSachTruongCam, Attributes, LoaiTextBox, DuLieuCoLuu, SoSauDauPhay);
        }
        public static string TextBox(string ParentID, object Value, string TenTruong = null, string DanhSachTruongCam = null, string Attributes = null, int LoaiTextBox = 0, Boolean DuLieuCoLuu = true, int SoSauDauPhay = -1)
        {
            if (string.IsNullOrEmpty(TenTruong)) TenTruong = "";
            //Xác định ControlID
            string ControlID = ParentID;
            if (string.IsNullOrEmpty(ControlID) == false)
            {
                ControlID += "_";
            }
            ControlID += TenTruong;
            string sR = "";
            string sGiaTri = "";
            string sDisabled = "";
            Boolean okCam = false;

            //Xác định giá trị hiển thị trên TextBox
            if (BaoMat.ChoPhepNhap(TenTruong, DanhSachTruongCam))
            {
                //Nếu có quyền nhập thì hiển thị giá trị
                sGiaTri = String.Format("{0}", Value);
            }
            else
            {
                //Nếu có quyền nhập thì ẩn giá trị
                okCam = true;
                sGiaTri = "*";
                sDisabled = "disabled=\"disabled\"";
            }
            //Giải mã giá trị hiển thị ra
            sGiaTri = HttpUtility.HtmlEncode(sGiaTri);
            if (DuLieuCoLuu)
            {
                sR = String.Format("<input id=\"{0}\" name=\"{0}\" value=\"{1}\" {2} {3} />", ControlID, sGiaTri, sDisabled, Attributes);
            }
            else
            {
                sR = String.Format("<input id=\"{0}\" value=\"{1}\" {2} {3} />", ControlID, sGiaTri, sDisabled, Attributes);
            }
            if (okCam == false)
            {
                //Trong trường hợp được phép nhập, kiểm tra có nhập số hay không
                Boolean okNhapSo = false;
                String LoaiNhap = "0";
                if (LoaiTextBox == 0)
                {
                    if ((TenTruong.StartsWith("iDM") == false &&
                          TenTruong.StartsWith("iID") == false &&
                          TenTruong.StartsWith("i"))
                         || TenTruong.StartsWith("r"))
                    {
                        okNhapSo = true;
                        LoaiNhap = "1";
                    }
                }
                else if (LoaiTextBox == 1)
                {
                    //Chỉ nhập số
                    okNhapSo = true;
                    LoaiNhap = "2";
                }

                if (okNhapSo)
                {
                    //Attributes += " onkeyup='ValidateNumberKeyUp(this);'";
                    //Attributes += " onkeypress='return ValidateNumberKeyPress(this, event);'";
                    //Attributes += " onblur='ValidateAndFormatNumber(this)'";
                    String sGiaTriDinhDang = CommonFunction.DinhDangSo(sGiaTri, SoSauDauPhay);
                    if (DuLieuCoLuu)
                    {
                        sR = String.Format("<input id=\"{0}_show\" name=\"{0}_show\" nhap-so=\"1\" loai-nhap=\"{4}\" so-sau-dau-phay=\"{5}\" value=\"{1}\" {2} {3} />", ControlID, HttpUtility.HtmlEncode(sGiaTriDinhDang), sDisabled, Attributes, LoaiNhap, SoSauDauPhay);
                        sR += String.Format("<input type=\"hidden\" id=\"{0}\" name=\"{0}\" value=\"{1}\"/>", ControlID, HttpUtility.HtmlEncode(sGiaTri));
                    }
                    else
                    {
                        sR = String.Format("<input id=\"{0}_show\" nhap-so=\"1\" loai-nhap=\"{4}\" so-sau-dau-phay=\"{5}\" value=\"{1}\" {2} {3} />", ControlID, HttpUtility.HtmlEncode(sGiaTriDinhDang), sDisabled, Attributes, LoaiNhap, SoSauDauPhay);
                        sR += String.Format("<input type=\"hidden\" id=\"{0}\" value=\"{1}\"/>", ControlID, HttpUtility.HtmlEncode(sGiaTri));
                    }
                }
            }
            return sR;
        }
        public static string TextBox(string ParentID, NameValueCollection data, string TenTruong = null, string DanhSachTruongCam = null, string Attributes = null, int LoaiTextBox = 0, Boolean DuLieuCoLuu = true, int SoSauDauPhay = -1)
        {
            object Value = LayGiaTriCuaTruong(ParentID, data, TenTruong);
            return TextBox(ParentID, Value, TenTruong, DanhSachTruongCam, Attributes, LoaiTextBox, DuLieuCoLuu, SoSauDauPhay);
        }

        public static string TextBox(string ParentID, object Value, string TenTruong, string Attributes)
        {
            string ControlID = String.Format("{0}", ParentID);
            if (String.IsNullOrEmpty(TenTruong) == false) ControlID = String.Format("{0}_{1}", ParentID, TenTruong);
            string sR = "";
            string sGiaTri = Convert.ToString(Value);
            string sDisabled = "";

            if (string.IsNullOrEmpty(ControlID))
            {
                ControlID = TenTruong;
            }
            if (TenTruong == null) TenTruong = "";

            sR = String.Format("<input id=\"{0}\" name=\"{0}\" value=\"{1}\" {2} {3} />", ControlID, HttpUtility.HtmlEncode(sGiaTri), sDisabled, Attributes);
            return sR;
        }
        #endregion

        #region "Hidden"
        public static string Hidden(string ParentID, object Value, string TenTruong, string DanhSachTruongCam, Boolean DuLieuCoLuu = true)
        {
            return TextBox(ParentID, Value, TenTruong, DanhSachTruongCam, "type=\"hidden\" khong-nhap=\"-1\"", 2, DuLieuCoLuu);
        }
        public static string Hidden(string ParentID, object Value)
        {
            return Hidden(ParentID, Value, null, null);
        }
        public static string Hidden(string ParentID, NameValueCollection data, string TenTruong, string DanhSachTruongCam)
        {
            return Hidden(ParentID, LayGiaTriCuaTruong(ParentID, data, TenTruong), TenTruong, DanhSachTruongCam);
        }
        public static string Hidden(string ParentID, NameValueCollection data, string TenTruong)
        {
            return Hidden(ParentID, LayGiaTriCuaTruong(ParentID, data, TenTruong), TenTruong, null);
        }
        #endregion

        #region "Autocomplete"
        public static string AutoComplete_Initialize(Object Attrs)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(Attrs);
            string TextControlID = (props["TextControlID"] == null) ? "" : Convert.ToString(props["TextControlID"].GetValue(Attrs));
            string ValueControlID = (props["ValueControlID"] == null) ? "" : Convert.ToString(props["ValueControlID"].GetValue(Attrs));
            string strUrl = (props["Url"] == null) ? "" : Convert.ToString(props["Url"].GetValue(Attrs));
            string Term = (props["Term"] == null) ? "q: request.term" : Convert.ToString(props["Term"].GetValue(Attrs));
            string funcComplete = (props["funcComplete"] == null) ? "" : Convert.ToString(props["funcComplete"].GetValue(Attrs));
            object options = (props["options"] == null) ? null : props["options"].GetValue(Attrs);
            return AutoComplete_Initialize(TextControlID, ValueControlID, strUrl, Term, funcComplete, options);
        }
        public static string AutoComplete_Initialize(string TextControlID, string ValueControlID, string url, string term, string funcComplete, object options)
        {
            if (String.IsNullOrEmpty(term))
            {
                term = "term: request.term";
            }
            String sb = "";
            sb += "<script language='javascript'>" + "";

            sb += "$(function() {" + "";

            sb += "   $('#" + TextControlID + "').autocomplete({" + "";
            sb += "source: function(request, response) {";
            sb += "     $.getJSON('" + url + "', {" + term + "}, response)";
            sb += "},";
            sb += "select: function(event, ui) {";
            sb += "   $('#" + TextControlID + "').val(ui.item.label);" + "";
            sb += "   $('#" + ValueControlID + "').val(ui.item.value);" + "";
            if (string.IsNullOrEmpty(funcComplete) == false)
            {
                sb += String.Format("{0}('{1}',ui);", funcComplete, ValueControlID) + "";
            }
            sb += "return false;";
            sb += "}";
            PropertyInfo[] properties = options.GetType().GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                sb += String.Format(" ,{0} : {1}",
                                        properties[i].Name,
                                        properties[i].GetValue(options, null));
            }
            sb += "   })" + "";

            sb += "});" + "";
            sb += "</script>" + "";
            return sb.ToString();
        }

        /// <summary>
        /// Hàm Autocomplete
        /// Điều kiện cần có custom-theme trong Content
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="Value"></param>
        /// <param name="Text"></param>
        /// <param name="TenTruong"></param>
        /// <param name="TenTruongText"></param>
        /// <param name="DanhSachTruongCam"></param>
        /// <param name="Attributes"></param>
        /// <returns></returns>
        public static string AutoComplete(Object Attrs)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(Attrs);
            string ParentID = (props["ParentID"] == null) ? "" : Convert.ToString(props["ParentID"].GetValue(Attrs));
            object Value = (props["Value"] == null) ? null : props["Value"].GetValue(Attrs);
            string TruongValue = (props["TruongValue"] == null) ? "" : Convert.ToString(props["TruongValue"].GetValue(Attrs));
            string Text = (props["Text"] == null) ? null : Convert.ToString(props["Text"].GetValue(Attrs));
            string TruongText = (props["TruongText"] == null) ? "" : Convert.ToString(props["TruongText"].GetValue(Attrs));
            string DanhSachTruongCam = (props["DanhSachTruongCam"] == null) ? "" : Convert.ToString(props["DanhSachTruongCam"].GetValue(Attrs));
            string Attributes = (props["Attributes"] == null) ? "" : Convert.ToString(props["Attributes"].GetValue(Attrs));
            string FunctionServerURL = (props["FunctionServerURL"] == null) ? "" : Convert.ToString(props["FunctionServerURL"].GetValue(Attrs));
            string FunctionServerTerm = (props["FunctionServerTerm"] == null) ? "" : Convert.ToString(props["FunctionServerTerm"].GetValue(Attrs));
            string funcComplete = (props["funcComplete"] == null) ? "" : Convert.ToString(props["funcComplete"].GetValue(Attrs));
            object options = (props["options"] == null) ? "" : props["options"].GetValue(Attrs);
            NameValueCollection data = (props["data"] == null) ? null : (NameValueCollection)(props["data"].GetValue(Attrs));
            if (data != null)
            {
                Value = LayGiaTriCuaTruong(ParentID, data, TruongValue);
                Text = data[TruongText];
            }
            return AutoComplete(ParentID, Value, Text, TruongValue, TruongText, FunctionServerURL, FunctionServerTerm, funcComplete, DanhSachTruongCam, Attributes, options);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="Value"></param>
        /// <param name="Text"></param>
        /// <param name="TruongValue"></param>
        /// <param name="TruongText"></param>
        /// <param name="FunctionServerUrl"></param>
        /// <param name="FunctionServerTerm"></param>
        /// <param name="funcComplete"></param>
        /// <param name="DanhSachTruongCam"></param>
        /// <param name="Attributes"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string AutoComplete(string ParentID, Object Value, String Text, string TruongValue, string TruongText, string FunctionServerUrl, string FunctionServerTerm, string funcComplete, string DanhSachTruongCam, string Attributes, object options)
        {
            String sR = "", sGiaTri = "";

            if (BaoMat.ChoPhepNhap(TruongValue, DanhSachTruongCam))
            {
                sGiaTri = String.Format("{0}", Text);
            }
            else
            {
                sGiaTri = "*";
                Attributes += " disabled=\"disabled\"";
            }
            Attributes += " autocomplete-oldcontrol=\"1\"";
            sR += TextBox(ParentID, sGiaTri, TruongText, null, Attributes);
            sR += Hidden(ParentID, Value, TruongValue, DanhSachTruongCam);
            sR += AutoComplete_Initialize(ParentID + "_" + TruongText, ParentID + "_" + TruongValue, FunctionServerUrl, FunctionServerTerm, funcComplete, options);
            return sR;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="Value"></param>
        /// <param name="Text"></param>
        /// <param name="TenTruong"></param>
        /// <param name="TenTruongText"></param>
        /// <param name="DanhSachTruongCam"></param>
        /// <param name="Attributes"></param>
        /// <returns></returns>
        public static string Autocomplete(string ParentID, Object Value, String Text, string TenTruong, string TenTruongText, string DanhSachTruongCam, string Attributes)
        {
            String sR = "", sGiaTri = "";

            if (BaoMat.ChoPhepNhap(TenTruong, DanhSachTruongCam))
            {
                sGiaTri = String.Format("{0}", Text);
            }
            else
            {
                sGiaTri = "*";
                Attributes += " disabled=\"disabled\"";
            }
            Attributes += " autocomplete-oldcontrol=\"1\"";
            sR += TextBox(ParentID, sGiaTri, TenTruongText, null, Attributes);
            sR += Hidden(ParentID, Value, TenTruong, DanhSachTruongCam);
            return sR;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="data"></param>
        /// <param name="TenTruong"></param>
        /// <param name="TenTruongText"></param>
        /// <param name="DanhSachTruongCam"></param>
        /// <param name="Attributes"></param>
        /// <returns></returns>
        public static string Autocomplete(string ParentID, NameValueCollection data, string TenTruong, string TenTruongText, string DanhSachTruongCam, string Attributes)
        {
            string gt1 = "", gt2 = "";
            if (data != null)
            {
                gt1 = LayGiaTriCuaTruong(ParentID, data, TenTruong);
                gt2 = data[TenTruongText];
                if (String.IsNullOrEmpty(gt2))
                {
                    gt2 = data[ParentID + "_" + TenTruongText];
                }
            }
            return Autocomplete(ParentID, gt1, gt2, TenTruong, TenTruongText, DanhSachTruongCam, Attributes);
        }
        #endregion

        #region "Autocomplete New"
        public static String AutoComplete_Initialize_New(string TextControlID, string ValueControlID, string url, string term1, string funcComplete, object options)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<script type=\"text/javascript\">");
            sb.Append("$(function () {");
            sb.Append("$('#" + TextControlID + ".typeahead').typeahead({");
            sb.Append("hint: true,");
            sb.Append("highlight: true,");
            sb.Append("minLength: 1");
            sb.Append("},");
            sb.Append("{");
            sb.Append("name: 'states',");
            sb.Append("displayKey: 'value',");
            sb.Append("source: function (request, response) {");
            sb.Append("$.ajax({");
            sb.Append("url: '" + url + "',");
            String sterm1 = "";
            if (!String.IsNullOrEmpty(term1))
            {
                sterm1 = String.Format(",'term1':'\"+{0}+\"'", term1);
            }
            sb.Append("data:\"{'term':'\"+ request+\"'" + sterm1 + "}\",");
            sb.Append("dataType: \"json\",");
            sb.Append("type: \"POST\",");
            sb.Append("contentType: \"application/json; charset=utf-8\",");
            sb.Append("success: function (data) {");
            //begin response
            sb.Append("response($.map(data, function (item) {");
            //begin return
            sb.Append("return {");
            sb.Append("value: item.label,");
            sb.Append("id: item.value,");
            sb.Append("item:item");
            sb.Append("}");
            //end return
            sb.Append("}))");
            //end response
            sb.Append("}");

            sb.Append("});");
            sb.Append("}");
            sb.Append("})");
            //bind(selected)
            sb.Append(".bind(\"typeahead:selected\", function (obj, datum, name) {");
            //Thêm hàm custom
            sb.Append("$('#" + TextControlID + "').val(datum.value);");
            sb.Append("$('#" + ValueControlID + "').val(datum.id);");
            if (string.IsNullOrEmpty(funcComplete) == false)
            {
                sb.Append(String.Format("{0}('{1}',datum.item);", funcComplete, ValueControlID));
            }
            //End thêm hàm
            sb.Append("console.log(datum.item.label);");
            sb.Append("});");
            //bind(selected)            
            sb.Append("});");
            sb.Append("</script>");




            return sb.ToString();
        }
        #endregion
        
        #region "AutocompleteTextBox"
        /// <summary>
        /// AutocompleteTextBox
        /// </summary>
        /// <param name="ParentID"></param>
        /// <param name="Value"></param>
        /// <param name="TenTruong"></param>
        /// <param name="DanhSachTruongCam"></param>
        /// <param name="Attributes"></param>
        /// <param name="ValueControlID">Control nhận giá trị ẩn</param>
        /// <param name="fnLayDSGiaTri">Hàm javascript ghép danh sách các giá trị trong URL</param>
        /// <param name="fnComplete">Hàm javascript được gọi sau khi đã kết thúc nhập giá trị</param>
        /// <param name="Url_LayDanhSach">URL của hàm lấy danh sách</param>
        /// <param name="Url_LayGiaTri">URL của hàm lấy giá trị</param>
        /// <param name="minLength">minLength</param>
        /// <param name="delay">delay</param>
        /// <returns></returns>
        public static string AutocompleteTextBox(Object Attrs)
        {
            PropertyDescriptorCollection props = TypeDescriptor.GetProperties(Attrs);
            string ParentID = (props["ParentID"] == null) ? "" : Convert.ToString(props["ParentID"].GetValue(Attrs));
            object Value = (props["Value"] == null) ? null : props["Value"].GetValue(Attrs);
            object Text = (props["Text"] == null) ? null : props["Text"].GetValue(Attrs);
            string TruongText = (props["TruongText"] == null) ? "" : Convert.ToString(props["TruongText"].GetValue(Attrs));
            string DanhSachTruongCam = (props["DanhSachTruongCam"] == null) ? "" : Convert.ToString(props["DanhSachTruongCam"].GetValue(Attrs));
            string Attributes = (props["Attributes"] == null) ? "" : Convert.ToString(props["Attributes"].GetValue(Attrs));

            String TruongValue = (props["TruongValue"] == null) ? "" : Convert.ToString(props["TruongValue"].GetValue(Attrs));
            String fnLayDSGiaTri = (props["fnLayDSGiaTri"] == null) ? "" : Convert.ToString(props["fnLayDSGiaTri"].GetValue(Attrs));
            String fnComplete = (props["fnComplete"] == null) ? "" : Convert.ToString(props["fnComplete"].GetValue(Attrs));
            String Url_LayDanhSach = (props["Url_LayDanhSach"] == null) ? "" : Convert.ToString(props["Url_LayDanhSach"].GetValue(Attrs));
            String Url_LayGiaTri = (props["Url_LayGiaTri"] == null) ? "" : Convert.ToString(props["Url_LayGiaTri"].GetValue(Attrs));
            int delay = (props["delay"] == null) ? 100 : Convert.ToInt16(props["delay"].GetValue(Attrs));
            int minLength = (props["minLength"] == null) ? 1 : Convert.ToInt16(props["minLength"].GetValue(Attrs));

            if (TruongValue == "")
            {
                TruongValue = TruongText;
            }
            if (TruongText == "")
            {
                TruongText = TruongValue;
            }

            NameValueCollection data = (props["data"] == null) ? null : (NameValueCollection)(props["data"].GetValue(Attrs));
            if (data != null)
            {
                Text = LayGiaTriCuaTruong(ParentID, data, TruongText);
                Value = LayGiaTriCuaTruong(ParentID, data, TruongValue);
            }
            Attributes += String.Format(" autocomplete-control='1'");
            Attributes += String.Format(" value_control_id='{0}_{1}'", ParentID, TruongValue);
            Attributes += String.Format(" url_danh_sach='{0}'", Url_LayDanhSach);
            Attributes += String.Format(" url_gia_tri='{0}'", Url_LayGiaTri);
            Attributes += String.Format(" fnlay_ds_gia_tri='{0}'", fnLayDSGiaTri);
            Attributes += String.Format(" fncomplete='{0}'", fnComplete);
            Attributes += String.Format(" delay='{0}'", delay);
            Attributes += String.Format(" minlength='{0}'", minLength);
            String vR = TextBox(ParentID, Text, TruongText, DanhSachTruongCam, Attributes, 2);
            vR += Hidden(ParentID, Value, TruongValue, DanhSachTruongCam);
            return vR;
        }
        #endregion

        #region "UploadFile"
        public static string UploadFile(string ParentID, string TenTruong, string Attributes)
        {
            string ControlID = String.Format("{0}_{1}", ParentID, TenTruong);

            String sR = String.Format("<input type=\"file\" id=\"{0}\" name=\"{0}\" {1} />", ControlID, Attributes);
            return sR;
        }
        #endregion

        #region "CheckBox"
        public static string CheckBox(string ParentID, string Value, string TenTruong, string DanhSachTruongCam, string Attributes)
        {
            string ControlID = String.Format("{0}_{1}", ParentID, TenTruong);
            Boolean isChecked = false;
            string sR = "";
            string sDisabled = "";

            if (string.IsNullOrEmpty(ControlID))
            {
                ControlID = TenTruong;
            }
            if (Value != null && (Value.ToUpper() == "TRUE" || Value.ToUpper() == "ON" || Value.ToUpper() == "1"))
            {
                isChecked = true;
            }
            string sChecked = "";
            if (BaoMat.ChoPhepNhap(TenTruong, DanhSachTruongCam))
            {
                sChecked = isChecked ? "checked=\"checked\"" : "";
            }
            else
            {
                sDisabled = "disabled=\"disabled\"";
            }
            string sExtAttributes = String.Format("  {0} {1}", Attributes, sChecked);

            sR = String.Format("<input id=\"{0}\" name=\"{0}\" type=\"checkbox\" {1} {2} {3} />", ControlID, sChecked, sDisabled, Attributes);
            sR += Hidden(ControlID + "_Kem", 1);
            return sR;
        }
        public static string CheckBox(string ParentID, string Value, string TenTruong, string DanhSachTruongCam)
        {
            return CheckBox(ParentID, Value, TenTruong, DanhSachTruongCam, null);
        }
        public static string CheckBox(string ParentID, string Value)
        {
            return CheckBox(ParentID, Value, null, null, null);
        }
        public static string CheckBox(string ParentID, NameValueCollection data, string TenTruong, string DanhSachTruongCam, string Attributes)
        {
            return CheckBox(ParentID, LayGiaTriCuaTruong(ParentID, data, TenTruong), TenTruong, DanhSachTruongCam, Attributes);
        }
        public static string CheckBox(string ParentID, NameValueCollection data, string TenTruong, string DanhSachTruongCam)
        {
            return CheckBox(ParentID, LayGiaTriCuaTruong(ParentID, data, TenTruong), TenTruong, DanhSachTruongCam, null);
        }
        public static string CheckBox(string ParentID, NameValueCollection data, string TenTruong)
        {
            return CheckBox(ParentID, LayGiaTriCuaTruong(ParentID, data, TenTruong), TenTruong, null, null);
        }
        public static string CheckBoxList(String ParentID, String Value, NameValueCollection data, String TenTruong, String DanhSachTruongCam = null, String Attributes = null)
        {
            Boolean isChecked = false;
            String sGiaTri = LayGiaTriCuaTruong(ParentID, data, TenTruong);
            String[] arrGiaTri;
            if (String.IsNullOrEmpty(sGiaTri) == false)
            {
                arrGiaTri = sGiaTri.Split(',');
                if (Value != null && arrGiaTri.Length > 0)
                {
                    for (int i = 0; i < arrGiaTri.Length; i++)
                    {
                        if (Value.Equals(arrGiaTri[i]))
                        {
                            isChecked = true;
                            break;
                        }
                    }

                }
            }
            string ControlID = String.Format("{0}_{1}", ParentID, TenTruong);

            string sR = "";
            string sDisabled = "";

            if (string.IsNullOrEmpty(ControlID))
            {
                ControlID = TenTruong;
            }

            string sChecked = "";
            if (BaoMat.ChoPhepNhap(TenTruong, DanhSachTruongCam))
            {
                sChecked = isChecked ? "checked=\"checked\"" : "";
            }
            else
            {
                sDisabled = "disabled=\"disabled\"";
            }
            string sExtAttributes = String.Format("  {0} {1}", Attributes, sChecked);

            sR = String.Format("<input id=\"{0}\" value=\"{4}\" name=\"{0}\" type=\"checkbox\" {1} {2} {3} />", ControlID, sChecked, sDisabled, Attributes, Value);
            return sR;
        }
        public static string CheckBoxList(String ParentID, String Value, String sGiaTri, String TenTruong, String DanhSachTruongCam = null, String Attributes = null)
        {
            Boolean isChecked = false;
            String[] arrGiaTri;
            if (String.IsNullOrEmpty(sGiaTri) == false)
            {
                arrGiaTri = sGiaTri.Split(',');
                if (Value != null && arrGiaTri.Length > 0)
                {
                    for (int i = 0; i < arrGiaTri.Length; i++)
                    {
                        if (Value.Equals(arrGiaTri[i]))
                        {
                            isChecked = true;
                            break;
                        }
                    }

                }
            }
            string ControlID = String.Format("{0}_{1}", ParentID, TenTruong);

            string sR = "";
            string sDisabled = "";

            if (string.IsNullOrEmpty(ControlID))
            {
                ControlID = TenTruong;
            }

            string sChecked = "";
            if (BaoMat.ChoPhepNhap(TenTruong, DanhSachTruongCam))
            {
                sChecked = isChecked ? "checked=\"checked\"" : "";
            }
            else
            {
                sDisabled = "disabled=\"disabled\"";
            }
            string sExtAttributes = String.Format("  {0} {1}", Attributes, sChecked);

            sR = String.Format("<input id=\"{0}\" value=\"{4}\" name=\"{0}\" type=\"checkbox\" {1} {2} {3} />", ControlID, sChecked, sDisabled, Attributes, Value);
            return sR;
        }
        #endregion

        #region "Option"
        public static string Option(string ParentID, string OptionValue, string Value, string TenTruong, string DanhSachTruongCam, string Attributes)
        {
            string ControlID = String.Format("{0}_{1}", ParentID, TenTruong);
            string sR = "";
            string sDisabled = "";

            if (string.IsNullOrEmpty(ControlID))
            {
                ControlID = TenTruong;
            }
            string sValue = "";
            if (BaoMat.ChoPhepNhap(TenTruong, DanhSachTruongCam))
            {
                sValue = string.Format("value=\"{0}\"", OptionValue);
                if (Value == OptionValue)
                {
                    sValue += " checked=\"checked\"";
                }
            }
            else
            {
                sDisabled = "disabled=\"disabled\"";
            }
            sR = String.Format("<input id=\"{0}\" name=\"{0}\" type=\"radio\" {1} {2} {3} />", ControlID, sValue, sDisabled, Attributes);
            return sR;
        }
        public static string Option(string ParentID, string OptionValue, string Value, string TenTruong, string DanhSachTruongCam)
        {
            return Option(ParentID, OptionValue, Value, TenTruong, DanhSachTruongCam, null);
        }
        public static string Option(string ParentID, string OptionValue, string Value)
        {
            return Option(ParentID, OptionValue, Value, null, null, null);
        }
        public static string Option(string ParentID, string OptionValue, NameValueCollection data, string TenTruong, string DanhSachTruongCam, string Attributes)
        {
            return Option(ParentID, OptionValue, LayGiaTriCuaTruong(ParentID, data, TenTruong), TenTruong, DanhSachTruongCam, Attributes);
        }
        public static string Option(string ParentID, string OptionValue, NameValueCollection data, string TenTruong, string DanhSachTruongCam)
        {
            return Option(ParentID, OptionValue, LayGiaTriCuaTruong(ParentID, data, TenTruong), TenTruong, DanhSachTruongCam, null);
        }
        public static string Option(string ParentID, string OptionValue, NameValueCollection data, string TenTruong)
        {
            return Option(ParentID, OptionValue, LayGiaTriCuaTruong(ParentID, data, TenTruong), TenTruong, null, null);
        }
        #endregion

        #region "ActionLink"
        public static string ActionLink(string URL, object Text, string ChucNang, string Attributes)
        {
            string sR = "";

            if (Globals.CoHoiTruocKhiXoa && ChucNang != null && ChucNang.ToUpper() == "DELETE")
            {
                Attributes += " onclick=\"javascript:return confirm('Bạn có chắc chắn muốn xóa không?');\" ";
            }
            sR = String.Format("<a href=\"{0}\" {1} >{2}</a>", URL, Attributes, Text);

            return sR;
        }
        public static string ActionLink(string URL, object Text, string ChucNang)
        {
            return ActionLink(URL, Text, ChucNang, null);
        }
        public static string ActionLink(string URL, object Text)
        {
            return ActionLink(URL, Text, null, null);
        }
        #endregion

        #region "PageLinks"
        public static string PageLinks(String Title, int currentPage, int totalPages, Func<int, string> URL)
        {
            if (totalPages <= 1) return "";
            StringBuilder result = new StringBuilder();
            int MinPage = (currentPage - Globals.PageRange) > 0 ? currentPage - Globals.PageRange : 1;
            int MaxPage = (currentPage + Globals.PageRange) > totalPages ? totalPages : currentPage + Globals.PageRange;

            result.AppendLine(Title);
            if (currentPage <= Globals.PageRange)
            {
                MaxPage = (2 * Globals.PageRange + 1 > totalPages) ? totalPages : 2 * Globals.PageRange + 1;
            }
            if (currentPage > totalPages - Globals.PageRange)
            {
                MinPage = (totalPages - 2 * Globals.PageRange < 1) ? 1 : totalPages - 2 * Globals.PageRange;
            }
            String strTG;
            if (MinPage > 1)
            {
                strTG = String.Format("<a href='{0}'> << </a>", URL(1));
                result.AppendLine(strTG);
            }

            if (MinPage < currentPage)
            {
                strTG = String.Format("<a href='{0}'> < </a>", URL(currentPage - 1));
                result.AppendLine(strTG);
            }
            for (int i = MinPage; i <= MaxPage; i++)
            {
                if (i == currentPage)
                {
                    result.AppendLine(String.Format("<span class=\"current\">{0}</span>", i));
                }
                else
                {
                    strTG = String.Format("<a href='{0}'> {1} </a>", URL(i), i);
                    result.AppendLine(strTG);
                }
            }
            if (MaxPage > currentPage)
            {
                strTG = String.Format("<a href='{0}'> > </a>", URL(currentPage + 1));
                result.AppendLine(strTG);
            }
            if (MaxPage < totalPages)
            {
                strTG = String.Format("<a href='{0}'> >> </a>", URL(totalPages));
                result.AppendLine(strTG);
            }
            return result.ToString();
        }
        public static string PageLinksAjax(String Title, int currentPage, int totalPages, Func<int, string, string> tagA)
        {
            if (totalPages <= 1) return "";
            StringBuilder result = new StringBuilder();
            int MinPage = (currentPage - Globals.PageRange) > 0 ? currentPage - Globals.PageRange : 1;
            int MaxPage = (currentPage + Globals.PageRange) > totalPages ? totalPages : currentPage + Globals.PageRange;

            result.AppendLine(Title);
            if (currentPage <= Globals.PageRange)
            {
                MaxPage = (2 * Globals.PageRange + 1 > totalPages) ? totalPages : 2 * Globals.PageRange + 1;
            }
            if (currentPage > totalPages - Globals.PageRange)
            {
                MinPage = (totalPages - 2 * Globals.PageRange < 1) ? 1 : totalPages - 2 * Globals.PageRange;
            }
            if (MinPage > 1)
            {
                result.AppendLine(tagA(1, "<< "));
            }

            if (MinPage < currentPage)
            {
                result.AppendLine(tagA(currentPage - 1, "< "));
            }
            for (int i = MinPage; i <= MaxPage; i++)
            {
                if (i == currentPage)
                {
                    result.AppendLine(String.Format("<span class=\"current\">{0} </span>", i));
                }
                else
                {
                    result.AppendLine(tagA(i, i.ToString() + " "));
                }
            }
            if (MaxPage > currentPage)
            {
                result.AppendLine(tagA(currentPage + 1, "> "));
            }
            if (MaxPage < totalPages)
            {
                result.AppendLine(tagA(totalPages, ">>"));
            }
            return result.ToString();
        }
        #endregion

        #region "TextArea"
        public static string TextArea(string ParentID, string Value, string TenTruong, string DanhSachTruongCam, string Attributes)
        {
            string ControlID = String.Format("{0}_{1}", ParentID, TenTruong);
            string sR = "";
            string sGiaTri = "";
            string sDisabled = "";

            if (string.IsNullOrEmpty(ControlID))
            {
                ControlID = TenTruong;
            }
            if (BaoMat.ChoPhepNhap(TenTruong, DanhSachTruongCam))
            {
                sGiaTri = Convert.ToString(Value);
            }
            else
            {
                sGiaTri = "*";
                sDisabled = "disabled=\"disabled\"";
            }
            sR = String.Format("<textarea id=\"{0}\" name=\"{0}\" {2} {3}>{1}</textarea>", ControlID, sGiaTri, sDisabled, Attributes);
            return sR;
        }
        public static string TextArea(string ParentID, string Value, string TenTruong, string DanhSachTruongCam)
        {
            return TextArea(ParentID, Value, TenTruong, DanhSachTruongCam, null);
        }
        public static string TextArea(string ParentID, string Value)
        {
            return TextArea(ParentID, Value, null, null, null);
        }
        public static string TextArea(string ParentID, NameValueCollection data, string TenTruong, string DanhSachTruongCam, string Attributes)
        {
            return TextArea(ParentID, LayGiaTriCuaTruong(ParentID, data, TenTruong), TenTruong, DanhSachTruongCam, Attributes);
        }
        public static string TextArea(string ParentID, NameValueCollection data, string TenTruong, string DanhSachTruongCam)
        {
            return TextArea(ParentID, LayGiaTriCuaTruong(ParentID, data, TenTruong), TenTruong, DanhSachTruongCam, null);
        }
        public static string TextArea(string ParentID, NameValueCollection data, string TenTruong)
        {
            return TextArea(ParentID, LayGiaTriCuaTruong(ParentID, data, TenTruong), null, null, null);
        }
        #endregion

        #region "DatePicker"
        public static string DatePicker(string ParentID, NameValueCollection data, string TenTruong, string DanhSachTruongCam, string Attributes)
        {
            string sNewName;
            sNewName = String.Format("{0}{1}", NgonNgu.MaDate, TenTruong);
            if (data[sNewName] == null)
            {
                sNewName = TenTruong;
            }


            return DatePicker(ParentID, LayGiaTriCuaTruong(ParentID, data, sNewName), TenTruong, DanhSachTruongCam, Attributes);
        }
        public static string DatePicker(string ParentID, NameValueCollection data, string TenTruong, string DanhSachTruongCam)
        {
            string sNewName = String.Format("{0}{1}", NgonNgu.MaDate, TenTruong);
            return DatePicker(ParentID, LayGiaTriCuaTruong(ParentID, data, sNewName), TenTruong, DanhSachTruongCam, null);
        }
        public static string DatePicker(string ParentID, NameValueCollection data, string TenTruong)
        {
            string sNewName = String.Format("{0}{1}", NgonNgu.MaDate, TenTruong);
            return DatePicker(ParentID, LayGiaTriCuaTruong(ParentID, data, sNewName), TenTruong, null, null);
        }
        public static string DatePicker(string ParentID, object Value, string TenTruong, string DanhSachTruongCam, string Attributes)
        {
            string ControlID = String.Format("{0}_{1}{2}", ParentID, NgonNgu.MaDate, TenTruong);
            string vR = "";
            string sGiaTri = "";
            string sDisabled = "";
            string sNewName = String.Format("{0}{1}", NgonNgu.MaDate, TenTruong);
            //string urlImage = Globals.URL_Path + "Content/Themes/images/icoLayNgay.gif";

            vR += "<script type='text/javascript'>";
            vR += "$(function() {";
            vR += "$.datepicker.setDefaults($.datepicker.regional['" + NgonNgu.MaDate + "']);";
            //vR += "$('#" + ControlID + "').datepicker({ changeMonth: true, changeYear: true, showOn: 'button', buttonImage: '" + urlImage + "',buttonImageOnly: true});";
            vR += "$('#" + ControlID + "').datepicker({ changeMonth: true, changeYear: true});";
            //vR += "$('#" + ControlID + "').datepicker({ changeMonth: true, changeYear: true, showOtherMonths: true, selectOtherMonths: true});";
            vR += "});";
            vR += "</script>";
            sGiaTri = Convert.ToString(Value);
            vR += String.Format("<input id=\"{0}\" type=\"text\" name=\"{0}\" value=\"{1}\" {2} {3} />", ControlID, sGiaTri, sDisabled, Attributes);
            vR += "<i class='glyphicon glyphicon glyphicon-calendar form-control-feedback blue' style='right: 13px;'></i>";

            //vR += "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" width=\"100%\" style=\"border:none;\">";
            //vR += "<tr>";
            //vR += "<td style=\"border:none;\">";
            //vR += String.Format("<input id=\"{0}\" type=\"text\" name=\"{0}\" value=\"{1}\" {2} {3} />", ControlID, sGiaTri, sDisabled, Attributes);
            //vR += "</td>";
            //vR += "<td width=\"10px\" style=\"border:none;\">&nbsp;</td>";
            //vR += "<td width=\"20px\" style=\"border:none;\">";
            //vR += String.Format("<a href=\"#\" class=\"icon_chonngay\" onclick=\"javascript:$('#{0}').datepicker('show');return false;\">&nbsp;</a>", ControlID);
            ////vR += "<input type=\"button\" class=\"bieutuong\">dmalsk</a>";
            //vR += "</td>";
            //vR += "</tr>";
            //vR += "</table>";


            return vR;
        }
        public static string DatePicker(string ParentID, object Value, string TenTruong, string DanhSachTruongCam)
        {
            return DatePicker(ParentID, Value, TenTruong, DanhSachTruongCam, null);
        }
        public static string DatePicker(string ParentID, object Value, string TenTruong)
        {
            return DatePicker(ParentID, Value, TenTruong, null, null);
        }
        #endregion

        public static string DropDownList_Days(string p1, SelectOptionList slDay, string sDay, string p2, string p3, string p4)
        {
            throw new NotImplementedException();
        }

        public static string CheckError(ModelStateDictionary ModelState)
        {
            String vR = "";
            if (ModelState.Count > 0)
            {
                int i;
                for (i = 0; i < ModelState.Count; i++)
                {
                    if (ModelState.Values.ElementAt(i).Errors.Count > 0)
                    {
                        String tgControlID = ModelState.Keys.ElementAt(i);
                        tgControlID = tgControlID.Replace("_err", "");

                        vR += "<script type='text/javascript'>";
                        vR += "try{";
                        vR += "if( document.getElementById('" + tgControlID + "_show') ) { ";
                        vR += "document.getElementById('" + tgControlID + "_show').focus();";
                        vR += "document.getElementById('" + tgControlID + "_show').select();";
                        vR += "}else{";
                        vR += String.Format("document.getElementById('{0}').focus();", tgControlID);
                        vR += String.Format("document.getElementById('{0}').select();", tgControlID);
                        vR += "}";
                        vR += "}catch(e){}";
                        vR += "</script>";
                        break;
                    }
                }
            }
            return vR;
        }
    }
}