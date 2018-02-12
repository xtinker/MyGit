using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using Mysoft.Map6.Core.Common;
using Mysoft.Map6.Core.EntityBase;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace View45
{
    public partial class EnumExportForm : Form
    {
        private readonly string _seachKeyTemp = "*{0}.{1}.Model.dll";
        private string _fileKeyWord = string.Empty;
        private string _sitePath = string.Empty;
        private string _app = "Slxt";

        private static string _rootPath;
        private FileInfo[] modeldll;

        private static IWorkbook _workBook;
        private static ISheet _sheet1;
        private static int _row;

        private List<EnumObject> enumObjects;

        public EnumExportForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 枚举解析事件，解析站点目录下的枚举使用情况
        /// </summary>
        private void btnAnalyze_Click(object sender, EventArgs e)
        {
            if (!checkIsSitePath(txtSitePath.Text.Trim()))
            {
                MessageBox.Show("请选择正确的站点目录！");
                txtSitePath.Text = "";
                return;
            }

            btnAnalyze.Enabled = false;
            btnExport.Enabled = false;

            txtContent.Text = "";
            if (!panel1.Visible)
            {
                panel1.Visible = true;
            }

            refreshPathInfo();
            modeldll = getModelFiles(_fileKeyWord);
            getEnumInfo();

            btnAnalyze.Enabled = true;
            btnExport.Enabled = true;
        }

        /// <summary>
        /// 导出Excel
        /// </summary>
        private void btnExport_Click(object sender, EventArgs e)
        {
            if (enumObjects.Count == 0)
            {
                MessageBox.Show("没有要导出的内容");
                return;
            }

            var saveFileDialog = new SaveFileDialog
            {
                FileName = "枚举对照关系.xlsx",
                Filter = "Excel 工作簿(*.xlsx)|*.xlsx|Excel 97-2003 工作簿(*.xls)|*.xls|All File(*.*)|*.*"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                bool isHigherVersion = filePath.ToLower().EndsWith("xlsx");
                saveExcelFile(filePath, isHigherVersion);
            }
        }

        /// <summary>
        /// 浏览按钮事件，弹窗选择站点目录
        /// </summary>
        private void btnScan_Click(object sender, EventArgs e)
        {
            var openFile = new FolderBrowserDialog();
            if (openFile.ShowDialog() == DialogResult.OK)
            {
                txtSitePath.Text = openFile.SelectedPath;

                if (!checkIsSitePath(openFile.SelectedPath))
                {
                    MessageBox.Show("请选择正确的站点目录！");
                    txtSitePath.Text = "";
                }
            }
        }

        /// <summary>
        /// 选择“其它”，支持模块的模糊查询
        /// </summary>
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            txtFileKeyWord.Visible = radioButton2.Checked;
            lblTip.Visible = radioButton2.Checked;
            if (!radioButton2.Checked)
            {
                txtFileKeyWord.Text = string.Empty;
                _fileKeyWord = string.Empty;
            }
        }

        private void rbtSlxt_CheckedChanged(object sender, EventArgs e)
        {
            _app = "Slxt";
        }

        private void rbtAll_CheckedChanged(object sender, EventArgs e)
        {
            _app = "";
        }

        /// <summary>
        /// 枚举信息文本框按钮事件，支持全选及"Ctrl+F"查询
        /// </summary>
        private void txtContent_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.Control && e.KeyCode == Keys.A)
            {
                ((TextBox)sender).SelectAll();
            }
            else if (e.Modifiers == Keys.Control && e.KeyCode == Keys.F)
            {
                SearchForm searchForm = SearchForm.GetInstance(this);
                searchForm.FormBorderStyle = FormBorderStyle.FixedDialog;
                searchForm.TopLevel = true;
                searchForm.ControlBox = true;
                searchForm.Dock = DockStyle.Fill;
                searchForm.StartPosition = FormStartPosition.Manual;
                var left = Width / 2 + Left - searchForm.Width / 2;
                var right = Height / 2 + Top - searchForm.Height / 2;
                searchForm.Location = new Point(left, right);
                searchForm.Show();
            }
        }

        #region 解析枚举

        /// <summary>
        /// 校验是否是ERP站点目录，目录下含bin目录就算
        /// </summary>
        /// <param name="path">站点目录</param>
        /// <returns></returns>
        private bool checkIsSitePath(string path)
        {
            if (string.IsNullOrEmpty(path))
                return false;

            DirectoryInfo drectory = new DirectoryInfo(path);
            var dr = drectory.GetDirectories("bin");
            return dr.Length > 0;
        }

        /// <summary>
        /// 获取查询范围内的枚举信息
        /// </summary>
        private void getEnumInfo()
        {
            enumObjects = new List<EnumObject>();
            int max = modeldll.Length;
            progressBar.Maximum = max;
            if (max == 0)
            {
                lblPercent.Text = "0/0";
            }

            for (int i = 0; i < max; i++)
            {
                var file = modeldll[i];
                lblTips.Text = "正在处理：" + file.Name;
                var assembly = Assembly.LoadFrom(file.FullName);
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.Name.EndsWith("Enum"))
                    {
                        EnumObject enumObject = getEnumObject(type);
                        enumObjects.Add(enumObject);

                        txtContent.Text = txtContent.Text + "\r\n" + getEnumObjectString(enumObject);
                        //让文本框获取焦点 
                        txtContent.Focus();
                        //设置光标的位置到文本尾 
                        txtContent.Select(txtContent.Text.Length, 0);
                        //滚动到控件光标处 
                        txtContent.ScrollToCaret();
                        Application.DoEvents();
                    }

                    //设置进度
                    progressBar.Value = (i + 1);
                    lblPercent.Text = (i + 1) + "/" + max;
                }
            }
            lblTips.Text = "处理完成，共计 " + enumObjects.Count + " 个枚举";
            Application.DoEvents();
        }

        /// <summary>
        /// 获取实体文件
        /// </summary>
        /// <param name="fileKeyWord">查询关键字，示例：实体文件“Mysoft.Slxt.FinanceMng.dll”中针对“FinanceMng”部分的模糊查询</param>
        /// <returns></returns>
        private FileInfo[] getModelFiles(string fileKeyWord)
        {
            //遍历目录，获取Model.dll
            DirectoryInfo drectory = new DirectoryInfo(_rootPath);

            var key = "*";
            if (!string.IsNullOrEmpty(fileKeyWord))
            {
                key = $"*{fileKeyWord}*";
            }

            var searchKey = string.Format(_seachKeyTemp, _app, key);
            return drectory.GetFiles(searchKey);
        }

        /// <summary>
        /// 初始化目录路径
        /// </summary>
        private void refreshPathInfo()
        {
            _sitePath = txtSitePath.Text.Trim();
            _rootPath = Path.Combine(_sitePath, "bin");

            if (radioButton2.Checked)
            {
                _fileKeyWord = txtFileKeyWord.Text.Trim();
            }
        }

        /// <summary>
        /// 获取枚举描述信息对象
        /// </summary>
        /// <param name="type">枚举项</param>
        /// <returns></returns>
        private EnumObject getEnumObject(Type type)
        {
            EnumObject enumObject = new EnumObject();

            var entitys = GetEntitys(type);
            enumObject.EnumObjectName = type.Name;
            enumObject.EntityAndField = entitys;

            //获取枚举类
            Array enumValue = type.GetFields(BindingFlags.Static | BindingFlags.Public);
            List<EnumInfo> enumInfos = new List<EnumInfo>();
            for (var i = 0; i < enumValue.Length; i++)
            {
                if (((FieldInfo[])enumValue)[i].FieldType != typeof(int))
                    continue;

                EnumInfo enumInfo = new EnumInfo();
                enumInfo.EnumName = ((FieldInfo[])enumValue)[i].Name;
                enumInfo.EnumValue = Convert.ToInt32(((FieldInfo[])enumValue)[i].GetValue(null));
                MultiLanguageAttribute multi = ((FieldInfo[])enumValue)[i].GetCustomAttribute<MultiLanguageAttribute>();
                enumInfo.Description = multi == null ? "" : Resource(multi.ResourceKey);
                enumInfos.Add(enumInfo);
            }
            enumObject.EnumInfos = enumInfos;

            return enumObject;
        }

        /// <summary>
        /// 枚举描述对象转字符串
        /// </summary>
        /// <param name="enumObject">枚举描述对象</param>
        /// <returns></returns>
        private string getEnumObjectString(EnumObject enumObject)
        {
            StringBuilder sb = new StringBuilder();
            string enumMainInfo = $"枚举名称：{enumObject.EnumObjectName}\t实体和字段：{string.Join(", ", enumObject.EntityAndField)}";
            sb.AppendLine(enumMainInfo);
            sb.AppendLine("EnumName                      \tEnumValue\tDescription");
            string enumInfoTemp = "{0}\t{1}\t{2}";
            // 创建新增行
            for (var i = 0; i < enumObject.EnumInfos.Count; i++)
            {
                var enumInfo = enumObject.EnumInfos[i];

                sb.AppendLine(string.Format(enumInfoTemp, enumInfo.EnumName.PadRight(30), enumInfo.EnumValue.ToString().PadRight(9), enumInfo.Description));
            }

            return sb.ToString();
        }

        //获取资源文件
        static string Resource(string key)
        {
            var assembly = Assembly.LoadFrom(Path.Combine(_rootPath, "Mysoft.Slxt.Resource.dll"));
            var resourceNames = assembly.GetManifestResourceNames();
            foreach (var res in resourceNames)
            {
                if (res.Contains("LanguageResource") == false)
                    continue;
                Stream s = assembly.GetManifestResourceStream(res);
                XmlSerializer xs = new XmlSerializer(typeof(LanguageResource));
                if (s != null)
                {
                    LanguageResource lr = (LanguageResource)xs.Deserialize(s);
                    var resource = lr.List.Find(t => t.Key == key);
                    if (resource != null)
                    {
                        return resource.Text;
                    }
                }
            }
            return "";
        }

        /// <summary>
        /// 获取实体及属性信息
        /// </summary>
        /// <param name="enumType">枚举项</param>
        /// <returns></returns>
        private List<string> GetEntitys(Type enumType)
        {
            List<string> entitys = new List<string>();
            foreach (var file in modeldll)
            {
                var assembly = Assembly.LoadFrom(file.FullName);
                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.BaseType == typeof(Entity))
                    {
                        //获取实体所有属性
                        List<PropertyInfo> props = type.GetProperties().ToList();
                        PropertyInfo info = props.Find(t => t.Name == enumType.Name || isContainEnum(props, t, enumType));
                        if (info != null)
                        {
                            var entityName = type.GetCustomAttribute<EntityNameAttribute>().Name;
                            var fieldName = info.Name;
                            entitys.Add(entityName + "." + fieldName);
                        }
                    }
                }
            }
            return entitys;
        }

        /// <summary>
        /// 实体属性是否包含枚举项
        /// </summary>
        /// <param name="props">实体属性集合</param>
        /// <param name="pi">要判断的属性</param>
        /// <param name="enumType">枚举</param>
        /// <returns></returns>
        private bool isContainEnum(List<PropertyInfo> props, PropertyInfo pi, Type enumType)
        {
            return props.Exists(t => t == pi && t.GetCustomAttribute<EnumColumnAttribute>() != null && t.GetCustomAttribute<EnumColumnAttribute>().TargetType == enumType);
        }

        #endregion

        #region 导出Excel

        private void saveExcelFile(string path, bool isHigherVersion)
        {
            if (isHigherVersion)
            {
                _workBook = new XSSFWorkbook();
            }
            else
            {
                _workBook = new HSSFWorkbook();
            }

            //行号标识重置
            _row = 0;

            _sheet1 = _workBook.CreateSheet("Sheet1");
            _sheet1.SetColumnWidth(0, (28 * 256));
            _sheet1.SetColumnWidth(1, (26 * 256));
            _sheet1.SetColumnWidth(2, (26 * 256));
            _sheet1.SetColumnWidth(3, (10 * 256));
            _sheet1.SetColumnWidth(4, (56 * 256));

            //设置标题
            exportTitle();

            foreach (var enumObject in enumObjects)
            {
                export(enumObject);
            }

            //如果存在删除
            if (File.Exists(path))
            {
                try
                {
                    File.Delete(path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
            }

            using (var file = new FileStream(path, FileMode.Create))
            {
                _workBook.Write(file);
                MessageBox.Show("保存成功");
            }
        }

        /// <summary>
        /// 设置标题
        /// </summary>
        private void exportTitle()
        {
            IRow header = _sheet1.CreateRow(0);
            //新建单元格
            ICell cell = header.CreateCell(0);
            // 单元格赋值
            cell.SetCellValue("枚举名称");
            //新建单元格
            ICell cell1 = header.CreateCell(1);

            cell1.SetCellValue("描述");
            //枚举值
            ICell cell2 = header.CreateCell(2);
            cell2.SetCellValue("字段名称");
            //文本值
            ICell cell3 = header.CreateCell(3);
            cell3.SetCellValue("值");
            //使用实体
            ICell cell4 = header.CreateCell(4);
            cell4.SetCellValue("实体及属性");

            _row += 1;
        }

        //设置行
        private void export(EnumObject enumObject)
        {
            //获取枚举类
            List<EnumInfo> enumInfos = enumObject.EnumInfos;
            // 创建新增行
            for (var i = _row; i < enumInfos.Count + _row; i++)
            {
                EnumInfo enumInfo = enumInfos[i - _row];
                IRow row1 = _sheet1.CreateRow(i);
                //新建单元格
                ICell cell = row1.CreateCell(0);
                // 单元格赋值
                cell.SetCellValue(enumObject.EnumObjectName);
                //新建单元格
                ICell cell1 = row1.CreateCell(1);

                cell1.SetCellValue(enumInfo.Description);
                //枚举值
                ICell cell2 = row1.CreateCell(2);
                cell2.SetCellValue(enumInfo.EnumName);
                //文本值
                ICell cell3 = row1.CreateCell(3);
                cell3.SetCellValue(enumInfo.EnumValue);
                //使用实体
                ICell cell4 = row1.CreateCell(4);
                cell4.SetCellValue(string.Join(", ", enumObject.EntityAndField));
            }

            if (enumInfos.Count > 0)
            {
                // 合并单元格
                _sheet1.AddMergedRegion(new CellRangeAddress(_row, _row + enumInfos.Count - 1, 0, 0));
                _sheet1.AddMergedRegion(new CellRangeAddress(_row, _row + enumInfos.Count - 1, 4, 4));
            }

            _row += enumInfos.Count;
        }

        #endregion
    }



}
