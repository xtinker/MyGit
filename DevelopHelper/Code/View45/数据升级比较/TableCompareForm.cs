using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using DbHelper;
using Model;
using Model.Enum;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;

namespace View45
{
    public partial class TableCompareForm : Form
    {
        private bool isLogin;
        private SqlHelper sqlHelper;
        private readonly string[] ignoreDbNames = { "master", "model", "msdb", "tempdb" };
        private List<DataDictCompare> dicSourceCompareList = new List<DataDictCompare>();
        private TableStatusEnum selectedTableStatus = TableStatusEnum.None;

        public TableCompareForm()
        {
            InitializeComponent();

            //初始化默认值
            txtSourceName.Text = "local";
            txtUser.Text = "sa";
            txtPassword.Text = "95938";
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (checkLoginInfo())
            {
                var connectionString = $"Data Source={txtSourceName.Text.Trim()};Initial Catalog={"master"};User Id={txtUser.Text.Trim()};Password={txtPassword.Text.Trim()};";
                sqlHelper = new SqlServerHelper(connectionString);
                isLogin = sqlHelper.ConnectionTest(connectionString);
                if (isLogin)
                {
                    //初始化数据库选择项
                    initDbName();
                    lblMessage.Text = "登录成功";
                }
                else
                {
                    lblMessage.Text = "";
                    MessageBox.Show("连接失败");
                }
            }
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            if (checkCompareInfo())
            {
                setBtnEnable(false);

                string sourceDbName = cbSourceDbName.Text;
                string targetDbName = cbTargetDbName.Text;

                //获取结果集
                dicSourceCompareList = getResult(sourceDbName, targetDbName);
                //设置DataGridView控件的展示
                setViewData(dicSourceCompareList);

                setBtnEnable(true);
            }
        }

