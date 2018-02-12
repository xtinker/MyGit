using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace View45
{
    public class Search
    {
        /// <summary>
        /// 查询内容
        /// </summary>
        private string _content;
        /// <summary>
        /// 检索关键字
        /// </summary>
        private string _searchKey;
        /// <summary>
        /// 是否向上查询
        /// </summary>
        private bool _isPrevious;
        /// <summary>
        /// 是否区分大小写
        /// </summary>
        private bool _isCase;
        /// <summary>
        /// 是否全字匹配
        /// </summary>
        private bool _isWhole;
        /// <summary>
        /// 检索超始位置
        /// </summary>
        private int _beginIndex;

        public int MatchIndex;
        public MatchCollection Matches;

        public Search(string content, string searchKey, int beginIndex = 0, bool isPrevious = false, bool isCase = false, bool isWhole = false)
        {
            _content = content;
            _searchKey = searchKey;
            _beginIndex = beginIndex;
            _isPrevious = isPrevious;
            _isCase = isCase;
            _isWhole = isWhole;
        }

        public Match GetSearchInfo()
        {
            Match match = null;
            string pattern = _searchKey.Replace("\\","\\\\");
            if (_isWhole)
            {
                pattern = $@"\b{_searchKey}\b";
            }

            Matches = Regex.Matches(_content, pattern, _isCase ? RegexOptions.None : RegexOptions.IgnoreCase);
            if (Matches != null && Matches.Count > 0)
            {
                match = Matches[0];
            }
            return match;
        }

        public Match GetMatch(bool isPrevious = false)
        {
            Match match = null;
            if (Matches != null)
            {
                if (isPrevious)
                {
                    MatchIndex--;
                    if (MatchIndex >= 0)
                    {
                        match = Matches[MatchIndex];
                    }
                    else
                    {
                        MatchIndex = 0;
                        match = Matches[MatchIndex];
                    }
                }
                else
                {
                    MatchIndex++;
                    if (MatchIndex < Matches.Count)
                    {
                        match = Matches[MatchIndex];
                    }
                    else
                    {
                        MatchIndex = Matches.Count - 1;
                        match = Matches[MatchIndex];
                    }
                }
            }
            return match;
        }

        public Match GetMatchByIndex(int index)
        {
            Match match = null;
            if (Matches != null)
            {
                match = Matches[index];
            }
            return match;
        }
    }

    public class SearchInfo
    {
        /// <summary>
        /// 关键字起始位置
        /// </summary>
        public int SelectionStart { get; set; }

        /// <summary>
        /// 关键字长度
        /// </summary>
        public int SelectionLength { get; set; }

        /// <summary>
        /// 新光标位置
        /// </summary>
        public int NewIndex => SelectionStart + SelectionLength;
    }
}
