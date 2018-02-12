using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace View45
{
    public partial class SearchForm : Form
    {
        private readonly EnumExportForm _parent;
        private bool _isPrevious;
        private bool _isCase;
        private bool _isWhole;
        private readonly string _content;
        private string _keyWord = string.Empty;
        private Match _oldMatch;
        private Match _match;
        Search searchHelper;

        private SearchForm(EnumExportForm parent)
        {
            _parent = parent;
            _content = _parent.txtContent.Text;

            InitializeComponent();
        }

        private static SearchForm _instance;
        public static SearchForm GetInstance(EnumExportForm parent)
        {
            //判断是否存在该窗体,或时候该字窗体是否被释放过,如果不存在该窗体,则 new 一个新窗体  
            if (_instance == null || _instance.IsDisposed)
            {
                _instance = new SearchForm(parent);
            }
            return _instance;
        }

        private void txtKey_TextChanged(object sender, EventArgs e)
        {
            hideNext();

            _keyWord = txtKey.Text;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_keyWord))
            {
                MessageBox.Show("请输入要查询的关键字");
                return;
            }

            if (_isPrevious)
            {
                //查询上一个
                _match = searchHelper.GetMatch(true);
                if (_match != null && _match == _oldMatch)
                {
                    MessageBox.Show("查找到达了搜索的起点");
                }
            }
            else
            {
                //查询
                searchHelper = new Search(_content, _keyWord, 0, false, _isCase, _isWhole);
                _match = searchHelper.GetSearchInfo();
            }

            showMatch();
            if (_match != null)
            {
                showNext();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            selectNext();
        }

        private void txtKey_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                selectNext();

                txtKey.Focus();
            }
        }

        private void cbCase_CheckedChanged(object sender, EventArgs e)
        {
            _isCase = cbCase.Checked;
            hideNext();
        }

        private void cbWhole_CheckedChanged(object sender, EventArgs e)
        {
            _isWhole = cbWhole.Checked;
            hideNext();
        }

        #region 私有方法

        private void showNext()
        {
            btnSearch.Text = "上一个";
            _isPrevious = true;
            btnNext.Visible = true;
        }

        private void hideNext()
        {
            btnSearch.Text = "查找";
            _isPrevious = false;
            btnNext.Visible = false;
        }

        private void selectNext()
        {
            if (searchHelper == null)
            {
                //查询
                searchHelper = new Search(_content, _keyWord, 0, false, _isCase, _isWhole);
                _match = searchHelper.GetSearchInfo();
                
                if (_match != null)
                {
                    showNext();
                }
            }
            else
            {
                _match = searchHelper.GetMatch();
                if (_match != null && _match == _oldMatch)
                {
                    MessageBox.Show("查找到达了搜索的终点");
                }
            }

            showMatch();
        }

        private void showMatch()
        {
            if (_match != null)
            {
                _oldMatch = _match;
                _parent.txtContent.Select(_match.Index, _keyWord.Length);
                //让文本框获取焦点 
                _parent.txtContent.Focus();
                //滚动到控件光标处 
                _parent.txtContent.ScrollToCaret();
                Application.DoEvents();
            }
            else
            {
                MessageBox.Show("未找到指定文本");
                hideNext();
            }
        }

        #endregion
    }
}