        private void rbtnAll_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnAll.Checked && checkLoginInfo() && checkCompareInfo())
            {
                selectedTableStatus = TableStatusEnum.None;
                queryByTableStatus(selectedTableStatus);
            }
        }

        private void rbtnCreated_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnCreated.Checked && checkLoginInfo() && checkCompareInfo())
            {
                selectedTableStatus = TableStatusEnum.Created;
                queryByTableStatus(selectedTableStatus);
            }
        }

        private void rbtnUpdated_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnUpdated.Checked && checkLoginInfo() && checkCompareInfo())
            {
                selectedTableStatus = TableStatusEnum.Update;
                queryByTableStatus(selectedTableStatus);
            }
        }

        private void rbtnDeleted_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtnDeleted.Checked && checkLoginInfo() && checkCompareInfo())
            {
                selectedTableStatus = TableStatusEnum.Deleted;
                queryByTableStatus(selectedTableStatus);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dicSourceCompareList.Count == 0)
            {
                MessageBox.Show("没有要导出的内容");
                return;
            }

            var saveFileDialog = new SaveFileDialog
            {
                FileName = "数据结构升级清单.xlsx",
                Filter = "Excel 工作簿(*.xlsx)|*.xlsx|Excel 97-2003 工作簿(*.xls)|*.xls|All File(*.*)|*.*"
            };

            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                string filePath = saveFileDialog.FileName;
                bool isHigherVersion = filePath.ToLower().EndsWith("xlsx");
                saveExcelFile(filePath, isHigherVersion);
            }
        }

        #region 私有方法

        /// <summary>
        /// 设置按钮的有效性
        /// </summary>
        /// <param name="enable"></param>
        private void setBtnEnable(bool enable)
        {
            btnLogin.Enabled = enable;
            btnCompare.Enabled = enable;
            btnExport.Enabled = enable;
        }

        /// <summary>
        /// 检测登录信息
        /// </summary>
        /// <returns></returns>
        private bool checkLoginInfo()
        {
            bool isValid = true;
            if (string.IsNullOrWhiteSpace(txtSourceName.Text.Trim()))
            {
                isValid = false;
                MessageBox.Show("服务器名称不能为空！");
            }
            else if (string.IsNullOrWhiteSpace(txtUser.Text.Trim()))
            {
                isValid = false;
                MessageBox.Show("用户名不能为空！");
            }
            else if (string.IsNullOrWhiteSpace(txtPassword.Text.Trim()))
            {
                isValid = false;
                MessageBox.Show("密码不能为空！");
            }

            return isValid;
        }

        /// <summary>
        /// 检测比对信息
        /// </summary>
        /// <returns></returns>
        private bool checkCompareInfo()
        {
            bool isValid = true;
            if (!isLogin)
            {
                isValid = false;
                MessageBox.Show("请先登录！");
            }
            else if (string.IsNullOrWhiteSpace(cbSourceDbName.Text.Trim()))
            {
                isValid = false;
                MessageBox.Show("请选择源数据库！");
            }
            else if (string.IsNullOrWhiteSpace(cbTargetDbName.Text.Trim()))
            {
                isValid = false;
                MessageBox.Show("请选择目标数据库！");
            }
            else if (cbSourceDbName.Text == cbTargetDbName.Text)
            {
                isValid = false;
                MessageBox.Show("请选择不同的数据库！");
            }

            return isValid;
        }

        /// <summary>
        /// 初始化数据库选择项
        /// </summary>
        private void initDbName()
        {
            //清除原数据库名称选项
            cbSourceDbName.Text = "";
            cbSourceDbName.Items.Clear();
            cbTargetDbName.Text = "";
            cbTargetDbName.Items.Clear();

            const string sqlDb = "SELECT name FROM sysdatabases ORDER BY name";
            var dt = sqlHelper.ExecuteDataTable(sqlDb);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var dbName = row["name"].ToString();
                    if (!ignoreDbNames.Contains(dbName))
                    {
                        cbSourceDbName.Items.Add(row["name"]);
                        cbTargetDbName.Items.Add(row["name"]);
                    }
                }
            }
        }

        /// <summary>
        /// 是否是高版本
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <returns>是否高版本</returns>
        private bool isHigher(string dbName)
        {
            DataTable dt = sqlHelper.ExecuteDataTable(string.Format(sqlVersion, dbName));

            return dt.Rows.Count > 0;
        }

        /// <summary>
        /// 设置DataGridView控件的展示
        /// </summary>
        /// <param name="dicSourceCompares">需要展示的结果集</param>
        private void setViewData(List<DataDictCompare> dicSourceCompares)
        {
            if (dicSourceCompares != null && dicSourceCompares.Count > 0)
            {
                dataGridView.DataSource = dicSourceCompares;
                dataGridView.Refresh();
            }
            else
            {
                DataTable emptyDt = new DataTable();
                emptyDt.Columns.Add("Message");
                DataRow emptyRow = emptyDt.NewRow();
                emptyRow["Message"] = "无数据";
                emptyDt.Rows.Add(emptyRow);
                dataGridView.DataSource = emptyDt;
                dataGridView.Refresh();
            }
        }

        /// <summary>
        /// 根据更新状态检索
        /// </summary>
        /// <param name="selectedStatus">选中的更新状态</param>
        private void queryByTableStatus(TableStatusEnum selectedStatus)
        {
            List<DataDictCompare> dicSourceCompareListTemp;
            if (selectedStatus == TableStatusEnum.None)
            {
                dicSourceCompareListTemp = dicSourceCompareList;
            }
            else
            {
                dicSourceCompareListTemp = dicSourceCompareList.Where(t => t.TableStatus == selectedStatus).ToList();
            }

            //排序
            if (dicSourceCompareListTemp.Count > 0)
            {
                dicSourceCompareListTemp = dicSourceCompareListTemp.Where(t => t.TableStatus != TableStatusEnum.None).OrderBy(t => t.table_name).ToList();
                for (int i = 0; i < dicSourceCompareListTemp.Count; i++)
                {
                    dicSourceCompareListTemp[i].RowNum = i + 1;
                }
            }

            setViewData(dicSourceCompareListTemp);
        }

        /// <summary>
        /// 获取结果集
        /// </summary>
        /// <param name="sourceDbName">源数据库名称</param>
        /// <param name="targetDbName">目标数据库名称</param>
        /// <returns></returns>
        private List<DataDictCompare> getResult(string sourceDbName, string targetDbName)
        {
            bool isHigherForSource = isHigher(sourceDbName);
            var sqlSource = isHigherForSource ? sqlHigher : sql;
            List<FieldCompare> dicSourceList = sqlHelper.ExecuteObjectList<FieldCompare>(string.Format(sqlSource, sourceDbName));

            bool isHigherForTarget = isHigher(targetDbName);
            var sqlTarget = isHigherForTarget ? sqlHigher : sql;
            List<FieldCompare> dicTargetList = sqlHelper.ExecuteObjectList<FieldCompare>(string.Format(sqlTarget, targetDbName));

            var sourceTableNameList = dicSourceList.Select(t => new
            {
                t.table_name,
                t.table_name_c
            }).Distinct().ToList();
            var targetTableNameList = dicTargetList.Select(t => new
            {
                t.table_name,
                t.table_name_c
            }).Distinct().ToList();

            List<DataDictCompare> dataDictCompares = new List<DataDictCompare>();

            foreach (var tableInfo in sourceTableNameList)
            {
                string tableName = tableInfo.table_name;
                DataDictCompare ddc = new DataDictCompare
                {
                    table_name = tableName,
                    table_name_c = tableInfo.table_name_c
                };

                //判断是否新增的表
                if (targetTableNameList.All(t => t.table_name != tableName))
                {
                    ddc.TableStatus = TableStatusEnum.Created;
                }
                else
                {
                    var fieldCompareList = dicSourceList.Where(t => t.table_name == tableName).ToList();
                    bool isSame = compareField(ref fieldCompareList, dicTargetList.Where(t => t.table_name == tableName).ToList());
                    ddc.FieldCompareList = fieldCompareList;
                    ddc.TableStatus = isSame ? TableStatusEnum.None : TableStatusEnum.Update;
                }

                dataDictCompares.Add(ddc);
            }

            foreach (var targetTableInfo in targetTableNameList)
            {
                //判断是否删除的表
                if (sourceTableNameList.All(t => t.table_name != targetTableInfo.table_name))
                {
                    DataDictCompare ddc = new DataDictCompare
                    {
                        table_name = targetTableInfo.table_name,
                        table_name_c = targetTableInfo.table_name_c,
                        TableStatus = TableStatusEnum.Deleted
                    };

                    dataDictCompares.Add(ddc);
                }
            }

            List<DataDictCompare> dicSourceCompareListTemp = new List<DataDictCompare>();
            if (dataDictCompares.Count > 0)
            {
                dicSourceCompareListTemp = dataDictCompares.Where(t => t.TableStatus != TableStatusEnum.None).OrderBy(t => t.table_name).ToList();
                for (int i = 0; i < dicSourceCompareListTemp.Count; i++)
                {
                    dicSourceCompareListTemp[i].RowNum = i + 1;
                }
            }

            return dicSourceCompareListTemp;
        }

        /// <summary>
        /// 比较数据字典是否相等
        /// </summary>
        /// <param name="sourceList">源</param>
        /// <param name="targetList">目标</param>
        /// <returns></returns>
        private bool compareField(ref List<FieldCompare> sourceList, List<FieldCompare> targetList)
        {
            bool isSame = true;

            List<FieldCompare> compareResultList = new List<FieldCompare>();
            foreach (FieldCompare source in sourceList)
            {
                //判断字段是否存在
                DataDict target = targetList.FirstOrDefault(t => t.field_name == source.field_name);
                if (target == null)
                {
                    source.FieldStatus = FieldStatusEnum.Created;
                    compareResultList.Add(source);
                }
                else
                {
                    //如果字段的数据类型、长度、小数位有一个不相同，则说明有修改字段
                    if (source.data_type != target.data_type || source.width != target.width || source.DecimalPrecision != target.DecimalPrecision)
                    {
                        source.FieldStatus = FieldStatusEnum.Update;
                        compareResultList.Add(source);
                    }
                }
            }

            //反向查询，是否有删字段
            foreach (FieldCompare target in targetList)
            {
                DataDict source = sourceList.FirstOrDefault(t => t.field_name == target.field_name);
                if (source == null)
                {
                    target.FieldStatus = FieldStatusEnum.Deleted;
                    compareResultList.Add(target);
                }
            }

            if (compareResultList.Count > 0)
            {
                isSame = false;
                sourceList = compareResultList;
            }

            return isSame;
        }

        private readonly string sqlVersion = "USE [{0}];SELECT [ApplicationName],[Application],[Version] FROM [myApplication] WHERE [ApplicationName]='销售系统' AND [Application]='0011' AND [Version] LIKE '1.0%'";

        private readonly string sqlHigher = "USE [{0}];SELECT [Guid],[TableGUID],[table_name],[table_name_c],[field_name],[field_name_c],[data_type],[width],[DecimalPrecision],[Description] FROM [data_dict]";

        private readonly string sql = "USE [{0}];SELECT [Guid],[TableGUID],[table_name],[table_name_c],[field_name],[field_name_c],[data_type],[width],[defaultvalue],[Description] FROM [data_dict]";

        #endregion

        #region 导出Excel

        private static IWorkbook _workBook;
        private static ISheet _sheet1;
        private static ISheet _sheet2;
        private static int _row;

        //标题单元格样式
        private ICellStyle titleStyle;
        //普通单元格边框样式
        private ICellStyle borderStyle;
        //标题字体样式对象
        private IFont titleFont;

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

            setTitleSyle();

            //行号标识重置
            _row = 0;

            _sheet1 = _workBook.CreateSheet("数据结构升级清单");
            _sheet1.SetColumnWidth(0, (28 * 256));
            _sheet1.SetColumnWidth(1, (28 * 256));
            _sheet1.SetColumnWidth(2, (13 * 256));

            //设置标题
            exportTitle();

            //导出第一张表
            for (int i = 0; i < dicSourceCompareList.Count; i++)
            {
                DataDictCompare tableInfo = dicSourceCompareList[i];
                IRow row1 = _sheet1.CreateRow(i + _row);
                //新建单元格
                ICell cell = row1.CreateCell(0);
                // 单元格赋值
                cell.SetCellValue(tableInfo.table_name_c);
                //新建单元格
                ICell cell1 = row1.CreateCell(1);

                cell1.SetCellValue(tableInfo.table_name);
                //枚举值
                ICell cell2 = row1.CreateCell(2);

                string statusStr = "";
                if (tableInfo.TableStatus == TableStatusEnum.Created)
                    statusStr = "新增";
                else if (tableInfo.TableStatus == TableStatusEnum.Update)
                    statusStr = "修改";
                else if (tableInfo.TableStatus == TableStatusEnum.Deleted)
                    statusStr = "删除";

                cell2.SetCellValue(statusStr);
            }

            //行号标识重置
            _row = 0;

            //导出第二张表
            _sheet2 = _workBook.CreateSheet("字段修改清单");
            _sheet2.SetColumnWidth(0, (31 * 256));
            _sheet2.SetColumnWidth(1, (22 * 256));
            _sheet2.SetColumnWidth(2, (22 * 256));
            _sheet2.SetColumnWidth(3, (30 * 256));

            var sourceList = dicSourceCompareList.Where(t => t.TableStatus == TableStatusEnum.Update).ToList();
            foreach (DataDictCompare sourceTable in sourceList)
            {
                exportTitle2(sourceTable);
                export(sourceTable);
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
            cell.SetCellValue("中文名称");
            cell.CellStyle = titleStyle;

            //新建单元格
            ICell cell1 = header.CreateCell(1);
            cell1.SetCellValue("表名");
            cell1.CellStyle = titleStyle;

            //枚举值
            ICell cell2 = header.CreateCell(2);
            cell2.SetCellValue("升级类型");
            cell2.CellStyle = titleStyle;

            _row += 1;
        }

        /// <summary>
        /// 设置标题
        /// </summary>
        private void exportTitle2(DataDictCompare tableInfo)
        {
            IRow header = _sheet2.CreateRow(_row);
            header.Height = 25 * 20;
            //新建单元格
            ICell cell = header.CreateCell(0);
            // 单元格赋值
            cell.SetCellValue(tableInfo.table_name_c);
            cell.CellStyle = titleStyle;

            //新建单元格
            ICell cell1 = header.CreateCell(1);
            cell1.SetCellValue(tableInfo.table_name);
            cell1.CellStyle = titleStyle;

            ICell cell2 = header.CreateCell(2);
            cell2.CellStyle = titleStyle;
            ICell cell3 = header.CreateCell(3);
            cell3.CellStyle = titleStyle;

            //设置一个合并单元格区域，使用上下左右定义CellRangeAddress区域
            //CellRangeAddress四个参数为：起始行，结束行，起始列，结束列
            _sheet2.AddMergedRegion(new CellRangeAddress(_row, _row, 1, 3));

            _row += 1;
        }

        //设置行
        private void export(DataDictCompare dataDictCompare)
        {
            //获取枚举类
            List<FieldCompare> dataDictList = dataDictCompare.FieldCompareList;

            //新增字段
            var createdFieldList = dataDictList.Where(t => t.FieldStatus == FieldStatusEnum.Created).ToList();
            if (createdFieldList.Count > 0)
            {
                // 创建新增行
                for (var i = _row; i < createdFieldList.Count + _row; i++)
                {
                    FieldCompare fieldInfo = createdFieldList[i - _row];
                    IRow row1 = _sheet2.CreateRow(i);
                    //新建单元格
                    ICell cell = row1.CreateCell(0);
                    // 单元格赋值
                    cell.SetCellValue("新增字段");
                    cell.CellStyle = titleStyle;

                    //新建单元格-字段名称
                    ICell cell1 = row1.CreateCell(1);
                    cell1.SetCellValue(fieldInfo.field_name);
                    cell1.CellStyle = borderStyle;

                    //新建单元格-字段类型
                    ICell cell2 = row1.CreateCell(2);
                    string fieldType = getFieldType(fieldInfo);
                    cell2.SetCellValue(fieldType);
                    cell2.CellStyle = borderStyle;

                    //新建单元格-字段中文名称
                    ICell cell3 = row1.CreateCell(3);
                    cell3.SetCellValue(fieldInfo.field_name_c);
                    cell3.CellStyle = borderStyle;
                }

                _row += createdFieldList.Count;
            }

            //更新字段
            var updatedFieldList = dataDictList.Where(t => t.FieldStatus == FieldStatusEnum.Update).ToList();
            if (updatedFieldList.Count > 0)
            {
                // 创建新增行
                for (var i = _row; i < updatedFieldList.Count + _row; i++)
                {
                    FieldCompare fieldInfo = updatedFieldList[i - _row];
                    IRow row1 = _sheet2.CreateRow(i);
                    //新建单元格
                    ICell cell = row1.CreateCell(0);
                    // 单元格赋值
                    cell.SetCellValue("更新字段");
                    cell.CellStyle = titleStyle;

                    //新建单元格-字段名称
                    ICell cell1 = row1.CreateCell(1);
                    cell1.SetCellValue(fieldInfo.field_name);
                    cell1.CellStyle = borderStyle;

                    //新建单元格-字段类型
                    ICell cell2 = row1.CreateCell(2);
                    string fieldType = getFieldType(fieldInfo);
                    cell2.SetCellValue(fieldType);
                    cell2.CellStyle = borderStyle;

                    //新建单元格-字段中文名称
                    ICell cell3 = row1.CreateCell(3);
                    cell3.SetCellValue(fieldInfo.field_name_c);
                    cell3.CellStyle = borderStyle;
                }

                _row += updatedFieldList.Count;
            }

            //删除字段
            var deletedFieldList = dataDictList.Where(t => t.FieldStatus == FieldStatusEnum.Deleted).ToList();
            if (deletedFieldList.Count > 0)
            {
                // 创建新增行
                for (var i = _row; i < deletedFieldList.Count + _row; i++)
                {
                    FieldCompare fieldInfo = deletedFieldList[i - _row];
                    IRow row1 = _sheet2.CreateRow(i);
                    //新建单元格
                    ICell cell = row1.CreateCell(0);
                    // 单元格赋值
                    cell.SetCellValue("删除字段");
                    cell.CellStyle = titleStyle;

                    //新建单元格-字段名称
                    ICell cell1 = row1.CreateCell(1);
                    cell1.SetCellValue(fieldInfo.field_name);
                    cell1.CellStyle = borderStyle;

                    //新建单元格-字段类型
                    ICell cell2 = row1.CreateCell(2);
                    string fieldType = getFieldType(fieldInfo);
                    cell2.SetCellValue(fieldType);
                    cell2.CellStyle = borderStyle;

                    //新建单元格-字段中文名称
                    ICell cell3 = row1.CreateCell(3);
                    cell3.SetCellValue(fieldInfo.field_name_c);
                    cell3.CellStyle = borderStyle;
                }

                _row += deletedFieldList.Count;
            }

            _row += 2;
        }

        private string getFieldType(FieldCompare fieldInfo)
        {
            string fieldType = fieldInfo.data_type;

            if (fieldInfo.width > 0)
            {
                if (fieldInfo.DecimalPrecision > 0)
                {
                    fieldType = string.Format(fieldType + "({0},{1})", fieldInfo.width, fieldInfo.DecimalPrecision);
                }
                else
                {
                    fieldType = string.Format(fieldType + "({0})", fieldInfo.width);
                }
            }

            return fieldType;
        }

        private void setTitleSyle()
        {
            //标题单元格样式
            titleStyle = _workBook.CreateCellStyle();
            borderStyle = _workBook.CreateCellStyle();

            //标题字体样式对象
            titleFont = _workBook.CreateFont();

            //设置单元格的样式：水平对齐居中
            titleStyle.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center;
            titleStyle.VerticalAlignment = VerticalAlignment.Center;
            titleStyle.FillPattern = FillPattern.SolidForeground;
            titleStyle.FillForegroundColor = HSSFColor.Grey25Percent.Index;

            //设置边框
            titleStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            titleStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            titleStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            titleStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;

            borderStyle.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            borderStyle.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            borderStyle.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            borderStyle.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;

            //设置字体加粗样式
            titleFont.Boldweight = short.MaxValue;
            //设置字体大小
            titleFont.FontHeight = 12;
            //使用SetFont方法将字体样式添加到单元格样式中 
            titleStyle.SetFont(titleFont);
        }

        #endregion
    }
}
